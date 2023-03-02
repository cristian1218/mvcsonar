using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class Orderscostumer
{
    public uint IdOrder { get; set; }

    public int CustomerId { get; set; }

    public DateTime? Date { get; set; }

    public string? Estado { get; set; }

    public int UsersId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Ordercostumersdetail> Ordercostumersdetails { get; } = new List<Ordercostumersdetail>();

    public virtual User Users { get; set; } = null!;
}
