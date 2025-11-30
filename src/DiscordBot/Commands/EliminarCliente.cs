using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class EliminarCliente :ModuleBase<SocketCommandContext>
    {
        [Command("EliminarCliente")]
        [Discord.Commands.Summary("Un usuario elimina a un cliente ya existente")]
        public async Task ExecuteAsync(string correoUsuario, int telefono = 0)
        {
            if (correoUsuario == null || telefono == 0)
            {
                await ReplyAsync("Uso correcto: !EliminarCliente <correoUsuario> <telefono>");
                return;
            }

            try
            {
                UsuarioFachada.Instance.EliminarCliente(telefono);
                await ReplyAsync($"Cliente:**{telefono}** eliminado exitosamente por el usuario** {correoUsuario}");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"Error al eliminar Cliente: {ex.Message}");
            }
        }
    }
}