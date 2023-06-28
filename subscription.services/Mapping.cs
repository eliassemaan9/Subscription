using AutoMapper;
using subscription.models.DTO;
using subscription.models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subscription.services
{
    public class Mapping:Profile
    {
        public Mapping() {
            CreateMap<SubscriptionDTO, Subscription>()
                .ForMember(c => c.Id, o => o.MapFrom(src => src.id))
                .ForMember(c => c.CancelAt, o => o.MapFrom(src => src.cancelAt))
                .ForMember(c => c.CanceledAt, o => o.MapFrom(src => src.canceledAt))
                .ForMember(c => c.StartDate, o => o.MapFrom(src => src.startDate))
                .ForMember(c => c.Status, o => o.MapFrom(src => src.status))
                .ForMember(c => c.EndDate, o => o.MapFrom(src => src.endDate))
                .ForMember(c => c.ProductId, o => o.MapFrom(src => src.productId))
                 .ForMember(c => c.UserId, o => o.MapFrom(src => src.userId))
                 .ForMember(c => c.EndedAt, o => o.MapFrom(src => src.endedAt))
                 .ForMember(c => c.Name, o => o.MapFrom(src => src.name))
                 .ForMember(c => c.User, o => o.Ignore())
                 .ForMember(c => c.Product, o => o.Ignore())
                 .ForMember(c => c.Discounts, o => o.Ignore())
                 .ForMember(c => c.Prices ,o => o.Ignore());
        }
    }
}
