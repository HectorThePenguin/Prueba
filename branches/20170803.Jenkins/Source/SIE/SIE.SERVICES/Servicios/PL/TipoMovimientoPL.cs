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
    public class TipoMovimientoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoMovimiento
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoMovimientoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoBL = new TipoMovimientoBL();
                int result = tipoMovimientoBL.Guardar(info);
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
        public ResultadoInfo<TipoMovimientoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoMovimientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoBL = new TipoMovimientoBL();
                ResultadoInfo<TipoMovimientoInfo> result = tipoMovimientoBL.ObtenerPorPagina(pagina, filtro);

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
        ///     Obtiene una lista de TipoMovimientos
        /// </summary>
        /// <returns></returns>
        public IList<TipoMovimientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoMovimientoBL = new TipoMovimientoBL();
                IList<TipoMovimientoInfo> result = tipoMovimientoBL.ObtenerTodos();

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
        ///     Obtiene una lista de TipoMovimiento filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<TipoMovimientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoBL = new TipoMovimientoBL();
                IList<TipoMovimientoInfo> result = tipoMovimientoBL.ObtenerTodos(estatus);

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
        /// <param name="tipoMovimientoID"></param>
        /// <returns></returns>
        public TipoMovimientoInfo ObtenerPorID(int tipoMovimientoID)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoBL = new TipoMovimientoBL();
                TipoMovimientoInfo result = tipoMovimientoBL.ObtenerPorID(tipoMovimientoID);

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
        /// <param name="tipoMovimientoID"></param>
        /// <returns></returns>
        public TipoMovimientoInfo ObtenerSoloTipoMovimiento(int tipoMovimientoID)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoBL = new TipoMovimientoBL();
                TipoMovimientoInfo result = tipoMovimientoBL.ObtenerSoloTipoMovimiento(tipoMovimientoID);

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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public TipoMovimientoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoMovimientoBL = new TipoMovimientoBL();
                TipoMovimientoInfo result = tipoMovimientoBL.ObtenerPorDescripcion(descripcion);
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
        public IList<TipoMovimientoInfo> ObtenerTipoMovimientoCalidadPasaeProceso()
        {
            try
            {
                Logger.Info();
                var tipoMovimientoBL = new TipoMovimientoBL();
                IList<TipoMovimientoInfo> result = tipoMovimientoBL.ObtenerTipoMovimientoCalidadPasaeProceso();
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
        /// Obtiene los tipos de movimientos de productos
        /// </summary>
        /// <returns></returns>
        public IList<TipoMovimientoInfo> ObtenerMovimientosProducto()
        {
            try
            {
                Logger.Info();
                var tipoMovimientoBL = new TipoMovimientoBL();
                IList<TipoMovimientoInfo> result = tipoMovimientoBL.ObtenerMovimientosProducto();

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

