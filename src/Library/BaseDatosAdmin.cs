using System.Collections.Generic;

namespace Library
{
    
    // ============================================================
    // GRASP Y SOLID UTILIZADOS EN ESTA CLASE
    //
    // ► GRASP
    // • Singleton: La clase garantiza una única instancia accesible por Instance.
    // • Information Expert: Es la experta en manejar objetos Admin
    //   (buscar, agregar, verificar existencia).

    // • Low Coupling: Solo depende del tipo Admin, sin otras dependencias fuertes.

    // • High Cohesion: Todas sus operaciones son coherentes con su única
    //   responsabilidad: administrar administradores.
    //
    // ► SOLID
    // • S — Single Responsibility Principle:
    //   La clase solo gestiona admins, cumple con SRP.
    //
    // • O — Open/Closed Principle:
    //   Parcialmente cumplido; es extensible pero agregar nuevas reglas requiere 
    //   modificar la clase.
    //
    // • D — Dependency Inversion Principle:
    //   Tiene dependencia concreta hacia Admin, no viola DIP pero tampoco lo aplica
    //   explícitamente.
    //
    // ============================================================
    // PATRONES
    //
    // ► Singleton
    //   Implementado con constructor privado + instancia estática + propiedad Instance.
    //
    // ============================================================
    
    /// <summary>
    /// Base de datos para los Admins
    /// </summary>
    public sealed class BaseDatosAdmin
    {
        private static readonly BaseDatosAdmin _instance = new BaseDatosAdmin();
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