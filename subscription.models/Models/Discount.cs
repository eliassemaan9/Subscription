using System;
using System.Collections.Generic;

namespace subscription.models.Models;

public partial class Discount
{
    public long Id { get; set; }

    public long? SubscriptionId { get; set; }

    public long? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string PromotionCode { get; set; }

    public virtual ICollection<DiscountDetail> DiscountDetails { get; set; } = new List<DiscountDetail>();

    public virtual Subscription Subscription { get; set; }

    public virtual User User { get; set; }
}
