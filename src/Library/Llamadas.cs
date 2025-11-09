using System;

namespace Library

{
    public class Llamadas : IInteracionDialogo
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public string Notas { get; set; }
        public int TelCliente { get; }
        public UsuarioOCliente Remitente {get; set;}
        
        public Llamadas(UsuarioOCliente remitente, string tema, string notas, DateTime fecha, int telCliente)
        {
            Remitente = remitente;
            Tema = tema;
            Notas = notas;
            Fecha = fecha;
            TelCliente = telCliente;
        }
    }
}