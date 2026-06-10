using DataBase.Entity;
using DevetionStudetns.DTO.TADTO;

namespace DevetionStudetns.Mappers.TAMapper
{
    public static class TAMapper
    {
        public static TA AddOrEditTA(this AddTADto tADTO)
        {
            return new TA
            {
                TAId = tADTO.TAId,
                SupervisedYear = tADTO.SupervisedYear,
            };
        }

        public static AddTADto ShowTA(this TA tA)
        {
            return new AddTADto
            {
                TAId = tA.TAId,
                SupervisedYear = tA.SupervisedYear,
            };
        }
    }
}
