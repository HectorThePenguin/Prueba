using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapEntradaGanadoCosteoDAL
    {
        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static EntradaGanadoCosteoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                EntradaGanadoCosteoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select new EntradaGanadoCosteoInfo
                                {
                                    EntradaGanadoCosteoID =
                                        info.Field<int>("EntradaGanadoCosteoID"),
                                    Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                    EntradaGanadoID = info.Field<int>("EntradaGanadoID"),
                                    Observacion = info.Field<string>("Observacion"),
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
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static EntradaGanadoCosteoInfo ObtenerPorEntradaGanadoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dtEntradaGanado = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtCalidad = ds.Tables[ConstantesDAL.DtDetalleCalidadGanado];
                DataTable dtDetalleEntrada = ds.Tables[ConstantesDAL.DtDetalleTipoGanado];
                DataTable dtCostoEntrada = ds.Tables[ConstantesDAL.DtDetalleCostoEntrada];

                EntradaGanadoCosteoInfo result =
                    (from info in dtEntradaGanado.AsEnumerable()
                     select new EntradaGanadoCosteoInfo
                                {
                                    EntradaGanadoCosteoID = info.Field<int>("EntradaGanadoCosteoID"),
                                    Organizacion =
                                        new OrganizacionInfo
                                            {
                                                OrganizacionID = info.Field<int>("OrganizacionID"),
                                                Descripcion = info.Field<string>("Organizacion")
                                            },
                                    EntradaGanadoID = info.Field<int>("EntradaGanadoID"),
                                    Observacion = info.Field<string>("Observacion"),
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                }).FirstOrDefault();

                List<EntradaGanadoCalidadInfo> detalleCalidadGanado =
                    (from info in dtCalidad.AsEnumerable()
                     select new EntradaGanadoCalidadInfo
                                {
                                    EntradaGanadoCalidadID =
                                        info.Field<int?>("EntradaGanadoCalidadID") == null
                                            ? 0
                                            : info.Field<int>("EntradaGanadoCalidadID"),
                                    CalidadGanado =
                                        new CalidadGanadoInfo
                                            {
                                                CalidadGanadoID = info.Field<int>("CalidadGanadoID"),
                                                Descripcion = info.Field<string>("CalidadGanado"),
                                                Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                                           ? Sexo.Macho
                                                           : Sexo.Hembra,
                                                Calidad = info.Field<string>("Calidad")
                                            },
                                    Valor = info.Field<int?>("Valor") == null ? 0 : info.Field<int>("Valor"),
                                    EntradaGanadoID =
                                        info.Field<int?>("EntradaGanadoID") == null
                                            ? 0
                                            : info.Field<int>("EntradaGanadoID"),
                                    Activo =
                                        info.Field<bool?>("Activo") == null
                                            ? EstatusEnum.Inactivo
                                            : info.Field<bool>("Activo").BoolAEnum(),
                                }).ToList();


                List<EntradaDetalleInfo> detalleEntrada =
                    (from info in dtDetalleEntrada.AsEnumerable()
                     select new EntradaDetalleInfo
                                {
                                    EntradaDetalleID = info.Field<int>("EntradaDetalleID"),
                                    EntradaGanadoCosteoID = info.Field<int>("EntradaGanadoCosteoID"),
                                    TipoGanado =
                                        new TipoGanadoInfo  
                                            {
                                                TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                Descripcion = info.Field<string>("TipoGanado"),
                                            },
                                    Cabezas = info.Field<int>("Cabezas"),
                                    PesoOrigen = info.Field<decimal>("PesoOrigen"),
                                    PesoLlegada = info.Field<decimal>("PesoLlegada"),
                                    PrecioKilo = info.Field<decimal>("PrecioKilo"),
                                    Importe = info.Field<decimal>("Importe"),
                                    ImporteOrigen = info.Field<decimal>("ImporteOrigen"),
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                }).ToList(); 

                List<EntradaGanadoCostoInfo> detalleCostoEntrada =
                    (from info in dtCostoEntrada.AsEnumerable()
                     select new EntradaGanadoCostoInfo
                                {
                                    EntradaGanadoCostoID = info.Field<int>("EntradaGanadoCostoID"),
                                    EntradaGanadoCosteoID = info.Field<int>("EntradaGanadoCosteoID"),
                                    Costo =
                                        new CostoInfo
                                            {
                                                CostoID = info.Field<int>("CostoID"),
                                                Descripcion = info.Field<string>("Costo"),
                                                ClaveContable = info.Field<string>("ClaveContable"),
                                                Retencion = new RetencionInfo
                                                {
                                                    RetencionID = info.Field<int?>("RetencionID") != null ? info.Field<int>("RetencionID") : 0,
                                                    Descripcion = info.Field<string>("RetencionDescripcion") ?? string.Empty
                                                }
                                            },
                                    TieneCuenta = info.Field<bool>("TieneCuenta"),
                                    Proveedor =
                                        new ProveedorInfo
                                            {
                                                ProveedorID = info.Field<int?>("ProveedorID") != null ? info.Field<int>("ProveedorID") : 0,
                                                Descripcion = info.Field<string>("Proveedor") ?? string.Empty,
                                                CodigoSAP = info.Field<string>("CodigoSAP") ?? string.Empty
                                            },
                                    NumeroDocumento = info.Field<string>("NumeroDocumento"),
                                    Importe = info.Field<decimal>("Importe"),
                                    Iva = info.Field<bool>("Iva"),
                                    Retencion = info.Field<bool>("Retencion"),
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                    CuentaProvision = info.Field<string>("CuentaProvision") ?? string.Empty,
                                    DescripcionCuenta = info.Field<string>("CuentraProvisionDescripcion") ?? string.Empty,
                                }).ToList();
                if (result == null)
                {
                    result = new EntradaGanadoCosteoInfo();
                }
                result.ListaCalidadGanado = detalleCalidadGanado;
                result.ListaEntradaDetalle = detalleEntrada;
                result.ListaCostoEntrada = detalleCostoEntrada;
                
                return result;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static EntradaGanadoCosteoInfo ObtenerPorSalidaOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dtDetalleEntrada = ds.Tables[0];
                DataTable dtCostoEntrada = ds.Tables[1];

                List<EntradaDetalleInfo> detalleEntrada =
                    (from info in dtDetalleEntrada.AsEnumerable()
                     select new EntradaDetalleInfo
                     {
                         TipoGanado =
                             new TipoGanadoInfo
                             {
                                 TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                 Descripcion = info.Field<string>("TipoGanado"),
                             },
                         Cabezas = info.Field<int>("Cabezas"),
                         PesoOrigen = info.Field<decimal>("PesoOrigen"),
                         PrecioKilo = info.Field<decimal>("PrecioKilo"),
                         Importe = info.Field<decimal>("Importe")
                     }).ToList();

                List<EntradaGanadoCostoInfo> detalleCostoEntrada =
                    (from info in dtCostoEntrada.AsEnumerable()
                     select new EntradaGanadoCostoInfo
                     {
                         Costo =
                             new CostoInfo
                             {
                                 CostoID = info.Field<int>("CostoID"),
                                 Descripcion = info.Field<string>("Costo"),
                                 ClaveContable = info.Field<string>("ClaveContable"),
                                 Retencion = new RetencionInfo
                                 {
                                     RetencionID = info.Field<int?>("RetencionID") != null ? info.Field<int>("RetencionID") : 0,
                                     Descripcion = info.Field<string>("Retencion") ?? string.Empty
                                 }
                             },
                         Importe = info.Field<decimal>("Importe"),
                         TieneCuenta = true
                     }).ToList();

                var result = new EntradaGanadoCosteoInfo();
                result.ListaEntradaDetalle = detalleEntrada;
                result.ListaCostoEntrada = detalleCostoEntrada;

                return result;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista con los costos de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaGanadoCosteoInfo> ObtenerPorFechasConciliacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dtEntradaGanado = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtCalidad = ds.Tables[ConstantesDAL.DtDetalleCalidadGanado];
                DataTable dtDetalleEntrada = ds.Tables[ConstantesDAL.DtDetalleTipoGanado];
                DataTable dtCostoEntrada = ds.Tables[ConstantesDAL.DtDetalleCostoEntrada];

                List<EntradaGanadoCosteoInfo> result =
                    (from info in dtEntradaGanado.AsEnumerable()
                     select new EntradaGanadoCosteoInfo
                                {
                                    EntradaGanadoCosteoID = info.Field<int>("EntradaGanadoCosteoID"),
                                    Organizacion =
                                        new OrganizacionInfo
                                            {
                                                OrganizacionID = info.Field<int>("OrganizacionID"),
                                                Descripcion = info.Field<string>("Organizacion")
                                            },
                                    EntradaGanadoID = info.Field<int>("EntradaGanadoID"),
                                    Observacion = info.Field<string>("Observacion"),
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                }).ToList();
                List<EntradaGanadoCalidadInfo> detalleCalidadGanado =
                    (from info in dtCalidad.AsEnumerable()
                     select new EntradaGanadoCalidadInfo
                                {
                                    EntradaGanadoCalidadID =
                                        info.Field<int?>("EntradaGanadoCalidadID") == null
                                            ? 0
                                            : info.Field<int>("EntradaGanadoCalidadID"),
                                    CalidadGanado =
                                        new CalidadGanadoInfo
                                            {
                                                CalidadGanadoID = info.Field<int>("CalidadGanadoID"),
                                                Descripcion = info.Field<string>("CalidadGanado"),
                                                Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                                           ? Sexo.Macho
                                                           : Sexo.Hembra,
                                                Calidad = info.Field<string>("Calidad")
                                            },
                                    Valor = info.Field<int?>("Valor") == null ? 0 : info.Field<int>("Valor"),
                                    EntradaGanadoID =
                                        info.Field<int?>("EntradaGanadoID") == null
                                            ? 0
                                            : info.Field<int>("EntradaGanadoID"),
                                    Activo =
                                        info.Field<bool?>("Activo") == null
                                            ? EstatusEnum.Inactivo
                                            : info.Field<bool>("Activo").BoolAEnum(),
                                }).ToList();
                List<EntradaDetalleInfo> detalleEntrada =
                    (from info in dtDetalleEntrada.AsEnumerable()
                     select new EntradaDetalleInfo
                                {
                                    EntradaDetalleID = info.Field<int>("EntradaDetalleID"),
                                    EntradaGanadoCosteoID = info.Field<int>("EntradaGanadoCosteoID"),
                                    TipoGanado =
                                        new TipoGanadoInfo
                                            {
                                                TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                Descripcion = info.Field<string>("TipoGanado"),
                                            },
                                    Cabezas = info.Field<int>("Cabezas"),
                                    PesoOrigen = info.Field<decimal>("PesoOrigen"),
                                    PesoLlegada = info.Field<decimal>("PesoLlegada"),
                                    PrecioKilo = info.Field<decimal>("PrecioKilo"),
                                    Importe = info.Field<decimal>("Importe"),
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                }).ToList();
                List<EntradaGanadoCostoInfo> detalleCostoEntrada =
                    (from info in dtCostoEntrada.AsEnumerable()
                     select new EntradaGanadoCostoInfo
                                {
                                    EntradaGanadoCostoID = info.Field<int>("EntradaGanadoCostoID"),
                                    EntradaGanadoCosteoID = info.Field<int>("EntradaGanadoCosteoID"),
                                    Costo =
                                        new CostoInfo
                                            {
                                                CostoID = info.Field<int>("CostoID"),
                                                Descripcion = info.Field<string>("Costo"),
                                                ClaveContable = info.Field<string>("ClaveContable"),
                                                Retencion = new RetencionInfo
                                                                {
                                                                    RetencionID =
                                                                        info.Field<int?>("RetencionID") != null
                                                                            ? info.Field<int>("RetencionID")
                                                                            : 0,
                                                                    Descripcion =
                                                                        info.Field<string>("RetencionDescripcion") ??
                                                                        string.Empty
                                                                }
                                            },
                                    TieneCuenta = info.Field<bool>("TieneCuenta"),
                                    Proveedor =
                                        new ProveedorInfo
                                            {
                                                ProveedorID =
                                                    info.Field<int?>("ProveedorID") != null
                                                        ? info.Field<int>("ProveedorID")
                                                        : 0,
                                                Descripcion = info.Field<string>("Proveedor") ?? string.Empty,
                                                CodigoSAP = info.Field<string>("CodigoSAP") ?? string.Empty
                                            },
                                    NumeroDocumento = info.Field<string>("NumeroDocumento"),
                                    Importe = info.Field<decimal>("Importe"),
                                    Iva = info.Field<bool>("Iva"),
                                    Retencion = info.Field<bool>("Retencion"),
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                    CuentaProvision = info.Field<string>("CuentaProvision") ?? string.Empty,
                                    DescripcionCuenta =
                                        info.Field<string>("CuentraProvisionDescripcion") ?? string.Empty,
                                }).ToList();
                result.ForEach(datos =>
                                   {
                                       datos.ListaCalidadGanado =
                                           detalleCalidadGanado.Where(id => id.EntradaGanadoID == datos.EntradaGanadoID)
                                               .ToList();
                                       datos.ListaEntradaDetalle =
                                           detalleEntrada.Where(
                                               id => id.EntradaGanadoCosteoID == datos.EntradaGanadoCosteoID).ToList();
                                       datos.ListaCostoEntrada =
                                           detalleCostoEntrada.Where(
                                               id => id.EntradaGanadoCosteoID == datos.EntradaGanadoCosteoID).ToList();
                                   });
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
 