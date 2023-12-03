using System;
using System.Collections.Generic;

namespace DBLaba2.SqlModels;

public partial class Inventory
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? QuantityInStock { get; set; }

    public DateOnly? StockUpdateDate { get; set; }

    public DateOnly? LastStockUpdateDate { get; set; }

    public virtual Product? Product { get; set; }
}
