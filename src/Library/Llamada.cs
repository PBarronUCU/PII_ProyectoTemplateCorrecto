using System;

namespace Library

{
    /// <summary>
    /// Clase Llamada. Hereda de interfaz IInteracionDialogo
    /// </summary>
    public class Llamada : IInteracionDialogo
    {
        /// <summary>
        /// Fecha en la que se realizo la llamada
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Temas tratados en la llamada
        /// </summary>
        public string Tema { get; set; }
        /// <summary>
        ///  Informaicion extra de la llamada
        /// </summary>
        public string Notas { get; set; }
        /// <summary>
        ///  Cliente que fue contactado
        /// </summary>
        public Cliente Cliente { get; }
        /// <summary>
        /// Puede ser USUARIO o CLIENTE
        /// </summary>
        public UsuarioOCliente Remitente {get; set;}
        public bool Respondida { get; set; } = false;
        /// <summary>
        /// Constructor de la Clase. Si el cliente es null, tira una exepcion
        /// </summary>
        /// <param name="remitente"></param>
        /// <param name="tema"></param>
        /// <param name="notas"></param>
        /// <param name="fecha"></param>
        /// <param name="cliente"></param>
        /// <param name="respondida"></param>
        /// <exception cref="ArgumentException"></exception>
        public Llamada(UsuarioOCliente remitente, string tema, string notas, DateTime fecha, Cliente cliente, bool respondida)
        {
            
            if (cliente == null)
            {
                throw new Exception("Cliente no encontrado");
            }
            Respondida = respondida;
            Remitente = remitente;
            Tema = tema; 
            Notas = notas; 
            Fecha = fecha;
            Cliente = cliente;
            
            
        }
    }
}