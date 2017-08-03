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
    internal class TipoMovimientoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoMovimiento
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoMovimientoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoDAL = new TipoMovimientoDAL();
                int result = info.TipoMovimientoID;
                if (info.TipoMovimientoID == 0)
                {
                    result = tipoMovimientoDAL.Crear(info);
                }
                else
                {
                    tipoMovimientoDAL.Actualizar(info);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoMovimientoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoMovimientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoDAL = new TipoMovimientoDAL();
                ResultadoInfo<TipoMovimientoInfo> result = tipoMovimientoDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista de TipoMovimientos
        /// </summary>
        /// <returns></returns>
        internal IList<TipoMovimientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoMovimientoDAL = new TipoMovimientoDAL();
                IList<TipoMovimientoInfo> result = tipoMovimientoDAL.ObtenerTodos();
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
        ///    Obtiene una lista de TipoMovimiento filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<TipoMovimientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoDAL = new TipoMovimientoDAL();
                IList<TipoMovimientoInfo> result = tipoMovimientoDAL.ObtenerTodos(estatus);

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
        ///     Obtiene una entidad TipoMovimiento por su Id
        /// </summary>
        /// <param name="tipoMovimientoID">Obtiene uan entidad TipoMovimiento por su Id</param>
        /// <returns></returns>
        internal TipoMovimientoInfo ObtenerPorID(int tipoMovimientoID)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoDAL = new TipoMovimientoDAL();
                TipoMovimientoInfo result = tipoMovimientoDAL.ObtenerPorID(tipoMovimientoID);
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
        ///     Obtiene una entidad TipoMovimiento por su Id
        /// </summary>
        /// <param name="tipoMovimientoID">Obtiene uan entidad TipoMovimiento por su Id</param>
        /// <returns></returns>
        internal TipoMovimientoInfo ObtenerSoloTipoMovimiento(int tipoMovimientoID)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoDAL = new TipoMovimientoDAL();
                TipoMovimientoInfo result = tipoMovimientoDAL.ObtenerSoloTipoMovimiento(tipoMovimientoID);
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
        /// Obtiene una entidad TipoMovimiento por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoMovimientoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoDAL = new TipoMovimientoDAL();
                TipoMovimientoInfo result = tipoMovimientoDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene los elementos que se mostraran
        /// para la pantalla de calidad pase proceso
        /// </summary>
        /// <returns></returns>
        internal IList<TipoMovimientoInfo> ObtenerTipoMovimientoCalidadPasaeProceso()
        {
            try
            {
                Logger.Info();
                var tipoMovimientoDAL = new TipoMovimientoDAL();
                IList<TipoMovimientoInfo> result = tipoMovimientoDAL.ObtenerTipoMovimientoCalidadPasaeProceso();
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
        /// Obtiene los tipos de movimiento para productos
        /// </summary>
        /// <returns></returns>
        internal IList<TipoMovimientoInfo> ObtenerMovimientosProducto()
        {
            try
            {
                Logger.Info();
                var tipoMovimientoDAL = new TipoMovimientoDAL();
                IList<TipoMovimientoInfo> result = tipoMovimientoDAL.ObtenerMovimientosProducto();

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

