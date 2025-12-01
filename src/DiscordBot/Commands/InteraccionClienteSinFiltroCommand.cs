using Discord.Commands;
using Library;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Interactions;
using System.Linq;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class InteraccionClienteSinFiltroCommand : ModuleBase<SocketCommandContext>
    {
        [Command("InteraccionesDelCliente")]
        [Discord.Commands.Summary("Te devuelve una lista detallada con las interacciones del cliente.")]
        public async Task ExecuteAsync(string correoUsuario = null, string telefonoCliente = null)
        {
            int telclient;

            bool conversionexitosa = int.TryParse(telefonoCliente, out telclient);

            if (correoUsuario == null || conversionexitosa == false)
            {
                await ReplyAsync("Uso correcto: !InteraccionesDelCliente <correoUsuario> <telefonoCliente>");
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

                // Construir lista detallada
                var listaDetalles = interacciones.Select(inter =>
                    $"**Interacción ({inter.GetType().Name})**\n" +
                    $"• **Fecha:** {inter.Fecha}\n" +
                    $"• **Tema:** {inter.Tema}\n" +
                    $"• **Notas:** {inter.Notas}\n" +
                    $"• **Cliente (tel):** {inter.Cliente?.Tel}\n"
                );

                string mensaje = "**Interacciones del cliente:**\n\n" +
                                 string.Join("\n", listaDetalles);

                await ReplyAsync(mensaje);
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al obtener interacciones: {ex.Message}");
            }
        }
    }
}
