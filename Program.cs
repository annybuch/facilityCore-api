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
            // Cria um host builder com as configurações padrão.
            CreateHostBuilder(args).Build().Run();
        }

        // Método responsável por criar o host builder com a configuração da aplicação.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Configura o host web para usar a startup da aplicação.
                    webBuilder.UseStartup<Startup>();
                });
    }
}
