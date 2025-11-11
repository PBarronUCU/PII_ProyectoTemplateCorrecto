using System;

namespace Library

{
    /// <summary>
    /// Esta clase se encarga de ejecturar los metodos de admin
    /// </summary>
    public static class AdminFachada
    {/// <summary>
     ///Guarda la instancia del administrador que está usando el sistema.
     /// </summary>
        private static Admin _adminActual; 

        /// <summary>
        /// Inicializa el administrador con el nombre indicado.
        /// </summary>
        /// <param name="name"></param>
        public static void InicializarAdmin(string name)
        {
            _adminActual = new Admin(name);
            
        }

        /// <summary>
        /// Crea un nuevo usuario a través del administrador actual. Si no se inicializa un Admin antes, tira una excepcion
        /// </summary>
        /// <param name="name"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        public static void CrearUsuario(string name, string apellido, string correo)
        {
            if (_adminActual == null)
            {
                throw new Exception("Debe inicializar un administrador antes de crear usuarios.");
                
            }
            _adminActual.AgregarUsuario(name, apellido, correo);
            
        }

        /// <summary>
        /// Suspende un usuario existente a través del administrador actual. Si no se inicializa un Admin antes, tira una excepcion
        /// </summary>
        /// <param name="correo"></param>
        public static void SuspenderUsuario(string correo)
        {
            if (_adminActual == null)
            {
                throw new Exception("Debe inicializar un administrador antes de suspender usuarios.");
               
            }
            _adminActual.SuspenderUsuario(correo);
        }
    }
}
