using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Discord.Commands;
using Discord.Interactions;
using Library;


namespace Ucu.Poo.DiscordDemo.DiscordBot.Commands
{
    public class CommandUsuarioMasVentas :ModuleBase<SocketCommandContext>
    {
        [Command("UsuarioConMasVentas")]
        [Discord.Commands.Summary
            ("Devuelte el usuario con mas ventas relizadas y un bono de $100 por venta")]
        public async Task ExecuteAsync()
        {
            try
            {
                CantidadDeVentasUsuario UserCantidad = AdminFachada.Instance.VendedorConMasVentas();
                int bono = UserCantidad.CantidadVentaBono;
                string nombre = UserCantidad.Usuario.Nombre;
                string apellido = UserCantidad.Usuario.Apellido;
                string correo = UserCantidad.Usuario.Correo;
                await ReplyAsync
                ($"El vendedor **{nombre} {apellido}**,CORREO:**{correo}** fue el mejor empleado!\n" +
                 $"Por eso recive un bonus de $**{bono}** ");
            }
            catch (Exception ex)
            {
                await ReplyAsync($"⚠️ Error al conseguir mejor Vendedor: {ex.Message}");

            }
           
        }
    }
}