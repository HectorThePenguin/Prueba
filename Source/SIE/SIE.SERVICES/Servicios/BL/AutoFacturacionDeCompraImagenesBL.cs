using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    public class AutoFacturacionDeCompraImagenesBL
    {
        public AutoFacturacionInfo ObtenerImagenDocumento(AutoFacturacionInfo info)
        {
            try
            {
                Logger.Info();
                var autoFacturacionDeCompraImagenesDAL = new AutoFacturacionDeCompraImagenesDAL();
                var result = autoFacturacionDeCompraImagenesDAL.ObtenerImagenDocumento(info);
                return result;

            }
            catch(ExcepcionGenerica)
            {
                throw;            
            }
            catch (Exception ex)
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
                var autoFacturacionDeCompraImagenesDAL = new AutoFacturacionDeCompraImagenesDAL();
                var result = autoFacturacionDeCompraImagenesDAL.ObtenerImagenIneCurp(info);
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
