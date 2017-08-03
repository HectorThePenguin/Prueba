using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxCheckListRoladoraDetalleDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CheckListRoladoraDetalleInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CheckListRoladoraDetalleID", filtro.CheckListRoladoraDetalleID},
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosCrear(CheckListRoladoraDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@CheckListRoladoraID", info.CheckListRoladora.CheckListRoladoraID},
							{"@CheckListRoladoraRangoID", info.CheckListRoladoraRango.CheckListRoladoraRangoID},
							{"@CheckListRoladoraAccionID", info.CheckListRoladoraAccion.CheckListRoladoraAccionID},
							{"@Activo", info.Activo},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
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
        ///  Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosActualizar(CheckListRoladoraDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@CheckListRoladoraDetalleID", info.CheckListRoladoraDetalleID},
							{"@CheckListRoladoraID", info.CheckListRoladora.CheckListRoladoraID},
							{"@CheckListRoladoraRangoID", info.CheckListRoladoraRango.CheckListRoladoraRangoID},
							{"@CheckListRoladoraAccionID", info.CheckListRoladoraAccion.CheckListRoladoraAccionID},
							{"@Activo", info.Activo},
                            {"@UsuarioModificacionID", info.UsuarioModificacionID},
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
        ///  Obtiene Parametros por Id
        /// </summary>
        /// <param name="checkListRoladoraDetalleID">Identificador de la entidad CheckListRoladoraDetalle</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int checkListRoladoraDetalleID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@CheckListRoladoraDetalleID", checkListRoladoraDetalleID}
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
        /// Obtiene Parametro pora filtrar por estatus 
        /// </summary> 
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@Activo", estatus}
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
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="descripcion">Descripción de la entidad </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@CheckListRoladoraDetalleID", descripcion}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosGuardar(List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in checkListRoladoraDetalle
                                 select new XElement("CheckListRoladoraDetalle",
                                                     new XElement("CheckListRoladoraID", detalle.CheckListRoladoraID),
                                                     new XElement("CheckListRoladoraRangoID",
                                                                  detalle.CheckListRoladoraRangoID),
                                                     new XElement("CheckListRoladoraAccionID",
                                                                  detalle.CheckListRoladoraAccionID.HasValue
                                                                      ? detalle.CheckListRoladoraAccionID
                                                                      : null),
                                                     new XElement("Activo", detalle.Activo.GetHashCode()),
                                                     new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlCheckListRoladoraDetalle", xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosCheckListCompleto(int organizacionID, int turno, int roladoraId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@Turno", turno},
                            {"@RoladoraID", roladoraId},
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

