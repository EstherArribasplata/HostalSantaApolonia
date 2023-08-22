using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using WPF_HostalSA.Conexion;

namespace WPF_HostalSA.Modelos
{
    public class Reserva
    {
        private int codReserva;
        private int codAlquiler;
        private int codCliente;
        private int codHabitacion;
        private DateTime fechaEntrada;
        private DateTime fechaSalida;

        public Reserva()
        {
        }

        public Reserva(int codReserva, int codAlquiler, int codCliente, int codHabitacion, DateTime fechaEntrada, DateTime fechaSalida)
        {
            this.codReserva = codReserva;
            this.codAlquiler = codAlquiler;
            this.codCliente = codCliente;
            this.codHabitacion = codHabitacion;
            this.fechaEntrada = fechaEntrada;
            this.fechaSalida = fechaSalida;
        }

        public int CodReserva { get => codReserva; set => codReserva = value; }
        public int CodAlquiler { get => codAlquiler; set => codAlquiler = value; }
        public int CodCliente { get => codCliente; set => codCliente = value; }
        public int CodHabitacion { get => codHabitacion; set => codHabitacion = value; }
        public DateTime FechaEntrada { get => fechaEntrada; set => fechaEntrada = value; }
        public DateTime FechaSalida { get => fechaSalida; set => fechaSalida = value; }
        public string Tipo { get; set; }
        public string Insertar(Reserva reserva)
        {
            string rpta = "";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("INSERT INTO Reserva ( FechaEntrada, FechaSalida, codAlquiler, codHabitacion, codCliente) " +
                                                "VALUES ( @FechaEntrada, @FechaSalida, @codAlquiler, @codHabitacion, @codCliente)", con);

                if (reserva.codAlquiler == 0)
                {
                    cmd.Parameters.AddWithValue("@codAlquiler", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@codAlquiler", reserva.codAlquiler);
                }

                cmd.Parameters.AddWithValue("@FechaEntrada", reserva.FechaEntrada);
                cmd.Parameters.AddWithValue("@FechaSalida", reserva.FechaSalida);
                
                cmd.Parameters.AddWithValue("@codHabitacion", reserva.codHabitacion);
                cmd.Parameters.AddWithValue("@codCliente", reserva.codCliente);

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



        public DataTable ObtenerTodasLasReservas()
        {
            DataTable dtReservas = new DataTable("Reserva");
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT R.codReserva, C.NombreC ,C.ApellidosC ,H.TipoH , H.NumeroH ,H.CostoH ,R.FechaEntrada ,R.FechaSalida 
                        FROM Reserva R
                        JOIN Cliente C ON R.codCliente = C.codCliente
                        JOIN Habitacion H ON R.codHabitacion = H.codHabitacion;", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtReservas);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al obtener las reservas: " + ex.Message);
                dtReservas = null;
            }
            return dtReservas;
        }
    }
}
