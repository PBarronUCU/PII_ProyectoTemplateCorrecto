using System;

namespace Library
{
    public class AdminFachada
    {
        private static Admin adminActual; // Guarda la instancia del administrador que está usando el sistema.

        // Inicializa el administrador con el nombre indicado.
        public static void InicializarAdmin(string name)
        {
            adminActual = new Admin(name);
            
        }

        // Crea un nuevo usuario a través del administrador actual.
        public static void CrearUsuario(string name, string apellido, string correo)
        {
            if (adminActual == null)
            {
                Console.WriteLine("Debe inicializar un administrador antes de crear usuarios.");
                
            }
            adminActual.AgregarUsuario(name, apellido, correo);
            
        }

        // Suspende un usuario existente a través del administrador actual.
        public static void SuspenderUsuario(string correo)
        {
            if (adminActual == null)
            {
                Console.WriteLine("Debe inicializar un administrador antes de suspender usuarios.");
                return;
            }
            adminActual.SuspenderUsuario(correo);
        }
    }
}
