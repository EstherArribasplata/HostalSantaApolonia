using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_HostalSA.Controladores;
using WPF_HostalSA.Modelos;
using WPF_HostalSA.Vistas.Reservacion;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgRegistrarReservacion.xaml
    /// </summary>
    public partial class PgRegistrarReservacion : Page
    {
        private DateTime fechaEntradaSeleccionada;
        private DateTime fechaSalidaSeleccionada;

        public PgRegistrarReservacion()
        {
            InitializeComponent();
            lvDetalleAlquiler.DataContext = habitacionesSeleccionadas;
            CargarHabitacionesDisponibles();
            lvDetalleCliente.DataContext = clientesSeleccionados;
            CargarClientesDisponibles();
        }

        private ObservableCollection<Habitacion> habitacionesSeleccionadas = new ObservableCollection<Habitacion>();
        private ObservableCollection<Cliente> clientesSeleccionados = new ObservableCollection<Cliente>();
        private decimal costoTotal = 0;
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
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);

            if (navigationService != null)
            {
                navigationService.Navigate(new PgReservacion());
            }
        }

        private void btnLimpiarReserva_Click(object sender, RoutedEventArgs e)
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
        private void CalcularCostoTotal()
        {
            costoTotal = habitacionesSeleccionadas.Sum(h => h.CostoH);
            tbCostoTotal.Text = costoTotal.ToString("C2");
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
        private void DatePickerFechaIngreso_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            fechaEntradaSeleccionada = dpFechaIngreso.SelectedDate ?? DateTime.MinValue;
            ActualizarListViewReserva();
        }
        private void DatePickerFechaSalida_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            fechaSalidaSeleccionada = dpFechaSalida.SelectedDate ?? DateTime.MinValue;
            ActualizarListViewReserva();
        }
        private void ActualizarListViewReserva()
        {
            lvDetalleReserva.Items.Clear();

            if (fechaEntradaSeleccionada != DateTime.MinValue && fechaSalidaSeleccionada != DateTime.MinValue)
            {
                lvDetalleReserva.Items.Add(new { FechaEntrada = fechaEntradaSeleccionada, FechaSalida = fechaSalidaSeleccionada });
            }
        }

        private void btnGuardarReserva_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvDetalleCliente.Items.Count == 0 || lvDetalleAlquiler.Items.Count == 0 || dpFechaIngreso.SelectedDate == null || dpFechaSalida.SelectedDate == null)
                {
                    MessageBox.Show("Complete los campos requeridos antes de agregar la reservación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime fechaIngreso = dpFechaIngreso.SelectedDate.Value;
                DateTime fechaSalida = dpFechaSalida.SelectedDate.Value;

                foreach (Cliente cliente in lvDetalleCliente.Items)
                {
                    foreach (Habitacion habitacion in lvDetalleAlquiler.Items)
                    {
                        string resultadoInsertar = CtrReserva.Insertar(fechaIngreso, fechaSalida, 0, habitacion.CodHabitacion, cliente.CodCliente);
                        if (resultadoInsertar != "Información registrada con éxito")
                        {
                            MessageBox.Show($"Error al insertar la reservación: {resultadoInsertar}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }

                MessageBox.Show("Reservación agregada exitosamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LimpiarCampos()
        {
            habitacionesSeleccionadas.Clear();
            clientesSeleccionados.Clear();
            tbCostoTotal.Clear();
        }
    }
}

