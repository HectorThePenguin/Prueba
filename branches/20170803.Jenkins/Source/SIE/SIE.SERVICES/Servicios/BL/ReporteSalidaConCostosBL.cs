using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Services.Info.Filtros;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    public class ReporteSalidaConCostosBL
    {
        readonly ReporteSalidaConCostosDAL ReporteSalidaConCostosDL;

        public ReporteSalidaConCostosBL()
        {
            ReporteSalidaConCostosDL = new ReporteSalidaConCostosDAL();
        }

        public IList<SIE.Services.Info.Info.ReporteSalidaConCostosInfo> obtenerReporte(ReporteSalidasConCostoParametrosInfo DatosConsulta)
        {
            try
            {
                Logger.Info();

                return ReporteSalidaConCostosDL.obtenerReporte(DatosConsulta);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
