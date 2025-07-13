using System.ComponentModel.DataAnnotations.Schema;

namespace LmsApi.Models.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }


        //public List<string> Options { get; set; } = new(); i'd have used this but ef core doesn't store list<string> directly into database
        public string Options { get; set; } // store as comma-separated string

        [NotMapped]
        public List<string> OptionList
        {
            get => Options?.Split(',').ToList() ?? new List<string>();
            set => Options = string.Join(',', value);
        }


        /*
         * var options = question.OptionList; // ["Paris", "London", "Rome", "Madrid"]
           options.Add("Berlin");             // ["Paris", "London", "Rome", "Madrid", "Berlin"]
           question.OptionList = options;     // This line updates the string: Options = "Paris,London,Rome,Madrid,Berlin"

           context.Questions.Update(question);
           context.SaveChanges();

         */


        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

    }
}
