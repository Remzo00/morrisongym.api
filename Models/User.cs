using System.ComponentModel.DataAnnotations;

namespace Morrison_Gym.API.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public Role Role { get; set; } = new();
        public Guid UserCode { get; set; }
    }
}
