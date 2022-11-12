using System.ComponentModel.DataAnnotations;

namespace Morrison_Gym.API.Entities
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; } = string.Empty;

        [Required]
        public string? FirstName { get; set; } = string.Empty;

        [Required]
        public string? LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }

        public Role? Role { get; set; } = new();

        [Required]
        public Guid UserCode { get; set; } = Guid.Empty;
    }
}
