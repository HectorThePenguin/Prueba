using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class EntradaGanadoSobrantePL
    {
        /// <summary>
        ///     Metodo que crear una entrada de ganado sobrante
        /// </summary>
        /// <param name="entradaGanadoSobranteInfo"></param>
        public int GuardarEntradaGanadoSobrante(EntradaGanadoSobranteInfo entradaGanadoSobranteInfo)
        {
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoSobranteBL();
                return entradaGanadoBL.GuardarEntradaGanadoSobrante(entradaGanadoSobranteInfo);
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
