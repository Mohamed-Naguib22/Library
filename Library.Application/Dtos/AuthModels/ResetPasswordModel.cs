﻿using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos.AuthModels
{
    public class ResetPasswordModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }
        [StringLength(128)]
        public string NewPassword { get; set; }
    }
}