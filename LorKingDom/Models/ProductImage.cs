﻿using System;
using System.Collections.Generic;

namespace LorKingDom.Models;

public partial class ProductImage
{
    public int ImageId { get; set; }

    public int ProductId { get; set; }

    public string Image { get; set; } = null!;

    public bool IsMain { get; set; }

    public virtual Product Product { get; set; } = null!;
}
