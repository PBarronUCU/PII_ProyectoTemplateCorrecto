
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class AsignarCliente : ModuleBase<SocketCommandContext>
    {
        [Command("AsignarCliente")]
        [Discord.Commands.Summary("Reasigna un cliente existenta a otro usuario")]
        public async Task ExecuteAsync(string correoUsuario1 = null, string telefono = null, string correoUsuario2 = null)
        {
            int telCliente;
            bool convercionexitosa = int.TryParse(telefono, out telCliente);
            if (correoUsuario1 == null || convercionexitosa == false || correoUsuario2 == null)
            {
                await ReplyAsync(
                    "Uso Correcto: !AsignarCliente <CorreoUsuarioactual> <TelCliente> <CorreoUsuarioNuevo>");
                return;
            }

            try
            {
                UsuarioFachada.Instance.AsignarCliente(correoUsuario1,correoUsuario2,telCliente);
                await ReplyAsync(
                    $"El Cliente **{telCliente}** fue reasignado del Usuario **{correoUsuario1}** al Usuario **{correoUsuario2}** ");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al Reasignar al Cliente: {ex.Message}");
                
            }
        }
    }
}