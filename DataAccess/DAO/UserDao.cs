using BusinessObject.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDao : BaseDao<User>
    {
        public UserDao(DbContext dbContext) : base(dbContext)
        {
        }
        public static async Task<User?> GetByIdAsync(string userId)
        {
            return await GetByIdAsync(userId);
        }
    }
}
