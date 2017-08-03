using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ConfiguracionPremezclasPL
    {
        public void Guardar(PremezclaInfo premezclaInfo, List<PremezclaDetalleInfo> listaPremezclaDetalle, List<PremezclaDetalleInfo> listaPremezclaEliminados, int usuario)
        {
            try
            {
                Logger.Info();
                var configuracionPremezclasBl = new ConfiguracionPremezclasBL();
                configuracionPremezclasBl.Guardar(premezclaInfo, listaPremezclaDetalle, listaPremezclaEliminados, usuario);
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
