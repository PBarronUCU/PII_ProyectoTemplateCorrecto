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
    }
}