using GORAKSHANA.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using GORAKSHANA.IService;

namespace GORAKSHANA.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        private readonly string _codeprefix = "GRSFD";
        private readonly long _appefix = 100000;
        public long Appefix => _appefix;
        public string Codeprefix => _codeprefix;
        public string CollectionName { get; set; } = "Producer";
        public IMongoDatabase database { get; internal set; }

        public IMongoCollection<T> _db => database.GetCollection<T>(CollectionName);

        public FilterDefinition<T> Filter { get; set; }

        public BaseService(IDatabaseSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }

        public T Get(FilterDefinition<T> filter) => _db.Find(filter).FirstOrDefault();

        public IEnumerable<T> GetList(FilterDefinition<T> filter = null)
        {
            if (Filter == null)
                return _db.Find(x => true).ToList();
            else
                return _db.Find(filter).ToList();
        }

        public void Create(T book) => _db.InsertOne(book);

        public bool Update(FilterDefinition<T> filter, T bookIn) => _db.ReplaceOne(filter, bookIn).IsAcknowledged;

        public bool Remove(FilterDefinition<T> filter) => _db.DeleteOne(filter).IsAcknowledged;



    }


}