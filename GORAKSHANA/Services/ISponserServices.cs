using System.Collections.Generic;
using GORAKSHANA.Models;

namespace GORAKSHANA.IService
{
    public interface ISponserServices:IBaseService<SponserModel>
    {
        List<SponserModel> Search(SearchModel model);

        Dictionary<string, List<string>> GetDataSourceTypes();

          ResponseModel Upsert(SponserModel model);

         string GenrateCode();
    }
}
