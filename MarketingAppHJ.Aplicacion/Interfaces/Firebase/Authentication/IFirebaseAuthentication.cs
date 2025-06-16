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
        public FirebaseAuthClient GetInstance();

        /// <summary>
        /// Obtiene el token de autenticación de Firebase de forma asíncrona.
        /// </summary>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado contiene el token de autenticación.</returns>
        Task<string> GetTokenAsync();

        /// <summary>
        /// Obtiene el identificador único del usuario autenticado.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Cierra la sesión del usuario actual.
        /// </summary>
        Task SignOutAsync();
    }
}
