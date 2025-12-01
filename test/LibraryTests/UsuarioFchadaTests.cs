using System;
using System.Linq;
using NUnit.Framework;
using Library;
using System.Collections.Generic;

namespace LibraryTests
{
    [TestFixture]
    public class UsuarioFachadaTests
    {
        private UsuarioFachada fachada;
        private BaseDatosCliente bdcliente;
        private BaseDatosUsuario bdusuario;

        [SetUp]
        public void SetUp()
        {
            // Reiniciar singletons antes de cada test

            fachada = UsuarioFachada.Instance;
            bdcliente = BaseDatosCliente.Instance;
            bdusuario = BaseDatosUsuario.Instance;
            
        }

        [Test]
        public void CrearCliente_Success_AddsClientToBaseAndUserCartera()
        {

            string correoUser = "a@a";
            AdminFachada.Instance.CrearAdmin("Admin1");
            AdminFachada.Instance.CrearUsuario("Admin1","Name","SSur",correoUser);
            fachada.CrearCliente(correoUser,"Nom","Ape",12,"correcliente@");
            var clientFromBd = BaseDatosCliente.Instance.ClienteSegunTelefono(12);
            Assert.That(clientFromBd, Is.Not.Null);
            Assert.That(clientFromBd.Correo, Is.EqualTo("correcliente@"));

            var userFromBd = BaseDatosUsuario.Instance.UsuarioSegunCorreo(correoUser);
            Assert.That(userFromBd.Cartera.Any(c => c.Tel == 12), Is.True);
        }

        [Test]
        public void CrearCliente_UsuarioNoEncontrado_ThrowsException()
        {
            // No se crea usuario
            var ex = Assert.Throws<Exception>(() =>
            {
                fachada.CrearCliente("noexiste@ucu.edu.uy", "Carlos", "Pérez", 11111, "c@c.com");
            });

            Assert.That(ex.Message, Is.EqualTo("Usuario no encontrado"));
        }

        [Test]
        public void CrearCliente_UsuarioSuspendido_ThrowsArgumentException()
        {
            AdminFachada.Instance.CrearAdmin("Admin1");
            string correoUser = "user2@ucu.edu.uy";
            AdminFachada.Instance.CrearUsuario("Admin1","Patri","Barr",correoUser);
            Usuario user =BaseDatosUsuario.Instance.UsuarioSegunCorreo(correoUser);
            user.Suspender();

            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                fachada.CrearCliente(correoUser, "María", "González", 22222, "Client@ucu.edu.uy");
            });

            Assert.That("El usuario esta suspendido",Is.EqualTo(ex.Message));
        }

        [Test]
        public void CrearVenta_Success_AddsVentaToBase()
        {
            // Arrange
            var correoUser = "vendedor@ucu.edu.uy";
            var user = new Usuario("Vendedor", "V", correoUser);
            // Crear cliente (se agrega a BD y a cartera del usuario con CrearCliente)
            fachada.CrearCliente(correoUser, "Cliente", "Uno", 33333, "cli@c.com");

            // Act
            string fecha = "2025-01-01";
            fachada.CrearVenta(correoUser, "TemaVenta", "Notas", fecha, 33333, 1500.0, "ProductoX");

            // Assert
            var ventas = BaseDatosVenta.Instance.ListaVentas;
            Assert.That(ventas.Count, Is.EqualTo(1));
            var venta = ventas.First();
            Assert.That(venta.Usuario.Correo, Is.EqualTo(correoUser));
            Assert.That(venta.Producto, Is.EqualTo("ProductoX"));
            Assert.That(Math.Abs(venta.Precio - 1500.0) < 1e-6, Is.True);
        }

        [Test]
        public void CrearVenta_UsuarioSuspendido_ThrowsArgumentException()
        {
            var correoUser = "vendedor2@ucu.edu.uy";
            var user = new Usuario("V", "Dos", correoUser);
            fachada.CrearCliente(correoUser, "Cliente", "A", 44444, "a@c.com");
            user.Suspender();

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                fachada.CrearVenta(correoUser, "T", "N", "2025-02-02", 44444, 100.0, "P");
            });

            Assert.That(ex.Message, Is.EqualTo("El usuario esta suspendido"));
        }

        [Test]
        public void CrearCoti_AddsCotizacionToUsuarioOportunidades()
        {
            var correoUser = "cot@ucu.edu.uy";
            var user = new Usuario("Coti", "Uno", correoUser);
            fachada.CrearCliente(correoUser, "Cliente", "C", 55555, "cc@c.com");

            fachada.CrearCoti(correoUser, "CotiTema", "CotiNotas", "2025-03-03", 55555, 999.99, "ProdC");

            var usu = BaseDatosUsuario.Instance.UsuarioSegunCorreo(correoUser);
            Assert.That(usu.OportunidadesVentas.Count, Is.EqualTo(1));
            var coti = usu.OportunidadesVentas.First();
            Assert.That(coti.Producto, Is.EqualTo("ProdC"));
            Assert.That(Math.Abs(coti.Valor - 999.99) < 1e-6, Is.True);
        }

        [Test]
        public void ModificarCliente_ChangesClientPropertiesInBase()
        {
            var correoUser = "mod@ucu.edu.uy";
            var user = new Usuario("Mod", "Uno", correoUser);
            // crear cliente y agregar al user
            fachada.CrearCliente(correoUser, "Antes", "Apellido", 66666, "antes@c.com");

            // Act: modificar
            fachada.ModificarCliente(correoUser, "Nuevo", "ApellidoN", 66666, "OTRO", "2000-05-05");

            var client = BaseDatosCliente.Instance.ClienteSegunTelefono(66666);
            Assert.That(client.Nombre, Is.EqualTo("Nuevo"));
            Assert.That(client.Apellido, Is.EqualTo("ApellidoN"));
            Assert.That(client.Genero, Is.EqualTo(Genero.Otro));
            Assert.That(client.FechaNac.Date, Is.EqualTo(DateTime.Parse("2000-05-05").Date));
        }

        [Test]
        public void FiltarClienteCorreo_ReturnsClientFromUserCartera()
        {
            var correoUser = "filt@ucu.edu.uy";
            var user = new Usuario("F", "Uno", correoUser);
            fachada.CrearCliente(correoUser, "Fil", "T", 77777, "fil@c.com");

            var result = fachada.FiltarClienteCorreo(correoUser, "fil@c.com");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Tel, Is.EqualTo(77777));
        }

        [Test]
        public void InteracionClienteSinFiltro_ReturnsCreatedInteractions()
        {
            var correoUser = "inter@ucu.edu.uy";
            var user = new Usuario("I", "Uno", correoUser);
            fachada.CrearCliente(correoUser, "Cli", "One", 88888, "one@c.com");

            fachada.CrearReunion(correoUser, "2025-04-04", "Reu", "NotasReu", 88888, "Oficina");

            var inters = fachada.InteracionClienteSinFiltro(correoUser, 88888);
            Assert.That(inters, Is.Not.Null);
            Assert.That(inters.Count, Is.EqualTo(1));
            Assert.That(inters.First().GetType().Name, Is.EqualTo("Reunion"));
        }
        
        
        
        [Test]
        public void AgregarNota_AppendsNoteToExistingInteraction()
        {
            var correoUser = "nota@ucu.edu.uy";
            var user = new Usuario("N", "Uno", correoUser);
            fachada.CrearCliente(correoUser, "Cli", "Note", 99999, "note@c.com");

            // crear un mensaje (tipo "Mensaje") en esa fecha
            string fecha = "2025-05-05";
            fachada.CrearMensaje(correoUser, "USUARIO", fecha, "TemaMsg", 99999, "NotasIniciales", false);

            // agregar nota
            fachada.AgregarNota(correoUser, 99999, fecha, "Mensaje", "Nota añadida");

            var usu = BaseDatosUsuario.Instance.UsuarioSegunCorreo(correoUser);
            var msg = usu.ListaInteracciones.Find(i => i.GetType().Name == "Mensaje");
            Assert.That(msg, Is.Not.Null);
            Assert.That(msg.Notas.Contains("Nota añadida"), Is.True);
        }

        [Test]
        public void FiltrarClienteNombreAndTelefono_WorkAsExpected()
        {
            var correoUser = "fil2@ucu.edu.uy";
            var user = new Usuario("FN", "Uno", correoUser);
            fachada.CrearCliente(correoUser, "Nombre", "Apellido", 101010, "n@c.com");

            var lista = fachada.FiltrarClienteNombre(correoUser, "Nombre", "Apellido");
            Assert.That(lista.Count, Is.EqualTo(1));
            Assert.That(lista.First().Correo, Is.EqualTo("n@c.com"));

            var byTel = fachada.FiltrarClienteTelefono(correoUser, 101010);
            Assert.That(byTel, Is.Not.Null);
            Assert.That(byTel.Correo, Is.EqualTo("n@c.com"));
        }

        [Test]
        public void AsignarCliente_MovesClientBetweenUsers()
        {
            // Arrange two users
            var correoFrom = "from@ucu.edu.uy";
            var correoTo = "to@ucu.edu.uy";
            var userFrom = new Usuario("From", "User", correoFrom);
            var userTo = new Usuario("To", "User", correoTo);

            // create client on userFrom
            fachada.CrearCliente(correoFrom, "Mover", "Cli", 121212, "mov@c.com");

            // preconditions
            Assert.That(userFrom.Cartera.Any(c => c.Tel == 121212), Is.True);
            Assert.That(userTo.Cartera.Any(c => c.Tel == 121212), Is.False);

            // Act
            fachada.AsignarCliente(correoFrom, correoTo, 121212);

            // Assert client moved
            var from = BaseDatosUsuario.Instance.UsuarioSegunCorreo(correoFrom);
            var to = BaseDatosUsuario.Instance.UsuarioSegunCorreo(correoTo);

            Assert.That(to.Cartera.Any(c => c.Tel == 121212), Is.True);
            Assert.That(from.Cartera.Any(c => c.Tel == 121212), Is.False);
        }

        [Test]
        public void PanelCliente_Returns_StringRepresentationOrNotFound()
        {
            var correoUser = "panel@ucu.edu.uy";
            var user = new Usuario("P", "Uno", correoUser);

            // no clients -> empty panel
            var empty = fachada.PanelCliente(correoUser);
            Assert.That(empty, Is.Empty.Or.EqualTo(string.Empty));

            // add client and interaction to produce some output
            fachada.CrearCliente(correoUser, "Panel", "C", 131313, "pc@c.com");
            fachada.CrearMensaje(correoUser, "USUARIO", "2025-06-06", "t", 131313, "nota", false);

            var panel = fachada.PanelCliente(correoUser);
            Assert.That(panel, Is.Not.Null.And.Not.Empty);
        }

        [TearDown]
        public void TearDown()
        {
            UsuarioFachada.ResetInstance();
            BaseDatosUsuario.ResetInstance();
            BaseDatosCliente.ResetInstance();
            BaseDatosVenta.ResetInstance();
        }
    }
    
}
