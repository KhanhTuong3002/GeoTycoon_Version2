using BusinessObject.Entites;
using DataTransferAPI.Repository.Interface;
using DataTransferAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataTransferAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetController : ControllerBase
    {
        private ISetRepository repository = new SetRepository();

        [HttpGet]
        public ActionResult<IEnumerable<SetQuestionDetail>> GetAllSet() => repository.GetSetQuestionDetails();

        [HttpGet("id")]
        public ActionResult<IEnumerable<SetQuestionDetail>> GetSetById(string id) => repository.GetSetById(id);

        [HttpGet("default")]
        public ActionResult<IEnumerable<SetQuestionDetail>> GetDefaultSet() => repository.GetDefaultSet();
    }
}
