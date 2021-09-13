using System.Collections.Generic;
using HepsiYemek.Entities;
using MongoDB.Bson;

namespace HepsiYemek.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetList();
        BsonDocument GetByIdAsBsonDoc(string id);
        Category Add(Category category);
        void Delete(string id);
        Category Update(Category category);
        Category Get(string id);

    }
}
