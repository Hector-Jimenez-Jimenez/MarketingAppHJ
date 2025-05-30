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
        public FirebaseClient Instance { get; } = new FirebaseClient("https://themarketingapp-15895-default-rtdb.firebaseio.com/");

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="FirebaseRealTimeDatabase"/>.  
        /// </summary>  
        /// <param name="firebaseClient">El cliente de Firebase que se utilizará para interactuar con la base de datos en tiempo real.</param>  
        /// <exception cref="ArgumentNullException">Se lanza si el cliente de Firebase proporcionado es nulo.</exception>  
        public FirebaseRealTimeDatabase(FirebaseClient firebaseClient)
        {
            if (firebaseClient == null)
            {
                throw new ArgumentNullException(nameof(firebaseClient), "El cliente de Firebase no puede ser nulo.");
            }
            Instance = firebaseClient;
        }

        /// <summary>  
        /// Crea una nueva instancia del cliente de Firebase con la URL especificada.  
        /// </summary>  
        /// <param name="url">La URL de la base de datos en tiempo real de Firebase.</param>  
        /// <returns>Una nueva instancia de <see cref="FirebaseClient"/>.</returns>  
        public FirebaseClient Create(string url) => new FirebaseClient(url);
    }
}
