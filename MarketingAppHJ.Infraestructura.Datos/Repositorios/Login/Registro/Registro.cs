using Firebase.Database.Query;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.Registro;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.Registro
{
    /// <summary>  
    /// Clase que implementa la funcionalidad de registro de usuarios utilizando Firebase Authentication.  
    /// </summary>  
    public class Registro : IRegistro
    {
        private readonly IFirebaseAuthentication _firebaseAuthentication;
        private readonly IFirebaseRealtimeDatabase _firebaseRealtimeDatabase;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="Registro"/> con una dependencia de autenticación Firebase.  
        /// </summary>
        /// <param name="firebaseAuthentication"></param>  
        /// <param name="firebaseRealtimeDatabase">Instancia de <see cref="IFirebaseRealtimeDatabase"/> para su registro en la Base de Datos.</param>  
        public Registro(IFirebaseAuthentication firebaseAuthentication, IFirebaseRealtimeDatabase firebaseRealtimeDatabase)
        {
            _firebaseAuthentication = firebaseAuthentication ?? throw new ArgumentNullException(nameof(firebaseAuthentication));
            _firebaseRealtimeDatabase = firebaseRealtimeDatabase ?? throw new ArgumentNullException(nameof(firebaseRealtimeDatabase));
        }

        /// <summary>  
        /// Guarda la información de un usuario en la base de datos en tiempo real de Firebase.  
        /// </summary>  
        /// <param name="usuario">Objeto <see cref="UsuarioDto"/> que contiene la información del usuario.</param>  
        /// <returns>Una tarea que representa la operación asincrónica de guardar el usuario.</returns>  
        public async Task GuardarUsuarioAsync(UsuarioDto usuario)
        {
            await _firebaseRealtimeDatabase.Instance
                .Child($"usuarios/{usuario.Id_Usuario}")
                .PutAsync(usuario);
        }

        /// <summary>  
        /// Registra un nuevo usuario en Firebase Authentication con el correo electrónico y la contraseña proporcionados.  
        /// </summary>  
        /// <param name="email">Correo electrónico del usuario.</param>  
        /// <param name="password">Contraseña del usuario.</param>  
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="apellidos">Apellidos del usuario.</param>
        /// <param name="direccion">Direccion de envio por defecto </param>
        /// <param name="telefono">Telefono del usuario </param>
        /// <returns>Una tarea que representa la operación asincrónica de registro.</returns>  
        public async Task RegistrarUsuarioAsync(string email, string password, string nombre, string apellidos, string direccion, string telefono)
        {
            var AuthToken = await _firebaseAuthentication.GetInstance().CreateUserWithEmailAndPasswordAsync(email, password);

            var usuario = new UsuarioDto
            {
                Id_Usuario = AuthToken.User.Uid,
                Email = email,
                FechaRegistro = DateTime.Now,
                Nombre = nombre,
                Apellidos = apellidos,
                Telefono = telefono,
                Direccion = direccion
            };

            await GuardarUsuarioAsync(usuario);

            Console.WriteLine($"Usuario registrado: {usuario.Nombre} con email: {usuario.Email}");
        }

    }
}
