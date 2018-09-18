using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IQuestionModuleRepository
    {
        IEnumerable<KeyValuePair> GetKeyValueList();

        IEnumerable<KeyValuePair> GetKeyValueListByTechId(string techId);
    }
    public class QuestionModuleRepository : RepositoryBase<QuestionModule>, IQuestionModuleRepository
    {
        public QuestionModuleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<KeyValuePair> GetKeyValueListByTechId(string techId)
        {
            return GetAll()
                .Where(x => x.InformationTechnologySkillID.ToString() == techId)
                .Select(x => new KeyValuePair
                {
                    Key = x.ID.ToString(),
                    Value = x.Name
                });
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
