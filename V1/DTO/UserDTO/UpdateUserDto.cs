using System.ComponentModel.DataAnnotations;

namespace V1.DTO.UserDTO
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "FirstName is required.")]
        [NameValidation]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        [NameValidation]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [MinLength(9, ErrorMessage = "الرقم يجب أن يحتوي عل 9 خانات على الأقل.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "DateOfBarth is required.")]
        [DateOnlyRange("1950-01-01", "2015-12-31", ErrorMessage = "تاريخ الميلاد يجب أن يكون بين 1 يناير 1950 و 31 ديسمبر 2009.")]
        public DateOnly DateOfBarth { get; set; }

        [Required(ErrorMessage = "RoleId is required.")]
        public int? RoleId { get; set; }

        [Required(ErrorMessage = "AccountStatus is required.")]
        [Range(0, 1, ErrorMessage = "must the Account Status  0 or 1 ")]
        public byte? AccountStatus { get; set; } = 1;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "الاسم يجب أن يحتوي عل 8 خانات على الأقل.")]
        public string Password { get; set; }
    }
}
