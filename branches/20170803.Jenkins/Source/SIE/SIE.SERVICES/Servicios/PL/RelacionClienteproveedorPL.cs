using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class RelacionClienteproveedorPL
    {

        public IList<RelacionClienteProveedorInfo> ObtenerPorProveedorID(int proveedorID, int organizacionID)
        {
           IList<RelacionClienteProveedorInfo> result;
            try
            {
                Logger.Info();
                var relacionClienteProveedorBL = new RelacionClienteproveedorBL();
                result = relacionClienteProveedorBL.ObtenerPorProveedorID(proveedorID, organizacionID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        public bool Guardar(List<RelacionClienteProveedorInfo> creditos)
        {
            try
            {
                Logger.Info();
                var relacionClienteProveedorBL = new RelacionClienteproveedorBL();
                return relacionClienteProveedorBL.Guardar(creditos);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
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
