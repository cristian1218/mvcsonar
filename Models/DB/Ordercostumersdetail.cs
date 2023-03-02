using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class Ordercostumersdetail
{
    public int IdOrderCostumersDetail { get; set; }

    public uint OrdersCostumersIdOrder { get; set; }

    public int? Quantity { get; set; }

    public string? Price { get; set; }

    public int ProductsIdProducts { get; set; }

    public virtual Orderscostumer OrdersCostumersIdOrderNavigation { get; set; } = null!;

    public virtual Product ProductsIdProductsNavigation { get; set; } = null!;
}
