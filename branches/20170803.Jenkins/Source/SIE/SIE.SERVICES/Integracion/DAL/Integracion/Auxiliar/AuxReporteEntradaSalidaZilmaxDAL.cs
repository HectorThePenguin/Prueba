using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxReporteEntradaSalidaZilmaxDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorOrganizacion(int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID},
                                     {"@TipoFormulaID_Fin", (int) ReporteEntradaSalidaZilmaxEnum.TipoFormulaID_Fin},
                                     {"@GrupoCorralID ", (int) ReporteEntradaSalidaZilmaxEnum.GrupoCorralID},
                                     {"@CalculoID", (int) ReporteEntradaSalidaZilmaxEnum.CalculoID},
                                     {"@TipoFormulaID_Ret", (int) ReporteEntradaSalidaZilmaxEnum.TipoFormulaID_Ret},
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerPorOrganizacion(int organizacionID, DateTime fechaZilmax)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID},
                                     {"@TipoFormulaID_Fin", (int) ReporteEntradaSalidaZilmaxEnum.TipoFormulaID_Fin},
                                     {"@GrupoCorralID ", (int) ReporteEntradaSalidaZilmaxEnum.GrupoCorralID},
                                     {"@CalculoID", (int) ReporteEntradaSalidaZilmaxEnum.CalculoID},
                                     {"@TipoFormulaID_Ret", (int) ReporteEntradaSalidaZilmaxEnum.TipoFormulaID_Ret},
                                     {"@FechaZilmax", fechaZilmax},
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
