using System;
using System.Collections.Generic;

namespace WebApp.Models.DB;

public partial class State
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
