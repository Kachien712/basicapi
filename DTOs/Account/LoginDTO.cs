﻿using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Account
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
