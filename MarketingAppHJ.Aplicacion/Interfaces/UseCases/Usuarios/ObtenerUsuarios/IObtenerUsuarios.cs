using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ObtenerUsuarios
{
    public interface IObtenerUsuarios
    {
        /// <summary>
        /// Obtiene una lista de usuarios de forma asíncrona.
        /// </summary>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado contiene una colección de objetos <see cref="UsuarioDto"/>.</returns>
        Task<IEnumerable<UsuarioDto>> ObtenerUsuariosAsync();
    }
}
