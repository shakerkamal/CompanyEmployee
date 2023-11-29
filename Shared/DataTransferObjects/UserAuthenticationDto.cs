﻿using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record UserAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")] 
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password name is required")] 
        public string? Password { get; init; }
    }
}
