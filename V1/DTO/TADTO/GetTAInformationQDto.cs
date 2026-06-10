namespace V1.DTO.TADTO
{
    public class GetTAInformationQDto
    {
        public string TAId { get; set; } = string.Empty;
        public string TaName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int SuperVisedYear { get; set; }
    }
}
