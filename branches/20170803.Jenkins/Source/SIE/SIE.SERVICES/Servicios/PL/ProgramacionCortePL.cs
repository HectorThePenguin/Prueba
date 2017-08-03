using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class ProgramacionCortePL
    {
        /// <summary>
        ///     Metodo que crea las programaciones de Corte para las entradas de Ganado proporcionales
        /// </summary>
        /// <param name="programacionCorte"></param>
        public int GuardarProgramacionCorte(IList<ProgramacionCorteInfo> programacionCorte)
        {
            try
            {
                Logger.Info();
                var programacionCorteBL = new ProgramacionCorteBL();
                return programacionCorteBL.GuardarProgramacionCorte(programacionCorte);
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
        ///     Metodo que crea las programaciones de Corte para las entradas de Ganado proporcionales
        /// </summary>
        /// <param name="programacionCorte"></param>
        /// <param name="organizacionID"></param>
        public int EliminarProgramacionCorte(ProgramacionCorte programacionCorte, int organizacionID)
        {
            try
            {
                Logger.Info();
                var programacionCorteBL = new ProgramacionCorteBL();
                return programacionCorteBL.EliminarProgramacionCorte( programacionCorte, organizacionID);
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
        /// Actalizar la fecha inicio de ña programacion de corte de ganado
        /// </summary>
        /// <param name="programacionCorte"></param>
        public void ActualizarFechaInicioProgramacionCorte(ProgramacionCorte programacionCorte)
        {
            try
            {
                Logger.Info();
                var programacionCorteBL = new ProgramacionCorteBL();
                programacionCorteBL.ActualizarFechaInicioProgramacionCorte(programacionCorte);
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
        /// Genera e imprime el reporte de programacion de corte
        /// </summary>
        /// <param name="programacion"></param>
        /// <returns></returns>
        public bool ImprimirProgramacionCorte(ImpresionProgramacionCorteInfo programacion)
        {
            var retValue = false;
            try
            {
                var pBl = new ProgramacionCorteBL();
                retValue = pBl.ImprimirProgramacionCorte(programacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retValue;
        }
    }
}
