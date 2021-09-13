using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HepsiYemek.Enums;

namespace HepsiYemek.Entities
{
    public class Product
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required, BsonDefaultValue(CurrencyType.TL)]
        public CurrencyType Currency { get; set; }

        //public virtual Category Category { get; set; }
    }
}
