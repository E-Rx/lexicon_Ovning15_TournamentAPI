using Service.Contracts;

namespace Tournament.API.Extensions
{

    public static class ServiceExtensions
    {
        public static IServiceCollection AddLazyServices(this IServiceCollection services)
        {
            services.AddScoped<Lazy<ITournamentDetailsService>>(provider =>
                new Lazy<ITournamentDetailsService>(() => provider.GetService<ITournamentDetailsService>()));
            services.AddScoped<Lazy<IGameService>>(provider =>
                new Lazy<IGameService>(() => provider.GetService<IGameService>()));

            return services;
        }
    }
}