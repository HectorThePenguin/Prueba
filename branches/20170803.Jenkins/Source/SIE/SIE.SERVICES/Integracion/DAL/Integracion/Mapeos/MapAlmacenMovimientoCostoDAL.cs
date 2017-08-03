using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAlmacenMovimientoCostoDAL
    {
        /// <summary>
        /// Obtiene un listado por almacen movimiento id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenMovimientoCostoInfo> ObtenerPorAlmacenMovimientoId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenMovimientoCostoInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenMovimientoCostoInfo
                         {
                             AlmacenMovimientoCostoId = info.Field<int>("AlmacenMovimientoCostoID"),
                             AlmacenMovimientoId = info.Field<long>("AlmacenMovimientoID"),
                             Proveedor = new ProveedorInfo()
                             {
                                 ProveedorID = info["ProveedorID"]==DBNull.Value ? 0 : info.Field<int>("ProveedorID"), 
                                 CodigoSAP = info["CodigoSAP"]==DBNull.Value ? "" : info.Field<string>("CodigoSAP"), 
                                 Descripcion = info["ProveedorDescripcion"]==DBNull.Value ? "" : info.Field<string>("ProveedorDescripcion")
                             },
                             CuentaSap = new CuentaSAPInfo()
                             {
                                 CuentaSAPID = info["CuentaSAPID"]==DBNull.Value ? 0 : info.Field<int>("CuentaSAPID"), 
                                 CuentaSAP = info["CuentaSAP"]==DBNull.Value ? "" : info.Field<string>("CuentaSAP"), 
                                 Descripcion = info["CuentaSapDescripcion"]==DBNull.Value ? "" : info.Field<string>("CuentaSapDescripcion")
                             },
                             Costo = new CostoInfo()
                             {
                                 CostoID = info["CostoID"] == DBNull.Value ? 0 : info.Field<int>("CostoID"), 
                                 Descripcion = info["CostoDescripcion"] == DBNull.Value ? "" : info.Field<string>("CostoDescripcion"), 
                                 ClaveContable = info["ClaveContable"] == DBNull.Value ? "" : info.Field<string>("ClaveContable")
                             },
                             Importe = info.Field<decimal>("Importe"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             TieneCuenta = info.Field<bool>("TieneCuenta"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Iva = info.Field<bool>("Iva"),
                             Retencion = info.Field<bool>("Retencion")
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un mapeo de Almacen Movimiento Costo
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<AlmacenMovimientoCostoInfo> ObtenerAlmacenMovimientoCostoPorContratoXML()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<AlmacenMovimientoCostoInfo> mapAlmacenMovimientoCosto = MapeoBasico();
                MapeoCuentaSAP(mapAlmacenMovimientoCosto);
                MapeoCosto(mapAlmacenMovimientoCosto);
                MapeoProveedor(mapAlmacenMovimientoCosto);
                MapeoContrato(mapAlmacenMovimientoCosto);
                return mapAlmacenMovimientoCosto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoContrato(IMapBuilderContext<AlmacenMovimientoCostoInfo> mapAlmacenMovimientoCosto)
        {
            try
            {
                Logger.Info();
                mapAlmacenMovimientoCosto.Map(x => x.Contrato).WithFunc(x => new ContratoInfo
                                                                                 {
                                                                                     ContratoId =
                                                                                         Convert.ToInt32(x["ContratoID"]),
                                                                                 });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCuentaSAP(IMapBuilderContext<AlmacenMovimientoCostoInfo> mapeoAlmacenMoviiento)
        {
            try
            {
                Logger.Info();
                mapeoAlmacenMoviiento.Map(x => x.CuentaSap).WithFunc(x => new CuentaSAPInfo
                                                                              {
                                                                                  CuentaSAPID =
                                                                                      Convert.ToInt32(x["CuentaSAPID"]),
                                                                                  CuentaSAP =
                                                                                      Convert.ToString(x["CuentaSAP"])
                                                                              });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCosto(IMapBuilderContext<AlmacenMovimientoCostoInfo> mapeoAlmacenMoviiento)
        {
            try
            {
                Logger.Info();
                mapeoAlmacenMoviiento.Map(x => x.Costo).WithFunc(x => new CostoInfo
                                                                          {
                                                                              CostoID = Convert.ToInt32(x["CostoID"]),
                                                                              Descripcion = Convert.ToString(x["Costo"]),
                                                                              FechaCosto = Convert.ToDateTime(x["FechaCreacion"])
                                                                          });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoProveedor(IMapBuilderContext<AlmacenMovimientoCostoInfo> mapeoAlmacenMoviiento)
        {
            try
            {
                Logger.Info();
                mapeoAlmacenMoviiento.Map(x => x.Proveedor).WithFunc(x => new ProveedorInfo
                                                                              {
                                                                                  ProveedorID =
                                                                                      Convert.ToInt32(x["ProveedorID"]),
                                                                                  Descripcion =
                                                                                      Convert.ToString(x["Proveedor"]),
                                                                                  CodigoSAP =
                                                                                      Convert.ToString(x["CodigoSAP"])
                                                                              });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un mapeo basico de la clase Almacen Movimiento Costo
        /// </summary>
        /// <returns></returns>
        private static IMapBuilderContext<AlmacenMovimientoCostoInfo> MapeoBasico()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<AlmacenMovimientoCostoInfo> mapAlmacenMovimientoCosto =
                    MapBuilder<AlmacenMovimientoCostoInfo>.MapNoProperties();
                mapAlmacenMovimientoCosto.Map(x => x.AlmacenMovimientoCostoId).ToColumn("AlmacenMovimientoCostoID");
                mapAlmacenMovimientoCosto.Map(x => x.AlmacenMovimientoId).ToColumn("AlmacenMovimientoID");
                mapAlmacenMovimientoCosto.Map(x => x.TieneCuenta).ToColumn("TieneCuenta");
                mapAlmacenMovimientoCosto.Map(x => x.ProveedorId).ToColumn("ProveedorID");
                mapAlmacenMovimientoCosto.Map(x => x.CuentaSAPID).ToColumn("CuentaSAPID");
                mapAlmacenMovimientoCosto.Map(x => x.CostoId).ToColumn("CostoID");
                mapAlmacenMovimientoCosto.Map(x => x.Cantidad).ToColumn("Cantidad");
                mapAlmacenMovimientoCosto.Map(x => x.Importe).ToColumn("Importe");
                return mapAlmacenMovimientoCosto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado por almacen movimiento id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenMovimientoCostoInfo> ObtenerPorContratoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenMovimientoCostoInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenMovimientoCostoInfo
                         {
                             AlmacenMovimientoCostoId = info.Field<int>("AlmacenMovimientoCostoID"),
                             AlmacenMovimientoId = info.Field<long>("AlmacenMovimientoID"),
                             Proveedor = new ProveedorInfo()
                             {
                                 ProveedorID = info["ProveedorID"] == DBNull.Value ? 0 : info.Field<int>("ProveedorID"),
                                 CodigoSAP = info["CodigoSAP"] == DBNull.Value ? "" : info.Field<string>("CodigoSAP"),
                                 Descripcion = info["ProveedorDescripcion"] == DBNull.Value ? "" : info.Field<string>("ProveedorDescripcion")
                             },
                             CuentaSap = new CuentaSAPInfo()
                             {
                                 CuentaSAPID = info["CuentaSAPID"] == DBNull.Value ? 0 : info.Field<int>("CuentaSAPID"),
                                 CuentaSAP = info["CuentaSAP"] == DBNull.Value ? "" : info.Field<string>("CuentaSAP"),
                                 Descripcion = info["CuentaSapDescripcion"] == DBNull.Value ? "" : info.Field<string>("CuentaSapDescripcion")
                             },
                             Costo = new CostoInfo()
                             {
                                 CostoID = info["CostoID"] == DBNull.Value ? 0 : info.Field<int>("CostoID"),
                                 Descripcion = info["CostoDescripcion"] == DBNull.Value ? "" : info.Field<string>("CostoDescripcion"),
                                 ClaveContable = info["ClaveContable"] == DBNull.Value ? "" : info.Field<string>("ClaveContable")
                             },
                             Importe = info.Field<decimal>("Importe"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             TieneCuenta = info.Field<bool>("TieneCuenta"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Iva = info.Field<bool>("Iva"),
                             Retencion = info.Field<bool>("Retencion")
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un mapeo de almacen movimiento costo
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<AlmacenMovimientoCostoInfo> ObtenerMapeoAlmacenMovimientoCosto()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<AlmacenMovimientoCostoInfo> mapAlmacenMovimientoCosto = MapeoBasico();
                MapeoCuentaSAP(mapAlmacenMovimientoCosto);
                MapeoCosto(mapAlmacenMovimientoCosto);
                MapeoProveedor(mapAlmacenMovimientoCosto);
                return mapAlmacenMovimientoCosto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
