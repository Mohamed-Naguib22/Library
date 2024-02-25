﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library.Application.Dtos.AuthModels
{
    public class ResetTokenModel
    {
        public string Token { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
        [JsonIgnore]
        public bool Succeeded { get; set; }
    }
}