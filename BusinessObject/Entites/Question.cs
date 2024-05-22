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

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [ForeignKey(nameof(Province))]
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        [MaxLength(450)]
        [ForeignKey(nameof(IdentityUser))] public string UserId { get; set; }
        public virtual IdentityUser User { get; set; } = default!;
        public string? Images { get; set; }
        [Required]
        [StringLength(600)]
        public string Content { get; set; }

        [Required]
        [StringLength(200)]
        public string Option1 { get; set; }       
        [Required]
        [StringLength(200)]
        public string Option2 { get; set; }       
        [Required]
        [StringLength(200)]
        public string? Option3 { get; set; }        
        [Required]
        [StringLength(200)]
        public string? Option4 { get; set; }
        [Required]
        [StringLength(200)]
        public string Answer { get; set; }

        [Required]
        [StringLength(2500)]
        public string Description { get; set; }
       
/*        [ForeignKey("ListId")]
        public SetQuestion List { get; set; }*/
        public DateTime Published { get; set; }
     


    }
}
