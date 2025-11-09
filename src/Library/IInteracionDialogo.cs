using System;
namespace Library
{
    public interface IInteracionDialogo
    {
        DateTime Fecha { get; set; }
        String Tema { get; set; }
        String Notas { get; set; }
        int TelCliente { get; }
        UsuarioOCliente Remitente {get; set;}
    }
}