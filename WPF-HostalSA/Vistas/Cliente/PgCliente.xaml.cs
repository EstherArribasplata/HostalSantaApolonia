using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgCliente.xaml
    /// </summary>
    public partial class PgCliente : Page
    {
        private DataTable dtClientes;
        public PgCliente()
        {
            InitializeComponent();
            cargarDg();
        }

        private void btnModificarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (dtgListarClientes.SelectedItem is DataRowView selectedRow)
            {
                // Obtener el ID del cliente seleccionado
                int codCliente = int.Parse(selectedRow["ID Cliente"].ToString());

                // Navegar a la página de modificar cliente (PgModificarCliente) y pasar el ID del cliente como parámetro
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(new PgModificarCliente());
                }
            }
        }

        private void cargarDg()
        {
            dtClientes = CtrCliente.ObtenerClientes();
            nombreCol(dtClientes);
            this.dtgListarClientes.ItemsSource = dtClientes.DefaultView;
        }

        private void nombreCol(DataTable dt)
        {
            dt.Columns[0].ColumnName = "ID Cliente";
            dt.Columns[1].ColumnName = "Nombre";
            dt.Columns[2].ColumnName = "Apellidos";
            dt.Columns[3].ColumnName = "DNI";
            dt.Columns[4].ColumnName = "Teléfono";
            dt.Columns[5].ColumnName = "Dirección";
            dt.Columns[6].ColumnName = "Correo";
        }

        private void dtgListarClientes_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (dtgListarClientes.SelectedItem is DataRowView selectedRow)
            {
                // Obtener los datos de la fila seleccionada
                int codCliente = int.Parse(selectedRow["ID Cliente"].ToString());
                string NombreC = selectedRow["Nombre"].ToString();
                string ApellidoC = selectedRow["Apellidos"].ToString();
                string DNIC = selectedRow["DNI"].ToString();
                string TelefonoC = selectedRow["Teléfono"].ToString();
                string DireccionC = selectedRow["Dirección"].ToString();
                string CorreoC = selectedRow["Correo"].ToString();

                // Crear un objeto Cliente con los datos obtenidos
                Cliente selecCliente = new Cliente(codCliente, NombreC, ApellidoC, DNIC, TelefonoC, DireccionC, CorreoC);

                // Navegar a la página PgDetalleCliente y pasar el cliente seleccionado como parámetro
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(new PgModificarCliente(selecCliente));
                }
            }
        }

        private void btnNuevoCliente_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la página de agregar cliente (PgAgregarCliente)
                navigationService.Navigate(new PgAgregarCliente());
            }
        }
    }
}
