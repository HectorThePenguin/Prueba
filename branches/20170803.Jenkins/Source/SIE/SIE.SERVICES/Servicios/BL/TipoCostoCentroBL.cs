using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class TipoCostoCentroBL
    {
        /// <summary>
        /// Obtiene todos los Tipos de costos de centro por estatus
        /// </summary>
        /// <param name="estatusEnum"></param>
        /// <returns></returns>
        internal List<TipoCostoCentroInfo> ObtenerTodos(EstatusEnum estatusEnum)
        {
            try
            {
                Logger.Info();
                var tipoCostoCentroDAL = new TipoCostoCentroDAL();
                List<TipoCostoCentroInfo> result = tipoCostoCentroDAL.ObtenerTodos(estatusEnum);
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
