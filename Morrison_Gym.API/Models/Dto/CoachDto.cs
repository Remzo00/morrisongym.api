namespace Morrison_Gym.API.Models.Dto
{
    public class CoachDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public User User { get; set; } = new();
    }
}
