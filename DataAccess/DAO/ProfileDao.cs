using BusinessObject.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProfileDao : BaseDao<Profile>
    {
        public ProfileDao(DbContext dbContext) : base(dbContext)
        {
        }
        public static async Task<Profile?> GetByIdAsync(string userId)
        {
            return await GetByIdAsync(userId);
        }
    }
}
