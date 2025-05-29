using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.LogOut;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.LogOut
{
    /// <summary>  
    /// Proporciona la funcionalidad para cerrar la sesión del usuario actual.  
    /// </summary>  
    public class LogOut : ILogOut
    {
        private readonly IFirebaseAuthentication _firebaseAuthentication;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="LogOut"/>.  
        /// </summary>  
        /// <param name="firebaseAuthentication">Instancia de autenticación de Firebase.</param>  
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="firebaseAuthentication"/> es nulo.</exception>  
        public LogOut(IFirebaseAuthentication firebaseAuthentication)
        {
            _firebaseAuthentication = firebaseAuthentication ?? throw new ArgumentNullException(nameof(firebaseAuthentication));
        }

        /// <summary>  
        /// Cierra la sesión del usuario actual.  
        /// </summary>  
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        public void CerrarSesionAsync()
        {
            _firebaseAuthentication.GetInstance().SignOut();
        }
    }
}
