using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;


namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxPreguntaDAL
    {
        /// <summary>
        /// Obtiene preguntas por id formulario 
        /// </summary>
        /// <param name="tipoPregunta"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFormularioID(int tipoPregunta)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FormularioId", tipoPregunta}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Parametros del encabezado de supervision de tecnica de deteccion
        /// </summary>
        /// <param name="supervision"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosSupervisionDeteccionTecnica(SupervisionDetectoresInfo supervision)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionId", supervision.OrganizacionId},
                        {"@OperadorId", supervision.OperadorId},
                        {"@FechaSupervision", supervision.FechaSupervision},
                        {"@CriterioSupervisionId", supervision.CriterioSupervisionId},
                        {"@Observaciones", supervision.Observaciones},
                        {"@UsuarioCreacionId", supervision.UsuarioCreacionId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Parametros de guardado del detalle de supervision de tecnica de deteccion
        /// </summary>
        /// <param name="respuesta"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosRespuestaSupervisionDeteccionTecnica(SupervisionDetectoresRespuestaInfo respuesta)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        
                        {"@SupervisionDetectoresId", respuesta.SupervisionDetectoresDetalleId},
                        {"@PreguntaId", respuesta.PreguntaId},
                        {"@Respuesta", respuesta.Respuesta},
                        {"@UsuarioCreacionId", respuesta.UsuarioCreacionId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Parametros para obtener las supervisiones de un operador
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOperadorId(int organizacionId, int operadorId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionId", organizacionId},
                        {"@OperadorId", operadorId}
                       
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Parametros para obtener las respuestas de supervision de deteccion
        /// </summary>
        /// <param name="supervisionDetectoresId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorSupervisionId(int supervisionDetectoresId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@SupervisionDeteccionId", supervisionDetectoresId}                       
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
