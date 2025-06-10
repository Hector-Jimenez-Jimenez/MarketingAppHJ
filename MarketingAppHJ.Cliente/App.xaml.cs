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
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Crea la ventana principal de la aplicación.
        /// </summary>
        /// <returns>La ventana principal.</returns>
        protected override Window CreateWindow(IActivationState activationState)
        {
            return new Window(new AppShell());
        }
    }
}
