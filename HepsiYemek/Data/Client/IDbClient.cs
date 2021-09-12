using MongoDB.Driver;
using HepsiYemek.Entities;

namespace HepsiYemek.Data.Configs
{
    public interface IDbClient
    {
        IMongoCollection<Product> GetProductsCollection();
        IMongoCollection<Category> GetCategoriesCollection();
    }

}
