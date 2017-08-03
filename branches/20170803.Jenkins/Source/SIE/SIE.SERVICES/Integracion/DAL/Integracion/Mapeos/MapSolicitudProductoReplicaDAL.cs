using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal static class MapSolicitudProductoReplicaDAL
    {

        /// <summary>
        ///     Metodo que mapea un folio de solicitud de productos de almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static SolicitudProductoReplicaInfo ObtenerPorID(DataSet ds)
        {
            SolicitudProductoReplicaInfo cab;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var almacenDAL = new Implementacion.AlmacenDAL();
                cab = new SolicitudProductoReplicaInfo()
                {
                    OrganizacionID = Convert.ToInt32(dt.Rows[0]["OrganizacionID"]),
                    FolioSolicitud = Convert.ToInt64(dt.Rows[0]["FolioSolicitud"]),
                    Solicitud = new FolioSolicitudInfo
                    {
                        FolioSolicitud = Convert.ToInt64(dt.Rows[0]["FolioSolicitud"])
                    },
                    AlmacenDestino = almacenDAL.ObtenerPorID(Convert.ToInt32(dt.Rows[0]["AlmacenDestinoID"])),
                    Activo = Convert.ToBoolean(dt.Rows[0]["Activo"]).BoolAEnum()

                };
                cab.Detalle = (from solicitudInfo in dt.AsEnumerable()
                               select new SolicitudProductoReplicaDetalleInfo
                               {
                                   InterfaceTraspasoSAPID = solicitudInfo.Field<int>("InterfaceTraspasoSAPID"),
                                   Producto = new ProductoInfo { ProductoId = solicitudInfo.Field<int>("ProductoID") },
                                   FolioSolicitud = solicitudInfo.Field<long>("FolioSolicitud"),
                                   ProductoID = solicitudInfo.Field<int>("ProductoID"),
                                   CuentaSAP = solicitudInfo.Field<string>("CuentaSAP"),
                                   Cantidad = solicitudInfo.Field<decimal>("Cantidad"),
                                   PrecioUnitario = solicitudInfo.Field<decimal>("PrecioUnitario"),
                                   FechaCreacion = solicitudInfo.Field<System.DateTime>("FechaCreacion"),
                                   Activo = solicitudInfo.Field<bool>("ActivoDetalle"),
                                   AlmacenMovimientoID = solicitudInfo.Field<long>("AlmacenMovimientoID"),
                                   Editar = (solicitudInfo.Field<long>("AlmacenMovimientoID") == 0) ? true : false,
                                   Material = solicitudInfo.Field<string>("Material")
                               }).ToList();

                var cont = cab.Detalle.Where(a => a.AlmacenMovimientoID > 0).Count();
                cab.Guardar = (cont == 0) ? true : false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return cab;
        }

        /// <summary>
        ///     Metodo que mapea un folio de solicitud de productos de almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static FolioSolicitudInfo ObtenerPorIDAyuda(DataSet ds)
        {
            FolioSolicitudInfo fol;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                fol = new FolioSolicitudInfo()
                {
                    FolioSolicitud = Convert.ToInt64(dt.Rows[0]["FolioSolicitud"]),
                    //AlmacenDestinoDescripcion = Convert.ToString(dt.Rows[0]["AlmacenDestinoDescripcion"])
                    AlmacenDestinoID = Convert.ToInt32(dt.Rows[0]["AlmacenDestinoID"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return fol;
        }

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<SolicitudProductoReplicaInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<SolicitudProductoReplicaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SolicitudProductoReplicaInfo cab = new SolicitudProductoReplicaInfo()
                {
                    InterfaceTraspasoSAPID = Convert.ToInt32(dt.Rows[0]["InterfaceTraspasoSAPID"]),
                    OrganizacionID = Convert.ToInt32(dt.Rows[0]["OrganizacionID"]),
                    FolioSolicitud = Convert.ToInt64(dt.Rows[0]["FolioSolicitud"]),
                    Solicitud = new FolioSolicitudInfo
                                    {
                                        FolioSolicitud = Convert.ToInt64(dt.Rows[0]["FolioSolicitud"]),
                                        AlmacenDestinoDescripcion = Convert.ToString(dt.Rows[0]["AlmacenDestinoDescripcion"])

                                    },
                    Activo = Convert.ToBoolean(dt.Rows[0]["Activo"]).BoolAEnum()
                };
                cab.Detalle = (from solicitudInfo in dt.AsEnumerable()
                               select new SolicitudProductoReplicaDetalleInfo
                                                           {
                                                               Producto = new ProductoInfo { ProductoId = solicitudInfo.Field<int>("ProductoID") },
                                                               ProductoID = solicitudInfo.Field<int>("ProductoID"),
                                                               CuentaSAP = solicitudInfo.Field<string>("CuentaSAP"),
                                                               Cantidad = solicitudInfo.Field<decimal>("Cantidad"),
                                                               FechaCreacion = solicitudInfo.Field<System.DateTime>("FechaCreacion"),
                                                               Activo = solicitudInfo.Field<bool>("ActivoDetalle")
                                                           }).ToList();
                resultado = new ResultadoInfo<SolicitudProductoReplicaInfo>
                {
                    Lista = new List<SolicitudProductoReplicaInfo>()
                    {
                        cab
                    },
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<FolioSolicitudInfo> ObtenerPorPaginaAyuda(DataSet ds)
        {
            ResultadoInfo<FolioSolicitudInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FolioSolicitudInfo> cab = new List<FolioSolicitudInfo>();
                foreach (DataRow a in dt.Rows)
                {
                    cab.Add( new FolioSolicitudInfo()
                    {
                        FolioSolicitud = Convert.ToInt64(a["FolioSolicitud"]),
                        AlmacenDestinoDescripcion = Convert.ToString(a["AlmacenDestinoDescripcion"]),
                        OrganizacionID = Convert.ToInt32(a["OrganizacionID"]),
                        AlmacenDestinoID = Convert.ToInt32(a["AlmacenDestinoID"])
                    });
                }
                resultado = new ResultadoInfo<FolioSolicitudInfo>
                {
                    Lista = cab,
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

    }
}
