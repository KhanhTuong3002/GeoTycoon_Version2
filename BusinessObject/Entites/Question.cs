using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entites
{
    public class Question : BaseEntity
    {
        [Column("question_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey(nameof(Province))]
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        [MaxLength(450)]
        [ForeignKey(nameof(IdentityUser))] public string UserId { get; set; }
        public virtual IdentityUser User { get; set; } = default!;
        public string Images { get; set; }
        public string Content { get; set; } = default!;
        public DateTime Published { get; set; }
     


    }
}
