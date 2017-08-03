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
    public class RegistroVigilanciaDAL : DALBase
    {
        /// <summary>
        /// Obtiene un registro de vigilancia por el id
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        internal RegistroVigilanciaInfo ObtenerPorId(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRegistroVigilanciaDAL.ObtenerParametroObtenerPorId(registroVigilanciaInfo);
                DataSet ds = Retrieve("RegistroVigilancia_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaDAL.ObtenerPorId(ds);
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
        /// Obtiene un registro de vigilancia por el folioturno
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        internal RegistroVigilanciaInfo ObtenerPorFolioTurno(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRegistroVigilanciaDAL.ObtenerParametroObtenerPorFolioTurno(registroVigilanciaInfo);
                DataSet ds = Retrieve("RegistroVigilancia_ObtenerPorFolioTurno", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaDAL.ObtenerPorId(ds);
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
        /// Obtiene un registro de vigilancia por el folioturno independientemente de si se encuentra activo o no.
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        internal RegistroVigilanciaInfo ObtenerPorFolioTurnoActivoInactivo(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            RegistroVigilanciaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRegistroVigilanciaDAL.ObtenerParametroObtenerPorFolioTurno(registroVigilanciaInfo);
                DataSet ds = Retrieve("RegistroVigilancia_ObtenerPorFolioTurnoActivoInactivo", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaDAL.ObtenerPorId(ds);
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
        /// Gurda registro en la tabla RegistroVigilancia
        /// </summary>
        /// <param name="registrovigilanciainfo"></param>
        /// <returns></returns>
        internal int GuardarDatos(RegistroVigilanciaInfo registrovigilanciainfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRegistroVigilanciaDAL.ObtenerParametrosGuardarDatos(registrovigilanciainfo);
                int result = Create("RegistroVigilancia_Crear", parameters);
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
        /// Modifica los campos de fecha salida y activo = 0 en la tabla "RegistroVigilancia". de esta forma se registra a que hora salio el camion
        /// </summary>
        /// <param name="registrovigilanciainfo"></param>
        /// <returns></returns>
        internal void RegistroSalida(RegistroVigilanciaInfo registrovigilanciainfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRegistroVigilanciaDAL.RegistroSalida(registrovigilanciainfo);
                Update("RegistroVigilancia_RegistroSalida", parameters);
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
        internal ResultadoInfo<RegistroVigilanciaInfo> ObtenerPorPagina(PaginacionInfo pagina, RegistroVigilanciaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxRegistroVigilanciaDAL.ObtenerParametrosObtenerPorPagina(pagina, filtro);
                DataSet ds = Retrieve("RegistroVigilancia_ObtenerPorPagina", parameters);
                ResultadoInfo<RegistroVigilanciaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaDAL.ObtenerPorPagina(ds);
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
        /// Devuelve true si el camion no tiene FechaSalida y no esta Activo (0) 
        /// </summary>
        /// <param name="camion"></param>
        /// <returns></returns>
        internal bool ObtenerDisponibilidadCamion(CamionInfo camion)
        {
            var resultado = false;
            try
            {
                Dictionary<string, object> parameters = AuxRegistroVigilanciaDAL.ObtenerDisponibilidadCamion(camion);
                DataSet ds = Retrieve("RegistroVigilancia_ObtenerDisponibilidadCamion", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRegistroVigilanciaDAL.ObtenerDisponibilidadCamion(ds);
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
            return resultado;
        }
    }
}
