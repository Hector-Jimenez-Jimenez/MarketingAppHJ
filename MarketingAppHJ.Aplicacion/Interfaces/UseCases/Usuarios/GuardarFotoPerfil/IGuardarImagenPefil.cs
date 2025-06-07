using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.GuardarFotoPerfil
{
    /// <summary>
    /// Define un contrato para guardar la imagen de perfil de un usuario.
    /// </summary>
    public interface IGuardarImagenPefil
    {
        /// <summary>
        /// Guarda la URL de la imagen de perfil de un usuario.
        /// </summary>
        /// <param name="userId">El identificador único del usuario.</param>
        /// <param name="ImagenPerfilUrl">La URL de la imagen de perfil.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task GuardarImagenPerfil(string userId, string ImagenPerfilUrl);
    }
}
