using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxAlertaDAL
    { 
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina">informacion de paginacion a usar para la consulta de las alertas</param>
        /// <param name="filtro">alerta de donde se obtienen los valores para los parametros</param>
        /// <returns>regresa una lista de parametros para consultar alertas usando paginacion</returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, AlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", string.IsNullOrWhiteSpace(filtro.Descripcion) ? string.Empty : filtro.Descripcion.Trim()},
                            {"@AlertaID", filtro.AlertaID},
                            {"@Activo", filtro.Activo},   
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene parametros para registrar una alerta
        /// </summary>
        /// <param name="filtro">alerta de donde se obtienen los valores para los parametros</param>
        /// <returns>regresa una lista de parametros para guardar una alerta</returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(AlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ModuloID", filtro.Modulo.ModuloID},
                            {"@Descripcion", string.IsNullOrWhiteSpace(filtro.Descripcion) ? string.Empty : filtro.Descripcion.Trim()},
                            {"@HorasRespuesta", filtro.HorasRespuesta},
                            {"@TerminadoAutomatico", filtro.TerminadoAutomatico},
                            {"@Activo", filtro.Activo},
                            {"@UsuarioCreacionID", filtro.UsuarioCreacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene parametros para editar una alerta
        /// </summary>
        /// <param name="filtro">alerta de donde se obtienen los valores para los parametros</param>
        /// <returns>Regresa una lista de parametros para actualizar la informacion de una alerta</returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(AlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlertaID", filtro.AlertaID},
                            {"@ModuloID", filtro.Modulo.ModuloID},
                            {"@Descripcion", string.IsNullOrWhiteSpace(filtro.Descripcion) ? string.Empty : filtro.Descripcion},
                            {"@HorasRespuesta", filtro.HorasRespuesta},
                            {"@TerminadoAutomatico", filtro.TerminadoAutomatico},
                            {"@Activo", filtro.Activo},
                            {"@UsuarioModificacionID ", filtro.UsuarioModificacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Obtiene parametros para validar si ya esta registrada una alerta a un modulo
        /// </summary>
        /// <param name="filtro">alerta de donde se obtienen los valores para los parametros</param>
        /// <returns>Regresa una lista de parametros para consultar la existencia de una alerta con una descripcion y modulo proporcionados</returns>
        internal static Dictionary<string, object> ObtenerParametrosAlertaValida(AlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", string.IsNullOrWhiteSpace(filtro.Descripcion) ? string.Empty : filtro.Descripcion.Trim()},
                            {"@ModuloID", filtro.Modulo.ModuloID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
