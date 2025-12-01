using System;

namespace Library

{
    /// <summary>
    /// Clase Cotizacion. Representa una oportunidad de venta
    /// </summary>
    public class Cotizacion : IInteracion
    {
        
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
        /// <summary>
        ///  Informaicion extra de la llamada
        /// </summary>
        public String Notas { get; set; }
        /// <summary>
        /// Temas tratados en la cotizacion,por ejemplo el producto a cotizar
        /// </summary>
        public string Tema { get; set; } 
        
        /// <summary>
        /// Constructor de la Clase. Si el cliente es null, tira una exepcion
        /// </summary>
        /// <param name="tema"></param>
        /// <param name="notas"></param>
        /// <param name="fecha"></param>
        /// <param name="cliente"></param>
        /// <param name="valor"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Cotizacion(string tema, string notas, DateTime fecha, Cliente cliente, Double valor)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente),"Cliente no encontrado");
            }     
            Cliente = cliente;
            Fecha = fecha;
            Notas = notas;
            Tema = tema;
            Valor = valor;
            
        }
    }
}