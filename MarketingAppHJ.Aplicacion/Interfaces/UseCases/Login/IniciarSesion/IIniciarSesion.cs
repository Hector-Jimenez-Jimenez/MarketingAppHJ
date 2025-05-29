namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.IniciarSesion
{
    public interface IIniciarSesion
    {
        /// <summary>
        /// Inicia sesión con el correo electrónico y la contraseña proporcionados.
        /// </summary>
        /// <param name="email">El correo electrónico del usuario.</param>
        /// <param name="password">La contraseña del usuario.</param>
        /// <returns>Un token de autenticación si el inicio de sesión es exitoso; de lo contrario, un mensaje de error.</returns>
        Task IniciarSesionAsync(string email, string password);
    }
}
