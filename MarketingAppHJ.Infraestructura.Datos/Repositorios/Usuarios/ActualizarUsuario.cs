using Firebase.Database.Query;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ActualizarUsuario;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.RealtimeDatabase;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Usuarios
{
    /// <summary>  
    /// Clase que implementa la funcionalidad para actualizar un usuario en la base de datos Firebase.  
    /// </summary>  
    public class ActualizarUsuario : IActualizarUsuario
    {
        private readonly FirebaseRealTimeDatabase _firebaseDatabase;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="ActualizarUsuario"/> con la base de datos Firebase proporcionada.  
        /// </summary>  
        /// <param name="firebaseDatabase">Instancia de la base de datos Firebase.</param>  
        public ActualizarUsuario(FirebaseRealTimeDatabase firebaseDatabase)
        {
            _firebaseDatabase = firebaseDatabase;
        }

        /// <summary>  
        /// Actualiza la información de un usuario en la base de datos Firebase.  
        /// </summary>  
        /// <param name="usuario">Objeto <see cref="UsuarioDto"/> que contiene la información del usuario a actualizar.</param>  
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        public async Task ActualizarUsuarioAsync(UsuarioDto usuario)
        {
            await _firebaseDatabase.Instance
                .Child($"usuarios/{usuario.Id_Usuario}")
                .PutAsync(usuario);
        }
    }
}
