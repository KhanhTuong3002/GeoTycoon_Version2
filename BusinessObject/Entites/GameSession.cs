using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entites
{
    public class GameSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SessionID { get; set; }
        [ForeignKey(nameof(Game))] public string GameId { get; set; }
        public virtual Game Games { get; set; }
        [ForeignKey(nameof(SetQuestion))] public string SetQuestionId { get; set; }
        public string AccessCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PhotonRoomId { get; set; }

    }
}
