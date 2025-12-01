using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CrearInteraccionconClinete : ModuleBase<SocketCommandContext>
    {
        [Command("CrearinteracioneMensaje")]
        [Discord.Commands.Summary("un usuario crea el registro de interaccion con un cliente")]
        public async Task ExecuteAsync(bool respondido = false, string correoUsuario = null, string telefono = null,
            string remitente = null, string fecha = null,string nota= null, string tema = null)

    {
            int telefonoCliente;
            bool conversionexitosa = int.TryParse(telefono, out telefonoCliente);


            
            if (correoUsuario == null || conversionexitosa == false)
            {
                await ReplyAsync("Uso correcto: !CrearInteracion <respondio> <correoUsuario> <telefono> <remitente> <fecha> <nota> <tema>");
                return;
            }

            try
            {
                UsuarioFachada.Instance.CrearMensaje(correoUsuario,remitente,fecha,tema,telefonoCliente,nota,respondido);

            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"Error al registrar la interaccion Mensaje: {ex.Message}");
            }
        }
    }
}