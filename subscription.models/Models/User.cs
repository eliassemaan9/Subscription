using System;
using System.Collections.Generic;

namespace subscription.models.Models;

public partial class User
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string NormalizedUserName { get; set; }

    public string Email { get; set; }

    public string NormalizedEmail { get; set; }

    public short EmailConfirmed { get; set; }

    public string PasswordHash { get; set; }

    public string PhoneNumber { get; set; }

    public short? PhoneNumberConfirmed { get; set; }

    public short? IsActive { get; set; }

    public short? IsDeleted { get; set; }

    public string Salt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
