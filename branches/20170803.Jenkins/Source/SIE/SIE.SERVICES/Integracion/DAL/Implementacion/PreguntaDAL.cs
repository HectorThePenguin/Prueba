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
    internal class PreguntaDAL : DALBase
    {
        /// <summary>
        /// Obtiene preguntas por id formulario 
        /// </summary>
        /// <param name="tipoPregunta"></param>
        /// <returns></returns>

        internal ResultadoInfo<PreguntaInfo> ObtenerPreguntasPorFormularioID(int tipoPregunta)
        {
            ResultadoInfo<PreguntaInfo> preguntaInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPreguntaDAL.ObtenerParametrosPorFormularioID(tipoPregunta);
                DataSet ds = Retrieve("Pregunta_ObtenerIDFormulario", parameters);
                if (ValidateDataSet(ds))
                {
                    preguntaInfo = MapPreguntaDAL.ObtenerPreguntasPorFormularioID(ds);
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
            return preguntaInfo;
        }

        /// <summary>
        /// Almacena en la base de datos el encabezado de la supervision de tecnica de deteccion
        /// </summary>
        /// <param name="supervision"></param>
        /// <returns></returns>
        internal int GuardarSupervisionDeteccionTecnica(SupervisionDetectoresInfo supervision)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPreguntaDAL.ObtenerParametrosSupervisionDeteccionTecnica(supervision);
                DataSet ds = Retrieve("SupervisionDeteccionTecnica_GuardarSupervision", parameters);
                if (ValidateDataSet(ds))
                {
                    retValue = MapPreguntaDAL.ObtenerSupervisionId(ds);
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

            return retValue;
        }

        /// <summary>
        /// Almacena en la base de datos el detalle de la supervision de tecnica de deteccion
        /// </summary>
        /// <param name="respuesta"></param>
        /// <returns></returns>
        internal int GuardarRespuestasSupervisionDeteccionTecnica(SupervisionDetectoresRespuestaInfo respuesta)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPreguntaDAL.ObtenerParametrosRespuestaSupervisionDeteccionTecnica(respuesta);
                DataSet ds = Retrieve("SupervisionDeteccionTecnica_GuardarRespuestaSupervision", parameters);
                if (ValidateDataSet(ds))
                {
                    retValue = MapPreguntaDAL.ObtenerSupervisionId(ds);
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

            return retValue;
        }

        /// <summary>
        /// Obtiene las supervisiones de tecnica de deteccion registradas a un operador
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        internal ResultadoInfo<SupervisionDetectoresInfo> ObtenerSupervisionesAnteriores(int organizacionId, int operadorId)
        {
            ResultadoInfo<SupervisionDetectoresInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPreguntaDAL.ObtenerParametrosPorOperadorId(organizacionId, operadorId);
                DataSet ds = Retrieve("SupervisionDeteccionTecnica_ObtenerSupervisionesAnteriores", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPreguntaDAL.ObtenerSupervisionDeteccion(ds);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene las respuestas registradas para una supervision id
        /// </summary>
        /// <param name="supervisionDetectoresId"></param>
        /// <returns></returns>
        internal IList<SupervisionDetectoresRespuestaInfo> ObtenerRespuestasSupervisionDeteccion(int supervisionDetectoresId)
        {
            IList<SupervisionDetectoresRespuestaInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPreguntaDAL.ObtenerParametrosPorSupervisionId(supervisionDetectoresId);
                DataSet ds = Retrieve("SupervisionDeteccionTecnica_ObtenerRespuestasSupervision", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapPreguntaDAL.ObtenerRespuestasSupervisionDeteccion(ds).Lista;
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
            return resultado;
        }
    }
}
