using System;
using System.Configuration;
using System.Windows;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Auxiliar
{
    public static class AuxConfiguracion
    {
        /// <summary>
        /// Obtiene la organización donde esta autenticado el usuario.
        /// </summary>
        /// <returns></returns>
        public static  int ObtenerOrganizacionUsuario()
        {
            object config =  Application.Current.Properties[ConstantesVista.OrganizacionUsuario] ?? "0";
            return int.Parse(config.ToString());
        }

        /// <summary>
        ///  Obtiene la versión del sistema
        /// </summary>
        /// <returns></returns>
        public static string ObtenerVersionAplicacion()
        {
            object config = ConfigurationManager.AppSettings[ConstantesVista.VersionSistema]  ?? string.Empty;
            return config.ToString();
        }

        /// <summary>
        /// Obtiene la organización donde esta autenticado el usuario.
        /// </summary>
        /// <returns></returns>
        public static int ObtenerUsuarioLogueado()
        {
            object config = Application.Current.Properties[ConstantesVista.Usuario] ?? "0";
            return int.Parse(config.ToString());
        }

        /// <summary>
        /// Obtiene la organización donde esta autenticado el usuario.
        /// </summary>
        /// <returns></returns>
        public static bool ObtenerUsuarioCorporativo()
        {
            object config = Application.Current.Properties[ConstantesVista.UsuarioCorporativo] ?? "0";
            return bool.Parse(config.ToString());
        }

        /// <summary>
        /// Obtiene la organización donde esta autenticado el usuario.
        /// </summary>
        /// <returns></returns>
        public static ConfiguracionInfo ObtenerConfiguracion()
        {
            var configuracion = new ConfiguracionInfo
                {
                    PuertoBascula = Application.Current.Properties[ConstantesVista.PuertoBascula].ToString(),
                    ImpresoraRecepcionGanado = Application.Current.Properties[ConstantesVista.ImpresoraRecepcionGanado].ToString(),
                    ImpresoraPoliza = Application.Current.Properties[ConstantesVista.ImpresoraPoliza].ToString(),
                    MaxCaracteresLinea = Application.Current.Properties[ConstantesVista.MaxCaracteresLinea] != null ? int.Parse(Application.Current.Properties[ConstantesVista.MaxCaracteresLinea].ToString()) : 0,
                    MaxCaracteresLineaPoliza = Application.Current.Properties[ConstantesVista.MaxCaracteresLineaPoliza] != null ? int.Parse(Application.Current.Properties[ConstantesVista.MaxCaracteresLineaPoliza].ToString()) : 0,
                    NombreFuentePoliza = Application.Current.Properties[ConstantesVista.NombreFuentePoliza].ToString(),
                    Dominio = Application.Current.Properties[ConstantesVista.Dominio].ToString(),
                    Contenedor = Application.Current.Properties[ConstantesVista.Contenedor].ToString(),
                    GrupoAD = Application.Current.Properties[ConstantesVista.GrupoAD].ToString(),
                    ServidorActiveDirectory = Application.Current.Properties[ConstantesVista.ServidorActiveDirectory].ToString(),
                    MaxLineasPorPaginaPoliza = Convert.ToInt32(Application.Current.Properties[ConstantesVista.MaxLineasPorPaginaPoliza].ToString()),
                    ProveedorPropio = Application.Current.Properties[ConstantesVista.ProveedorPropio].ToString(),
                    BasculaBaudrate = Application.Current.Properties[ConstantesVista.BasculaBaudrate].ToString(),
                    BasculaBitStop = Application.Current.Properties[ConstantesVista.BasculaBitStop].ToString(),
                    BasculaDataBits = Application.Current.Properties[ConstantesVista.BasculaDataBits].ToString(),
                    BasculaParidad = Application.Current.Properties[ConstantesVista.BasculaParidad].ToString(),

                    PuertoDickey = Application.Current.Properties[ConstantesVista.PuertoDickey].ToString(),
                    DickeyBaudrate = Application.Current.Properties[ConstantesVista.DickeyBaudrate].ToString(),
                    DickeyBitStop = Application.Current.Properties[ConstantesVista.DickeyBitStop].ToString(),
                    DickeyDataBits = Application.Current.Properties[ConstantesVista.DickeyDataBits].ToString(),
                    DickeyParidad = Application.Current.Properties[ConstantesVista.DickeyParidad].ToString(),
                };
            return configuracion;
        }
    }
}
