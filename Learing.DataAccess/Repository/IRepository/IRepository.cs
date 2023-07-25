using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        //T -Category

        void Add(T entity);
        IEnumerable<T> GetAll(string? includeProps = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProps = null);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
