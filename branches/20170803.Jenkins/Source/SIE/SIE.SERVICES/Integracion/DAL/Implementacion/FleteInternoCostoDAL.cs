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
    internal class FleteInternoCostoDAL : DALBase
    {
        /// <summary>
        /// Crea flete interno costo a partir de un listado
        /// </summary>
        internal int Crear(List<FleteInternoCostoInfo> listaFleteInternoCosto, FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteInternoCostoDAL.ObtenerParametrosCrearFleteInternoCosto(listaFleteInternoCosto, fleteInternoDetalleInfo);
                int result = Create("FleteInternoCosto_Crear", parameters);
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
        /// Actualizar flete interno costo
        /// </summary>
        /// <returns></returns>
        internal void Actualizar(List<FleteInternoCostoInfo> listaFleteInternoCosto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteInternoCostoDAL.ObtenerParametrosActualizarFleteInternoCosto(listaFleteInternoCosto);
                Update("FleteInternoCosto_Actualizar", parameters);
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
        /// Metodo para obtener flete interno detalle por flete interno id
        /// </summary>
        /// <returns></returns>
        internal List<FleteInternoCostoInfo> ObtenerPorFleteInternoDetalleId(FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxFleteInternoCostoDAL.ObtenerParametrosObtenerPorFleteInternoDetalleId(fleteInternoDetalleInfo);
                var ds = Retrieve("FleteInternoCosto_ObtenerPorFleteInternoDetalleID", parameters);
                List<FleteInternoCostoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteInternoCostoDAL.ObtenerPorFleteInternoDetalleId(ds);
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
