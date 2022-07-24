﻿namespace Morrison_Gym.API.Models.Dto
{
    public class CustomerDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }        
    }
}