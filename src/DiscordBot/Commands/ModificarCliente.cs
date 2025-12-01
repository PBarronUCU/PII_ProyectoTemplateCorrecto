
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class ModificarCliente : ModuleBase<SocketCommandContext>
    {
        [Command("ModificarCliente")]
        [Discord.Commands.Summary("Modifica los datos de un cliente existente")]
        public async Task ExecuteAsync(string correoUsuario = null,string nombre =null,string appellido = null, string telefono = null, string genero= null,string fechanac = null)
        {
            int telCliente;
            bool convercionexitosa = int.TryParse(telefono, out telCliente);
            if (correoUsuario == null || nombre ==null|| appellido == null|| convercionexitosa == false|| genero== null||fechanac == null)
            {
                await ReplyAsync("Uso correcto: !ModificarCliente <CorreoUsuario> <NuevoNombre> <NuevoApellido <Telefono> <Genero> <FechadeNac>");
                return;  
            }

            try
            {
                UsuarioFachada.Instance.ModificarCliente(correoUsuario,nombre,appellido,telCliente,genero,fechanac);
                await ReplyAsync($"El Usuario **{correoUsuario}** Modifico al cliente con numero **{telCliente}**");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al modificar los datos del cliente{ex.Message}");
            }
        }
    }
}