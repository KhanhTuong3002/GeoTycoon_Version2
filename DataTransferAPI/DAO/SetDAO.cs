using BusinessObject.Entites;
using DataAccess;
using DataTransferAPI.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace DataTransferAPI.DAO
{
    public class SetDAO
    {
        private static SetDAO? instance = null;
        private static readonly object instanceLock = new object();
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
        public List<SetDTO> GetSetQuestions()
        {
            var listSet = new List<SetQuestionDetail>();
            try
            {
                using (var context = new GeoTycoonDbcontext())
                {
                    listSet = context.SetQuestionDetails.Include(s => s.SetQuestion).Include(s =>  s.Question).OrderBy(s => s.SetQuestionId).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return JSonCoverter(listSet);
        }
        public List<SetDTO> GetSetById(string setId)
        {
            var set = new List<SetQuestionDetail>();
            try
            {
                using (var context = new GeoTycoonDbcontext())
                {
                    set = context.SetQuestionDetails.Where(s => s.Id.ToString().Equals(setId)).Include(s => s.SetQuestion).Include(s => s.Question).OrderBy(s => s.SetQuestionId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return JSonCoverter(set);
        }

        public List<SetDTO> GetDefaultSet()
        {
            var set = new List<SetQuestionDetail>();
            var defaultSet = new List<SetQuestionDetail>();
            try
            {
                using (var context = new GeoTycoonDbcontext())
                {
                    defaultSet = context.SetQuestionDetails.Include(s => s.SetQuestion).Include(s => s.Question).OrderBy(s => s.SetQuestionId).ToList();

                    foreach (SetQuestionDetail item in defaultSet)
                    {
                        if (GetRoleUser(item.SetQuestion.UserId)=="ADMINISTRATOR")
                        {
                            set.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return JSonCoverter(set);
        }

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

        public List<SetDTO> JSonCoverter(List<SetQuestionDetail> list)
        {
            var listQ = new List<QuestionDTO>();
            var listS = new List<SetDTO>();

            var questions = new List<SetQuestionDetail>();
            var set = new List<SetQuestionDetail>();
            questions = list.ToList();
            set = list.DistinctBy(l => l.SetQuestionId).ToList();

            foreach (var item in questions)
            {
                QuestionDTO q = new QuestionDTO()
                {
                    Id = item.QuestionId,
                    Title = item.Question.Title,
                    Content = item.Question.Content,
                    Images = item.Question.Images,  
                    Option1 = item.Question.Option1,
                    Option2 = item.Question.Option2,
                    Option3 = item.Question.Option3,
                    Option4 = item.Question.Option4,
                    //Province = item.Question.Province.ProvinceName,
                    Answer = item.Question.Answer,
                    Description = item.Question.Description,
                    Published = item.Question.Published,
                    setId = item.SetQuestionId,
                };
                listQ.Add(q);
            }
            foreach (var item in set)
            {
                SetDTO s = new SetDTO()
                {
                    Id = item.SetQuestionId,
                    QuestionNumber = item.SetQuestion.QuestionNumber,
                    SetName = item.SetQuestion.SetName,
                    UserId = item.SetQuestion.UserId,
                    questionDTOs = listQ.Where(q => q.setId == item.SetQuestionId).ToList()
                };
                listS.Add(s);
            }
            return listS;
        }
    }
}
