using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.GuardarPedido;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ActualizarUsuario;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.GuardarFotoPerfil;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.IObtenerPerfilUsuario;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Usuarios.GuardarImagenPerfil
{
    /// <summary>
    /// Clase responsable de guardar la imagen de perfil de un usuario.
    /// </summary>
    public class GuardadoImagenPerfil : IGuardarImagenPefil
    {
        private readonly IActualizarUsuario _actualizarUsuario;
        private readonly IObtenerPerfilUsuario _obtenerPerfilUsuario;

        /// <summary>
        /// Constructor de la clase <see cref="GuardadoImagenPerfil"/>.
        /// </summary>
        /// <param name="actualizarUsuario">Servicio para actualizar la información del usuario.</param>
        /// <param name="obtenerPerfilUsuario">Servicio para obtener el perfil del usuario.</param>
        /// <exception cref="ArgumentNullException">Se lanza si alguno de los parámetros es nulo.</exception>
        public GuardadoImagenPerfil(IActualizarUsuario actualizarUsuario, IObtenerPerfilUsuario obtenerPerfilUsuario)
        {
            _actualizarUsuario = actualizarUsuario ?? throw new ArgumentNullException(nameof(actualizarUsuario));
            _obtenerPerfilUsuario = obtenerPerfilUsuario ?? throw new ArgumentNullException(nameof(obtenerPerfilUsuario));
        }

        /// <summary>
        /// Guarda la URL de la imagen de perfil de un usuario.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="ImagenPerfilUrl">URL de la imagen de perfil.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        public async Task GuardarImagenPerfil(string userId, string ImagenPerfilUrl)
        {
            var usuario = await _obtenerPerfilUsuario.ObtenerPerfilUsuarioAsync(userId);
            var dto = new UsuarioDto
            {
                Id_Usuario = userId,
                AvatarUrl = ImagenPerfilUrl,
                Apellidos = usuario.Apellidos,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                FechaRegistro = usuario.FechaRegistro,
                FechaNacimiento = usuario.FechaNacimiento,
                Telefono = usuario.Telefono,
                Direccion = usuario.Direccion,
                Username = usuario.Username
            };

            await _actualizarUsuario.ActualizarUsuarioAsync(dto);
        }
    }
}
