using System;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class CalidadMateriaPrimaBL
    {
        /// <summary>
        /// Metodo para Guardar Calidad pase proceso
        /// </summary>
        /// <param name="indicadoresPaseProceso"></param>
        /// <param name="observaciones"> </param>
        /// <param name="movimiento"> </param>
        /// <param name="usuarioID"> </param>
        public void GuardarCalidadPaseProceso(string indicadoresPaseProceso, string observaciones
                                            , int movimiento, int usuarioID)
        {
            try
            {
                Logger.Info();
                var calidadMateriaPrimaDAL = new CalidadMateriaPrimaDAL();
                calidadMateriaPrimaDAL.GuardarCalidadPaseProceso(indicadoresPaseProceso, observaciones, movimiento,
                                                                 usuarioID);
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
