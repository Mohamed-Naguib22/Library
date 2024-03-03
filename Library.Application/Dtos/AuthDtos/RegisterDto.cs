﻿using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos.AuthDtos
{
    public class RegisterDto
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [EmailAddress, StringLength(128)]
        public string Email { get; set; }
        [StringLength(128)]
        public string Password { get; set; }
        [Phone, StringLength(11, MinimumLength = 11)]
        public string? PhoneNumber { get; set; }
    }
}
