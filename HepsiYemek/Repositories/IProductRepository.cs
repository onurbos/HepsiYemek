using System.Collections.Generic;
using System.Threading.Tasks;
using HepsiYemek.Entities;
using MongoDB.Bson;

namespace HepsiYemek.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetList();
        BsonDocument GetByIdAsBsonDoc(string id);
        Product Add(Product product);
        void Delete(string id);
        Product Update(Product product);
        
        Product Get(string id);
    }
}
