using System.Web.Http;
using Owin;
using System.Web.Http.Dependencies; // Para IDependencyResolver e IDependencyScope
using System.Collections.Generic;  // Para IEnumerable<object>
using System.Linq; // Para Enumerable.Empty<object>()
using CameraAPI.Services;
using System;
using Swashbuckle.Application;

namespace CameraAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            try
            {
                // Configuração de rotas
                Console.WriteLine("Inicializando configurações de rotas...");
                config.MapHttpAttributeRoutes();
                Console.WriteLine("Rotas configuradas com sucesso.");

                // Configuração do Swagger antes de aplicar o DependencyResolver
                config.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Camera API")
                     .Description("API para controle da câmera térmica")
                     .Contact(cc => cc.Name("Willam Castro").Email("willam.castro@outlook.com"));
                })
                .EnableSwaggerUi();

                // Registro do serviço como singleton
                config.DependencyResolver = new SimpleDependencyResolver();

                // Log de controladores detectados
                foreach (var controller in config.Services.GetHttpControllerSelector().GetControllerMapping())
                {
                    Console.WriteLine($"Controller detectado: {controller.Key}, Tipo: {controller.Value.ControllerType}");
                }

                // Inicializando o pipeline OWIN
                Console.WriteLine("Inicializando servidor Web API...");
                appBuilder.UseWebApi(config);
                Console.WriteLine("Servidor Web API iniciado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante a inicialização: {ex.Message}");
                throw;
            }
        }
    }

    public class SimpleDependencyResolver : IDependencyResolver
    {
        private readonly CameraService _cameraService = new CameraService();

        // Retorna um serviço específico
        public object GetService(Type serviceType)
        {
            return serviceType == typeof(CameraService) ? _cameraService : null;
        }

        // Retorna uma coleção de serviços (não usada no momento)
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }

        // Implementação de BeginScope (retorna o próprio resolvedor)
        public IDependencyScope BeginScope()
        {
            return this;
        }

        // Libera recursos alocados
        public void Dispose()
        {
            _cameraService.Disconnect();
            Console.WriteLine("Recursos do CameraService liberados.");
        }
    }
}
