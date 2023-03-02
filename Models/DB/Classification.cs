using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class Classification
{
    public int Id { get; set; }

    public string? Descripton { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
