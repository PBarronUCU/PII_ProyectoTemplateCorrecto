using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class VendedorConMasVentasCommand : ModuleBase<SocketCommandContext>
    {
        [Command("VendedorConMasVentas")]
        [Discord.Commands.Summary("El administrador obtiene el vendedor con más ventas")]
        public async Task EjecutarAsync(string Admin.correo)
        {
            
            public ResultadoVendedorVenta ObtenerVendedorConMasVentas(string correoAdmin)
            {
                var admin = BaseDatosAdmin.Instance.AdminSegunNombre(correoAdmin);
                if (admin == null) throw new Exception("Administrador no encontrado");

                var ventas = BaseDatosVenta.Instance.ListaVentas;
                if (ventas.Count == 0) throw new Exception("No existen ventas registradas");

                var agrupadas = ventas.GroupBy(v => v.Usuario);
                var mayor = agrupadas.OrderByDescending(g => g.Count()).First();

                var vendedor = mayor.Key;
                var cantidad = mayor.Count();
                var bono = cantidad * 100;

                return new ResultadoVendedorVenta(vendedor, cantidad, bono);
            }

            try
            {
                var resultado = UsuarioFachada.Instance.ObtenerVendedorConMasVentas(Admin.correo);
                await ReplyAsync($"Vendedor: {resultado.Vendedor.Nombre} {resultado.Vendedor.Apellido}. Cantidad de ventas: {resultado.CantidadVentas}. Bono: ${resultado.Bono}.");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync(ex.Message);
            }
        }
    }
}
