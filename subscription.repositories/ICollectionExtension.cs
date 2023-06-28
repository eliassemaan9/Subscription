using Microsoft.Extensions.DependencyInjection;
using subscription.repositories.Helper;
using subscription.repositories.IRepositories;
using subscription.repositories.Log4net;
using subscription.repositories.Repositories;

namespace subscription.repositories
{
    public static class ICollectionExtension
    {
        public static IServiceCollection  AddServiceRepositories(this IServiceCollection services)
        {
            services.AddTransient<IHelper,Helpers>();
            services.AddTransient<IBasicRepository, BasicRepository>();
            services.AddTransient<ILog4net, subscription.repositories.Log4net.Log4net>();
            services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
            return services;
        }
    }
}