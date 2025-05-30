using Firebase.Auth;
using Firebase.Auth.Providers;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;

namespace MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.Authentication
{
    /// <summary>  
    /// Proporciona autenticación utilizando Firebase.  
    /// </summary>  
    public class FirebaseAuthentication : IFirebaseAuthentication
    {
        private const string _ApiKey = "AIzaSyB8LYJu_vfyaC4zsJXpnXgTVYKU685AMow";
        private const string _AuthDomain = "themarketingapp-15895.firebaseapp.com";

        /// <summary>  
        /// Obtiene la instancia del cliente de autenticación de Firebase.  
        /// </summary>  
        private readonly FirebaseAuthClient Instance = new
            (
                new FirebaseAuthConfig()
                {
                    ApiKey = _ApiKey,
                    AuthDomain = _AuthDomain,
                    Providers = new FirebaseAuthProvider[]
                    {
                              new EmailProvider()
                    }
                }
            );

        /// <summary>  
        /// Obtiene el identificador único del usuario autenticado.  
        /// </summary>  
        public string UserId => Instance.User?.Uid ?? string.Empty;

        /// <summary>  
        /// Devuelve la instancia del cliente de autenticación de Firebase.  
        /// </summary>  
        /// <returns>Instancia de <see cref="FirebaseAuthClient"/>.</returns>  
        public FirebaseAuthClient GetInstance()
        {
            return Instance;
        }

        public async Task<string> GetTokenAsync()
        {
            if(Instance == null) return string.Empty;

            return await Instance.User.GetIdTokenAsync();
        }
    }
}
