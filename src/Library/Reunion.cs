using System;


namespace Library
{
    public class Reunion : IInteracion
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public string Notas { get; set; }
        public Cliente Cliente { get; }
        public string Lugar { get; set; }
        /// <summary>
        ///Constructor de la Clase. Si el cliente es null, tira una exepcion
        /// </summary>
        /// <param name="tema"></param>
        /// <param name="notas"></param>
        /// <param name="fecha"></param>
        /// <param name="cliente"></param>
        /// <param name="lugar"></param>
        public Reunion(string tema, string notas, DateTime fecha, Cliente cliente, string lugar)
        {
            
            
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente),"Cliente no encontrado");
            }            Tema = tema;
            Notas = notas;
            Fecha = fecha;
            Cliente = cliente;
            Lugar = lugar;
        }
    }
}