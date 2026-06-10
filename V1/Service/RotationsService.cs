using DataBase.entitys;
using DevetionStudetns.DTO.RotationsDTO;
using FinalProject.Interface.IRepositry;
using loginpage.DBcon;

namespace DevetionStudetns.Service
{
    public class RotationsService
    {
        private readonly IRotations _context;
        public RotationsService(IRotations context)
        {
            _context = context;
        }
    
        public GeneralMsgDto AddRotationsService(AddRotaionsDto addRotaionsDto)
        {
            return _context.AddRotations(addRotaionsDto);
        }
        public GeneralMsgDto DeleteRotationService(int RotationsId)
        {
            return _context.DeleteRotations(RotationsId);

        }
        public GeneralMsgDto UpdateRotationsService(AddRotaionsDto NewRotaionsData, int RotaionstId)
        {
            return _context.UpdateRotations(NewRotaionsData, RotaionstId);

        }
        public List<GetRotaionDto> ShowDateRotaionInOneYearService(string Year)
        {
            return _context.ShowDateRotaionInOneYear(Year);


        }
        public GetRotaionDto ShowDateRotaionByRotationIdService(int RotationId)
        {
            return _context.ShowDateRotaionByRotationId(RotationId);

        }


    }
}
