using Firebase.Database;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObservarCambiosCarrito;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.ObservarCambiosCarrito
{
    /// <summary>
    /// Clase que implementa la observación de cambios en el carrito de un usuario.
    /// </summary>
    public class ObservarCambiosCarrito : IObservarCambiosCarrito
    {
        private readonly FirebaseClient _client;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ObservarCambiosCarrito"/>.
        /// </summary>
        /// <param name="client">Cliente de Firebase utilizado para interactuar con la base de datos.</param>
        public ObservarCambiosCarrito(FirebaseClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Observa los cambios en los elementos del carrito de un usuario específico.
        /// </summary>
        /// <param name="userId">El identificador del usuario cuyo carrito se va a observar.</param>
        /// <returns>Un observable que emite eventos de Firebase relacionados con los elementos del carrito.</returns>
        public IObservable<FirebaseEvent<CarritoItemDto>> ObservarCambios(string userId)
        {
            return _client
                        .Child($"carritos/{userId}/items")
                        .AsObservable<CarritoItemDto>();
        }
    }
}
