using System;

namespace Library

{
    /// <summary>
    /// Esta clase se encarga de ejecturar los metodos de admin
    /// </summary>
    public class AdminFachada
    { /// <summary>
     ///Guarda la instancia del administrador que está usando el sistema.
     /// </summary>
        BaseDatosAdmin _bd = BaseDatosAdmin.Instance;

        /// <summary>
        /// Inicializa el administrador con el nombre indicado.
        /// </summary>
        /// <param name="name"></param>
        public void CrearAdmin(string name)
        {
            if (!_bd.ExisteNombre(name))
            {
                Admin admin = new Admin(name);
                _bd.AgregarAdmin(admin);
            }
            else
            {
                throw new Exception("El Admin ya existe");
            }
        }

        /// <summary>
        /// Crea un nuevo usuario a través del administrador actual. Si no se inicializa un Admin antes, tira una excepcion
        /// </summary>
        /// <param name="name"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        /// <param name="nombreAdmin"></param>
        public void CrearUsuario(string nombreAdmin,string name, string apellido, string correo)
        {
            Admin admin =_bd.AdminSegunNombre(nombreAdmin);
            if (admin == null)
            {
                throw new Exception("Debe inicializar un administrador antes de crear usuarios.");
                
            }
            admin.AgregarUsuario(name, apellido, correo);
            
        }

        /// <summary>
        /// Suspende un usuario existente a través del administrador actual. Si no se inicializa un Admin antes, tira una excepcion
        /// </summary>
        /// <param name="correo"></param>
        public void SuspenderUsuario(string nombreAdmin,string correo)
        {
            Admin admin =_bd.AdminSegunNombre(nombreAdmin);
            if (admin == null)
            {
                throw new Exception("Debe inicializar un administrador antes de suspender usuarios.");
               
            }
            admin.SuspenderUsuario(correo);
        }
    }
}
