using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database.Streaming;
using Firebase.Database;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.ObtenerTodosProductos;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.ObtenerProductos;
using static Java.Util.Concurrent.Flow;

namespace MarketingAppHJ.Cliente.ViewModels.MainPageViewModel
{
    /// <summary>
    /// ViewModel para gestionar el catálogo de productos.
    /// </summary>
    public partial class MainPageViewModel : ObservableObject, IDisposable
    {
        private readonly FirebaseClient _firebaseClient = new("https://marketingapphj-default-rtdb.firebaseio.com/");
        private IDisposable _subscription;
        private readonly IObtenerProductos _usecase;
        public ObservableCollection<ProductoDto> Productos { get; } = new();
        [ObservableProperty] bool isBusy;
        public void Dispose()
        {
            _subscription?.Dispose();
            _firebaseClient.Dispose();
        }
        /// <summary>
        /// Constructor de la clase MainPageViewModel.
        /// </summary>
        /// <param name="usecase">Caso de uso para obtener productos.</param>
        public MainPageViewModel(IObtenerProductos usecase,FirebaseClient firebase)
        {
            _usecase = usecase;
            _firebaseClient = firebase;
            SubscribeToRealtimeUpdates();
        }

        public MainPageViewModel() { }

        [RelayCommand]
        public async Task CargarProductosAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            Productos.Clear();
            var list = await _usecase.ObtenerProductosAsync();
            foreach (var p in list) Productos.Add(p);
            IsBusy = false;
        }

        private void SubscribeToRealtimeUpdates()
        {
            _subscription = _firebaseClient
                .Child("productos")
                .AsObservable<ProductoDto>()
                .Subscribe(firebaseObject =>
                {
                    var key = firebaseObject.Key;
                    var dto = firebaseObject.Object;
                    dto.Id = key;

                    switch (firebaseObject.EventType)
                    {
                        case FirebaseEventType.InsertOrUpdate:
                            // Si ya existe, actualiza; si no, añade
                            var existing = Productos.FirstOrDefault(p => p.Id == key);
                            if (existing != null)
                            {
                                var idx = Productos.IndexOf(existing);
                                Productos[idx] = dto;
                            }
                            else
                            {
                                Productos.Add(dto);
                            }
                            break;

                        case FirebaseEventType.Delete:
                            // Si se borra, quita de la colección
                            var toRemove = Productos.FirstOrDefault(p => p.Id == key);
                            if (toRemove != null)
                                Productos.Remove(toRemove);
                            break;
                    }
                });
        }
    }
}
