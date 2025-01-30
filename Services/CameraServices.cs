using Flir.Atlas.Image;
using Flir.Atlas.Live.Device;
using Flir.Atlas.Live.Discovery;
using Flir.Atlas.Live.Remote;
using System;

namespace CameraAPI.Services
{
    /// <summary>
    /// Classe responsável pela interface com a câmera.
    /// </summary>
    public class CameraService
    {   
            
        private static readonly Lazy<CameraService> _instance = new Lazy<CameraService>(() => new CameraService());
        
        /// <summary>
        /// No comments.
        /// </summary> 
        public static CameraService Instance => _instance.Value;

        private ThermalCamera Camera;

        /// <summary>
        /// Conecta a câmera térmica ao sistema.
        /// </summary>
        /// <param name="ipAddress">O endereço IP da câmera.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        public void Connect(string ipAddress)
        {
            Disconnect();
            var device = CameraDeviceInfo.Create(ipAddress, Interface.Gigabit, ImageFormat.FlirFileFormat);
            if (device != null)
            {
                Camera = new ThermalCamera();
                Camera.ImageInitialized += OnImageInitialized; // Subscrição ao evento
                Camera.Connect(device);
                Console.WriteLine($"Câmera conectada no IP: {ipAddress}");
            }
            else
            {
                Console.WriteLine("Não foi possível conectar à câmera.");
            }
        }

        /// <summary>
        /// Desconecta a câmera térmica.
        /// </summary>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        public void Disconnect()
        {
            if (Camera != null)
            {
                Camera.Disconnect();
                Camera.Dispose();
                Camera = null;
                Console.WriteLine("Câmera desconectada.");
            }
        }

        /// <summary>
        /// Salva a imagem atual da câmera térmica.
        /// </summary>
        /// /// <param name="savePath">O caminho onde será salvo a imagem.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>o
        public void CaptureImage(string savePath)
        {
            if (Camera == null)
            {
                throw new InvalidOperationException("Câmera não conectada.");
            }

            var image = Camera.GetImage();
            if (image is ThermalImage thermalImage)
            {
                thermalImage.Scale.IsAutoAdjustEnabled = true;
                thermalImage.SaveSnapshot(savePath);
                Console.WriteLine($"Imagem capturada e salva em: {savePath}");
            }
            else
            {
                Console.WriteLine("Erro: Imagem não disponível.");
            }
        }

        /// <summary>
        /// Faz a calibração automática do foco da câmera.
        /// </summary>
        public void SetAutoFocus()
        {
            if (Camera != null && Camera.RemoteControl != null)
            {
                Camera.RemoteControl.Focus.Mode(FocusMode.Auto);
                Console.WriteLine("Foco automático ajustado com sucesso.");
            }
            else
            {
                Console.WriteLine("Câmera ou controle remoto não disponível.");
            }
        }

        /// <summary>
        /// Obtém o status da câmera térmica.
        /// </summary>
        public string GetStatus()
        {
            return Camera != null && Camera.IsConnected ? "Conectada" : "Desconectada";
        }

        // Evento acionado quando a imagem é inicializada
        private void OnImageInitialized(object sender, EventArgs e)
        {
            try
            {
                var image = GetImage();
                if (image == null)
                {
                    Console.WriteLine("Imagem não está disponível no momento.");
                    return;
                }

                image.EnterLock();
                if (image is ThermalImage thermalImage)
                {
                    thermalImage.Scale.IsAutoAdjustEnabled = true;
                    SetAutoFocus();
                    Console.WriteLine("Imagem inicializada e ajustada.");
                }
                image.ExitLock();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar imagem: {ex.Message}");
            }
        }

        // Método auxiliar para obter a imagem atual da câmera
        private ImageBase GetImage()
        {
            return Camera?.GetImage();
        }
    }
}
