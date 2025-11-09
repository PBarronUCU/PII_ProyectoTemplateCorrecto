using System;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;



namespace Library
{
    public class Venta
    {
        public DateTime FechaVenta {get;}
        public Usuario Usuario { get; }
        public Cliente Cliente { get;}
        public string Producto { get;}
        public double Precio { get;}

        public Venta(Usuario usuario, Cliente cliente, string producto, double precio, DateTime diaVenta)
        {
            Usuario = usuario;
            Cliente = cliente;
            Producto = producto;
            Precio = precio;
            FechaVenta = diaVenta;
        }
    }
}