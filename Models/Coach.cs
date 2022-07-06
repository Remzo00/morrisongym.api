using System.ComponentModel.DataAnnotations;

namespace Morrison_Gym.API.Models
{
    public class Coach
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = new();
    }
}
