using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerTodosProductos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using Microsoft.Maui.Dispatching;

namespace MarketingAppHJ.Cliente.ViewModels.MainPageViewModel
{
    /// <summary>
    /// ViewModel principal para la página principal de la aplicación.
    /// </summary>
    public partial class MainPageViewModel : ObservableObject, IDisposable
    {
        #region Intefaces
        readonly IFirebaseRealtimeDatabase _firebaseClient;
        readonly IObtenerProductos _usecase;
        #endregion

        #region Variables
        IDisposable? _subscription = null;
        const int PageSize = 10;
        int _currentPage = 0;
        List<ProductoDto> _allProductos = new();

        [ObservableProperty]
        ObservableCollection<ProductoDto> productos = new();

        [ObservableProperty]
        string textoBusqueda = string.Empty;

        [ObservableProperty]
        bool isBusy;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de la clase MainPageViewModel.
        /// </summary>
        /// <param name="usecase">Caso de uso para obtener productos.</param>
        /// <param name="firebase">Cliente de Firebase Realtime Database.</param>
        public MainPageViewModel(IObtenerProductos usecase, IFirebaseRealtimeDatabase firebase)
        {
            _usecase = usecase;
            _firebaseClient = firebase;
            LoadInitialData();
        }
        #endregion

        #region Métodos
        async void LoadInitialData()
        {
            await CargarProductosAsync();
        }

        /// <summary>
        /// Carga los productos de forma asíncrona desde el caso de uso y los actualiza en la vista.
        /// </summary>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        [RelayCommand]
        public async Task CargarProductosAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            var lista = await _usecase.ObtenerProductosAsync();
            _allProductos = lista.ToList();

            _currentPage = 0;
            Productos = new ObservableCollection<ProductoDto>();
            TextoBusqueda = string.Empty;
            LoadNextPage();

            IsBusy = false;

            SubscribeToRealtimeUpdates();
        }

        partial void OnTextoBusquedaChanged(string value)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _currentPage = 0;
                FiltrarYActualizarProductos();
            });
        }

        void FiltrarYActualizarProductos()
        {
            if (string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                Productos.Clear();
                _currentPage = 0;
                LoadNextPage();
                return;
            }

            var filtro = TextoBusqueda.Trim().ToLowerInvariant();
            var filtrados = _allProductos
                .Where(p => !string.IsNullOrEmpty(p.Nombre)
                            && p.Nombre.Trim().ToLowerInvariant().StartsWith(filtro))
                .ToList();

            Productos = new ObservableCollection<ProductoDto>(filtrados);
        }

        /// <summary>
        /// Carga la siguiente página de productos en la colección observable.
        /// </summary>
        public void LoadNextPage()
        {
            if (!string.IsNullOrWhiteSpace(TextoBusqueda))
                return;

            var inicio = _currentPage * PageSize;
            if (inicio >= _allProductos.Count) return;

            var pagina = _allProductos
                .Skip(inicio)
                .Take(PageSize)
                .ToList();

            foreach (var prod in pagina)
            {
                if (!Productos.Any(p => p.Id == prod.Id))
                    Productos.Add(prod);
            }

            _currentPage++;
        }

        void SubscribeToRealtimeUpdates()
        {
            _subscription?.Dispose();

            _subscription = _firebaseClient.Instance
                .Child("productos")
                .AsObservable<ProductoDto>()
                .Subscribe(evt =>
                {
                    var key = evt.Key;
                    var dto = evt.Object;
                    if (string.IsNullOrEmpty(key) || dto is null)
                        return;

                    dto.Id = key;

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        lock (_allProductos)
                        {
                            var existente = _allProductos.FirstOrDefault(p => p.Id == key);

                            if (evt.EventType == FirebaseEventType.Delete)
                            {
                                if (existente != null)
                                    _allProductos.Remove(existente);

                                var enVista = Productos.FirstOrDefault(p => p.Id == key);
                                if (enVista != null)
                                    Productos.Remove(enVista);
                            }
                            else
                            {
                                if (existente != null)
                                {
                                    existente.Nombre = dto.Nombre;
                                    existente.Descripcion = dto.Descripcion;
                                    existente.ImagenUrl = dto.ImagenUrl;
                                    existente.Stock = dto.Stock;
                                    existente.CategoriaId = dto.CategoriaId;
                                    existente.Precio = dto.Precio;
                                }
                                else
                                {
                                    _allProductos.Add(dto);
                                }
                            }
                        }

                        FiltrarYActualizarProductos();
                    });
                });
        }

        /// <summary>
        /// Libera los recursos utilizados por la instancia de MainPageViewModel.
        /// </summary>
        public void Dispose()
        {
            _subscription?.Dispose();
            _firebaseClient.Instance.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
