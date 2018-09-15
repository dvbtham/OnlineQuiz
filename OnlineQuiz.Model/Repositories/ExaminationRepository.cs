using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OnlineQuiz.Model.Repositories
{
    public interface IExaminationRepository : IRepository<Examination>
    {
        IEnumerable<ExaminationQuestionViewModel> GetExaminationQuestions(ExamResultViewModel model);
    }

    public class ExaminationRepository : RepositoryBase<Examination>, IExaminationRepository
    {
        public ExaminationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ExaminationQuestionViewModel> GetExaminationQuestions(ExamResultViewModel model)
        {
            var pars = new SqlParameter[] {
                new SqlParameter("@ExamResultID", model.ID),
                new SqlParameter("@IDExaminee", model.IDExaminee),
                new SqlParameter("@ExaminationID", model.ExaminationID),
                new SqlParameter("@ExamCode", model.ExamCode),
            };

            return DbContext.Database.SqlQuery<ExaminationQuestionViewModel>
                ("spGetExaminationQuestion @ExamResultID, @IDExaminee, @ExaminationID, @ExamCode", pars);

        }
    }
}
