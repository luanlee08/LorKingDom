using System;
using System.Collections.Generic;

namespace LorKingDom.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int AccountId { get; set; }

    public int StatusId { get; set; }

    public int PaymentMethodId { get; set; }

    public int ShippingMethodId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual ShippingMethod ShippingMethod { get; set; } = null!;

    public virtual StatusOrder Status { get; set; } = null!;
}
