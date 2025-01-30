using System;

namespace CameraAPI
{
    /// <summary>
    /// Classe responsável por todo o fluxo de inicialização.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Método principal.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("Inicializando o servidor Web API...");

            using (var server = Microsoft.Owin.Hosting.WebApp.Start<Startup>("http://localhost:5000"))
            {
                Console.WriteLine("Servidor iniciado em http://localhost:5000");
                Console.WriteLine("Pressione Ctrl+C para encerrar.");

                Console.CancelKeyPress += (sender, e) =>
                {
                    e.Cancel = true;
                    Console.WriteLine("Encerrando servidor...");
                    server.Dispose();
                    Console.WriteLine("Servidor encerrado com sucesso.");
                    Environment.Exit(0);
                };

                // Mantém o programa ativo
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
