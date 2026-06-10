using database.models;
using DevetionStudetns.DTO.Hospital;

namespace DevetionStudetns.Mappers.HospitalMappier
{
    public static class AddHospitalMapper
    {
        public static Hospital AddHos(this HospitalDto dto)
        {
            return new Hospital
            {
                HospitalName = dto.HospitalName,
                Location = dto.Location,
                ContactNumber = dto.CountantNumber,
                HospitalCapacity = dto.HospitalCapacity,
            };
        }

        public static GetHospitalDto GetHos(this Hospital dto)
        {
            return new GetHospitalDto
            {
                HospitalId = dto.HospitalId,
                HospitalName = dto.HospitalName,
                Location = dto.Location,
                CountantNumber = dto.ContactNumber,
                HospitalCapacity = dto.HospitalCapacity,
            };
        }
    }
}
