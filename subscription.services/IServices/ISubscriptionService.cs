using subscription.models.DTO;
using subscription.models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.services.IServices
{
    public interface ISubscriptionService
    {
        List<Subscription> GetByUserId(long userId);
        Subscription GetSubscriptionByUserIdProductId(long productId, long userId);
        Subscription Add(SubscriptionDTO subscription);
        Subscription update(SubscriptionDTO subscription);
    }
}
