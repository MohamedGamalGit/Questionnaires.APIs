using System.ComponentModel.DataAnnotations;

namespace Questionnaires.API.Data.Models
{
    public class Response
    {
        [Key]
        public int Id { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
