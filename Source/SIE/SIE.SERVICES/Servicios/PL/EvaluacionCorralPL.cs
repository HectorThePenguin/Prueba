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
    public class EvaluacionCorralPL
    {
        /// <summary>
        ///     Metodo que crea una evaluacion corral
        /// </summary>
        /// <param name="evaluacionCorral"></param>
        /// <param name="tipoFolio"></param>
        public int GuardarEvaluacionCorral(EvaluacionCorralInfo evaluacionCorral,int tipoFolio)
        {
            try
            {
                Logger.Info();
                var evaluacionCorralBL = new EvaluacionCorralBL();
                return evaluacionCorralBL.GuardarEvaluacionCorral(evaluacionCorral, tipoFolio);
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

        public ResultadoInfo<EvaluacionCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, EvaluacionCorralInfo filtros)
        {
            try
            {
                Logger.Info();
                var evaluacionBL = new EvaluacionCorralBL();
                ResultadoInfo<EvaluacionCorralInfo> result = evaluacionBL.ObtenerPorPagina(pagina, filtros);
                return result;
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

        public List<EvaluacionCorralInfo> ObtenerEvaluaciones(EvaluacionCorralInfo filtros)
        {
            try
            {
                Logger.Info();
                var evaluacionBL = new EvaluacionCorralBL();
                List<EvaluacionCorralInfo> result = evaluacionBL.ObtenerEvaluaciones(filtros);
                return result;
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

        public void ImprimirEvaluacionPartida(EvaluacionCorralInfo evaluacionCorralInfoSelecionado,bool webForm)
        {
            try
            {
                Logger.Info();
                var evaluacionBL = new EvaluacionCorralBL();
                evaluacionBL.ImprimirEvaluacionPartida(evaluacionCorralInfoSelecionado,webForm);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
