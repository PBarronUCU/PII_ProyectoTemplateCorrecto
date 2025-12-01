using System;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;



namespace Library
{
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