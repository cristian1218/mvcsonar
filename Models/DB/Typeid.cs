using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class Typeid
{
    public int Id { get; set; }

    public string CodId { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();

    public virtual ICollection<Provider> Providers { get; } = new List<Provider>();
}
