using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class Product
{
    public int IdProducts { get; set; }

    public string? Descrption { get; set; } = null!;

    public int? PackingId { get; set; }

    public int? ClassificationId { get; set; }

    public int? Cost { get; set; }

    public virtual Classification? Classification { get; set; } = null!;

    public virtual ICollection<Ordercostumersdetail> Ordercostumersdetails { get; } = new List<Ordercostumersdetail>();

    public virtual Packing? Packing { get; set; } = null!;

    public virtual ICollection<Provider> Providers { get; } = new List<Provider>();
}
