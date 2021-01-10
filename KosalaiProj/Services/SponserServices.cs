using System;
using System.Collections.Generic;
using System.Linq;
using GORAKSHANA.IServices;
using GORAKSHANA.Models;
using MongoDB.Driver;

namespace GORAKSHANA.Services
{
    public class SponserServices : DataBaseService<SponserModel>,ISponserServices<SponserModel>
    {

        public SponserServices(IDatabaseSettings settings) : base(settings, "Producer")
        {
        }
        public List<SponserModel> Search(SearchModel model)
        {
            try
            {
                List<FilterDefinition<SponserModel>> filter =
                    new List<FilterDefinition<SponserModel>>();
                if (!string.IsNullOrEmpty(model.name))
                    filter
                        .Add(Builder
                            .Eq(nameof(SponserModel.firstName), model.name));
                if (!string.IsNullOrEmpty(model.contactno))
                    filter
                        .Add(Builder
                            .Eq(nameof(SponserModel.pri_contact_no),
                            model.contactno));
                if (!string.IsNullOrEmpty(model.sponserType))
                    filter
                        .Add(Builder
                            .Eq(nameof(SponserModel.sponser_Type),
                            model.sponserType));
                if (filter.Count > 0)
                    return Books.Find(Builder.And(filter.ToArray())).ToList();
                else
                    return Books.Find(book => true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is SponserServices services &&
            EqualityComparer<object>.Default.Equals(Builder, services.Builder);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
