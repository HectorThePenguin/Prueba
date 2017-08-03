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
    internal class CorralRangoDAL : DALBase
    {  
        /// <summary>
        /// Metodo que regresa una lista los Corrales Disponibles por OrganizacionID para configuracion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<CorralRangoInfo> ObtenerPorOrganizacionID(int organizacionID)
        {
            var lista = new List<CorralRangoInfo>();
            try
            {
                Dictionary<string, object> parameters = AuxCorralRangoDAL.ObtenerParametrosPorOrganizacionID(organizacionID);
                var ds = Retrieve("[dbo].[CorralRango_ObtenerPorOrganizacionID]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapCorralRangoDAL.ObtenerPorOrganizacionID(ds);
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
            return lista;
        }

        /// <summary>
        /// Funcion que permite obtener de la base de datos la lista de corrales configurados
        /// </summary>
        /// <param name="organizacionID">Id de Organizacion</param>
        /// <returns></returns>
        internal List<CorralRangoInfo> ObtenerCorralesConfiguradosPorOrganizacionID(int organizacionID)
        {
            List<CorralRangoInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCorralRangoDAL.ObtenerParametrosPorOrganizacionID(organizacionID);
                var ds = Retrieve("[dbo].[CorralRango_ObtenerCorralesConfigurados]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapCorralRangoDAL.ObtenerCorralesConfiguradosPorOrganizacionID(ds);
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
            return lista;
        }

        /// <summary>
        /// Crea un registro en la tabla de CorralRango
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(CorralRangoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralRangoDAL.ObtenerParametrosCrear(info);
                Create("[dbo].[CorralRango_Crear]", parameters);
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
        /// Actualiza un registro en la tabla de CorralRango
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(List<CorralRangoInfo> info) 
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralRangoDAL.ObtenerParametrosActualizar(info);
                Update("CorralRango_Actualizar", parameters);
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
        /// Metodo que regresa un boolean true si el corral tiene o false si no tiene lote asignado
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal Boolean ObtenerLoteAsignado(int organizacionID, int corralID)
        {
            var tieneLoteAsignado = false;
            try
            {
                Dictionary<string, object> parameters = AuxCorralRangoDAL.ObtenerParametrosLoteAsignado(organizacionID, corralID);
                var ds = Retrieve("[dbo].[CorralRango_ValidaLoteAsignado]", parameters);
                if (ValidateDataSet(ds))
                {
                    tieneLoteAsignado = MapCorralRangoDAL.ObtenerLoteAsignado(ds);
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
            return tieneLoteAsignado;
        }

        /// <summary>
        /// Funcion qque obtiene el corral destino
        /// </summary>
        /// <param name="corralRangoInfo"></param>
        /// <param name="dias"></param>
        internal IList<CorralRangoInfo> ObtenerCorralDestino(CorralRangoInfo corralRangoInfo, int dias)
        {
            IList<CorralRangoInfo> list = null;
            try
            {
                Dictionary<string, object> parameters = AuxCorralRangoDAL.ObtenerParametrosObtenerCorralDestino(corralRangoInfo, dias);
                var ds = Retrieve("[dbo].[CorteGanado_ObtenerCorralDestino]", parameters);
                if (ValidateDataSet(ds))
                {
                    list = MapCorralRangoDAL.ObtenerCorralDestino(ds);
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
            return list;
        }
        internal IList<CorralRangoInfo> ObtenerCorralDestinoSinTipoGanado(CorralRangoInfo corralRangoInfo, int diasBloqueo)
        {
            IList<CorralRangoInfo> list = null;
            try
            {
                Dictionary<string, object> parameters = AuxCorralRangoDAL.ObtenerParametrosObtenerCorralDestinoSinTipoGanado(corralRangoInfo, diasBloqueo);
                var ds = Retrieve("[dbo].[CorteGanado_ObtenerCorralDestinoSinTipoGanado]", parameters);
                if (ValidateDataSet(ds))
                {
                    list = MapCorralRangoDAL.ObtenerCorralDestino(ds);
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
            return list;
        }

        /// <summary>
        /// Metodo para eliminar la configuracion de corrales
        /// </summary>
        /// <param name="corralGrid"></param>
        public void Eliminar(CorralRangoInfo corralGrid)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralRangoDAL.ObtenerParametrosEliminar(corralGrid);
                Delete("CorralRango_Eliminar", parameters);
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
