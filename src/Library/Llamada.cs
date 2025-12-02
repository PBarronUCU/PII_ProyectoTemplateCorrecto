using System;

namespace Library

    // ============================================================
    // GRASP Y SOLID UTILIZADOS EN ESTA CLASE
    //
    // ► GRASP
    // • Creator:
    //   La clase Llamada crea su propia instancia validando los datos
    //   necesarios (como que el cliente no sea null).
    //
    // • Information Expert:
    //   Llamada conoce su propia información (tema, notas, fecha, etc.),
    //   por lo que debe ser quien gestione estos datos.
    //
    // • High Cohesion:
    //   Agrupa toda la información y comportamiento propio de una llamada.
    //
    // • Low Coupling:
    //   Depende solo de Cliente y UsuarioOCliente, pero no coordina 
    //   lógicas externas.
    //
    // ► SOLID
    // • S — Single Responsibility:
    //   Su única responsabilidad es representar una llamada como interacción.
    //
    // • O — Open/Closed:
    //   Puede extenderse creando nuevos tipos de interacciones sin modificarla.
    //
    // • L — Liskov Substitution:
    //   Cumple con la interfaz IInteracionDialogo, por lo que puede sustituirla.
    //
    // • I — Interface Segregation:
    //   Implementa solo los miembros de su interfaz, sin sobrecarga innecesaria.
    //
    // • D — Dependency Inversion:
    //   Depende de la abstracción IInteracionDialogo.
    //
    // ============================================================


{
    /// <summary>
    /// Clase Llamada. Hereda de interfaz IInteracionDialogo
    /// </summary>
    public class Llamada : IInteracionDialogo
    {
        /// <summary>
        /// Fecha en la que se realizo la llamada
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Temas tratados en la llamada
        /// </summary>
        public string Tema { get; set; }
        /// <summary>
        ///  Informaicion extra de la llamada
        /// </summary>
        public string Notas { get; set; }
        /// <summary>
        ///  Cliente que fue contactado
        /// </summary>
        public Cliente Cliente { get; }
        /// <summary>
        /// Puede ser USUARIO o CLIENTE
        /// </summary>
        public UsuarioOCliente Remitente {get; set;}
        public bool Respondida { get; set; } = false;
        /// <summary>
        /// Constructor de la Clase. Si el cliente es null, tira una exepcion
        /// </summary>
        /// <param name="remitente"></param>
        /// <param name="tema"></param>
        /// <param name="notas"></param>
        /// <param name="fecha"></param>
        /// <param name="cliente"></param>
        /// <param name="respondida"></param>
        /// <exception cref="ArgumentException"></exception>
        public Llamada(UsuarioOCliente remitente, string tema, string notas, DateTime fecha, Cliente cliente, bool respondida)
        {
            
            if (cliente == null)
            {
                throw new Exception("Cliente no encontrado");
            }
            Respondida = respondida;
            Remitente = remitente;
            Tema = tema; 
            Notas = notas; 
            Fecha = fecha;
            Cliente = cliente;
            
            
        }
    }
}