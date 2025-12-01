using Discord.Commands;
using Library;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Interactions;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class FiltrarClienteTel : ModuleBase<SocketCommandContext>
    {
        [Command("FiltrarClienteTel")]
        [Discord.Commands.Summary("Filtra un cliente usando su telefono, validando al usuario solicitante.")]
        public async Task ExecuteAsync(string correoUsuario = null, 
            string telefono = null)
        {
            int telCliente;
            bool convercionexitosa = int.TryParse(telefono, out telCliente);
            if (correoUsuario == null || convercionexitosa == false)
            {
                await ReplyAsync("Uso correcto: !FiltrarClienteCorreo <correoUsuario> <correoCliente>");
                return;
            }

            try
            {
                Cliente cliente = UsuarioFachada.Instance.FiltrarClienteTelefono(correoUsuario, telCliente);

                if (cliente == null)
                {
                    await ReplyAsync("No se encontró un cliente con ese telefono.");
                    return;
                }

                await ReplyAsync(
                    $"Cliente encontrado: **{cliente.Nombre} {cliente.Apellido}** (Telefono: {cliente.Tel}).");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al filtrar cliente: {ex.Message}");
            }
        }
    }
}