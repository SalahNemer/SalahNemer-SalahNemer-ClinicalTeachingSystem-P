using DataBase.DBcon;
using DataBase.entitys;
using DevetionStudetns.NewFolder;
using FinalProject.Interface.IRepositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using testDtoAndmapper.Entity;
using V1.DTO.DivisionAiDTO;

namespace V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionAiController : ControllerBase
    {

        private readonly DBC _context;

        public DivisionAiController(DBC context)
        {
            _context = context;
        }
        //تحسين
        [HttpPost("ai-distribute/{mainGroupId}")]
        public async Task<IActionResult> AIDistributeStudents(int mainGroupId, [FromBody] DivisionAiRequest2 request)
        {
            var mainGroup = await _context.mainGrops
                .Include(m => m.SubGroups)
                .ThenInclude(sg => sg.divisions)
                .FirstOrDefaultAsync(m => m.MainGroupId == mainGroupId);

            if (mainGroup == null)
                return NotFound("Main group not found");

            int totalRequestedPerGroup = request.MaleCountPerGroup + request.FemaleCountPerGroup;

            if (totalRequestedPerGroup > 9)
            {
                return BadRequest($"الحد الأقصى المسموح به لعدد الطلاب في المجموعة الفرعية هو 9. لقد قمت بطلب {totalRequestedPerGroup} طلاب.");
            }

            var assignedStudentIds = await _context.Divisions
                .Select(d => d.StudentId)
                .Distinct()
                .ToListAsync();

            var students = await _context.students
                .Include(s => s.User)
                .Where(s => s.StudentLevel == mainGroup.AcademicYearId && s.User != null && !assignedStudentIds.Contains(s.UserId))
                .ToListAsync();

            var females = students
                .Where(s => s.User.Gender != null &&
                            (s.User.Gender.Trim().ToLower() == "أنثى" || s.User.Gender.Trim().ToLower() == "female"))
                .OrderByDescending(s => s.CumulativeAverage)
                .ToList();

            var males = students
                .Where(s => s.User.Gender != null &&
                            (s.User.Gender.Trim().ToLower() == "ذكر" || s.User.Gender.Trim().ToLower() == "male"))
                .OrderByDescending(s => s.CumulativeAverage)
                .ToList();

            int requiredMales = request.MaleCountPerGroup * request.GroupCount;
            int requiredFemales = request.FemaleCountPerGroup * request.GroupCount;

            if (males.Count < requiredMales && females.Count < requiredFemales)
            {
                return BadRequest($"لا يوجد عدد كافٍ من الذكور والإناث:\n- الذكور المتوفرين: {males.Count} من أصل {requiredMales}\n- الإناث المتوفرات: {females.Count} من أصل {requiredFemales}");
            }
            else if (males.Count < requiredMales)
            {
                return BadRequest($"لا يوجد عدد كافٍ من الذكور. العدد المتوفر حاليًا: {males.Count} من أصل {requiredMales} مطلوب.");
            }
            else if (females.Count < requiredFemales)
            {
                return BadRequest($"لا يوجد عدد كافٍ من الإناث. العدد المتوفر حاليًا: {females.Count} من أصل {requiredFemales} مطلوب.");
            }

            int subGroupNumber = mainGroup.SubGroups?.Count() ?? 0;

            var allGPAs = new List<double>();
            int totalStudents = 0;
            double totalGenderBalance = 0;
            int newlyCreatedGroups = 0;

            for (int i = 0; i < request.GroupCount; i++)
            {
                var groupFemales = females.Take(request.FemaleCountPerGroup).ToList();
                var groupMales = males.Take(request.MaleCountPerGroup).ToList();

                if (groupFemales.Count < request.FemaleCountPerGroup || groupMales.Count < request.MaleCountPerGroup)
                {
                    break;
                }

                var subGroup = new SubGroup
                {
                    SubGroupSympole = $"{mainGroup.MainGroupSympole}-{subGroupNumber + i+1}",
                    MainGroupId = mainGroupId,
                    NumberOfStudetns = request.FemaleCountPerGroup + request.MaleCountPerGroup
                };

                _context.subGroups.Add(subGroup);
                await _context.SaveChangesAsync();

                females.RemoveRange(0, groupFemales.Count);
                males.RemoveRange(0, groupMales.Count);

                int femaleCountInGroup = groupFemales.Count;
                int maleCountInGroup = groupMales.Count;

                var groupStudents = groupFemales.Concat(groupMales).ToList();
                totalStudents += groupStudents.Count;
                allGPAs.AddRange(groupStudents.Select(s => s.CumulativeAverage));

                foreach (var student in groupStudents)
                {
                    _context.Divisions.Add(new Division
                    {
                        StudentId = student.UserId,
                        SubGroupId = subGroup.SubGroupId
                    });
                }

                await _context.SaveChangesAsync();

                int totalGroupStudents = femaleCountInGroup + maleCountInGroup;
                double genderBalance = 0;

                if (totalGroupStudents > 0)
                {
                    double malePercentage = (maleCountInGroup / (double)totalGroupStudents) * 100;
                    double femalePercentage = (femaleCountInGroup / (double)totalGroupStudents) * 100;
                    genderBalance = 100 - Math.Abs(malePercentage - femalePercentage);
                }

                totalGenderBalance += genderBalance;
                newlyCreatedGroups++;
            }

            double averageGPA = allGPAs.Count > 0 ? allGPAs.Average() : 0;
            double averageGenderBalance = newlyCreatedGroups > 0 ? totalGenderBalance / newlyCreatedGroups : 0;
            int totalSubGroupsNow = await _context.subGroups.CountAsync(sg => sg.MainGroupId == mainGroupId);

            return Ok(new
            {
                message = "تم تنفيذ توزيع ذكي باستخدام AI",
                newlyCreatedGroups,
                totalSubGroupsNow,
                totalStudentsDistributed = totalStudents,
                averageGPA = Math.Round(averageGPA, 2),
                genderBalance = Math.Round(averageGenderBalance, 2)
            });
        }

    }
}
