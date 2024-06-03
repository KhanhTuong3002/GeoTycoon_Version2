using BusinessObject.Entites;
using DataTransferAPI.DAO;
using DataTransferAPI.Repository.Interface;

namespace DataTransferAPI.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        public Question GetQuestionById(string id) => QuestionDAO.GetQuestionById(id);
        

        public List<Question> GetQuestions() => QuestionDAO.GetQuestions();
        
    }
}
