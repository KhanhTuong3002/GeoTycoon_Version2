using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entites
{
    public class UserAnswer : BaseEntity
    {
        [Column("Uanswer_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id { get; set; }
        [ForeignKey(nameof(UserQuestion))] public string UquestionId { get; set; }
        public string Uanswers { get; set; }
        public string? Description { get; set; }
        public bool? IsCorrect { get; set; }
        public virtual UserQuestion UserQuestion { get; set; } = default!;

    }
}
