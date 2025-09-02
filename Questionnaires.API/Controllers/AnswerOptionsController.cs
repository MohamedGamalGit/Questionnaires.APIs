using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaires.API.Data.Models;
using Questionnaires.API.Repositories;

namespace Questionnaires.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerOptionsController : ControllerBase
    {
        private readonly IRepository<AnswerOption> _answerOptionRepository;

        public AnswerOptionsController(IRepository<AnswerOption> answerOptionRepository)
        {
            _answerOptionRepository = answerOptionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int questionId)
        {
            var list = _answerOptionRepository.GetAll().Where(x=>x.QuestionId== questionId);
            //.Include(q => q.Questions)
            //.ToListAsync();

            return Ok(list);
        }
    }
}
