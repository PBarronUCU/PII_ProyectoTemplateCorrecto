using System;
using NUnit.Framework;
using Library;
using System.Linq;

namespace LibraryTests
{
    [TestFixture]
    public class AdminFachada_MejorVendedor_Tests
    {
        private AdminFachada adminFachada;
        private string nombreAdmin = "AdminTestMV";

        [SetUp]
        public void SetUp()
        {
            // resetear todas las bases para evitar contaminación entre tests
            BaseDatosAdmin.Instance.Reset();
            BaseDatosUsuario.Instance.Reset();
            BaseDatosCliente.Instance.Reset();
            BaseDatosVenta.Instance.Reset();

            adminFachada = AdminFachada.Instance;
            adminFachada.CrearAdmin(nombreAdmin);
        }

        [Test]
        public MejorVendedorResultado VendedorConMasVentas()
{
    var ventas = BaseDatosVenta.Instance.ObtenerTodasLasVentas();

    if (ventas == null || ventas.Count == 0)
        throw new Exception("No hay ventas registradas.");

    // Lista paralela para correos y conteos
    List<string> correos = new List<string>();
    List<int> conteoVentas = new List<int>();
    List<Usuario> vendedores = new List<Usuario>();

    // Contamos manualmente
    foreach (var venta in ventas)
    {
        if (venta == null || venta.Usuario == null || string.IsNullOrEmpty(venta.Usuario.Correo))
            continue;

        string correo = venta.Usuario.Correo;

        // Buscar si ya está en la lista
        int indice = -1;
        for (int i = 0; i < correos.Count; i++)
        {
            if (correos[i] == correo)
            {
                indice = i;
                break;
            }
        }

        // Si no existe, lo agregamos
        if (indice == -1)
        {
            correos.Add(correo);
            vendedores.Add(venta.Usuario);
            conteoVentas.Add(1);
        }
        else
        {
            conteoVentas[indice]++;
        }
    }

    if (correos.Count == 0)
        throw new Exception("No hay ventas válidas con vendedor asignado.");

    // Buscar el índice del máximo
    int maxVentas = conteoVentas[0];
    int indiceMax = 0;

    for (int i = 1; i < conteoVentas.Count; i++)
    {
        if (conteoVentas[i] > maxVentas)
        {
            maxVentas = conteoVentas[i];
            indiceMax = i;
        }
    }

    return new MejorVendedorResultado
    {
        Vendedor = vendedores[indiceMax],
        CantidadVentas = maxVentas,
        Bono = maxVentas * 100
    };
}



        [Test]
        public void VendedorConMasVentas_SinVentas_LanzaExcepcion()
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                adminFachada.VendedorConMasVentas();
            });

            Assert.That(ex.Message, Is.EqualTo("No hay ventas registradas."));
        }

        [Test]
        public void VendedorConMasVentas_VentasSinUsuarioIgnoradas()
        {
            // crear vendedor
            adminFachada.CrearUsuario(nombreAdmin, "V1", "A", "v1@test2.com");
            var u1 = BaseDatosUsuario.Instance.UsuarioSegunCorreo("v1@test2.com");

            // cliente
            BaseDatosCliente.Instance.CrearCliente("Cliente", "X", "c2@test.com", 2001);
            var cliente = BaseDatosCliente.Instance.ClienteSegunTelefono(2001);

            // crear una venta con usuario null (simulamos agregando directamente)
            var ventaInvalida = new Venta(null, "Tinv", "Ninv", DateTime.Parse("2025-01-05"), cliente, 10.0, "Pinv");
            BaseDatosVenta.Instance.AgregarVenta(ventaInvalida);

            // crear ventas válidas
            BaseDatosVenta.Instance.CrearVentas(u1, "T1", "N1", DateTime.Parse("2025-01-06"), cliente, 50.0, "P1");

            var resultado = adminFachada.VendedorConMasVentas();

            Assert.That().IsNotNull(resultado);
            Assert.AreEqual("v1@test2.com", resultado.Vendedor.Correo);
            Assert.AreEqual(1, resultado.CantidadVentas);
            Assert.AreEqual(100, resultado.Bono);
        }
    }
}
