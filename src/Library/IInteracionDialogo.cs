using System;
namespace Library
{
    /// <summary>
    /// Interfaz para las interacciones que representa una forma de dialogo
    /// </summary>
    public interface IInteracionDialogo: IInteracion
    {
        UsuarioOCliente Remitente {get; set;}
        bool Respondida { get; set; }
    }
}