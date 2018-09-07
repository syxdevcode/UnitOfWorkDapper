using Microsoft.Extensions.DependencyInjection;
using System;
using UnitOfWorkDapper.Core;

namespace UnitOfWorkDapper.Web.Extensions
{
    public static class DapperDBContextExtensions
    {
        public static IServiceCollection AddDapperDBContext<T>(this IServiceCollection services, Action<DapperDBContextOptions> setupAction) where T : DapperDBContext
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.AddScoped<IContext, T>();

            return services;
        }
    }
}