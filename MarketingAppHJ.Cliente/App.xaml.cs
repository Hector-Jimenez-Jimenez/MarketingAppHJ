using MarketingAppHJ.Cliente.Views.Login.LoginPage;

namespace MarketingAppHJ.Cliente
{
    /// <summary>
    /// Representa la clase principal de la aplicación.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="App"/>.
        /// </summary>
        /// <param name="shell">El contenedor principal de la aplicación.</param>
        public App(AppShell shell)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Crea una nueva ventana para la aplicación.
        /// </summary>
        /// <param name="activationState">El estado de activación de la aplicación.</param>
        /// <returns>Una nueva instancia de <see cref="Window"/>.</returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}