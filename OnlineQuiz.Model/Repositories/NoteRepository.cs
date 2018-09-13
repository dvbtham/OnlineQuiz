using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface INoteRepository     {         IEnumerable<KeyValuePair> GetAll();     }      public class NoteRepository : RepositoryBase<Note>, INoteRepository     {         public NoteRepository(IDbFactory dbFactory) : base(dbFactory)         {         }

        public IEnumerable<KeyValuePair> GetAll()
        {
            return DbContext.Notes.Select(x => new KeyValuePair
            {
                Key = x.ID.ToString(),
                Value = x.Name
            });
        }
    }
}
