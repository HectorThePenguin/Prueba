using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class RuteoBL
    {
        /// <summary>
        ///     Obtiene un objeto por id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal RuteoInfo ObtenerPorID(int filtro)
        {
            RuteoInfo result;
            try
            {
                Logger.Info();
                var ruteoDAL = new RuteoDAL();
                result = ruteoDAL.ObtenerPorID(filtro);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<RuteoInfo> ObtenerPorPagina(PaginacionInfo pagina, RuteoInfo filtro)
        {
            ResultadoInfo<RuteoInfo> result;
            try
            {
                Logger.Info();
                var ruteoDAL = new RuteoDAL();
                result = ruteoDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<RuteoDetalleInfo> ObtenerDetallePorPagina(PaginacionInfo pagina, RuteoInfo filtro)
        {
            ResultadoInfo<RuteoDetalleInfo> result;
            try
            {
                Logger.Info();
                var ruteoDAL = new RuteoDAL();
                result = ruteoDAL.ObtenerDetallePorPagina(pagina, filtro);
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
    }
}
