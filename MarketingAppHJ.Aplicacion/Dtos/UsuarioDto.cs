using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Dtos
{
    /// <summary>  
    /// Representa un usuario en el sistema.  
    /// </summary>  
    public class UsuarioDto
    {
        /// <summary>  
        /// Identificador único del usuario.  
        /// </summary>  
        public string Id_Usuario { get; set; } = string.Empty;

        /// <summary>  
        /// Nombre del usuario.  
        /// </summary>  
        public string Nombre { get; set; } = string.Empty;

        /// <summary>  
        /// Apellidos del usuario.  
        /// </summary>  
        public string Apellidos { get; set; } = string.Empty;

        /// <summary>  
        /// Correo electrónico del usuario.  
        /// </summary>  
        public string Email { get; set; } = string.Empty;

        /// <summary>  
        /// URL del avatar del usuario.  
        /// </summary>  
        public string AvatarUrl { get; set; } = string.Empty;

        /// <summary>  
        /// Fecha en la que el usuario se registró en el sistema.  
        /// </summary>  
        public DateTime FechaRegistro { get; set; }

        /// <summary>  
        /// Fecha de nacimiento del usuario.  
        /// </summary>  
        public DateTime FechaNacimiento { get; set; }

        /// <summary>  
        /// Número de teléfono del usuario.  
        /// </summary>  
        public string Telefono { get; set; } = string.Empty;

        /// <summary>  
        /// Dirección del usuario.  
        /// </summary>  
        public string Direccion { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de usuario del usuario.
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
