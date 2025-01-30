using System.Web.Http;
using System;
using CameraAPI.Services;

namespace CameraAPI.Controller
{
    [RoutePrefix("api/camera")]
    public class CameraController : ApiController
    {
        private readonly CameraService _cameraService = CameraService.Instance;

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
