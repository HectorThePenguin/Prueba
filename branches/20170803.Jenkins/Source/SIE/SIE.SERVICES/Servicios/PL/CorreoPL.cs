using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CorreoPL
    {
        /// <summary>
        /// Envia el correo electronico de una incidencia con los correo configurados
        /// </summary>
        /// <param name="organizacion"></param>
        /// <param name="correoAenviar"></param>
        /// <returns></returns>
        public ResultadoOperacion EnviarCorreoElectronicoInsidencia(OrganizacionInfo organizacion, CorreoInfo correoAenviar)
        {
            ResultadoOperacion resultado;
            try
            {
                Logger.Info();
                var correoBl = new CorreoBL();
                resultado = correoBl.EnviarCorreoElectronicoInsidencia(organizacion, correoAenviar);

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
            return resultado;

        }

        /// <summary>
        /// Envia el correo electronico de una incidencia con los correo configurados
        /// </summary>
        /// <param name="organizacion"></param>
        /// <param name="correoAenviar"></param>
        /// <returns></returns>
        public ResultadoOperacion EnviarCorreo(OrganizacionInfo organizacion, CorreoInfo correoAenviar)
        {
            ResultadoOperacion resultado;
            try
            {
                Logger.Info();
                var correoBl = new CorreoBL();
                resultado = correoBl.EnviarCorreo(correoAenviar, organizacion);

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
            return resultado;

        }
    }
}
