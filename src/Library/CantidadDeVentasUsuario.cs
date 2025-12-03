using System.ComponentModel.DataAnnotations;

namespace Library
{
    /// <summary>
    /// DEFENSA PROYECTO: cree esta clase para poer guardan en un mismo objeto un usuario y un numero que
    /// representa la cantidad de bono. Lo hice asi porque no se como hacer lo que seria en python una matrix de dimension 2 con dos
    /// tipos de datos diferentes y como estoy limitado por el tiempo tome la opcion mas facil que se me occurio.
    /// </summary>
    public class CantidadDeVentasUsuario
    {
        
        public Usuario Usuario {get; set;}
        /// <summary>
        /// El bono que va a recivir el usuario
        /// Podria haber representado la cantidad de ventas tambien, pero como era para la defense
        /// decidi hacer asi.
        /// Si hubiese sido el proyecto este argumento hubiese representado la cantidad de ventas y luego otras
        /// partes del codigo se hubiese encargado de multiplicar para conseguir el bono.
        /// </summary>
        public int CantidadVentaBono {get; set;}

        public CantidadDeVentasUsuario(Usuario usuario, int cantidadVenta)
        {
            Usuario = usuario;
            CantidadVentaBono = cantidadVenta;
        }
        
        
    }
    
    
}