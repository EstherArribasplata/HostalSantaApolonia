using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;

namespace WPF_HostalSA.Vistas.Reservacion
{
    /// <summary>
    /// Lógica de interacción para PgReservacion.xaml
    /// </summary>
    public partial class PgReservacion : Page
    {
        private DataTable dtReserva;
        public PgReservacion()
        {
            InitializeComponent();
            CargarDatos();
        }
        private void CargarDatos()
        {
            DataTable dtReserva = CtrReserva.ObtenerReservas();

            if (dtReserva != null && dtReserva.Rows.Count > 0)
            {
                nombreCol(dtReserva);
                dtgListarReservacion.ItemsSource = dtReserva.DefaultView;
            }
            else
            {
                MessageBox.Show("No se encontraron reservas en la base de datos.");
            }
        }

        private void nombreCol(DataTable dt)
        {
            dt.Columns[0].ColumnName = "ID Reserva";
            dt.Columns[1].ColumnName = "Nombre de Cliente";
            dt.Columns[2].ColumnName = "Apellido de Cliente";
            dt.Columns[3].ColumnName = "Tipo de Habitación";
            dt.Columns[4].ColumnName = "Número de Habitación";
            dt.Columns[5].ColumnName = "Costo de Habitación";
            dt.Columns[6].ColumnName = "Fecha de Entrada";
            dt.Columns[7].ColumnName = "Fecha de Salida";
        }

        private void btnNuevaRese_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgRegistrarReservacion());
            }
        }

        private void btnModificarRes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgModificarReservacion());
            }
        }

        private void btnEliminarReser_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListarReservacion.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dtgListarReservacion.SelectedItem;
                int codReserva = Convert.ToInt32(row["ID Reserva"]);

                bool eliminado = CtrReserva.EliminarReserva(codReserva);

                if (eliminado)
                {
                    MessageBox.Show("Reserva eliminada exitosamente.");
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el Reserva.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un Reserva para eliminar.");
            }
        }
    }
}
