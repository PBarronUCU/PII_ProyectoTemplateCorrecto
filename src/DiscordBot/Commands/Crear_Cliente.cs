using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CrearClienteComand :ModuleBase<SocketCommandContext>
    {
        [Command("CrearCliente")]
        [Discord.Commands.Summary("Crea un cliente usando el usuario Indicado")]
        public async Task ExecuteAsync(
            string correoUsuario = null,
            string nombre = null,
            string apellido = null,
            string correo = null,
            int telefono = 0,
            Genero genero = false,
            DateTime fechanac= null ,)
        {
            if (correoUsuario == null || nombre == null || apellido == null || correo == null || telefono == 0|| genero == || fechanac==)
            {
                await ReplyAsync("Uso correcto: !CrearCliente <correousuario> <nombre> <apellido> <correo> <telefono> <genero> <fechanac>");
                return;
            }

            try
            {
                UsuarioFachada.Instance.CrearCliente(correoUsuario, nombre, apellido, telefono, correo);
                await ReplyAsync(
                    $"Cliente **{nombre}{apellido}{telefono}**Creado exitosamente por el Usuario**{correoUsuario}");
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠️ Error al crear el Cliente: {ex.Message}");
            }
        }
    }
}