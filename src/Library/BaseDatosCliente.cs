using System;
using System.Collections.Generic;

namespace Library

{
    public sealed class BaseDatosCliente
    {
        private static readonly BaseDatosCliente _instance = new BaseDatosCliente();
        public List<Cliente> ListaCliente = new List<Cliente>();
        
        
        private BaseDatosCliente()
        {
        }
        
        public static BaseDatosCliente Instance
        {
            get { return _instance; }
        }
        
        public bool ExisteTel(int tel)
        {
            bool result = false;
            foreach (Cliente client in ListaCliente)
            {
                if (client.Tel == tel)
                {
                    result = true;
                }
            }
            return result;
        }
        
        public bool ExisteCorreoCliente(string correo)
        {
            bool result = false;
            foreach (Cliente client in ListaCliente)
            {
                if (client.Correo == correo)
                {
                    result = true;
                }
            }
            return result;
        }
        
        public bool ExisteCorreo(string correo)
        {
            BaseDatosUsuario bd1 = BaseDatosUsuario.Instance;
            bool resultcliente = ExisteCorreoCliente(correo);
            bool resultusuario = bd1.ExisteCorreoUser(correo);
            return resultusuario | resultcliente;
        }
        
        public void AgregarCliente(Cliente client)
        {
            string correo = client.Correo;
            int tel = client.Tel;
            if (!ExisteCorreo(correo) & !ExisteTel(tel))
            {
                ListaCliente.Add(client);
            }
        }
        
        public Cliente ClienteSegunTelefono(int telefono)
        {
            Cliente cliente = ListaCliente.Find(x => x.Tel == telefono);
            return cliente;
        }
        
        public void EliminarCliente(int telefono)
        {
            Cliente cliente = ClienteSegunTelefono(telefono);
            ListaCliente.Remove(cliente);
            BaseDatosUsuario bd1 = BaseDatosUsuario.Instance;
            
            foreach (Usuario usu in bd1.ListaUsuario)
            {
                if (usu.VerificarTelCartera(telefono))
                {
                    usu.EliminarTelCartera(telefono);
                }
            }
        }
        
    }
}