using System;
using System.Collections.Generic;

namespace subscription.models.Models;

public partial class Subscription
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public string Name { get; set; }

    public int? Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CancelAt { get; set; }

    public DateTime? CanceledAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();

    public virtual User User { get; set; }
}
