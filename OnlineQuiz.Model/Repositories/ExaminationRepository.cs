using OnlineQuiz.Model.Infrastructure;
using OnlineQuiz.Model.Entity;

namespace OnlineQuiz.Model.Repositories
{
    public interface IExaminationRepository : IRepository<Examination>
    {
    }

    public class ExaminationRepository : RepositoryBase<Examination>, IExaminationRepository
    {
        public ExaminationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
