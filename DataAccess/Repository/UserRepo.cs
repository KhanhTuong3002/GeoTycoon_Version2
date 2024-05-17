using BusinessObject.Entites;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepo(DbContext dbContext) : BaseRepository<User>(new UserDao(dbContext))
    {
        
    }
}
