using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        Task RegistrarUsuarioAsync(string email, string password);
    }
}
