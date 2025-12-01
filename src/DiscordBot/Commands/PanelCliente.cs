using System;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class PanelCliente : ModuleBase<SocketCommandContext>
    {
        [Command("PanelCliente")]
        [Discord.Commands.Summary("Muestra la informacion del panel del Usuario")]
        public async Task ExecuteAsync(string correoUsuario = null)
        {
            if (correoUsuario == null )
            {
                await ReplyAsync("Uso correcto: !PanelCliente <correoUsuario>");
                return;
            }

            try
            {
                await ReplyAsync($"{UsuarioFachada.Instance.PanelCliente(correoUsuario)}");


            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al obtener informacion: {ex.Message}");

            }
        }
    }
}