using System;
using System.Collections.Generic;

namespace LorKingDom.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Sku { get; set; }

    public int? CategoryId { get; set; }

    public int? MaterialId { get; set; }

    public int? AgeId { get; set; }

    public int? SexId { get; set; }

    public int? PriceRangeId { get; set; }

    public int? BrandId { get; set; }

    public int? OriginId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Age? Age { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual Material? Material { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Origin? Origin { get; set; }

    public virtual PriceRange? PriceRange { get; set; }

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Sex? Sex { get; set; }
}
