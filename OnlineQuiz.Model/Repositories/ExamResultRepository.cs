using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IExamResultRepository
    {
        ExamResultViewModel Get(string examineeId, int examCode);
        void Update(string examResultId, string questionId, KeyValuePair model);
    }

    public class ExamResultRepository : RepositoryBase<ExamResult>, IExamResultRepository
    {
        public ExamResultRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public ExamResultViewModel Get(string examineeId, int examCode)
        {
            var pars = new SqlParameter[]
           {
                new SqlParameter("@IDExaminee", examineeId),
                new SqlParameter("@ExamCode", examCode)
           };

            var examResult = DbContext.Database
                .SqlQuery<ExamResultViewModel>
                ("spInsertExamResultAndDetail @IDExaminee, @ExamCode", parameters: pars).FirstOrDefault();
            return examResult;
        }

        public void Update(string examResultId, string questionId, KeyValuePair model)
        {
            try
            {
                var examResult = DbContext.ExamResultDetails
                    .FirstOrDefault(x => x.ExamResultID.ToString() == examResultId && x.QuesionID.ToString() == questionId);

                if(examResult != null)
                {
                    examResult.Answer = model.Key;
                    examResult.ResultAnswer = model.Value;

                    DbContext.Entry(examResult).State = System.Data.Entity.EntityState.Modified;

                }
            }
            catch (System.Exception e)
            {

                throw e;
            }
        }
    }
}
