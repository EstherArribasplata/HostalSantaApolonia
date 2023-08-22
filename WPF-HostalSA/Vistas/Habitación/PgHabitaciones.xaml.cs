using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgHabitaciones.xaml
    /// </summary>
    public partial class PgHabitaciones : Page
    {
        private DataTable dtHabitacion;
        public PgHabitaciones()
        {
            InitializeComponent();
            cargarDg();
        }

        private void btnNuevaHab_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgAgregarHabitaciones());
            }
        }

        private void btnModificarHab_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgModificarHabitaciones());
            }
        }
        private void cargarDg()
        {
            dtHabitacion = CtrHabitacion.ObtenerHabitaciones();
            nombreCol(dtHabitacion);
            this.dtgListarHabitacion.ItemsSource = dtHabitacion.DefaultView;
        }
        private void nombreCol(DataTable dt)
        {
            dt.Columns[0].ColumnName = "ID Habitación";
            dt.Columns[1].ColumnName = "Número de Habitación";
            dt.Columns[2].ColumnName = "Costo de Habitación";
            dt.Columns[3].ColumnName = "Tipo de Habitación";
            dt.Columns[4].ColumnName = "Número de camas";
            dt.Columns[5].ColumnName = "Estado de Habitación";
        }

        private void dtgListarHabitacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgListarHabitacion.SelectedItem is DataRowView selectedRow)
            {
                // Obtener los datos de la fila seleccionada
                int codHabitacion = int.Parse(selectedRow["ID Habitación"].ToString());
                string numeroH = selectedRow["Número de Habitación"].ToString();
                decimal costoH = decimal.Parse(selectedRow["Costo de Habitación"].ToString());
                string tipoH = selectedRow["Tipo de Habitación"].ToString();
                int numeroCamas = int.Parse(selectedRow["Número de Camas"].ToString());
                string estadoH = selectedRow["Estado de Habitación"].ToString();
                // Crear un objeto Habitacion con los datos obtenidos
                Habitacion selecHabitacion = new Habitacion(codHabitacion, numeroH, costoH, tipoH, numeroCamas, estadoH);

                // Navegar a la página PgDetalleHabitacion y pasar la habitación seleccionada como parámetro
                NavigationService navigationService = NavigationService.GetNavigationService(this);

                if (navigationService != null)
                {
                    navigationService.Navigate(new PgModificarHabitaciones(selecHabitacion));
                }
            }
        }
    }
}

