namespace Morrison_Gym.API.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public Guid UserCode { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
    }
    public class UserRegisterDto
    {
        public int Id { get; set; }
        public Guid UserCode { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }
        public Guid UserCode { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
    }
}
