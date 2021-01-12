using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GORAKSHANA.IService;
using GORAKSHANA.Models;
using MongoDB.Driver;

namespace GORAKSHANA.Services
{
    public class MasterService : BaseService<MasterModel>, IMasterService
    {
        public MasterService(
            IDatabaseSettings databaseSettings
        ) :
            base(databaseSettings, "")
        {
        }

        public bool Add(Common cmd, MasterModel model)
        {
            try
            {
                CollectionName = cmd.ToString();
                var result = _db.Find(x => x.Id == model.Id).FirstOrDefault();
                if (result == null)
                    _db.InsertOne(model);
                else
                    _db.ReplaceOne(x => x.Id == model.Id, model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public IEnumerable<MasterModel> GetList(Common cmd)
        {
            CollectionName = cmd.ToString();
            return _db.Find(x => true).ToList();
        }
    }
}
