using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxTraspasoMateriaPrimaDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, FiltroTraspasoMpPaMed filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@DescripcionProducto", filtro.DescripcionProducto},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@DiasPermitidos", filtro.DiasPermitidos},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(TraspasoMateriaPrimaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@ContratoOrigenID", info.ContratoOrigen.ContratoId},
                            {"@ContratoDestinoID", info.ContratoDestino.ContratoId},
							{"@AlmacenOrigenID", info.AlmacenOrigen.AlmacenID},
                            {"@AlmacenDestinoID", info.AlmacenDestino.AlmacenID},
							{"@InventarioLoteOrigenID", info.AlmacenInventarioLoteOrigen.AlmacenInventarioLoteId},
							{"@InventarioLoteDestinoID", info.AlmacenInventarioLoteDestino.AlmacenInventarioLoteId},
							{"@CuentaSAPID", info.CuentaSAP.CuentaSAPID},
							{"@Justificacion", info.Justificacion},
							{"@AlmacenMovimientoEntradaID", info.AlmacenMovimientoDestino.AlmacenMovimientoID},
							{"@AlmacenMovimientoSalidaID", info.AlmacenMovimientoOrigen.AlmacenMovimientoID},
							{"@Activo", info.Activo},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
                            {"@TipoFolio",TipoFolio.TraspasoMateriaPrima.GetHashCode()},
                            {"@OrganizacionID",info.Organizacion.OrganizacionID},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(TraspasoMateriaPrimaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@TraspasoMateriaPrimaID", info.TraspasoMateriaPrimaID},
							{"@ContratoOrigenID", info.ContratoOrigen.ContratoId},
                            {"@ContratoDestinoID", info.ContratoDestino.ContratoId},
							{"@AlmacenOrigenID", info.AlmacenOrigen.AlmacenID},
                            {"@AlmacenDestinoID", info.AlmacenDestino.AlmacenID},
							{"@FolioTraspaso", info.FolioTraspaso},
							{"@InventarioLoteOrigenID", info.AlmacenInventarioLoteOrigen.AlmacenInventarioLoteId},
							{"@InventarioLoteDestinoID", info.AlmacenInventarioLoteDestino.AlmacenInventarioLoteId},
							{"@CuentaSAPID", info.CuentaSAP.CuentaSAPID},
							{"@Justificacion", info.Justificacion},
							{"@AlmacenMovimientoEntradaID", info.AlmacenMovimientoOrigen.AlmacenMovimientoID},
							{"@AlmacenMovimientoSalidaID", info.AlmacenMovimientoDestino.AlmacenMovimientoID},
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
        /// <param name="traspasoMateriaPrimaID">Identificador de la entidad TraspasoMateriaPrima</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int traspasoMateriaPrimaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TraspasoMateriaPrimaID", traspasoMateriaPrimaID}
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
								{"@TraspasoMateriaPrimaID", descripcion}
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorFolio(FiltroTraspasoMpPaMed filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@FolioTraspaso", filtro.Folio},
                            {"@Activo", filtro.Activo.GetHashCode()},
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

