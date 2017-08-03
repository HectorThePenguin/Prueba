using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class SupervisionGanadoPL
    {
        /// <summary>
        /// Guarda los aretes detectados despues de la revision
        /// </summary>
        /// <param name="supervision"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public int GuardarEstatusDeteccion(List<SupervisionGanadoInfo> supervision, int organizacionId)
        {
            try
            {
                Logger.Info();
                var supervisionBL = new SupervisionGanadoBL();
                return supervisionBL.GuardarEstatusDeteccion(supervision, organizacionId);
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
        /// Valida el arete y arete testigo ingresado que no se haya detectado en el dia
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="areteTestigo"></param>
        /// <returns></returns>
        public int ValidarAreteDetectado(string arete, string areteTestigo)
        {
            try
            {
                Logger.Info();
                var supervisionBL = new SupervisionGanadoBL();
                return supervisionBL.ValidarAreteDetectado(arete, areteTestigo);
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
