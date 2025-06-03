using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ActualizarUsuario
{
    /// <summary>  
    /// Interfaz para actualizar la información de un usuario.  
    /// </summary>  
    public interface IActualizarUsuario
    {
        /// <summary>  
        /// Actualiza la información de un usuario en el sistema.  
        /// </summary>  
        /// <param name="usuario">Objeto de tipo <see cref="UsuarioDto"/> que contiene la información actualizada del usuario.</param>  
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        Task ActualizarUsuarioAsync(UsuarioDto usuario);
    }
}
