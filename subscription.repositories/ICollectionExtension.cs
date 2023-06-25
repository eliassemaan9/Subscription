using Microsoft.Extensions.DependencyInjection;
using subscription.repositories.Helper;

namespace subscription.repositories
{
    public static class ICollectionExtension
    {
        public static IServiceCollection  AddServiceRepositories(this IServiceCollection services)
        {
            services.AddTransient<IHelper,Helpers>();
            return services;
        }
    }
}