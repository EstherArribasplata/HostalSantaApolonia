using System.Data;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Controladores
{
    public class CtrUsuario
    {
        public static string Insertar(string nomUsuario, string claveUsuario, int cargoUsuario, string dniUsuario, string celularUsuario)
        {
            Usuario usu = new Usuario();
            usu.NomUsuario = nomUsuario;
            usu.ClaveUsuario = claveUsuario;
            usu.CargoUsuario = cargoUsuario;
            usu.DniUsuario = dniUsuario;
            usu.CelularUsuario = celularUsuario;
            return usu.insertar(usu);
        }

        public static DataTable ObtenerUsuario(string usuario, string clave, int cargo)
        {
            Usuario usu = new Usuario();
            usu.NomUsuario = usuario;
            usu.ClaveUsuario = clave;
            usu.CargoUsuario = cargo;
            return new Usuario().obtenerUsuario(usu);
        }
        public static DataTable ObtenerDatosUsuario()
        {
            return new Usuario().obtenerDatosUsu();
        }
    }
}
