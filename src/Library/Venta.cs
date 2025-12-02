using System;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;



namespace Library
{
    // ============================================================
    // GRASP, SOLID Y PATRONES USADOS EN ESTA CLASE
    //
    // ► GRASP
    // • Creator:
    //   La clase Venta cumple Creator porque recibe toda la información
    //   necesaria para construirse (usuario, cliente, fecha, notas, tema,
    //   precio, producto). Por lo tanto es quien debe crearse a sí misma.
    //
    // • Information Expert:
    //   Venta conoce todos los datos de una venta y es experta en almacenarlos.
    //
    // • Low Coupling:
    //   Depende solo de Usuario, Cliente y la interfaz IInteracion.
    //
    // • High Cohesion:
    //   Su única responsabilidad es representar la información de una venta.
    //
    // ► SOLID
    // • S — Single Responsibility Principle:
    //   La clase solo modela una venta; no gestiona listas ni acceso a datos.
    //
    // • O — Open/Closed Principle:
    //   Abierta a extensión (pueden agregarse comportamientos),
    //   pero cerrada a modificación en su estado básico.
    //
    // • L — Liskov Substitution Principle:
    //   No usa herencia, pero implementa IInteracion sin violarlo.
    //
    // • I — Interface Segregation Principle:
    //   Implementa solo una interfaz pequeña y específica (IInteracion).
    //
    // ============================================================
    public class Venta : IInteracion
    {
        
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha {get; set; }
        public double Precio { get; set; }
        public String Notas { get; set; }
        public string Tema { get; set; } 
        public string Producto { get;}

        public Venta(Usuario usuario, string tema, string notas, DateTime fecha, Cliente cliente, Double valor, string producto)
        {
            if (usuario == null && cliente == null)
            {
                throw new Exception("Cliente y Usuario no encontrado");
            }
            if (cliente == null)
            {
                throw new Exception("Cliente no encontrado");
            }
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            Usuario = usuario;
            Cliente = cliente;
            Fecha = fecha;
            Notas = notas;
            Tema = tema;
            Precio = valor;
            Producto = producto;
            
        }
    }
}