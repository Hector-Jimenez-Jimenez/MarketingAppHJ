namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ResetConstraseña
{
    /// <summary>  
    /// Interfaz para el caso de uso de restablecimiento de contraseña.  
    /// </summary>  
    public interface IResetContrasena
    {
        /// <summary>  
        /// Envía un correo electrónico para restablecer la contraseña del usuario.  
        /// </summary>  
        /// <param name="email">El correo electrónico del usuario.</param>  
        /// <returns>Una tarea que representa la operación asíncrona.</returns>  
        Task ResetearContrasenaAsync(string email);
    }
}
