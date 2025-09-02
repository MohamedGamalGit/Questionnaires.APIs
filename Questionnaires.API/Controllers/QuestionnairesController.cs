using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaires.API.Data;
using Questionnaires.API.Data.Models;
using Questionnaires.API.Dtos;
using Questionnaires.API.Repositories;
using System.Security.Claims;

namespace Questionnaires.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionnairesController : ControllerBase
    {
        private readonly IRepository<Questionnaire> _questionnaireRepository;
        private readonly IRepository<User> _userRepository;
        private readonly QuestionnairesDbContext _context;

        public QuestionnairesController(IRepository<Questionnaire> questionnaireRepository, IRepository<User> userRepository, QuestionnairesDbContext context)
        {
            _questionnaireRepository = questionnaireRepository;
            _userRepository = userRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //var userId = GetUserId();
                //var questionnairrIds = _context.Responses.Where(q => q.UserId == userId).Select(q => q.QuestionnaireId).ToList();

                //var list = _questionnaireRepository.GetAll().Where(x => !questionnairrIds.Contains(x.Id)).ToList();
                var list = _questionnaireRepository.GetAll();
                //.Include(q => q.Questions)
                //.ToListAsync();

                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionnaireCreateDto dto)
        {
            var questionnaire = new Questionnaire
            {
                Title = dto.Title,
                UserId = GetUserId()
            };

            _questionnaireRepository.Add(questionnaire);
            return Ok();
        }

        private int GetUserId()
        {
            var result = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Username == result);
            return user.Id;
        }

        [HttpGet,Route("GetCount")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var list = _questionnaireRepository.GetAll().Count;

                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
