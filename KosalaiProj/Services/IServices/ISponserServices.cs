using GORAKSHANA.Models;
using System.Collections.Generic;
namespace GORAKSHANA.IServices
{
    public interface ISponserServices<T> : IFactoryServices<T>
    {
        List<SponserModel> Search(SearchModel model);
    }
}