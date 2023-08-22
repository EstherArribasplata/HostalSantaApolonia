using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgRegistrarUsuario.xaml
    /// </summary>
    public partial class PgRegistrarUsuario : Page
    {
        private int selectedCargo;
        public PgRegistrarUsuario()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgUsuarios());
            }
        }
        private void cbCargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCargo.SelectedItem != null)
            {
                // Obtener el ítem seleccionado del ComboBox
                ComboBoxItem selectedRoleItem = cbCargo.SelectedItem as ComboBoxItem;

                // Asignar el valor correspondiente a la variable int
                switch (selectedRoleItem.Content.ToString())
                {
                    case "Administrador":
                        selectedCargo = 1;
                        break;
                    case "Recepcionista":
                        selectedCargo = 2;
                        break;
                    case "Personal de Limpieza":
                        selectedCargo = 3;
                        break;
                    default:
                        selectedCargo = 0;
                        break;
                }
            }
        }

        private void btnGuardarUsuario_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si los campos requeridos están vacíos
            if (string.IsNullOrWhiteSpace(tbNombres.Text) ||
                string.IsNullOrWhiteSpace(tbContrasena.Text) ||
                string.IsNullOrWhiteSpace(tbDNI.Text) ||
                string.IsNullOrWhiteSpace(tbCelular.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string msj = "";
            msj = CtrUsuario.Insertar(tbNombres.Text.Trim(), tbContrasena.Text.Trim(), selectedCargo, tbDNI.Text.Trim(), tbCelular.Text.Trim());
            MessageBox.Show(msj, "Seguridad");
            tbNombres.Clear();
            tbContrasena.Clear();
            cbCargo.Items.Clear();
            tbDNI.Clear();
            tbCelular.Clear();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            tbNombres.Text = string.Empty;
            tbContrasena.Text = string.Empty;
            tbDNI.Text = string.Empty;
            cbCargo.Text = string.Empty;
            tbCelular.Text = string.Empty;
        }
    }
}
