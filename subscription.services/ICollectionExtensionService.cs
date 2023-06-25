using Microsoft.Extensions.DependencyInjection;

namespace subscription.services
{
    public static class ICollectionExtensionService
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            return services;
        }
    }
}