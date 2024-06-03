using BusinessObject.Entites;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataTransferAPI.DAO
{
    public class QuestionDAO
    {
        public static List<Question> GetQuestions()
        {
            var listQuestions = new List<Question>();
            try
            {
                using (var context = new GeoTycoonDbcontext())
                {
                    listQuestions = context.Questions.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listQuestions;
        }
        public static Question GetQuestionById(string questionId)
        {
            var question = new Question();
            try
            {
                using (var context = new GeoTycoonDbcontext())
                {
                    question = context.Questions.SingleOrDefault(q => q.Id.Equals(questionId));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return question;
        }
    } 
}
