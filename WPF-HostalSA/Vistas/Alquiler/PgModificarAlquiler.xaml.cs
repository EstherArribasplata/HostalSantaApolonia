using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgModificarAlquiler.xaml
    /// </summary>
    public partial class PgModificarAlquiler : Page
    {
        public PgModificarAlquiler()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgAlquiler());
            }
        }

        private void btnLimpiarAlquiler_Click(object sender, RoutedEventArgs e)
        {
            tbBuscarHabitacion.Text = string.Empty;
            tbNombreCli.Text = string.Empty;
            tbApellidoCli.Text = string.Empty;
            tbDNICli.Text = string.Empty;
            tbCelularCli.Text = string.Empty;
            tbDirecCliente.Text = string.Empty;
            tbCorreoCli.Text = string.Empty;
        }
    }
}

