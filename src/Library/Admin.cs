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
        /// <summary>
        /// Agrega usuario a la base de datos. Si el correo ya existe lanza una excepcion
        /// </summary>
        /// <param name="name"></param>
        /// <param name="apell"></param>
        /// <param name="correo"></param>
        /// <exception cref="Exception"></exception>
        public void AgregarUsuario(string name, string apell, string correo)
        {
            BaseDatosUsuario bd2 = BaseDatosUsuario.Instance;
            if (!bd2.ExisteCorreo(correo))
            {
                    new Usuario(name, apell, correo);
                    
            }
            else
            {
                throw new Exception("Este correo ya esta en uso");
            }
            
            
        }
        /// <summary>
        /// Suspende un usuario. Los usuarios suspendido no pueden relizar sus funciones.
        /// </summary>
        /// <param name="correo"></param>
        public void SuspenderUsuario(string correo)
        {
        
            BaseDatosUsuario bd1 = BaseDatosUsuario.Instance;
            Usuario u = bd1.UsuarioSegunCorreo(correo);
            if (bd1.ExisteCorreoUser(correo))
            {
                u.Suspender();
            }
            else
            {
                throw new Exception("No se ha encontrado el usuario");
            }
        }
    }
}