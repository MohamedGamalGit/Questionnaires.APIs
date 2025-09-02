using Questionnaires.API.Enum;

namespace Questionnaires.API.Dtos
{
    public class AddQuestionDto
    {
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public int QuestionnaireId { get; set; }
        public List<string> Options { get; set; }
    }
}
