using LmsApi.Models.Entities;

namespace LmsApi.Models.DTOs.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserRole UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        //public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
