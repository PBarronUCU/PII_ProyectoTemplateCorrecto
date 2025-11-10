using System.Collections.Generic;
using System;

namespace Library

{
    /// <summary>
    /// Base de datos para las Ventas
    /// </summary>
    public sealed class BaseDatosVenta
    {
        private static readonly BaseDatosVenta _instance = new BaseDatosVenta();
        /// <summary>
        /// Donde se guardan las Ventas
        /// </summary>
        public List<Venta> ListaVentas = new List<Venta>();
        
        private BaseDatosVenta()
        {
        }
        /// <summary>
        /// Usar este metodo para referirse siempre a la misma instancia de esta clase
        /// </summary>
        public static BaseDatosVenta Instance
        {
            get { return _instance; }
        }
        /// <summary>
        /// Recorre todos las ventas guardadas. Compara todos los atributos con los parametros recibidos, devuelve true si todos coinciden.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cliente"></param>
        /// <param name="producto"></param>
        /// <param name="precio"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Agrega una venta a la lista. Solo la agrega si el metodo ExisteVenta devuelve false.
        /// </summary>
        /// <param name="venta"></param>
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