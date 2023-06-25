using System;
using System.Collections.Generic;

namespace subscription.models.Models;

public partial class DiscountDetail
{
    public long Id { get; set; }

    public long? DiscountId { get; set; }

    public DateTime? RedeemDate { get; set; }

    public long? RedeemedCount { get; set; }

    public short? Status { get; set; }

    public long? RedemtionsLimit { get; set; }

    public virtual Discount Discount { get; set; }
}
