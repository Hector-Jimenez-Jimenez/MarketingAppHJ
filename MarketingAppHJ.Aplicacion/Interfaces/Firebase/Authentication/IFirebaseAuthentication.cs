using Firebase.Auth;

namespace MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication
{
    /// <summary>
    /// Interfaz para la autenticación de Firebase.
    /// </summary>
    public interface IFirebaseAuthentication
    {
        /// <summary>
        /// Obtiene el cliente de autenticación de Firebase.
        /// </summary>
        public FirebaseAuthClient Instance { get; }
    }
}
