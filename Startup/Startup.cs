namespace FacilityCore.API
{
    public class Startup
    {
        // ** No caso da biblioteca, está vazio para iniciar.

        /// <summary>
        /// Registra os serviços necessários para a aplicação.
        /// </summary>
        /// <param name="services">Coleção de serviços.</param>
        public void ConfigureServices(IServiceCollection services) { }

        /// <summary>
        /// Configura o pipeline da aplicação ASP.NET Core.
        /// </summary>
        /// <param name="app">Construtor do pipeline da aplicação.</param>
        public void Configure(IApplicationBuilder app) { }
    }
}
