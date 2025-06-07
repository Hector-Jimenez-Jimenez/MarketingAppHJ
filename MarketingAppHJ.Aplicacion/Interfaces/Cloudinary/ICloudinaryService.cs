namespace MarketingAppHJ.Aplicacion.Interfaces.Cloudinary
{
    /// <summary>
    /// Proporciona métodos para interactuar con el servicio de Cloudinary.
    /// </summary>
    public interface ICloudinaryService
    {
        /// <summary>
        /// Sube una imagen al servicio de Cloudinary de forma asíncrona.
        /// </summary>
        /// <param name="flujoImagen">El flujo de datos de la imagen que se va a subir.</param>
        /// <param name="nombreArchivo">El nombre del archivo de la imagen que se va a subir.</param>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado contiene la URL de la imagen subida.</returns>
        Task<string> SubirImagenAsync(Stream flujoImagen, string nombreArchivo);
    }
}
