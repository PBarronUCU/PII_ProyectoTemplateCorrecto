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
        /// <param name="usuario"></param>
        /// <param name="tema"></param>
        /// <param name="notas"></param>
        /// <param name="fecha"></param>
        /// <param name="cliente"></param>
        /// <param name="valor"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        public bool ExisteVenta(Usuario usuario, string tema, string notas, DateTime fecha, Cliente cliente, Double valor, string producto)
        {
            bool resultado = false;
            foreach (Venta venta in ListaVentas)
            {
                if (venta.Usuario==usuario && venta.Tema==tema && venta.Notas==notas && venta.Fecha==fecha && venta.Cliente==cliente && venta.Precio==valor && venta.Producto==producto)
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
            DateTime fecha = venta.Fecha;
            Cliente cliente = venta.Cliente;
            string producto = venta.Producto;
            double precio = venta.Precio;
            string tema = venta.Tema;
            string notas = venta.Notas;
            if (!ExisteVenta(usu,tema,notas,fecha,cliente,precio,producto))
            {
                ListaVentas.Add(venta);
            }
            else
            {
                throw new ArgumentException("Venta repetida");
            }
        }
        /// <summary>
        /// Crea una lista donde guarda las ventas que realisa el usuario en las fechas que él quiere revisar para ver la rentavilidad del negocio 
        /// </summary>
        /// <param name="fechabaja"></param>
        /// <param name="fechaalta"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Venta> VentasPeriodo(Usuario user,DateTime fechabaja,DateTime fechaalta)
        { 
            List<Venta> ventasUser = ListaVentas.FindAll(x => x.Usuario == user);
            return ventasUser.FindAll(i => i.Fecha>= fechabaja && i.Fecha <= fechaalta);

        }
        
        
    }
}