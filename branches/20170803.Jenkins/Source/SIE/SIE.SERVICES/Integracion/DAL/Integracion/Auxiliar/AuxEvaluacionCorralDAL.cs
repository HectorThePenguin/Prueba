using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxEvaluacionCorralDAL
    {
        /// <summary>
        /// Metodo que obtiene los parametros para guardar una entrada
        /// </summary>
        /// <param name="evaluacionCorral"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardado(EvaluacionCorralInfo evaluacionCorral, int tipoFolio)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {                        
                        {"@OrganizacionID", evaluacionCorral.Organizacion.OrganizacionID},
                        {"@CorralID", evaluacionCorral.Corral.CorralID},
                        {"@LoteID", evaluacionCorral.Lote.LoteID},
                        {"@Cabezas", evaluacionCorral.Cabezas},
                        {"@EsMetafilaxia", evaluacionCorral.EsMetafilaxia},
                        {"@OperadorID", evaluacionCorral.Operador.OperadorID},
                        {"@NivelGarrapata", evaluacionCorral.NivelGarrapata},
                        {"@Autorizado", evaluacionCorral.MetafilaxiaAutorizada},
                        {"@Justificacion", evaluacionCorral.Justificacion},
                        {"@UsuarioCreacion", evaluacionCorral.UsuarioCreacionID},
                        {"@TipoFolio", tipoFolio}
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
        ///     Obtiene parametros para crear el detalle de la evaluacion de corral
        /// </summary>
        /// <param name="evaluacionCorralDetalle"></param>
        /// <param name="EvaluacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoEvaluacionCorral(
            IEnumerable<EvaluacionCorralDetalleInfo> evaluacionCorralDetalle, int EvaluacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                                 from detalle in evaluacionCorralDetalle
                                 select new XElement("EvaluacionDetalle",
                                                     new XElement("OrganizacionID", detalle.OrganizacionID),
                                                     new XElement("EvaluacionID", EvaluacionID),
                                                     new XElement("PreguntaID", detalle.PreguntaID),
                                                     new XElement("Respuesta", detalle.Respuesta),
                                                     new XElement("UsuarioModificacion", detalle.UsuarioModificacion),
                                                         new XElement("UsuarioCreacion", detalle.UsuarioCreacion)
                                     ));

                parametros = new Dictionary<string, object>
                    {
                        {"@XmlEvaluacionCorralDetalle", xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, EvaluacionCorralInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                        {"@FechaEvaluacion", filtro.FechaEvaluacion},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        public static Dictionary<string, object> ObtenerParametrosObtenerEvaluaciones( EvaluacionCorralInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                        {"@FechaEvaluacion", filtro.FechaEvaluacion}
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
