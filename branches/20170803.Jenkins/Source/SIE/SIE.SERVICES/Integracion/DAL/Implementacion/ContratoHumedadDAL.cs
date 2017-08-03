using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ContratoHumedadDAL :  DALBase 
    {
        /// <summary>
        /// Crea registros de contrato humedad
        /// </summary>
        /// <returns></returns>
        internal int CrearContratoHumedad(List<ContratoHumedadInfo> listaContratoHumedadInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxContratoHumedadDAL.ObtenerParametrosCrearContratoHumedad(listaContratoHumedadInfo);
                int result = Create("ContratoHumedad_CrearContratoHumedad", parameters);
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
        /// Obtiene un listado de humedades por contratoid
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal List<ContratoHumedadInfo> ObtenerPorContratoId(ContratoInfo contrato)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxContratoHumedadDAL.ObtenerPorContratoId(contrato);
                DataSet ds = Retrieve("ContratoHumedad_ObtenerPorContratoID", parameters);
                List<ContratoHumedadInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapContratoHumedadDAL.ObtenerPorContratoId(ds);
                }
                return lista;
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
        /// Obtiene la humedad a la fecha
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal ContratoHumedadInfo ObtenerHumedadAlaFecha(ContratoInfo contrato)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxContratoHumedadDAL.ObtenerPorContratoId(contrato);
                DataSet ds = Retrieve("ContratoHumedad_ObtenerHumedadAlaFechaPorContratoID", parameters);
                ContratoHumedadInfo humedad = null;
                if (ValidateDataSet(ds))
                {
                    humedad = MapContratoHumedadDAL.ObtenerHumedadAlaFecha(ds);
                }
                return humedad;
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
