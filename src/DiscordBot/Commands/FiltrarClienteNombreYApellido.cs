using Discord.Commands;
using Library;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Discord.Interactions;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class FiltrarClienteNombreYApellido : ModuleBase<SocketCommandContext>
    {
        [Command("FiltrarClienteNombre")]
        [Discord.Commands.Summary("Filtra un cliente usando su Nombre y Apellido, validando al usuario solicitante.")]
        public async Task ExecuteAsync(string correoUsuario = null,
            string nombre = null, string apellido =null)
        {
         
            if (correoUsuario == null || nombre == null || apellido == null)
            {
                await ReplyAsync("Uso correcto: !FiltrarClienteCorreo <correoUsuario> <NombreCliente> <ApellidoCliente>");
                return;
            }

            try
            {
                var clientes = UsuarioFachada.Instance.FiltrarClienteNombre(correoUsuario, nombre,apellido);

                if (clientes == null || clientes.Count == 0)
                {
                    await ReplyAsync("No se encontró ningún cliente con ese nombre y apellido.");
                    return;
                }

                if (clientes.Count == 1)
                {
                    var c = clientes[0];
                    await ReplyAsync($"Cliente encontrado: **{c.Nombre} {c.Apellido}** (Telefono: {c.Tel}).");
                }
                else
                {
                    var lines = clientes.Select(c => $"• {c.Nombre} {c.Apellido} (Tel: {c.Tel})");
                    await ReplyAsync("Se encontraron varios clientes:\n" + string.Join("\n", lines));
                }
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al filtrar cliente: {ex.Message}");
            }
        }
    }
}