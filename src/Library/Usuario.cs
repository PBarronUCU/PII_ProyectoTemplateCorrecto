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
            
                Nombre = nombre;
                Apellido = apellido;
                Correo = correo;
                Suspendido = false;
                
            
            
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
        /// Registra las cotizaciones tenidas entre Usuario y Cliente. La agrega en ListaInteracciones y OportunidadesVenta
        /// </summary>
        /// <param name="cotizacion"></param>

        
        public void AgregarCotizacion(Cotizacion cotizacion)
        {
                OportunidadesVentas.Add(cotizacion);
                ListaInteracciones.Add(cotizacion);
            
        }

        
        /// <summary>
        /// Registra las interacciones tenidas entre Usuario y Cliente. La agrega en ListaInteraccion
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
        /// Busca una interaccion con un Cliente, fecha y tipo de imnteraccion en especifico  para implementarle una nota a la interaccion buscada
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="fecha"></param>
        /// <param name="tipo"></param>
        /// <param name="nota"></param>
        public void AgregarNota(Cliente cliente, DateTime fecha, string tipo, string nota)
        {
            IInteracion interaccion = ListaInteracciones
                .Find(i => i.Cliente == cliente && i.GetType().Name == tipo && i.Fecha.Date == fecha.Date);

            if (interaccion != null)
            {
                // Si la interacción existe, agregar la nota
                interaccion.Notas += $"\n{nota}";
            }
            else
            {
                throw new Exception("No se encontro ninguna interaccion que coincida");
            }
        }

        /// <summary>
        /// Devuelve una lista con todas las interacciones con un cliente 
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public List<IInteracion> InteracionClienteSinFiltro(Cliente cliente)
        {
            return ListaInteracciones.FindAll(i => i.Cliente == cliente);
        }
        /// <summary>
        /// De todas las interacciones filtra las que son con un cliente y en la fecha indicada para devolvera en una lista 
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<IInteracion> InteracionClienteFiltroFecha(Cliente cliente, DateTime fecha)
        {
            return ListaInteracciones.FindAll(i => i.Cliente == cliente && i.Fecha.Date == fecha.Date);
        }
        /// <summary>
        /// Filtra las interacciones por el tipo que quiere el usuario y esas interacciones las devuelve a traves de una lista 
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public List<IInteracion> InteracionClienteFiltroTipo(Cliente cliente, string tipo)
        {
            return ListaInteracciones.FindAll(i => i.Cliente == cliente && i.GetType().Name == tipo);
        }
        /// <summary>
        /// Filtra todas las interacciones por una fecha y tipo de interacion , devolviendolas a traves de una lista 
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="tipo"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<IInteracion> InteracionClienteFiltroTipoFecha(Cliente cliente, string tipo, DateTime fecha)
        {
            return ListaInteracciones.FindAll(i =>
                i.Cliente == cliente &&
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
                List<IInteracion> listinte = InteracionClienteSinFiltro(client);
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
                        usuario.OportunidadesVentas.Add(coti);
                        OportunidadesVentas.Remove(coti);
                    }
                }

                foreach (var interaccion in ListaInteracciones)
                {
                    if (interaccion.Cliente == cliente)
                    {
                        usuario.ListaInteracciones.Add(interaccion);
                        ListaInteracciones.Remove(interaccion);
                    }
                }
                usuario.Cartera.Add(cliente);
                EliminarTelCartera(cliente.Tel);
            }
            else
            {
                throw new Exception("No se ha encontrado el cliente o usuario");
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