using System.Collections.Generic;
using System;

namespace Library

{
    public sealed class BaseDatosVenta
    {
        private static readonly BaseDatosVenta _instance = new BaseDatosVenta();
        public List<Venta> ListaVentas = new List<Venta>();
        
        private BaseDatosVenta()
        {
        }
        
        public static BaseDatosVenta Instance
        {
            get { return _instance; }
        }
        
        public bool ExisteVenta(Usuario user, Cliente cliente, string producto, double precio, DateTime fecha)
        {
            bool resultado = false;
            foreach (Venta venta in ListaVentas)
            {
                if (venta.Usuario==user & venta.Cliente==cliente & venta.Producto==producto & venta.Precio==precio & venta.FechaVenta==fecha)
                {
                    resultado =  true;
                }
            }
            return resultado;
        }
        
        public void AgregarVenta(Venta venta)
        {
            Usuario usu = venta.Usuario;
            DateTime fecha = venta.FechaVenta;
            Cliente cliente = venta.Cliente;
            string producto = venta.Producto;
            double precio = venta.Precio;
            if (!ExisteVenta(usu,cliente, producto, precio, fecha))
            {
                ListaVentas.Add(venta);
            }
        }
        
        
    }
}