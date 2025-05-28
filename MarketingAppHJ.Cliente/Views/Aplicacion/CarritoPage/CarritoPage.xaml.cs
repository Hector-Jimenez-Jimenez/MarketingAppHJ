using System;
using System.Globalization;
using System.Linq;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.CarritoPage
{
    public partial class CarritoPage : ContentPage
    {
        readonly CarritoPageViewModel _vm;
        IDisposable _subscription;

        public CarritoPage(CarritoPageViewModel vm)
        {
            InitializeComponent();
            _vm = vm ?? throw new ArgumentNullException(nameof(vm));
            BindingContext = _vm;

            // Asignamos la fuente de datos de la CollectionView
            Collecion.ItemsSource = _vm.Items;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // 1) Carga inicial
            await _vm.CargarCarritoAsync();
            Total.Text = _vm.TotalPrice
                .ToString("C", CultureInfo.GetCultureInfo("es-ES"));

            // 2) Suscripción única a cambios en Firebase
            _subscription = _vm
                .ObservarCambios()
                .Subscribe(evt => {
                    MainThread.BeginInvokeOnMainThread(() => {
                        HandleCartEvent(evt);
                        Total.Text = _vm.TotalPrice
                            .ToString("C", CultureInfo.GetCultureInfo("es-ES"));
                    });
                });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _subscription?.Dispose();
        }

        void HandleCartEvent(FirebaseEvent<CarritoItemDto> evt)
        {
            var key = evt.Key;
            var dto = evt.Object;
            if (string.IsNullOrEmpty(key) || dto is null)
                return;

            dto.ProductoId = key;

            switch (evt.EventType)
            {
                case FirebaseEventType.InsertOrUpdate:
                    var exist = _vm.Items.FirstOrDefault(i => i.ProductoId == key);
                    if (exist != null)
                    {
                        var idx = _vm.Items.IndexOf(exist);
                        _vm.Items[idx] = dto;
                    }
                    else
                    {
                        _vm.Items.Add(dto);
                    }
                    break;

                case FirebaseEventType.Delete:
                    var toRm = _vm.Items.FirstOrDefault(i => i.ProductoId == key);
                    if (toRm != null)
                        _vm.Items.Remove(toRm);
                    break;
            }

            _vm.TotalPrice = _vm.Items.Sum(i => i.Total);
        }

        private async void OnIncrementClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is CarritoItemDto item)
                await _vm.IncrementarCantidadAsync(item);
        }

        private async void OnDecrementClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is CarritoItemDto item)
                await _vm.DecrementarCantidadAsync(item);
        }

        private async void OnRemoveClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is string id)
                await _vm.EliminarItemAsync(id);
        }

        private async void OnClearClicked(object sender, EventArgs e)
        {
            await _vm.VaciarCarritoAsync();

            // Firebase a veces no emite delete por cada child,
            // así que limpiamos manualmente la UI:
            _vm.Items.Clear();
            _vm.TotalPrice = 0;
            Total.Text = "€0,00";
        }
        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            // Navega a la página de checkout
            await Shell.Current.GoToAsync("checkout");
        }
    }
}
