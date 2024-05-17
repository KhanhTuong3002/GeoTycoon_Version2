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
        [ForeignKey(nameof(UserQuestion))] public string UquestionId { get; set; }
        public virtual UserQuestion UserQuestion { get; set; } = default!;
        public int AuthorId { get; set; }
        public string UpdContent { get; set; }
        public string UpdAnswers { get; set; }
        public string UpdDescription { get; set; }
        public String Status { get; set; }
    }
}
