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
        ///  Telefono del cliente que fue contactado
        /// </summary>
        public int TelCliente { get; }
        /// <summary>
        /// Puede ser USUARIO o CLIENTE
        /// </summary>
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
        /// <exception cref="ArgumentException"></exception>
        public Llamada(UsuarioOCliente remitente, string tema, string notas, DateTime fecha, int telCliente, bool respondida)
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