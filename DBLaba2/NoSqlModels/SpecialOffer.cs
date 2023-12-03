using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DBLaba2.NoSqlModels
{
    public class SpecialOffer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int? ProductId { get; set; }

        public decimal? Discount { get; set; }

        public DateOnly? ValidUntil { get; set; }

        public DateOnly? RegistrationDate { get; set; }

        public byte[] LastModifiedDate { get; set; } = null!;

        public int? ModifiedBy { get; set; }

        public virtual Product? Product { get; set; }
    }
}
