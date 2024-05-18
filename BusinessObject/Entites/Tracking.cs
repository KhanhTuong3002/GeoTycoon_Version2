using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entites
{
    public class Tracking : BaseEntity
    {
        [Column("tracking_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [ForeignKey(nameof(Question))] public string QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int AuthorId { get; set; }
        public string UpdContent { get; set; }
        public string UpdAnswers { get; set; }
        public string UpdDescription { get; set; }
        public String Status { get; set; }
    }
}
