using BusinessObject.Entites;

namespace DataTransferAPI.Repository.Interface
{
    public interface ISetRepository
    {
        List<SetQuestionDetail> GetSetQuestionDetails();
        List<SetQuestionDetail> GetSetById(string id);
        List<SetQuestionDetail> GetDefaultSet();
    }
}
