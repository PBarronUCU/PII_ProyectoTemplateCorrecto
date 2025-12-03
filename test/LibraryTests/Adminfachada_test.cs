using System;
using NUnit.Framework;
using Library;
namespace LibraryTests
{
    [TestFixture]
    public class Adminfachada_test
    {
        private AdminFachada fachada;
        private String nameAdmin = "AdminOriginal";
        [OneTimeSetUp]
        public void SetUp()
        {
            fachada= AdminFachada.Instance;
            fachada.CrearAdmin(nameAdmin);
            
        }
        
        /// <summary>
        /// Crea un usuario lo arega a la base de datosy revisa si el usuario se creo exitosamente
        /// </summary>
        [Test]
        public void CrearUsuariyAgregarUsuarioEnBaseDatos()
        {
            fachada.CrearAdmin("Admin1_1");
            BaseDatosUsuario bdUser = BaseDatosUsuario.Instance;
            fachada.CrearUsuario("Admin1_1","Juan", "Pérez", "juan@ucu.edu.uy");

            Usuario usuario = bdUser.UsuarioSegunCorreo("juan@ucu.edu.uy");

            Assert.That(usuario, Is.Not.Null);
            Assert.That(usuario.Nombre, Is.EqualTo("Juan"));
        }
        /// <summary>
        /// Prueba que salte una exepcion al crear dos usuarios con el mismo correo
        /// </summary>
        [Test]
        public void CrearUsuariRepetido()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                fachada.CrearUsuario(nameAdmin,"Juan", "Pérez", "juanrepe1@ucu.edu.uy");
                fachada.CrearUsuario(nameAdmin,"Patri", "Soto", "juanrepe1@ucu.edu.uy");
            });
            Assert.That("El correo ya esta ocupado",Is.EqualTo(ex.Message));

        }
        
        /// <summary>
        /// Prueba a que salte una exepcion al crear dos admins con el mismo nombre
        /// </summary>
        [Test]
        public void CrearAdminRepetido()
        {
            Exception ex = Assert.Throws<Exception>(() =>
            {
                fachada.CrearAdmin("Admin1_2");
                fachada.CrearAdmin("Admin1_2");
            });
            Assert.That("El Admin ya existe",Is.EqualTo(ex.Message));
        }
        
        /// <summary>
        /// Prueba a que salte una exepcion al crear un usuario con un amdin que no existe
        /// </summary>
        [Test]
        public void CrearUsuarioSinAdministrador()
        {
            Exception ex = Assert.Throws<Exception>(() =>
            {
                fachada.CrearUsuario("Admin","Juan", "Pérez", "juan@ucu.edu.uy");
            });
            Assert.That("Admin no encontrado.",Is.EqualTo(ex.Message));

        }

        /// <summary>
        /// Suspende a un usuario x y verifica si esta suspendido 
        /// </summary>
        [Test]
        public void SuspenderUsuarioyDeberiaMarcarUsuarioComoSuspendido()
        {
            
            fachada.CrearUsuario(nameAdmin, "Ana", "López", "anasusp1@ucu.edu.uy");

            fachada.SuspenderUsuario(nameAdmin, "anasusp1@ucu.edu.uy");

            var bd = BaseDatosUsuario.Instance;
            var usuario = bd.UsuarioSegunCorreo("anasusp1@ucu.edu.uy");

            Assert.That(usuario.Suspendido, Is.True);

        }
        /// <summary>
        /// Prueba que lanze uan exepcion al suspender un usuario inexistente
        /// </summary>
        [Test]
        public void SuspenderUsuarioInexistente()
        {
            Exception ex = Assert.Throws<Exception>(() =>
            {
                
                fachada.SuspenderUsuario(nameAdmin, "anasuspnoexiste@ucu.edu.uy");
            });
            Assert.That("No se ha encontrado el usuario",Is.EqualTo(ex.Message));

        }

        /// <summary>
        /// DEFENSAPROYECTO: testea la nueva historia de usuario
        /// tambien testea que en caso repetido salga el primero que fue registrado
        /// </summary>
        /// <returns></returns>
        [Test]
        public void UsuarioConMasVentas()
        {
            AdminFachada.Instance.CrearAdmin("AdminDEFENSA");
            AdminFachada.Instance.CrearUsuario("AdminDEFENSA","Patricio","Barron","DEFENSA1USER@");
            AdminFachada.Instance.CrearUsuario("AdminDEFENSA","Enzo","Olivera","DEFENSA2USER@");
            AdminFachada.Instance.CrearUsuario("AdminDEFENSA","Tiziano","Soto","DEFENSA3USER@");
            AdminFachada.Instance.CrearUsuario("AdminDEFENSA","Emmanuel","MeOlvidePerdonEmma","DEFENSA4USER@");
            
            UsuarioFachada.Instance.CrearCliente("DEFENSA1USER@","test","test",1241,"DEFENSA1CLIENTE@");
            UsuarioFachada.Instance.CrearCliente("DEFENSA2USER@","test","test",1242,"DEFENSA2CLIENTE@@");
            UsuarioFachada.Instance.CrearCliente("DEFENSA3USER@","test","test",1243,"DEFENSA3CLIENTE@");
            UsuarioFachada.Instance.CrearCliente("DEFENSA4USER@","test","test",1244,"DEFENSA4CLIENTE@");
            
            UsuarioFachada.Instance.CrearVenta("DEFENSA1USER@","Prueba","Prueba","1/2/2025",1241,13,"PruebaP1");
            UsuarioFachada.Instance.CrearVenta("DEFENSA1USER@","Prueba","Prueba","1/2/2025",1241,13,"PruebaP2");
            UsuarioFachada.Instance.CrearVenta("DEFENSA1USER@","Prueba","Prueba","1/2/2025",1241,13,"PruebaP3");
            UsuarioFachada.Instance.CrearVenta("DEFENSA1USER@","Prueba","Prueba","1/2/2025",1241,13,"PruebaP4");
            
            UsuarioFachada.Instance.CrearVenta("DEFENSA2USER@","Prueba","Prueba","1/2/2025",1242,13,"PruebaE1");
            UsuarioFachada.Instance.CrearVenta("DEFENSA2USER@","Prueba","Prueba","1/2/2025",1242,13,"PruebaE2");
            UsuarioFachada.Instance.CrearVenta("DEFENSA2USER@","Prueba","Prueba","1/2/2025",1242,13,"PruebaE3");
            UsuarioFachada.Instance.CrearVenta("DEFENSA2USER@","Prueba","Prueba","1/2/2025",1242,13,"PruebaE4");
            
            UsuarioFachada.Instance.CrearVenta("DEFENSA3USER@","Prueba","Prueba","1/2/2025",1243,13,"PruebaT1");
            UsuarioFachada.Instance.CrearVenta("DEFENSA3USER@","Prueba","Prueba","1/2/2025",1243,13,"PruebaT2");
            
            UsuarioFachada.Instance.CrearVenta("DEFENSA4USER@","Prueba","Prueba","1/2/2025",1244,13,"PruebaEm1");


            CantidadDeVentasUsuario test = AdminFachada.Instance.VendedorConMasVentas();
            Assert.That(test.Usuario.Nombre, Is.EqualTo("Patricio"));
            Assert.That(test.CantidadVentaBono, Is.EqualTo(400));










        }
    }
}