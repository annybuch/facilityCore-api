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
            // ** Cria um host builder com as configura��es padr�o.
            var hostBuilder = Host.CreateDefaultBuilder(args);

            // ** Configura o host web para usar a startup da aplica��o.
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

            // ** Constr�i e executa o host da aplica��o.
            hostBuilder.Build().Run();
        }
    }
}

