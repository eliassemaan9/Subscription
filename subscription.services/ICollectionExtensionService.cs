using Microsoft.Extensions.DependencyInjection;
using subscription.services.IServices;
using subscription.services.Services;

namespace subscription.services
{
    public static class ICollectionExtensionService
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddTransient<IBasicService, BasicService>();
            services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddAutoMapper(typeof(ICollectionExtensionService));
            return services;
        }
    }
}