using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgAgregarCliente.xaml
    /// </summary>
    public partial class PgAgregarCliente : Page
    {
        public PgAgregarCliente()
        {
            InitializeComponent();
        }

        private void btnAgrgarCli_Click(object sender, RoutedEventArgs e)
        {
            string msj = "";

            // Obtener los datos ingresados por el usuario desde los controles de la interfaz
            string nombreCliente = tbNombCli.Text.Trim();
            string apellidosCliente = tbApelliCli.Text.Trim();
            string dniCliente = tbDNICli.Text.Trim();
            string telefonoCliente = tbTeleCli.Text.Trim();
            string direccionCliente = tbDirCli.Text.Trim();
            string correoCliente = tbCorreo.Text.Trim();

            // Verificar si los campos están vacíos
            if (string.IsNullOrWhiteSpace(nombreCliente) ||
                string.IsNullOrWhiteSpace(apellidosCliente) ||
                string.IsNullOrWhiteSpace(dniCliente) ||
                string.IsNullOrWhiteSpace(telefonoCliente) ||
                string.IsNullOrWhiteSpace(direccionCliente) ||
                string.IsNullOrWhiteSpace(correoCliente))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de agregar un cliente.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Insertar el cliente en la base de datos utilizando el controlador CtrCliente
            msj = CtrCliente.Insertar(nombreCliente, apellidosCliente, dniCliente, telefonoCliente, direccionCliente, correoCliente);

            // Mostrar el mensaje de resultado de la inserción en un cuadro de diálogo
            MessageBox.Show(msj, "Seguridad");

            // Limpiar los TextBox después de la inserción
            tbNombCli.Clear();
            tbApelliCli.Clear();
            tbDNICli.Clear();
            tbTeleCli.Clear();
            tbDirCli.Clear();
            tbCorreo.Clear();
        }

        private void btnLimpiar_Click_1(object sender, RoutedEventArgs e)
        {
            tbNombCli.Text = string.Empty;
            tbApelliCli.Text = string.Empty;
            tbDNICli.Text = string.Empty;
            tbTeleCli.Text = string.Empty;
            tbDirCli.Text = string.Empty;
            tbCorreo.Text = string.Empty;
        }

        private void btnRegresar_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la página de lista de clientes (PgListaClientes)
                navigationService.Navigate(new PgCliente());
            }
        }
    }
}