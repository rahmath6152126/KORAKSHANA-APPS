using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;
using DBModel;
using MongoDB.Driver;
using Services;
using Sharedmodels;

namespace KosalaiProj.Services
{
    public class SponserServices : ISponserServices
    {
        private readonly IMongoCollection<SponserModel> _books;

        private readonly string Codeprefix = "KOVPM";

        private readonly long Appefix = 10000;

        private readonly FilterDefinitionBuilder<SponserModel>
            builder = Builders<SponserModel>.Filter;

        public SponserServices(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<SponserModel>("Producer");
        }

        private string GenrateCode()
        {
            var len = GetList().Count() + 1;

            return $"{Codeprefix}{Appefix + len}";
        }

        public List<SponserModel> GetList() =>
            _books.Find(book => true).ToList();

        public SponserModel Get(string id) =>
            _books.Find(book => book.Id == id).FirstOrDefault();

        public SponserModel Create(SponserModel book)
        {
            book.code =
                string.IsNullOrEmpty(book.code) ? GenrateCode() : book.code;
            if (
                _books
                    .Find(builder.Eq(nameof(SponserModel.code), book.code))
                    .ToList()
                    .Count() ==
                0
            )
            {
                _books.InsertOne(book);
                return book;
            }
            else
            {
                throw new DuplicateWaitObjectException(nameof(book.code));
            }
        }

        public void Update(string id, SponserModel bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(SponserModel bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);

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

            master.Add(nameof(patcam), Enum.GetNames(typeof(patcam)).ToList());

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
                    return _books.Find(builder.And(filter.ToArray())).ToList();
                else
                    return _books.Find(book => true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is SponserServices services &&
            EqualityComparer<object>.Default.Equals(builder, services.builder);
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
