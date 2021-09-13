using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using HepsiYemek.Data.Configs;
using HepsiYemek.Entities;

namespace HepsiYemek.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly IMongoCollection<Category> _collection;

        public CategoryRepository(IDbClient dbClient)
        {
            _collection = dbClient.GetCategoriesCollection();
        }


        public Category Add(Category category)
        {
            _collection.InsertOne(category);
            return category;
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(x => x.Id == id);
        }

        public BsonDocument GetByIdAsBsonDoc(string id) => _collection.Find(x => x.Id == id).FirstOrDefault()?.ToBsonDocument();

        public List<Category> GetList() => _collection.Find(x => true).ToList();

        public Category Update(Category category)
        {
            _collection.ReplaceOne(x => x.Id == category.Id, category);
            return category;
        }
        
        public Category Get(string id) => _collection.Find(x => x.Id == id).FirstOrDefault();
    }
}
