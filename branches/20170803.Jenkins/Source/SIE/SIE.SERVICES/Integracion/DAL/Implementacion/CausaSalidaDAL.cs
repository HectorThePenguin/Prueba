using System;
using System.Collections.Generic;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using System.Data.SqlClient;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CausaSalidaDAL:DALBase
    {

        internal CausaSalidaInfo ObtenerPorTipoMovimiento(int tipoMovimientoEnum)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCausaSalidaDAL.ObtenerParametrosPorTipoMovimiento(tipoMovimientoEnum);
                DataSet ds = Retrieve("SalidaIndividualRecuperacion_ObtenerCausa", parameters);
                CausaSalidaInfo causaSalida = null;
                if (ValidateDataSet(ds))
                {
                    causaSalida = MapCausaSalidaDAL.ObtenerPorTipoMovimiento(ds);
                }
                return causaSalida;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal List<CausaSalidaInfo> ObtenerPorTipoMovimientoLista(int tipoMovimientoEnum)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCausaSalidaDAL.ObtenerParametrosPorTipoMovimiento(tipoMovimientoEnum);
                DataSet ds = Retrieve("SalidaIndividualRecuperacion_ObtenerCausa", parameters);
                List<CausaSalidaInfo> causaSalida = null;
                if (ValidateDataSet(ds))
                {
                    causaSalida = MapCausaSalidaDAL.ObtenerPorTipoMovimientoLista(ds);
                }
                return causaSalida;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Crear un registro de CausaSalida
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(CausaSalidaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaSalidaDAL.ObtenerParametrosCrear(info);
                int result = Create("CausaSalida_Crear", parameters);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para actualizar un registro de CausaSalida
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CausaSalidaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaSalidaDAL.ObtenerParametrosActualizar(info);
                Update("CausaSalida_Actualizar", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal ResultadoInfo<CausaSalidaInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaSalidaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCausaSalidaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CausaSalida_ObtenerPorPagina", parameters);
                ResultadoInfo<CausaSalidaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaSalidaDAL.ObtenerPorPagina(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de CausaSalida
        /// </summary>
        /// <returns></returns>
        internal IList<CausaSalidaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CausaSalida_ObtenerTodos");
                IList<CausaSalidaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaSalidaDAL.ObtenerTodos(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal IList<CausaSalidaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaSalidaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CausaSalida_ObtenerTodos", parameters);
                IList<CausaSalidaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaSalidaDAL.ObtenerTodos(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un registro de CausaSalida
        /// </summary>
        /// <param name="causaSalidaID">Identificador de la CausaSalida</param>
        /// <returns></returns>
        internal CausaSalidaInfo ObtenerPorID(int causaSalidaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaSalidaDAL.ObtenerParametrosPorID(causaSalidaID);
                DataSet ds = Retrieve("CausaSalida_ObtenerPorID", parameters);
                CausaSalidaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaSalidaDAL.ObtenerPorID(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un registro de CausaSalida
        /// </summary>
        /// <param name="descripcion">Descripción de la CausaSalida</param>
        /// <returns></returns>
        internal CausaSalidaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaSalidaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CausaSalida_ObtenerPorDescripcion", parameters);
                CausaSalidaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaSalidaDAL.ObtenerPorDescripcion(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una Causa Salida por su Descripción y Tipo de Movimiento
        /// </summary>
        /// <param name="filtro">Contenedor con los parámetros</param>
        /// <returns></returns>
        internal CausaSalidaInfo ObtenerPorTipoMovimientoDescripcion(CausaSalidaInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaSalidaDAL.ObtenerParametrosPorTipoMovimientoDescripcion(filtro);
                DataSet ds = Retrieve("CausaSalida_ObtenerPorTipoMovimientoDescripcion", parameters);
                CausaSalidaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaSalidaDAL.ObtenerPorTipoMovimientoDescripcion(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
