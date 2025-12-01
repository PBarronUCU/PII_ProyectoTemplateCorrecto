using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CrearInteraccionconClinete: ModuleBase<SocketCommandContext>
    {
        [Command("Crearinteraciones")]
        [Discord.Commands.Summary("un usuario crea el registro de interaccion con un cliente")]
        public async Task ExecuteAsync(string correoUsuario = null, int telefono = 0,string remitente=null ,string fecha = null)
        {
            if (correoUsuario == null || telefono == 0)
            {
                await ReplyAsync("Uso correcto: !CrearInteracion <correoUsuario> <telefono>");
                return;
            }

            try
            {
                //UsuarioFachada.Instance.CrearMensaje();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}