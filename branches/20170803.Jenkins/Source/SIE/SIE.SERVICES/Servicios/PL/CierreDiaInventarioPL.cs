using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CierreDiaInventarioPL
    {
        /// <summary>
        /// Guardar el inventario de cierre
        /// </summary>
        /// <param name="datosGrid"></param>
        /// <param name="almacenCierreDiaInventarioInfo"></param>
        /// <returns></returns>
        public int GuardarCierreDiaInventario(IList<AlmacenCierreDiaInventarioInfo> datosGrid, AlmacenCierreDiaInventarioInfo almacenCierreDiaInventarioInfo)
        {
            try
            {
                Logger.Info();
                var cierreDiaInventarioBl = new CierreDiaInventarioBL();
                int result = cierreDiaInventarioBl.GuardarCierreDiaInventario(datosGrid, almacenCierreDiaInventarioInfo);
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
