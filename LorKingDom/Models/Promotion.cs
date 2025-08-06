using System;
using System.Collections.Generic;

namespace LorKingDom.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public int? ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string PromotionCode { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? DiscountPercent { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Product? Product { get; set; }
}
