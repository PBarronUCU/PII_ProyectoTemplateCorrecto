using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands{
    
    public class SuspenderCommand : ModuleBase<SocketCommandContext>
    {
        [Command("SuspenderUsuario")]
        [Discord.Commands.Summary("Suspende un usuario existente a través del administrador indicado.")]
        public async Task ExecuteAsync(string nombreAdmin = null, string correo = null)
        
        {
            if (nombreAdmin == null || correo == null)
            {
                await ReplyAsync("Uso correcto: !SuspenderUsuario <nombreAdmin> <correo>");
                return;
            }

            try
            {
                AdminFachada.Instance.SuspenderUsuario(nombreAdmin,correo);
                await ReplyAsync($"Usuario Correo:**{correo}** suspendido con éxito por el admin **{nombreAdmin}**");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al suspender al usuario: {ex.Message}");
            }
        }
    }
}