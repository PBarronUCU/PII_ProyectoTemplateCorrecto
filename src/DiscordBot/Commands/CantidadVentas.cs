using System;
using Discord.Commands;
using Library;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Interactions;
using System.Linq;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CantidadVentas: ModuleBase<SocketCommandContext>
    {
        [Command("CantidadVentas")]
        [Discord.Commands.Summary(
            "Un admimn puede ver todas las venats realizadas en orden disminuyente en orden de quien realizo mas ventas")]
        public async Task ExecuteAsync(string nombreAdmin = null)
        {
            if (nombreAdmin == null)
            {
                ReplyAsync("Uso Correcto: **!CantidadVentas <Admin>");
                return;
            }

            try
            {
                AdminFachada.Instance.VeralmejotVendedor(nombreAdmin);
                await ReplyAsync(
                    $"El usuario con mayor ventas fue **{}** con un total de **{}** por lo que recibira un bono de **${}**");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al obtener el usuario con mayor ventas: {ex.Message}");
            }
        }
    }
}