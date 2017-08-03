using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ParametroSemanaPL
    {
        /// <summary>
        /// Metodo para obtener el ParametroSemana
        /// </summary>
        /// <param name="parametroSemanaInfo"></param>
        /// <returns></returns>
        public ParametroSemanaInfo ObtenerParametroSemanaPorDescripcion(ParametroSemanaInfo parametroSemanaInfo)
        {
            ParametroSemanaInfo result;
            try
            {
                Logger.Info();
                var parametroSemanaBL = new ParametroSemanaBL();
                result = parametroSemanaBL.ObtenerParametroSemanaPorDescripcion(parametroSemanaInfo);
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
            return result;

        }

        /// <summary>
        /// Metodo para obtener el numero de semana.
        /// </summary>
        /// <param name="fechaCalcular"></param>
        /// <returns></returns>
        public int ObtenerNumeroSemana(DateTime fechaCalcular)
        {
            int result;
            try
            {
                Logger.Info();
                var parametroSemanaBL = new ParametroSemanaBL();
                result = parametroSemanaBL.ObtenerNumeroSemana(fechaCalcular);
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
            return result;

        }
    }
}
