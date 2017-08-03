using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxConfiguracionAlertasDAL
    {
        /// <summary>
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ConfiguracionAlertasConsulta
        /// </summary>
        /// <param name="paginas"></param>
        /// <param name="filtros"></param>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ConsultarConfiguracionAlertas(PaginacionInfo paginas, ConfiguracionAlertasGeneraInfo filtros)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                { 
                    { "@Descripcion",  filtros.AlertaInfo.Descripcion ?? ""},
                    { "@Activo",  filtros.AlertaInfo.ConfiguracionAlerta.Activo},
                    { "@Inicio", paginas.Inicio},
                    { "@Limite", paginas.Limite}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ConfiguracionAlertasConsultaAccion
        /// </summary>
        /// <param name="id"></param>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ObtenerListaAcciones(int id)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    { "@ID",  id}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ConfiguracionAlertas_CrearNueva
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> InsertarConfiguracionAlerta(ConfiguracionAlertasGeneraInfo filtros)
        {
            Dictionary<string, object> parametros;
            var lista = filtros.ListaAccionInfo;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from accion in filtros.ListaAlertaAccionInfo
                                 select new XElement("Acciones",
                                                     new XElement("AccionID",accion.AccionId ),
                                                     new XElement("Activo", EstatusEnum.Activo.GetHashCode())
                                     ));

                parametros = new Dictionary<string, object>
                {
                    { "@AlertaID",  filtros.AlertaInfo.AlertaID},
                    { "@Activo",  filtros.ConfiguracionAlertas.Activo},
                    { "@Datos", filtros.ConfiguracionAlertas.Datos},
                    { "@Fuentes", filtros.ConfiguracionAlertas.Fuentes},
                    { "@Condiciones", filtros.ConfiguracionAlertas.Condiciones},
                    { "@Agrupador", filtros.ConfiguracionAlertas.Agrupador},
                    { "@UsuarioCreacionID", filtros.ConfiguracionAlertas.UsuarioCreacionID},
                    { "@NivelAlertaID", filtros.ConfiguracionAlertas.NivelAlerta.NivelAlertaId},
                    { "@XmlAcciones", xml.ToString()}
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
        /// Obtiene un diccionario con los parametros nesesarios para la ejecucion del procedimiento almacenado
        /// ConfiguracionAlertas_Update
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> EditarConfiguracionAlerta(ConfiguracionAlertasGeneraInfo filtros)
        {
            Dictionary<string, object> parametros;
            var lista = filtros.ListaAccionInfo;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from accion in filtros.ListaAlertaAccionInfo
                                 select new XElement("Acciones",
                                                     new XElement("AccionID", accion.AccionId),
                                                     new XElement("AlertaAccionID", accion.AlertaAccionId),
                                                     new XElement("AlertaID", accion.AlertaId),
                                                     new XElement("Activo", EstatusEnum.Activo.GetHashCode())
                                     ));

                parametros = new Dictionary<string, object>
                {
                    { "@AlertaConfiguracionID" , filtros.ConfiguracionAlertas.AlertaConfiguracionID},
                    { "@AlertaID",  filtros.AlertaInfo.AlertaID},
                    { "@Activo",  filtros.ConfiguracionAlertas.Activo},
                    { "@Datos", filtros.ConfiguracionAlertas.Datos},
                    { "@Fuentes", filtros.ConfiguracionAlertas.Fuentes},
                    { "@Condiciones", filtros.ConfiguracionAlertas.Condiciones},
                    { "@Agrupador", filtros.ConfiguracionAlertas.Agrupador},
                    { "@UsuarioModificacionID", filtros.AlertaInfo.UsuarioModificacionID},
                    { "@NivelAlertaID", filtros.ConfiguracionAlertas.NivelAlerta.NivelAlertaId},
                    { "@XmlAcciones", xml.ToString()}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para ejecutar el procedimiento almacenado Alertas_ObtenerTodas
        /// </summary>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ObtenerTodasLasAlertasActivas()
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@Activo", EstatusEnum.Activo}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para ejecutar el procedimiento almacenado Acciones_ObtenerTodasLasActivas
        /// </summary>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ObtenerTodasLasAccionesActivas()
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@Activo", EstatusEnum.Activo}
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
        /// Obtiene un diccionario con los parametros
        /// para ejecutar el procedimiento almacenado ConfiguracionAlerta_ObtenerAlertaPorID
        /// </summary>
        /// <param name="idAlerta"></param>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ConsultaAlerta(long idAlerta)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@IDAlerta", idAlerta}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ConfiguracionAlertas_ObtenerAlertasPorPaginas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns>los parametros para ejecutar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaFiltroAlertas(PaginacionInfo pagina, AlertaInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@Inicio", pagina.Inicio},
                    {"@Limite", pagina.Limite},
                    {"@Descripcion", filtro.Descripcion}
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
