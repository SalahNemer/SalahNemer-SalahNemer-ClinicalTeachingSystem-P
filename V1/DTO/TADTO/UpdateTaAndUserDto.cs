using System.ComponentModel.DataAnnotations;

namespace V1.DTO.TADTO
{
    public class UpdateTaAndUserDto
    {
        //ta
        [Required(ErrorMessage = "يرجى ادخال السنة المشرف عليها")]
        public int SupervisedYear { get; set; }
        //user
        [Required]
        public string UserId { get; set; }

        [Required]
        public string IdNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateOnly DateOfBarth { get; set; }

        [Required]
        public int? RoleId { get; set; }

        [Required]
        public byte? AccountStatus { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
