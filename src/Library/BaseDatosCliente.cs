using System;
using System.Collections.Generic;

namespace Library

{
    /// <summary>
    /// Base de datos para los Clientes
    /// </summary>
    public sealed class BaseDatosCliente
    {
        private static BaseDatosCliente _instance = new BaseDatosCliente();
        /// <summary>
        /// Donde se guardan los clientes
        /// </summary>
        public List<Cliente> ListaCliente = new List<Cliente>();
        
        
        private BaseDatosCliente()
        {
        }
        /// <summary>
        /// Usar este metodo para referirse siempre a la misma instancia de esta clase
        /// </summary>
        public static BaseDatosCliente Instance
        {
            get { return _instance; }
        }
        
        /// <summary>
        /// Metodo para poder reiniciar mis singletons despues de cada test
        /// </summary>
        public static void ResetInstance()
        {
            _instance = new BaseDatosCliente();
        }
        
        /// <summary>
        /// Recorre todos los clientes guardados. Compara el telefonos de cada uno con el parametro, devuelve true si uno coincide.
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
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

        public void CrearCliente(string nombre, string apellido, string correo, int tel)
        {
            Cliente client = new Cliente(nombre, apellido, correo, tel);
            BaseDatosCliente.Instance.AgregarCliente(client);
        }
        
        /// <summary>
        /// Recorre todos los clientes guardados. Compara el correo de cada uno con el parametro, devuelve true si uno coincide.
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Recorre todos los CLIENTES y USUARIOS guardados. Compara el correo de cada uno con el parametro, devuelve true si uno coincide.
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public bool ExisteCorreo(string correo)
        {
            BaseDatosUsuario bd1 = BaseDatosUsuario.Instance;
            bool resultcliente = ExisteCorreoCliente(correo);
            bool resultusuario = bd1.ExisteCorreoUser(correo);
            return resultusuario | resultcliente;
        }
        /// <summary>
        /// Agrega un cliente a la lista. Solo lo agrega si no existe ya el correo ni telefono.
        /// </summary>
        /// <param name="client"></param>
        public void AgregarCliente(Cliente client)
        {
            string correo = client.Correo;
            int tel = client.Tel;
            if (!ExisteCorreo(correo) & !ExisteTel(tel))
            {
                ListaCliente.Add(client);
            }
            else
            {
                throw new ArgumentException("Correo o Telefono ya ocupado");
            }
        }
        /// <summary>
        /// Toma el telefono y devuelve una instancia de CLiente
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        public Cliente ClienteSegunTelefono(int telefono)
        {
            Cliente cliente = ListaCliente.Find(x => x.Tel == telefono);
            return cliente;
        }
        /// <summary>
        /// Elimina un cliente de la lista. Tambien busca en la base de datos de usuario
        /// para eliminar al cliente de la cartera del usuario que lo tenga guardado
        /// </summary>
        /// <param name="telefono"></param>
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
        
        /// <summary>
        /// El telefono es para identificar el Cliente, NO para cambiarlo.
        /// </summary>
        public void ModificarCliente(string nombre,string apell, int tel, Genero genero,DateTime fecha )
        {
            Cliente client = ClienteSegunTelefono(tel);
            client.Nombre = nombre;
            client.Apellido = apell;
            client.Genero = genero;
            client.FechaNac =  fecha;
            
        }
    }
}