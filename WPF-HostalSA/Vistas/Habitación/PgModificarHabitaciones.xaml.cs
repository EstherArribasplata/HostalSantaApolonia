using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgModificarHabitaciones.xaml
    /// </summary>
    public partial class PgModificarHabitaciones : Page
    {
        private Habitacion habitacion;
        private Habitacion selecHabitacion;

        public PgModificarHabitaciones()
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


        public PgModificarHabitaciones(Habitacion selecHabitacion)
        {
            InitializeComponent();
            habitacion = selecHabitacion;
            CargarDatosHabitacion();
        }

        private void CargarDatosHabitacion()
        {
            tbNumHabi.Text = habitacion.NumeroH;
            tbCostoHab.Text = habitacion.CostoH.ToString();
            cbTipoHab.Text = habitacion.TipoH;
            cbNumCamas.Text = habitacion.NumeroCamas.ToString();
            cbEstadoHab.Text = habitacion.EstadoH;
        }

        

        private void btnActuHab_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los nuevos valores de la habitación desde los controles de texto
            string nuevoNumeroHabitacion = tbNumHabi.Text;
            decimal nuevoCostoHabitacion = decimal.Parse(tbCostoHab.Text);
            string nuevoTipoHabitacion = cbTipoHab.Text;
            int nuevoNumeroCamas = int.Parse(cbNumCamas.Text);
            string nuevoEstadoHabitacion = cbEstadoHab.Text;


            // Actualizar los valores de la habitación seleccionada con los nuevos valores
            habitacion.NumeroH = nuevoNumeroHabitacion;
            habitacion.CostoH = nuevoCostoHabitacion;
            habitacion.TipoH = nuevoTipoHabitacion;
            habitacion.NumeroCamas = nuevoNumeroCamas;
            habitacion.EstadoH = nuevoEstadoHabitacion;

            // Actualizar la habitación en la base de datos
            habitacion.ActualizarHabitacion();
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            // Navegar a la nueva página
            navigationService.Navigate(new PgHabitaciones());

        }
    }
}


