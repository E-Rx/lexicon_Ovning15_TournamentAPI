using Service.Contracts;

namespace Tournament.API.Extensions
{

    public static class ServiceExtensions
    {
        public static IServiceCollection AddLazyServices(this IServiceCollection services)
        {
            services.AddScoped<Lazy<ITournamentDetailsService>>(provider =>
                new Lazy<ITournamentDetailsService>(() => provider.GetRequiredService<ITournamentDetailsService>()));
            services.AddScoped<Lazy<IGameService>>(provider =>
                new Lazy<IGameService>(() => provider.GetRequiredService<IGameService>()));

            return services;
        }
    }
}