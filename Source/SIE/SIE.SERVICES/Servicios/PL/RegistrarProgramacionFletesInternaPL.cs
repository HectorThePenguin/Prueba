using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class RegistrarProgramacionFletesInternaPL
    {
        /// <summary>
        /// Guarda un flete y su detalle
        /// </summary>
        public void Guardar(FleteInternoInfo fleteInternoInfo, int usuarioId)
        {
            try
            {
                Logger.Info();
                var registrarProgramacionFletesInternaBl = new RegistrarProgramacionFletesInternaBL();
                registrarProgramacionFletesInternaBl.Guardar(fleteInternoInfo, usuarioId);
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
        /// Actualiza un flete
        /// </summary>
        /// <param name="fleteInternoInfo"></param>
        /// <param name="usuarioId"></param>
        public void CancelarFlete(FleteInternoInfo fleteInternoInfo, int usuarioId)
        {
            try
            {
                Logger.Info();
                var registrarProgramacionFletesInternaBl = new RegistrarProgramacionFletesInternaBL();
                registrarProgramacionFletesInternaBl.CancelarFlete(fleteInternoInfo, usuarioId);
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
