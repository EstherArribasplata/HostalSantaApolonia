using System.Data;
using System.Data.SqlClient;
using WPF_HostalSA.Conexion;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Controladores
{

    public class CtrHabitacion
    {
        public static string Insertar(string numeroH, decimal costoH, string tipoH, int numeroCamas, string estadoH)
        {
            Habitacion habitacion = new Habitacion();
            habitacion.NumeroH = numeroH;
            habitacion.CostoH = costoH;
            habitacion.TipoH = tipoH;
            habitacion.NumeroCamas = numeroCamas;
            habitacion.EstadoH = estadoH;
            return habitacion.Insertar(habitacion);
        }

        public static DataTable ObtenerHabitacionesPorTipo(string tipoHabitacion)
        {
            Habitacion habitacion = new Habitacion();
            return habitacion.ObtenerHabitacionesPorTipo(tipoHabitacion);
        }

        public static DataTable ObtenerHabitaciones()
        {
            return new Habitacion().ObtenerDatosHabitaciones();
        }

        public static string ActualizarEstadoHabitacion(int codHabitacion, string nuevoEstado)
        {
            string resultado = "";

            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Habitacion SET EstadoH = @NuevoEstado WHERE codHabitacion = @CodHabitacion", con);
                    cmd.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@CodHabitacion", codHabitacion);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        resultado = "Estado de habitación actualizado correctamente.";
                    }
                    else
                    {
                        resultado = "No se pudo actualizar el estado de la habitación.";
                    }
                }
            }
            catch (SqlException ex)
            {
                resultado = "Error al actualizar el estado de la habitación: " + ex.Message;
            }

            return resultado;
        }
    }
}

