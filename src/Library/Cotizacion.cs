using System;

namespace Library

{
    public class Cotizacion
    {
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public Double Valor { get; set; }
        public String Importancia { get; set; }

        public Cotizacion(Usuario usuario, Cliente cliente, DateTime fecha, Double valor, String imp)
        {
            Usuario = usuario;
            Cliente = cliente;
            Fecha = fecha;
            Valor = valor;
            Importancia = imp;
            
        }
        
    }
}