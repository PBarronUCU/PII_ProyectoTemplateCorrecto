using System;
using System.Collections.Generic;

namespace Library

// ============================================================
    // GRASP Y SOLID UTILIZADOS EN ESTA CLASE
    //
    // ► GRASP
    // • Singleton: La clase crea una única instancia accesible via Instance.
    // • Information Expert: La clase es experta en usuarios; por eso 
    //   implementa búsqueda, creación, eliminación, verificación.
    // • Low Coupling: Tiene una mínima dependencia hacia BaseDatosCliente.
    // • High Cohesion: Todas sus operaciones se centran en gestionar usuarios.
    //
    // ► SOLID
    // • S — Single Responsibility Principle:
    //   Administra exclusivamente Usuarios.
    //
    // • O — Open/Closed Principle:
    //   Parcialmente cumplido. Puede extenderse, pero requiere modificaciones internas.
    //
    // ============================================================
    // PATRONES
    //
    // ► Singleton (APLICA)
    //   Implementado: constructor privado + instancia estática + propiedad Instance.
    //
    // ► Facade (APLICA parcialmente)
    //   La clase sirve como una interfaz simple para gestionar usuarios,
    //   ocultando detalles de almacenamiento interno.
    //
    // ► Iterator 
    //   El foreach sobre ListaUsuario utiliza iteradores de C#.
    //
    // ============================================================

{
    /// <summary>
    /// Base de datos para los Usuarios
    /// </summary>
    public sealed class BaseDatosUsuario
    {
        private static readonly BaseDatosUsuario _instance = new BaseDatosUsuario();
        /// <summary>
        /// Donde se guardan los Usuarios
        /// </summary>
        public List<Usuario> ListaUsuario = new List<Usuario>();
        
        
        private BaseDatosUsuario()
        {
        }
        /// <summary>
        /// Usar este metodo para referirse siempre a la misma instancia de esta clase
        /// </summary>
        public static BaseDatosUsuario Instance
        {
            get { return _instance; }
        }
        
        
        
        
        /// <summary>
        /// Recorre todos los clientes guardados. Compara el correo de cada uno con el parametro, devuelve true si uno coincide.
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public bool ExisteCorreoUser(string correo)
        {
            bool result = false;
            foreach (Usuario user in ListaUsuario)
            {
                if (user.Correo == correo)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// Recorre todos los CLIENTES y USUARIOS guardados. Compara el correo de cada uno con el parametro, devuelve true si uno coincide.
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public bool ExisteCorreo(string correo)
        {
            BaseDatosCliente bd1 = BaseDatosCliente.Instance;
            bool resultcliente = bd1.ExisteCorreoCliente(correo);
            bool resultusuario = ExisteCorreoUser(correo);
            return resultusuario | resultcliente;
        }
        
        /// <summary>
        /// Agrega un Usuario a la lista. Solo lo agrega si no existe ya el correo.
        /// </summary>
        /// <param name="user"></param>
        public void AgregarUsuario(Usuario user)
        {
            string correo = user.Correo;
            if (!ExisteCorreo(correo))
            {
                ListaUsuario.Add(user);
            }
            else
            {
                throw new ArgumentException("El correo ya esta ocupado");
            }
            
        }
        /// <summary>
        /// Toma el correo y devuelve una instancia de Usuario
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public Usuario UsuarioSegunCorreo(string correo)
        {
            Usuario usu = ListaUsuario.Find(x => x.Correo == correo);
            return usu;
        }
        /// <summary>
        /// Elimina un Usuario de la lista
        /// </summary>
        /// <param name="correo"></param>
        public void EliminarUsuario(string correo)
        {
            Usuario usu = UsuarioSegunCorreo(correo);
            ListaUsuario.Remove(usu);
        }
        /// <summary>
        /// Metodo para crear usuario. Si ya existe el correo salta exepcion
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        public void CrearUsuario(string nombre,string apellido, string correo)
        {
            Usuario user = new Usuario(nombre, apellido, correo);
            Instance.AgregarUsuario(user);
        }
        
    }

}
