using System;

namespace Library
{
    /// <summary>
    /// Fachada para los constructores de las interacciones
    /// </summary>
    public sealed class InteraccionFachada
    {
        private static readonly InteraccionFachada _instance = new InteraccionFachada();
        
        private InteraccionFachada()
        {
            
        }
        /// <summary>
        /// Usar este metodo para referirse siempre a la misma instancia de esta clase
        /// </summary>
        public static InteraccionFachada Instance
        {
            get { return _instance; }
        }
        
        
        /// <summary>
        ///  Crea una nueva reunión con los datos proporcionados
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="tema"></param>
        /// <param name="notas"></param>
        /// <param name="telcliente"></param>
        /// <param name="lugar"></param>
        /// <returns></returns>
        public Reunion CrearReunion(string fecha, string tema, string notas, int telcliente, string lugar)
        {
            DateTime f = DateTime.Parse(fecha);
            Reunion reunion = new Reunion(tema,notas,f,telcliente,lugar);
            return reunion;
        }

        /// <summary>
        /// Crea un nuevo correo electrónico con los datos especificados
        /// </summary>
        /// <param name="remitente"></param>
        /// <param name="fecha"></param>
        /// <param name="tema"></param>
        /// <param name="telcliente"></param>
        /// <param name="notas"></param>
        /// <param name="respondido"></param>
        /// <returns></returns>
        public CorreoElectronico CrearCorreo(string remitente, string fecha, string tema, int telcliente, string notas, bool respondido)
        {
            DateTime f = DateTime.Parse(fecha);
            UsuarioOCliente rem = Enum.Parse<UsuarioOCliente>(remitente);
            CorreoElectronico correo = new CorreoElectronico(rem,tema,notas,f,telcliente,respondido);
            return correo;
        }

        /// <summary>
        /// Crea una nueva llamada con la información proporcionada
        /// </summary>
        /// <param name="remitente"></param>
        /// <param name="fecha"></param>
        /// <param name="tema"></param>
        /// <param name="telcliente"></param>
        /// <param name="notas"></param>
        /// <param name="respondido"></param>
        /// <returns></returns>
        public Llamada CrearLlamada(string remitente, string fecha, string tema, int telcliente, string notas, bool respondido)
        {
            DateTime f = DateTime.Parse(fecha);
            UsuarioOCliente rem = Enum.Parse<UsuarioOCliente>(remitente);
            Llamada llamada = new Llamada(rem, tema, notas, f, telcliente,respondido);
            return llamada;
        }

        /// <summary>
        /// Crea un nuevo mensaje con los datos indicados
        /// </summary>
        /// <param name="remitente"></param>
        /// <param name="fecha"></param>
        /// <param name="tema"></param>
        /// <param name="telcliente"></param>
        /// <param name="notas"></param>
        /// <param name="respondido"></param>
        /// <returns></returns>
        public Mensaje CrearMensaje(string remitente, string fecha, string tema, int telcliente, string notas, bool respondido)
        {
            DateTime f = DateTime.Parse(fecha);
            UsuarioOCliente rem = Enum.Parse<UsuarioOCliente>(remitente);
            Mensaje mensaje = new Mensaje(rem, tema, notas, f, telcliente,respondido);
            return mensaje;
        }
    }

    
}