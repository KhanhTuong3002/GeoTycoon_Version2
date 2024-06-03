using BusinessObject.Entites;
using DataAccess.Repository;
using DataTransferAPI.Repository;
using DataTransferAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataTransferAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionRepository questionRepository = new QuestionRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Question>> GetQuestions() => questionRepository.GetQuestions();

        [HttpGet("id")]
        public ActionResult<Question> GetCategoryById(string id) => questionRepository.GetQuestionById(id);

    }
}
