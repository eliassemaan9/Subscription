using System;
using System.Collections.Generic;

namespace subscription.models.Models;

public partial class Lookup
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string LookupCode { get; set; }

    public string Name { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }
}
