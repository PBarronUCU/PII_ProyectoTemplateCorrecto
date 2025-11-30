using System;

namespace Library

{
    /// <summary>
    /// Clase Cotizacion. Representa una oportunidad de venta
    /// </summary>
    public class Cotizacion
    {
        /// <summary>
        /// Usuario que esta ofrece la cotizacion
        /// </summary>
        public Usuario Usuario { get; set; }
        /// <summary>
        /// Cliente que esta ligado a al cotizacion
        /// </summary>
        public Cliente Cliente { get; set; }
        /// <summary>
        /// Fecha que fue enviada
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Valor especulado
        /// </summary>
        public Double Valor { get; set; }
        
        public String Importancia { get; set; }
        
        /// <summary>
        /// metodo constructor de clase Cotizacion
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="cliente"></param>
        /// <param name="fecha"></param>
        /// <param name="valor"></param>
        /// <param name="imp"></param>
        public Cotizacion(Usuario usuario, Cliente cliente, DateTime fecha, Double valor, String imp)
        {
            Usuario = usuario;
            Cliente = cliente;
            Fecha = fecha;
            Valor = valor;
            Importancia = imp;
            
        }
        
    }
}