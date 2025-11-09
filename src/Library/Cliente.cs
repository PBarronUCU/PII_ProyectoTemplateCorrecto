using System;

namespace Library

{
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; }
        public int Tel { get; }
        public Genero Genero { get; set; }
        public DateTime FechaNac { get; set; }
        
        
        

        public Cliente(string nombre, string apellido, string correo, int tel)
        {
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
                Console.WriteLine("Correo o Telefono ya ocupado");
            }
        }
    }
}