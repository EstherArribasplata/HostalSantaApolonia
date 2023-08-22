using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgUsuarios.xaml
    /// </summary>
    public partial class PgUsuarios : Page
    {
        private DataTable dtUsuario;
        public ObservableCollection<Usuario> Usuarios { get; set; }
        public PgUsuarios()
        {
            InitializeComponent();
            cargarDg();
        }

        private void btnNuevoUsuario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                // Navegar a la página PgRegistrarUsuario
                navigationService.Navigate(new PgRegistrarUsuario());
            }
        }


        private void Main_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
        private void cargarDg()
        {
            dtUsuario = CtrUsuario.ObtenerDatosUsuario();
            nombreCol(dtUsuario);
            this.dtgListaUsuario.ItemsSource = dtUsuario.DefaultView;
        }
        private void nombreCol(DataTable dt)
        {
            dt.Columns[0].ColumnName = "ID Usuario";
            dt.Columns[1].ColumnName = "Nombre de Usuario";
            dt.Columns[2].ColumnName = "Contraseña de Usuario";
            dt.Columns[3].ColumnName = "Cargo de Usuario";
            dt.Columns[4].ColumnName = "DNI de Usuario";
            dt.Columns[5].ColumnName = "Celular de Usuario";
        }

        private void dtgListaUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgListaUsuario.SelectedItem is DataRowView selectedRow)
            {
                // Obtener los datos de la fila seleccionada
                int codUsuario = int.Parse(selectedRow["ID Usuario"].ToString());
                string nomUsuario = selectedRow["Nombre de Usuario"].ToString();
                string claveUsuario = selectedRow["Contraseña de Usuario"].ToString();
                int cargoUsuario = int.Parse(selectedRow["Cargo de Usuario"].ToString());
                string dniUsuario = selectedRow["DNI de Usuario"].ToString();
                string celularUsuario = selectedRow["Celular de Usuario"].ToString();

                // Crear un objeto Usuario con los datos obtenidos
                Usuario selectedUsuario = new Usuario(codUsuario, nomUsuario, claveUsuario, cargoUsuario, dniUsuario, celularUsuario);

                // Navegar a la página PgDetalleUsuario y pasar el usuario seleccionado como parámetro
                NavigationService navigationService = NavigationService.GetNavigationService(this);

                if (navigationService != null)
                {
                    navigationService.Navigate(new PgDetalleUsuario(selectedUsuario));
                }
            }
        }
    }
}
