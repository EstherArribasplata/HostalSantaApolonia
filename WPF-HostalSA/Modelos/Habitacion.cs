using System.Data;
using System.Data.SqlClient;
using System.Windows;
using WPF_HostalSA.Conexion;

namespace WPF_HostalSA.Modelos
{
    public class Habitacion
    {
        private int codHabitacion;
        private string numeroH;
        private decimal costoH;
        private string tipoH;
        private int numeroCamas;
        private string estadoH;

        public Habitacion()
        {
        }

        public Habitacion(int codHabitacion, string numeroH, decimal costoH, string tipoH, int numeroCamas, string estadoH)
        {

            this.CodHabitacion = codHabitacion;
            this.NumeroH = numeroH;
            this.CostoH = costoH;
            this.TipoH = tipoH;
            this.NumeroCamas = numeroCamas;
            this.EstadoH = estadoH;

        }

        public int CodHabitacion { get => codHabitacion; set => codHabitacion = value; }
        public string NumeroH { get => numeroH; set => numeroH = value; }
        public decimal CostoH { get => costoH; set => costoH = value; }
        public string TipoH { get => tipoH; set => tipoH = value; }
        public int NumeroCamas { get => numeroCamas; set => numeroCamas = value; }
        public string EstadoH { get => estadoH; set => estadoH = value; }


        public string Insertar(Habitacion habitacion)
        {
            string rpta = "";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("INSERT INTO Habitacion (NumeroH, CostoH, TipoH, NumeroCamas, EstadoH) " +
"VALUES (@NumeroH, @CostoH, @TipoH, @NumeroCamas, @EstadoH)", con);

                cmd.Parameters.AddWithValue("@NumeroH", habitacion.NumeroH);
                cmd.Parameters.AddWithValue("@CostoH", habitacion.CostoH);
                cmd.Parameters.AddWithValue("@TipoH", habitacion.TipoH);
                cmd.Parameters.AddWithValue("@NumeroCamas", habitacion.NumeroCamas);
                cmd.Parameters.AddWithValue("@EstadoH", habitacion.EstadoH);
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

        public DataTable ObtenerDatosHabitaciones()
        {
            string rpta = "";
            DataTable dtHabitaciones = new DataTable("Habitacion");
            try
            {
                SqlConnection con = new SqlConnection();
                SqlDataAdapter da = new SqlDataAdapter();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("SELECT codHabitacion, NumeroH, CostoH, TipoH, NumeroCamas, EstadoH FROM Habitacion", con);
                da.SelectCommand = cmd;
                da.Fill(dtHabitaciones);
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                dtHabitaciones = null;

            }
            return dtHabitaciones;
        }

        public void ActualizarHabitacion( )
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("UPDATE Habitacion SET NumeroH = @NumeroH, CostoH = @CostoH, TipoH = @TipoH, NumeroCamas = @NumeroCamas, EstadoH = @EstadoH WHERE codHabitacion = @CodHabitacion", con);
                cmd.Parameters.AddWithValue("@NumeroH", this.NumeroH);
                cmd.Parameters.AddWithValue("@CostoH", this.CostoH);
                cmd.Parameters.AddWithValue("@TipoH", this.TipoH);
                cmd.Parameters.AddWithValue("@NumeroCamas", this.NumeroCamas);
                cmd.Parameters.AddWithValue("@EstadoH", this.EstadoH);
                cmd.Parameters.AddWithValue("@CodHabitacion", this.CodHabitacion);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Habitación actualizada correctamente.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al actualizar la habitación: " + ex.Message);
            }
        }
        public DataTable ObtenerHabitacionesPorTipo(string tipoHabitacion)
        {
            DataTable dtHabitaciones = new DataTable("Habitacion");
            try
            {
                SqlConnection con = new SqlConnection();
                SqlDataAdapter da = new SqlDataAdapter();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("SELECT codHabitacion, NumeroH, CostoH, TipoH FROM Habitacion WHERE TipoH = @TipoHabitacion", con);
                cmd.Parameters.AddWithValue("@TipoHabitacion", tipoHabitacion);
                da.SelectCommand = cmd;
                da.Fill(dtHabitaciones);
            }
            catch (SqlException)
            {
                dtHabitaciones = null;
            }
            return dtHabitaciones;
        }
        public bool EliminarHabitacion( )
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("DELETE FROM Habitacion WHERE codHabitacion = @CodHabitacion", con);
                cmd.Parameters.AddWithValue("@CodHabitacion", codHabitacion);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected > 0; // Devuelve verdadero si se eliminó al menos una fila en la base de datos.
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al eliminar la habitación: " + ex.Message);
                return false;
            }
        }
    }
}



