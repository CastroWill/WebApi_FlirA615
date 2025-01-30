using System.Web.Http;
using System;
using CameraAPI.Services;

namespace CameraAPI.Controller
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento da câmera térmica.
    /// </summary>
    [RoutePrefix("api/camera")]
    public class CameraController : ApiController
    {
        private readonly CameraService _cameraService = CameraService.Instance;

        /// <summary>
        /// Conecta a câmera térmica ao sistema.
        /// </summary>
        /// <param name="ipAddress">O endereço IP da câmera.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPost]
        [Route("connect")]
        public IHttpActionResult Connect([FromBody] string ipAddress)
        {
            try
            {
                _cameraService.Connect(ipAddress);
                return Ok(new { status = "success", message = "Câmera conectada." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// Desconecta a câmera térmica.
        /// </summary>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPost]
        [Route("disconnect")]
        public IHttpActionResult Disconnect()
        {
            try
            {
                _cameraService.Disconnect();
                return Ok(new { status = "success", message = "Câmera desconectada." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Salva a imagem atual da câmera térmica.
        /// </summary>
        /// /// <param name="savePath">O caminho onde será salvo a imagem.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPost]
        [Route("capture")]
        public IHttpActionResult CaptureImage([FromBody] string savePath)
        {
            try
            {
                _cameraService.CaptureImage(savePath);
                return Ok(new { status = "success", message = "Imagem capturada.", path = savePath });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Faz a calibração automática do foco da câmera.
        /// </summary>
        [HttpPost]
        [Route("autoFocus")]
        public IHttpActionResult AutoFocus()
        {
            try
            {
                _cameraService.SetAutoFocus();
                return Ok(new { status = "success", message = "Foco automático ajustado." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Obtém o status da câmera térmica.
        /// </summary>
        [HttpGet]
        [Route("status")]
        public IHttpActionResult GetStatus()
        {
            try
            {
                var status = _cameraService.GetStatus();
                return Ok(new { status = "success", cameraStatus = status });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
