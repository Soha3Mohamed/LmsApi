using LmsApi.Models.Entities;

namespace LmsApi.Models.DTOs.User
{
    public class AddUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole UserRole { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
