using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Vistas
{
    public partial class PgModificarCliente : Page
    {
        private Cliente cliente;

        public PgModificarCliente()
        {
            InitializeComponent();
        }

        public PgModificarCliente(Cliente selecCliente)
        {
            InitializeComponent();
            cliente = selecCliente;
            CargarDatosCliente();
        }

        private void CargarDatosCliente()
        {
            tbNombCli.Text = cliente.NombreC;
            tbApelliCli.Text = cliente.ApellidosC;
            tbDNICli.Text = cliente.DniC;
            tbTeleCli.Text = cliente.TelefonoC;
            tbDirCli.Text = cliente.DireccionC;
            tbCorreo.Text = cliente.CorreoC;
        }

        private void LimpiarCampos()
        {
            tbNombCli.Clear();
            tbApelliCli.Clear();
            tbDNICli.Clear();
            tbTeleCli.Clear();
            tbDirCli.Clear();
            tbCorreo.Clear();
        }

        private void btnActualizarCli_Click(object sender, RoutedEventArgs e)
        {
            string nuevoNombre = tbNombCli.Text.Trim();
            string nuevoApellidos = tbApelliCli.Text.Trim();
            string nuevoDNI = tbDNICli.Text.Trim();
            string nuevoTelefono = tbTeleCli.Text.Trim();
            string nuevaDireccion = tbDirCli.Text.Trim();
            string nuevoCorreo = tbCorreo.Text.Trim();

            cliente.NombreC = nuevoNombre;
            cliente.ApellidosC = nuevoApellidos;
            cliente.DniC = nuevoDNI;
            cliente.TelefonoC = nuevoTelefono;
            cliente.DireccionC = nuevaDireccion;
            cliente.CorreoC = nuevoCorreo;

            cliente.ActualizarCliente();

            MessageBox.Show("Cliente actualizado correctamente.");

            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new PgCliente());
        }

        private void btnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (cliente != null && cliente.CodCliente > 0)
                {
                    if (cliente.EliminarCliente())
                    {
                        MessageBox.Show("Cliente eliminado correctamente.");
                    }
                }
                else
                {
                    MessageBox.Show("Error al eliminar el cliente.");
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado un cliente válido para eliminar.");
            }

            LimpiarCampos();
        }

        private void btnRegresar_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.Navigate(new PgCliente());
            }
        }
    }
}