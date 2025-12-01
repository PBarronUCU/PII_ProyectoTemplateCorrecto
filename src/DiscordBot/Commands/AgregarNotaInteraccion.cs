using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Discord.Interactions;
namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class AgregarNotaInteraccionCommand : ModuleBase<SocketCommandContext>
    {
        [Command("AgregarNota")]
        [Discord.Commands.Summary("Agrega una nota a una interacción existente de un cliente.")]
        public async Task ExecuteAsync(string correoUsuario = null, string telefono = null, string fecha = null, string tipo = null, [Remainder] string nota = null)
        {
            int telefonoCliente;

            bool conversion = int.TryParse(telefono, out telefonoCliente);

            // Validación de parámetros
            if (correoUsuario == null || conversion == false || fecha == null || tipo == null || nota == null)
            {
                await ReplyAsync(
                    "Uso correcto:\n" +
                    "`!AgregarNota <correoUsuario> <telefonoCliente> <fecha> <tipoInteraccion> <nota>`"
                );
                return;
            }

            try
            {
                UsuarioFachada.Instance.AgregarNota(
                    correoUsuario,
                    telefonoCliente,
                    fecha,
                    tipo,
                    nota
                );

                await ReplyAsync(
                    $"✔ Nota agregada correctamente a la interacción **{tipo}** del cliente **{telefonoCliente}** en la fecha **{fecha}**."
                );
            }
            catch (System.Exception ex)
            {
                await ReplyAsync($"⚠ Error al agregar la nota: {ex.Message}");
            }
        }
    }
}