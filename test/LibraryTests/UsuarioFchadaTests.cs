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
        private UsuarioFachada UserFachada;
        private AdminFachada AdminFachada;
        private BaseDatosCliente BDCliente;
        private BaseDatosUsuario BDUsuario;
        private BaseDatosVenta BDVenta;
        private string correoUser = "correouser@";
        private string nombreAdmin = "Administrador2";
        
        /// <summary>
        /// No funciona, Intente con el profe en clase no pudimos arreglar este error.
        /// Los test funcionan individualmente.
        /// El error salta en el OneTimeSetUp.
        /// 
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            UserFachada = UsuarioFachada.Instance;
            AdminFachada = AdminFachada.Instance;
            BDCliente = BaseDatosCliente.Instance;
            BDUsuario = BaseDatosUsuario.Instance;
            BDVenta = BaseDatosVenta.Instance;
            AdminFachada.Instance.CrearAdmin(nombreAdmin);
            AdminFachada.Instance.CrearUsuario(nombreAdmin,"Nombre","Apellido",correoUser);
        }

        [Test]
        public void CrearCliente_Success_AddsClientToBaseAndUserCartera()
        {
            
            UserFachada.CrearCliente(correoUser,"Nom","Ape",1,"A@");
            var clientFromBd = BaseDatosCliente.Instance.ClienteSegunTelefono(1);
            Assert.That(clientFromBd, Is.Not.Null);
            Assert.That(clientFromBd.Correo, Is.EqualTo("A@"));

            var userFromBd = BaseDatosUsuario.Instance.UsuarioSegunCorreo(correoUser);
            Assert.That(userFromBd.Cartera.Any(c => c.Tel == 1), Is.True);
        }

        [Test]
        public void CrearCliente_UsuarioNoEncontrado_ThrowsException()
        {
            // No se crea usuario
            var ex = Assert.Throws<Exception>(() =>
            {
                UserFachada.CrearCliente("noexiste@ucu.edu.uy", "Carlos", "Pérez", 11111, "c@c.com");
            });

            Assert.That(ex.Message, Is.EqualTo("Usuario no encontrado"));
        }

        [Test]
        public void CrearCliente_UsuarioSuspendido_ThrowsArgumentException()
        {
           AdminFachada.CrearUsuario(nombreAdmin,"Patri","Barr","B@");
            Usuario user =BDUsuario.UsuarioSegunCorreo("B@");
            user.Suspender();

            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                UserFachada.CrearCliente("B@","María", "González", 3, "Client@ucu.edu.uy");
            });

            Assert.That("El usuario esta suspendido",Is.EqualTo(ex.Message));
        }

        [Test]
        public void CrearVenta_Success_AddsVentaToBase()
        {
            // Arrange
            // Crear cliente (se agrega a BD y a cartera del usuario con CrearCliente)
            UserFachada.CrearCliente(correoUser, "Cliente", "Uno", 3, "c3@c.com");

            // Act
            string fecha = "2025-01-01";
            UserFachada.CrearVenta(correoUser, "TemaVenta1", "Notas1", fecha, 3, 1500.0, "ProductoX");

            // Assert
            var ventas = BDVenta.ListaVentas;
            Assert.That(ventas.Count, Is.EqualTo(1));
            var venta = ventas.First();
            Assert.That(venta.Usuario.Correo, Is.EqualTo(correoUser));
            Assert.That(venta.Producto, Is.EqualTo("ProductoX"));
            Assert.That(Math.Abs(venta.Precio - 1500.0) < 1e-6, Is.True);
        }

        [Test]
        public void CrearVenta_UsuarioSuspendido_ThrowsArgumentException()
        {
            AdminFachada.CrearUsuario(nombreAdmin,"Patri","Barr","susp1@");
            UserFachada.CrearCliente("susp1@", "Cliente5", "A", 5, "a5@c.com");
            AdminFachada.SuspenderUsuario(nombreAdmin,"susp1@");
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                UserFachada.CrearVenta("susp1@","T", "N", "2025-02-02", 5, 100.0, "P");
            });

            Assert.That(ex.Message, Is.EqualTo("El usuario esta suspendido"));
        }

        [Test]
        public void CrearCoti_AddsCotizacionToUsuarioOportunidades()
        {
            
            UserFachada.CrearCliente(correoUser, "Cliente", "C", 6, "c6@c.com");
            UserFachada.CrearCoti(correoUser, "CotiTema", "CotiNotas", "2025-03-03", 6, 999.99, "ProdC");

            var usu = BaseDatosUsuario.Instance.UsuarioSegunCorreo(correoUser);
            Assert.That(usu.OportunidadesVentas.Count, Is.EqualTo(1));
            var coti = usu.OportunidadesVentas.First();
            Assert.That(coti.Producto, Is.EqualTo("ProdC"));
            Assert.That(Math.Abs(coti.Valor - 999.99) < 1e-6, Is.True);
        }

        [Test]
        public void ModificarCliente_ChangesClientPropertiesInBase()
        {
            // crear cliente y agregar al user
            UserFachada.CrearCliente(correoUser, "Antes", "Apellido", 7, "cliant7@c.com");
            
            
            // Act: modificar
            UserFachada.ModificarCliente(correoUser, "Nuevo", "ApellidoN", 7, "Otro", "2000-05-05");

            var client = BaseDatosCliente.Instance.ClienteSegunTelefono(7);
            Assert.That(client.Nombre, Is.EqualTo("Nuevo"));
            Assert.That(client.Apellido, Is.EqualTo("ApellidoN"));
            Assert.That(client.Genero, Is.EqualTo(Genero.Otro));
            Assert.That(client.FechaNac.Date, Is.EqualTo(DateTime.Parse("2000-05-05").Date));
        }

        [Test]
        public void FiltarClienteCorreo_ReturnsClientFromUserCartera()
        {
            AdminFachada.CrearUsuario(nombreAdmin,"Enzo","Olivera","Filtro1@");
            UserFachada.CrearCliente("Filtro1@", "Fil", "T", 8, "filc8@c.com");

            var result = UserFachada.FiltarClienteCorreo("Filtro1@", "filc8@c.com");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Tel, Is.EqualTo(8));
        }

        [Test]
        public void InteracionClienteSinFiltro_ReturnsCreatedInteractions()
        {
            AdminFachada.CrearUsuario(nombreAdmin,"Enzo1","Olivera1","Filtro2@");
            UserFachada.CrearCliente("Filtro2@", "Cli", "One", 9, "one9@c.com");

            UserFachada.CrearReunion("Filtro2@", "2025-04-04", "Reu", "NotasReu", 9, "Oficina");

            var inters = UserFachada.InteracionClienteSinFiltro("Filtro2@", 9);
            Assert.That(inters, Is.Not.Null);
            Assert.That(inters.Count, Is.EqualTo(1));
            Assert.That(inters.First().GetType().Name, Is.EqualTo("Reunion"));
        }
        
        
        
        [Test]
        public void AgregarNota_AppendsNoteToExistingInteraction()
        {
            AdminFachada.CrearUsuario(nombreAdmin,"Enzo2","Olivera2","Filtro3@");
            UserFachada.CrearCliente("Filtro3@", "Cli", "Note", 10, "note10@c.com");

            // crear un mensaje (tipo "Mensaje") en esa fecha
            string fecha = "2025-05-05";
            UserFachada.CrearMensaje("Filtro3@", "Usuario", fecha, "TemaMsg", 10, "NotasIniciales", false);

            // agregar nota
            UserFachada.AgregarNota("Filtro3@", 10, fecha, "Mensaje", "Nota añadida");

            var usu = BaseDatosUsuario.Instance.UsuarioSegunCorreo("Filtro3@");
            var msg = usu.ListaInteracciones.Find(i => i.GetType().Name == "Mensaje");
            Assert.That(msg, Is.Not.Null);
            Assert.That(msg.Notas.Contains("Nota añadida"), Is.True);
        }

        [Test]
        public void FiltrarClienteNombreAndTelefono_WorkAsExpected()
        {
            
            AdminFachada.CrearUsuario(nombreAdmin,"Enzo3","Olivera3","Filtro45@");
            UserFachada.CrearCliente("Filtro45@", "Nombre", "Apellido", 11, "n11@c.com");

            var lista = UserFachada.FiltrarClienteNombre("Filtro45@", "Nombre", "Apellido");
            Assert.That(lista.Count, Is.EqualTo(1));
            Assert.That(lista.First().Correo, Is.EqualTo("n11@c.com"));

            var byTel = UserFachada.FiltrarClienteTelefono("Filtro45@", 11);
            Assert.That(byTel, Is.Not.Null);
            Assert.That(byTel.Correo, Is.EqualTo("n11@c.com"));
        }

        [Test]
        public void AsignarCliente_MovesClientBetweenUsers()
        {
            // Arrange two users
            var correoFrom = "from@ucu.edu.uy";
            var correoTo = "to@ucu.edu.uy";
            AdminFachada.CrearUsuario(nombreAdmin,"Tizi","Soto","Mover1@");
            AdminFachada.CrearUsuario(nombreAdmin,"Tizi1","Soto1","Mover2@");
            var userFrom = BDUsuario.UsuarioSegunCorreo("Mover1@");
            var userTo = BDUsuario.UsuarioSegunCorreo("Mover2@");

            // create client on userFrom
            UserFachada.CrearCliente("Mover1@", "Mover", "Cli", 14, "mov14@c.com");

            // preconditions
            Assert.That(userFrom.Cartera.Any(c => c.Tel == 14), Is.True);
            Assert.That(userTo.Cartera.Any(c => c.Tel == 14), Is.False);

            // Act
            UserFachada.AsignarCliente("Mover1@", "Mover2@", 14);

            // Assert client moved
            var from = BDUsuario.UsuarioSegunCorreo("Mover1@");
            var to = BDUsuario.UsuarioSegunCorreo("Mover2@");

            Assert.That(to.Cartera.Any(c => c.Tel == 14), Is.True);
            Assert.That(from.Cartera.Any(c => c.Tel == 14), Is.False);
        }

        [Test]
        public void PanelCliente_Returns_StringRepresentationOrNotFound()
        {
            var correoUser = "panel@ucu.edu.uy";
            AdminFachada.CrearUsuario(nombreAdmin,"Tizi2","Soto2","Panel@");
            ;

            // no clients -> empty panel
            var empty = UserFachada.PanelCliente("Panel@");
            Assert.That(empty, Is.Empty.Or.EqualTo(string.Empty));

            // add client and interaction to produce some output
            UserFachada.CrearCliente("Panel@", "Panel", "C", 15, "p5c1@c.com");
            UserFachada.CrearMensaje("Panel@", "Usuario", "2025-06-06", "t", 15, "nota", false);

            var panel = UserFachada.PanelCliente("Panel@");
            Assert.That(panel, Is.Not.Null.And.Not.Empty);
        }
        
        
        
        

        [TearDown]
        public void TearDown()
        {
        }
    }
    
}
