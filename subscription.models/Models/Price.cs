using System;
using System.Collections.Generic;

namespace subscription.models.Models;

public partial class Price
{
    public long Id { get; set; }

    public long? SubscriptionId { get; set; }

    public decimal? Price1 { get; set; }

    public int? CurrencyId { get; set; }

    public string Description { get; set; }

    public int? ProductId { get; set; }

    public virtual Subscription Subscription { get; set; }
}
