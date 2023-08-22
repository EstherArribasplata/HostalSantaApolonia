using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgAgregarHabitaciones.xaml
    /// </summary>
    public partial class PgAgregarHabitaciones : Page
    {
        public PgAgregarHabitaciones()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgHabitaciones());
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            tbNumHabi.Text = string.Empty;
            tbCostoHab.Text = string.Empty;
            cbTipoHab.Text = string.Empty;
            cbNumCamas.Text = string.Empty;
            cbEstadoHab.Text = string.Empty;
        }

        private void btnAgrgarHab_Click(object sender, RoutedEventArgs e)
        {
            string msj = "";

            // Obtener los datos ingresados por el usuario desde los controles de la interfaz
            string numeroHabitacion = tbNumHabi.Text.Trim();
            decimal costoHabitacion;

            if (!decimal.TryParse(tbCostoHab.Text.Trim(), out costoHabitacion))
            {
                MessageBox.Show("Llene los campos solicitados.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string tipoHabitacion = cbTipoHab.Text.Trim();
            int numeroCamas;

            if (!int.TryParse(cbNumCamas.Text.Trim(), out numeroCamas))
            {
                MessageBox.Show("El número de camas debe ser un valor numérico válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string estadoHabitacion = cbEstadoHab.Text.Trim();

            // Validar que no haya campos vacíos
            if (string.IsNullOrEmpty(numeroHabitacion) || string.IsNullOrEmpty(tipoHabitacion) || string.IsNullOrEmpty(estadoHabitacion))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de agregar la habitación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Insertar la habitación en la base de datos utilizando el controlador CtrHabitacion
            msj = CtrHabitacion.Insertar(numeroHabitacion, costoHabitacion, tipoHabitacion, numeroCamas, estadoHabitacion);
            // Mostrar el mensaje de resultado de la inserción en un cuadro de diálogo
            MessageBox.Show(msj, "Seguridad");

            // Limpiar los TextBox después de la inserción
            tbNumHabi.Clear();
            tbCostoHab.Clear();
            cbTipoHab.Text = string.Empty;
            cbNumCamas.Text = string.Empty;
            cbEstadoHab.Text = string.Empty;
        }
    }
}

