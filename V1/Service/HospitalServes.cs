using database.models;
using DevetionStudetns.DTO.Hospital;
using DevetionStudetns.Interface;
using loginpage.DBcon;

namespace DevetionStudetns.Service
{
    public class HospitalServes
    {
        private readonly IHospital _hospital;
        public HospitalServes(IHospital hospital)
        {
            _hospital = hospital;
        }

        public async Task<List<GetHospitalDto>> getHospital()
        {
            return await _hospital.GetHospital();
        }

        public async Task<GeneralMsgDto> addHospital(HospitalDto hospital)
        {
            return await _hospital.AddHospital(hospital);
        }

        public async Task<GeneralMsgDto> UpdateHospital(int id, HospitalDto hospital)
        {
            return await _hospital.UpdateHospitalData(id, hospital);
        }
        public async Task<GeneralMsgDto> deleteHospital(int id)
        {
            return await _hospital.DeleteHospital(id);
        }
        public async Task<List<GetHospitalDto>> GetHospitalById(int hospitalId)
        {
            return await _hospital.GetHospitalById(hospitalId);

        }


    }
}
