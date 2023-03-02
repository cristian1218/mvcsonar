using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class Packing
{
    public int Id { get; set; }

    public string? DescriptionPacking { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
