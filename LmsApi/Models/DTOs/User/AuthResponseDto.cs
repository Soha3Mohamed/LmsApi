﻿namespace LmsApi.Models.DTOs.User
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
