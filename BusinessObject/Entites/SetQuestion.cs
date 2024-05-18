using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BusinessObject.Entites
{
    public class SetQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SetName { get; set; }
        [ForeignKey(nameof(IdentityUser))] public string UserId { get; set; }
        //public virtual IdentityUser User { get; set; } = default!;

        [Required]
        public int QuestionNumber { get; set; }

        //public ICollection<Question> Questions { get; set; }
    }
}
