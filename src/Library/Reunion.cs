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
        public Reunion(string tema, string notas, DateTime fecha, int telCliente, string lugar)
        {
            
            Tema = tema;
            Notas = notas;
            Fecha = fecha;
            TelCliente = telCliente;
            Lugar = lugar;
        }
    }
}