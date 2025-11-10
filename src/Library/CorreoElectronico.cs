using System;

namespace Library

{
    public class CorreoElectronico : IInteracionDialogo
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public string Notas { get; set; }
        public int TelCliente { get; }
        public UsuarioOCliente Remitente {get; set;}
        public bool Respondida { get; set; } = false;
        public CorreoElectronico(UsuarioOCliente remitente, string tema, string notas, DateTime fecha, int telCliente, bool respondida)
        {
            Respondida = respondida;
            Remitente = remitente;
            Tema = tema;
            Notas = notas;
            Fecha = fecha;
            TelCliente = telCliente;
        }
    }
}