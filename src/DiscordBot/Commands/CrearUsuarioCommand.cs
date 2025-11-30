using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CrearUsuarioCommand : ModuleBase<SocketCommandContext>
    {
        [Command("CrearUsuario")]
        [Discord.Commands.Summary("Crea un usuario usando el administrador indicado.")]
        public async Task ExecuteAsync(
            string nombreAdmin = null,
            string nombre = null,
            string apellido = null,
            string correo = null)
        {
            if (nombreAdmin == null || nombre == null || apellido == null || correo == null)
            {
                await ReplyAsync("Uso correcto: !CrearUsuario <admin> <nombre> <apellido> <correo>");
                return;
            }

            try
            {
                AdminFachada.Instance.CrearUsuario(nombreAdmin, nombre, apellido, correo);

                await ReplyAsync(
                    $"Usuario **{nombre} {apellido}** creado con éxito por el admin **{nombreAdmin}**.");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al crear el usuario: {ex.Message}");
            }
        }
    }
}