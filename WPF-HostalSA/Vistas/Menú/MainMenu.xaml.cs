using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using WPF_HostalSA.Vistas.Reservacion;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        [DllImport("user32.dll")]

        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            Main.Content = new PgUsuarios();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new PgHabitaciones();
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new PgAlquiler();
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            Main.Content = new PgPago();
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_5(object sender, RoutedEventArgs e)
        {
            Main.Content = new PgReportes();
        }

        private void RadioButton_Checked_6(object sender, RoutedEventArgs e)
        {
            Main.Content = new PgReservacion();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Muestra un cuadro de diálogo para confirmar el cierre de sesión
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas cerrar sesión?", "Confirmar Cierre de Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                this.Close();
            }
        }

        private void rbClientes_Checked(object sender, RoutedEventArgs e)
        {
            Main.Content = new PgCliente();
        }
    }
}