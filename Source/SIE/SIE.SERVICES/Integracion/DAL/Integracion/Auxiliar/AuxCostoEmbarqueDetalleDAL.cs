using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxCostoEmbarqueDetalleDAL
    {
        /// <summary>
        ///     Obtiene parametros para crear
        /// </summary>
        /// <param name="costoEmbarqueDetalleInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardado(CostoEmbarqueDetalleInfo costoEmbarqueDetalleInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueDetalleID", costoEmbarqueDetalleInfo.EmbarqueDetalleID},
                        {"@CostoID", costoEmbarqueDetalleInfo.Costo.CostoID},
                        {"@RenglonEscala", costoEmbarqueDetalleInfo.Orden},
                        {"@Importe", costoEmbarqueDetalleInfo.Importe},
                        {"@Activo", costoEmbarqueDetalleInfo.Activo}
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="costoEmbarqueDetalleID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int costoEmbarqueDetalleID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CostoEmbarqueDetalleID", costoEmbarqueDetalleID}
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="embarqueDetalleID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorEmbarqueDetalleID(int embarqueDetalleID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueDetalleID", embarqueDetalleID}
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
        ///     Obtiene parametros para actualizar
        /// </summary>
        /// <param name="costoEmbarqueDetalleInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(CostoEmbarqueDetalleInfo costoEmbarqueDetalleInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CostoEmbarqueDetalleID", costoEmbarqueDetalleInfo.CostoEmbarqueDetalleID},
                        {"@EmbarqueDetalleID", costoEmbarqueDetalleInfo.EmbarqueDetalleID},
                        {"@CostoID", costoEmbarqueDetalleInfo.Costo.CostoID},
                        {"@RenglonEscala", costoEmbarqueDetalleInfo.Orden},
                        {"@Importe", costoEmbarqueDetalleInfo.Importe},
                        {"@Activo", costoEmbarqueDetalleInfo.Activo}
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
        ///     Obtienelos parametros por la Entrada de Ganado
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorEmbarqueIDOrganizacionOrigen(EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueID", entradaGanado.EmbarqueID},
                        {"@OrganizacionOrigenID", entradaGanado.OrganizacionOrigenID}
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
        ///     Obtiene Parametros pora filtrar por estatus
        /// </summary>
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", estatus.GetHashCode()}
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
        ///     Obtienelos parametros por la Entrada de Ganado
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorEmbarqueID(EntradaGanadoInfo entradaGanado)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@EmbarqueID", entradaGanado.EmbarqueID}
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
        /// Obtiene los parametros del embarque detalle para la busqueda de costos relacionados  
        /// </summary>
        /// <param name="embarqueDetalleInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorProveedorIDOrigenIDDestinoIDCostoID(EmbarqueInfo embarqueInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@TipoEmbarqueID", embarqueInfo.TipoEmbarque.TipoEmbarqueID},
                        {"@ProveedorID", embarqueInfo.ListaEscala[0].Proveedor.ProveedorID},
                        {"@OrganizacionOrigenID", embarqueInfo.ListaEscala[0].OrganizacionOrigen.OrganizacionID},
                        {"@OrganizacionDestinoID",embarqueInfo.ListaEscala[0].OrganizacionDestino.OrganizacionID},
                        {"@CostoID", embarqueInfo.ListaEscala[0].ListaCostoEmbarqueDetalle[0].Costo.CostoID}
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
        /// Obtiene los parametros de embarque detalle para la busqueda de los embarques detalles que coincidan
        /// </summary>
        /// <param name="embarqueInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerEmbarqueDetallesCostosPorProveedorIDOrigenIDDestinoIDCostoID(EmbarqueInfo embarqueInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@TipoEmbarqueID", embarqueInfo.TipoEmbarque.TipoEmbarqueID},
                        {"@ProveedorID", embarqueInfo.ListaEscala[0].Proveedor.ProveedorID},
                        {"@OrganizacionOrigenID", embarqueInfo.ListaEscala[1].OrganizacionOrigen.OrganizacionID},
                        {"@OrganizacionDestinoID",embarqueInfo.ListaEscala[2].OrganizacionDestino.OrganizacionID},
                        {"@OrdenDestino", embarqueInfo.ListaEscala[2].Orden },
                        {"@CostoID", embarqueInfo.ListaEscala[0].ListaCostoEmbarqueDetalle[0].Costo.CostoID}
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