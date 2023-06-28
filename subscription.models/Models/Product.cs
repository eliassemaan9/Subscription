using System;
using System.Collections.Generic;

namespace subscription.models.Models;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
