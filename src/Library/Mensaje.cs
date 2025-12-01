using System;

namespace Library

{
    public class Mensaje : IInteracionDialogo
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public string Notas { get; set; }
        public Cliente Cliente { get; }
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
        public Mensaje (UsuarioOCliente remitente, string tema, string notas, DateTime fecha, Cliente cliente ,bool respondida)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente),"Cliente no encontrado");
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