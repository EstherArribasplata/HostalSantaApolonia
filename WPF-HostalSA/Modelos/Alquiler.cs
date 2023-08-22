using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using WPF_HostalSA.Conexion;

namespace WPF_HostalSA.Modelos
{
    public class Alquiler
    {
        private int codAlquiler;
        private int codReserva;
        private int codCliente;
        private int codHabitacion;
        private string estadoPago;

        public Alquiler()
        {
        }

        public Alquiler(int codAlquiler, int codReserva, int codCliente, int codHabitacion, string estadoPago)
        {
            this.CodAlquiler = codAlquiler;
            this.CodReserva = codReserva;
            this.CodCliente = codCliente;
            this.CodHabitacion = codHabitacion;
            this.EstadoPago = estadoPago;
        }

        public int CodAlquiler { get => codAlquiler; set => codAlquiler = value; }
        public int CodReserva { get => codReserva; set => codReserva = value; }
        public int CodCliente { get => codCliente; set => codCliente = value; }
        public int CodHabitacion { get => codHabitacion; set => codHabitacion = value; }
        public string EstadoPago { get => estadoPago; set => estadoPago = value; }

        public string Insertar(Alquiler alquiler)
        {
            String rpta = "";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("INSERT INTO Alquiler (codReserva, codCliente, codHabitacion, EstadoPago) " +
                                                "VALUES (@CodReserva, @CodCliente, @CodHabitacion, @EstadoPago)", con);

                if (alquiler.CodReserva == 0)
                {
                    cmd.Parameters.AddWithValue("@CodReserva", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CodReserva", alquiler.CodReserva);
                }

                cmd.Parameters.AddWithValue("@CodCliente", alquiler.CodCliente);
                cmd.Parameters.AddWithValue("@CodHabitacion", alquiler.CodHabitacion);
                cmd.Parameters.AddWithValue("@EstadoPago", alquiler.EstadoPago);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                rpta = "Información registrada con éxito";
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
            }

            return rpta;
        }

        public DataTable obtenerTodosLosAlquileres()
        {
            DataTable dtAlquiler = new DataTable("Alquiler");
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT a.codAlquiler, a.codReserva, c.NombreC, c.ApellidosC, h.NumeroH, h.CostoH, h.TipoH, h.NumeroCamas, a.EstadoPago
                        FROM Alquiler a
                        JOIN Cliente c ON a.codCliente = c.codCliente
                        JOIN Habitacion h ON a.codHabitacion = h.codHabitacion
                    ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtAlquiler);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al obtener los alquileres: " + ex.Message);
                dtAlquiler = null;
            }
            return dtAlquiler;
        }
    }
}
