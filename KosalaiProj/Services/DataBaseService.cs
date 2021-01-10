using System.Collections.Generic;
using System.Threading.Tasks;
using GORAKSHANA.IServices;
using GORAKSHANA.Models;
using MongoDB.Driver;

namespace GORAKSHANA.Services
{
    public class DataBaseService<T> : IFactoryServices<T>
    {

        private IMongoCollection<T> _books;

        private string _codeprefix = "GO-VPM";

        private long _appefix = 100000;
        private FilterDefinitionBuilder<T> _builder;

        public FilterDefinitionBuilder<T> Builder { get => _builder; }

        public FilterDefinition<T> Filter { get; set; }
        public IMongoCollection<T> Books { get => _books; }
        public long Appefix { get => _appefix; }
        public string Codeprefix { get => _codeprefix; }

        public DataBaseService(IDatabaseSettings settings, string collectionName)
        {
            if (settings is null)
            {
                throw new System.ArgumentNullException(nameof(settings));
            }

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<T>(collectionName);

            _builder = Builders<T>.Filter;
        }
        public T Create(T book)
        {
            Books.InsertOne(book);
            return book;
        }
        public T Get(FilterDefinition<T> filter) =>
          Books.Find(Filter).FirstOrDefault();
        public List<T> GetList(FilterDefinition<T> filter = null)
        {
            return (filter == null) ? Books.Find(Books => true).ToList() : Books.Find(filter).ToList();
        }

        public bool Remove(FilterDefinition<T> filter, T bookIn) => _books.DeleteOne(filter).IsAcknowledged;

        public bool Update(FilterDefinition<T> filter, UpdateDefinition<T> bookIn) => Books.UpdateOne(Filter, bookIn).IsAcknowledged;


    }
}