using System.ComponentModel.DataAnnotations;

namespace Questionnaires.API.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; } // استخدم Hash مش نص صريح

        public string Role { get; set; } = "User"; // أو Admin

        public ICollection<Questionnaire> Questionnaires { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}
