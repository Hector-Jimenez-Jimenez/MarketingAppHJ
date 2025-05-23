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
        /// <summary>
        /// Obtiene la instancia del cliente de autenticación de Firebase.
        /// </summary>
        public FirebaseAuthClient Instance { get; } = new(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyB8LYJu_vfyaC4zsJXpnXgTVYKU685AMow",
            AuthDomain = "https://themarketingapp-15895-default-rtdb.firebaseio.com/",
            Providers = new FirebaseAuthProvider[]
            {
                    new EmailProvider()
            },
        });

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FirebaseAuthentication"/> con la configuración predeterminada.
        /// </summary>
        public FirebaseAuthentication()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FirebaseAuthentication"/> con una clave API y un dominio de autenticación específicos.
        /// </summary>
        /// <param name="apiKey">La clave API de Firebase.</param>
        /// <param name="authDomain">El dominio de autenticación de Firebase.</param>
        public FirebaseAuthentication(string apiKey, string authDomain)
        {
            Instance = new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = apiKey,
                AuthDomain = authDomain,
                Providers = new FirebaseAuthProvider[]
                {
                        new EmailProvider()
                }
            });
        }
    }
}
