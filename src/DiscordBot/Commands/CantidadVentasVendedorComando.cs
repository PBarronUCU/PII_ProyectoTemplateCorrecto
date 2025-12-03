using Discord.Commands;
using Library;
using System.Threading.Tasks;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class MejorVendedorCommand : ModuleBase<SocketCommandContext>
    {
        [Command("MejorVendedor")]
        [Discord.Commands.Summary("Muestra el vendedor con mayor cantidad de ventas y su bono.")]
        public async Task ExecuteAsync()
        {
            try
            {
                var resultado = AdminFachada.Instance.VendedorConMasVentas();

                if (resultado == null || resultado.Vendedor == null)
                {
                    await ReplyAsync("No se pudo determinar el mejor vendedor.");
                    return;
                }

                await ReplyAsync(
                    $" **Mejor vendedor:** {resultado.Vendedor.Correo}\n" +
                    $" **Cantidad de ventas:** {resultado.CantidadVentas}\n" +
                    $" **Bono obtenido:** ${resultado.Bono}"
                );
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠ Error: {ex.Message}");
            }
        }
    }
}