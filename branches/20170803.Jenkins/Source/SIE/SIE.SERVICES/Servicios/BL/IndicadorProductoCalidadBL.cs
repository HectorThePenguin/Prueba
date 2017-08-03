using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class IndicadorProductoCalidadBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad IndicadorProductoCalidad
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(IndicadorProductoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                var indicadorProductoCalidadDAL = new IndicadorProductoCalidadDAL();
                int result = info.IndicadorProductoCalidadID;
                if (info.IndicadorProductoCalidadID == 0)
                {
                    result = indicadorProductoCalidadDAL.Crear(info);
                }
                else
                {
                    indicadorProductoCalidadDAL.Actualizar(info);
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
        public ResultadoInfo<IndicadorProductoCalidadInfo> ObtenerPorPagina(PaginacionInfo pagina, IndicadorProductoCalidadInfo filtro)
        {
            try
            {
                Logger.Info();
                var indicadorProductoCalidadDAL = new IndicadorProductoCalidadDAL();
                ResultadoInfo<IndicadorProductoCalidadInfo> result = indicadorProductoCalidadDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de IndicadorProductoCalidad
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoCalidadInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var indicadorProductoCalidadDAL = new IndicadorProductoCalidadDAL();
                IList<IndicadorProductoCalidadInfo> result = indicadorProductoCalidadDAL.ObtenerTodos();
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
        public IList<IndicadorProductoCalidadInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var indicadorProductoCalidadDAL = new IndicadorProductoCalidadDAL();
                IList<IndicadorProductoCalidadInfo> result = indicadorProductoCalidadDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad IndicadorProductoCalidad por su Id
        /// </summary>
        /// <param name="indicadorProductoCalidadID">Obtiene una entidad IndicadorProductoCalidad por su Id</param>
        /// <returns></returns>
        public IndicadorProductoCalidadInfo ObtenerPorID(int indicadorProductoCalidadID)
        {
            try
            {
                Logger.Info();
                var indicadorProductoCalidadDAL = new IndicadorProductoCalidadDAL();
                IndicadorProductoCalidadInfo result = indicadorProductoCalidadDAL.ObtenerPorID(indicadorProductoCalidadID);
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
        /// Obtiene una entidad IndicadorProductoCalidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public IndicadorProductoCalidadInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var indicadorProductoCalidadDAL = new IndicadorProductoCalidadDAL();
                IndicadorProductoCalidadInfo result = indicadorProductoCalidadDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un indicador producto calidad
        /// </summary>
        /// <param name="indicadorProductoCalidad"></param>
        /// <returns></returns>
        public IndicadorProductoCalidadInfo ObtenerPorIndicadorProducto(IndicadorProductoCalidadInfo indicadorProductoCalidad)
        {
            try
            {
                Logger.Info();
                var indicadorProductoCalidadDAL = new IndicadorProductoCalidadDAL();
                IndicadorProductoCalidadInfo result =
                    indicadorProductoCalidadDAL.ObtenerPorIndicadorProducto(indicadorProductoCalidad);
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
        /// Obtiene un indicador producto calidad
        /// </summary>
        /// <param name="indicadorID"></param>
        /// <returns></returns>
        public List<ProductoInfo> ObtenerProductosPorIndicador(int indicadorID)
        {
            try
            {
                Logger.Info();
                var indicadorProductoCalidadDAL = new IndicadorProductoCalidadDAL();
                List<ProductoInfo> result =
                    indicadorProductoCalidadDAL.ObtenerProductosPorIndicador(indicadorID);
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

