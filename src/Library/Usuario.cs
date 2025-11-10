using System;
using System.Collections.Generic;using System.Collections.ObjectModel;
using System.Linq;

namespace Library


{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; }
        public List<Cliente> Cartera = new List<Cliente>();
        public List<Cotizacion> OportunidadesVentas = new List<Cotizacion>();
        public List<IInteracion> ListaInteracciones = new List<IInteracion>();//Leimos la Letra siempre desde el punto de vista del Usuario=Vendedor.
        public bool Suspendido { get; private set; }                        

        public Usuario(string nombre, string apellido, string correo)
        {
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
                Console.WriteLine("El correo ya esta ocupado");
            }
        }

        public void CrearCliente(String name, String apell, int tel, String correo)
        {
            BaseDatosCliente bd1 = BaseDatosCliente.Instance; 
            //Inecesario, pero por si acaso para no encontrarme errores al agregarlo a la cartera
            if (!bd1.ExisteCorreo(correo) & !bd1.ExisteTel(tel))
            {
                Cliente instancia1 = new Cliente(name, apell, correo, tel);
                Cartera.Add(instancia1);
            }
            else
            {
                Console.WriteLine("El correo o telefono ya esta ocupado");
            }
        }
        

        public void Suspender()
        {
            Suspendido = true;
        }
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
                    Console.WriteLine("Telefono no encontrado");
                }
                
            }
            else
            {
                Console.WriteLine("Venta repetida");
            }
        }
        
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
                Console.WriteLine("Telefono no encontrado");
            }
        }

        /// <summary>
        /// El telefono es para identificar el Cliente, NO para cambiarlo.
        /// </summary>
        public void ModificarCliente(string nombre,string apell, int tel, Genero genero,DateTime fecha )
        {
            BaseDatosCliente bd1 = BaseDatosCliente.Instance;
            Cliente client =  bd1.ClienteSegunTelefono(tel);
            client.Nombre = nombre;
            client.Apellido = apell;
            client.Genero = genero;
            client.FechaNac =  fecha;
            
        }

        public void AgregarInteracion(IInteracion interacion)
        {
            ListaInteracciones.Add(interacion);
        }
        public List<Cliente> FiltrarClienteNom(string name, string apellido)
        {
            return Cartera.FindAll(c => c.Nombre == name && c.Apellido == apellido);
        }

        public Cliente FiltrarClienteTel(int telefono)
        {
            return Cartera.Find(c => c.Tel == telefono);
        }

        public Cliente FiltrarClienteCorreo(string correo)
        {
            return Cartera.Find(c => c.Correo == correo);
        }

        public void AgregarNota(int telcliente, DateTime fecha, string tipo, string nota)
        {
            // Buscar una interacción del cliente correspondiente
            IInteracion interaccion = ListaInteracciones
                .Find(i => i.TelCliente == telcliente && i.GetType().Name == tipo && i.Fecha.Date == fecha.Date);

            if (interaccion != null)
            {
                // Si la interacción existe, agregar la nota
                interaccion.Notas += $"\n{nota}";
            }
        }


        public List<IInteracion> InteracionClienteSinFiltro(int telcliente)
        {
            return ListaInteracciones.FindAll(i => i.TelCliente == telcliente);
        }

        public List<IInteracion> InteracionClienteFiltroFecha(int telcliente, DateTime fecha)
        {
            return ListaInteracciones.FindAll(i => i.TelCliente == telcliente && i.Fecha.Date == fecha.Date);
        }

        public List<IInteracion> InteracionClienteFiltroTipo(int telcliente, string tipo)
        {
            return ListaInteracciones.FindAll(i => i.TelCliente == telcliente && i.GetType().Name == tipo);
        }
        public List<IInteracion> InteracionClienteFiltroTipoFecha(int telcliente, string tipo, DateTime fecha)
        {
            return ListaInteracciones.FindAll(i =>
                i.TelCliente == telcliente &&
                i.GetType().Name == tipo &&
                i.Fecha.Date == fecha.Date);
        }


        public List<IInteracionDialogo> IntreaSinResponder()
        {
            List<IInteracionDialogo> resultado = new List<IInteracionDialogo>();
            foreach (var interacion in ListaInteracciones  )
            {
                IInteracionDialogo dialogo = interacion as IInteracionDialogo;
                if (dialogo.Remitente == UsuarioOCliente.Cliente)
                {
                    resultado.Add(dialogo);
                }
            }
            return resultado;//Verificar
            
        }

        public List<IInteracion> InteraViejas()
        {
           DateTime Fecha_Limite= DateTime.Now.AddDays(-30);
           return ListaInteracciones.FindAll(i => i.Fecha < Fecha_Limite);
        }

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

    }
}

