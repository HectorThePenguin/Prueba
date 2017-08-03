using System;
using System.Collections.Generic;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Auxiliar;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AccionDAL :DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de accion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(AdministrarAccionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAccionDAL.ObtenerParametrosCrear(info);
                int result = Create("Accion_Crear", parameters);
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
        /// Metodo para actualizar un registro de Cliente
        /// </summary>
        /// <param name="info">Accion que se registrara en la base de datos</param>
        internal void Actualizar(AdministrarAccionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAccionDAL.ObtenerParametrosActualizar(info);
                Update("Accion_Actualizar", parameters);
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
        /// Obtiene una lista paginada
        /// </summary>
        /// <param name="pagina">informacion de paginacion usada en la consulta</param>
        /// <param name="filtro">filtros o parametros de busqueda</param>
        /// <returns></returns>
        internal ResultadoInfo<AdministrarAccionInfo> ObtenerPorPagina(PaginacionInfo pagina, AdministrarAccionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAccionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Accion_ObtenerPorPagina", parameters);
                ResultadoInfo<AdministrarAccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAccionDAL.ObtenerPorPagina(ds);
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

        /// <summary>Busca una accion en la base de datos que coincida con la descripcion proporcionada</summary>
        /// <param name="Descripcion">Descripcion de la accion que se buscara en la base de datos</param>
        /// <returns>Regresa la accion encontrada</returns>
        internal AdministrarAccionInfo ObtenerPorDescripcion(String Descripcion)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAccionDAL.ObtenerPorDescripcion(Descripcion);
                DataSet ds = Retrieve("Accion_ObtenerPorDescripcion", parameters);
                AdministrarAccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAccionDAL.ObtenerPorDescripcion(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Valida si la accion proporcionada es la ultima accion no asignada en alguna configuracion
        /// </summary>
        /// <param name="accionId">Id de la accion que se buscara</param>
        /// <returns>Regresa True si es valida, false si no lo es</returns>
        internal bool ValidarAsignacionesAsignadasById(int accionId)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAccionDAL.ValidarAsignacionesAsignadasById(accionId);
                int ds = Create("Accion_ValidarAsignacionesAsignadasByID", parameters);
                bool result = Convert.ToBoolean(ds);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
