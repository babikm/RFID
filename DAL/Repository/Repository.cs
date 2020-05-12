using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IMongoDatabase _database;
        private readonly string _collectionName;
        private IMongoCollection<T> _collection;

        public Repository(IMongoDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
            _collection = _database.GetCollection<T>(collectionName);
        }

        public void Add(T entity)
        {
            _collection.InsertOneAsync(entity);
        }

        public void Delete(string id)
        {
            _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        }

        public T Get(string id)
        {
            var data = _collection.FindSync(Builders<T>.Filter.Eq("Id", id));
            return data.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            var all = _collection.FindSync(Builders<T>.Filter.Empty);
            return all.ToList();
        }

        public T GetByTagId(string tagId)
        {
            var data = _collection.FindSync(Builders<T>.Filter.Eq("TagId", tagId));
            return data.FirstOrDefault();
        }

        public T Update(Expression<Func<T, bool>> filter, T replacement)
        {
            return _collection.FindOneAndReplace(filter, replacement);
        }
    }
}
