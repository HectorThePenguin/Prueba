using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ParametroGeneralPL
    {
        /// <summary>
        /// Obtiene una entidad de ParametroGeneral por Clave de Parametro
        /// </summary>
        /// <param name="claveParametro">Clave del Parametro</param>
        /// <returns></returns>
        public ParametroGeneralInfo ObtenerPorClaveParametro(string claveParametro)
        {
            try
            {
                Logger.Info();
                var parametroGeneralBL = new ParametroGeneralBL();
                return parametroGeneralBL.ObtenerPorClaveParametro(claveParametro);
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
