﻿using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class Customer
{
    public int Id { get; set; }

    public string? NameCustomer { get; set; }

    public string? LastNameCustomer { get; set; }

    public string? IdNumber { get; set; }

    public string? TelNumber { get; set; }

    public string? Address { get; set; }

    public string? Correo { get; set; }

    public int TypeIdId { get; set; }

    public string TypeIdCodId { get; set; } = null!;

    public virtual ICollection<Orderscostumer> Orderscostumers { get; } = new List<Orderscostumer>();

    public virtual Typeid Type { get; set; } = null!;
}
