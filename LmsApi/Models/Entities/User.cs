namespace LmsApi.Models.Entities
{
    public enum UserRole{
        Admin =  0, Student = 1, Instructor = 2
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } //should be hashed
        public UserRole UserRole { get; set; } 
        public DateTime CreatedAt { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public List<Rating> Ratings { get; set; } = new List<Rating>();

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    }
}
