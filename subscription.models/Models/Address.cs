using System;
using System.Collections.Generic;

namespace subscription.models.Models;

public partial class Address
{
    public long Id { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }

    public string State { get; set; }

    public long? UserId { get; set; }

    public virtual User User { get; set; }
}
