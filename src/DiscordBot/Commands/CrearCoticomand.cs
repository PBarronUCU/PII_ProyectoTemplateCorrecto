using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CrearCoticomand: ModuleBase<SocketCommandContext>
    {
        [Command("CrearCotizacion")]
        [Discord.Commands.Summary("Un usuario crea una cotizacion a un cliente")]
        public async Task ExecuteAsync(string correoUsuario=null, string tema = null, string nota=null, string fecha=null, string telefono = null,string valor=null,string producto=null)
        { 
            int telCliente;
            bool convercionexitosa = int.TryParse(telefono, out telCliente);
            double precioventa;
            bool convercionexitosas = double.TryParse(valor, out precioventa);
            if (correoUsuario== null|| tema ==null || nota ==null || fecha ==null ||convercionexitosa==false|| convercionexitosas==false || producto==null)
            {
                await ReplyAsync();
                return;
            }

            try
            {
                UsuarioFachada.Instance.CrearCoti(correoUsuario,tema,nota,fecha,telCliente,precioventa,producto);
                await ReplyAsync($"El Usuario **{correoUsuario}** realizo una cotisacion por el producto **{producto}** a un precio de **${precioventa}** al Cliente con numero **{telCliente}");

            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al realizar cotisacion: {ex.Message}");
            }
        }
        
    }
}