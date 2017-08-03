using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ParametroSemanaBL
    {
        /// <summary>
        /// Metodo para obtener el Parametro de Semana
        /// </summary>
        /// <param name="parametroSemanaInfo"></param>
        /// <returns></returns>
        internal ParametroSemanaInfo ObtenerParametroSemanaPorDescripcion(ParametroSemanaInfo parametroSemanaInfo)
        {
            ParametroSemanaInfo result;
            try
            {
                Logger.Info();
                var parametroSemanaDAL = new ParametroSemanaDAL();
                result = parametroSemanaDAL.ObtenerParametroSemanaPorDescripcion(parametroSemanaInfo);
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
        /// Metodo para obtener el Parametro de Semana
        /// </summary>
        /// <param name="fechaCalcular"></param>
        /// <returns></returns>
        internal int ObtenerNumeroSemana(DateTime fechaCalcular)
        {
            int result;
            try
            {
                Logger.Info();
                var parametroSemanaDAL = new ParametroSemanaDAL();
                result = parametroSemanaDAL.ObtenerNumeroSemana(fechaCalcular);
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
