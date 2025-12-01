using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Discord.Interactions;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CrearInteraccionLlamada : ModuleBase<SocketCommandContext>
    {
        [Command("CrearInteraccionLlamada")]
        [Discord.Commands.Summary("Crea un registro de llamada entre un usuario y un cliente.")]
        public async Task ExecuteAsync(
            bool respondido = false,
            string correoUsuario = null,
            string telefono = null,
            string remitente = null,
            string fecha = null,
            string nota = null,
            string tema = null)
        {
            int telefonoCliente;
            bool conversionexitosa = int.TryParse(telefono, out telefonoCliente);

            if (correoUsuario == null || conversionexitosa == false ||
                remitente == null || fecha == null || nota == null || tema == null)
            {
                await ReplyAsync(
                    "Uso correcto: !CrearInteraccionLlamada <respondido> <correoUsuario> <telefono> <remitente> <fecha> <nota> <tema>");
                return;
            }

            try
            {
                UsuarioFachada.Instance.CrearLlamada(correoUsuario, remitente, fecha, tema, telefonoCliente, nota, respondido);

                if (remitente == "Cliente")
                {
                    await ReplyAsync($"La llamada del cliente {telefonoCliente} fue registrada correctamente.");
                }
                else
                {
                    await ReplyAsync($"La llamada al cliente {telefonoCliente} fue registrada correctamente.");
                }
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"Error al registrar la interacción Llamada: {ex.Message}");
            }
        }
    }
}
