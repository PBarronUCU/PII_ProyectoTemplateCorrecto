
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CrearInteraccionconClinete : ModuleBase<SocketCommandContext>
    {
        [Command("CrearInteracioneMensaje")]
        [Discord.Commands.Summary("un usuario crea el registro de interaccion con un cliente")]
        public async Task ExecuteAsync(bool respondido = false, string correoUsuario = null, string telefono = null,
            string remitente = null, string fecha = null,string nota= null, string tema = null)

    {
            int telefonoCliente;
            bool conversionexitosa = int.TryParse(telefono, out telefonoCliente);


            
            if (correoUsuario == null || conversionexitosa == false||remitente ==null || fecha ==null|| nota ==null || tema ==null )
            {
                await ReplyAsync("Uso correcto: !CrearInteracion <respondio> <correoUsuario> <telefono> <remitente> <fecha> <nota> <tema>");
                return;
            }

            try
            {
                UsuarioFachada.Instance.CrearMensaje(correoUsuario,remitente,fecha,tema,telefonoCliente,nota,respondido);
                if (remitente == "Cliente")
                {
                    await ReplyAsync(
                        $"El mensaje del cliente con el numero {telefonoCliente}, fue enviado correctamente");
                }
                else
                {
                    await ReplyAsync(
                        $"El mensaje al cliente con el numero {telefonoCliente}, fue enviado correctamente");
                }
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"Error al registrar la interaccion Mensaje: {ex.Message}");
            }
            
            
        }
    }
}