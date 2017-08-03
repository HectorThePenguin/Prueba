using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using SIE.Services.Servicios.BL;
namespace SIE.Services.Servicios.PL
{
    public class TratamientoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Tratamiento
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public void Guardar(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                tratamientoBL.Guardar(info);
                //int result = tratamientoBL.Guardar(info);
                //return result;
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
        /// Metodo para Guardar/Modificar una entidad Tratamiento
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public void Centros_Guardar(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                tratamientoBL.Centros_Guardar(info);
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
        public ResultadoInfo<TratamientoInfo> ObtenerPorPagina(PaginacionInfo pagina, TratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                ResultadoInfo<TratamientoInfo> result = tratamientoBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TratamientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                IList<TratamientoInfo> result = tratamientoBL.ObtenerTodos();
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
        public IList<TratamientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                IList<TratamientoInfo> result = tratamientoBL.ObtenerTodos(estatus);
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
        /// <param name="tratamientoID"></param>
        /// <returns></returns>
        public TratamientoInfo ObtenerPorID(int tratamientoID)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                TratamientoInfo result = tratamientoBL.ObtenerPorID(tratamientoID);
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
        public TratamientoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                TratamientoInfo result = tratamientoBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene la lista de tratamientos por tipo
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <param name="bMetafilaxia"></param>
        /// <returns></returns>
        public IList<TratamientoInfo> ObtenerTipoTratamientosCorte(TratamientoInfo tratamientoInfo, Metafilaxia bMetafilaxia)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                IList<TratamientoInfo> result = tratamientoBL.ObtenerTipoTratamientosCorte(tratamientoInfo, bMetafilaxia);

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
        /// Obtener la lista de tratamientos por tipo de reimplante
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <returns></returns>
        public IList<TratamientoInfo> ObtenerTratamientosPorTipoReimplante(TratamientoInfo tratamientoInfo)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                IList<TratamientoInfo> result = tratamientoBL.ObtenerTratamientosPorTipoReimplante(tratamientoInfo);

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

        public IList<TratamientoInfo> ObtenerProductosPorTratamiento(IList<TratamientoInfo> tratamientos)
        {
            IList<TratamientoInfo> resultado;
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                resultado = tratamientoBL.ObtenerProductosPorTratamientoSeleccionado(tratamientos);

                return resultado;
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
        public ResultadoInfo<TratamientoInfo> ObtenerTratamientosPorFiltro(PaginacionInfo pagina, FiltroTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                ResultadoInfo<TratamientoInfo> result = tratamientoBL.ObtenerTratamientosPorFiltro(pagina, filtro);
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
        public ResultadoInfo<TratamientoInfo> Centros_ObtenerTratamientosPorFiltro(PaginacionInfo pagina, FiltroTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                ResultadoInfo<TratamientoInfo> result = tratamientoBL.Centros_ObtenerTratamientosPorFiltro(pagina, filtro);
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
        /// Valida si el código del tratamiento ya existe para esa organización
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public bool ValidarExisteTratamiento(TratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                bool result = tratamientoBL.ValidarExisteTratamiento(filtro);
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
        /// Obtiene los tratamientos por problema
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <param name="listaProblemas"></param>
        /// <returns></returns>
        public IList<TratamientoInfo> ObtenerTratamientosPorProblemas(TratamientoInfo tratamientoInfo, List<int> listaProblemas)
        {
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                var result = tratamientoBL.ObtenerTratamientosPorProblemas(tratamientoInfo, listaProblemas);
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
        /// Validar Existencias de productos en Inventario
        /// </summary>
        /// <param name="tratamientos"></param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        public ResultadoValidacion ComprobarExistenciaTratamientos(IList<TratamientoInfo> tratamientos, int almacenID)
        {
            ResultadoValidacion resultado;
            try
            {
                Logger.Info();
                var tratamientoBL = new TratamientoBL();
                resultado = tratamientoBL.ComprobarExistenciaTratamientos(tratamientos, almacenID);

                return resultado;
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
