using System;
using System.Collections.Generic;

namespace Library

{
    public sealed class BaseDatosUsuario
    {
        private static readonly BaseDatosUsuario _instance = new BaseDatosUsuario();
        public List<Usuario> ListaUsuario = new List<Usuario>();
        
        
        private BaseDatosUsuario()
        {
        }
        
        public static BaseDatosUsuario Instance
        {
            get { return _instance; }
        }
        
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
        
        public bool ExisteCorreo(string correo)
        {
            BaseDatosCliente bd1 = BaseDatosCliente.Instance;
            bool resultcliente = bd1.ExisteCorreoCliente(correo);
            bool resultusuario = ExisteCorreoUser(correo);
            return resultusuario | resultcliente;
        }
        
        
        
        
        public void AgregarUsuario(Usuario user)
        {
            string correo = user.Correo;
            if (!ExisteCorreo(correo))
            {
                ListaUsuario.Add(user);
            }
            
        }
        
        public Usuario UsuarioSegunCorreo(string correo)
        {
            Usuario usu = ListaUsuario.Find(x => x.Correo == correo);
            return usu;
        }
        
        public void EliminarUsuario(string correo)
        {
            Usuario usu = UsuarioSegunCorreo(correo);
            ListaUsuario.Remove(usu);
        }
    }

}
