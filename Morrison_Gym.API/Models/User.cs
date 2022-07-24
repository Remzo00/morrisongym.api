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
<<<<<<< HEAD:Models/User.cs
        public Role Role { get; set; } = new();
=======
        public Role? Role { get; set; } = new();
>>>>>>> main:Morrison_Gym.API/Models/User.cs
        public Guid UserCode { get; set; } = Guid.Empty;
    }
}
