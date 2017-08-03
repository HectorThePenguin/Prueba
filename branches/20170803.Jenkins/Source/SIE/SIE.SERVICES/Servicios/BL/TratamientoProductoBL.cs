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
    internal class TratamientoProductoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TratamientoProducto
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TratamientoProductoInfo info)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoDAL = new TratamientoProductoDAL();
                int result = info.TratamientoProductoID;
                if (info.TratamientoProductoID == 0)
                {
                    result = tratamientoProductoDAL.Crear(info);
                }
                else
                {
                    tratamientoProductoDAL.Actualizar(info);
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
        internal ResultadoInfo<TratamientoProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, TratamientoProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoDAL = new TratamientoProductoDAL();
                ResultadoInfo<TratamientoProductoInfo> result = tratamientoProductoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TratamientoProducto
        /// </summary>
        /// <returns></returns>
        internal IList<TratamientoProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tratamientoProductoDAL = new TratamientoProductoDAL();
                IList<TratamientoProductoInfo> result = tratamientoProductoDAL.ObtenerTodos();
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
        internal IList<TratamientoProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoDAL = new TratamientoProductoDAL();
                IList<TratamientoProductoInfo> result = tratamientoProductoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TratamientoProducto por su Id
        /// </summary>
        /// <param name="tratamientoProductoID">Obtiene una entidad TratamientoProducto por su Id</param>
        /// <returns></returns>
        internal TratamientoProductoInfo ObtenerPorID(int tratamientoProductoID)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoDAL = new TratamientoProductoDAL();
                TratamientoProductoInfo result = tratamientoProductoDAL.ObtenerPorID(tratamientoProductoID);
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
        /// Obtiene una entidad TratamientoProducto por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad TratamientoProducto por su Id</param>
        /// <returns></returns>
        internal TratamientoProductoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoDAL = new TratamientoProductoDAL();
                TratamientoProductoInfo result = tratamientoProductoDAL.ObtenerPorDescripcion(descripcion);
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
        internal ResultadoInfo<TratamientoProductoInfo> ObtenerPorPaginaTratamientoID(PaginacionInfo pagina, TratamientoProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoDAL = new TratamientoProductoDAL();
                ResultadoInfo<TratamientoProductoInfo> result = tratamientoProductoDAL.ObtenerPorPaginaTratamientoID(pagina, filtro);
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

        internal List<TratamientoProductoInfo> ObtenerPorTratamientoID(TratamientoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoDAL = new TratamientoProductoDAL();
                List<TratamientoProductoInfo> result = tratamientoProductoDAL.ObtenerPorTratamientoID(tratamiento);
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
        internal List<HistorialClinicoDetalleInfo> ObtenerTratamientoAplicadoPorMovimientoTratamientoID(AnimalMovimientoInfo animalMovimiento, TratamientoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                var tratamientoProductoDAL = new TratamientoProductoDAL();
                List<HistorialClinicoDetalleInfo> result = 
                    tratamientoProductoDAL.ObtenerTratamientoAplicadoPorMovimientoTratamientoID(animalMovimiento, tratamiento);
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

