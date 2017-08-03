using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Interfaces;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Servicios.PL
{
    public class TipoCostoCentroPL
    {
        /// <summary>
        /// Obtener todos los tipos de costos de centro
        /// </summary>
        /// <param name="estatusEnum"></param>
        /// <returns></returns>
        public List<TipoCostoCentroInfo> ObtenerTodos(EstatusEnum estatusEnum)
        {
            try
            {
                Logger.Info();
                var tipoCostoCentroPL = new TipoCostoCentroBL();
                List<TipoCostoCentroInfo> result = tipoCostoCentroPL.ObtenerTodos(estatusEnum);
                return result;
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
