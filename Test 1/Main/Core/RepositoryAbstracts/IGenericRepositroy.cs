using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryAbstracts
{
    public interface IGenericRepositroy<T> where T : class,new()
    {
        void Add(T item);
        void Remove(T item);
        void Commit();
        T Get(Func<T, bool>? func = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? func = null);
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? func = null,
            Expression<Func<T,object>>? orderBy = null,
            bool isOrderByDesting = false,
            params Expression<Func<T, object>>[] includes
            );
    }
}
