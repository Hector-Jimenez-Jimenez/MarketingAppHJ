using System.ComponentModel;
using Firebase.Database;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;

namespace MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.RealtimeDatabase
{
    /// <summary>
    /// Proporciona una implementación para interactuar con Firebase Realtime Database.
    /// </summary>
    public class FirebaseRealTimeDatabase : IFirebaseRealtimeDatabase
    {
        /// <summary>
        /// Obtiene la instancia del cliente de Firebase.
        /// </summary>
        public FirebaseClient Instance { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FirebaseRealTimeDatabase"/>.
        /// </summary>
        
        public static FirebaseClient Create(string url) => new FirebaseClient(url);
    }
}
