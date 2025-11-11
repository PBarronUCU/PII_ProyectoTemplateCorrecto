using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library
{
    /// <summary>
    /// Se encarga de Agregar usuarios a la base de datos y si es necesario, suspenderlos
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// Nombre del Admin
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nombre"></param>
        public Admin(string nombre)
        {
            Nombre = nombre;
        }
        
        public void AgregarUsuario(string name, string apell, string correo)
        {
            BaseDatosUsuario bd2 = BaseDatosUsuario.Instance;
            if (!bd2.ExisteCorreo(correo))
            {
                Usuario instanca2 = new Usuario(name, apell, correo);
            }
            else
            {
                throw new Exception("Este correo ya esta en uso");
            }
        }

        public void SuspenderUsuario(string correo)
        {
        
            BaseDatosUsuario bd1 = BaseDatosUsuario.Instance;
            foreach (Usuario usuario in bd1.ListaUsuario) //Habria que usar el metodo que ya tiene la clase
            {
                if (usuario.Correo == correo) // buscamos el correo del usuario a suspender
                {
                    usuario.Suspender();
                    break; // salimos del bucle cuando lo encontramos
                }
            }
        }
    }
}