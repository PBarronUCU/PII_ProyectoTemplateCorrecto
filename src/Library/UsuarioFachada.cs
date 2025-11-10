using System;
using System.Collections.Generic;

namespace Library
{
    public class UsuarioFachada
    {
        BaseDatosUsuario bd = BaseDatosUsuario.Instance;

        public void CrearCliente(string correoUsuario, string nombre, string apellido, int tel, string correo)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            u?.CrearCliente(nombre, apellido, tel, correo);
        }

        public void CrearVenta(string correoUsuario, int telcliente, string prod, double precio, string fecha)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            if (u != null)
            {
                DateTime f = DateTime.Parse(fecha);
                u.CrearVentas(telcliente, prod, precio, f);
            }
        }

        public void CrearCoti(string correoUsuario, int telcliente, string fecha, double valor, string imp)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            if (u != null)
            {
                DateTime f = DateTime.Parse(fecha);
                u.CrearCoti(telcliente, f, valor, imp);
            }
        }

        public void ModificarCliente(string correoUsuario, string nombre, string apellido, int tel, string gen, string fechanac)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            if (u != null)
            {
                Genero genero = Enum.Parse<Genero>(gen);
                DateTime fecha = DateTime.Parse(fechanac);
                u.ModificarCliente(nombre, apellido, tel, genero, fecha);
            }
        }

        public Cliente FiltarClienteCorreo(string correoUsuario, string correoCliente)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.FiltrarClienteCorreo(correoCliente);
        }

        public List<IInteracion> InteracionClienteSinFiltro(string correoUsuario, int telcliente)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.InteracionClienteSinFiltro(telcliente);
        }

        public List<IInteracion> InteracionClienteFiltroFecha(string correoUsuario, int telcliente, string fecha)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            DateTime f = DateTime.Parse(fecha);
            return u?.InteracionClienteFiltroFecha(telcliente, f);
        }

        public List<IInteracion> InteracionClienteFiltroTipo(string correoUsuario, int telcliente, string tipo)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.InteracionClienteFiltroTipo(telcliente, tipo);
        }

        public List<IInteracion> InteracionClienteFiltroTipoFecha(string correoUsuario, int telcliente, string tipo, string fecha)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            DateTime f = DateTime.Parse(fecha);
            return u?.InteracionClienteFiltroTipoFecha(telcliente, tipo, f);
        }

        public List<IInteracionDialogo> InteraSinResponder(string correoUsuario)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.IntreaSinResponder();
        }

        public List<IInteracion> InteraViejas(string correoUsuario)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.InteraViejas();
        }

        public List<Venta> VentasPeriodo(string correoUsuario, string fechabaja, string fechaalta)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            DateTime baja = DateTime.Parse(fechabaja);
            DateTime alta = DateTime.Parse(fechaalta);
            return u?.VentasPeriodo(baja, alta);
        }

        public void AgregarInteracion(string correoUsuario, IInteracion interacion)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            u?.AgregarInteracion(interacion);
        }

        public string PanelCliente(string correoUsuario)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u != null ? u.PanelCliente() : "Usuario no encontrado";
        }
    }
}
