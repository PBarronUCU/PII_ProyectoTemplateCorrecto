using System;
namespace Library
{
    public interface IInteracion
    {
        DateTime Fecha { get; set; }
        String Tema { get; set; }
        String Notas { get; set; }
        int TelCliente { get; }
    }
}