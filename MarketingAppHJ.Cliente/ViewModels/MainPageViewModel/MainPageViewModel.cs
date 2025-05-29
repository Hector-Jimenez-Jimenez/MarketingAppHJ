using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database.Streaming;
using Firebase.Database;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerTodosProductos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.RealtimeDatabase;

namespace MarketingAppHJ.Cliente.ViewModels.MainPageViewModel
{
    /// <summary>
    /// ViewModel para gestionar el catálogo de productos.
    /// </summary>
    public partial class MainPageViewModel : ObservableObject, IDisposable
    {
        #region Paginacion
        private const int PageSize = 10;
        private int _currentPage = 0;
        #endregion

        #region Interfaces y Firbase
        private readonly IFirebaseRealtimeDatabase _firebaseClient;
        private IDisposable _subscription;
        private readonly IObtenerProductos _usecase;
        private readonly List<ProductoDto> _allProductos = new ();
        #endregion

        #region observablePropieties
        public ObservableCollection<ProductoDto> Productos { get; } = new();

        [ObservableProperty] bool isBusy;
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor de la clase MainPageViewModel.
        /// </summary>
        /// <param name="usecase">Caso de uso para obtener productos.</param>
        public MainPageViewModel(IObtenerProductos usecase,IFirebaseRealtimeDatabase firebase)
        {
            _usecase = usecase;
            _firebaseClient = firebase;
            CargarProductosAsync();
        }

        /// <summary>
        /// Constructor por defecto para MainPageViewModel.
        /// </summary>
        public MainPageViewModel() { }
        #endregion

        #region Metodos y Comandos
        /// <summary>
        /// Libera los recursos utilizados por el ViewModel.
        /// </summary>
        public void Dispose()
        {
            _subscription?.Dispose();
            _firebaseClient.Instance.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Carga los productos desde el caso de uso y los agrega a la colección observable.
        /// </summary>
        [RelayCommand]
        public async Task CargarProductosAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            var list = await _usecase.ObtenerProductosAsync();
            _allProductos.Clear();
            _allProductos.AddRange(list);
            foreach (var p in list) Productos.Add(p);
            IsBusy = false;

            SubscribeToRealtimeUpdates();
        }

        /// <summary>
        /// Carga la siguiente página de productos en la colección observable.
        /// </summary>
        public void LoadNextPage()
        {
            var inicio = _currentPage * PageSize;
            if (inicio >= _allProductos.Count) return;

            var productosPagina = _allProductos.Skip(inicio).Take(PageSize).ToList();
            foreach (var producto in productosPagina)
            {
                if (!Productos.Any(p => p.Id == producto.Id))
                {
                    Productos.Add(producto);
                }
            }
            _currentPage++;
        }

        /// <summary>
        /// Suscribe a actualizaciones en tiempo real de los productos desde Firebase.
        /// </summary>
        private void SubscribeToRealtimeUpdates()
        {
            _subscription = _firebaseClient.Instance
                .Child("productos")
                .AsObservable<ProductoDto>()
                .Subscribe(e =>
                {
                    var key = e.Key;
                    var dto = e.Object;
                    dto.Id = key;
                    lock (_allProductos)
                    {
                        var existing = _allProductos.FirstOrDefault(p => p.Id == key);
                        if(e.EventType == FirebaseEventType.Delete)
                        {
                            if(existing != null) _allProductos.Remove(existing);
                            var borrados = Productos.FirstOrDefault(p => p.Id == key);
                            if (borrados != null) Productos.Remove(borrados);
                        }
                        else if (e.EventType == FirebaseEventType.InsertOrUpdate)
                        {
                            if (existing != null)
                            {
                                existing.Nombre = dto.Nombre;
                                existing.Descripcion = dto.Descripcion;
                                existing.ImagenUrl = dto.ImagenUrl;
                                existing.Stock = dto.Stock;
                                existing.CategoriaId = dto.CategoriaId;
                                existing.Precio = dto.Precio;
                            }
                            else
                            {
                                _allProductos.Add(dto);
                            }
                        }
                    }
                }
            );
        }
        #endregion
    }
}
