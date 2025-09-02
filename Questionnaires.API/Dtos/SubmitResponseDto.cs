namespace Questionnaires.API.Dtos
{
    public class SubmitResponseDto
    {
        public int QuestionnaireId { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
