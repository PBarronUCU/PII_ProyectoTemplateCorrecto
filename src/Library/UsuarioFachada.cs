using System;
using System.Collections.Generic;

namespace Library
{
    public class UsuarioFachada
    {   
        // Instancia única de la base de datos de usuarios.
        BaseDatosUsuario bd = BaseDatosUsuario.Instance;

        // Permite al usuario identificado por su correo crear un nuevo cliente.
        public void CrearCliente(string correoUsuario, string nombre, string apellido, int tel, string correo)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            u?.CrearCliente(nombre, apellido, tel, correo);
        }

        // Permite al usuario crear una venta asociada a un cliente.
        public void CrearVenta(string correoUsuario, int telcliente, string prod, double precio, string fecha)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            if (u != null)
            {
                DateTime f = DateTime.Parse(fecha);
                u.CrearVentas(telcliente, prod, precio, f);
            }
        }

        // Permite al usuario crear una cotización asociada a un cliente.
        public void CrearCoti(string correoUsuario, int telcliente, string fecha, double valor, string imp)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            if (u != null)
            {
                DateTime f = DateTime.Parse(fecha);
                u.CrearCoti(telcliente, f, valor, imp);
            }
        }

        // Modifica los datos de un cliente existente en la cartera del usuario.
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

        // Devuelve un cliente específico según su correo electrónico.
        public Cliente FiltarClienteCorreo(string correoUsuario, string correoCliente)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.FiltrarClienteCorreo(correoCliente);
        }

        // Retorna todas las interacciones de un cliente sin aplicar filtros.
        public List<IInteracion> InteracionClienteSinFiltro(string correoUsuario, int telcliente)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.InteracionClienteSinFiltro(telcliente);
        }

        // Retorna las interacciones de un cliente filtradas por fecha.
        public List<IInteracion> InteracionClienteFiltroFecha(string correoUsuario, int telcliente, string fecha)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            DateTime f = DateTime.Parse(fecha);
            return u?.InteracionClienteFiltroFecha(telcliente, f);
        }

        // Retorna las interacciones de un cliente filtradas por tipo.
        public List<IInteracion> InteracionClienteFiltroTipo(string correoUsuario, int telcliente, string tipo)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.InteracionClienteFiltroTipo(telcliente, tipo);
        }

        // Retorna las interacciones de un cliente filtradas por tipo y fecha.
        public List<IInteracion> InteracionClienteFiltroTipoFecha(string correoUsuario, int telcliente, string tipo, string fecha)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            DateTime f = DateTime.Parse(fecha);
            return u?.InteracionClienteFiltroTipoFecha(telcliente, tipo, f);
        }

        // Retorna las interacciones de tipo diálogo que aún no han sido respondidas
        public List<IInteracionDialogo> InteraSinResponder(string correoUsuario)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.IntreaSinResponder();
        }

        // Retorna las interacciones consideradas viejas o inactivas.
        public List<IInteracion> InteraViejas(string correoUsuario)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u?.InteraViejas();
        }

        // Retorna las ventas realizadas en un período determinado.
        public List<Venta> VentasPeriodo(string correoUsuario, string fechabaja, string fechaalta)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            DateTime baja = DateTime.Parse(fechabaja);
            DateTime alta = DateTime.Parse(fechaalta);
            return u?.VentasPeriodo(baja, alta);
        }

        // Agrega una nueva interacción al usuario.
        public void AgregarInteracion(string correoUsuario, IInteracion interacion)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            u?.AgregarInteracion(interacion);
        }

        // Devuelve la información general del panel del usuario.
        public string PanelCliente(string correoUsuario)
        {
            Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
            return u != null ? u.PanelCliente() : "Usuario no encontrado";
        }
    }
}
