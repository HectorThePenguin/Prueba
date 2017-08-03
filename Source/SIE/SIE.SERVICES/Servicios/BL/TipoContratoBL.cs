using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class TipoContratoBL
    {
        /// <summary>
        /// Obtiene todos los tipos de contrato
        /// </summary>
        /// <returns>Lista de TipoContratoInfo</returns>
        internal List<TipoContratoInfo> ObtenerTodos()
        {
            List<TipoContratoInfo> result;
            try
            {
                Logger.Info();
                var tipoContratoDAL = new TipoContratoDAL();
                result = tipoContratoDAL.ObtenerTodos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un objeto TipoContrato por id
        /// </summary>
        /// <param name="tipoContratoId"></param>
        /// <returns></returns>
        internal TipoContratoInfo ObtenerPorId(int tipoContratoId)
        {
            try
            {
                Logger.Info();
                var tipoContratoDAL = new TipoContratoDAL();
                TipoContratoInfo result = tipoContratoDAL.ObtenerPorId(tipoContratoId);
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
