﻿using System;
using System.Collections.Generic;

namespace LorKingDom.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int AccountId { get; set; }

    public int ProductId { get; set; }

    public string? ImgReview { get; set; }

    public bool IsDeleted { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = null!;

    public byte Status { get; set; }

    public DateTime ReviewedAt { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
