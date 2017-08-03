using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxEntradaGanadoCosteoDAL
    {
        /// <summary>
        ///     Obtiene parametros para Crear
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(EntradaGanadoCosteoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", info.Organizacion.OrganizacionID},
                            {"@EntradaGanadoID", info.EntradaGanadoID},
                            {"@Activo", info.Activo},
                            {"@UsuarioCreacion", info.UsuarioCreacionID},
                            {"@Observacion", info.Observacion}
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
        ///     Obtiene parametros para crear el detalle de la entrada
        /// </summary>
        /// <param name="listaEntradaDetalle"></param>
        /// <param name="entradaGanadoCosteoId">Identificador de la entrada ganado costeo </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoEntradaDetalle(
            IEnumerable<EntradaDetalleInfo> listaEntradaDetalle, int entradaGanadoCosteoId)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from info in listaEntradaDetalle
                                 select
                                     new XElement("EntradaDetalle",
                                                  new XElement("EntradaDetalleID", info.EntradaDetalleID),
                                                  new XElement("EntradaGanadoCosteoID", entradaGanadoCosteoId),
                                                  new XElement("TipoGanadoID", info.TipoGanado.TipoGanadoID),
                                                  new XElement("Cabezas", info.Cabezas),
                                                  new XElement("PesoOrigen", info.PesoOrigen),
                                                  new XElement("PesoLlegada", info.PesoLlegada),
                                                  new XElement("PrecioKilo", info.PrecioKilo),
                                                  new XElement("Importe", info.Importe),
                                                  new XElement("ImporteOrigen", info.ImporteOrigen),
                                                  new XElement("Activo", info.Activo.GetHashCode()),
                                                  new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                  new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                     ));

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlDetalle", xml.ToString()}
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
        ///     Obtiene parametros para crear el detalle de la entrada
        /// </summary>
        /// <param name="listaGanadoCalidad"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoCalidadGanado(
            IEnumerable<EntradaGanadoCalidadInfo> listaGanadoCalidad)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from info in listaGanadoCalidad
                                 where info.Valor > 0
                                 select
                                    new XElement("EntradaGanadoCalidad",
                                                 new XElement("EntradaGanadoCalidadID", info.EntradaGanadoCalidadID),
                                                 new XElement("EntradaGanadoID", info.EntradaGanadoID),
                                                 new XElement("CalidadGanadoID", info.CalidadGanado.CalidadGanadoID),
                                                 new XElement("Valor", info.Valor),
                                                 new XElement("Activo", info.Activo.GetHashCode()),
                                                 new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                 new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                    ));

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlDetalle", xml.ToString()}
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
        ///     Obtiene parametros para crear el detalle de la entrada
        /// </summary>
        /// <param name="listaGanadoCostoEntrada"> </param>
        /// <param name="entradaGanadoCosteoId"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoCostoEntrada(
            IEnumerable<EntradaGanadoCostoInfo> listaGanadoCostoEntrada, int entradaGanadoCosteoId)
        {
            try
            {

                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from info in listaGanadoCostoEntrada
                                 select
                                     new XElement("EntradaGanadoCosto",
                                                  new XElement("EntradaGanadoCostoID", info.EntradaGanadoCostoID),
                                                  new XElement("EntradaGanadoCosteoID", entradaGanadoCosteoId),
                                                  new XElement("CostoID", info.Costo.CostoID),
                                                  new XElement("TieneCuenta", info.TieneCuenta),
                                                  new XElement("ProveedorID", ValidarProveedor(info.Proveedor)),
                                                  new XElement("NumeroDocumento", info.NumeroDocumento),
                                                  new XElement("Importe", info.Importe),
                                                  new XElement("ProveedorComisionID", info.ProveedorComisionID),
                                                  new XElement("Iva", info.Iva),
                                                  new XElement("Retencion", info.Retencion),
                                                  new XElement("Origen", info.Origen),
                                                  new XElement("Activo", info.Activo.GetHashCode()),
                                                  new XElement("UsuarioCreacion", info.UsuarioCreacionID),
                                                  new XElement("UsuarioModificacion", info.UsuarioModificacionID),
                                                  new XElement("CuentaProvision", info.CuentaProvision)
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlDetalle", xml.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static object ValidarProveedor(ProveedorInfo proveedorInfo)
        {
            if (proveedorInfo == null)
            {
                return DBNull.Value;
            }
            return proveedorInfo.ProveedorID;
        }

        /// <summary>
        ///     Obtiene parametros para Actualizar
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(EntradaGanadoCosteoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaGanadoCosteoID", info.EntradaGanadoCosteoID},
                            {"@OrganizacionID", info.Organizacion.OrganizacionID},
                            {"@EntradaGanadoID", info.EntradaGanadoID},
                            {"@Activo", info.Activo},
                            {"@UsuarioModificacion", info.UsuarioModificacionID},
                            {"@Observacion", info.Observacion}
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
        ///     Obtiener Parametros por Id
        /// </summary>
        /// <param name="entradaGanadoCosteoID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int entradaGanadoCosteoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaGanadoCosteoID", entradaGanadoCosteoID}
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
        ///     Obtiene Parametros por EntradaGanadoId
        /// </summary>
        /// <param name="entradaGanadoId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorEntradaGanadoID(int entradaGanadoId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaGanadoID", entradaGanadoId},
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
        ///     Obtiene Parametros por EntradaGanadoId
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorSalidaOrganizacion(EntradaGanadoInfo entradaGanado)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", entradaGanado.OrganizacionOrigenID},
                            {"@SalidaID", entradaGanado.FolioOrigen}
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
        /// Obtener los parametros para marcar el costeo de la entrada de ganado como prorrateado
        /// </summary>
        /// <param name="entradaGanadoCosteoId"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosInactivarProrrateoaCosteo(int entradaGanadoCosteoId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaGanadoCosteoID", entradaGanadoCosteoId}
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
        /// Obtiene los parametros necesarios para la 
        /// ejecucion del procedimiento almacenado
        /// EntradaGanadoCosteo_ObtenerPorFechaConciliacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFechasConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@FechaInicial", fechaInicial},
                            {"@FechaFinal", fechaFinal},
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
        ///     Obtiene Parametros por EntradaGanadoId
        /// </summary>
        /// <param name="entradaGanadoId"></param>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorEntradaGanadoID(int entradaGanadoId, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaGanadoID", entradaGanadoId},
                            {"@Estatus", estatus},
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
