using System;

namespace Library

{
    public class Mensaje : IInteracionDialogo
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public string Notas { get; set; }
        public int TelCliente { get; }
        public UsuarioOCliente Remitente {get; set;}
        public bool Respondida { get; set; } = false;
        /// <summary>
        /// Constructor de la Clase. Si no encuentra el telefono del cliente en la base de datos, tira una excepcion
        /// </summary>
        /// <param name="remitente"></param>
        /// <param name="tema"></param>
        /// <param name="notas"></param>
        /// <param name="fecha"></param>
        /// <param name="telCliente"></param>
        /// <param name="respondida"></param>
        public Mensaje (UsuarioOCliente remitente, string tema, string notas, DateTime fecha, int telCliente ,bool respondida)
        {
            
            BaseDatosCliente bd1 = BaseDatosCliente.Instance;
            if (bd1.ExisteTel(telCliente))
            {
                Respondida = respondida;
                Remitente = remitente;
                Tema = tema;
                Notas = notas;
                Fecha = fecha;
                TelCliente = telCliente;
            }
            else
            {
                throw new ArgumentException("Telefono no encontrado");
            }
        }
    }
}