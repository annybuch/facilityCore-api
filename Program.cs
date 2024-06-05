namespace FacilityCore.API
{
    public class Program
    {
        /// <summary>
        /// Ponto de entrada da aplicação ASP.NET Core.
        /// </summary>
        /// <param name="args">Argumentos de linha de comando.</param>
        public static void Main(string[] args)
        {
            // ** Cria um host builder com as configurações padrão.
            var hostBuilder = Host.CreateDefaultBuilder(args);

            // ** Configura o host web para usar a startup da aplicação.
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

            // ** Constrói e executa o host da aplicação.
            hostBuilder.Build().Run();
        }
    }
}

