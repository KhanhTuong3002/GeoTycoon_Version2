using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entites
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string GameId { get; set; }
        public string GameName { get; set; }
        [ForeignKey(nameof(SetQuestion))] public string SetQuestionId { get; set; }
        public virtual SetQuestion SetQuestion { get; set; }    


    }
}
