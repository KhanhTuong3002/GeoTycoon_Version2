using System.ComponentModel.DataAnnotations;

namespace DataTransferAPI.DTO
{
    public class SetDTO
    {
        public String Id { get; set; }
        public string SetName { get; set; }
        public int QuestionNumber { get; set; }
        public string? UserId { get; set; }

        public ICollection<QuestionDTO> questionDTOs { get; set; }
    }
}
