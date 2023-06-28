using AutoMapper;
using subscription.models.DTO;
using subscription.models.Models;
using subscription.repositories.IRepositories;
using subscription.services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.services.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private IMapper _mapper;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper) 
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }
        public List<Subscription> GetByUserId(long userId)
        {
            return _subscriptionRepository.GetByUserId(userId);
        }
        public Subscription GetSubscriptionByUserIdProductId(long productId, long userId)
        {
            return _subscriptionRepository.GetSubscriptionByUserIdProductId(productId,userId);

        }
        public Subscription Add(SubscriptionDTO subscriptionDTO)
        {
            Subscription subscription = _mapper.Map<SubscriptionDTO,Subscription>(subscriptionDTO);
            return _subscriptionRepository.Add(subscription);

        }
        public Subscription update(SubscriptionDTO subscriptionDTO)
        {
            Subscription subscription = _mapper.Map<SubscriptionDTO, Subscription>(subscriptionDTO);

            return _subscriptionRepository.update(subscription);
        }
    }
}
