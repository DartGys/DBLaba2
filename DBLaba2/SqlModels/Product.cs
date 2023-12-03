using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace DBLaba2.SqlModels;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public byte[] LastModifiedDate { get; set; } = null!;

    public int? ModifiedBy { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<SpecialOffer> SpecialOffers { get; set; } = new List<SpecialOffer>();
}
