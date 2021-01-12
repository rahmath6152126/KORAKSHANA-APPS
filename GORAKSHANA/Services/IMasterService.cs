using System.Collections.Generic;
using GORAKSHANA.Models;

namespace GORAKSHANA.IService
{
    public interface IMasterService:IBaseService<MasterModel>
    {
      bool Add(Common cmd, MasterModel model);

      IEnumerable<MasterModel> GetList(Common cmd);
    }
}
