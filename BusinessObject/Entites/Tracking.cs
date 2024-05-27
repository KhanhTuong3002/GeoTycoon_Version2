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
    public class Tracking : BaseEntity
    {
        [Column("tracking_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public virtual Question Question { get; set; }

        public string? UserName { get; set; }

        [Required]
        public string? UserId { get; set; }

        public string Action { get; set; } // e.g., "Added", "Updated", "Deleted"

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }
}
