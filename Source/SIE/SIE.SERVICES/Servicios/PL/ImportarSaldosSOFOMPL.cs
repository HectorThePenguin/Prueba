using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;


namespace SIE.Services.Servicios.PL
{
    public class ImportarSaldosSOFOMPL
    {
        public List<ImportarSaldosSOFOMInfo> Guardar(List<ImportarSaldosSOFOMInfo> info)
        {
            try
            {
                Logger.Info();
                var saldosBL = new ImportarSaldosSOFOMBL();
                return saldosBL.Guardar(info);
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

        public ImportarSaldosSOFOMInfo CreditoSOFOM_ObtenerPorID(int creditoID)
        {
            try
            {
                Logger.Info();
                var bl = new ImportarSaldosSOFOMBL();
                return bl.CreditoSOFOM_ObtenerPorID(creditoID);
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
