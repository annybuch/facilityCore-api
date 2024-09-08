namespace FacilityCore.API
{
    public class Program
    {
        /// <summary>
        /// Ponto de entrada da aplica��o ASP.NET Core.
        /// </summary>
        /// <param name="args">Argumentos de linha de comando.</param>
        public static void Main(string[] args)
        {
            // Cria um host builder com as configura��es padr�o.
            CreateHostBuilder(args).Build().Run();
        }

        // M�todo respons�vel por criar o host builder com a configura��o da aplica��o.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Configura o host web para usar a startup da aplica��o.
                    webBuilder.UseStartup<Startup>();
                });
    }
}
