using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxCorralRangoDAL
    {   
        /// <summary>
        /// Metodo Agrega el OrganizacionID como parametro para regresar una lista los Corrales Disponibles
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionID(int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID}
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
        /// Metodo Agrega los campos como parametro para crear un registro en la tabla CorralRango
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>        
        internal static Dictionary<String, Object> ObtenerParametrosCrear(CorralRangoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@CorralID", filtro.CorralID},
                        {"@Sexo", filtro.Sexo},
                        {"@RangoInicial", filtro.RangoInicial},
                        {"@RangoFinal", filtro.RangoFinal},
                        {"@Activo", filtro.Activo},
                       /* {"@FechaCreacion", filtro.FechaCreacion},*/
                        {"@UsuarioCreacionID", filtro.UsuarioCreacionId}
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
        /// Metodo Agrega los campos como parametro para actualizar un registro en la tabla CorralRango
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>    
        internal static Dictionary<String, Object> ObtenerParametrosActualizar(List<CorralRangoInfo> filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                //var lista = cabezasCortadas.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from corralRango in filtro
                               select
                                   new XElement("CorralRango",
                                                new XElement("CorralAnteriorID", corralRango.CorralAnteriorID),
                                                new XElement("OrganizacionID", corralRango.OrganizacionID),
                                                new XElement("CorralID", corralRango.CorralID),          
                                                new XElement("Sexo", corralRango.Sexo),
                                                new XElement("RangoInicial", corralRango.RangoInicial),
                                                new XElement("RangoFinal", corralRango.RangoFinal),
                                                new XElement("UsuarioModificacionID", corralRango.UsuarioModificacionId)
                                                )
                                   );

                parametros = new Dictionary<string, object>
                    {
                        {"@XmlActualizar", xml.ToString()}
                        /*{"@CorralIDAnterior", filtro.CorralAnteriorID},
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@CorralID", filtro.CorralID},                      
                        {"@Sexo", filtro.Sexo},
                        {"@RangoInicial", filtro.RangoInicial},
                        {"@RangoFinal", filtro.RangoFinal},*/
                    /*    {"@FechaModificacion", filtro.FechaModificacion},*/
                        /*{"@UsuarioModificacionID", filtro.UsuarioModificacionId}*/
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
        /// Metodo que regresa los parametros a enviar a la consulta que valida si tiene lote asignado o no
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosLoteAsignado(int organizacionID, int corralID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@CorralID", corralID}
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
        /// Metodo que regresa los parametros a enviar a la consulta que obtiene el corral destino
        /// </summary>
        /// <param name="corralRangoInfo"></param>
        /// /// <param name="dias"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerCorralDestino(CorralRangoInfo corralRangoInfo,int dias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Sexo", corralRangoInfo.Sexo},
                        {"@Peso", corralRangoInfo.RangoInicial},
                        /*{"@TipoGanadoID", corralRangoInfo.TipoGanadoID},*/
                        {"@OrganizacionID", corralRangoInfo.OrganizacionID},
                        {"@DiasValido", dias}
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
        /// Metodo que regresa los parametros a enviar a la consulta que obtiene el corral destino
        /// </summary>
        /// <param name="corralRangoInfo"></param>
        /// <param name="diasBloqueo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerCorralDestinoSinTipoGanado(CorralRangoInfo corralRangoInfo, int diasBloqueo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Sexo", corralRangoInfo.Sexo},
                        {"@Peso", corralRangoInfo.RangoInicial},
                        {"@OrganizacionID", corralRangoInfo.OrganizacionID},
                        {"DiasValido", diasBloqueo}
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
        /// Metodo que regresa los parametros a enviar a la eliminaciond e corrales disponibles
        /// </summary>
        /// <param name="corralGrid"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEliminar(CorralRangoInfo corralGrid)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", corralGrid.OrganizacionID},
                        {"@CorralID", corralGrid.CorralID}
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
