using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase
{
    /// <summary>  
    /// Interface para interactuar con la base de datos en tiempo real de Firebase.  
    /// </summary>  
    public interface IFirebaseRealtimeDatabase
    {
        /// <summary>  
        /// Obtiene la instancia del cliente de Firebase.  
        /// </summary>  
        FirebaseClient Instance { get; }

    }
}
