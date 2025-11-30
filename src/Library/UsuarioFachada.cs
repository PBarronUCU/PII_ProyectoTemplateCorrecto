using System;
using System.Collections.Generic;

namespace Library
{

    public sealed class UsuarioFachada
    {
        // Instancia única de la base de datos de usuarios.

        /// <summary>
        /// Esta clase se encarga de ejecturar los metodos de admin
        /// </summary>
        
        
            private static readonly UsuarioFachada _instance = new UsuarioFachada();

            BaseDatosUsuario bd = BaseDatosUsuario.Instance;
            BaseDatosVenta bdventa = BaseDatosVenta.Instance;
            BaseDatosCliente bdcliente = BaseDatosCliente.Instance;

            private UsuarioFachada()
            {

            }

            /// <summary>
            /// Usar este metodo para referirse siempre a la misma instancia de esta clase
            /// </summary>
            public static UsuarioFachada Instance
            {
                get { return _instance; }
            }

            /// <summary>
            ///  Permite al usuario identificado por su correo crear un nuevo cliente.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="nombre"></param>
            /// <param name="apellido"></param>
            /// <param name="tel"></param>
            /// <param name="correo"></param>
            public void CrearCliente(string correoUsuario, string nombre, string apellido, int tel, string correo)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (!u.Suspendido)
                {
                    Cliente client = new Cliente(nombre, apellido, correo, tel);
                    u.AgregarClienteACartera(client);
                }
                else
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

            }

            /// <summary>
            ///  Permite al usuario crear una venta asociada a un cliente.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="telcliente"></param>
            /// <param name="prod"></param>
            /// <param name="precio"></param>
            /// <param name="fecha"></param>
            /// <exception cref="ArgumentException"></exception>
            public void CrearVenta(string correoUsuario, int telcliente, string prod, double precio, string fecha)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (!u.Suspendido)
                {
                    if (u != null)
                    {
                        DateTime f = DateTime.Parse(fecha);
                        u.CrearVentas(telcliente, prod, precio, f);
                    }
                }
                else
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }
            }

            /// <summary>
            ///  Permite al usuario crear una cotización asociada a un cliente.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="telcliente"></param>
            /// <param name="fecha"></param>
            /// <param name="valor"></param>
            /// <param name="imp"></param>
            /// <exception cref="ArgumentException"></exception>
            public void CrearCoti(string correoUsuario, int telcliente, string fecha, double valor, string imp)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                if (u != null)
                {
                    DateTime f = DateTime.Parse(fecha);
                    u.CrearCoti(telcliente, f, valor, imp);
                }

            }

            /// <summary>
            ///  Modifica los datos de un cliente existente en la cartera del usuario.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="nombre"></param>
            /// <param name="apellido"></param>
            /// <param name="tel"></param>
            /// <param name="gen"></param>
            /// <param name="fechanac"></param>
            /// <exception cref="ArgumentException"></exception>
            public void ModificarCliente(string correoUsuario, string nombre, string apellido, int tel, string gen,
                string fechanac)
            {

                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                if (u != null)
                {
                    Genero genero = Enum.Parse<Genero>(gen);
                    DateTime fecha = DateTime.Parse(fechanac);
                    bdcliente.ModificarCliente(nombre, apellido, tel, genero, fecha);
                }
            }

            /// <summary>
            ///  Devuelve un cliente específico según su correo electrónico.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="correoCliente"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public Cliente FiltarClienteCorreo(string correoUsuario, string correoCliente)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                return u?.FiltrarClienteCorreo(correoCliente);
            }

            /// <summary>
            ///  Retorna todas las interacciones de un cliente sin aplicar filtros.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="telcliente"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public List<IInteracion> InteracionClienteSinFiltro(string correoUsuario, int telcliente)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                return u?.InteracionClienteSinFiltro(telcliente);
            }

            /// <summary>
            ///  Retorna las interacciones de un cliente filtradas por fecha.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="telcliente"></param>
            /// <param name="fecha"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public List<IInteracion> InteracionClienteFiltroFecha(string correoUsuario, int telcliente, string fecha)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                DateTime f = DateTime.Parse(fecha);
                return u?.InteracionClienteFiltroFecha(telcliente, f);
            }

            /// <summary>
            ///  Retorna las interacciones de un cliente filtradas por tipo.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="telcliente"></param>
            /// <param name="tipo"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public List<IInteracion> InteracionClienteFiltroTipo(string correoUsuario, int telcliente, string tipo)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                return u?.InteracionClienteFiltroTipo(telcliente, tipo);
            }

            /// <summary>
            ///  Retorna las interacciones de un cliente filtradas por tipo y fecha.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="telcliente"></param>
            /// <param name="tipo"></param>
            /// <param name="fecha"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public List<IInteracion> InteracionClienteFiltroTipoFecha(string correoUsuario, int telcliente, string tipo,
                string fecha)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                DateTime f = DateTime.Parse(fecha);
                return u?.InteracionClienteFiltroTipoFecha(telcliente, tipo, f);
            }

            /// <summary>
            ///  Retorna las interacciones de tipo diálogo que aún no han sido respondidas
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public List<IInteracionDialogo> InteraSinResponder(string correoUsuario)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                return u?.IntreaSinResponder();
            }

            /// <summary>
            ///  Retorna las interacciones consideradas viejas o inactivas.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public List<IInteracion> InteraViejas(string correoUsuario)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                return u?.InteraViejas();
            }

            /// <summary>
            ///  Retorna las ventas realizadas en un período determinado
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="fechabaja"></param>
            /// <param name="fechaalta"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public List<Venta> VentasPeriodo(string correoUsuario, string fechabaja, string fechaalta)
            {

                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                DateTime baja = DateTime.Parse(fechabaja);
                DateTime alta = DateTime.Parse(fechaalta);
                return bdventa.VentasPeriodo(u, baja, alta);
            }

            /// <summary>
            /// Agrega una nueva interacción al usuario.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <param name="interacion"></param>
            /// <exception cref="ArgumentException"></exception>
            public void AgregarInteracion(string correoUsuario, IInteracion interacion)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                u?.AgregarInteracion(interacion);
            }

            /// <summary>
            ///  Devuelve la información general del panel del usuario.
            /// </summary>
            /// <param name="correoUsuario"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public string PanelCliente(string correoUsuario)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                return u != null ? u.PanelCliente() : "Usuario no encontrado";
            }

            public void EliminarCliente(int telefono)
            {
                BaseDatosCliente bd = BaseDatosCliente.Instance;
                bd.EliminarCliente(telefono);
            }
        }
    }

