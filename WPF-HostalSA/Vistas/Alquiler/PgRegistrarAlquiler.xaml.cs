using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgRegistrarAlquiler.xaml
    /// </summary>
    public partial class PgRegistrarAlquiler : Page
    {
        public PgRegistrarAlquiler()
        {
            InitializeComponent();
            lvDetalleAlquiler.DataContext = habitacionesSeleccionadas;
            CargarHabitacionesDisponibles();
            lvDetalleCliente.DataContext = clientesSeleccionados;
            CargarClientesDisponibles();
        }

        private void CargarHabitacionesDisponibles()
        {
            DataTable dtHabitaciones = CtrHabitacion.ObtenerHabitaciones();
            dtgListaHabitacion.ItemsSource = dtHabitaciones.DefaultView;
        }
        private void CargarClientesDisponibles()
        {
            DataTable dtClientes = CtrCliente.ObtenerClientes();
            dtgListaCliente.ItemsSource = dtClientes.DefaultView;
        }
        private ObservableCollection<Habitacion> habitacionesSeleccionadas = new ObservableCollection<Habitacion>();
        private ObservableCollection<Cliente> clientesSeleccionados = new ObservableCollection<Cliente>();
        private decimal costoTotal = 0;

        private void btnGuardarAlquiler_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (habitacionesSeleccionadas.Count == 0 || clientesSeleccionados.Count == 0)
                {
                    MessageBox.Show("Llene todos los campos para poder realizar el alquiler.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                foreach (Habitacion habitacion in habitacionesSeleccionadas)
                {
                    if (habitacion.EstadoH == "Ocupada")
                    {
                        MessageBox.Show($"La habitación {habitacion.NumeroH} está ocupada y no puede ser alquilada en su estado actual.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (habitacion.EstadoH == "Por Limpiar")
                    {
                        MessageBox.Show($"La habitación {habitacion.NumeroH} La habitación necesita limpieza y no puede ser alquilada en su estado actual.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }


                // Insertar alquiler en la base de datos
                int codReserva = 0;
                string estadoPago = cbEstPago.Text;

                foreach (Habitacion habitacion in habitacionesSeleccionadas)
                {
                    foreach (Cliente cliente in clientesSeleccionados)
                    {
                        string resultadoInsertar = CtrAlquiler.Insertar(codReserva, cliente.CodCliente, habitacion.CodHabitacion, estadoPago);
                        if (resultadoInsertar != "Información registrada con éxito")
                        {
                            MessageBox.Show($"Error al insertar el alquiler: {resultadoInsertar}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        // Actualizar el estado de la habitación a "OCUPADA"
                        CtrHabitacion.ActualizarEstadoHabitacion(habitacion.CodHabitacion, "Ocupada");
                    }
                }
                MessageBox.Show("Alquiler registrado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos(); // Limpia los campos después de guardar el alquiler
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el alquiler: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LimpiarCampos()
        {
            // Limpia los campos relevantes después de guardar el alquiler
            habitacionesSeleccionadas.Clear();
            clientesSeleccionados.Clear();
            tbCostoTotal.Clear();
        }

        private void CalcularCostoTotal()
        {
            costoTotal = habitacionesSeleccionadas.Sum(h => h.CostoH);
            tbCostoTotal.Text = costoTotal.ToString("C2");
        }
        private void BuscarHabitacionPorTipo(string tipoHabitacion)
        {
            DataTable dtHabitaciones = CtrHabitacion.ObtenerHabitacionesPorTipo(tipoHabitacion);

            if (dtHabitaciones != null)
            {
                dtgListaHabitacion.ItemsSource = dtHabitaciones.DefaultView;
            }
            else
            {
                MessageBox.Show("Error al buscar habitaciones por tipo.");
            }
        }
        private void btnBuscarHab_Click_1(object sender, RoutedEventArgs e)
        {
            string tipoHabitacion = tbBuscarHabitacion.Text.Trim();
            BuscarHabitacionPorTipo(tipoHabitacion);
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                // Navegar a la nueva página
                navigationService.Navigate(new PgAlquiler());
            }
        }

        private void btnLimpiarAlquiler_Click(object sender, RoutedEventArgs e)
        {


        }

        private void dtgListaHabitacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgListaHabitacion.SelectedItem is DataRowView selectedRow)
            {
                int codHabitacion = int.Parse(selectedRow["codHabitacion"].ToString());
                string numeroH = selectedRow["NumeroH"].ToString();
                decimal costoH = decimal.Parse(selectedRow["CostoH"].ToString());
                string tipoH = selectedRow["TipoH"].ToString();
                int numeroCamas = int.Parse(selectedRow["NumeroCamas"].ToString());
                string estadoH = selectedRow["EstadoH"].ToString();
                Habitacion habitacion = new Habitacion(codHabitacion, numeroH, costoH, tipoH, numeroCamas, estadoH);
                habitacionesSeleccionadas.Add(habitacion);
                // Actualizar el ListView de detalles de alquiler
                lvDetalleAlquiler.Items.Refresh();
                CalcularCostoTotal();
            }
        }

        private void dtgListaCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgListaCliente.SelectedItem is DataRowView selectedRow)
            {
                int codCliente = int.Parse(selectedRow["codCliente"].ToString());
                string nombreC = selectedRow["NombreC"].ToString();
                string apellidosC = selectedRow["ApellidosC"].ToString();
                string dniC = selectedRow["DNIC"].ToString();
                string telefonoC = selectedRow["TelefonoC"].ToString();
                string direccionC = selectedRow["DireccionC"].ToString();
                string correoC = selectedRow["CorreoC"].ToString();
                Cliente cliente = new Cliente(codCliente, nombreC, apellidosC, dniC, telefonoC, direccionC, correoC);
                clientesSeleccionados.Add(cliente);
                // Actualizar el ListView de detalles de alquiler
                lvDetalleCliente.Items.Refresh();
            }
        }
    }
}
