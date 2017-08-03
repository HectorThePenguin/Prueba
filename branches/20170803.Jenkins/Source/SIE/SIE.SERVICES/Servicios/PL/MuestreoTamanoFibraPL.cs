using System;
using System.Reflection;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class MuestreoTamanoFibraPL
    {
        public bool Guardar(List<MuestreoFibraFormulaInfo> listaMuestreo)
        {
            try
            {
                Logger.Info();
                var muestreoTamanoFibraBl = new MuestreoTamanoFibraBL();
                muestreoTamanoFibraBl.Guardar(listaMuestreo);
                return true;
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

        public bool Guardar(List<MuestreoFibraProductoInfo> listaMuestreo)
        {
            try
            {
                Logger.Info();
                var muestreoTamanoFibraBl = new MuestreoTamanoFibraBL();
                muestreoTamanoFibraBl.Guardar(listaMuestreo);
                return true;
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
