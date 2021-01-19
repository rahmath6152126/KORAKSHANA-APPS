using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using GORAKSHANA.Models;
using GORAKSHANA.IService;

namespace GORAKSHANA.Services
{
    public class SponserServices : BaseService<SponserModel>, ISponserServices
    {

        FilterDefinitionBuilder<SponserModel> builder;
        public SponserServices(IDatabaseSettings settings) : base(settings, "Producer")
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
        }

        public string GenrateCode()
        {
            var len = GetList().Count() + 1;

            return $"{Codeprefix}{Appefix + len}";
        }

        public ResponseModel Upsert(SponserModel model)
        {
            try
            {
                var existing = new SponserModel();
                existing = _db.Find(x => x.code == model.code).FirstOrDefault();
                if (existing != null)
                {
                    var result = _db.ReplaceOne(x => x.code == model.code, model).IsAcknowledged;
                    return new ResponseModel()
                    {
                        Message = result ? "Saved Sucessfully" : "Error an update existing fields",
                        status = result
                    };
                }
                existing = _db.Find(x => x.firstName.ToLower() == model.firstName.ToLower() && x.sponser_Type.Equals(model.sponser_Type)).FirstOrDefault();
                if (existing == null)
                {
                    model.code = GenrateCode();
                    _db.InsertOne(model);
                    return new ResponseModel()
                    {
                        Message = "Saved Sucessfully",
                        status = true
                    };
                }
                else
                {
                    return new ResponseModel()
                    {
                        Message = "Duplicate records found",
                        status = false
                    };
                }
            }


            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Message = ex.Message,
                    status = false
                };
            }
        }
        public Dictionary<string, List<string>> GetDataSourceTypes()
        {
            Dictionary<string, List<string>> master =
                new Dictionary<string, List<string>>();
            master
                .Add(nameof(sponserType),
                Enum.GetNames(typeof(sponserType)).ToList());

            master
                .Add(nameof(eventType),
                Enum.GetNames(typeof(eventType)).ToList());

            master
                .Add(nameof(tamilYr), Enum.GetNames(typeof(tamilYr)).ToList());

            master
                .Add(nameof(tamilMon),
                Enum.GetNames(typeof(tamilMon)).ToList());

            master.Add(nameof(star), Enum.GetNames(typeof(star)).ToList());

            master.Add(nameof(thithi), Enum.GetNames(typeof(thithi)).ToList());

            master.Add(nameof(paymentMode), Enum.GetNames(typeof(paymentMode)).ToList());

            master.Add(nameof(referenceType), Enum.GetNames(typeof(referenceType)).ToList());

            master.Add(nameof(Patcam), Enum.GetNames(typeof(Patcam)).ToList());

            return master;
        }

        public List<SponserModel> Search(SearchModel model)
        {
            try
            {
                List<FilterDefinition<SponserModel>> filter =
                    new List<FilterDefinition<SponserModel>>();
                if (!string.IsNullOrEmpty(model.name))
                    filter
                        .Add(builder
                            .Eq(nameof(SponserModel.firstName), model.name));
                if (!string.IsNullOrEmpty(model.contactno))
                    filter
                        .Add(builder
                            .Eq(nameof(SponserModel.pri_contact_no),
                            model.contactno));
                if (!string.IsNullOrEmpty(model.sponserType))
                    filter
                        .Add(builder
                            .Eq(nameof(SponserModel.sponser_Type),
                            model.sponserType));
                if (filter.Count > 0)
                    return _db.Find(builder.And(filter.ToArray())).ToList();
                else
                    return _db.Find(book => true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
