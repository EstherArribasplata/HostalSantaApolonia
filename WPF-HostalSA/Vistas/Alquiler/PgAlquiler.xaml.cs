using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgAlquiler.xaml
    /// </summary>
    public partial class PgAlquiler : Page
    {
        private DataTable dtAlquiler;
        public PgAlquiler()
        {
            InitializeComponent();

            CargarDatos();
        }
        private void CargarDatos()
        {
            dtAlquiler = CtrAlquiler.ObtenerAlquileres();

            if (dtAlquiler != null && dtAlquiler.Rows.Count > 0)
            {
                nombreCol(dtAlquiler);
                dtgListarAlquiler.ItemsSource = dtAlquiler.DefaultView;
            }
            else
            {
                MessageBox.Show("No se encontraron alquileres en la base de datos.");
            }
        }

        private void nombreCol(DataTable dt)
        {
            dt.Columns[0].ColumnName = "ID Alquiler";
            dt.Columns[1].ColumnName = "Código de Reserva";
            dt.Columns[2].ColumnName = "Nombre de Cliente";
            dt.Columns[3].ColumnName = "Apellido de Cliente";
            dt.Columns[4].ColumnName = "Número de Habitación";
            dt.Columns[5].ColumnName = "Costo de Habitación";
            dt.Columns[6].ColumnName = "Tipo de Habitación";
            dt.Columns[7].ColumnName = "Número de Camas";
            dt.Columns[8].ColumnName = "Estado de Pago";
        }


        private void btnNuevoAlquiler_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgRegistrarAlquiler());
            }
        }

        private void btnModificarAlquiler_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgModificarAlquiler());
            }
        }

        private void btnEliminarAlquiler_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListarAlquiler.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dtgListarAlquiler.SelectedItem;
                int codAlquiler = Convert.ToInt32(row["ID Alquiler"]);

                bool eliminado = CtrAlquiler.EliminarAlquiler(codAlquiler);

                if (eliminado)
                {
                    MessageBox.Show("Alquiler eliminado exitosamente.");
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el alquiler.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un alquiler para eliminar.");
            }
        }
    }
}

