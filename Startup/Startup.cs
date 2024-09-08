using FacilityCore.API.Autenticacao.JWT;
using FacilityCore.API.Autenticacao.JWT.Models;

namespace FacilityCore.API
{
    public class Startup
    {
        // Propriedade para acessar as configurações da aplicação.
        public IConfiguration Configuration { get; }

        // Construtor que recebe a configuração da aplicação.
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Registra os serviços necessários para a aplicação.
        /// </summary>
        /// <param name="services">Coleção de serviços.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Carrega as configurações do JWT a partir do arquivo appsettings.json.
            var configuracoesJwt = Configuration.GetSection("JwtSettings").Get<ConfiguracoesJwt>();

            // Registra as configurações JWT e o serviço de geração de tokens.
            services.AddSingleton(configuracoesJwt);
            services.AddScoped<GerarTokenJwt>();
        }

        /// <summary>
        /// Configura o pipeline da aplicação ASP.NET Core.
        /// </summary>
        /// <param name="app">Construtor do pipeline da aplicação.</param>
        public void Configure(IApplicationBuilder app)
        {
            // Adiciona o uso de roteamento e endpoints básicos para a aplicação.
            app.UseRouting();

            // Define o mapeamento dos endpoints.
            app.UseEndpoints(endpoints =>
            {
                // Adicione aqui as rotas de controladores ou endpoints.
            });
        }
    }
}
