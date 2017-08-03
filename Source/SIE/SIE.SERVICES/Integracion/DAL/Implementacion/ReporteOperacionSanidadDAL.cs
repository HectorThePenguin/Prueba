using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteOperacionSanidadDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista de los Conceptos y sus días de diferencia
        /// </summary>
        /// <returns> </returns>
        internal List<ContenedorReporteOperacionSanidadInfo> ObtenerConceptosOperacionSanidad(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteOperacionSanidadDAL.ObtenerParametrosConceptosOperacionSanidad(organizacionID, fechaInicial, fechaFinal);
                DataSet ds = Retrieve("ReporteOperacionSanidad_ObtenerPorFecha", parameters);
                List<ContenedorReporteOperacionSanidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteOperacionSanidadDAL.ObtenerConceptosOperacionSanidad(ds);
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
