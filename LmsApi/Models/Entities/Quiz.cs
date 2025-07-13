namespace LmsApi.Models.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public int CourseId { get; set; }

        public List<Question> Questions { get; set; }
    }
}
