using DBLaba2.SqlModels;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DBLaba2.NoSqlModels
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public DateOnly? RegistrationDate { get; set; }

        public byte[] LastModifiedDate { get; set; } = null!;

        public int? ModifiedBy { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        public virtual ICollection<SqlModels.SpecialOffer> SpecialOffers { get; set; } = new List<SqlModels.SpecialOffer>();
    }
}
