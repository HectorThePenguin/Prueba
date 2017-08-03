using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Transactions;

namespace SIE.Services.Servicios.BL
{
    internal class RelacionClienteproveedorBL
    {
        internal IList<RelacionClienteProveedorInfo> ObtenerPorProveedorID(int proveedorID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var relacionClienteProveedorDAL = new RelacionClienteProveedorDAL();
                IList<RelacionClienteProveedorInfo> result = relacionClienteProveedorDAL.ObtenerPorProveedorID(proveedorID, organizacionID);

                return result;
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

        internal bool Guardar(List<RelacionClienteProveedorInfo> creditos)
        {
            try
            {
                Logger.Info();
                var relacionClienteProveedorDAL = new RelacionClienteProveedorDAL();
                var result = false;
                using (var scope = new TransactionScope())
                {
                    result = relacionClienteProveedorDAL.Guardar(creditos);
                    scope.Complete();
                }
                return result;
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
