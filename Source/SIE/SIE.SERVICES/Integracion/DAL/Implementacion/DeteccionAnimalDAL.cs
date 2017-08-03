using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class DeteccionAnimalDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de DeteccionAnimal
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(DeteccionAnimalInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDeteccionAnimalDAL.ObtenerParametrosCrear(info);
                int result = Create("DeteccionAnimal_Crear", parameters);
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
        /// Metodo para actualizar un registro de DeteccionAnimal
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(DeteccionAnimalInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDeteccionAnimalDAL.ObtenerParametrosActualizar(info);
                Update("DeteccionAnimal_Actualizar", parameters);
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
        public ResultadoInfo<DeteccionAnimalInfo> ObtenerPorPagina(PaginacionInfo pagina, DeteccionAnimalInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxDeteccionAnimalDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("DeteccionAnimal_ObtenerPorPagina", parameters);
                ResultadoInfo<DeteccionAnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDeteccionAnimalDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de DeteccionAnimal
        /// </summary>
        /// <returns></returns>
        public IList<DeteccionAnimalInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("DeteccionAnimal_ObtenerTodos");
                IList<DeteccionAnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDeteccionAnimalDAL.ObtenerTodos(ds);
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
        public IList<DeteccionAnimalInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDeteccionAnimalDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("DeteccionAnimal_ObtenerTodos", parameters);
                IList<DeteccionAnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDeteccionAnimalDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de DeteccionAnimal
        /// </summary>
        /// <param name="deteccionAnimalID">Identificador de la DeteccionAnimal</param>
        /// <returns></returns>
        public DeteccionAnimalInfo ObtenerPorID(int deteccionAnimalID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDeteccionAnimalDAL.ObtenerParametrosPorID(deteccionAnimalID);
                DataSet ds = Retrieve("DeteccionAnimal_ObtenerPorID", parameters);
                DeteccionAnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDeteccionAnimalDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de DeteccionAnimal
        /// </summary>
        /// <param name="descripcion">Descripción de la DeteccionAnimal</param>
        /// <returns></returns>
        public DeteccionAnimalInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDeteccionAnimalDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("DeteccionAnimal_ObtenerPorDescripcion", parameters);
                DeteccionAnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDeteccionAnimalDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene por Animal Movimiento ID
        /// </summary>
        /// <param name="animalMovimientoID">id del animal movimiento</param>
        /// <returns></returns>
        public DeteccionAnimalInfo ObtenerPorAnimalMovimientoID(long animalMovimientoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDeteccionAnimalDAL.ObtenerParametrosPorAnimalMovimientoID(animalMovimientoID);
                DataSet ds = Retrieve("DeteccionAnimal_ObtenerPorAnimalMovimientoID", parameters);
                DeteccionAnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDeteccionAnimalDAL.ObtenerPorAnimalMovimientoID(ds);
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

