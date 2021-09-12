using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HepsiYemek.Data.Configs;
using HepsiYemek.Data.Context;
using HepsiYemek.Entities;
using Microsoft.Extensions.Configuration;

namespace HepsiYemek.Data.Client
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Product> _productsCollection;
        private readonly IMongoCollection<Category> _categoriesCollection;

        public DbClient(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("ConnectionString"));
            var database = client.GetDatabase(configuration.GetConnectionString("DataBaseName"));
            _productsCollection = database.GetCollection<Product>("Products");
            _categoriesCollection = database.GetCollection<Category>("Categories");
        }

        public IMongoCollection<Product> GetProductsCollection() => _productsCollection;
        public IMongoCollection<Category> GetCategoriesCollection() => _categoriesCollection;
    }
}
