using Discord.Commands;
using Library;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Interactions;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class FiltrarClienteCorreoCommand : ModuleBase<SocketCommandContext>
    {
        [Command("FiltrarClienteCorreo")]
        [Discord.Commands.Summary("Filtra un cliente usando su correo, validando al usuario solicitante.")]
        public async Task ExecuteAsync(string correoUsuario = null, 
                                        string correoCliente = null)
        {
            if (correoUsuario == null || correoCliente == null)
            {
                await ReplyAsync("Uso correcto: !FiltrarClienteCorreo <correoUsuario> <correoCliente>");
                return;
            }

            try
            {
                Cliente cliente = UsuarioFachada.Instance.FiltarClienteCorreo(correoUsuario, correoCliente);

                if (cliente == null)
                {
                    await ReplyAsync("No se encontró un cliente con ese correo.");
                    return;
                }

                await ReplyAsync(
                    $"Cliente encontrado: **{cliente.Nombre} {cliente.Apellido}** (correo: {cliente.Correo}).");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al filtrar cliente: {ex.Message}");
            }
        }
    }
}