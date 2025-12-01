using System;
using NUnit.Framework;
using Library;

namespace LibraryTests
{
    /*
    /// <summary>
    /// Se validan los métodos de creación de interacciones (reuniones, correos, llamadas y mensajes),
    /// asegurando que se instancien correctamente y manejen adecuadamente los errores de formato o argumentos.
    /// </summary>
    [TestFixture]
    public class InteraccionFachadaTests
    {
        private InteraccionFachada fachada;

        /// <summary>
        /// Inicializa una nueva instancia de la fachada antes de cada test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            fachada = InteraccionFachada.Instance;
        }


        //REUNIÓN


        /// <summary>
        /// Verifica que el método cree una instancia válida cuando los datos son correctos.
        /// </summary>
        [Test]
        public void CrearReunion_DatosValidos_CreaInstanciaCorrectamente()
        {
            string fecha = "2025-11-10";
            string tema = "Plan de ventas";
            string notas = "Revisión trimestral";
            int telCliente = 123456;
            string lugar = "Oficina Central";

            var reunion = fachada.CrearReunion(fecha, tema, notas, telCliente, lugar);

            Assert.That(reunion, Is.Not.Null);
            Assert.That(reunion.Fecha, Is.EqualTo(DateTime.Parse(fecha)));
            Assert.That(reunion.Tema, Is.EqualTo(tema));
            Assert.That(reunion.Notas, Is.EqualTo(notas));
            Assert.That(reunion.TelCliente, Is.EqualTo(telCliente));
            Assert.That(reunion.Lugar, Is.EqualTo(lugar));
        }

        /// <summary>
        /// Verifica que se lance una excepción de formato si la fecha proporcionada no es válida
        /// al crear una reunión.
        /// </summary>
        [Test]
        public void CrearReunion_FechaInvalida_LanzaExcepcion()
        {
            Assert.Throws<FormatException>(() =>
            {
                fachada.CrearReunion("fecha_invalida", "tema", "notas", 123, "lugar");
            });
        }


        //CORREO


        /// <summary>
        /// Verifica que el método cree correctamente una instancia de correo con datos válidos.
        /// </summary>
        [Test]
        public void CrearCorreo_DatosValidos_CreaInstanciaCorrectamente()
        {
            string remitente = "Usuario";
            string fecha = "2025-11-10";
            string tema = "Presupuesto";
            string notas = "Adjunto presupuesto actualizado";
            int telCliente = 123456;
            bool respondido = true;

            var correo = fachada.CrearCorreo(remitente, fecha, tema, telCliente, notas, respondido);

            Assert.That(correo, Is.Not.Null);
            Assert.That(correo.Fecha, Is.EqualTo(DateTime.Parse(fecha)));
            Assert.That(correo.Tema, Is.EqualTo(tema));
            Assert.That(correo.Notas, Is.EqualTo(notas));
            Assert.That(correo.TelCliente, Is.EqualTo(telCliente));
            Assert.That(correo.Respondida, Is.True);
            Assert.That(correo.Remitente, Is.EqualTo(UsuarioOCliente.Usuario));
        }

        /// <summary>
        /// Verifica que se lance una excepción si el remitente proporcionado no es válido
        /// al intentar crear un correo.
        /// </summary>
        [Test]
        public void CrearCorreo_RemitenteInvalido_LanzaExcepcion()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                fachada.CrearCorreo("Invalido", "2025-11-10", "tema", 123, "notas", false);
            });
        }


        //LLAMADA


        /// <summary>
        /// Verifica que el método cree correctamente una instancia de llamada con datos válidos.
        /// </summary>
        [Test]
        public void CrearLlamada_DatosValidos_CreaInstanciaCorrectamente()
        {
            string remitente = "Cliente";
            string fecha = "2025-11-10";
            string tema = "Consulta de producto";
            string notas = "Cliente llamó para consultar precios";
            int telCliente = 987654;
            bool respondido = false;

            var llamada = fachada.CrearLlamada(remitente, fecha, tema, telCliente, notas, respondido);

            Assert.That(llamada, Is.Not.Null);
            Assert.That(llamada.Fecha, Is.EqualTo(DateTime.Parse(fecha)));
            Assert.That(llamada.Tema, Is.EqualTo(tema));
            Assert.That(llamada.Notas, Is.EqualTo(notas));
            Assert.That(llamada.TelCliente, Is.EqualTo(telCliente));
            Assert.That(llamada.Respondida, Is.False);
            Assert.That(llamada.Remitente, Is.EqualTo(UsuarioOCliente.Cliente));
        }

        /// <summary>
        /// Verifica que se lance una excepción de formato si la fecha proporcionada es inválida
        /// al crear una llamada.
        /// </summary>
        [Test]
        public void CrearLlamada_FechaInvalida_LanzaExcepcion()
        {
            Assert.Throws<FormatException>(() =>
            {
                fachada.CrearLlamada("Usuario", "fecha_mala", "tema", 1, "notas", false);
            });
        }


        //MENSAJE


        /// <summary>
        /// Verifica que el método cree correctamente una instancia de mensaje cuando los datos son válidos.
        /// </summary>
        [Test]
        public void CrearMensaje_DatosValidos_CreaInstanciaCorrectamente()
        {
            string remitente = "Usuario";
            string fecha = "2025-11-10";
            string tema = "Recordatorio";
            string notas = "Recordar reunión semanal";
            int telCliente = 456789;
            bool respondido = false;

            var mensaje = fachada.CrearMensaje(remitente, fecha, tema, telCliente, notas, respondido);

            Assert.That(mensaje, Is.Not.Null);
            Assert.That(mensaje.Fecha, Is.EqualTo(DateTime.Parse(fecha)));
            Assert.That(mensaje.Tema, Is.EqualTo(tema));
            Assert.That(mensaje.Notas, Is.EqualTo(notas));
            Assert.That(mensaje.TelCliente, Is.EqualTo(telCliente));
            Assert.That(mensaje.Respondida, Is.False);
            Assert.That(mensaje.Remitente, Is.EqualTo(UsuarioOCliente.Usuario));
        }

        /// <summary>
        /// Verifica que se lance una excepción si el remitente es inválido al crear un mensaje.
        /// </summary>
        [Test]
        public void CrearMensaje_RemitenteInvalido_LanzaExcepcion()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                fachada.CrearMensaje("Desconocido", "2025-11-10", "tema", 1, "notas", false);
            });
        }
    }*/
}

