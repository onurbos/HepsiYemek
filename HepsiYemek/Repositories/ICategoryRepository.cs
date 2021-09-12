using System.Collections.Generic;
using HepsiYemek.Entities;

namespace HepsiYemek.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetList();
        Category Get(string id);
        Category Add(Category category);
        void Delete(string id);
        Category Update(Category category);

    }
}
