using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HepsiYemek.Data.Configs;
using HepsiYemek.Entities;

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

        public Product Get(string id) => _collection.Find(x => x.Id == id).First();

        public List<Product> GetList() => _collection.Find(x => true).ToList();

        public Product Update(Product product)
        {
            Get(product.Id);
            _collection.ReplaceOne(x => x.Id == product.Id, product);
            return product;
        }


    }
}
