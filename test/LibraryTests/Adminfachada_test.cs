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
            // Si tus bases son singleton, podés limpiar datos previos
            // BaseDatosUsuario.Instance.Reset();
        }

        [Test]
        public void CrearUsuario_DeberiaAgregarUsuarioEnBaseDatos()
        {
            AdminFachada.CrearUsuario("Juan", "Pérez", "juan@ucu.edu.uy");

            var usuario = BaseDatosUsuario.Instance.UsuarioSegunCorreo("juan@ucu.edu.uy");

            Assert.That(usuario, Is.Not.Null);
            Assert.That(usuario.Nombre, Is.EqualTo("Juan"));
        }

        [Test]
        public void SuspenderUsuario_DeberiaMarcarUsuarioComoSuspendido()
        {
    
            var usuario = new Usuario("Ana", "López", "ana@ucu.edu.uy");
            AdminFachada.SuspenderUsuario("ana@ucu.edu.uy");

            Assert.That(usuario.Suspendido, Is.True);
        }
        
    }
}