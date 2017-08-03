using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class MenuPL
    {
        /// <summary>
        ///     Obtiene un lista de las opciones que el usuario puede ver
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="aplicacionWeb">Indica si se esta accediento desde la aplicacion Web</param>
        /// <returns></returns>
        public IList<MenuInfo> ObtenerPorUsuario(string usuario, bool aplicacionWeb)
        {
            IList<MenuInfo> menuLista;
            try
            {
                Logger.Info();
                var menuBL = new MenuBL();
                menuLista = menuBL.ObtenerPorUsuario(usuario, aplicacionWeb);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return menuLista;
        }
    }
}