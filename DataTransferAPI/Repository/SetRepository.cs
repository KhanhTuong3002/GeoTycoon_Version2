using BusinessObject.Entites;
using DataTransferAPI.DAO;
using DataTransferAPI.DTO;
using DataTransferAPI.Repository.Interface;

namespace DataTransferAPI.Repository
{
    public class SetRepository : ISetRepository
    {
        public List<SetDTO> GetDefaultSet() => SetDAO.Instance.GetDefaultSet();

        public List<SetDTO> GetSetById(string id) => SetDAO.Instance.GetSetById(id);

        public List<SetDTO> GetSetQuestionDetails() => SetDAO.Instance.GetSetQuestions();
        
    }
}
