using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AdministracionRuteoPL
    {

        /// <summary>
        ///     Obtiene un lista de detalle paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<RuteoDetalleInfo> ObtenerDetallePorPagina(PaginacionInfo pagina, RuteoInfo filtro)
        {
            ResultadoInfo<RuteoDetalleInfo> result;
            try
            {
                Logger.Info();
                var ruteoBL = new RuteoBL();
                result = ruteoBL.ObtenerDetallePorPagina(pagina, filtro);
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
        public ResultadoInfo<RuteoInfo> ObtenerPorPagina(PaginacionInfo pagina, RuteoInfo filtro)
        {
            ResultadoInfo<RuteoInfo> result;
            try
            {
                Logger.Info();
                var ruteoBL = new RuteoBL();
                result = ruteoBL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un ruteo por id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public RuteoInfo ObtenerPorID(int filtro)
        {
           RuteoInfo result;
            try
            {
                Logger.Info();
                var ruteoBL = new RuteoBL();
                result = ruteoBL.ObtenerPorID(filtro);
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
        ///     Obtiene un ruteo por ID
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public RuteoInfo ObtenerPorID(RuteoInfo filtro)
        {
            RuteoInfo result;
            try
            {
                Logger.Info();
                var ruteoBL = new RuteoBL();
                result = ruteoBL.ObtenerPorID(filtro.RuteoID);
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
