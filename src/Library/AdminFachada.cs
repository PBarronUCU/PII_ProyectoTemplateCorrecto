using System;

namespace Library

{ // -----------------------------------------------------------------------------
// GRASP UTILIZADOS:
// • Controller: Esta clase actúa como punto de entrada para las operaciones del 
//   administrador, coordinando acciones sin ejecutar la lógica interna.
//
// • Low Coupling: La fachada solo interactúa con BaseDatosAdmin y Admin, evitando 
//   dependencias innecesarias.
//
// • High Cohesion: La responsabilidad de la clase está muy clara: servir como 
//   intermediario para operaciones administrativas.
//
// • Information Expert: La lógica de búsqueda y verificación sigue estando en la 
//   base de datos, no aquí.

// SOLID UTILIZADOS:
// • SRP: La clase solo se encarga de coordinar operaciones de Admin.
//
// • OCP: Puede ampliarse con nuevas operaciones de fachada sin modificar las existentes.
//
// PATRONES:
// • Singleton: Usado. La clase expone Instance para una única fachada global.
//
// • Facade: Usado fuertemente. Centraliza y simplifica accesos a las operaciones de Admin.
//
// • Iterator: Usado implícitamente dentro de las bases de datos cuando recorren listas.
// -----------------------------------------------------------------------------
    
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
        /// <param name="nombreAdmin"></param>
        /// <param name="name"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        
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
        /// <param name="nombreAdmin"></param>
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

        public string VeralmejotVendedor(string nombreAdmin)
        {
            Admin admin = _bd.AdminSegunNombre(nombreAdmin);
            if (admin == null)
            {
                throw new Exception("Admin no encontrado.");
            }
            admin.VerUsuarioconmasVentas();
            return admin.VerUsuarioconmasVentas();
            
        }
    }
}
