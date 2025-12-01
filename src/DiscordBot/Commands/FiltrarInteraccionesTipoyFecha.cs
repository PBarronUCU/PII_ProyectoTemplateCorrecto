
using Discord.Commands;
using Library;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Discord.Interactions;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class FiltrarInteracionClienteTipoyFecha: ModuleBase<SocketCommandContext>

    {
        [Command("FiltrarInteracionTipoyFecha")]
        [Discord.Commands.Summary("Filtralas interacciones con un Cliente de un tipo especifico y en una fecha en especial")]
        public async Task ExecuteAsync(string correoUsuario = null,string telefono = null, string tipo = null, string fecha = null)
        {
            int telCliente;
            bool convercionexitosa = int.TryParse(telefono, out telCliente);
            if (correoUsuario == null || convercionexitosa == false || tipo == null || fecha==null)
            {
                await ReplyAsync("Uso correcto: !FiltrarInteracionFecha <correoUsuario> <telefonCliente> <tipo> <fecha>");
                return;
            }

            try
            {
                var interacciones =
                    UsuarioFachada.Instance.InteracionClienteFiltroTipoFecha(correoUsuario, telCliente, tipo,fecha);
                if (interacciones == null || interacciones.Count == 0)
                {
                    await ReplyAsync("El cliente no tiene interacciones registradas de ese Tipo o en esa Fecha.");
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