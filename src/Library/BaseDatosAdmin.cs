using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Base de datos para los Admins
    /// </summary>
    public sealed class BaseDatosAdmin
    {
        private static BaseDatosAdmin _instance = new BaseDatosAdmin();
        /// <summary>
        /// Donde se guardan los admins
        /// </summary>
        public List<Admin> ListaAdmin = new List<Admin>();
        
        private BaseDatosAdmin()
        {
        }
        /// <summary>
        /// Usar este metodo para referirse siempre a la misma instancia de esta clase
        /// </summary>
        public static BaseDatosAdmin Instance
        {
            get { return _instance; }
        }
        /// <summary>
        /// Metodo para poder reiniciar mis singletons despues de cada test
        /// </summary>
        public static void ResetInstance()
        {
            _instance = new BaseDatosAdmin();
        }
        
        /// <summary>
        /// Recorre todos los admins guardados. Compara el nombre de cada uno con el parametro, devuelve true si uno coincide.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ExisteNombre(string name)
        {
            bool result = false;
            foreach (Admin admin in ListaAdmin)
            {
                if (admin.Nombre == name)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// Agrega un admin a la lista. Solo lo agrega si no existe un admin con ese nombre.
        /// </summary>
        /// <param name="admin"></param>
        public void AgregarAdmin(Admin admin)
        {
            string nombre = admin.Nombre;
            if (!ExisteNombre(nombre))
            {
                ListaAdmin.Add(admin);
            }
        }
        /// <summary>
        /// Toma el nombre y devuelve una instancia de Admin
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public Admin AdminSegunNombre(string nombre)
        {
            Admin admin = ListaAdmin.Find(x => x.Nombre == nombre);
            return admin;
        }
    }
}