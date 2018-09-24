using OnlineQuiz.Common.ViewModel;
using OnlineQuiz.Model.Entity;
using OnlineQuiz.Model.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace OnlineQuiz.Model.Repositories
{
    public interface IExaminationRoomRepository : IGetKeyValueList
    {
        ExaminationRoomViewModel GetById(string id);
    }

    public class ExaminationRoomRepository : RepositoryBase<ExaminationRoom>, IExaminationRoomRepository
    {
        public ExaminationRoomRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public ExaminationRoomViewModel GetById(string id)
        {
            return DbContext.ExaminationRooms.Select(x => new ExaminationRoomViewModel
            {
                ID = x.ID,
                Name = x.Name,
                Quantity = x.Quantity,
                Remark = x.Remark
            }).FirstOrDefault();
        }

        public IEnumerable<KeyValuePair> GetKeyValueList()
        {
            return GetAll().Select(x => new KeyValuePair { Key = x.ID.ToString(), Value = x.Name });
        }
    }
}
