using System.ComponentModel.DataAnnotations;

namespace Questionnaires.API.Data.Models
{
    public class Questionnaire
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Response> Responses { get; set; }

    }
}
