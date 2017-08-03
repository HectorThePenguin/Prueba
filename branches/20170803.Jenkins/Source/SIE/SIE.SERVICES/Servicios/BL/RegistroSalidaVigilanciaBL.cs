using System;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    class RegistroSalidaVigilanciaBL
    {
        /// <summary>
        ///  Modifica los campos de fecha salida y activo = 0 en la tabla "RegistroVigilancia". de esta forma se registra a que hora salio el camion
        /// </summary>
        /// <param name="RegistroVigilanciaInfo"></param>
        /// <returns></returns>
        public void RegistroSalida(RegistroVigilanciaInfo registrovigilanciainfo)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    var registrovigilanciaBl = new RegistroVigilanciaBL();
                    registrovigilanciaBl.RegistroSalida(registrovigilanciainfo);
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
