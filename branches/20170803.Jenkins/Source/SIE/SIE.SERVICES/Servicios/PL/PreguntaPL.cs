using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
   public class PreguntaPL
    {

        /// <summary>
        /// Obtiene preguntas por id formulario 
        /// </summary>
        /// <param name="tipoPregunta"></param>
        /// <returns></returns>
        public ResultadoInfo<PreguntaInfo> ObtenerPorFormularioID(int tipoPregunta)
        {
            ResultadoInfo<PreguntaInfo> preguntaInfo;
            try
            {
                Logger.Info();
                var preguntaBL = new PreguntaBL();
                preguntaInfo = preguntaBL.ObtenerPorFormularioID(tipoPregunta);
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
            return preguntaInfo;
        }

       /// <summary>
       /// Obtiene los criterios para la supervision de deteccion
       /// </summary>
       /// <returns></returns>
       public ResultadoInfo<CriterioSupervisionInfo> ObtenerCriteriosSupervision()
       {
           ResultadoInfo<CriterioSupervisionInfo> criterios;
           try
           {
               Logger.Info();
               var preguntaBL = new PreguntaBL();
               criterios = preguntaBL.ObtenerCriteriosSupervision();
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
           return criterios;
       }

       /// <summary>
       /// Registra en la base de datos la supervision de tecnica de deteccion
       /// </summary>
       /// <param name="supervision"></param>
       /// <returns></returns>
       public int GuardarSupervisionDeteccionTecnica(SupervisionDetectoresInfo supervision)
       {
           int retValue = -1;
           try
           {
               Logger.Info();
               var preguntaBL = new PreguntaBL();
               retValue = preguntaBL.GuardarSupervisionDeteccionTecnica(supervision);
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
           return retValue;
       }

       /// <summary>
       /// Obtiene las evaluaciones anteriores para un operador de deteccion de la organizacion especificada
       /// </summary>
       /// <param name="organizacionId"></param>
       /// <param name="operadorId"></param>
       /// <returns></returns>
       public ResultadoInfo<SupervisionDetectoresInfo> ObtenerSupervisionesAnteriores(int organizacionId, int operadorId)
       {
           ResultadoInfo<SupervisionDetectoresInfo> resultado;
           try
           {
               Logger.Info();
               var preguntaBL = new PreguntaBL();
               resultado = preguntaBL.ObtenerSupervisionesAnteriores(organizacionId, operadorId);
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
