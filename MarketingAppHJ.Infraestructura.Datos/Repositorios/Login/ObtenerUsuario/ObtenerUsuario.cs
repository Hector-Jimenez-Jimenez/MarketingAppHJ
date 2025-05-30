using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ObtenerUsuario;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.ObtenerUsuario
{
    /// <summary>  
    /// Clase que implementa la funcionalidad para obtener información de un usuario desde la base de datos en tiempo real de Firebase.  
    /// </summary>  
    public class ObtenerUsuario : IObtenerUsuario
    {
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="ObtenerUsuario"/> con la dependencia de FirebaseRealtimeDatabase.  
        /// </summary>  
        /// <param name="firebaseRealtimeDatabase">Instancia de la base de datos en tiempo real de Firebase.</param>  
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="firebaseRealtimeDatabase"/> es nulo.</exception>  
        public ObtenerUsuario(IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase ?? throw new ArgumentNullException(nameof(firebaseRealtimeDatabase));
        }

        /// <summary>  
        /// Obtiene la información de un usuario por su ID desde la base de datos en tiempo real de Firebase.  
        /// </summary>  
        /// <param name="idUsuario">El identificador único del usuario.</param>  
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene la información del usuario.</returns>  
        /// <exception cref="Exception">Se lanza si el usuario con el ID especificado no se encuentra.</exception>  
        public async Task<UsuarioDto> ObtenerUsuarioAsync(string idUsuario)
        {
            var usuario = await _firebaseRealtimeDatabase.Instance
                .Child($"usuarios/{idUsuario}")
                .OnceSingleAsync<UsuarioDto>();

            return usuario ?? throw new Exception($"Usuario con ID {idUsuario} no encontrado.");
        }
    }
}
