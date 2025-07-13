namespace LmsApi.Models.Entities
{
    public enum RatingValue
    {
        VeryBad = 1, Bad = 2, Okay = 3, Good = 4, VeryGood = 5
    }
    public class Rating
    {
        //Id, UserId, CourseId, RatingValue (1–5), Comment, CreatedAt
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public RatingValue RatingValue { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
