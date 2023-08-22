using System.Data;
using System.Data.SqlClient;
using System.Windows;
using WPF_HostalSA.Conexion;

namespace WPF_HostalSA.Modelos
{
    public class Cliente
    {
        private int codCliente;
        private string nombreC;
        private string apellidosC;
        private string dniC;
        private string telefonoC;
        private string direccionC;
        private string correoC;

        public Cliente()
        {
        }

        public Cliente(int codCliente, string nombreC, string apellidosC, string dniC, string telefonoC, string direccionC, string correoC)
        {
            this.CodCliente = codCliente;
            this.NombreC = nombreC;
            this.ApellidosC = apellidosC;
            this.DniC = dniC;
            this.TelefonoC = telefonoC;
            this.DireccionC = direccionC;
            this.CorreoC = correoC;
        }

        public int CodCliente { get => codCliente; set => codCliente = value; }
        public string NombreC { get => nombreC; set => nombreC = value; }
        public string ApellidosC { get => apellidosC; set => apellidosC = value; }
        public string DniC { get => dniC; set => dniC = value; }
        public string TelefonoC { get => telefonoC; set => telefonoC = value; }
        public string DireccionC { get => direccionC; set => direccionC = value; }
        public string CorreoC { get => correoC; set => correoC = value; }

        public string Insertar(Cliente cliente)
        {
            string rpta = "";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("INSERT INTO Cliente (NombreC, ApellidosC, DNIC, TelefonoC, DireccionC, CorreoC) " +
                                                "VALUES (@NombreC, @ApellidosC, @DNIC, @TelefonoC, @DireccionC, @CorreoC)", con);

                cmd.Parameters.AddWithValue("@NombreC", cliente.NombreC);
                cmd.Parameters.AddWithValue("@ApellidosC", cliente.ApellidosC);
                cmd.Parameters.AddWithValue("@DNIC", cliente.DniC);
                cmd.Parameters.AddWithValue("@TelefonoC", cliente.TelefonoC);
                cmd.Parameters.AddWithValue("@DireccionC", cliente.DireccionC);
                cmd.Parameters.AddWithValue("@CorreoC", cliente.CorreoC);
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

        public DataTable ObtenerDatosClientes()
        {
            string rpta = "";
            DataTable dtClientes = new DataTable("Cliente");
            try
            {
                SqlConnection con = new SqlConnection();
                SqlDataAdapter da = new SqlDataAdapter();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("SELECT codCliente, NombreC, ApellidosC, DNIC, TelefonoC, DireccionC, CorreoC FROM Cliente", con);
                da.SelectCommand = cmd;
                da.Fill(dtClientes);
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                dtClientes = null;
            }
            return dtClientes;
        }
        public void ActualizarCliente()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("UPDATE Cliente SET NombreC = @NombreC, ApellidosC = @ApellidosC, DNIC = @DNIC, TelefonoC = @TelefonoC, DireccionC = @DireccionC, CorreoC = @CorreoC WHERE codCliente = @CodCliente", con);
                cmd.Parameters.AddWithValue("@NombreC", this.NombreC);
                cmd.Parameters.AddWithValue("@ApellidosC", this.ApellidosC);
                cmd.Parameters.AddWithValue("@DNIC", this.DniC);
                cmd.Parameters.AddWithValue("@TelefonoC", this.TelefonoC);
                cmd.Parameters.AddWithValue("@DireccionC", this.DireccionC);
                cmd.Parameters.AddWithValue("@CorreoC", this.CorreoC);
                cmd.Parameters.AddWithValue("@CodCliente", this.CodCliente);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Cliente actualizado correctamente.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al actualizar el cliente: " + ex.Message);
            }
        }

        public bool EliminarCliente()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("DELETE FROM Cliente WHERE codCliente = @CodCliente", con);
                cmd.Parameters.AddWithValue("@CodCliente", this.CodCliente);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0; // Devuelve verdadero si se eliminó al menos una fila en la base de datos.
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al eliminar el cliente: " + ex.Message);
                return false;
            }
        }
    }
}
