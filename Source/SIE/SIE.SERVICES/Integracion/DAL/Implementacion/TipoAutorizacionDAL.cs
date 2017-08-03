using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class TipoAutorizacionDAL : DALBase
    {
        /// <summary>
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<MovimientosAutorizarModel> ObtenerMovimientosAutorizacion()
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoAutorizacionDAL.ObtenerTodos();
                DataSet ds = Retrieve("TipoAutorizacion_ObtenerMovimientos", parameters);
                List<MovimientosAutorizarModel> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoAutorizacionDAL.ObtenerMovimientosAutorizacion(ds);
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
        /// Valida que se cumplan las precondiciones
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal int ValidarPreCondiciones()
        {
            var result = 0;
            try
            {
                Logger.Info();
                var parameters = AuxTipoAutorizacionDAL.ValidarPreCondiciones();
                result = Create("AutorizacionMateriaPrima_ValidarPreCondiciones", parameters);
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
    }
}
