using DBLaba2.SqlModels;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DBLaba2.NoSqlModels
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int? ProductId { get; set; }

        public int? QuantityInStock { get; set; }

        public DateOnly? StockUpdateDate { get; set; }

        public DateOnly? LastStockUpdateDate { get; set; }

        public virtual Product? Product { get; set; }
    }
}
