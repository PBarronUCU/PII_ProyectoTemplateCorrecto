using System;
using Discord.Commands;
using Library;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Discord.Interactions;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class VentasPeriodo: ModuleBase<SocketCommandContext>
    {
        [Command("VentasPeriodo")]
        [Discord.Commands.Summary("Filtra las ventas realisadas en un periodo de Tiempo")]
        public async Task ExecuteAsync(string correoUsuario = null, string fechabaja = null, string fechaalta = null)
        {
            if (correoUsuario == null || fechabaja == null || fechaalta == null)
            {
                await ReplyAsync("Uso correcto: !VentaPeriodp <correoUsuario> <Fecha1> <Fecha2>");
                return;
            }

            try
            {
                var ventas = UsuarioFachada.Instance.VentasPeriodo(correoUsuario, fechabaja, fechaalta);
                if (ventas == null || ventas.Count == 0)
                {
                    await ReplyAsync("El Usuario no tiene ventas entre las fechas indicadas");
                    return;
                }

                var listaDetalles = ventas.Select(venta =>
                    $"**Venta de :({venta.Producto})**\n" +
                    $"• **Fecha:** {venta.Fecha}\n" +
                    $"• ** Cliente:{venta.Cliente?.Tel}\n" +
                    $"• ** Precio:{venta.Precio}\n"
                );
                string mensaje = "**Ventas al cliente:**\n\n" +
                                 string.Join("\n", listaDetalles);

                await ReplyAsync(mensaje);

            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al obtener PeriodoVenta: {ex.Message}");
            }
        }
        
    }
}