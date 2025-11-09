using System;
namespace Library
{
    public interface IInteracionDialogo: IInteracion
    {
        UsuarioOCliente Remitente {get; set;}
    }
}