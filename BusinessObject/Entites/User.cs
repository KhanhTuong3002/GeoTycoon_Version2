using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entites
{
    public class User : IdentityUser, IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public override string Id { get; set; }

        public string Avatar { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public bool Isbanned { get; set; } = false;

        public User()
        {
            Id = SnowflakeGenerator.Generate(10);
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public User(string userName) : this()
        {
            UserName = userName;
        }
    }
}
