using JPBM.Interfaces;
using JPBM.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JPBM.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigurarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ITipoContatoRepository, TipoContatoRepository>();
            services.AddTransient<IContatoRepository, ContatoRepository>();
            services.AddTransient<IItemRifaRepository, ItemRifaRepository>();
            services.AddTransient<IRifaRepository, RifaRepository>();
        }
    }
}
