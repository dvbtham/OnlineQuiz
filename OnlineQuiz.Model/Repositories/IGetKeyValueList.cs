using OnlineQuiz.Common.ViewModel;
using System.Collections.Generic;

namespace OnlineQuiz.Model.Repositories
{
    public interface IGetKeyValueList
    {
        IEnumerable<KeyValuePair> GetKeyValueList();
    }

    public interface IGetKeyValue
    {
        KeyValuePair GetKeyValueById(string id);
    }
}
