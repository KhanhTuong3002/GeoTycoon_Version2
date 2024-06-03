using BusinessObject.Entites;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataTransferAPI.DTO
{
    public class QuestionDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        //public string Province { get; set; }
        //public string Author { get; set; }
        public string? Images { get; set; }
        public string setId { get; set; }
        public string Content { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string Answer { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }
        //public virtual SetDTO SetDTO { get; set; }
    }
}
