using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ResetConstraseña;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.ResetearContrasena
{
    /// <summary>  
    /// Clase que implementa la funcionalidad para restablecer la contraseña de un usuario.  
    /// </summary>  
    public class ResetearContrasena : IResetContrasena
    {
        private readonly IFirebaseAuthentication _firebaseAuthentication;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="ResetearContrasena"/>.  
        /// </summary>  
        /// <param name="firebaseAuthentication">Instancia de autenticación de Firebase.</param>  
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="firebaseAuthentication"/> es nulo.</exception>  
        public ResetearContrasena(IFirebaseAuthentication firebaseAuthentication)
        {
            _firebaseAuthentication = firebaseAuthentication ?? throw new ArgumentNullException(nameof(firebaseAuthentication));
        }

        /// <summary>  
        /// Envía un correo electrónico para restablecer la contraseña del usuario.  
        /// </summary>  
        /// <param name="email">Correo electrónico del usuario.</param>  
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        /// <exception cref="Exception">Se lanza si ocurre un error al enviar el correo de restablecimiento.</exception>  
        public async Task ResetearContrasenaAsync(string email)
        {
            try
            {
                await _firebaseAuthentication.GetInstance()
                    .ResetEmailPasswordAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo de restablecimiento de contraseña: " + ex.Message);
            }
        }
    }
}
