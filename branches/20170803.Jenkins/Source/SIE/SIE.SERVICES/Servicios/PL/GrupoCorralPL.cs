using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class GrupoCorralPL
    {
        /// <summary>
        /// Obtiene la lista de Grupo Corral
        /// </summary>
        /// <returns></returns>
        public IList<GrupoCorralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                using(var grupoCorralBl = new GrupoCorralBL())
                {
                    IList<GrupoCorralInfo> result = grupoCorralBl.ObtenerTodos();
                    return result;
                }
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
        }

        /// <summary>
        /// Obtiene un lista de grupos de corral que dependiendo del estatus.
        /// </summary>
        /// <returns></returns>
        public IList<GrupoCorralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                using (var grupoCorralBl = new GrupoCorralBL())
                {
                    IList<GrupoCorralInfo> result = grupoCorralBl.ObtenerTodos(estatus);
                    return result;
                }
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
        }
    }
}
