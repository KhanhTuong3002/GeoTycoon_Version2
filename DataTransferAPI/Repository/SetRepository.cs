using BusinessObject.Entites;
using DataTransferAPI.DAO;
using DataTransferAPI.Repository.Interface;

namespace DataTransferAPI.Repository
{
    public class SetRepository : ISetRepository
    {
        public List<SetQuestionDetail> GetDefaultSet() => SetDAO.Instance.GetDefaultSet();

        public List<SetQuestionDetail> GetSetById(string id) => SetDAO.Instance.GetSetById(id);

        public List<SetQuestionDetail> GetSetQuestionDetails() => SetDAO.Instance.GetSetQuestions();
        
    }
}
