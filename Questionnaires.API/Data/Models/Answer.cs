using System.ComponentModel.DataAnnotations;

namespace Questionnaires.API.Data.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int ResponseId { get; set; }
        public Response Response { get; set; }

        public int? SelectedOptionId { get; set; } // لـ SingleChoice
        public AnswerOption SelectedOption { get; set; }

        public string TextAnswer { get; set; } // لـ Text
    }
}
