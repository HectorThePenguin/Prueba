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
    public class IndicadorProductoPL 
    {
        public List<IndicadorProductoInfo> ObtenerPorProductoId(ProductoInfo productoInfo, EstatusEnum estatus)
        {
            List<IndicadorProductoInfo> result;
            try
            {
                Logger.Info();
                var indicadorProductoBL = new IndicadorProductoBL();
                result = indicadorProductoBL.ObtenerPorProductoId(productoInfo, estatus);
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
        /// Metodo para Guardar/Modificar una entidad IndicadorProducto
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(IndicadorProductoInfo info)
        {
            try
            {
                Logger.Info();
                var indicadorProductoBL = new IndicadorProductoBL();
                int result = indicadorProductoBL.Guardar(info);
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

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<IndicadorProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, IndicadorProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var indicadorProductoBL = new IndicadorProductoBL();
                ResultadoInfo<IndicadorProductoInfo> result = indicadorProductoBL.ObtenerPorPagina(pagina, filtro);
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

        /// <summary>
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var indicadorProductoBL = new IndicadorProductoBL();
                IList<IndicadorProductoInfo> result = indicadorProductoBL.ObtenerTodos();
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

        /// <summary>
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var indicadorProductoBL = new IndicadorProductoBL();
                IList<IndicadorProductoInfo> result = indicadorProductoBL.ObtenerTodos(estatus);
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

        /// <summary>
        /// Obtiene un indicador producto por clave
        /// de indicador y clave de producto
        /// </summary>
        /// <param name="indicadorProductoInfo"></param>
        /// <returns></returns>
        public IndicadorProductoInfo ObtenerPorIndicadorProducto(IndicadorProductoInfo indicadorProductoInfo)
        {
            IndicadorProductoInfo result;
            try
            {
                Logger.Info();
                var indicadorProductoBL = new IndicadorProductoBL();
                result = indicadorProductoBL.ObtenerPorIndicadorProducto(indicadorProductoInfo);
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
