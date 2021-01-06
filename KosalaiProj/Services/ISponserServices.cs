using System.Collections.Generic;
using DBModel;
using Sharedmodels;

namespace Services
{
    public interface ISponserServices
    {
        List<SponserModel> GetList();

        SponserModel Get(string id);

        SponserModel Create(SponserModel book);

        void Update(string id, SponserModel bookIn);

        void Remove(SponserModel bookIn);

        void Remove(string id);

        Dictionary<string, List<string>> GetDataSourceTypes();

        List<SponserModel> Search(SearchModel model);
    }
}
