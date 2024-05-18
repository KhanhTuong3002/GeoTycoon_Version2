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
    public class SetQuestionDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string Id { get; set; }
        [ForeignKey(nameof(SetQuestion))] public string SetQuestionId { get; set; }
        public virtual SetQuestion SetQuestion { get; set; }
        [ForeignKey(nameof(Question))] public string QuestionId { get; set; }
        public virtual Question Question { get; set; } = default!;

    }
}
