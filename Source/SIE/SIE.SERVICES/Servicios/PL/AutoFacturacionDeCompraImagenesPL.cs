using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class AutoFacturacionDeCompraImagenesPL
    {
        public AutoFacturacionInfo ObtenerImagenDocumento(AutoFacturacionInfo info)
        { 
            try
            {
                Logger.Info();
                var autoFacturacionDeCompraImagenesBL = new AutoFacturacionDeCompraImagenesBL();
                var  result = autoFacturacionDeCompraImagenesBL.ObtenerImagenDocumento(info);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
            Logger.Error(ex);
            throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public AutoFacturacionInfo ObtenerImagenIneCurp(AutoFacturacionInfo info)
        {
            try
            {
                Logger.Info();
                var autoFacturacionDeCompraImagenesBL = new AutoFacturacionDeCompraImagenesBL();
                var result = autoFacturacionDeCompraImagenesBL.ObtenerImagenIneCurp(info);
                return result;
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
