using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class Provider
{
    public int Id { get; set; }

    public string? NameProvider { get; set; }

    public string? LastNameProvider { get; set; }

    public string? IdNumber { get; set; }

    public string? TelNumber { get; set; }

    public string? Address { get; set; }

    public string? Correo { get; set; }

    public int TypeIdId { get; set; }

    public string TypeIdCodId { get; set; } = null!;

    public virtual Typeid Type { get; set; } = null!;

    public virtual ICollection<Product> ProductsIdProducts { get; } = new List<Product>();
}
