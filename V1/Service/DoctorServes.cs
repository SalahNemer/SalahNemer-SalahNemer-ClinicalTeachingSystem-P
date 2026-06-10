using database.models;
using DevetionStudetns.DTO.DoctorDto;
using DevetionStudetns.Interface;
using FinalProject.DTO.DoctorDto;
using loginpage.DBcon;
using V1.DTO.DoctorDTO;

namespace DevetionStudetns.Service
{
    public class DoctorServes
    {
        private readonly IDoctor _doctor;
        public DoctorServes(IDoctor doctor)
        {
            _doctor = doctor;
        }
        public async Task<List<DoctorDto>> GetDoctors()
        {
            return await _doctor.Get();
        }

        public async Task<List<GetAllDataQDto>> GetAllDatatDoctors()
        {
            return await _doctor.Get_All_Data();
        }
        public async Task<List<GetAllDataQDto>> GetDoctorsByDepartement(int departementID)
        {
            return await _doctor.GetDoctorsByDepartement(departementID);
        }

        public async Task<GeneralMsgDto> AddDoctors(DoctorDto doctors)
        {
            return await _doctor.Add(doctors);
        }
        public async Task<GeneralMsgDto> DeleteDoctors(string id)
        {
            return await _doctor.Delete(id);
        }
        public async Task<GeneralMsgDto> UpdateDoctors(string id, UpdateDoctorDto doctors)
        {
            return await _doctor.Update(id, doctors);
        }
        public List<GetDoctorQ1Dto> GetDoctorById(string DcotroId)
        {
            return  _doctor.GetDoctorById(DcotroId);
        }

        public async Task<GeneralMsgDto> UpdateDoctorAndUserServes(UpdateDoctorAndUserDto Doctor, string DoctorIs)
        {
            return await _doctor.UpdateDoctorAndUser(Doctor, DoctorIs);
        }
    }
}
