using subscription.models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.repositories.IRepositories
{
    public interface ISubscriptionRepository
    {
        List<Subscription> GetByUserId(long userId);
        Subscription GetSubscriptionByUserIdProductId(long productId, long userId);
        Subscription Add(Subscription subscription);
        Subscription update(Subscription subscription);

    }
}
