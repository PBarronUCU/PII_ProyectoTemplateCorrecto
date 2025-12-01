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
        [Command("CrearCOtizacion")]
        [Discord.Commands.Summary("Un usuario crea una cotizacion a un cliente")]
        public async Task ExecuteAsync(
        )
        {
            if (1==1)
            {
                await ReplyAsync();
                return;
            }

            try
            {
                await ReplyAsync();

            }
            catch (System.Exception ex)
            {
                await ReplyAsync();
            }
        }
        
    }
}