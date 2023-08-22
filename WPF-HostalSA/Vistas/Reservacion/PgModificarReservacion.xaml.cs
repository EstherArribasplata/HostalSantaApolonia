using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Vistas.Reservacion;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgModificarReservacion.xaml
    /// </summary>
    public partial class PgModificarReservacion : Page
    {
        public PgModificarReservacion()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgReservacion());
            }
        }

        private void btnLimpiarReserva_Click(object sender, RoutedEventArgs e)
        {
            tbBuscarHabitacion.Text = string.Empty;
            tbNombreCli.Text = string.Empty;
            tbApellidoCli.Text = string.Empty;
            tbDNICli.Text = string.Empty;
            dtpFechaIngreso.Text = string.Empty;
            tbCelularCli.Text = string.Empty;
            tbDirecCliente.Text = string.Empty;
            tbCorreoCli.Text = string.Empty;
            dtpFechaSalida.Text = string.Empty;
        }
    }
}
