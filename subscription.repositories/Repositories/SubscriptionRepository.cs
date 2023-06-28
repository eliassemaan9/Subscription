using Microsoft.AspNetCore.Mvc;
using subscription.models.Models;
using subscription.repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace subscription.repositories.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly SubscriptionContext _context;
        public SubscriptionRepository(SubscriptionContext context)
        {
            _context = context;
        }

        public Subscription Add(Subscription subscription)
        {
            try
            {
                _context.Subscriptions.Add(subscription);
                _context.SaveChanges();
                return subscription;
            }
            catch
            {
                throw new Exception("Unhandled Error exception");
            }

        }
        public Subscription update(Subscription subscription)
        {
            try
            {
                _context.Subscriptions.Update(subscription);
                _context.SaveChanges();
                return subscription;
            }
            catch
            {
                throw new Exception("Unhandled Error exception");
            }

        }
        public List<Subscription> GetByUserId(long userId)
        {
            List<Subscription> lstSubscription = new List<Subscription>();
            try
            {
                lstSubscription = _context.Subscriptions.Where(c => c.UserId == userId).ToList();
                if(lstSubscription.Count > 0)
                {
                    foreach(var item in lstSubscription)
                    {
                        item.User = null;
                        item.Product = null;
                        item.Prices = null;
                        item.Discounts = null;
                    }
               
                }
                return lstSubscription;
            }
            catch
            {
                    throw new Exception("Unhandled Error exception");
            }

           
        }
        public Subscription GetSubscriptionByUserIdProductId(long productId ,long userId)
        {
            Subscription subscription = new Subscription();
            try
            {
                subscription = _context.Subscriptions.Where(c => c.UserId == userId && c.ProductId == productId).FirstOrDefault();
                subscription.User = null;
                subscription.Product = null;
                subscription.Prices = null;
                subscription.Discounts = null;
                return subscription;
            }
            catch
            {
                throw new Exception("Unhandled Error exception");
            }
        }
    }
}
