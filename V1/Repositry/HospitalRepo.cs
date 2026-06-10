using database.models;
using DataBase.DBcon;
using DevetionStudetns.DTO.Hospital;
using DevetionStudetns.Error.SuccessfullyMsg;
using DevetionStudetns.Interface;
using DevetionStudetns.Mappers.HospitalMappier;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.EntityFrameworkCore;

namespace DevetionStudetns.Repositry.HospitalRepostry
{
    public class HospitalRepo : IHospital
    {
        private readonly DBC _context;
        public HospitalRepo(DBC context)
        {
            _context = context;
        }

        public async Task<GeneralMsgDto> AddHospital(HospitalDto hospitalDto)
        {
            var hospital =await _context.hospitals.FirstOrDefaultAsync(u => u.HospitalName == hospitalDto.HospitalName && u.HospitalCapacity == hospitalDto.HospitalCapacity && u.Location == hospitalDto.Location && u.ContactNumber == hospitalDto.CountantNumber);
            var HOSPITAL = await _context.hospitals.FirstOrDefaultAsync(U => U.HospitalName == hospitalDto.HospitalName && U.Location == hospitalDto.Location);
            var hospitalPhone = await _context.hospitals.FirstOrDefaultAsync(hp => hp.ContactNumber == hospitalDto.CountantNumber);
            try
            {
                if (hospital == null)
                {
                    if (HOSPITAL == null)
                    {
                        if (hospitalPhone == null)
                        {
                            if (hospitalDto.HospitalCapacity > 0 && hospitalDto.HospitalCapacity < 500)
                            {
                                _context.hospitals.Add(hospitalDto.AddHos());
                                await _context.SaveChangesAsync();
                                GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                    SuccessfullyMsgs.Added_successfully,
                                                    "Success",
                                                    "Success"
                                                  );
                                return SucMsg;
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.Hospital_Capacity_Must_Be_Greater_Than_0_And_Less_Than_500,
                                    "Enter the requird filled",
                                    "Ther is not any data to Update it "
                                   );
                                return ErrorMsg;
                            }
                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Duplicate_Phone_Number,
                                "Enter the requird filled",
                                "Ther is not any data to Update it "
                               );
                            return ErrorMsg;
                        }

                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.Hospital_with_the_same_name_already_exists,
                            "Enter the requird filled",
                            "Ther is not any data to Update it "
                            );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                        IErrorMsgs.DUPLICATE_RECORD_RERRO,
                          "Enter the requird filled",
                          "Ther is not any data to Update it "
                         );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }

        public async Task<GeneralMsgDto> DeleteHospital(int id)
        {
            var hospital =await _context.hospitals.FirstOrDefaultAsync(u => u.HospitalId == id);
            try
            {
                if (hospital != null)
                {
                    _context.hospitals.Remove(hospital);
                    await _context.SaveChangesAsync();
                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                        SuccessfullyMsgs.The_operation_was_completed_successfully,
                                        "Success",
                                        "Success"
                                       );
                    return SucMsg;
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.The_hospital_does_not_exist,
                                        "Enter the requird filled",
                                         "Ther is not any data to Update it "
                                     );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }

        public async Task<List<GetHospitalDto>> GetHospital()
        {
            try
            {
                var hospitals = await _context.hospitals
                    .Select(h => new GetHospitalDto
                    {
                        HospitalId = h.HospitalId,
                        HospitalName = h.HospitalName,
                        Location = h.Location,
                        CountantNumber = h.ContactNumber,
                        HospitalCapacity = h.HospitalCapacity,
                    })
                    .ToListAsync();

                return hospitals;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }
        public async Task<List<GetHospitalDto>> GetHospitalById(int hospitalId)
        {
            try
            {
                var hospitals = await _context.hospitals     
                    .Where(h => h.HospitalId == hospitalId)
                    .Select(h => new GetHospitalDto
                    {
                        HospitalId = h.HospitalId,
                        HospitalName = h.HospitalName,
                        Location = h.Location,
                        CountantNumber = h.ContactNumber,
                        HospitalCapacity = h.HospitalCapacity,
                    })
                    .ToListAsync();

                return hospitals;
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }

        }



        public async Task<GeneralMsgDto> UpdateHospitalData(int id, HospitalDto dto)
        {
            var hospitalID = await _context.hospitals.FirstOrDefaultAsync(u => u.HospitalId == id );
            var hospital = await _context.hospitals.FirstOrDefaultAsync(u => u.HospitalName == dto.HospitalName && u.HospitalCapacity == dto.HospitalCapacity && u.Location == dto.Location && u.ContactNumber == dto.CountantNumber );
            var HOSPITAL = await _context.hospitals.FirstOrDefaultAsync(U => U.HospitalName == dto.HospitalName && U.Location == dto.Location && U.HospitalId != id);
            var hospitalPhone = await _context.hospitals.FirstOrDefaultAsync(hp => hp.ContactNumber == dto.CountantNumber && hp.HospitalId != id);
            try
            {
                if (hospitalID != null)
                {
                    if (hospital == null)
                    {
                        if (HOSPITAL == null)
                        {
                            if (hospitalPhone == null)
                            {
                                if (dto.HospitalCapacity > 0 && dto.HospitalCapacity < 200)
                                {
                                    hospitalID.HospitalName = dto.HospitalName;
                                    hospitalID.Location = dto.Location;
                                    hospitalID.ContactNumber = dto.CountantNumber;
                                    hospitalID.HospitalCapacity = dto.HospitalCapacity;
                                    _context.hospitals.Update(hospitalID);
                                    await _context.SaveChangesAsync();
                                    GeneralMsgDto SucMsg = new GeneralMsgDto(
                                                       SuccessfullyMsgs.The_operation_was_completed_successfully,
                                                       "Success",
                                                       "Success"
                                                      );
                                    return SucMsg;
                                }
                                else
                                {
                                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                        IErrorMsgs.Hospital_Capacity_Must_Be_Greater_Than_0_And_Less_Than_500,
                                        "Enter the requird filled",
                                        "Ther is not any data to Update it "
                                       );
                                    return ErrorMsg;
                                }
                            }
                            else
                            {
                                GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                    IErrorMsgs.Duplicate_Phone_Number,
                                    "Enter the requird filled",
                                    "Ther is not any data to Update it "
                                   );
                                return ErrorMsg;
                            }

                        }
                        else
                        {
                            GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                                IErrorMsgs.Hospital_with_the_same_name_already_exists,
                                "Enter the requird filled",
                                "Ther is not any data to Update it "
                                );
                            return ErrorMsg;
                        }
                    }
                    else
                    {
                        GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                            IErrorMsgs.DUPLICATE_RECORD_RERRO,
                              "Enter the requird filled",
                              "Ther is not any data to Update it "
                             );
                        return ErrorMsg;
                    }
                }
                else
                {
                    GeneralMsgDto ErrorMsg = new GeneralMsgDto(
                    IErrorMsgs.The_hospital_does_not_exist,
                     "Enter the requird filled",
                     "Ther is not any data to Update it "
                         );
                    return ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى", ex);
            }
        }
    }
}
