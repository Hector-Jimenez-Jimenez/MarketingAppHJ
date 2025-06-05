using Firebase.Database;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.IniciarSesion;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.IniciarSesion
{
    /// <summary>  
    /// Clase que implementa la funcionalidad para iniciar sesión utilizando Firebase Authentication.  
    /// </summary>  
    public class IniciarSesion : IIniciarSesion
    {
        private readonly IFirebaseAuthentication _firebaseAuthentication;

        /// <summary>  
        /// Inicializa una nueva instancia de la clase <see cref="IniciarSesion"/>.  
        /// </summary>  
        /// <param name="firebaseAuthentication">Instancia de autenticación de Firebase.</param>  
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="firebaseAuthentication"/> es nulo.</exception>  
        public IniciarSesion(IFirebaseAuthentication firebaseAuthentication)
        {
            _firebaseAuthentication = firebaseAuthentication ?? throw new ArgumentNullException(nameof(firebaseAuthentication));
        }

        /// <summary>  
        /// Inicia sesión con las credenciales proporcionadas.  
        /// </summary>  
        /// <param name="email">Correo electrónico del usuario.</param>  
        /// <param name="password">Contraseña del usuario.</param>  
        /// <returns>Una tarea que representa la operación asincrónica.</returns>  
        /// <exception cref="Exception">Se lanza si ocurre un error durante el inicio de sesión.</exception>  
        public async Task IniciarSesionAsync(string email, string password)
        {
            await _firebaseAuthentication.GetInstance()
                        .SignInWithEmailAndPasswordAsync(email, password);
        }
    }
}
