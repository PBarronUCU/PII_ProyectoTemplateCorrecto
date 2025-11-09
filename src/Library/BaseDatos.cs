using System;
using System.Collections.Generic;

namespace Library

{
    public sealed class BaseDatos
    {
        private static readonly BaseDatos _instance = new BaseDatos();
        public List<Usuario> ListaUsuario = new List<Usuario>();
        public List<Ventas> ListaVentas = new List<Ventas>();
        public List<Cliente> ListaCliente = new List<Cliente>();
        
        
        private BaseDatos()
        {
        }
        
        public static BaseDatos Instance
        {
            get { return _instance; }
        }

        public bool ExisteCorreo(string correo)
        {
            bool result = false;
            foreach (Usuario user in ListaUsuario)
            {
                if (user.Correo == correo)
                {
                    result = true;
                }
            }
            foreach (Cliente client in ListaCliente)
            {
                if (client.Correo == correo)
                {
                    result = true;
                }
            }
            return result;
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

        public void AgregarUsuario(Usuario user)
        {
            string correo = user.Correo;
            if (!ExisteCorreo(correo))
            {
                ListaUsuario.Add(user);
            }
            
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

        public bool ExisteVenta(Usuario user, Cliente cliente, string producto, double precio, DateTime fecha)
        {
            bool resultado = false;
            foreach (Ventas venta in ListaVentas)
            {
                if (venta.Usuario==user & venta.Cliente==cliente & venta.Producto==producto & venta.Precio==precio & venta.FechaVenta==fecha)
                {
                    resultado =  true;
                }
            }
            return resultado;
        }

        public void AgregarVenta(Ventas venta)
        {
            Usuario usu = venta.Usuario;
            DateTime fecha = venta.FechaVenta;
            Cliente cliente = venta.Cliente;
            string producto = venta.Producto;
            double precio = venta.Precio;
            if (!ExisteVenta(usu,cliente, producto, precio, fecha))
            {
                ListaVentas.Add(venta);
            }
        }

        public Usuario UsuarioSegunCorreo(string correo)
        {
            Usuario usu = ListaUsuario.Find(x => x.Correo == correo);
            return usu;
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
            foreach (Usuario usu in ListaUsuario)
            {
                if (usu.VerificarTelCartera(telefono))
                {
                    usu.EliminarTelCartera(telefono);
                }
            }
        }

        public void EliminarUsuario(string correo)
        {
            Usuario usu = UsuarioSegunCorreo(correo);
            ListaUsuario.Remove(usu);
        }


    }
}