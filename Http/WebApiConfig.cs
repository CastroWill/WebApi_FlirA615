using System.Web.Http;
using Swashbuckle.Application;

namespace CameraAPI.Http
{
    /// <summary>
    /// Classe responsável pela criação da Web Api.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Método responsável pela configuração das rotas e do Swagger.
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            // Configuração de rotas
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Configuração do Swagger
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Camera API")
                 .Description("API para controle da câmera térmica")
                 .Contact(cc => cc.Name("Willam Castro").Email("willam.castro@outlook.com"));
            })
            .EnableSwaggerUi();
        }
    }
}
