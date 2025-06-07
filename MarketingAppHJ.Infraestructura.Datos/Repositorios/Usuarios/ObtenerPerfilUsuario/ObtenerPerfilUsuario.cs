using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.IObtenerPerfilUsuario;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Usuarios.ObtenerPerfilUsuario
{
    /// <summary>  
    /// Clase que implementa la funcionalidad para obtener el perfil de un usuario desde Firebase Realtime Database.  
    /// </summary>  
    public class ObtenerPerfilUsuario : IObtenerPerfilUsuario
    {
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="ObtenerPerfilUsuario"/>.  
        /// </summary>  
        /// <param name="firebaseRealtimeDatabase">Instancia de la base de datos Firebase Realtime Database.</param>  
        public ObtenerPerfilUsuario(IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase;
        }

        /// <summary>  
        /// Obtiene el perfil de un usuario por su identificador.  
        /// </summary>  
        /// <param name="usuarioId">El identificador único del usuario.</param>  
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene los datos del usuario.</returns>  
        public async Task<UsuarioDto> ObtenerPerfilUsuarioAsync(string usuarioId)
        {
            var usuario = await _firebaseRealtimeDatabase.Instance
                .Child($"usuarios/{usuarioId}")
                .OnceSingleAsync<UsuarioDto>();

            return usuario;
        }
    }
}
