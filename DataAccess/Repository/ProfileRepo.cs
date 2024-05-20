using BusinessObject.Entites;
using DataAccess.DAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProfileRepo(DbContext dbContext) : BaseRepository<Profile>(new ProfileDao(dbContext))
    {
        public Profile GetprofileFrommUser(IdentityUser user)
        {
            var p = GetAll().FirstOrDefault(profile => profile.User == user);
            if(p == null)
            {
                p = new Profile {UserId=user.Id};
                Add(p);
            }
            return p;
        }
        public void UpdateProfile(Profile profile)
        {
            dbContext.Set<Profile>().Update(profile);
            dbContext.SaveChanges();
        }
    }
}
