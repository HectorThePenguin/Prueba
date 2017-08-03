using System.Windows;
using SuKarne.Controls.Enum;

namespace SuKarne.Controls.MessageBox
{
    public static class SkMessageBox
    {
        /// <summary>
        /// Muestra un mensaje en un cuadro de texto 
        /// </summary>
        /// <param name="mensaje">Mensaje de texto a desplegar</param>
        /// <returns>MessageBoxResult</returns>
        public static MessageBoxResult Show(string mensaje)
        {
            return MuestraMensaje(null, mensaje);
        }

        /// <summary>
        /// Muestra un mensaje en un cuadro de texto con una ventana padre
        /// </summary>
        /// <param name="ventanaPadre"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(Window ventanaPadre, string mensaje)
        {
            return MuestraMensaje(ventanaPadre, mensaje);
        }

        /// <summary>
        /// Muestra un mensaje en un cuadro de texto con titulo y boton a desplegar
        /// </summary>
        /// <param name="mensaje"></param>        
        /// <param name="botones"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string mensaje, MessageBoxButton botones)
        {
            return MuestraMensaje(null, mensaje, botones);
        }

        /// <summary>
        /// Muestra un mensaje en un cuadro de texto con titulo e icono 
        /// </summary>
        /// <param name="mensaje"></param>        
        /// <param name="botones"></param>
        /// <param name="icono"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string mensaje, MessageBoxButton botones,
                                            MessageImage icono)
        {
            return MuestraMensaje(null, mensaje, botones, icono);
        }

        /// <summary>
        /// Muestra un mensaje en un cuadro de texto de una ventana padre, titulo y boton
        /// </summary>
        /// <param name="ventanaPadre"></param>
        /// <param name="mensaje"></param>        
        /// <param name="botones"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(Window ventanaPadre, string mensaje, MessageBoxButton botones)
        {
            return MuestraMensaje(ventanaPadre, mensaje, botones);
        }

        /// <summary>
        /// Muestra un mensaje en un cuadro de texto con titulo, boton, icon, y el resultado por defecto
        /// </summary>
        /// <param name="mensaje"></param>        
        /// <param name="botones"></param>
        /// <param name="icono"></param>
        /// <param name="resultadoDefecto"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string mensaje, MessageBoxButton botones,
                                            MessageImage icono, MessageBoxResult resultadoDefecto)
        {
            return MuestraMensaje(null, mensaje, botones, icono, resultadoDefecto);
        }

        /// <summary>
        /// Muestra un mensaje en un cuadro de texto para una ventana padre, con titulo, boton e icono
        /// </summary>
        /// <param name="ventanaPadre"></param>
        /// <param name="mensaje"></param>        
        /// <param name="botones"></param>
        /// <param name="icono"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(Window ventanaPadre, string mensaje, MessageBoxButton botones,
                                            MessageImage icono)
        {
            return MuestraMensaje(ventanaPadre, mensaje, botones, icono);
        }

        /// <summary>
        /// Muestra un mensaje en un cuadro de texto con titulo, boton, icono, resultado por defecto
        /// </summary>
        /// <param name="mensaje"></param>        
        /// <param name="botones"></param>
        /// <param name="icono"></param>
        /// <param name="resultadoDefecto"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string mensaje, MessageBoxButton botones,
                                            MessageImage icono, MessageBoxResult resultadoDefecto,
                                            MessageBoxOptions options)
        {
            return MuestraMensaje(null, mensaje, botones, icono, resultadoDefecto, options);
        }

        /// <summary>
        /// Muestra un mensaje en un cuadro de texto de una ventana padre, con titulo, boton, icono, y resultado por defecto
        /// </summary>
        /// <param name="ventanaPadre"></param>
        /// <param name="mensaje"></param>       
        /// <param name="botones"></param>
        /// <param name="icono"></param>
        /// <param name="resultadoDefecto"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(Window ventanaPadre, string mensaje, MessageBoxButton botones,
                                            MessageImage icono, MessageBoxResult resultadoDefecto)
        {
            return MuestraMensaje(ventanaPadre, mensaje, botones, icono, resultadoDefecto);
        }

        /// <summary>
        /// Muestra un mensaje en un cuadro de texto de una ventana padre, con titulo, boton, icono, resultado por defecto y opciones 
        /// </summary>
        /// <param name="ventanaPadre"></param>
        /// <param name="mensaje"></param>        
        /// <param name="botones"></param>
        /// <param name="icono"></param>
        /// <param name="resultadoDefecto"></param>
        /// <param name="opciones"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(Window ventanaPadre, string mensaje, MessageBoxButton botones,
                                            MessageImage icono, MessageBoxResult resultadoDefecto,
                                            MessageBoxOptions opciones)
        {
            return MuestraMensaje(ventanaPadre, mensaje, botones, icono, resultadoDefecto, opciones);
        }

        private static MessageBoxResult MuestraMensaje(
            Window ventanaPadre,
            string mensaje,
            MessageBoxButton boton = MessageBoxButton.OK,
            MessageImage icono = MessageImage.None,
            MessageBoxResult resultadoDefecto = MessageBoxResult.None,
            MessageBoxOptions opciones = MessageBoxOptions.None)
        {
            return SkMessageBoxWindow.Show(
                delegate(Window messageBoxWindow) { messageBoxWindow.Owner = ventanaPadre; },
                mensaje, boton, icono, resultadoDefecto, opciones);
        }
    }
}