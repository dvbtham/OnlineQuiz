using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IExamPeriodRepository
    {
        IEnumerable<KeyValuePair> GetKeyValueList();
        ExamPeriodViewModel GetById(string id);

    }
    public class ExamPeriodRepository : RepositoryBase<ExamPeriod>, IExamPeriodRepository
    {
        public ExamPeriodRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public ExamPeriodViewModel GetById(string id)
        {
            var entity = GetSingleByCondition(x => x.ID.ToString() == id);
            if(entity != null)
            {
                var vm = new ExamPeriodViewModel
                {
                    ID = entity.ID,
                    StartDate = entity.StartDate.Value,
                    EndDate = entity.EndDate.Value,
                    Name = entity.Name
                };

                return vm;
            }
            return new ExamPeriodViewModel();
        }

        public IEnumerable<KeyValuePair> GetKeyValueList()
        {
            return GetAll().Select(x => new KeyValuePair
            {
                Key = x.ID.ToString(),
                Value = x.Name
            });
        }
    }
}
