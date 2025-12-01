using Discord.Commands;
using Library;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Interactions;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class InteraccionClienteSinFiltroCommand : ModuleBase<SocketCommandContext>
    {
        [Command("InteraccionesCliente")]
        [Discord.Commands.Summary("Indica si un cliente tiene interacciones registradas.")]
        public async Task ExecuteAsync(string correoUsuario = null,
            int telefonoCliente = 0)
        {
            if (correoUsuario == null || telefonoCliente == 0)
            {
                await ReplyAsync("Uso correcto: !InteraccionesCliente <correoUsuario> <telefonoCliente>");
                return;
            }

            try
            {
                var interacciones = UsuarioFachada.Instance
                    .InteracionClienteSinFiltro(correoUsuario, telefonoCliente);

                if (interacciones == null || interacciones.Count == 0)
                {
                    await ReplyAsync("El cliente no tiene interacciones registradas.");
                    return;
                }

                await ReplyAsync("El cliente **sí** tiene interacciones registradas.");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al obtener interacciones: {ex.Message}");
            }
        }
    }
}