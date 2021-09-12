using System.Collections.Generic;
using HepsiYemek.Entities;

namespace HepsiYemek.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetList();
        Product Get(string id);
        Product Add(Product product);
        void Delete(string id);
        Product Update(Product product);

    }
}
