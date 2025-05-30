using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ObtenerUsuario
{
    /// <summary>  
    /// Interfaz para obtener información de un usuario específico.  
    /// </summary>  
    public interface IObtenerUsuario
    {
        /// <summary>  
        /// Obtiene los detalles de un usuario por su identificador.  
        /// </summary>  
        /// <param name="idUsuario">El identificador único del usuario.</param>  
        /// <returns>Un objeto <see cref="UsuarioDto"/> que contiene los detalles del usuario.</returns>  
        Task<UsuarioDto> ObtenerUsuarioAsync(string idUsuario);
    }
}
