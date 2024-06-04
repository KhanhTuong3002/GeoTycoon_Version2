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
    public class SetQuestion : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SetName { get; set; }

        [Required]
        public int QuestionNumber { get; set; }

        //public ICollection<Question> Questions { get; set; }
    }
}
