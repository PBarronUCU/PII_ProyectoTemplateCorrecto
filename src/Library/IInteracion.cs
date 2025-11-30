using System;
namespace Library
{
    /// <summary>
    /// Interfaz para las interacciones
    /// </summary>
    public interface IInteracion
    {
        /// <summary>
        /// Fecha en la que se dio la interracion
        /// </summary>
        DateTime Fecha { get; set; }
        /// <summary>
        /// Temas tratados en al interracion
        /// </summary>
        String Tema { get; set; }
        /// <summary>
        /// Informaicion extra
        /// </summary>
        String Notas { get; set; }
        /// <summary>
        /// Telefono del cliente que fue contactado
        /// </summary>
        Cliente Cliente { get; }
    }
}