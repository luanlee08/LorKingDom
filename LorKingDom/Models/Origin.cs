using System;
using System.Collections.Generic;

namespace LorKingDom.Models;

public partial class Origin
{
    public int OriginId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
