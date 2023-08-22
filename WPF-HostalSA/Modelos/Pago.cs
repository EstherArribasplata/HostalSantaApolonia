using System.Data;
using System.Data.SqlClient;
using System.Windows;
using WPF_HostalSA.Conexion;

namespace WPF_HostalSA.Modelos
{
    public class Pago
    {
        private int codPago;
        private int codAlquiler;
        private decimal precioTotal;

        public Pago()
        {
        }

        public Pago(int codPago, int codAlquiler, decimal precioTotal)
        {
            this.CodPago = codPago;
            this.CodAlquiler = codAlquiler;
            this.PrecioTotal = precioTotal;
        }

        public int CodPago { get => codPago; set => codPago = value; }
        public int CodAlquiler { get => codAlquiler; set => codAlquiler = value; }
        public decimal PrecioTotal { get => precioTotal; set => precioTotal = value; }

        public string Insertar(Pago pago)
        {
            string rpta = "";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("INSERT INTO Pago (codAlquiler, precioTotal) " +
                                                "VALUES (@CodAlquiler, @PrecioTotal)", con);

                cmd.Parameters.AddWithValue("@CodAlquiler", pago.CodAlquiler);
                cmd.Parameters.AddWithValue("@PrecioTotal", pago.PrecioTotal);
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

        public DataTable ObtenerDatosPagos()
        {
            string rpta = "";
            DataTable dtPagos = new DataTable("Pago");
            try
            {
                SqlConnection con = new SqlConnection();
                SqlDataAdapter da = new SqlDataAdapter();
                con.ConnectionString = ConexionDB.cn;
                SqlCommand cmd = new SqlCommand("SELECT CodPago, codAlquiler, precioTotal FROM Pago", con);
                da.SelectCommand = cmd;
                da.Fill(dtPagos);
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                dtPagos = null;
            }
            return dtPagos;
        }
    }
}