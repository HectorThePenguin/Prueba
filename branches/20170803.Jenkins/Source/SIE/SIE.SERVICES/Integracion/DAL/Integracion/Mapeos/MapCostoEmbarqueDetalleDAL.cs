using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  static class MapCostoEmbarqueDetalleDAL
    {
        /// <summary>
        ///     Metodo que obtiene un info de Costo Embarque Detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CostoEmbarqueDetalleInfo ObtenerPorID(DataSet ds)
        {
            CostoEmbarqueDetalleInfo costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from costinfo in dt.AsEnumerable()
                             select new CostoEmbarqueDetalleInfo
                             {
                                 CostoEmbarqueDetalleID = costinfo.Field<int>("CostoEmbarqueDetalleID"),
                                 Costo = new CostoInfo
                                     {
                                         CostoID = costinfo.Field<int>("CostoID"),
                                         Descripcion = costinfo.Field<string>("Costo"),
                                         ClaveContable = costinfo.Field<string>("ClaveContable"),
                                         Retencion = new RetencionInfo
                                             {
                                                 RetencionID =  costinfo.Field<int?>("RetencionID") != null ? costinfo.Field<int>("RetencionID") : 0,
                                                 Descripcion = costinfo.Field<string>("Retencion") ?? string.Empty
                                             }
                                     },
                                 Orden = costinfo.Field<int>("Orden"),
                                 Importe = costinfo.Field<decimal>("Importe"),
                                 Activo = costinfo.Field<bool>("Activo").BoolAEnum()
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costoInfo;
        }

        /// <summary>
        ///     Metodo que obtiene una lista Costo Embarque Detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<CostoEmbarqueDetalleInfo> ObtenerPorEmbarqueDetalleID(DataSet ds)
        {
            List<CostoEmbarqueDetalleInfo> costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from costinfo in dt.AsEnumerable()
                             select new CostoEmbarqueDetalleInfo
                             {
                                 CostoEmbarqueDetalleID = costinfo.Field<int>("CostoEmbarqueDetalleID"),
                                 Costo = new CostoInfo
                                 {
                                     CostoID = costinfo.Field<int>("CostoID"),
                                     Descripcion = costinfo.Field<string>("Costo"),
                                     ClaveContable = costinfo.Field<string>("ClaveContable"),
                                     Retencion = new RetencionInfo
                                     {
                                         RetencionID = costinfo.Field<int?>("RetencionID") != null ? costinfo.Field<int>("RetencionID") : 0,
                                         Descripcion = costinfo.Field<string>("Retencion") ?? string.Empty
                                     }
                                 },
                                 Orden = costinfo.Field<int>("Orden"),
                                 Importe = costinfo.Field<decimal>("Importe"),
                                 Activo = costinfo.Field<bool>("Activo").BoolAEnum()
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costoInfo;
        }

        /// <summary>
        ///     Metodo que obtiene una lista Entrada GanadoCosto Entrada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<EntradaGanadoCostoInfo> ObtenerPorEmbarqueIDOrganizacionOrigen(DataSet ds)
        {
            List<EntradaGanadoCostoInfo> costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from costinfo in dt.AsEnumerable()
                             select new EntradaGanadoCostoInfo
                             {
                                 Costo = new CostoInfo
                                 {
                                     CostoID = costinfo.Field<int>("CostoID"),
                                     Descripcion = costinfo.Field<string>("Costo"),
                                     ClaveContable = costinfo.Field<string>("ClaveContable"),
                                     Retencion = new RetencionInfo
                                     {
                                         RetencionID = costinfo.Field<int?>("RetencionID") != null ? costinfo.Field<int>("RetencionID") : 0,
                                         Descripcion = costinfo.Field<string>("Retencion") ?? string.Empty
                                     }
                                 },
                                 Proveedor = new ProveedorInfo
                                     {
                                         ProveedorID = costinfo.Field<int>("ProveedorID"),
                                         Descripcion = costinfo.Field<string>("Proveedor"),
                                         CodigoSAP = costinfo.Field<string>("CodigoSAP")
                                     },
                                 Importe = costinfo.Field<decimal>("Importe"),
                                 EditarNumeroDocumento = true
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costoInfo;
        }

        /// <summary>
        ///     Metodo que obtiene una lista Entrada GanadoCosto Entrada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<EntradaGanadoCostoInfo> ObtenerPorEmbarqueID(DataSet ds)
        {
            List<EntradaGanadoCostoInfo> costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from costinfo in dt.AsEnumerable()
                             select new EntradaGanadoCostoInfo
                             {
                                 Costo = new CostoInfo
                                 {
                                     CostoID = costinfo.Field<int>("CostoID"),
                                     Descripcion = costinfo.Field<string>("Costo"),
                                     ClaveContable = costinfo.Field<string>("ClaveContable"),
                                     Retencion = new RetencionInfo
                                     {
                                         RetencionID = costinfo.Field<int?>("RetencionID") != null ? costinfo.Field<int>("RetencionID") : 0,
                                         Descripcion = costinfo.Field<string>("Retencion") ?? string.Empty
                                     }
                                 },
                                 Proveedor = new ProveedorInfo
                                 {
                                     ProveedorID = costinfo.Field<int>("ProveedorID"),
                                     Descripcion = costinfo.Field<string>("Proveedor"),
                                     CodigoSAP = costinfo.Field<string>("CodigoSAP")
                                 },
                                 Importe = costinfo.Field<decimal>("Importe"),
                                 EditarNumeroDocumento = true
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costoInfo;
        }
        /// <summary>
        ///     Metodo que obtiene una lista Entrada ganado en base al Embarque
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaGanadoInfo> ObtenerEntradasPorEmbarqueID(DataSet ds)
        {
            List<EntradaGanadoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from entradaInfo in dt.AsEnumerable()
                                                  select new EntradaGanadoInfo
                                                  {
                                                      EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                                      FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                                      Costeado = entradaInfo.Field<bool>("Costeado"),
                                                      PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                                      PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                                      OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                                      TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID")
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
        /// Metodo que obtiene la lista de costos utilizados para un origen, destino y proveedor
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<CostoInfo> ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID(DataSet ds)
        {
            List<CostoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from costoInfo in dt.AsEnumerable()
                         select new CostoInfo {
                             FechaCosto = costoInfo.Field<DateTime>("FechaCreacion"),
                             ImporteCosto = costoInfo.Field<decimal>("Importe")
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
        /// Metodo que obtiene la lista de embarqueDetalles para un embarque de tipo ruteo que coincida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EmbarqueDetalleInfo> ObtenerEmbarqueDetallesCostosPorProveedorIDOrigenIDDestinoIDCostoID(DataSet ds)
        {
            List<EmbarqueDetalleInfo> lista;
            try
            {
                Logger.Info();
                DataTable dtDetalles = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtCostos = ds.Tables[ConstantesDAL.DtDetallesCostos];

                lista = (from embarqueDetalleInfo in dtDetalles.AsEnumerable()
                        select new EmbarqueDetalleInfo  
                        {
                            EmbarqueID = embarqueDetalleInfo.Field<int>("EmbarqueID"),
                            EmbarqueDetalleID = embarqueDetalleInfo.Field<int>("EmbarqueDetalleID"),
                            Orden = embarqueDetalleInfo.Field<int>("Orden"),
                            OrganizacionOrigen = new OrganizacionInfo (){ OrganizacionID = embarqueDetalleInfo.Field<int>("OrganizacionOrigenID")},
                            OrganizacionDestino = new OrganizacionInfo (){ OrganizacionID = embarqueDetalleInfo.Field<int>("OrganizacionDestinoID")},
                            FechaLlegada = embarqueDetalleInfo.Field<DateTime>("FechaCreacion"),
                            ListaCostoEmbarqueDetalle = (from costoInfo in dtCostos.AsEnumerable()
                                                         where costoInfo.Field<int>("EmbarqueDetalleID") == embarqueDetalleInfo.Field<int>("EmbarqueDetalleID")
                                                        select new CostoEmbarqueDetalleInfo
                                                        {
                                                            Importe = costoInfo.Field<decimal>("Importe")
                                                        }).ToList()
                        }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
    }
}