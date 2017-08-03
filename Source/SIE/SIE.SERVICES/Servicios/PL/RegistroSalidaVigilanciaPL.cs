using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class RegistroSalidaVigilanciaPL
    {
        /// <summary>
        ///  Modifica los campos de fecha salida y activo = 0 en la tabla "RegistroVigilancia". de esta forma se registra a que hora salio el camion
        /// </summary>
        /// <param name="registrovigilanciainfo"></param>
        /// <returns></returns>
        public void RegistroSalida(RegistroVigilanciaInfo registrovigilanciainfo)
        {
            try
            {
                Logger.Info();
                var registroSalidaVigilanciaBl = new RegistroSalidaVigilanciaBL();
                registroSalidaVigilanciaBl.RegistroSalida(registrovigilanciainfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}