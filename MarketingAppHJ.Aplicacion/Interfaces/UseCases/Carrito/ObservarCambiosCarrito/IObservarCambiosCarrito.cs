using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;

namespace MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObservarCambiosCarrito
{
    /// <summary>
    /// Interfaz para observar los cambios en el carrito de un usuario.
    /// </summary>
    public interface IObservarCambiosCarrito
    {
        /// <summary>
        /// Observa los cambios en el carrito de un usuario específico.
        /// </summary>
        /// <param name="userId">El identificador único del usuario.</param>
        /// <returns>Un observable que emite eventos de Firebase relacionados con los cambios en el carrito.</returns>
        IObservable<FirebaseEvent<CarritoItemDto>> ObservarCambios(string userId);
    }
}
