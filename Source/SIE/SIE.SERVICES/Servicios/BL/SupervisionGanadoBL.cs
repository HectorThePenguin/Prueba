using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class SupervisionGanadoBL
    {
        /// <summary>
        /// Guarda los aretes detectados despues de la revision
        /// </summary>
        /// <param name="supervision"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal int GuardarEstatusDeteccion(List<SupervisionGanadoInfo> supervision, int organizacionId)
        {
            try
            {
                Logger.Info();
                var supervisionDal = new SupervisionGanadoDAL();
                return supervisionDal.GuardarEstatusDeteccion(supervision, organizacionId);
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
        internal int ValidarAreteDetectado(string arete, string areteTestigo)
        {
            try
            {
                Logger.Info();
                var supervisionDal = new SupervisionGanadoDAL();
                return supervisionDal.ValidarAreteDetectado(arete, areteTestigo);
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
