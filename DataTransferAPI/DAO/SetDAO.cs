using BusinessObject.Entites;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace DataTransferAPI.DAO
{
    public class SetDAO
    {
        private readonly UserManager<IdentityUser> _userManager;
        private static SetDAO? instance = null;
        private static readonly object instanceLock = new object();
        //public SetDAO(UserManager<IdentityUser> userManager) {  _userManager = userManager; }
        public static SetDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SetDAO();
                    }
                    return instance;
                }
            }
        }
        public List<SetQuestionDetail> GetSetQuestions()
        {
            var listSet = new List<SetQuestionDetail>();
            try
            {
                using (var context = new GeoTycoonDbcontext())
                {
                    listSet = context.SetQuestionDetails.Include(s => s.SetQuestion).Include(s =>  s.Question).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listSet;
        }
        public List<SetQuestionDetail> GetSetById(string setId)
        {
            var set = new List<SetQuestionDetail>();
            try
            {
                using (var context = new GeoTycoonDbcontext())
                {
                    set = context.SetQuestionDetails.Where(s => s.Id.ToString().Equals(setId)).Include(s => s.SetQuestion).Include(s => s.Question).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return set;
        }

        public List<SetQuestionDetail> GetDefaultSet()
        {
            var set = new List<SetQuestionDetail>();
            try
            {
                using (var context = new GeoTycoonDbcontext())
                {
                    set = context.SetQuestionDetails.Include(s => s.SetQuestion).Include(s => s.Question).ToList();
                    foreach (var item in set)
                    {
                        if (GetRoleUser(item.Question.UserId)!="ADMINISTRATOR")
                        {
                            set.Remove(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return set;
        }
        //public string GetUsersAdminAsync(string userId)
        //{
        //    var user = new IdentityUser();
        //    string role = "";
        //    using (var context = new GeoTycoonDbcontext())
        //    {
        //        user = context.Users.SingleOrDefault(u => u.Id.ToString().Equals(userId));
        //        role = _userManager.GetRolesAsync(user).ToString();
        //        return role;
        //    }
        //}

        public string GetRoleUser(string userId) 
        {
            var role = new IdentityRole();
            using (var context = new GeoTycoonDbcontext())
            {
                role = context.Roles.SingleOrDefault(r => r.NormalizedName.ToString().Equals("ADMINISTRATOR"));
                var userRole = context.UserRoles.SingleOrDefault(u => u.UserId.Equals(userId) && u.RoleId.Equals(role.Id));
                if (userRole != null)
                {
                    return "ADMINISTRATOR";
                }
                return "other";
            }
        }
    }
}
