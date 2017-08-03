using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ServicioAlimentoDAL : DALBase
    {
        /// <summary>
        /// Obtiene el Servicio de Alimento por Organizacion y CorralID
        /// </summary>
        /// <param name="corralID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal ServicioAlimentoInfo ObtenerPorCorralID(int organizacionID, int corralID)
        {
            ServicioAlimentoInfo servicioAlimentoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxServicioAlimentoDAL.ObtenerPorCorralID(organizacionID, corralID);
                DataSet ds = Retrieve("ServicioAlimento_ObtenerPorCorralID", parameters);
                if (ValidateDataSet(ds))
                {
                    servicioAlimentoInfo = MapServicioAlimentoDAL.ObtenerPorCorralID(ds);
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
            return servicioAlimentoInfo;
        }

        /// <summary>
        ///     Metodo que guardar ServicioAlimento
        /// </summary>
        /// <param name="servicioAlimento"></param>
        internal void Guardar(IList<ServicioAlimentoInfo> servicioAlimento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxServicioAlimentoDAL.ObtenerParametrosGuardar(servicioAlimento);
                Create("ServicioAlimento_Guardar", parameters);
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
        ///     Metodo que actualiza ServicioAlimento
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(ServicioAlimentoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxServicioAlimentoDAL.ObtenerParametrosActualizar(info);
                Update("ServicioAlimento_Actualizar", parameters);
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
        /// Metodo para obtener informacion diaria de la tabla servicioalimentos
        /// </summary>
        /// <returns></returns>
        internal IList<ServicioAlimentoInfo> ObtenerInformacionDiariaAlimento(int organizacionId)
        {
            IList<ServicioAlimentoInfo> servicioAlimentoInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxServicioAlimentoDAL.ObtenerInformacionDiariaAlimento(organizacionId);
                DataSet ds = Retrieve("ServicioAlimento_InformacionDiaria", parameters);
                if (ValidateDataSet(ds))
                {
                    servicioAlimentoInfo = MapServicioAlimentoDAL.ObtenerPorInformacionDiariaAlimento(ds);
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
            return servicioAlimentoInfo;
        }

        /// <summary>
        /// Metodo para Eliminar un servicio de alimentos programado
        /// </summary>
        /// <returns></returns>
        internal void Eliminar(CorralRangoInfo corralGrid)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxServicioAlimentoDAL.ObtenerEliminar(corralGrid);
                Delete("ServicioAlimento_Eliminar", parameters);
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
        /// Metodo para Eliminar un servicio de alimentos programado
        /// </summary>
        /// <returns></returns>
        internal void EliminarXML(List<CorralInfo> corralesEliminar)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxServicioAlimentoDAL.ObtenerEliminarXML(corralesEliminar);
                Delete("ServicioAlimento_EliminarXml", parameters);
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
