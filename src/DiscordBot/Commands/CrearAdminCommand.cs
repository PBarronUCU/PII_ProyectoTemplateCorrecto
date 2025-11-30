using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands{
    
    public class CrearAdminCommand : ModuleBase<SocketCommandContext>
    {
        [Command("CrearAdmin")]
        [Discord.Commands.Summary("Crea el administrador con el nombre indicado.")]
        public async Task ExecuteAsync(string nombreAdmin = null)
        {
            if (nombreAdmin == null)
            {
                await ReplyAsync("Uso correcto: !CrearAdmin <name>");
                return;
            }

            try
            {
                AdminFachada.Instance.CrearAdmin(nombreAdmin);
                await ReplyAsync($"Usuario **{nombreAdmin}** creado con éxito");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al crear el admin: {ex.Message}");
            }
        }
    }
}