using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace V1.DTO.ApprovalsDTO
{
    public class UpdateApprovalsDto
    {
        public int DivisionStatus { get; set; } 
        public string Notes { get; set; }
    }
}
