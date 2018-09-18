using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface ITechSkillRepository
    {
        IEnumerable<KeyValuePair> GetKeyValueList();
    }
    public class TechSkillRepository : RepositoryBase<InformationTechnologySkill>, ITechSkillRepository
    {
        public TechSkillRepository(IDbFactory dbFactory) : base(dbFactory)
        {
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
