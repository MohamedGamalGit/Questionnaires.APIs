using Questionnaires.API.Enum;
using System.ComponentModel.DataAnnotations;

namespace Questionnaires.API.Data.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }

        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public ICollection<AnswerOption> AnswerOptions { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
