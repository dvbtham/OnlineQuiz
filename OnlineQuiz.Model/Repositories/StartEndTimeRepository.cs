using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IStartEndTimeRepository : IGetKeyValueList
    {

    }
    public class StartEndTimeRepository : RepositoryBase<StartEndTime>, IStartEndTimeRepository
    {
        public StartEndTimeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<KeyValuePair> GetKeyValueList()
        {
            return GetAll().OrderBy(x => x.Remark).Select(x =>
              new KeyValuePair
              {
                  Key = x.ID.ToString(),
                  Value = x.Remark + " (" + x.TimePeriod + ")"
              });
        }
    }
}
