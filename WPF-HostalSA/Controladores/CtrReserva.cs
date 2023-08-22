using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using WPF_HostalSA.Conexion;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Controladores
{
    public class CtrReserva
    {
        public static string Insertar(DateTime fechaIngreso, DateTime fechaSalida, int codAlquiler, int codHabitacion, int codCliente)
        {
            Reserva reserva = new Reserva();
            reserva.FechaEntrada = fechaIngreso;
            reserva.FechaSalida = fechaSalida;
            reserva.CodAlquiler = codAlquiler;
            reserva.CodHabitacion = codHabitacion;
            reserva.CodCliente = codCliente;
            return reserva.Insertar(reserva);
        }

        public static DataTable ObtenerReservas()
        {
            return new Reserva().ObtenerTodasLasReservas();
        }

        public static bool EliminarReserva(int codReserva)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    con.Open();
                    string query = "DELETE FROM Reserva WHERE codReserva = @CodReserva";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CodReserva", codReserva);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Retorna true si se eliminó al menos una fila
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el reserva: " + ex.Message);
                return false;
            }
        }
        public static DataTable ObtenerReservasPorTipo(string tipo)
        {
            DataTable dtReservas = new DataTable("Reserva");
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    con.Open();
                    string query = @"
                        SELECT R.codReserva, C.NombreC ,C.ApellidosC ,H.TipoH , H.NumeroH ,H.CostoH ,R.FechaEntrada ,R.FechaSalida 
                        FROM Reserva R
                        JOIN Cliente C ON R.codCliente = C.codCliente
                        JOIN Habitacion H ON R.codHabitacion = H.codHabitacion
                        WHERE H.TipoH = @Tipo";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Tipo", tipo);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtReservas);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al obtener las reservas por tipo: " + ex.Message);
                dtReservas = null;
            }
            return dtReservas;
        }
    }
}
