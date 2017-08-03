using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class UnidadMedicionBL
    {
        /// <summary>
        ///     Obtiene un lista paginada de Unidad Medicion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<UnidadMedicionInfo> ObtenerPorPagina(PaginacionInfo pagina, UnidadMedicionInfo filtro)
        {
            ResultadoInfo<UnidadMedicionInfo> result;
            try
            {
                Logger.Info();
                var unidadMedicionDAL = new UnidadMedicionDAL();
                result = unidadMedicionDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene una Unidad Medicion por Id
        /// </summary>
        /// <param name="unidadMedicionId"></param>
        /// <returns></returns>
        internal UnidadMedicionInfo ObtenerPorID(int unidadMedicionId)
        {
            UnidadMedicionInfo unidadMedicionInfo;
            try
            {
                Logger.Info();
                var unidadMedicionDAL = new UnidadMedicionDAL();
                unidadMedicionInfo = unidadMedicionDAL.ObtenerPorID(unidadMedicionId);
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<UnidadMedicionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var unidadMedicionDAL = new UnidadMedicionDAL();
                IList<UnidadMedicionInfo> result = unidadMedicionDAL.ObtenerTodos(estatus);
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
