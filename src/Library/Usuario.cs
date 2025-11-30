using System;
using System.Collections.Generic;using System.Collections.ObjectModel;
using System.Linq;

namespace Library


{/// <summary>
 /// Clase Usuario
 /// </summary>
    public class Usuario
    {
        /// <summary>
        /// ingresa el Nombre de pila del Usuario
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Apellido { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Correo { get; }
        /// <summary>
        /// Lista de Clientes Guardados 
        /// </summary>
        public List<Cliente> Cartera = new List<Cliente>();
        /// <summary>
        /// Guarda las Oportunidades de Venta que realisa el Usuario
        /// </summary>
        public List<Cotizacion> OportunidadesVentas = new List<Cotizacion>();
        /// <summary>
        /// Guarda las Interacioones del Usuario
        /// </summary>
        public List<IInteracion> ListaInteracciones = new List<IInteracion>();//Leimos la Letra siempre desde el punto de vista del Usuario=Vendedor.
        /// <summary>
        /// Metodo para suspender a un Usuario
        /// </summary>
        public bool Suspendido { get; private set; }                        
        /// <summary>
        /// Constructor de clase Usuario. Si el correo no contiene una @ o ya esta en uso, tira una excpecion 
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        public Usuario(string nombre, string apellido, string correo)
        {
            if (!correo.Contains("@"))
            {
                throw new ArgumentException("El correo no es valido");
            }
            BaseDatosUsuario bd1 = BaseDatosUsuario.Instance;
            if (!bd1.ExisteCorreo(correo))
            {
                Nombre = nombre;
                Apellido = apellido;
                Correo = correo;
                Suspendido = false;
                bd1.AgregarUsuario(this);
            }
            else
            {
                throw new ArgumentException("El correo ya esta ocupado");
            }
        }
        
        
        
        /// <summary>
        /// Metodo para suspender al usuario. Si el usuario ya esta suspendido tira una exepcion
        /// </summary>
        public void Suspender()
        {
            if (!Suspendido)
            {
                Suspendido = true;
            }
            else
            {
                throw new Exception("Este usuario ya esta suspendido");
            }
            
        }
        /// <summary>
        /// Registra una venta hecha entre un Usuario y su Clinte 
        /// </summary>
        /// <param name="tel"></param>
        /// <param name="prod"></param>
        /// <param name="precio"></param>
        /// <param name="fecha"></param>
        public void CrearVentas(int tel,string prod,Double precio,DateTime fecha)

        {
            BaseDatosVenta bd1 = BaseDatosVenta.Instance;
            BaseDatosCliente bd2 = BaseDatosCliente.Instance;
            Cliente client = bd2.ClienteSegunTelefono(tel);
            if (!bd1.ExisteVenta(this,client, prod, precio, fecha))
            {
                if (VerificarTelCartera(tel))
                {
                    Venta instancia3 = new Venta(this,client,prod,precio,fecha);
                    bd1.AgregarVenta(instancia3);
                }
                else
                {
                    throw new ArgumentException("Telefono no encontrado");
                }
                
            }
            else
            {
                throw new ArgumentException("Venta repetida");
            }
        }
        /// <summary>
        /// Verifica si el Cliente esta en la lista a traves de su telefono
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public bool VerificarTelCartera(int tel)
        {
            bool resultado = false;
            foreach (Cliente cliente in Cartera)
            {
                if (cliente.Tel == tel)
                {
                    resultado = true;
                }
            }
            return resultado;
        }
        /// <summary>
        /// Elimina a un Cliente de la lista a traves de su telefono
        /// </summary>
        /// <param name="tel"></param>
        public void EliminarTelCartera(int tel)
        {
            foreach (Cliente cliente in Cartera)
            {
                if (cliente.Tel == tel)
                {
                    Cartera.Remove(cliente);
                }
            }
        }
        
        
        /// <summary>
        /// Crea una Cotizaciun para una futura venta entre Usuario y cliente 
        /// </summary>
        /// <param name="telcliente"></param>
        /// <param name="fecha"></param>
        /// <param name="valor"></param>
        /// <param name="imp"></param>
        public void CrearCoti(int telcliente, DateTime fecha, Double valor, String imp)
        {
            BaseDatosCliente bd1 = BaseDatosCliente.Instance;
            Cliente cliente = bd1.ClienteSegunTelefono(telcliente);
            if (VerificarTelCartera(telcliente))
            {
                Cotizacion instancia2 = new Cotizacion(this,cliente,fecha, valor, imp);
                OportunidadesVentas.Add(instancia2);
            }
            else
            {
                throw new ArgumentException("Telefono no encontrado");
            }
        }

        
        /// <summary>
        /// Registra las interacciones tenidas entre Usuario y Cliente 
        /// </summary>
        /// <param name="interacion"></param>
        public void AgregarInteracion(IInteracion interacion)
        {
            ListaInteracciones.Add(interacion);
        }
       /// <summary>
       /// Busca los clientes por su Nombre y Apellido devolviendo todos los que tengan ese Nombre y Apelledo
       /// </summary>
       /// <param name="name"></param>
       /// <param name="apellido"></param>
       /// <returns></returns>
        public List<Cliente> FiltrarClienteNom(string name, string apellido)
        {
            return Cartera.FindAll(c => c.Nombre == name && c.Apellido == apellido);
        }
        /// <summary>
        /// Filtra a los clientes por su numero de Telefono por lo que devolvera el cliente que tenga ese Telefono
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        public Cliente FiltrarClienteTel(int telefono)
        {
            return Cartera.Find(c => c.Tel == telefono);
        }
        /// <summary>
        /// Filtra a los clientes por medio del Correo Electronico devolviendo quien tenga ese Correo 
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        public Cliente FiltrarClienteCorreo(string correo)
        {
            return Cartera.Find(c => c.Correo == correo);
        }
        /// <summary>
        /// Busca una interaccion con un Cliente en esoecifico a traves del Telefono , fecha y tipo de imnteraccion para implementarle una nota a la interaccion buscada
        /// </summary>
        /// <param name="telcliente"></param>
        /// <param name="fecha"></param>
        /// <param name="tipo"></param>
        /// <param name="nota"></param>
        public void AgregarNota(int telcliente, DateTime fecha, string tipo, string nota)
        {
            IInteracion interaccion = ListaInteracciones
                .Find(i => i.TelCliente == telcliente && i.GetType().Name == tipo && i.Fecha.Date == fecha.Date);

            if (interaccion != null)
            {
                // Si la interacción existe, agregar la nota
                interaccion.Notas += $"\n{nota}";
            }
        }

        /// <summary>
        /// Devuelve una lista con todas las interacciones con un cliente 
        /// </summary>
        /// <param name="telcliente"></param>
        /// <returns></returns>
        public List<IInteracion> InteracionClienteSinFiltro(int telcliente)
        {
            return ListaInteracciones.FindAll(i => i.TelCliente == telcliente);
        }
        /// <summary>
        /// De todas las interacciones filtra las que son con un cliente y en la fecha indicada para devolvera en una lista 
        /// </summary>
        /// <param name="telcliente"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<IInteracion> InteracionClienteFiltroFecha(int telcliente, DateTime fecha)
        {
            return ListaInteracciones.FindAll(i => i.TelCliente == telcliente && i.Fecha.Date == fecha.Date);
        }
        /// <summary>
        /// Filtra las interacciones por el tipo que quiere el usuario y esas interacciones las devuelve a traves de una lista 
        /// </summary>
        /// <param name="telcliente"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<IInteracion> InteracionClienteFiltroTipo(int telcliente, string tipo)
        {
            return ListaInteracciones.FindAll(i => i.TelCliente == telcliente && i.GetType().Name == tipo);
        }
        /// <summary>
        /// Filtra todas las interacciones por una fecha y tipo de interacion , devolviendolas a traves de una lista 
        /// </summary>
        /// <param name="telcliente"></param>
        /// <param name="tipo"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<IInteracion> InteracionClienteFiltroTipoFecha(int telcliente, string tipo, DateTime fecha)
        {
            return ListaInteracciones.FindAll(i =>
                i.TelCliente == telcliente &&
                i.GetType().Name == tipo &&
                i.Fecha.Date == fecha.Date);
        }


        /// <summary>
        /// Busca todas las interacciones que tengan un remitente y no fueron contestadas por el usuario
        /// </summary>
        /// <returns></returns>
        public List<IInteracionDialogo> IntreaSinResponder()
        {
            List<IInteracionDialogo> resultado = new List<IInteracionDialogo>();
            foreach (var interacion in ListaInteracciones  )
            {
                IInteracionDialogo dialogo = interacion as IInteracionDialogo;
                if (dialogo != null && !dialogo.Respondida)
                {
                    resultado.Add(dialogo);
                }
            }
            return resultado;
            
        }

       /// <summary>
       /// Busca las interacciones con un cliente y revisa en que fecha fueron realisada y las compara un limite de x tiempo para devolver las que esten cerca o pasaron de ese tiempo 
       /// </summary>
       /// <returns></returns>
        public List<IInteracion> InteraViejas()
        {
           DateTime Fecha_Limite= DateTime.Now.AddDays(-30);
           return ListaInteracciones.FindAll(i => i.Fecha < Fecha_Limite);
        }

       /// <summary>
       /// Le muestra a un Usuario una lista con todos sus Clientes 
       /// </summary>
       /// <returns></returns>
       public string PanelCliente()
        {
            List<string> mostrar = new List<string>();
            foreach (Cliente client in Cartera)
            {
                string stringcliente = $"{client.Nombre} {client.Apellido}:";
                List<IInteracion> listinte = InteracionClienteSinFiltro(client.Tel);
                List<string> listintestring = new List<string>();
                foreach (IInteracion inte in listinte)
                {
                    listintestring.Add($"{inte.GetType()},{inte.Fecha},Tema:{inte.Tema},Notas:{inte.Notas}");
                }
                stringcliente += string.Join(Environment.NewLine, listintestring);
                mostrar.Add(stringcliente);

            }

            string resultado = string.Join(Environment.NewLine, mostrar);
            return resultado;

            
            
            
        }
       /// <summary>
       /// Asigna o Cambia a un Cliente con Usuario determinado 
       /// </summary>
       /// <param name="telcliente"></param>
       /// <param name="correouser"></param>
        
        public void AsignarCliente(int telcliente, string correouser)
        {
            BaseDatosCliente bdcliente = BaseDatosCliente.Instance;
            BaseDatosUsuario bdusuario = BaseDatosUsuario.Instance;
            if (bdusuario.ExisteCorreoUser(correouser) & bdcliente.ExisteTel(telcliente))
            {
                Cliente cliente = bdcliente.ClienteSegunTelefono(telcliente);
                Usuario usuario = bdusuario.UsuarioSegunCorreo(correouser);
                foreach (var coti in OportunidadesVentas)
                {
                    if (coti.Cliente == cliente)
                    {
                        coti.Usuario = usuario;
                        usuario.OportunidadesVentas.Add(coti);
                        OportunidadesVentas.Remove(coti);
                    }
                }

                foreach (var interaccion in ListaInteracciones)
                {
                    if (interaccion.TelCliente == cliente.Tel)
                    {
                        usuario.ListaInteracciones.Add(interaccion);
                        ListaInteracciones.Remove(interaccion);
                    }
                }
                usuario.Cartera.Add(cliente);
                EliminarTelCartera(cliente.Tel);
            }
        }
        /// <summary>
        /// Metodo para que la fachada pueda agregar un cliente a la Cartera del usuario.
        /// </summary>
        /// <param name="cliente"></param>
        public void AgregarClienteACartera(Cliente cliente)
        {
            Cartera.Add(cliente);
        }
    }
}