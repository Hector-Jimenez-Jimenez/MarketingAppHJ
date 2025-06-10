using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Cliente.Helper
{
    /// <summary>  
    /// Proporciona una extensión de marcado para traducir claves de recursos a cadenas localizadas.  
    /// </summary>  
    [ContentProperty(nameof(Key))]
    public class TranslateExtension : IMarkupExtension<BindingBase>
    {
        private const string ResourceId = "MarketingAppHJ.Cliente.Resources.Strings.Strings";
        private static readonly ResxLocalizationProvider LocalizationProvider = new();

        /// <summary>  
        /// Obtiene o establece la clave del recurso que se traducirá.  
        /// </summary>  
        public string Key { get; set; }

        /// <summary>  
        /// Proporciona el valor de la extensión de marcado.  
        /// </summary>  
        /// <param name="serviceProvider">Proveedor de servicios.</param>  
        /// <returns>Un objeto BindingBase configurado para la traducción.</returns>  
        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            if (Key == null) return null;

            // Usar la instancia estática
            return new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{Key}]",
                Source = LocalizationProvider // Cambiado aquí
            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
        }

        public static void RefreshTranslations() => LocalizationProvider.Refresh();
        /// <summary>  
        /// Proveedor de localización basado en recursos Resx.  
        /// </summary>  
        public class ResxLocalizationProvider : INotifyPropertyChanged
        {
            private static readonly ResourceManager ResourceManager = new(ResourceId, typeof(ResxLocalizationProvider).Assembly);

            /// <summary>  
            /// Obtiene el valor de cadena localizado para la clave especificada.  
            /// </summary>  
            /// <param name="key">Clave de recurso.</param>  
            /// <returns>Cadena localizada o una cadena vacía si no se encuentra.</returns>  
            public string this[string key] => ResourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? string.Empty;

            /// <summary>  
            /// Evento que se dispara cuando una propiedad cambia.  
            /// </summary>  
            public event PropertyChangedEventHandler PropertyChanged;



            public void Refresh()
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }
    }
}
