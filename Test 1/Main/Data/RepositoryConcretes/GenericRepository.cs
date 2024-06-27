using Core.RepositoryAbstracts;
using Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcretes
{
    public class GenericRepository<T> : IGenericRepositroy<T> where T : class, new()
    {
        AppDbContext _dbContext;
        private DbSet<T> _table;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<T>();
        }

        public void Add(T item)
        {
            _dbContext.Set<T>().Add(item);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public T Get(Func<T, bool>? func = null)
        {
            return func == null ? _dbContext.Set<T>().FirstOrDefault() :
                                  _dbContext.Set<T>().Where(func).FirstOrDefault();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>>? func = null)
        {
            return await (func == null ? _dbContext.Set<T>().FirstOrDefaultAsync() :
                                         _dbContext.Set<T>().FirstOrDefaultAsync(func));
        }
        public async Task<IQueryable<T>> GetAll
            (Expression<Func<T, bool>>? func = null,
             Expression<Func<T, object>>? orderBy = null,
             bool isOrderByDesting = false,
            params Expression<Func<T, object>>[] includes
            )
        {
            IQueryable<T> data = _table;

            if(includes is not  null)
            {
                foreach(var item in includes) 
                {
                    data = data.Include(item);
                }
            }

            if(orderBy is not null)
            { 
                data = isOrderByDesting
                    ?data.OrderByDescending(orderBy)
                    :data.OrderBy(orderBy);
            }

            return func == null ? data : data.Where(func);
        }

        public void Remove(T item)
        {
            _dbContext.Set<T>().Remove(item);
        }
    }
}
