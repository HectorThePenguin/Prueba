using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using System.Collections.Generic;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapGastoMateriaPrimaDAL
    {
        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static GastoMateriaPrimaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                GastoMateriaPrimaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GastoMateriaPrimaInfo
                             {
                                 GastoMateriaPrimaID = info.Field<int>("GastoMateriaPrimaID"),
                                 Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                                 FolioGasto = info.Field<long>("FolioGasto"),
                                 TipoMovimiento = new TipoMovimientoInfo { TipoMovimientoID = info.Field<int>("TipoMovimientoID"), Descripcion = info.Field<string>("TipoMovimiento") },
                                 Fecha = info.Field<DateTime>("Fecha"),
                                 Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                                 TieneCuenta = info.Field<bool>("TieneCuenta"),
                                 CuentaSAP = new CuentaSAPInfo
                                     {
                                         CuentaSAPID = info.Field<int?>("CuentaSAPID") != null ? info.Field<int>("CuentaSAPID") : 0, 
                                         Descripcion = info.Field<string>("CuentaSAP")
                                     },
                                 Proveedor = new ProveedorInfo
                                     {
                                         ProveedorID = info.Field<int?>("ProveedorID") != null ? info.Field<int>("ProveedorID") : 0, 
                                         Descripcion = info.Field<string>("Proveedor")
                                     },
                                 AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                 AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                                     {
                                         AlmacenInventarioLoteId = info.Field<int?>("AlmacenInventarioLoteID") != null ? info.Field<int>("AlmacenInventarioLoteID") : 0
                                     },
                                 Importe = info.Field<decimal>("Importe"),
                                 Iva = info.Field<bool>("IVA"),
                                 Observaciones = info.Field<string>("Observaciones"),
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
        /// Obtiene mapeo de gastos de materia prima
        /// para la conciliacion
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<GastoMateriaPrimaInfo> ObtenerPolizasConciliacion()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<GastoMateriaPrimaInfo> mapGastoMateriaPrima = MapeoBasico();
                MapeoCuentaSAP(mapGastoMateriaPrima);
                MapeoAlmacenInventarioLote(mapGastoMateriaPrima);
                MapeoProveedor(mapGastoMateriaPrima);
                MapeoOrganizacion(mapGastoMateriaPrima);
                MapeoTipoMovimiento(mapGastoMateriaPrima);
                MapeoProducto(mapGastoMateriaPrima);
                return mapGastoMateriaPrima;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoProducto(IMapBuilderContext<GastoMateriaPrimaInfo> mapGastoMateriaPrima)
        {
            try
            {
                Logger.Info();
                mapGastoMateriaPrima.Map(x => x.Producto).WithFunc(x => new ProductoInfo
                {
                    ProductoId = Convert.ToInt32(x["ProductoID"]),
                    Descripcion = Convert.ToString(x["Producto"]),
                    ProductoDescripcion = Convert.ToString(x["Producto"]),
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoTipoMovimiento(IMapBuilderContext<GastoMateriaPrimaInfo> mapGastoMateriaPrima)
        {
            try
            {
                Logger.Info();
                mapGastoMateriaPrima.Map(x => x.TipoMovimiento).WithFunc(x => new TipoMovimientoInfo
                {
                    TipoMovimientoID = Convert.ToInt32(x["TipoMovimientoID"])
                });
                mapGastoMateriaPrima.Map(x => x.EsEntrada).WithFunc(
                    entrada =>
                    Convert.ToInt32(entrada["TipoMovimientoID"]) == TipoMovimiento.EntradaPorAjuste.GetHashCode());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoOrganizacion(IMapBuilderContext<GastoMateriaPrimaInfo> mapGastoMateriaPrima)
        {
            try
            {
                Logger.Info();
                mapGastoMateriaPrima.Map(x => x.Organizacion).WithFunc(x => new OrganizacionInfo
                {
                    OrganizacionID = Convert.ToInt32(x["OrganizacionID"]),
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoProveedor(IMapBuilderContext<GastoMateriaPrimaInfo> mapGastoMateriaPrima)
        {
            try
            {
                Logger.Info();
                mapGastoMateriaPrima.Map(x => x.Proveedor).WithFunc(x => new ProveedorInfo
                                                                             {
                                                                                 ProveedorID = Convert.ToInt32(x["ProveedorID"]),
                                                                                 CodigoSAP = Convert.ToString(x["Proveedor"])
                                                                             });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoAlmacenInventarioLote(IMapBuilderContext<GastoMateriaPrimaInfo> mapGastoMateriaPrima)
        {
            try
            {
                Logger.Info();
                mapGastoMateriaPrima.Map(x => x.AlmacenInventarioLote).WithFunc(x => new AlmacenInventarioLoteInfo
                                                                                         {
                                                                                             AlmacenInventarioLoteId = Convert.ToInt32(x["AlmacenInventarioLoteID"])
                                                                                         });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCuentaSAP(IMapBuilderContext<GastoMateriaPrimaInfo> mapGastoMateriaPrima)
        {
            try
            {
                Logger.Info();
                mapGastoMateriaPrima.Map(x => x.CuentaSAP).WithFunc(x => new CuentaSAPInfo
                                                                             {
                                                                                 CuentaSAP =
                                                                                     Convert.ToString(x["CuentaSAP"]),
                                                                                 CuentaSAPID =
                                                                                     Convert.ToInt32(x["CuentaSAPID"]),
                                                                                 Descripcion =
                                                                                     Convert.ToString(x["Cuenta"])
                                                                             });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static IMapBuilderContext<GastoMateriaPrimaInfo> MapeoBasico()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<GastoMateriaPrimaInfo> mapGastoMateriaPrima =
                    MapBuilder<GastoMateriaPrimaInfo>.MapNoProperties();
                mapGastoMateriaPrima.Map(x => x.GastoMateriaPrimaID).ToColumn("GastoMateriaPrimaID");
                mapGastoMateriaPrima.Map(x => x.FolioGasto).ToColumn("FolioGasto");
                mapGastoMateriaPrima.Map(x => x.Fecha).ToColumn("Fecha");
                mapGastoMateriaPrima.Map(x => x.TieneCuenta).ToColumn("TieneCuenta");
                mapGastoMateriaPrima.Map(x => x.Importe).ToColumn("Importe");
                mapGastoMateriaPrima.Map(x => x.Iva).ToColumn("IVA");
                mapGastoMateriaPrima.Map(x => x.Observaciones).ToColumn("Observaciones");
                mapGastoMateriaPrima.Map(x => x.AlmacenMovimientoID).ToColumn("AlmacenMovimientoID");
                mapGastoMateriaPrima.Map(x => x.AlmacenID).ToColumn("AlmacenID");
                return mapGastoMateriaPrima;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static IEnumerable<AreteInfo> MapeoAretes(DataSet ds)
        {
            List<AreteInfo> aretes;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                aretes = (from info in dt.AsEnumerable()
                             select new AreteInfo
                             {
                                 Arete = info.Field<string>("Arete")
                             }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return aretes;
        }
    }
}
