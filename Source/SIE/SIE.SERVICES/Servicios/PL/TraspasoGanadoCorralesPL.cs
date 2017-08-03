using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TraspasoGanadoCorralesPL
    {
        public bool GuardarTraspasoGanadoCorrales(List<AnimalInfo> aretesTotal, CorralInfo corralInfoDestino, UsuarioInfo usuarioInfo)
        {
            try
            {
                Logger.Info();
                var traspasoGanadoCorralesBL = new TraspasoGanadoCorralesBL();
                return traspasoGanadoCorralesBL.GuardarTraspasoGanadoCorrales(aretesTotal, corralInfoDestino, usuarioInfo);
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

        public bool GuardarTraspasoGanadoCorralesRecepcion(CorralInfo corralInfoOrigen, CorralInfo corralInfoDestino, UsuarioInfo usuarioInfo)
        {
            try
            {
                Logger.Info();
                var traspasoGanadoCorralesBL = new TraspasoGanadoCorralesBL();
                return traspasoGanadoCorralesBL.GuardarTraspasoGanadoCorralesRecepcion(corralInfoOrigen, corralInfoDestino, usuarioInfo);
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
