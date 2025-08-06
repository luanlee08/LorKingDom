using System;
using System.Collections.Generic;

namespace LorKingDom.Models;

public partial class ShippingMethod
{
    public int ShippingMethodId { get; set; }

    public string MethodName { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
