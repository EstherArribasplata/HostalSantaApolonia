using System.Data;
using System.Data.SqlClient;
using System.Windows;
using WPF_HostalSA.Conexion;

namespace WPF_HostalSA.Modelos
{
    public class Usuario
    {
        private int codUsuario;
        private string nomUsuario;
        private string claveUsuario;
        private int cargoUsuario;
        private string dniUsuario;
        private string celularUsuario;

        public Usuario()
        {
        }

        public Usuario(int codUsuario, string nomUsuario, string claveUsuario, int cargoUsuario, string dniUsuario, string celularUsuario)
        {
            this.CodUsuario = codUsuario;
            this.NomUsuario = nomUsuario;
            this.ClaveUsuario = claveUsuario;
            this.CargoUsuario = cargoUsuario;
            this.DniUsuario = dniUsuario;
            this.CelularUsuario = celularUsuario;
        }

        public int CodUsuario { get => codUsuario; set => codUsuario = value; }
        public string NomUsuario { get => nomUsuario; set => nomUsuario = value; }
        public string ClaveUsuario { get => claveUsuario; set => claveUsuario = value; }
        public int CargoUsuario { get => cargoUsuario; set => cargoUsuario = value; }
        public string DniUsuario { get => dniUsuario; set => dniUsuario = value; }
        public string CelularUsuario { get => celularUsuario; set => celularUsuario = value; }

        public string insertar(Usuario usu)
        {
            string rpta = "";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("INSERT INTO Usuario VALUES (@NombreU,@ContrasenaU,@CargoU, @DniU, @CelularU)", con);
                cmd.Parameters.AddWithValue("@NombreU", usu.nomUsuario);
                cmd.Parameters.AddWithValue("@ContrasenaU", usu.claveUsuario);
                cmd.Parameters.AddWithValue("@CargoU", usu.cargoUsuario);
                cmd.Parameters.AddWithValue("@DniU", usu.dniUsuario);
                cmd.Parameters.AddWithValue("@CelularU", usu.celularUsuario);

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

        public DataTable obtenerUsuario(Usuario login)
        {
            DataTable dtUsuario = new DataTable("Usuario");
            try
            {
                SqlConnection con = new SqlConnection();
                SqlDataAdapter da = new SqlDataAdapter();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("SELECT NombreU, CargoU FROM usuario " + "WHERE NombreU = @NombreU AND ContrasenaU = @ContrasenaU", con);
                cmd.Parameters.AddWithValue("@NombreU", login.nomUsuario);
                cmd.Parameters.AddWithValue("@ContrasenaU", login.claveUsuario);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dtUsuario.Load(dr);
                dr.Close();
                con.Close();
                return dtUsuario;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                dtUsuario = null;
            }
            return dtUsuario;
        }
        public DataTable obtenerDatosUsu()
        {
            string rpta = "";
            DataTable dtUsuario = new DataTable("Usuario");
            try
            {
                SqlConnection con = new SqlConnection();
                SqlDataAdapter da = new SqlDataAdapter();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("SELECT codUsuario, NombreU, ContrasenaU, CargoU, DNIU, CelularU FROM Usuario", con);
                da.SelectCommand = cmd;
                da.Fill(dtUsuario);
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                dtUsuario = null;
            }
            return dtUsuario;
        }

        public void ActualizarUsuario()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("UPDATE Usuario SET NombreU = @NombreU, ContrasenaU = @ContrasenaU, DNIU = @DNIU, CelularU = @CelularU WHERE codUsuario = @CodUsuario", con);
                cmd.Parameters.AddWithValue("@NombreU", this.NomUsuario);
                cmd.Parameters.AddWithValue("@ContrasenaU", this.ClaveUsuario);
                cmd.Parameters.AddWithValue("@DNIU", this.DniUsuario);
                cmd.Parameters.AddWithValue("@CelularU", this.CelularUsuario);
                cmd.Parameters.AddWithValue("@CodUsuario", this.CodUsuario);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Usuario actualizado correctamente.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al actualizar el usuario: " + ex.Message);
            }
        }

        public bool EliminarUsuario()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE codUsuario = @CodUsuario", con);
                cmd.Parameters.AddWithValue("@CodUsuario", this.CodUsuario);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0; // Devuelve verdadero si se eliminó al menos una fila en la base de datos.
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message);
                return false;
            }
        }
    }
}
