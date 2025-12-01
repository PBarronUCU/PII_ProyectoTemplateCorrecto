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
            string telefonoCliente = null)
        {
            int telclient;
            int.TryParse(telefonoCliente, out telclient);

            if (correoUsuario == null || telefonoCliente == null)
            {
                await ReplyAsync("Uso correcto: !InteraccionesCliente <correoUsuario> <telefonoCliente>");
                return;
            }

            try
            {
                var interacciones = UsuarioFachada.Instance
                    .InteracionClienteSinFiltro(correoUsuario, telclient);

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