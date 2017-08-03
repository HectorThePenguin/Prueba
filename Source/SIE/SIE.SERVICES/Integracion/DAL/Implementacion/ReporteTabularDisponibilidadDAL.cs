using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Base.Integracion.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteTabularDisponibilidadDAL: DALBase
    {
        /// <summary>
        ///     Obtiene un lista de información tabular disponibilidad semanal
        /// </summary>
        /// <returns> </returns>
        internal List<ReporteTabularDisponibilidadInfo> ObtenerReporteTabularDisponibilidad(int organizacionId, TipoCorral produccion,DateTime fecha)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteTabularDisponibilidadDAL.ObtenerParametrosReporteTabularDisponibilidad(organizacionId,produccion,fecha);
                DataSet ds = Retrieve("ReporteTabularDisponibilidadSemanal", parameters);
                List<ReporteTabularDisponibilidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteTabularDisponibilidadDAL.ObtenerReporteTabularDisponibilidad(ds);
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
