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
        
        
            private static UsuarioFachada _instance = new UsuarioFachada();

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
            /// Metodo para poder reiniciar mis singletons despues de cada test
            /// </summary>
            public static void ResetInstance()
            {
                _instance = new UsuarioFachada();
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
                if (u == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
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
            /// <param name="tema"></param>
            /// <param name="notas"></param>
            /// <param name="fecha"></param>
            /// <param name="telcliente"></param>
            /// <param name="precio"></param>
            /// <param name="producto"></param>
            /// <exception cref="ArgumentException"></exception>
            public void CrearVenta(string correoUsuario, string tema, string notas, string fecha, int telcliente, Double precio, string producto)
            {
                Usuario u = bd.UsuarioSegunCorreo(correoUsuario);
                Cliente client = bdcliente.ClienteSegunTelefono(telcliente);
                if (!u.Suspendido)
                {
                    if (u != null)
                    {
                        DateTime f = DateTime.Parse(fecha);
                        new Venta(u,tema,notas,f,client,precio,producto);
                        
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
            /// <param name="correouser"></param>
            /// <param name="tema"></param>
            /// <param name="notas"></param>
            /// <param name="fecha"></param>
            /// <param name="telcliente"></param>
            /// <param name="valor"></param>
            /// <param name="producto"></param>
            /// <exception cref="ArgumentException"></exception>
            public void CrearCoti(string correouser,string tema,string notas,string fecha,int telcliente,double valor,string producto)
            {
                Usuario u = bd.UsuarioSegunCorreo(correouser);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                Cliente client = bdcliente.ClienteSegunTelefono(telcliente);
                if (u != null)
                {
                    DateTime f = DateTime.Parse(fecha);
                    Cotizacion coti = new Cotizacion(tema,notas,f,client,valor,producto);
                    u.AgregarCotizacion(coti);
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
                Cliente client = bdcliente.ClienteSegunTelefono(telcliente);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                return u?.InteracionClienteSinFiltro(client);
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
                Cliente client = bdcliente.ClienteSegunTelefono(telcliente);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                DateTime f = DateTime.Parse(fecha);
                return u?.InteracionClienteFiltroFecha(client, f);
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
                Cliente client = bdcliente.ClienteSegunTelefono(telcliente);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                return u?.InteracionClienteFiltroTipo(client, tipo);
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
                Cliente client = bdcliente.ClienteSegunTelefono(telcliente);
                if (u.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }

                DateTime f = DateTime.Parse(fecha);
                return u?.InteracionClienteFiltroTipoFecha(client, tipo, f);
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
            /// Elimina de la base de datos a un cliente ya existente
            /// </summary>
            /// <param name="telefono"></param>
        

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
            
            /// <summary>
            /// Elimina un cliente de la lista. Tambien busca en la base de datos de usuario
            /// para eliminar al cliente de la cartera del usuario que lo tenga guardado
            /// </summary>
            /// <param name="telefono"></param>
            public void EliminarCliente(int telefono)
            {
                BaseDatosCliente bd = BaseDatosCliente.Instance;
                bd.EliminarCliente(telefono);
            }
            /// <summary>
            /// Crea un reunion y la agrega al usuario indicado por el correo.
            /// </summary>
            /// <param name="correouser"></param>
            /// <param name="fecha"></param>
            /// <param name="tema"></param>
            /// <param name="notas"></param>
            /// <param name="telcliente"></param>
            /// <param name="lugar"></param>
            public void CrearReunion(string correouser,string fecha, string tema, string notas, int telcliente, string lugar)
            {
                Usuario user = bd.UsuarioSegunCorreo(correouser);
                if (user.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }
                Cliente cliente = bdcliente.ClienteSegunTelefono(telcliente);
                
                DateTime f = DateTime.Parse(fecha);
                Reunion reunion = new Reunion(tema,notas,f,cliente,lugar);
                user.AgregarInteracion(reunion);
            }
            /// <summary>
            /// Crea un correo y la asigna al usuario inicado por el correo
            /// </summary>
            /// <param name="correouser"></param>
            /// <param name="remitente"></param>
            /// <param name="fecha"></param>
            /// <param name="tema"></param>
            /// <param name="telcliente"></param>
            /// <param name="notas"></param>
            /// <param name="respondido"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public void CrearCorreo(string correouser,string remitente, string fecha, string tema, int telcliente, string notas, bool respondido)
            {
                Usuario user = bd.UsuarioSegunCorreo(correouser);
                if (user.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }
                Cliente cliente = bdcliente.ClienteSegunTelefono(telcliente);

                DateTime f = DateTime.Parse(fecha);
                UsuarioOCliente rem = Enum.Parse<UsuarioOCliente>(remitente);
                CorreoElectronico correo = new CorreoElectronico(rem,tema,notas,f,cliente,respondido);
                user.AgregarInteracion(correo);

            }
            /// <summary>
            /// Crea una llamada y la asigna al usuario indicado por el correo
            /// </summary>
            /// <param name="correouser"></param>
            /// <param name="remitente"></param>
            /// <param name="fecha"></param>
            /// <param name="tema"></param>
            /// <param name="telcliente"></param>
            /// <param name="notas"></param>
            /// <param name="respondido"></param>
            /// <returns></returns>
            public void CrearLlamada(string correouser,string remitente, string fecha, string tema, int telcliente, string notas, bool respondido)
            {
                Usuario user = bd.UsuarioSegunCorreo(correouser);
                if (user.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }
                Cliente cliente = bdcliente.ClienteSegunTelefono(telcliente);
                
                DateTime f = DateTime.Parse(fecha);
                UsuarioOCliente rem = Enum.Parse<UsuarioOCliente>(remitente);
                Llamada llamada = new Llamada(rem, tema, notas, f, cliente,respondido);
                user.AgregarInteracion(llamada);
            }
            /// <summary>
            /// Crea un mensaje y lo asigna al usuario indicado al correo
            /// </summary>
            /// <param name="correouser"></param>
            /// <param name="remitente"></param>
            /// <param name="fecha"></param>
            /// <param name="tema"></param>
            /// <param name="telcliente"></param>
            /// <param name="notas"></param>
            /// <param name="respondido"></param>
            /// <returns></returns>
            public void CrearMensaje(string correouser,string remitente, string fecha, string tema, int telcliente, string notas, bool respondido)
            {
                Usuario user = bd.UsuarioSegunCorreo(correouser);
                if (user.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }
                Cliente cliente = bdcliente.ClienteSegunTelefono(telcliente);
                
                DateTime f = DateTime.Parse(fecha);
                UsuarioOCliente rem = Enum.Parse<UsuarioOCliente>(remitente);
                Mensaje mensaje = new Mensaje(rem, tema, notas, f, cliente,respondido);
                user.AgregarInteracion(mensaje);
            }
            /// <summary>
            /// Busca los clientes del Usuario especificado por su Nombre y Apellido devolviendo todos los que tengan ese Nombre y Apelledo
            /// </summary>
            /// <param name="correouser"></param>
            /// <param name="nombre"></param>
            /// <param name="apellido"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public List<Cliente> FiltrarClienteNombre(string correouser,string nombre, string apellido)
            {
                Usuario user = bd.UsuarioSegunCorreo(correouser);
                if (user.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }
                return user.FiltrarClienteNom(nombre,apellido);
            }
            /// <summary>
            /// Filtra a los clientes del usuario indicado por su numero de Telefono por lo que devolvera el cliente que tenga ese Telefono
            /// </summary>
            /// <param name="correouser"></param>
            /// <param name="telefono"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public Cliente FiltrarClienteTelefono(string correouser, int telefono)
            {
                Usuario user = bd.UsuarioSegunCorreo(correouser);
                if (user.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }
                return user.FiltrarClienteTel(telefono);
            }
            /// <summary>
            /// Busca una interaccion con un Cliente, fecha y tipo de imnteraccion en especifico  para implementarle una nota a la interaccion buscada
            /// </summary>
            /// <param name="correouser"></param>
            /// <param name="telefono"></param>
            /// <param name="fecha"></param>
            /// <param name="tipo"></param>
            /// <param name="nota"></param>
            /// <exception cref="ArgumentException"></exception>
            public void AgregarNota(string correouser,int telcliente, string fecha, string tipo, string nota)
            {
                Usuario user = bd.UsuarioSegunCorreo(correouser);
                Cliente client = bdcliente.ClienteSegunTelefono(telcliente);
                if (user.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }
                DateTime f = DateTime.Parse(fecha);
                user.AgregarNota(client,f,tipo,nota);
                
                
            }
            /// <summary>
            /// Le permite al usuario asignar uno de sus cliente a uno de sus compañeros para que puedo trabajar con el
            /// el primer correo es el correo del usuario que quiere asignar a alguien.
            /// El segundo es el correo del usuario que va a ser asignado
            /// </summary>
            /// <param name="correouserpropio"></param>
            /// <param name="correouserobjetivo"></param>
            /// <param name="telcliente"></param>
            /// <exception cref="ArgumentException"></exception>
            public void AsignarCliente(string correouserpropio,string correouserobjetivo, int telcliente)
            {
                Usuario user = bd.UsuarioSegunCorreo(correouserpropio);
                if (user.Suspendido)
                {
                    throw new ArgumentException("El usuario esta suspendido");
                }
                user.AsignarCliente(telcliente,correouserobjetivo);
            }
        }
    }

