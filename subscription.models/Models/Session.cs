using System;
using System.Collections.Generic;

namespace subscription.models.Models;

public partial class Session
{
    public long? UserId { get; set; }

    public DateTime? LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public long Id { get; set; }

    public string AccessToken { get; set; }

    public virtual User User { get; set; }
}
