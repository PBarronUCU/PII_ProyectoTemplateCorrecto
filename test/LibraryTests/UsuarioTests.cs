using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Library;

namespace LibraryTests
{
    /// <summary>
    /// Se validan los principales comportamientos relacionados con
    /// clientes, cotizaciones, ventas e interacciones.
    /// </summary>
    [TestFixture]
    public class UsuarioTests
    {
        /*
        private BaseDatosUsuario bdUsuarios;
        private BaseDatosCliente bdClientes;
        private BaseDatosVenta bdVentas;

        [SetUp]
        public void SetUp()
        {
            // Antes de cada test, se limpian las bases de datos simuladas.
            bdUsuarios = BaseDatosUsuario.Instance;
            bdClientes = BaseDatosCliente.Instance;
            bdVentas = BaseDatosVenta.Instance;

            bdUsuarios.ListaUsuario.Clear();
            bdClientes.ListaCliente.Clear();
            bdVentas.ListaVentas.Clear();
        }

        /// <summary>
        /// Verifica que se cree un usuario correctamente cuando el correo no está ocupado.
        /// </summary>
        [Test]
        public void CrearUsuario_ConCorreoUnico_CreaUsuarioCorrectamente()
        {
            var usuario = new Usuario("Juan", "Pérez", "juan@mail.com");

            Assert.That(usuario.Nombre, Is.EqualTo("Juan"));
            Assert.That(usuario.Apellido, Is.EqualTo("Pérez"));
            Assert.That(usuario.Correo, Is.EqualTo("juan@mail.com"));
            Assert.That(usuario.Suspendido, Is.False);
            Assert.That(bdUsuarios.ListaUsuario.Contains(usuario), Is.True);
        }

        /// <summary>
        /// Verifica que el método Suspender cambie el estado del usuario correctamente.
        /// </summary>
        [Test]
        public void SuspenderUsuario_CambiaEstadoSuspendido()
        {
            var usuario = new Usuario("Ana", "García", "ana@mail.com");

            usuario.Suspender();

            Assert.That(usuario.Suspendido, Is.True);
        }

        /// <summary>
        /// Verifica que se pueda crear un cliente válido y se agregue tanto a la cartera del usuario
        /// como a la base de datos global de clientes.
        /// </summary>
        [Test]
        public void CrearCliente_AgregaClienteALaCarteraYBase()
        {
            var usuario = new Usuario("Luis", "Díaz", "luis@mail.com");
            usuario.CrearCliente("Carlos", "Fernández", 12345, "carlos@mail.com");

            Assert.That(usuario.Cartera.Count, Is.EqualTo(1));
            Assert.That(bdClientes.ListaCliente.Count, Is.EqualTo(1));

            var cliente = usuario.Cartera.First();
            Assert.That(cliente.Nombre, Is.EqualTo("Carlos"));
            Assert.That(cliente.Tel, Is.EqualTo(12345));
        }

        /// <summary>
        /// Verifica que el método VerificarTelCartera detecte correctamente si un cliente está en la cartera.
        /// </summary>
        [Test]
        public void VerificarTelCartera_DevuelveTrueSiClienteExiste()
        {
            var usuario = new Usuario("María", "López", "maria@mail.com");
            usuario.CrearCliente("Pedro", "Suárez", 5555, "pedro@mail.com");

            Assert.That(usuario.VerificarTelCartera(5555), Is.True);
            Assert.That(usuario.VerificarTelCartera(9999), Is.False);
        }

        /// <summary>
        /// Verifica que EliminarTelCartera elimine correctamente un cliente de la cartera.
        /// </summary>
        [Test]
        public void EliminarTelCartera_EliminaClienteCorrectamente()
        {
            var usuario = new Usuario("Mario", "Rossi", "mario@mail.com");
            usuario.CrearCliente("Laura", "Gómez", 7777, "laura@mail.com");

            usuario.EliminarTelCartera(7777);

            Assert.That(usuario.Cartera.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Verifica que el filtrado por nombre y apellido devuelva los clientes correctos.
        /// </summary>
        [Test]
        public void FiltrarClienteNom_DevuelveClientesCoincidentes()
        {
            var usuario = new Usuario("Diego", "Paz", "diego@mail.com");
            usuario.CrearCliente("Lucía", "Santos", 1111, "lucia@mail.com");
            usuario.CrearCliente("Lucía", "Santos", 2222, "lucia2@mail.com");

            var resultados = usuario.FiltrarClienteNom("Lucía", "Santos");

            Assert.That(resultados.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// Verifica que CrearCoti agregue correctamente una cotización en la lista de oportunidades del usuario.
        /// </summary>
        [Test]
        public void CrearCoti_AgregaCotizacionALaLista()
        {
            var usuario = new Usuario("Pablo", "Ramírez", "pablo@mail.com");
            usuario.CrearCliente("Sofía", "Gómez", 1000, "sofia@mail.com");

            usuario.CrearCoti(1000, DateTime.Now, 2000.0, "Alta");

            Assert.That(usuario.OportunidadesVentas.Count, Is.EqualTo(1));
            Assert.That(usuario.OportunidadesVentas.First().Cliente.Nombre, Is.EqualTo("Sofía"));
        }

        /// <summary>
        /// Verifica que CrearVentas registre correctamente una venta y la agregue a la base de datos.
        /// </summary>
        [Test]
        public void CrearVentas_AgregaVentaCorrectamente()
        {
            var usuario = new Usuario("Laura", "Martín", "laura@mail.com");
            usuario.CrearCliente("Oscar", "Medina", 2000, "oscar@mail.com");

            usuario.CrearVentas(2000, "Notebook", 1200.0, DateTime.Today);

            Assert.That(bdVentas.ListaVentas.Count, Is.EqualTo(1));
            var venta = bdVentas.ListaVentas.First();
            Assert.That(venta.Producto, Is.EqualTo("Notebook"));
            Assert.That(venta.Usuario, Is.EqualTo(usuario));
        }

        /// <summary>
        /// Verifica que el método AgregarInteracion agregue una interacción a la lista del usuario.
        /// </summary>
        [Test]
        public void AgregarInteracion_AgregaCorrectamente()
        {
            var usuario = new Usuario("Felipe", "Castro", "felipe@mail.com");
            var interaccion = new InteraccionPrueba
            {
                Fecha = DateTime.Today,
                Tema = "Llamada",
                Notas = "Cliente interesado",
                TelCliente = 999
            };

            usuario.AgregarInteracion(interaccion);

            Assert.That(usuario.ListaInteracciones.Contains(interaccion), Is.True);
        }

        /// <summary>
        /// Verifica que InteraViejas devuelva solo interacciones con más de 30 días de antigüedad.
        /// </summary>
        [Test]
        public void InteraViejas_DevuelveSoloAntiguas()
        {
            var usuario = new Usuario("Sergio", "Núñez", "sergio@mail.com");
            usuario.AgregarInteracion(new InteraccionPrueba
            {
                Fecha = DateTime.Now.AddDays(-40),
                Tema = "Seguimiento",
                Notas = "Sin respuesta",
                TelCliente = 123
            });
            usuario.AgregarInteracion(new InteraccionPrueba
            {
                Fecha = DateTime.Now.AddDays(-5),
                Tema = "Llamada",
                Notas = "Cliente interesado",
                TelCliente = 123
            });

            var viejas = usuario.InteraViejas();

            Assert.That(viejas.Count, Is.EqualTo(1));
            Assert.That(viejas.First().Tema, Is.EqualTo("Seguimiento"));
        }

        /// <summary>
        /// Verifica que VentasPeriodo devuelva solo las ventas que ocurren dentro del rango dado.
        /// </summary>
        [Test]
        public void VentasPeriodo_DevuelveSoloVentasDentroDelRango()
        {
            var usuario = new Usuario("Carmen", "Prieto", "carmen@mail.com");
            var cliente = new Cliente("Tomás", "Rodríguez", "tomas@mail.com", 8000);
            var venta1 = new Venta(usuario, cliente, "TV", 500, DateTime.Today.AddDays(-5));
            var venta2 = new Venta(usuario, cliente, "PC", 1000, DateTime.Today.AddDays(-40));

            bdVentas.AgregarVenta(venta1);
            bdVentas.AgregarVenta(venta2);

            var ventasPeriodo = usuario.VentasPeriodo(DateTime.Today.AddDays(-10), DateTime.Today);

            Assert.That(ventasPeriodo.Count, Is.EqualTo(1));
            Assert.That(ventasPeriodo.First().Producto, Is.EqualTo("TV"));
        }

        /// <summary>
        /// Clase auxiliar mínima para pruebas de interacciones.
        /// </summary>
        private class InteraccionPrueba : IInteracion
        {
            public DateTime Fecha { get; set; }
            public string Tema { get; set; }
            public string Notas { get; set; }
            public int TelCliente { get; set; }
        }*/
    }
}
