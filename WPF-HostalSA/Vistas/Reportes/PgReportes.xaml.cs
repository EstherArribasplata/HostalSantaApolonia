using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using WPF_HostalSA.Conexion;

namespace WPF_HostalSA.Vistas
{
    /// <summary>
    /// Lógica de interacción para PgReportes.xaml
    /// </summary>
    public partial class PgReportes : Page
    {
        public PgReportes()
        {
            InitializeComponent();
        }
        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime fechaInicio = dpFechaInicio.SelectedDate ?? DateTime.MinValue;
                DateTime fechaFin = dpFechaFin.SelectedDate ?? DateTime.MaxValue;

                DataTable dt = GetReservasPorFechas(fechaInicio, fechaFin);

                dgReporteReservas.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DataTable GetReservasPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(ConexionDB.cn))
            {
                con.Open();

                string query = @"SELECT
                                    R.codReserva,
                                    C.NombreC,
                                    C.ApellidosC,
                                    H.TipoH,
                                    H.NumeroH,
                                    H.CostoH,
                                    R.FechaEntrada,
                                    R.FechaSalida
                                FROM
                                    Reserva R
                                JOIN
                                    Cliente C ON R.codCliente = C.codCliente
                                JOIN
                                    Habitacion H ON R.codHabitacion = H.codHabitacion
                                WHERE
                                    R.FechaEntrada >= @FechaInicio AND R.FechaSalida <= @FechaFin;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}
