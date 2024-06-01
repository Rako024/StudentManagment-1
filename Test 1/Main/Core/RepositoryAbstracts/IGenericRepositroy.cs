using System;
using System.Collections.Generic;
using System.Linq;
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
        List<T> GetAll(Func<T, bool>? func = null);
    }
}
