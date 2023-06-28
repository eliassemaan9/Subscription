using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.models.DTO
{
    public class SubscriptionDTO
    {
        public long id { get; set; }

        public long? userId { get; set; }

        public string name { get; set; }

        public int? status { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public DateTime? cancelAt { get; set; }

        public DateTime? canceledAt { get; set; }

        public DateTime? endedAt { get; set; }

        public long? productId { get; set; }
    }
}
