﻿namespace Morrison_Gym.API.Models.Dto
{
    public class UserDto
    {
        public Guid UserCode { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }
}
