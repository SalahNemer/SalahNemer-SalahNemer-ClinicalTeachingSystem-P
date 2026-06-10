namespace V1.DTO.PolicieDTO
{
    public class GetPolicieQDto
    {
        public int PolicieId { get; set; }
        public string Title { get; set; }
        public string PolicyIdentifier { get; set; }
        public string Objectives { get; set; }
        public string ExecutionResponsible { get; set; }
        public string Procedures { get; set; }
        public string Forms { get; set; }
    }
}
