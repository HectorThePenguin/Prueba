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
    public class TratamientoProductoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TratamientoProducto
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TratamientoProductoInfo info)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoBL = new TratamientoProductoBL();
                int result = tratamientoProductoBL.Guardar(info);
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
        public ResultadoInfo<TratamientoProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, TratamientoProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoBL = new TratamientoProductoBL();
                ResultadoInfo<TratamientoProductoInfo> result = tratamientoProductoBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TratamientoProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tratamientoProductoBL = new TratamientoProductoBL();
                IList<TratamientoProductoInfo> result = tratamientoProductoBL.ObtenerTodos();
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
        public IList<TratamientoProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoBL = new TratamientoProductoBL();
                IList<TratamientoProductoInfo> result = tratamientoProductoBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="tratamientoProductoID"></param>
        /// <returns></returns>
        public TratamientoProductoInfo ObtenerPorID(int tratamientoProductoID)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoBL = new TratamientoProductoBL();
                TratamientoProductoInfo result = tratamientoProductoBL.ObtenerPorID(tratamientoProductoID);
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
        public TratamientoProductoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoBL = new TratamientoProductoBL();
                TratamientoProductoInfo result = tratamientoProductoBL.ObtenerPorDescripcion(descripcion);
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
        public ResultadoInfo<TratamientoProductoInfo> ObtenerPorPaginaTratamientoID(PaginacionInfo pagina, TratamientoProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoBL = new TratamientoProductoBL();
                ResultadoInfo<TratamientoProductoInfo> result = tratamientoProductoBL.ObtenerPorPaginaTratamientoID(pagina, filtro);
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
        /// 
        /// </summary>
        /// <param name="tratamiento"></param>
        /// <returns></returns>
        public List<TratamientoProductoInfo> ObtenerPorTratamientoID(TratamientoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoBL = new TratamientoProductoBL();
                List<TratamientoProductoInfo> result = tratamientoProductoBL.ObtenerPorTratamientoID(tratamiento);
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
        /// 
        /// </summary>
        /// <param name="animalMovimiento"></param>
        /// <param name="tratamiento"></param>
        /// <returns></returns>
        public List<HistorialClinicoDetalleInfo> ObtenerTratamientoAplicadoPorMovimientoTratamientoID(AnimalMovimientoInfo animalMovimiento, TratamientoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoBL = new TratamientoProductoBL();
                List<HistorialClinicoDetalleInfo> result = 
                    tratamientoProductoBL.ObtenerTratamientoAplicadoPorMovimientoTratamientoID(animalMovimiento, tratamiento);
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

