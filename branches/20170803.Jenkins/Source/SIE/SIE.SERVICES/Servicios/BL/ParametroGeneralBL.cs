using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ParametroGeneralBL
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
                var parametroGeneralDAL = new ParametroGeneralDAL();
                return parametroGeneralDAL.ObtenerPorClaveParametro(claveParametro);
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

        /// <summary>
        /// Obtiene una entidad de ParametroGeneral por Clave de Parametro
        /// </summary>
        /// <param name="claveParametro">Clave del Parametro</param>
        /// <returns></returns>
        public ParametroGeneralInfo ObtenerPorClaveParametroActivo(string claveParametro)
        {
            try
            {
                Logger.Info();
                var parametroGeneralDAL = new ParametroGeneralDAL();
                return parametroGeneralDAL.ObtenerPorClaveParametroActivo(claveParametro);
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
