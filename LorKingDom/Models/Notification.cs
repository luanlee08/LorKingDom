using System;
using System.Collections.Generic;

namespace LorKingDom.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? AccountId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Account? Account { get; set; }
}
