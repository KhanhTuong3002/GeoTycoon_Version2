using BusinessObject.Entites;
using DataTransferAPI.DTO;

namespace DataTransferAPI.Repository.Interface
{
    public interface ISetRepository
    {
        List<SetDTO> GetSetQuestionDetails();
        List<SetDTO> GetSetById(string id);
        List<SetDTO> GetDefaultSet();
    }
}
