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
    class AnalisisGrasaBL
    {
        //Guarda los datos para una lista de Analisis Grasos
        internal bool Guardar(List<AnalisisGrasaInfo> listaAnalisisGrasaInfo)
        {
            try
            {
                Logger.Info();
                using (var transaction = new TransactionScope())
                {
                    var analisisGrasaDal = new AnalisisGrasaDAL();
                    foreach (var analisisGrasaInfo in listaAnalisisGrasaInfo)
                    {
                        analisisGrasaDal.Guardar(analisisGrasaInfo);
                    }
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
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
        }
    }
}
