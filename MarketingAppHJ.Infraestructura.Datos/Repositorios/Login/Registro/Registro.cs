using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.Registro;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.Registro
{
    /// <summary>  
    /// Clase que implementa la funcionalidad de registro de usuarios utilizando Firebase Authentication.  
    /// </summary>  
    public class Registro : IRegistro
    {
        private readonly IFirebaseAuthentication _firebaseAuthentication;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="Registro"/> con una dependencia de autenticación Firebase.  
        /// </summary>  
        /// <param name="firebaseAuthentication">Instancia de <see cref="IFirebaseAuthentication"/> para manejar la autenticación.</param>  
        public Registro(IFirebaseAuthentication firebaseAuthentication)
        {
            _firebaseAuthentication = firebaseAuthentication;
        }

        /// <summary>  
        /// Registra un nuevo usuario en Firebase Authentication con el correo electrónico y la contraseña proporcionados.  
        /// </summary>  
        /// <param name="email">Correo electrónico del usuario.</param>  
        /// <param name="password">Contraseña del usuario.</param>  
        /// <returns>Una tarea que representa la operación asincrónica de registro.</returns>  
        public async Task RegistrarUsuarioAsync(string email, string password)
        {
            await _firebaseAuthentication.GetInstance().CreateUserWithEmailAndPasswordAsync(email, password);
        }
    }
}
