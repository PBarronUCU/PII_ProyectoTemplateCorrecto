using System;
namespace Library
{
    
    // ============================================================
    // GRASP Y SOLID UTILIZADOS EN ESTA INTERFAZ
    //
    // ► GRASP
    // • Polymorphism:
    //   La interfaz permite que distintas clases representen interacciones
    //   con comportamientos diferentes, sin que el código cliente dependa
    //   de implementaciones concretas.
    //
    // • Low Coupling:
    //   IInteracion desacopla el uso de interacciones de sus implementaciones.
    //
    // • High Cohesion:
    //   La interfaz define exclusivamente las propiedades esenciales de
    //   cualquier interacción, manteniendo cohesión alta.
    //
    // ► SOLID
    // • S — Single Responsibility Principle:
    //   La interfaz representa solo el contrato mínimo de una interacción.
    //
    // • O — Open/Closed:
    //   Permite agregar nuevos tipos de interacciones sin modificar esta interfaz.
    //
    // • L — Liskov Substitution Principle:
    //   Cualquier clase que implemente IInteracion puede sustituirla sin problemas.
    //
    // • I — Interface Segregation:
    //   Interfaz pequeña y enfocada → NO obliga a implementar cosas que no corresponden.
    //
    // • D — Dependency Inversion:
    //   El código que dependa de IInteracion depende de una abstracción, no de clases concretas.
    //
    // ============================================================

    
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