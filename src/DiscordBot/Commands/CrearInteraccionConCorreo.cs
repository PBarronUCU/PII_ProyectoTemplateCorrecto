using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Discord.Interactions;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CrearInteraccionCorreo : ModuleBase<SocketCommandContext>
    {
        [Command("CrearInteraccionCorreo")]
        [Discord.Commands.Summary("Un usuario crea un registro de correo con un cliente.")]
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
                await ReplyAsync("Uso correcto: !CrearInteraccionCorreo <respondido> <correoUsuario> <telefono> <remitente> <fecha> <nota> <tema>");
                return;
            }

            try
            {
                UsuarioFachada.Instance.CrearCorreo(correoUsuario, remitente, fecha, tema, telefonoCliente, nota, respondido);

                if (remitente == "Cliente")
                {
                    await ReplyAsync($"El correo del cliente con el número {telefonoCliente}, fue registrado correctamente.");
                }
                else
                {
                    await ReplyAsync($"El correo al cliente con el número {telefonoCliente}, fue registrado correctamente.");
                }
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"Error al registrar la interacción Correo: {ex.Message}");
            }
        }
    }
}