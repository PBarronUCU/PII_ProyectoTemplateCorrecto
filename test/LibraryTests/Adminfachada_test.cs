using System;
using NUnit.Framework;
using Library;
namespace LibraryTests
{
    public class Adminfachada_test
    {
        private AdminFachada fachada;   
        [SetUp]
        public void SetUp()
        {
            fachada =  AdminFachada.Instance;
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
        /// Suspende a un usuario x y verifica si esta suspendido 
        /// </summary>
        [Test]
        public void SuspenderUsuarioyDeberiaMarcarUsuarioComoSuspendido()
        {
            
            fachada.CrearAdmin("Admin1");
            var usuario = new Usuario("Ana", "López", "ana@ucu.edu.uy");
            fachada.SuspenderUsuario("Admin1", "ana@ucu.edu.uy");

            Assert.That(usuario.Suspendido, Is.True);
        }
        
    }
}