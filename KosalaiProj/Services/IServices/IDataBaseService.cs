using System.Collections.Generic;
using System.Threading.Tasks;
using GORAKSHANA.Models;
using MongoDB.Driver;

namespace GORAKSHANA.IServices
{
    public interface IFactoryServices<T>
    {
        string Codeprefix { get; }
        long Appefix { get; }


        FilterDefinitionBuilder<T> Builder { get; }


        List<T> GetList(FilterDefinition<T> filter = null);

        T Get(FilterDefinition<T> filter);

        T Create(T book);

        bool Update(FilterDefinition<T> filter, UpdateDefinition<T> bookIn);

        bool Remove(FilterDefinition<T> filter, T bookIn);

    }
}