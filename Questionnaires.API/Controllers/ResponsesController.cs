using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaires.API.Data.Models;
using Questionnaires.API.Dtos;
using Questionnaires.API.Repositories;
using System.Security.Claims;

namespace Questionnaires.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ResponsesController : ControllerBase
    {
        private readonly IRepository<Response> _responseRepository;
        private readonly IRepository<User> _userRepository;

        public ResponsesController(IRepository<Response> responseRepository, IRepository<User> userRepository)
        {
            _responseRepository = responseRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitResponse(SubmitResponseDto dto)
        {
            try
            {
                var response = new Response
                {
                    QuestionnaireId = dto.QuestionnaireId,
                    UserId = GetUserId(),
                    Answers = new List<Answer>()
                };

                foreach (var answer in dto.Answers)
                {
                    response.Answers.Add(new Answer
                    {
                        QuestionId = answer.QuestionId,
                        SelectedOptionId = answer.SelectedOptionId,
                        TextAnswer = answer.TextAnswer
                    });
                }

                _responseRepository.Add(response);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpGet("{questionnaireId}/results")]
        public async Task<IActionResult> GetResults(int questionnaireId)
        {
            var responses =  _responseRepository.GetAll()
                .Where(r => r.QuestionnaireId == questionnaireId)
                //.Include(r => r.Answers)
                .ToList();

            return Ok(responses);
        }

        private int GetUserId()
        {
            var result = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Username == result);
            return user.Id;
        }
    }
}
