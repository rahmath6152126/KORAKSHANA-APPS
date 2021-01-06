using System.Collections.Generic;
using DBModel;

namespace Services
{
    public interface IServices<T>
    {
         List<SponserModel> GetList();

        SponserModel Get(string id);

        SponserModel Create(SponserModel book);

        void Update(string id, SponserModel bookIn);

        void Remove(SponserModel bookIn);

        void Remove(string id);

        Dictionary<string, List<string>> GetDataSourceTypes();
    }
}