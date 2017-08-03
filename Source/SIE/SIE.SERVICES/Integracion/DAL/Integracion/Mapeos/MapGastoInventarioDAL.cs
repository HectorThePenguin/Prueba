using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapGastoInventarioDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<GastoInventarioInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<GastoInventarioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new GastoInventarioInfo
                             {
								GastoInventarioID = info.Field<int>("GastoInventarioID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								TipoGasto = info.Field<string>("TipoGasto").TipoGastoAEnum(),
								FolioGasto = info.Field<long>("FolioGasto"),
								FechaGasto = info.Field<DateTime>("FechaGasto"),
								Costo = new CostoInfo { CostoID = info.Field<int>("CostoID"), Descripcion = info.Field<string>("Costo") },
								TieneCuenta = info.Field<bool>("TieneCuenta"),
								CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAP") },
								Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("Proveedor") },
								Factura = info.Field<string>("Factura"),
								Importe = info.Field<decimal>("Importe"),
								IVA = info.Field<bool>("IVA"),
								Observaciones = info.Field<string>("Observaciones"),
								Retencion = info.Field<bool>("Retencion"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<GastoInventarioInfo>
                        {
                            Lista = lista,
                            TotalRegistros = totalRegistros
                        };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<GastoInventarioInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<GastoInventarioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new GastoInventarioInfo
                             {
								GastoInventarioID = info.Field<int>("GastoInventarioID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								TipoGasto = info.Field<string>("TipoGasto").TipoGastoAEnum(),
								FolioGasto = info.Field<long>("FolioGasto"),
								FechaGasto = info.Field<DateTime>("FechaGasto"),
								Costo = new CostoInfo { CostoID = info.Field<int>("CostoID"), Descripcion = info.Field<string>("Costo") },
								TieneCuenta = info.Field<bool>("TieneCuenta"),
								CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAP") },
								Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("Proveedor") },
								Factura = info.Field<string>("Factura"),
								Importe = info.Field<decimal>("Importe"),
								IVA = info.Field<bool>("IVA"),
								Observaciones = info.Field<string>("Observaciones"),
								Retencion = info.Field<bool>("Retencion"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static GastoInventarioInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                GastoInventarioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GastoInventarioInfo
                             {
								GastoInventarioID = info.Field<int>("GastoInventarioID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								TipoGasto = info.Field<string>("TipoGasto").TipoGastoAEnum(),
								FolioGasto = info.Field<long>("FolioGasto"),
								FechaGasto = info.Field<DateTime>("FechaGasto"),
								Costo = new CostoInfo
								    {
								        CostoID = info.Field<int>("CostoID"),
                                        Descripcion = info.Field<string>("Costo"),
                                        ClaveContable = info.Field<string>("ClaveContable")
								    },
								TieneCuenta = info.Field<bool>("TieneCuenta"),
								CuentaSAP = new CuentaSAPInfo
								    {
								        CuentaSAPID = info.Field<int?>("CuentaSAPID") != null ? info.Field<int>("CuentaSAPID") : 0, 
                                        CuentaSAP = info.Field<string>("CuentaSAP"),
                                        Descripcion = info.Field<string>("DescripcionCuentaSAP")
								    },
								Proveedor = new ProveedorInfo
								    {
								        ProveedorID = info.Field<int?>("ProveedorID") != null ? info.Field<int>("ProveedorID") : 0,
                                        Descripcion = info.Field<string>("Proveedor"),
                                        CodigoSAP = info.Field<string>("CodigoSAP")
								    },
								Factura = info.Field<string>("Factura"),
								Importe = info.Field<decimal>("Importe"),
								IVA = info.Field<bool>("IVA"),
								Observaciones = info.Field<string>("Observaciones"),
								Retencion = info.Field<bool>("Retencion"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static GastoInventarioInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                GastoInventarioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GastoInventarioInfo
                             {
								GastoInventarioID = info.Field<int>("GastoInventarioID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								TipoGasto = info.Field<string>("TipoGasto").TipoGastoAEnum(),
								FolioGasto = info.Field<long>("FolioGasto"),
								FechaGasto = info.Field<DateTime>("FechaGasto"),
								Costo = new CostoInfo { CostoID = info.Field<int>("CostoID"), Descripcion = info.Field<string>("Costo") },
								TieneCuenta = info.Field<bool>("TieneCuenta"),
								CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAP") },
								Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("Proveedor") },
								Factura = info.Field<string>("Factura"),
								Importe = info.Field<decimal>("Importe"),
								IVA = info.Field<bool>("IVA"),
								Observaciones = info.Field<string>("Observaciones"),
								Retencion = info.Field<bool>("Retencion"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static IMapBuilderContext<GastoInventarioInfo> ObtenerGastosInventarioConciliacion()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<GastoInventarioInfo> mapGastosInventario = MapeoBasico();
                MapeoOrganizacion(mapGastosInventario);
                MapeoCosto(mapGastosInventario);
                MapeoCuentaSAP(mapGastosInventario);
                MapeoProveedor(mapGastosInventario);
                MapeoCorral(mapGastosInventario);
                return mapGastosInventario;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCorral(IMapBuilderContext<GastoInventarioInfo> mapGastosInventario)
        {
            try
            {
                Logger.Info();
                mapGastosInventario.Map(x => x.Corral).WithFunc(x => new CorralInfo
                                                                         {
                                                                             CorralID = Convert.ToInt32(x["CorralID"]),
                                                                             Codigo = Convert.ToString(x["Codigo"])
                                                                         });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoProveedor(IMapBuilderContext<GastoInventarioInfo> mapGastosInventario)
        {
            try
            {
                Logger.Info();
                mapGastosInventario.Map(x => x.Proveedor).WithFunc(x => new ProveedorInfo
                                                                            {
                                                                                Descripcion =
                                                                                    Convert.ToString(x["Proveedor"]),
                                                                                CodigoSAP =
                                                                                    Convert.ToString(x["CodigoSAP"]),
                                                                                ProveedorID =
                                                                                    Convert.ToInt32(x["ProveedorID"])
                                                                            });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCuentaSAP(IMapBuilderContext<GastoInventarioInfo> mapGastosInventario)
        {
            try
            {
                Logger.Info();
                mapGastosInventario.Map(x => x.CuentaSAP).WithFunc(x => new CuentaSAPInfo
                                                                            {
                                                                                Descripcion =
                                                                                    Convert.ToString(
                                                                                        x["DescripcionCuentaSAP"]),
                                                                                CuentaSAP =
                                                                                    Convert.ToString(x["CuentaSAP"]),
                                                                                CuentaSAPID =
                                                                                    Convert.ToInt32(x["CuentaSAPID"])
                                                                            });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCosto(IMapBuilderContext<GastoInventarioInfo> mapGastosInventario)
        {
            try
            {
                Logger.Info();
                mapGastosInventario.Map(x => x.Costo).WithFunc(x => new CostoInfo
                                                                        {
                                                                            CostoID = Convert.ToInt32(x["CostoID"]),
                                                                            Descripcion = Convert.ToString(x["Costo"]),
                                                                            ClaveContable =
                                                                                Convert.ToString(x["ClaveContable"])
                                                                        });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoOrganizacion(IMapBuilderContext<GastoInventarioInfo> mapGastosInventario)
        {
            try
            {
                Logger.Info();
                mapGastosInventario.Map(x => x.Organizacion).WithFunc(x => new OrganizacionInfo
                                                                               {
                                                                                   OrganizacionID = Convert.ToInt32(x["OrganizacionID"]),
                                                                                   Descripcion = Convert.ToString(x["Organizacion"]),
                                                                               });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static IMapBuilderContext<GastoInventarioInfo> MapeoBasico()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<GastoInventarioInfo> mapGastosInventario =
                    MapBuilder<GastoInventarioInfo>.MapNoProperties();
                mapGastosInventario.Map(x => x.GastoInventarioID).ToColumn("GastoInventarioID");
                mapGastosInventario.Map(x => x.TipoGasto).WithFunc(x => Convert.ToString(x["TipoGasto"]).TipoGastoAEnum());
                mapGastosInventario.Map(x => x.FolioGasto).ToColumn("FolioGasto");
                mapGastosInventario.Map(x => x.FechaGasto).ToColumn("FechaGasto");
                mapGastosInventario.Map(x => x.TieneCuenta).ToColumn("TieneCuenta");
                mapGastosInventario.Map(x => x.Factura).ToColumn("Factura");
                mapGastosInventario.Map(x => x.Importe).ToColumn("Importe");
                mapGastosInventario.Map(x => x.IVA).ToColumn("IVA");
                mapGastosInventario.Map(x => x.Observaciones).ToColumn("Observaciones");
                mapGastosInventario.Map(x => x.Retencion).ToColumn("Retencion");
                mapGastosInventario.Map(x => x.CentroCosto).ToColumn("CentroCosto");
                mapGastosInventario.Map(x => x.CuentaGasto).ToColumn("CuentaGasto");
                mapGastosInventario.Map(x => x.TotalCorrales).ToColumn("TotalCorrales");
                return mapGastosInventario;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

