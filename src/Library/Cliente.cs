using System;

namespace Library

    // ============================================================
    // GRASP Y SOLID UTILIZADOS EN ESTA CLASE
    //
    // ► GRASP
    // • Information Expert:
    //   La clase Cliente es la experta en almacenar su propia 
    //   información (nombre, apellido, correo, etc.).
    //
    // • Creator:
    //   Se aplica parcialmente: Cliente inicializa su propio estado
    //   cuando se lo construye.
    //
    // • Low Coupling:
    //   La clase tiene bajo acoplamiento, excepto por una dependencia
    //   hacia BaseDatosCliente.
    //
    // • High Cohesion:
    //   La clase solo representa datos de un cliente, tiene una única 
    //   responsabilidad clara.
    //
    // ► SOLID
    // • S — Single Responsibility Principle:
    //   Cliente solo representa los datos y validaciones básicas
    //   relacionadas a un cliente.
    //
    // • O — Open/Closed:
    //   Podría extenderse, pero no está totalmente cerrada a cambios.
    //
    // ============================================================




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
        /// Constructor de clase cliente.
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
            
                Nombre = nombre;
                Apellido = apellido;
                Correo = correo;
                Tel = tel;
            
            
            
        }
    }
}