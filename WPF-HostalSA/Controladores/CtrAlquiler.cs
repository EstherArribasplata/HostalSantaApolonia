using System;
using System.Data;
using System.Data.SqlClient;
using WPF_HostalSA.Conexion;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Controladores
{
    public class CtrAlquiler
    {
        public static string Insertar(int codReserva, int codCliente, int codHabitacion, string estadoPago)
        {
            Alquiler alquiler = new Alquiler();
            alquiler.CodReserva = codReserva;
            alquiler.CodCliente = codCliente;
            alquiler.CodHabitacion = codHabitacion;
            alquiler.EstadoPago = estadoPago;
            return alquiler.Insertar(alquiler);
        }

        public static DataTable ObtenerAlquileres()
        {
            return new Alquiler().obtenerTodosLosAlquileres();
        }
        public static bool EliminarAlquiler(int codAlquiler)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    con.Open();
                    string query = "DELETE FROM Alquiler WHERE codAlquiler = @CodAlquiler";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CodAlquiler", codAlquiler);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Retorna true si se eliminó al menos una fila
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el alquiler: " + ex.Message);
                return false;
            }
        }

        public static DataTable ObtenerAlquileresPendientes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    con.Open();
                    string query = @"
                SELECT A.codAlquiler ,C.NombreC, C.ApellidosC,H.TipoH ,H.CostoH, A.EstadoPago
                FROM Alquiler A
                INNER JOIN Cliente C ON A.codCliente = C.codCliente
                INNER JOIN Habitacion H ON A.codHabitacion = H.codHabitacion
                WHERE A.EstadoPago = 'Pendiente'
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dtAlquileres = new DataTable();
                            adapter.Fill(dtAlquileres);
                            return dtAlquileres;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener alquileres pendientes: " + ex.Message);
            }
        }
        public static DataTable ObtenerAlquileresPagados()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    con.Open();
                    string query = @"
                SELECT A.codAlquiler ,C.NombreC, C.ApellidosC,H.TipoH ,H.CostoH, A.EstadoPago
                FROM Alquiler A
                INNER JOIN Cliente C ON A.codCliente = C.codCliente
                INNER JOIN Habitacion H ON A.codHabitacion = H.codHabitacion
                WHERE A.EstadoPago = 'Pagado'
            ";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dtAlquileres = new DataTable();
                            adapter.Fill(dtAlquileres);
                            return dtAlquileres;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener alquileres pagados: " + ex.Message);
            }
        }

        public static bool ActualizarEstadoPago(int codAlquiler, string nuevoEstado)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    con.Open();
                    string query = "UPDATE Alquiler SET EstadoPago = @NuevoEstado WHERE codAlquiler = @CodAlquiler";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@CodAlquiler", codAlquiler);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Retorna true si se actualizó al menos una fila
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el estado de pago: " + ex.Message);
                return false;
            }
        }
    }
}