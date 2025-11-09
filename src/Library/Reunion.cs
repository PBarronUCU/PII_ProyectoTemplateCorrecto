using System;


namespace Library
{
    public class Reunion : IInteracion
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public string Notas { get; set; }
        public int TelCliente { get; }
        public UsuarioOCliente Remitente {get; set;}
        public Reunion(string tema, string notas, DateTime fecha, int telCliente)
        {
            
            Tema = tema;
            Notas = notas;
            Fecha = fecha;
            TelCliente = telCliente;
        }
    }
}