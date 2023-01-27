﻿using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.DTO
{
    public class UserLoginDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
