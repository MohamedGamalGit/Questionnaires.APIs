using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaires.API.Data.Models;
using Questionnaires.API.Dtos;
using Questionnaires.API.Enum;
using Questionnaires.API.Repositories;

namespace Questionnaires.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<AnswerOption> _answerOptionRepository;

        public QuestionsController(IRepository<Question> questionRepository, IRepository<AnswerOption> answerOptionRepository)
        {
            _questionRepository = questionRepository;
            _answerOptionRepository = answerOptionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(AddQuestionDto dto)
        {
            try
            {
                var question = new Question
                {
                    Text = dto.Text,
                    Type = dto.Type,
                    QuestionnaireId = dto.QuestionnaireId
                };

                _questionRepository.Add(question);
                // Add answer options if applicable
                if (dto.Type == QuestionType.SingleChoice && dto.Options != null)
                {
                    foreach (var opt in dto.Options)
                    {
                        _answerOptionRepository.Add(new AnswerOption
                        {
                            QuestionId = question.Id,
                            Text = opt
                        });
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByQuestionnaireId(int questionnaireId)
        {
            var list = _questionRepository.GetAll().Where(x=>x.QuestionnaireId== questionnaireId);
            //.Include(q => q.Questions)
            //.ToListAsync();

            return Ok(list);

        }
    }
}
