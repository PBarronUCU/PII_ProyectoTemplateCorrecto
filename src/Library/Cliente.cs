using System;

namespace Library

{
    /// <summary>
    /// Clase cliente
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Apellido del cliente
        /// </summary>
        public string Apellido { get; set; }
        /// <summary>
        /// Correo del cliente. Es unico
        /// </summary>
        public string Correo { get; }
        /// <summary>
        /// Telefono del cliente. Es unico y se usa para identificar a un cliente en la base de datos
        /// </summary>
        public int Tel { get; }
        /// <summary>
        /// Puede ser MASCULINO,FEMENINO U OTRO
        /// </summary>
        public Genero Genero { get; set; }
        /// <summary>
        /// Fechad de nacimiento del cliente
        /// </summary>
        public DateTime FechaNac { get; set; }
        
        
        
        /// <summary>
        /// Constructor de clase cliente. Si el correo o telefono ya existe, tira un excepcion.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        /// <param name="tel"></param>
        public Cliente(string nombre, string apellido, string correo, int tel)
        {
            if (!correo.Contains("@"))
            {
                throw new ArgumentException("El correo no es valido");
            }
            BaseDatosCliente bd1 = BaseDatosCliente.Instance;
            if (!bd1.ExisteCorreo(correo) & !bd1.ExisteTel(tel))
            {
                Nombre = nombre;
                Apellido = apellido;
                Correo = correo;
                Tel = tel;
                bd1.AgregarCliente(this);
            }
            else
            {
                throw new ArgumentException("Correo o Telefono ya ocupado");
            }
        }
    }
}