using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxReporteDetalleCorteDAL
    {
        /// <summary>
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ReporteMedicamentosAplicados_Obtener
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosReporteDetalleCorte(int organizacionID, DateTime fechaInicial, DateTime fechaFinal, int idUsuario,int TipoMovimientoID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionSeleccionada", organizacionID},
                                     {"@RangoFechasInicialSeleccionado", Convert.ToDateTime(fechaInicial).ToString("yyyyMMdd")},
                                     {"@RangoFechasFinalSeleccionado", Convert.ToDateTime(fechaFinal).ToString("yyyyMMdd")},
                                     {"@IdUsuario", idUsuario},
                                     {"@TipoMovimientoID",TipoMovimientoID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
