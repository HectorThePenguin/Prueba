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
    public class AuxProgramaciondeFletesDAL
    {
        /// <summary>
        /// Obtener parametros de fletes
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosObtenerContratosPorTipo(int activo,int tipoFlete)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", activo},
                            {"@TipoFleteID",tipoFlete},
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
        /// Obtener parametros fletes detalle
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosObtenerFletes(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", contratoInfo.ContratoId},
                            {"@Activo",EstatusEnum.Activo},

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
        /// optener parametros para guaradar programacion de flete
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GuardarProgramaciondeFlete(List<ProgramaciondeFletesInfo> listaProgramaciondeFletesInfos)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                //var lista = cabezasCortadas.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from programacionFlete in listaProgramaciondeFletesInfos
                               select
                                   new XElement("ProgramacionFlete",
                                                new XElement("ContratoID", programacionFlete.Flete.ContratoID),
                                                new XElement("OrganizacionID", programacionFlete.Organizacion.OrganizacionID),
                                                new XElement("ProveedorID", programacionFlete.Flete.Proveedor.ProveedorID),
                                                new XElement("MermaPermitida", programacionFlete.Flete.MermaPermitida),
                                                new XElement("TipoTarifaID",programacionFlete.Flete.TipoTarifa.TipoTarifaId),
                                                new XElement("UsuarioCreacionID", programacionFlete.Flete.UsuarioCreacionID),
                                                new XElement("UsuarioModificacionID", programacionFlete.Flete.UsuarioModificacionID),
                                                new XElement("Activo", (int)EstatusEnum.Activo),
                                                new XElement("Observaciones",programacionFlete.Flete.Observaciones),
                                                new XElement("Opcion",programacionFlete.Flete.Opcion)
                                                )
                                   );

                parametros = new Dictionary<string, object>
                    {
                        {"@XmlGuardarProgramacionFlete", xml.ToString()},
                        {"@Activo",(int)EstatusEnum.Activo}
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
        /// Obtiene los parametros para elimnar los provedores del detalle de los fletes
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        /// <param name="usuarioModifica"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerEliminarProveedorFlete(List<ProgramaciondeFletesInfo> listaProgramaciondeFletesInfos,int usuarioModifica)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                        from proveedor in listaProgramaciondeFletesInfos
                        select
                            new XElement("ProgramacionFleteDetalle",
                                new XElement("FleteID", proveedor.Flete.FleteID),
                                new XElement("Activo", (int) EstatusEnum.Inactivo),
                                new XElement("UsuarioModificacionID", usuarioModifica)
                                )
                        );

                parametros = new Dictionary<string, object>
                {
                    {"@XmlGuardarProgramacionFlete", xml.ToString()}
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
        /// Obtiene los parametros necesarios para eliminar el detalle del flete
        /// </summary>
        /// <param name="listaFleteDetalle"></param>
        /// <param name="usuarioModifica"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerEliminarFleteDetalle(List<FleteDetalleInfo> listaFleteDetalle, int usuarioModifica)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                        from detalle in listaFleteDetalle
                        select
                            new XElement("ProgramacionFleteDetalle",
                                new XElement("FleteDetalleID", detalle.FleteDetalleID),
                                new XElement("Activo", (int)EstatusEnum.Inactivo),
                                new XElement("UsuarioModificacionID", usuarioModifica)
                                )
                        );

                parametros = new Dictionary<string, object>
                {
                    {"@XmlFleteDetalle", xml.ToString()}
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
        /// Guarda el detalle del flete
        /// </summary>
        /// <param name="regresoCostosFletes"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GuardarProgramaciondeFleteDetalle(List<FleteDetalleInfo> regresoCostosFletes)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                //var lista = cabezasCortadas.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from programacionFleteDetalle in regresoCostosFletes
                               select
                                   new XElement("ProgramacionFleteDetalle",
                                                new XElement("FleteID", programacionFleteDetalle.FleteID),
                                                new XElement("FleteDetalleID", programacionFleteDetalle.FleteDetalleID),
                                                new XElement("CostoID", programacionFleteDetalle.CostoID),
                                                new XElement("Tarifa", programacionFleteDetalle.Tarifa),
                                                new XElement("Activo",(int)EstatusEnum.Activo),
                                                new XElement("UsuarioCreacionID", programacionFleteDetalle.UsuarioCreacion),
                                                new XElement("UsuarioModificacionID",programacionFleteDetalle.UsuarioModificacion),
                                                new XElement("Opcion",programacionFleteDetalle.Opcion)
                                                
                                                )
                                   );

                parametros = new Dictionary<string, object>
                    {
                        {"@XmlGuardarProgramacionFlete", xml.ToString()},
                        {"@Activo",(int)EstatusEnum.Activo}
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
        /// Obtiene los parametros para cancelar la programacion de fletes
        /// </summary>
        /// <param name="listaProgramaciondeFletesInfos"></param>
        /// <returns></returns>
        public static Dictionary<string, object> CancelarProgramacionFlete(ProgramaciondeFletesInfo listaProgramaciondeFletesInfos)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@FleteID", listaProgramaciondeFletesInfos.Flete.FleteID},
                        {"@UsuarioModificacionID",listaProgramaciondeFletesInfos.Flete.UsuarioModificacionID}
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, ContratoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioContrato", filtro.Folio},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@TipoFleteID", filtro.TipoFlete.TipoFleteId},
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
    }
}
