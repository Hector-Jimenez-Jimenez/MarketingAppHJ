using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.IObtenerPerfilUsuario
{
    /// <summary>  
    /// Interfaz para obtener el perfil de un usuario.  
    /// </summary>  
    public interface IObtenerPerfilUsuario
    {
        /// <summary>  
        /// Obtiene el perfil de un usuario por su identificador.  
        /// </summary>  
        /// <param name="usuarioId">El identificador único del usuario.</param>  
        /// <returns>Un objeto <see cref="UsuarioDto"/> que contiene la información del perfil del usuario.</returns>  
        Task<UsuarioDto> ObtenerPerfilUsuarioAsync(string usuarioId);
    }
}
