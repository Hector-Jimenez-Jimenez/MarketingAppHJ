using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MarketingAppHJ.Aplicacion.Interfaces.Cloudinary;
using Microsoft.Extensions.Options;

namespace MarketingAppHJ.Infraestructura.Negocio.Servicios.CloudinaryService
{
    /// <summary>
    /// Servicio para interactuar con la API de Cloudinary.
    /// </summary>
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CloudinaryService"/>.
        /// </summary>
        /// <param name="options">Configuración de Cloudinary.</param>
        /// <exception cref="ArgumentException">Se lanza si los parámetros de configuración son inválidos.</exception>
        public CloudinaryService(IOptions<CloudinarySettings> options)
        {
            var config = options.Value;

            if (string.IsNullOrWhiteSpace(config.CloudName) ||
                string.IsNullOrWhiteSpace(config.ApiKey) ||
                string.IsNullOrWhiteSpace(config.ApiSecret))
            {
                throw new ArgumentException("Parámetros de Cloudinary no válidos.");
            }

            var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        /// <summary>
        /// Sube una imagen a Cloudinary de forma asíncrona.
        /// </summary>
        /// <param name="flujoImagen">El flujo de datos de la imagen a subir.</param>
        /// <param name="nombreArchivo">El nombre del archivo de la imagen.</param>
        /// <returns>La URL segura de la imagen subida.</returns>
        /// <exception cref="Exception">Se lanza si ocurre un error al subir la imagen.</exception>
        public async Task<string> SubirImagenAsync(Stream flujoImagen, string nombreArchivo)
        {
            var parametrosSubida = new ImageUploadParams
            {
                File = new FileDescription(nombreArchivo, flujoImagen),
                PublicId = nombreArchivo,
                Overwrite = true,
                Folder = "IMAGENES_PERFIL"
            };

            var resultadoSubida = await _cloudinary.UploadAsync(parametrosSubida);

            if (resultadoSubida.StatusCode == System.Net.HttpStatusCode.OK)
                return resultadoSubida.SecureUrl.ToString();

            throw new Exception("Error al subir la imagen");
        }
    }
}
