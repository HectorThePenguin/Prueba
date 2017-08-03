using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class PaisDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada de Pais por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<PaisInfo> ObtenerPorPagina(PaginacionInfo pagina, PaisInfo filtro)
        {
            ResultadoInfo<PaisInfo> paisLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxPaisDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Pais_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    paisLista = MapPaisDAL.ObtenerPorPagina(ds);
                }
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
            return paisLista;
        }

        /// <summary>
        ///     Obtiene una pais por Id
        /// </summary>
        /// <param name="paisID"></param>
        /// <returns></returns>
        internal PaisInfo ObtenerPorID(int paisID)
        {
            PaisInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPaisDAL.ObtenerParametroPorID(paisID);
                DataSet ds = Retrieve("Pais_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPaisDAL.ObtenerPorID(ds);
                }
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
            return result;
        }


        /// <summary>
        /// Obtiene el chofer por su identificador 
        /// </summary>
        /// <param name="paisInfo"></param>
        /// <returns></returns>
        internal PaisInfo ObtenerPorID(PaisInfo paisInfo)
        {
            PaisInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPaisDAL.ObtenerParametroPorID(paisInfo);
                DataSet ds = Retrieve("Pais_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPaisDAL.ObtenerPorID(ds);
                }
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
            return result;
        }


        /// <summary>
        ///     Obtiene un Pais por descripcion
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        internal PaisInfo ObtenerPorDescripcion(string descripcion)
        {
            PaisInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPaisDAL.ObtenerParametroPorDescripcion(descripcion);
                DataSet ds = Retrieve("Pais_ObtenerPorDescripcion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPaisDAL.ObtenerPorDescripcion(ds);
                }
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
            return result;
        }

        /// <summary>
        ///     Obtiene un Pais por descripcion corta
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        internal PaisInfo ObtenerPorDescripcionCorta(string descripcionCorta)
        {
            PaisInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPaisDAL.ObtenerParametroPorDescripcionCorta(descripcionCorta);
                DataSet ds = Retrieve("Pais_ObtenerPorDescripcionCorta", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPaisDAL.ObtenerPorDescripcion(ds);
                }
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
            return result;
        }

        /// <summary>
        ///     Metodo que crear una Pais
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(PaisInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPaisDAL.ObtenerParametrosCrear(info);
                Create("Pais_Crear", parameters);
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
        ///     Metodo que actualiza un Pais
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(PaisInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxPaisDAL.ObtenerParametrosActualizar(info);
                Update("Pais_Actualizar", parameters);
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
