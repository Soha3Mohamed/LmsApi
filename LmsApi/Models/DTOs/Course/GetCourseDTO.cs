namespace LmsApi.Models.DTOs.Course
{
    public class GetCourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int InstructorId { get; set; }
        public int LessonsCount { get; set; }
    }
}
