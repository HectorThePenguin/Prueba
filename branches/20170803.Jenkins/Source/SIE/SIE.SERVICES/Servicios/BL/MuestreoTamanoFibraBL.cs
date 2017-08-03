using System;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    public class MuestreoTamanoFibraBL
    {
        internal bool Guardar(List<MuestreoFibraFormulaInfo> listaMuestreoInfo)
        {
            try
            {
                Logger.Info();
               
                using (var transaction = new TransactionScope())
                {
                    var muestreoTamanoFibraDal = new MuestreoTamanoFibraDAL();
                    muestreoTamanoFibraDal.Guardar(listaMuestreoInfo);
                    transaction.Complete();
                }
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

        internal bool Guardar(List<MuestreoFibraProductoInfo> listaMuestreoInfo)
        {
            try
            {
                Logger.Info();

                using (var transaction = new TransactionScope())
                {
                    var muestreoTamanoFibraDal = new MuestreoTamanoFibraDAL();
                    muestreoTamanoFibraDal.Guardar(listaMuestreoInfo);
                    transaction.Complete();
                }
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
