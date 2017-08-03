using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapFleteInternoDAL
    {
        /// <summary>
        /// Obtiene un listado de fletes por estado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<FleteInternoInfo> ObtenerFletesPorEstado(DataSet ds)
        {
            List<FleteInternoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new FleteInternoInfo
                         {
                             FleteInternoId = info.Field<int>("FleteInternoID"),
                             Organizacion = new OrganizacionInfo() { OrganizacionID = info.Field<int>("OrganizacionID") },
                             TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = info.Field<int>("TipoMovimientoID")},
                             AlmacenOrigen = new AlmacenInfo() { AlmacenID = info.Field<int>("AlmacenIDOrigen")},
                             AlmacenDestino = new AlmacenInfo() { AlmacenID = info.Field<int>("AlmacenIDDestino")},
                             Producto = new ProductoInfo() { ProductoId = info.Field<int>("ProductoID") },
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Obtiene un listado de flete internos por pagina
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<FleteInternoInfo> ObtenerPorPaginaFiltroDescripcionOrganizacion(DataSet ds)
        {
            ResultadoInfo<FleteInternoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<FleteInternoInfo> lista = (from info in dt.AsEnumerable()
                                                select new FleteInternoInfo
                                            {
                                                FleteInternoId = info.Field<int>("FleteInternoID"),
                                                Organizacion = new OrganizacionInfo() { OrganizacionID = info.Field<int>("OrganizacionIDFleteInterno"), Descripcion = info.Field<string>("OrganizacionDescripcion") },
                                                OrganizacionDestino = new OrganizacionInfo() { OrganizacionID = info.Field<int>("OrganizacionIDDestino"), Descripcion = info.Field<string>("OrganizacionDescripcionDestino") },
                                                TipoMovimiento = new TipoMovimientoInfo() {TipoMovimientoID = info.Field<int>("TipoMovimientoID"), Descripcion = info.Field<string>("Descripcion")},
                                                AlmacenOrigen = new AlmacenInfo(){AlmacenID = info.Field<int>("AlmacenIDOrigen"), CodigoAlmacen = info.Field<string>("CodigoAlmacenOrigen"), Descripcion = info.Field<string>("DescripcionAlmacenOrigen")},
                                                AlmacenDestino = new AlmacenInfo() { AlmacenID = info.Field<int>("AlmacenIDDestino"), CodigoAlmacen = info.Field<string>("CodigoAlmacenDestino"), Descripcion = info.Field<string>("DescripcionAlmacenDestino") },
                                                Producto = new ProductoInfo() { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("DescripcionProducto"), Descripcion = info.Field<string>("DescripcionProducto"), SubfamiliaId = info.Field<int>("SubFamiliaID")},
                                                Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                Guardado = true
                                            }).ToList();
                resultado = new ResultadoInfo<FleteInternoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un flete interno por configuracion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static FleteInternoInfo ObtenerPorConfiguracion(DataSet ds)
        {
            FleteInternoInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new FleteInternoInfo
                                 {
                                     FleteInternoId = info.Field<int>("FleteInternoID"),
                                     Organizacion = new OrganizacionInfo() { OrganizacionID = info.Field<int>("OrganizacionIDFleteInterno"), Descripcion = info.Field<string>("OrganizacionDescripcionOrigen"), TipoOrganizacion = new TipoOrganizacionInfo() { TipoOrganizacionID = info.Field<int>("TipoOrganizacionID") } },
                                     OrganizacionDestino = new OrganizacionInfo(){OrganizacionID = info.Field<int>("OrganizacionDestinoID"), Descripcion = info.Field<string>("OrganizacionDescripcionDestino")},
                                     TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = info.Field<int>("TipoMovimientoID"), Descripcion = info.Field<string>("Descripcion") },
                                     AlmacenOrigen = new AlmacenInfo() { AlmacenID = info.Field<int>("AlmacenIDOrigen"), CodigoAlmacen = info.Field<string>("CodigoAlmacenOrigen"), Descripcion = info.Field<string>("DescripcionAlmacenOrigen") },
                                     AlmacenDestino = new AlmacenInfo() { AlmacenID = info.Field<int>("AlmacenIDDestino"), CodigoAlmacen = info.Field<string>("CodigoAlmacenDestino"), Descripcion = info.Field<string>("DescripcionAlmacenDestino") },
                                     Producto = new ProductoInfo() { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("DescripcionProducto"), Descripcion = info.Field<string>("DescripcionProducto"), SubfamiliaId = info.Field<int>("SubFamiliaID")},
                                     Activo = info.Field<bool>("Activo").BoolAEnum(),
                                     Guardado = true
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un listado de flete interno costo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<FleteInternoCostoInfo> ObtenerCostosPorFleteConfiguracion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FleteInternoCostoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FleteInternoCostoInfo
                         {
                             FleteInternoCostoId = info.Field<int>("FleteInternoCostoID"),
                             FleteInternoDetalleId = info.Field<int>("FleteInternoDetalleID"),
                             Costo = new CostoInfo() { CostoID = info.Field<int>("CostoID") },
                             Tarifa = info.Field<decimal>("Tarifa"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionId = info.Field<int>("UsuarioCreacionID"),
                             TipoTarifaID = info.Field<int?>("TipoTarifaID") != null ? info.Field<int>("TipoTarifaID") : 1 //Si tiene NULL, por default poner la tarifa por Tonelada,
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
