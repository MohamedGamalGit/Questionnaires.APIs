using System.ComponentModel.DataAnnotations;

namespace Questionnaires.API.Data.Models
{
    public class AnswerOption
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
