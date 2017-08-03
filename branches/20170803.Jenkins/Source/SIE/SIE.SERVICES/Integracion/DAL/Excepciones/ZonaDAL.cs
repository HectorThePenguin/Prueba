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

namespace SIE.Services.Integracion.DAL.Excepciones
{
    internal class ZonaDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada de Zona por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ZonaInfo> ObtenerPorPagina(PaginacionInfo pagina, ZonaInfo filtro)
        {
            ResultadoInfo<ZonaInfo> zonaLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxZonaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Zona_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    zonaLista = MapZonaDAL.ObtenerPorPagina(ds);
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
            return zonaLista;
        }

        /// <summary>
        ///     Obtiene una Zona por Id
        /// </summary>
        /// <param name="zonaID"></param>
        /// <returns></returns>
        internal ZonaInfo ObtenerPorID(int zonaID)
        {
            ZonaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxZonaDAL.ObtenerParametroPorID(zonaID);
                DataSet ds = Retrieve("Zona_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapZonaDAL.ObtenerPorID(ds);
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
        /// <param name="zonaInfo"></param>
        /// <returns></returns>
        internal ZonaInfo ObtenerPorID(ZonaInfo zonaInfo)
        {
            ZonaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxZonaDAL.ObtenerParametroPorID(zonaInfo);
                DataSet ds = Retrieve("Zona_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapZonaDAL.ObtenerPorID(ds);
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
        ///     Obtiene una Zona por descripcion
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        internal ZonaInfo ObtenerPorDescripcion(string descripcion)
        {
            ZonaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxZonaDAL.ObtenerParametroPorDescripcion(descripcion);
                DataSet ds = Retrieve("Zona_ObtenerPorDescripcion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapZonaDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una lista de usuarios paginada por 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        internal ResultadoInfo<ZonaInfo> ObtenerPorDescripcion(PaginacionInfo pagina, ZonaInfo filtro,
                                                                IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<ZonaInfo> zonaLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxZonaDAL.ObtenerParametrosPorPagina(pagina, filtro,
                                                                                                 dependencias);
                DataSet ds = Retrieve("Zona_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    zonaLista = MapZonaDAL.ObtenerPorPagina(ds);
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
            return zonaLista;
        }

        /// <summary>
        ///     Metodo que crear una Zona
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(ZonaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxZonaDAL.ObtenerParametrosCrear(info);
                Create("Zona_Crear", parameters);
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
        ///     Metodo que actualiza una Zona
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(ZonaInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxZonaDAL.ObtenerParametrosActualizar(info);
                Update("Zona_Actualizar", parameters);
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
