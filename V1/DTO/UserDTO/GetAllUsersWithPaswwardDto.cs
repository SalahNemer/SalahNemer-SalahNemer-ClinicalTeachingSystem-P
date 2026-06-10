namespace V1.DTO.UserDTO
{
    public class GetAllUsersWithPaswwardDto
    {
        public string UserId { get; set; }
        public string IdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int roleId { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBarth { get; set; }
        public string Password { get; set; }
    }
}
