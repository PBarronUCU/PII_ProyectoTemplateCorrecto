using System;
using NUnit.Framework;
using Library;
namespace LibraryTests
{
    [TestFixture]
    public class Adminfachada_test
    {
        private AdminFachada fachada;   
        [SetUp]
        public void SetUp()
        {
            AdminFachada.ResetInstance();
            UsuarioFachada.ResetInstance();
            BaseDatosCliente.ResetInstance();
            BaseDatosUsuario.ResetInstance();
            BaseDatosAdmin.ResetInstance();
            BaseDatosVenta.ResetInstance();
            fachada= AdminFachada.Instance;
        }
        
        /// <summary>
        /// Crea un usuario lo arega a la base de datosy revisa si el usuario se creo exitosamente
        /// </summary>
        [Test]
        public void CrearUsuariyAgregarUsuarioEnBaseDatos()
        {
            fachada.CrearAdmin("Admin1");
            BaseDatosUsuario bdUser = BaseDatosUsuario.Instance;
            fachada.CrearUsuario("Admin1","Juan", "Pérez", "juan@ucu.edu.uy");

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
            Exception ex = Assert.Throws<Exception>(() =>
            {
                fachada.CrearAdmin("Admin1");
                fachada.CrearUsuario("Admin1","Juan", "Pérez", "juan@ucu.edu.uy");
                fachada.CrearUsuario("Admin1","Patri", "Soto", "juan@ucu.edu.uy");
            });
            Assert.That("Este correo ya esta en uso",Is.EqualTo(ex.Message));

        }
        
        /// <summary>
        /// Prueba a que salte una exepcion al crear dos admins con el mismo nombre
        /// </summary>
        [Test]
        public void CrearAdminRepetido()
        {
            Exception ex = Assert.Throws<Exception>(() =>
            {
                fachada.CrearAdmin("Admin1");
                fachada.CrearAdmin("Admin1");
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
                fachada.CrearUsuario("Admin1","Juan", "Pérez", "juan@ucu.edu.uy");
            });
            Assert.That("Admin no encontrado.",Is.EqualTo(ex.Message));

        }

        /// <summary>
        /// Suspende a un usuario x y verifica si esta suspendido 
        /// </summary>
        [Test]
        public void SuspenderUsuarioyDeberiaMarcarUsuarioComoSuspendido()
        {
            
            fachada.CrearAdmin("Admin1");
            fachada.CrearUsuario("Admin1", "Ana", "López", "ana@ucu.edu.uy");

            fachada.SuspenderUsuario("Admin1", "ana@ucu.edu.uy");

            var bd = BaseDatosUsuario.Instance;
            var usuario = bd.UsuarioSegunCorreo("ana@ucu.edu.uy");

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
                fachada.CrearAdmin("Admin1");
                fachada.SuspenderUsuario("Admin1", "ana@ucu.edu.uy");
            });
            Assert.That("No se ha encontrado el usuario",Is.EqualTo(ex.Message));

        }
    }
}