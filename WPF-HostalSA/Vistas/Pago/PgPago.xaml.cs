using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WPF_HostalSA.Controladores;

namespace WPF_HostalSA.Vistas
{
    public partial class PgPago : Page
    {
        private DataTable dtPago;
        public PgPago()
        {

            InitializeComponent();
            CargarDatos();


        }

        private void CargarDatos()
        {
            try
            {
                // Obtener alquileres pendientes
                DataTable dtAlquileresPendientes = CtrAlquiler.ObtenerAlquileresPendientes();

                dgAlquileres.ItemsSource = dtAlquileresPendientes.DefaultView;

                // Obtener alquileres pagados
                DataTable dtAlquileresPagados = CtrAlquiler.ObtenerAlquileresPagados();

                dgAlquileres.ItemsSource = dtAlquileresPagados.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }


        private void btnAgregarPago_Click(object sender, RoutedEventArgs e)
        {
            if (dgAlquileres.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dgAlquileres.SelectedItem;
                int codAlquiler = Convert.ToInt32(selectedRow["codAlquiler"]);
                decimal precioTotal = Convert.ToDecimal(selectedRow["CostoH"]);
                string estadoPago = selectedRow["EstadoPago"].ToString();

                if (estadoPago.Equals("Pendiente"))
                {
                    string resultado = CtrPago.Insertar(codAlquiler, precioTotal);

                    if (resultado.Equals("Información registrada con éxito"))
                    {
                        MessageBox.Show("Pago registrado correctamente.");
                        CtrAlquiler.ActualizarEstadoPago(codAlquiler, "Pagado");
                        CargarDatos();
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el pago: " + resultado);
                    }
                }
                else
                {
                    MessageBox.Show("El alquiler ya está marcado como pagado.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un alquiler de la lista para registrar el pago.");
            }

        }
        private void dgAlquileres_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgAlquileres.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dgAlquileres.SelectedItem;
                int codAlquiler = Convert.ToInt32(selectedRow["codAlquiler"]);

                // Cambiar el estado de pago a "Pagado"
                if (CtrAlquiler.ActualizarEstadoPago(codAlquiler, "Pagado"))
                {
                    // Actualizar la vista del DataGrid
                    CargarDatos();
                    MessageBox.Show("Estado de pago actualizado a 'Pagado'.");
                }
                else
                {
                    MessageBox.Show("Error al actualizar el estado de pago.");
                }
            }
        }

        private void rbPendientes_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dtAlquileresPendientes = CtrAlquiler.ObtenerAlquileresPendientes();

                dgAlquileres.ItemsSource = dtAlquileresPendientes.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alquileres pendientes: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void rbPagados_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dtAlquileresPagados = CtrAlquiler.ObtenerAlquileresPagados();

                dgAlquileres.ItemsSource = dtAlquileresPagados.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alquileres pagados: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}