using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Interfaces.Cloudinary
{
    /// <summary>
    /// Representa la configuración necesaria para interactuar con el servicio de Cloudinary.
    /// </summary>
    public class CloudinarySettings
    {
        /// <summary>
        /// Nombre de la nube asociado a la cuenta de Cloudinary.
        /// </summary>
        public string CloudName { get; set; } = string.Empty;

        /// <summary>
        /// Clave de API utilizada para autenticar las solicitudes a Cloudinary.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Secreto de API utilizado para autenticar las solicitudes a Cloudinary.
        /// </summary>
        public string ApiSecret { get; set; } = string.Empty;
    }
}
