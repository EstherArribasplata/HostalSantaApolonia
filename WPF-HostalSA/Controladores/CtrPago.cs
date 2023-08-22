using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using WPF_HostalSA.Conexion;
using WPF_HostalSA.Modelos;

namespace WPF_HostalSA.Controladores
{
    public class CtrPago
    {
        public static string Insertar(int codAlquiler, decimal precioTotal)
        {
            Pago pago = new Pago();
            pago.CodAlquiler = codAlquiler;
            pago.PrecioTotal = precioTotal;
            return pago.Insertar(pago);
        }

        public static DataTable ObtenerPagos()
        {
            return new Pago().ObtenerDatosPagos();
        }
    }
}