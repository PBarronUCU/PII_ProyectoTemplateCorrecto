using System;
using NUnit.Framework;
using Library;
namespace LibraryTests
{
    public class Adminfachada_test
    {
        [SetUp]
        public void Setup()
        {
        
        }
        
        /// <summary>
        /// Crea un usuario lo arega a la base de datosy revisa si el usuario se creo exitosamente
        /// </summary>
        [Test]
        public void CrearUsuariyAgregarUsuarioEnBaseDatos()
        {
            AdminFachada.CrearUsuario("Juan", "Pérez", "juan@ucu.edu.uy");

            var usuario = BaseDatosUsuario.Instance.UsuarioSegunCorreo("juan@ucu.edu.uy");

            Assert.That(usuario, Is.Not.Null);
            Assert.That(usuario.Nombre, Is.EqualTo("Juan"));
        }

        /// <summary>
        /// Suspende a un usuario x y verifica si esta suspendido 
        /// </summary>
        [Test]
        public void SuspenderUsuarioyDeberiaMarcarUsuarioComoSuspendido()
        {
    
            var usuario = new Usuario("Ana", "López", "ana@ucu.edu.uy");
            AdminFachada.SuspenderUsuario("ana@ucu.edu.uy");

            Assert.That(usuario.Suspendido, Is.True);
        }
        
    }
}