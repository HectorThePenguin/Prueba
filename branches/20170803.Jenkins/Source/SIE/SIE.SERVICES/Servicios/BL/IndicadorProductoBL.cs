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
    internal class IndicadorProductoBL 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productoInfo"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal List<IndicadorProductoInfo> ObtenerPorProductoId(ProductoInfo productoInfo, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var indicadorProductoDAL = new IndicadorProductoDAL();
                List<IndicadorProductoInfo> result = indicadorProductoDAL.ObtenerPorProductoId(productoInfo, estatus);
                if (result != null)
                {
                    foreach (var indicadorProductoInfo in result)
                    {
                        if (indicadorProductoInfo.IndicadorInfo.IndicadorId > 0)
                        {
                            var indicadorBl = new IndicadorBL();
                            indicadorProductoInfo.IndicadorInfo = indicadorBl.ObtenerPorId(indicadorProductoInfo.IndicadorInfo);
                        }

                        indicadorProductoInfo.IsEditable = true;
                    }
                }
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
        /// Metodo para Guardar/Modificar una entidad IndicadorProducto
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(IndicadorProductoInfo info)
        {
            try
            {
                Logger.Info();
                var indicadorProductoDAL = new IndicadorProductoDAL();
                int result = info.IndicadorProductoId;
                if (info.IndicadorProductoId == 0)
                {
                    result = indicadorProductoDAL.Crear(info);
                }
                else
                {
                    indicadorProductoDAL.Actualizar(info);
                }
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
                var indicadorProductoDAL = new IndicadorProductoDAL();
                ResultadoInfo<IndicadorProductoInfo> result = indicadorProductoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de IndicadorProducto
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var indicadorProductoDAL = new IndicadorProductoDAL();
                IList<IndicadorProductoInfo> result = indicadorProductoDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var indicadorProductoDAL = new IndicadorProductoDAL();
                IList<IndicadorProductoInfo> result = indicadorProductoDAL.ObtenerTodos(estatus);
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
        internal IndicadorProductoInfo ObtenerPorIndicadorProducto(IndicadorProductoInfo indicadorProductoInfo)
        {
            try
            {
                Logger.Info();
                var indicadorProductoDAL = new IndicadorProductoDAL();
                IndicadorProductoInfo result = indicadorProductoDAL.ObtenerPorIndicadorProducto(indicadorProductoInfo);
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
