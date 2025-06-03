using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.Registro
{
    /// <summary>  
    /// Interface para el registro de usuarios.  
    /// </summary>  
    public interface IRegistro
    {
        /// <summary>  
        /// Registra un nuevo usuario en el sistema.  
        /// </summary>  
        /// <param name="email">Correo electrónico del usuario.</param>  
        /// <param name="password">Contraseña del usuario.</param>  
        /// <param name="nombre">Nombre del usuario.</param>  
        /// <param name="apellidos">Apellidos del usuario.</param>  
        /// <param name="direccion"> Direccion de envio</param>
        /// <param name="telefono"> Teléfono del Usuario </param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        Task RegistrarUsuarioAsync(string email, string password, string nombre, string apellidos, string direccion, string telefono);

        /// <summary>  
        /// Guarda la información de un usuario en el sistema.  
        /// </summary>  
        /// <param name="usuario">Objeto que contiene los datos del usuario.</param>  
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        Task GuardarUsuarioAsync(UsuarioDto usuario);
    }
}
