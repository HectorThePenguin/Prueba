using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EvaluacionCorralDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear una evaluacion de Riesgos de Partida
        /// </summary>
        /// <param name="evaluacionCorral"></param>
        /// <param name="tipoFolio"></param>
        
        internal int GuardarEvaluacionCorral(EvaluacionCorralInfo evaluacionCorral,int tipoFolio)
        {
            int evaluacionCorralID;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEvaluacionCorralDAL.ObtenerParametrosGuardado(evaluacionCorral, tipoFolio);
                evaluacionCorralID = Create("EvaluacionCorral_Crear", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return evaluacionCorralID;
        }

        /// <summary>
        ///     Metodo para guardar el detalle de la Evaluacion de Riesgos por Corral
        /// </summary>
        /// <param name="evaluacionCorralDetalle"></param>
        /// <param name="evaluacionCorralID"></param>
        internal void GuardarEvaluacionCorralDetalle(IEnumerable<EvaluacionCorralDetalleInfo> evaluacionCorralDetalle, int evaluacionCorralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEvaluacionCorralDAL.ObtenerParametrosGuardadoEvaluacionCorral(evaluacionCorralDetalle, evaluacionCorralID);
                Create("EvaluacionCorral_GuardarDetalle", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal ResultadoInfo<EvaluacionCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, EvaluacionCorralInfo filtro)
        {
            ResultadoInfo<EvaluacionCorralInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxEvaluacionCorralDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("EvaluacionPartida_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapEvaluacionCorralDAL.ObtenerPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        internal List<EvaluacionCorralInfo> ObtenerEvaluaciones(EvaluacionCorralInfo filtro)
        {
            List<EvaluacionCorralInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxEvaluacionCorralDAL.ObtenerParametrosObtenerEvaluaciones( filtro);
                DataSet ds = Retrieve("EvaluacionPartida_ObtenerEvaluaciones", parameters);
                if (ValidateDataSet(ds))
                {
                    ResultadoInfo<EvaluacionCorralInfo> resultado = MapEvaluacionCorralDAL.ObtenerPorPagina(ds);
                    if (resultado != null && resultado.Lista != null)
                    {
                        lista = (List<EvaluacionCorralInfo>) resultado.Lista;
                    }
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
        
        /// <summary>
        /// Metodo para obtener la respuesta a las preguntas de evaluacion de partida
        /// </summary>
        /// <param name="preguntaId"></param>
        /// <param name="evaluacionId"></param>
        /// <returns></returns>
        internal string ObtenerRespuestaAPreguntaEvaluacion(int preguntaId, int evaluacionId)
        {
            string respuesta;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = new Dictionary<string, object>
                    {                        
                        {"@EvaluacionID", evaluacionId},
                        {"@PreguntaID", preguntaId},
                    };

                respuesta = RetrieveValue<string>("EvaluacionPartida_ObtenerRespuestaAPregunta", parametros);

            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return respuesta;
        }
    }
}
