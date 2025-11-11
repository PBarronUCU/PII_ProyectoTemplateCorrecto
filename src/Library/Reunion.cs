using System;


namespace Library
{
    public class Reunion : IInteracion
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public string Notas { get; set; }
        public int TelCliente { get; }
        public string Lugar { get; set; }
        /// <summary>
        ///Constructor de la Clase. Si no encuentra el telefono del cliente en la base de datos, tira una excepcion
        /// </summary>
        /// <param name="tema"></param>
        /// <param name="notas"></param>
        /// <param name="fecha"></param>
        /// <param name="telCliente"></param>
        /// <param name="lugar"></param>
        public Reunion(string tema, string notas, DateTime fecha, int telCliente, string lugar)
        {
            
            
            BaseDatosCliente bd1 = BaseDatosCliente.Instance;
            if (bd1.ExisteTel(telCliente))
            {
                Tema = tema;
                Notas = notas;
                Fecha = fecha;
                TelCliente = telCliente;
                Lugar = lugar;
            }
            else
            {
                throw new ArgumentException("Telefono no encontrado");
            }
        }
    }
}