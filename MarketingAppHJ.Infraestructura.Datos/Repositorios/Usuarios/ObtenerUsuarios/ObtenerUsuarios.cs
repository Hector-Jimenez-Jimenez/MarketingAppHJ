using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ObtenerUsuarios;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Usuarios.ObtenerUsuarios
{
    /// <summary>  
    /// Clase que implementa la funcionalidad para obtener usuarios desde Firebase Realtime Database.  
    /// </summary>  
    public class ObtenerUsuarios : IObtenerUsuarios
    {
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="ObtenerUsuarios"/>.  
        /// </summary>  
        /// <param name="firebaseRealtimeDatabase">Instancia de la base de datos Firebase Realtime Database.</param>  
        public ObtenerUsuarios(IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase;
        }

        /// <summary>  
        /// Obtiene una lista de usuarios desde Firebase Realtime Database.  
        /// </summary>  
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene una lista de objetos <see cref="UsuarioDto"/>.</returns>  
        public async Task<IEnumerable<UsuarioDto>> ObtenerUsuariosAsync()
        {
            var usuarios = await _firebaseRealtimeDatabase.Instance
                .Child("usuarios")
                .OnceAsync<UsuarioDto>();
            return usuarios.Select(u =>
            {
                var usuario = u.Object;
                usuario.Id_Usuario = u.Key;
                return usuario;
            }).ToList();
        }
    }
}
