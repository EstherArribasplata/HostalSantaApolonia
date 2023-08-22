using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_HostalSA.Controladores;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public string cargo;
        private MainMenu frame;

        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            Window principal = new MainMenu();
            Page habitaciones = new PgHabitaciones();
            DataTable user = new DataTable();
            user = CtrUsuario.ObtenerUsuario(txtUsuario.Text, txtContrasena.Password.ToString(), 2);
            if (user.Rows.Count > 0)
            {
                this.Hide();
                int cargoCode = int.Parse(user.Rows[0][1].ToString());

                switch (cargoCode)
                {
                    case 1: // Administrador
                        cargo = "Administrador";
                        break;

                    case 2: // Recepcionista
                        cargo = "Recepcionista";
                        ((MainMenu)principal).rbUsuarios.IsEnabled = false;
                        ((MainMenu)principal).rbReportes.IsEnabled = false;
                        break;

                    case 3: // Personal de Limpieza
                        cargo = "Personal de Limpieza";
                        ((MainMenu)principal).rbReservaciones.IsEnabled = false;
                        ((MainMenu)principal).rbAlquileres.IsEnabled = false;
                        ((PgHabitaciones)habitaciones).btnNuevaHab.IsEnabled = false;
                        ((MainMenu)principal).rbPagos.IsEnabled = false;
                        ((MainMenu)principal).rbUsuarios.IsEnabled = false;
                        ((MainMenu)principal).rbReportes.IsEnabled = false;
                        ((MainMenu)principal).rbClientes.IsEnabled = false;
                        break;
                    default:
                        cargo = "Desconocido";
                        break;
                }
                principal.Show();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o Contraseña incorrecta", "Ingrese nuevamente", MessageBoxButton.OK);
            }
        }
        public void iniciarFrame(MainMenu frame)
        {
            this.frame = frame;
        }
    }
}
