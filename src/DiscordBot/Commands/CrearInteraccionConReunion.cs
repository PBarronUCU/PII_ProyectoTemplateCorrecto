using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Discord.Interactions;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CrearInteraccionReunion : ModuleBase<SocketCommandContext>
    {
        [Command("CrearInteraccionReunion")]
        [Discord.Commands.Summary("Un usuario crea un registro de reunión con un cliente.")]
        public async Task ExecuteAsync(string correoUsuario = null, string telefono = null, string remitente = null, string fecha = null, string nota = null, string tema = null, string lugar = null)
        {
            int telefonoCliente;
            bool conversionexitosa = int.TryParse(telefono, out telefonoCliente);

            if (correoUsuario == null || conversionexitosa == false ||
                remitente == null || fecha == null || nota == null || tema == null || lugar == null)
            {
                await ReplyAsync(
                    "Uso correcto: !CrearInteraccionReunion <correoUsuario> <telefono> <remitente> <fecha> <nota> <tema> <lugar>");
                return;
            }

            try
            {
                UsuarioFachada.Instance.CrearReunion(correoUsuario,fecha, tema, nota, telefonoCliente, lugar);

                await ReplyAsync(
                    $"La reunión con el cliente {telefonoCliente} fue registrada correctamente."
                );
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"Error al registrar la reunión: {ex.Message}");
            }
        }
    }
}
