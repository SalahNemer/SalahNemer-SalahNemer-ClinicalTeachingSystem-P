using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FastReport;
using DataBase.DBcon;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using testDtoAndmapper.Entity;
using FastReport.Utils;
using DevetionStudetns.NewFolder;
using V1.DTO.DistributionDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using DevetionStudetns.DTO.SubGroupDTO;  // تأكد من استخدام المكتبة الصحيحة  // تأكد من استخدام المكتبة الصحيحة
using DevetionStudetns.Mappers.SubGroupMapper;
using DataBase.entitys;
using V1.DTO.AppointmentDTO;
using database.models;

namespace V1.Controllers.CreateReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class fastReportController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly DBC _context;
        public fastReportController(IWebHostEnvironment env, DBC con)
        {
            _env = env;
            _context = con;
        }
        [HttpGet("CreateReportForSubGroupByMainGroupId/{MainGropId}")]
        public IActionResult CreateReportForSubGroupByMainGroupId(int MainGropId)
        {
            FastReport.Utils.Config.WebMode = true;

            string filePath = System.IO.Path.Combine(_env.ContentRootPath, "getAllSubGroupByMainGroupId.frx");

            FastReport.Data.MsSqlDataConnection conn = new FastReport.Data.MsSqlDataConnection();
            RegisteredObjects.AddConnection(typeof(FastReport.Data.MsSqlDataConnection));

            using (Report rep = new Report())
            using (FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport())
            {
                rep.Load(filePath);

                if (MainGropId <= 0)
                {
                    return BadRequest("⚠️ معرّف المجموعة غير صالح.");
                }

                List<SubGroup> getSubGroup = _context.subGroups
                    .Where(p => p.MainGroupId == MainGropId) 
                    .ToList();

                if (!getSubGroup.Any()) 
                {
                    return NotFound($"⚠️ لا توجد بيانات للمجموعة ذات المعرف {MainGropId}.");
                }

                string sql = @"	 
                            select 
                                b.MainGroupSympole,
                                b.AcademicYearName,
                                s.SubGroupSympole,
                                s.NumberOfStudetns
                                from SubGroup s
                                join MainGroup b on b.MainGroupId = s.MainGroupId
                                where b.MainGroupId =@MainGropId

                        ";

                var result = _context.Database.SqlQueryRaw<GetSubGroupReprotDto>(
                    sql, new SqlParameter("MainGropId", MainGropId)).ToList();

                if (!result.Any()) 
                {
                    return NotFound($"لا توجد بيانات للعرض  .");
                }

                rep.SetParameterValue("MainGroupFilter", MainGropId);

                rep.RegisterData(result, "GetAllSubGroupInOneMainGroup");
                rep.GetDataSource("GetAllSubGroupInOneMainGroup").Enabled = true;


                if (rep.Prepare())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        rep.Export(pdfExport, ms);
                        ms.Position = 0;

                        return File(ms.ToArray(), "application/pdf", $"SubGroup_Report_{MainGropId}.pdf");
                    }
                }

                return BadRequest("❌ فشل في إنشاء التقرير.");
            }
        }
        [HttpGet("CreateReportGetAllStudentsInOneMainGroup/{MainGropId}")]
        public IActionResult CreateReportGetAllStudentsInOneMainGroup(int MainGropId)
        {
            FastReport.Utils.Config.WebMode = true;

            string filePath = System.IO.Path.Combine(_env.ContentRootPath, "GetAllStudentsInOneMainGroupById.frx");

            FastReport.Data.MsSqlDataConnection conn = new FastReport.Data.MsSqlDataConnection();
            RegisteredObjects.AddConnection(typeof(FastReport.Data.MsSqlDataConnection));

            using (Report rep = new Report())
            using (FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport())
            {
                rep.Load(filePath);

                if (MainGropId <= 0)
                {
                    return BadRequest("⚠️ معرّف المجموعة غير صالح.");
                }

                List<SubGroup> getSubGroup = _context.subGroups
                    .Where(p => p.MainGroupId == MainGropId)
                    .ToList();

                if (!getSubGroup.Any())
                {
                    return NotFound($"⚠️ لا توجد بيانات للمجموعة ذات المعرف {MainGropId}.");
                }

                string sql = @"	 
                             select 
                             b.MainGroupSympole,
	                         s.SubGroupSympole,
	                         a.UserId,
	                         r.FullName
                             from SubGroup s
                             join MainGroup b on b.MainGroupId = s.MainGroupId
	                         join Divisions d on d.SubGroupId = s.SubGroupId 
	                         join Students a on a.UserId = d.StudentId 
	                         join Users r on r.UserId = a.UserId

	                         where s.MainGroupId =@MainGropId

                        ";

                var result = _context.Database.SqlQueryRaw<GetAllStudentsInOneMainGroups>(
                    sql, new SqlParameter("MainGropId", MainGropId)).ToList();
                if (!result.Any()) 
                {
                    return NotFound($"لا توجد بيانات للعرض  .");
                }


                rep.SetParameterValue("MainGroupFilter", MainGropId);

                rep.RegisterData(result, "GetAllStudentsInOneMainGroup");
                rep.GetDataSource("GetAllStudentsInOneMainGroup").Enabled = true;


                if (rep.Prepare())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        rep.Export(pdfExport, ms);
                        ms.Position = 0;

                        return File(ms.ToArray(), "application/pdf", $"SubGroup_Report_{MainGropId}.pdf");
                    }
                }

                return BadRequest("❌ فشل في إنشاء التقرير.");
            }
        }
        [HttpGet("CreateReportGetTheDistrbutionDataByRotationIdAndMainGroupId/{MainGropId}/{RotationId}")]
        public IActionResult CreateReportGetTheDistrbutionDataByRotationIdAndMainGroupId(int MainGropId, int RotationId)
        {
            FastReport.Utils.Config.WebMode = true;

            string filePath = System.IO.Path.Combine(_env.ContentRootPath, "GetDistrbutionByRotaionIdAndMainGroupId.frx");

            FastReport.Data.MsSqlDataConnection conn = new FastReport.Data.MsSqlDataConnection();
            RegisteredObjects.AddConnection(typeof(FastReport.Data.MsSqlDataConnection));

            using (Report rep = new Report())
            using (FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport())
            {
                rep.Load(filePath);

                if (MainGropId <= 0)
                {
                    return BadRequest("⚠️ معرّف المجموعة غير صالح.");
                }

                List<SubGroup> getSubGroup = _context.subGroups
                    .Where(p => p.MainGroupId == MainGropId)
                    .ToList();

                if (!getSubGroup.Any())
                {
                    return NotFound($"⚠️ لا توجد بيانات للمجموعة ذات المعرف {MainGropId}.");
                }

                string sql = @"	        
                            select 
                            m.MainGroupSympole ,
                            s.SubGroupSympole,
                            a.WeekName,
                            a.StartSessionDate,
                            a.EndSessionDate,
                            u.FullName,
                            c.CourseName,
                            h.HospitalName
                            from Distributions d 
                            join SubGroup s on s.SubGroupId = d.SubGroupId
                            join MainGroup m on m.MainGroupId = s.MainGroupId
                            join Appointments a on a.AppointmentId = d.AppointmentId
                            join Doctors dc on dc.UserId = d.DoctorId 
                            join Users u on u.UserId = dc.UserId
                            join Course c on c.CouresId = d.CourseId 
                            join Hospitals h on h.HospitalId = dc.HospitalId
                            where s.MainGroupId =@MainGropId
                            and d.RotationId =@RotationId
                        ";

                var result = _context.Database.SqlQueryRaw<GetDistrbutionInOneMainGroupByMainGruopIdAndRotationIdDto>(
                    sql, new SqlParameter("MainGropId", MainGropId), new SqlParameter("RotationId", RotationId)).ToList();


                if (!result.Any()) 
                {
                    return NotFound($"لا توجد بيانات للعرض  .");
                }

                rep.SetParameterValue("MainGroupFilter", MainGropId);
                rep.SetParameterValue("RotationIdFilter", RotationId);

                rep.RegisterData(result, "GetDistrbutionByRotaionIdAndMainGroupId");
                rep.GetDataSource("GetDistrbutionByRotaionIdAndMainGroupId").Enabled = true;


                if (rep.Prepare())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        rep.Export(pdfExport, ms);
                        ms.Position = 0;

                        return File(ms.ToArray(), "application/pdf", $"SubGroup_Report_{MainGropId}.pdf");
                    }
                }


                return BadRequest("❌ فشل في إنشاء التقرير.");
            }
        }

        [HttpGet("CreateReportGetTheDistrbutionDataByRotationIdAndSubGroupId/{SubGroupId}/{RotationId}")]
        public IActionResult CreateReportGetTheDistrbutionDataByRotationIdAndSubGroupId(int SubGroupId, int RotationId)
        {
            FastReport.Utils.Config.WebMode = true;

            string filePath = System.IO.Path.Combine(_env.ContentRootPath, "GetDistrbutionByRotaionIdAndSubGroupId.frx");

            FastReport.Data.MsSqlDataConnection conn = new FastReport.Data.MsSqlDataConnection();
            RegisteredObjects.AddConnection(typeof(FastReport.Data.MsSqlDataConnection));

            using (Report rep = new Report())
            using (FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport())
            {
                rep.Load(filePath);

                if (SubGroupId <= 0)
                {
                    return BadRequest("⚠️ معرّف المجموعة غير صالح.");
                }

                List<SubGroup> getSubGroup = _context.subGroups
                    .Where(p => p.SubGroupId == SubGroupId)
                    .ToList();

                if (!getSubGroup.Any())
                {
                    return NotFound($"⚠️ لا توجد بيانات للمجموعة ذات المعرف {SubGroupId}.");
                }

                string sql = @"	        
                            select 
                            m.MainGroupSympole ,
                            s.SubGroupSympole,
                            a.WeekName,
                            a.StartSessionDate,
                            a.EndSessionDate,
                            u.FullName,
                            c.CourseName,
                            h.HospitalName
                            from Distributions d 
                            join SubGroup s on s.SubGroupId = d.SubGroupId
                            join MainGroup m on m.MainGroupId = s.MainGroupId
                            join Appointments a on a.AppointmentId = d.AppointmentId
                            join Doctors dc on dc.UserId = d.DoctorId 
                            join Users u on u.UserId = dc.UserId
                            join Course c on c.CouresId = d.CourseId 
                            join Hospitals h on h.HospitalId = dc.HospitalId
                            where s.SubGroupId =@SubGroupId
                            and d.RotationId =@RotationId
                        ";

                var result = _context.Database.SqlQueryRaw<GetDistrbutionInOneMainGroupByMainGruopIdAndRotationIdDto>(
                    sql, new SqlParameter("SubGroupId", SubGroupId), new SqlParameter("RotationId", RotationId)).ToList();

                if (!result.Any()) 
                {
                    return NotFound($"لا توجد بيانات للعرض  .");
                }

                rep.SetParameterValue("SubGruopFilter", SubGroupId);
                rep.SetParameterValue("RotationIdFilter", RotationId);

                rep.RegisterData(result, "GetDistrbutionByRotaionIdAndSubGroupId");
                rep.GetDataSource("GetDistrbutionByRotaionIdAndSubGroupId").Enabled = true;


                if (rep.Prepare())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        rep.Export(pdfExport, ms);
                        ms.Position = 0;

                        return File(ms.ToArray(), "application/pdf", $"SubGroup_Report_{SubGroupId}.pdf");
                    }
                }


                return BadRequest("❌ فشل في إنشاء التقرير.");
            }
        }

        [HttpGet("CreateReportGetTheDistrbutionDataByRotationIdAndDoctorId/{DoctorId}")]
        public async Task<IActionResult> CreateReportGetTheDistrbutionDataByRotationIdAndDoctorId(string DoctorId)
        {
            FastReport.Utils.Config.WebMode = true;

            string filePath = System.IO.Path.Combine(_env.ContentRootPath, "GetDistrbutionByRotaionIdAndDoctorId.frx");

            FastReport.Data.MsSqlDataConnection conn = new FastReport.Data.MsSqlDataConnection();
            RegisteredObjects.AddConnection(typeof(FastReport.Data.MsSqlDataConnection));

            using (Report rep = new Report())
            using (FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport())
            {
                rep.Load(filePath);

                var GetAppoutmentId = @"
                                	  select 
		                                r.RotationId
		                                from Rotations r 
		                                where       
		                                 CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
		                                BETWEEN DATEADD(DAY, -7, CAST(r.StartRotationDate AS DATE))
		                                AND CAST(r.EndRotationDate AS DATE) 
                            ";
                var AppotmentResult = await _context.Rotations
                            .FromSqlRaw(GetAppoutmentId)
                            .Select(a => a.RotationId)
                            .FirstOrDefaultAsync();

                if (AppotmentResult == null) 
                {
                    return NotFound("⚠️ لا توجد فترة تدريب حالية.");
                }


                string sql = @"	        
                            select  DISTINCT
                            m.MainGroupSympole ,
                            s.SubGroupSympole,
                            a.WeekName,
                            a.StartSessionDate,
                            a.EndSessionDate,
                            u.FullName,
                            c.CourseName,
                            h.HospitalName
                            from Distributions d 
                            join SubGroup s on s.SubGroupId = d.SubGroupId
                            join MainGroup m on m.MainGroupId = s.MainGroupId
                            join Appointments a on a.AppointmentId = d.AppointmentId
                            join Doctors dc on dc.UserId = d.DoctorId 
                            join Users u on u.UserId = dc.UserId
                            join Course c on c.CouresId = d.CourseId 
                            join Hospitals h on h.HospitalId = dc.HospitalId
                            JOIN Rotations r ON r.RotationId = d.RotationId
                        	left join Divisions div on div.SubGroupId = d.SubGroupId

                            where d.DoctorId = @DoctorId
                            and d.RotationId = @AppotmentResult
                            AND d.DistributionStatus = 3
	                        and div.DivisionStatus = 3
	                         ORDER BY 
                                a.StartSessionDate ASC ;
                        ";

                var result = _context.Database.SqlQueryRaw<GetDistrbutionInOneMainGroupByMainGruopIdAndRotationIdDto>(
                    sql, new SqlParameter("DoctorId", DoctorId)
                    , new SqlParameter("AppotmentResult", AppotmentResult)).ToList();


                if (result == null || !result.Any())
                {
                    return NotFound($"⚠️ لا توجد بيانات للطبيب ذات المعرف {DoctorId}.");
                }

                rep.SetParameterValue("MainGroupFilter", DoctorId);
                rep.SetParameterValue("RotationIdFilter", AppotmentResult);

                rep.RegisterData(result, "GetDistrbutionByRotaionIdAndDoctorId");
                rep.GetDataSource("GetDistrbutionByRotaionIdAndDoctorId").Enabled = true;


                if (rep.Prepare())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        rep.Export(pdfExport, ms);
                        ms.Position = 0;

                        return File(ms.ToArray(), "application/pdf", $"doctorDistrbution_{DoctorId}.pdf");
                    }
                }


                return BadRequest("❌ فشل في إنشاء التقرير.");
            }
        }

        [HttpGet("CreateReportGetTheDistrbutionDataByRotationIdAndStudentsId/{StudentsId}")]
        public async Task<IActionResult> CreateReportGetTheDistrbutionDataByRotationIdAndStudentsId(string StudentsId)
        {
            FastReport.Utils.Config.WebMode = true;

            string filePath = System.IO.Path.Combine(_env.ContentRootPath, "GetDistrbutionByRotaionIdAndStudentsId.frx");

            FastReport.Data.MsSqlDataConnection conn = new FastReport.Data.MsSqlDataConnection();
            RegisteredObjects.AddConnection(typeof(FastReport.Data.MsSqlDataConnection));

            using (Report rep = new Report())
            using (FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport())
            {
                rep.Load(filePath);
                var GetAppoutmentId = @"
                                	  select 
		                                r.RotationId
		                                from Rotations r 
		                                where       
		                                 CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Israel Standard Time' AS DATE)
		                                BETWEEN DATEADD(DAY, -7, CAST(r.StartRotationDate AS DATE))
		                                AND CAST(r.EndRotationDate AS DATE) 
                            ";
                var AppotmentResult = await _context.Rotations
                            .FromSqlRaw(GetAppoutmentId)
                            .Select(a => a.RotationId)
                            .FirstOrDefaultAsync();
                if (AppotmentResult == null) 
                {
                    return BadRequest("⚠️ لا توجد فترة تدريب حالية.");
                }

                string sql = @"	        
                            SELECT DISTINCT
                                    m.MainGroupSympole,
                                    s.SubGroupSympole,
                                    a.WeekName,
                                    a.StartSessionDate,
                                    a.EndSessionDate,
                                    u.FullName,
                                    c.CourseName,
                                    h.HospitalName                              
                                FROM Distributions d 
                                JOIN SubGroup s ON s.SubGroupId = d.SubGroupId
                                JOIN MainGroup m ON m.MainGroupId = s.MainGroupId
                                JOIN Appointments a ON a.AppointmentId = d.AppointmentId
                                JOIN Doctors dc ON dc.UserId = d.DoctorId 
                                JOIN Users u ON u.UserId = dc.UserId
                                JOIN Course c ON c.CouresId = d.CourseId 
                                JOIN Hospitals h ON h.HospitalId = dc.HospitalId
                                JOIN Divisions div ON div.SubGroupId = s.SubGroupId
                                JOIN Students std ON std.UserId = div.StudentId 
                                JOIN Users StdD ON StdD.UserId = std.UserId
                                JOIN Rotations r ON r.RotationId = d.RotationId
                               	left join Divisions di on div.SubGroupId = d.SubGroupId

                                WHERE div.StudentId = @StudentsId
                                  	and d.RotationId = @AppotmentResult
	                                and d.DistributionStatus = 3
	                                and di.DivisionStatus = 3
	                            ORDER BY 
                                a.StartSessionDate ASC ;

                                ";

                var result = _context.Database.SqlQueryRaw<GetDistrbutionInOneMainGroupByMainGruopIdAndRotationIdDto>(
                    sql, new SqlParameter("StudentsId", StudentsId)
                    , new SqlParameter("AppotmentResult", AppotmentResult)).ToList();


                if (result == null || !result.Any())
                {
                    return NotFound($"⚠️ لا توجد بيانات للمجموعة ذات المعرف {StudentsId}.");
                }
                rep.SetParameterValue("MainGroupFilter", StudentsId);
                rep.SetParameterValue("RotationIdFilter", AppotmentResult);

                rep.RegisterData(result, "GetDistrbutionByRotaionIdAndStudentsId");
                rep.GetDataSource("GetDistrbutionByRotaionIdAndStudentsId").Enabled = true;


                if (rep.Prepare())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        rep.Export(pdfExport, ms);
                        ms.Position = 0;

                        return File(ms.ToArray(), "application/pdf", $"StduentsDistrbution_{StudentsId}.pdf");
                    }
                }


                return BadRequest("❌ فشل في إنشاء التقرير.");
            }
        }

    }
}

