using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class TipoFleteBL
    {
        /// <summary>
        /// Metrodo para obtener tipos flete
        /// </summary>
        internal List<TipoFleteInfo> ObtenerTiposFleteTodos(EstatusEnum estatus)
        {
            List<TipoFleteInfo> result;
            try
            {
                Logger.Info();
                var fleteDAL = new TipoFleteDAL();
                result = fleteDAL.ObtenerTiposFleteTodos(estatus);
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
            return result;
        }

        /// <summary>
        /// Obtiene un objeto TipoFlete por id
        /// </summary>
        /// <param name="tipoFleteId"></param>
        /// <returns></returns>
        internal TipoFleteInfo ObtenerPorId(int tipoFleteId)
        {
            try
            {
                Logger.Info();
                var tipoFleteDAL = new TipoFleteDAL();
                TipoFleteInfo result = tipoFleteDAL.ObtenerPorId(tipoFleteId);
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
