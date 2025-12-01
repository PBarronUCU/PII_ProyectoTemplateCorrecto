using Discord.Commands;
using Library;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Discord.Interactions;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class FiltrarInteracionsinResponder: ModuleBase<SocketCommandContext>

    {
        [Command("InteraSinResponder")]
        [Discord.Commands.Summary("Filtralas interacciones sin Responder del Usuario")]
        public async Task ExecuteAsync(string correoUsuario = null)
        {
            if (correoUsuario == null )
            {
                await ReplyAsync("Uso correcto: !FiltrarInteracionSinResponder <correoUsuario>");
                return;
            }

            try
            {
                var interacciones =
                    UsuarioFachada.Instance.InteraSinResponder(correoUsuario);
                if (interacciones == null || interacciones.Count == 0)
                {
                    await ReplyAsync("El Usuario no tiene interacciones Sin Responder.");
                    return;
                }

                // Construir lista detallada
                var listaDetalles = interacciones.Select(inter =>
                    $"**Interacción ({inter.GetType().Name})**\n" +
                    $"• **Fecha:** {inter.Fecha}\n" +
                    $"• **Tema:** {inter.Tema}\n" +
                    $"• **Notas:** {inter.Notas}\n" +
                    $"• **Cliente (tel):** {inter.Cliente?.Tel}\n"
                );

                string mensaje = "**Interacciones del cliente:**\n\n" +
                                 string.Join("\n", listaDetalles);

                await ReplyAsync(mensaje);
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al obtener interacciones: {ex.Message}");
            }
        }

    }
}