using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class UnidadMedicionPL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<UnidadMedicionInfo> ObtenerPorPagina(PaginacionInfo pagina, UnidadMedicionInfo filtro)
        {
            ResultadoInfo<UnidadMedicionInfo> resultado;
            try
            {
                Logger.Info();
                var unidadMedicionBL = new UnidadMedicionBL();
                resultado = unidadMedicionBL.ObtenerPorPagina(pagina, filtro);
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
            return resultado;
        }

        /// <summary>
        ///     Obtiene un Unidad Medicion por Id
        /// </summary>
        /// <param name="unidadMedicionId"></param>
        /// <returns></returns>
        public UnidadMedicionInfo ObtenerPorID(int unidadMedicionId)
        {
            UnidadMedicionInfo unidadMedicionInfo;
            try
            {
                Logger.Info();
                var unidadMedidcionBL = new UnidadMedicionBL();
                unidadMedicionInfo = unidadMedidcionBL.ObtenerPorID(unidadMedicionId);
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
            return unidadMedicionInfo;
        }

        /// <summary>
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<UnidadMedicionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var unidadMedicionBL = new UnidadMedicionBL();
                IList<UnidadMedicionInfo> result = unidadMedicionBL.ObtenerTodos(estatus);
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
