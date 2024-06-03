using BusinessObject.Entites;

namespace DataTransferAPI.Repository.Interface
{
    public interface IQuestionRepository
    {
        List<Question> GetQuestions();
        Question GetQuestionById(string id);
    }
}
