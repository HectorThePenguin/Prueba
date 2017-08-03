using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CrearVigilanciaPL
    {
        /// <summary>
        /// Se utiliza para guardar datos en la tabla RegistroVigilancia
        /// </summary>
        /// <param name="registrovigilanciainfo"></param>
        /// <returns></returns>
        public int Guardar(RegistroVigilanciaInfo registrovigilanciainfo)
        {
            try
            {
                Logger.Info();
                var registrovigilanciabl = new CrearVigilanciaBL();
                int resultado = registrovigilanciabl.GuardarDatos(registrovigilanciainfo);
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
