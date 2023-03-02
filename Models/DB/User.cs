using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RolesId { get; set; }

    public int StateId { get; set; }

    public virtual ICollection<Orderscostumer> Orderscostumers { get; } = new List<Orderscostumer>();

    public virtual Role Roles { get; set; } = null!;

    public virtual State State { get; set; } = null!;
}
