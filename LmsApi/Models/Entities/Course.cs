namespace LmsApi.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int InstructorId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsPublished { get; set; } = false;
        public User User { get; set; }
        public int QuizId { get; set; }
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        public List<Rating> Rating { get; set; } = new List<Rating>();

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}
