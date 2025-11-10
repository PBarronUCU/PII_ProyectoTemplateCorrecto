using System;

namespace Library
{
    public class InteraccionFachada
    {
        // Crea una nueva reunión con los datos proporcionados
        public Reunion CrearReunion(string fecha, string tema, string notas, int telcliente, string lugar)
        {
            DateTime f = DateTime.Parse(fecha);
            Reunion reunion = new Reunion(f, tema, notas, telcliente, lugar);
            return reunion;
        }

        // Crea un nuevo correo electrónico con los datos especificados
        public CorreoElectronico CrearCorreo(string remitente, string fecha, string tema, int telcliente, string notas, bool respondido)
        {
            DateTime f = DateTime.Parse(fecha);
            UsuarioOCliente rem = Enum.Parse<UsuarioOCliente>(remitente);
            CorreoElectronico correo = new CorreoElectronico(rem, f, tema, telcliente, notas, respondido);
            return correo;
        }

        // Crea una nueva llamada con la información proporcionada
        public Llamada CrearLlamada(string remitente, string fecha, string tema, int telcliente, string notas, bool respondido)
        {
            DateTime f = DateTime.Parse(fecha);
            UsuarioOCliente rem = Enum.Parse<UsuarioOCliente>(remitente);
            Llamada llamada = new Llamada(rem, f, tema, telcliente, notas, respondido);
            return llamada;
        }

        // Crea un nuevo mensaje con los datos indicados
        public Mensaje CrearMensaje(string remitente, string fecha, string tema, int telcliente, string notas, bool respondido)
        {
            DateTime f = DateTime.Parse(fecha);
            UsuarioOCliente rem = Enum.Parse<UsuarioOCliente>(remitente);
            Mensaje mensaje = new Mensaje(rem, f, tema, telcliente, notas, respondido);
            return mensaje;
        }
    }

    // Clase vacía temporal, solo para evitar errores de compilación
    public class Llamada
    {
    }
}