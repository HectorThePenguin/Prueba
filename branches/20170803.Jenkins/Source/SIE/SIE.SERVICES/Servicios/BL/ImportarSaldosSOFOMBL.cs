using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ImportarSaldosSOFOMBL
    {
        internal List<ImportarSaldosSOFOMInfo> Guardar(List<ImportarSaldosSOFOMInfo> info)
        {
            try
            {
                
                Logger.Info();
                var result = new List<ImportarSaldosSOFOMInfo>();
                var saldosDAL = new ImportarSaldosSOFOMDAL();
                using (var scope = new TransactionScope())
                {
                    result = saldosDAL.Guardar(info);
                    scope.Complete();
                }
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

        internal ImportarSaldosSOFOMInfo CreditoSOFOM_ObtenerPorID(int creditoID)
        {
            try
            {

                Logger.Info();
                var dal = new ImportarSaldosSOFOMDAL();
                return dal.CreditoSOFOM_ObtenerPorID(creditoID);               
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
