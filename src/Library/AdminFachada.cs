using System;

namespace Library

{
    /// <summary>
    /// Esta clase se encarga de ejecturar los metodos de admin
    /// </summary>
    public sealed class AdminFachada
    {
        private static readonly AdminFachada _instance = new AdminFachada();
        BaseDatosAdmin _bd = BaseDatosAdmin.Instance;
        
        private AdminFachada()
        {
            
        }
        /// <summary>
        /// Usar este metodo para referirse siempre a la misma instancia de esta clase
        /// </summary>
        public static AdminFachada Instance
        {
            get { return _instance; }
        }
        
        
        /// <summary>
        /// Crea el administrador con el nombre indicado. Si ya existe un admin con ese nombre, tira una excepcion.
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
        /// Crea un nuevo usuario a través del administrador indicado. Si no se encuentra al Administrador, tira una excepcion
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
                throw new Exception("Admin no encontrado.");
                
            }
            admin.AgregarUsuario(name, apellido, correo);
            
        }

        /// <summary>
        /// Suspende un usuario existente a través del administrador indicado.  Si no se encuentra al Administrador, tira una excepcion
        /// </summary>
        /// <param name="correo"></param>
        public void SuspenderUsuario(string nombreAdmin,string correo)
        {
            Admin admin =_bd.AdminSegunNombre(nombreAdmin);
            if (admin == null)
            {
                throw new Exception("Admin no encontrado.");
               
            }
            admin.SuspenderUsuario(correo);
        }
    }
}
