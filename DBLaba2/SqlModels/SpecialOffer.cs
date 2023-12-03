using System;
using System.Collections.Generic;

namespace DBLaba2.SqlModels;

public partial class SpecialOffer
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public decimal? Discount { get; set; }

    public DateOnly? ValidUntil { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public byte[] LastModifiedDate { get; set; } = null!;

    public int? ModifiedBy { get; set; }

    public virtual Product? Product { get; set; }
}
