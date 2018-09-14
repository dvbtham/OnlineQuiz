using OnlineQuiz.Model.Infrastructure;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Common.ViewModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IExaminationRepository : IRepository<Examination>
    {
        IEnumerable<ExaminationQuestionViewModel> GetExaminationQuestions(string examinId);
    }

    public class ExaminationRepository : RepositoryBase<Examination>, IExaminationRepository
    {
        public ExaminationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ExaminationQuestionViewModel> GetExaminationQuestions(string examinId)
        {
            var pars = new SqlParameter("@ExaminationID", examinId);
            return DbContext.Database.SqlQuery<ExaminationQuestionViewModel>("spGetCauHoiCuaKyThi @ExaminationID", pars);
          
        }
    }
}
