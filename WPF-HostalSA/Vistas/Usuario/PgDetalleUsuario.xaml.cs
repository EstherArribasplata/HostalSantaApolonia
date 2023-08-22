using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgDetalleUsuario.xaml
    /// </summary>
    public partial class PgDetalleUsuario : Page
    {
        private Usuario usuario;
        private Usuario selectedUsuario;

        public PgDetalleUsuario()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgUsuarios());
            }
        }

        public PgDetalleUsuario(Usuario selectedUsuario)
        {
            InitializeComponent();
            usuario = selectedUsuario;
            CargarDatosUsuario();
        }

        private void CargarDatosUsuario()
        {
            tbNombres.Text = usuario.NomUsuario.ToString();
            tbContrasena.Text = usuario.ClaveUsuario;
            tbDNI.Text = usuario.DniUsuario;
            tbCelular.Text = usuario.CelularUsuario;
        }

        private void btnModificarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            // Obtener los nuevos valores del usuario desde los controles de texto
            string nuevoNombreUsuario = tbNombres.Text;
            string nuevaClaveUsuario = tbContrasena.Text;
            string nuevoDniUsuario = tbDNI.Text;
            string nuevoCelularUsuario = tbCelular.Text;

            // Actualizar los valores del objeto usuario con los nuevos valores
            usuario.NomUsuario = nuevoNombreUsuario;
            usuario.ClaveUsuario = nuevaClaveUsuario;
            usuario.DniUsuario = nuevoDniUsuario;
            usuario.CelularUsuario = nuevoCelularUsuario;

            // Actualizar el usuario en la base de datos
            usuario.ActualizarUsuario();

            NavigationService navigationService = NavigationService.GetNavigationService(this);
            // Navegar a la nueva página
            navigationService.Navigate(new PgUsuarios());

        }

        private void btnEliminarUsuario_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Eliminar el usuario seleccionado de la base de datos
                if (usuario != null && usuario.CodUsuario > 0)
                {
                    if (usuario.EliminarUsuario())
                    {
                        MessageBox.Show("Usuario eliminado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el usuario.");
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado un usuario válido para eliminar.");
                }
            }
            tbNombres.Text = string.Empty;
            tbContrasena.Text = string.Empty;
            tbDNI.Text = string.Empty;
            cbCargo.Text = string.Empty;
            tbCelular.Text = string.Empty;
        }
    }
}
