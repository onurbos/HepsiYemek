using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HepsiYemek.Data.Configs;
using HepsiYemek.Entities;
using MongoDB.Bson;

namespace HepsiYemek.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IDbClient dbClient)
        {
            _collection = dbClient.GetProductsCollection();
        }


        public Product Add(Product product)
        {
            _collection.InsertOne(product);
            return product;
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(x => x.Id == id);
        }

        public BsonDocument GetByIdAsBsonDoc(string id)
        {
            var product = _collection
                .Find(x => x.Id == id)
                .FirstOrDefault();

            if (product == null) return null;

            var productAsBsonDoc = _collection.Aggregate()
                .Match(m => m.CategoryId == product.CategoryId)
                .Lookup("Categories", "CategoryId", "_id", "CategoryId")
                .FirstOrDefault();

            return productAsBsonDoc;
        }

        public List<Product> GetList() => _collection.Find(x => true).ToList();

        public Product Update(Product product)
        {
            _collection.ReplaceOne(x => x.Id == product.Id, product);
            return product;
        }

        public Product Get(string id)
        {
            return _collection
                .Find(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}