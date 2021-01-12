using System.Collections.Generic;
using MongoDB.Driver;

namespace GORAKSHANA.IService
{
    public interface IBaseService<T>
    {
        IEnumerable<T> GetList(FilterDefinition<T> filter = null);

        T Get(FilterDefinition<T> filter);

        void Create(T book);

        bool Update(FilterDefinition<T> filter, T bookIn);

        bool Remove(FilterDefinition<T> filter);

    }
}