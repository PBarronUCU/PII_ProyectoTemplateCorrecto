//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using Library;
using Ucu.Poo.DiscordDemo.DiscordBot.Services;

namespace ConsoleApplication
{
    /// <summary>
    /// Programa de consola de demostración.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Punto de entrada al programa principal.
        /// </summary>
        public static void Main()
        {
            AdminFachada fachaadmin = AdminFachada.Instance;
            fachaadmin.CrearAdmin("Admin2");
            AdminFachada fachadaadmin = AdminFachada.Instance;
            fachaadmin.CrearUsuario("Admin2","rodrigo","montiel","r@");
            UsuarioFachada fachausuario = UsuarioFachada.Instance;
            fachausuario.CrearCliente("r@","esteban","quito",099,"eq@");
            BotLoader.LoadAsync().GetAwaiter().GetResult();
          
            
            
        }
    }
}