using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineQuiz.Service.Services
{
    public interface IBaseService<T>
    {
        T Add(T entity);

        void Update(T entity);

        T FindById(int id);

        T FindById(Guid? id);

        T Delete(int id);

        T Delete(Guid? id);

        IEnumerable<T> GetAll(string include = null);
    }
}
