using System.Data;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Controladores
{
    public class CtrCliente
    {
        public static string Insertar(string nombreC, string apellidosC, string DNIC, string telefonoC, string direccionC, string correoC)
        {
            Cliente cliente = new Cliente();
            cliente.NombreC = nombreC;
            cliente.ApellidosC = apellidosC;
            cliente.DniC = DNIC;
            cliente.TelefonoC = telefonoC;
            cliente.DireccionC = direccionC;
            cliente.CorreoC = correoC;
            return cliente.Insertar(cliente);
        }

        public static DataTable ObtenerClientes()
        {
            return new Cliente().ObtenerDatosClientes();
        }
    }
}
