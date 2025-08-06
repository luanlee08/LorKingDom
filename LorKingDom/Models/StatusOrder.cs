using System;
using System.Collections.Generic;

namespace LorKingDom.Models;

public partial class StatusOrder
{
    public int StatusId { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
