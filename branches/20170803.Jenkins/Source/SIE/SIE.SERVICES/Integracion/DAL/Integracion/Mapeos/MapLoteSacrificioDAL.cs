using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Facturas;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapLoteSacrificioDAL
    {
        /// <summary>
        /// Mapea los datos del lote sacrificio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteSacrificioInfo ObtenerLoteSacrificio(DataSet ds)
        {
            LoteSacrificioInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteSacrificioInfo
                         {
                             OrdenSacrificioId = info.Field<int>("OrdenSacrificio"),
                             FolioOrdenSacrificio = info.Field<int>("FolioOrdenSacrificio"),
                             Corrales = info.Field<int>("Corrales"),
                             Cabezas = info.Field<int>("Cabezas"),
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }
        /// <summary>
        /// Mapea los datos del lote sacrificio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteSacrificioInfo ObtenerLoteSacrificioACancelar(DataSet ds)
        {
            LoteSacrificioInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteSacrificioInfo
                         {
                             OrdenSacrificioId = info.Field<int>("OrdenSacrificioID"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             Cliente = new ClienteInfo
                             {
                                 ClienteID = info.Field<int>("ClienteID"),
                                 Descripcion = info.Field<string>("NombreCliente"),
                                 CodigoSAP = info.Field<string>("CodigoSAP")
                             }
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }
        /// <summary>
        /// Mapea los datos de la lista de lotes sacrificio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<LoteSacrificioInfo> ObtenerFacturasPorOrdenSacrificioACancelar(DataSet ds)
        {
            List<LoteSacrificioInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteSacrificioInfo
                         {
                             FolioFactura = info.Field<string>("FolioFactura")
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
        ///  Obtiene los datos de la factura para generarla
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<FacturaInfo> ObtenerDatosFacturaPorOrdenSacrificio(DataSet ds)
        {
            List<FacturaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new FacturaInfo
                         {
                             Serie = info["Serie"] == DBNull.Value ? "" : info.Field<string>("Serie"),
                             TrxNumber = info["Folio"] == DBNull.Value ? "" : info.Field<string>("Folio"),
                             FechaFactura = info["Fecha"] == DBNull.Value ? "" : info.Field<DateTime>("Fecha").ToString("MM'/'dd'/'yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                             DatosCliente = new DatosCliente
                             {
                                 CodigoCliente = info["CodigoSAP"] == DBNull.Value ? "" : info.Field<string>("CodigoSAP"),
                                 Nombre = info["NombreCliente"] == DBNull.Value ? "" : info.Field<string>("NombreCliente"),
                                 CodigoFiscal = info["RFC"] == DBNull.Value ? "" : info.Field<string>("RFC"),
                                 Direccion1 = info["Calle"] == DBNull.Value ? "" : info.Field<string>("Calle"),
                                 CodigoPostal = info["CodigoPostal"] == DBNull.Value ? "" : info.Field<string>("CodigoPostal"),
                                 Ciudad = info["Poblacion"] == DBNull.Value ? "" : info.Field<string>("Poblacion"),
                                 Estado = info["Estado"] == DBNull.Value ? "" : info.Field<string>("Estado"),
                                 Pais = info["Pais"] == DBNull.Value ? "" : info.Field<string>("Pais"),
                                 MetodoDePago = info["MetodoPago"] == DBNull.Value ? "" : info.Field<int>("MetodoPago").ToString(CultureInfo.InvariantCulture),
                                 CondicionDePago = info["CondicionPago"] == DBNull.Value ? "" : info.Field<int>("CondicionPago").ToString(CultureInfo.InvariantCulture),
                                 DiasDePago = info["DiasPago"] == DBNull.Value ? "" : info.Field<int>("DiasPago").ToString(CultureInfo.InvariantCulture)
                             },
                             DatosDeEnvio = new DatosDeEnvio
                             {
                                 CodigoCliente = info["CodigoSAP"] == DBNull.Value ? "" : info.Field<string>("CodigoSAP"),
                                 Nombre = info["NombreCliente"] == DBNull.Value ? "" : info.Field<string>("NombreCliente"),
                                 CodigoFiscal = info["RFC"] == DBNull.Value ? "" : info.Field<string>("RFC"),
                                 Direccion1 = info["Calle"] == DBNull.Value ? "" : info.Field<string>("Calle"),
                                 CodigoPostal = info["CodigoPostal"] == DBNull.Value ? "" : info.Field<string>("CodigoPostal"),
                                 Ciudad = info["Poblacion"] == DBNull.Value ? "" : info.Field<string>("Poblacion"),
                                 Estado = info["Estado"] == DBNull.Value ? "" : info.Field<string>("Estado"),
                                 Pais = info["Pais"] == DBNull.Value ? "" : info.Field<string>("Pais")
                             },
                             ItemsFactura = new List<ItemsFactura>
                                {
                                    // Canal
                                    new ItemsFactura
                                    {
                                        LineNumber = "1",
                                        Description = info.Field<decimal>("PesoNoqueo") > 1 ? 
                                                        "Canales " + info.Field<string>("TipoGanado") : 
                                                        "Canal " + info.Field<string>("TipoGanado"),

                                        QuantityInvoiced = info["PesoNoqueo"] == DBNull.Value ? "" : info.Field<decimal>("PesoNoqueo").ToString(CultureInfo.InvariantCulture),
                                        PrecioNeto = info["ImporteCanal"] == DBNull.Value ? "" : info.Field<decimal>("ImporteCanal").ToString(CultureInfo.InvariantCulture),
                                        UomCode = "Kg",
                                        UnitSellingPrice = info["PrecioNetoCanal"] == DBNull.Value ? "" : info.Field<decimal>("PrecioNetoCanal").ToString(CultureInfo.InvariantCulture),
                                        CantidadBultos = info["Cabezas"] == DBNull.Value ? "" : info.Field<int>("Cabezas").ToString(CultureInfo.InvariantCulture)
                                    },
                                    // Visceras
                                    new ItemsFactura
                                    {
                                        LineNumber = "2",
                                        Description = "Visceras " + info.Field<string>("TipoGanado"),
                                        QuantityInvoiced = info["Visceras"] == DBNull.Value ? "" : info.Field<decimal>("Visceras").ToString(CultureInfo.InvariantCulture),
                                        PrecioNeto = info["ImporteVisera"] == DBNull.Value ? "" : info.Field<decimal>("ImporteVisera").ToString(CultureInfo.InvariantCulture),
                                        UomCode = "Pza",
                                        UnitSellingPrice = info["PrecioNetoViscera"] == DBNull.Value ? "" : info.Field<decimal>("PrecioNetoViscera").ToString(CultureInfo.InvariantCulture),
                                        CantidadBultos = "0"//info["Cabezas"] == DBNull.Value ? "" : info.Field<int>("Cabezas").ToString(CultureInfo.InvariantCulture)
                                    },
                                    // Piel
                                    new ItemsFactura
                                    {
                                        LineNumber = "3",
                                        Description = info.Field<decimal>("PesoPiel") > 1 ? 
                                                        "Pieles " + info.Field<string>("TipoGanado") : 
                                                        "Piel " + info.Field<string>("TipoGanado"),
                                        
                                        QuantityInvoiced = info["PesoPiel"] == DBNull.Value ? "" : info.Field<decimal>("PesoPiel").ToString(CultureInfo.InvariantCulture),
                                        PrecioNeto = info["ImportePiel"] == DBNull.Value ? "" : info.Field<decimal>("ImportePiel").ToString(CultureInfo.InvariantCulture),
                                        UomCode = "Kg",
                                        UnitSellingPrice = info["PrecioNetoPiel"] == DBNull.Value ? "" : info.Field<decimal>("PrecioNetoPiel").ToString(CultureInfo.InvariantCulture),
                                        CantidadBultos = "0"//info["Cabezas"] == DBNull.Value ? "" : info.Field<int>("Cabezas").ToString(CultureInfo.InvariantCulture)
                                    }
                                }
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
        /// Obtiene una lista con los datos necesarios para la
        /// generacion de la poliza de sacrificio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PolizaSacrificioModel> ObtenerDatosPolizaSacrificio(DataSet ds)
        {
            List<PolizaSacrificioModel> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new PolizaSacrificioModel
                                    {
                                        LoteID = info.Field<int>("LoteID"),
                                        Fecha = info.Field<DateTime>("Fecha"),
                                        ImporteCanal = info.Field<decimal>("ImporteCanal"),
                                        ImportePiel = info.Field<decimal>("ImportePiel"),
                                        ImporteViscera = info.Field<decimal>("ImporteViscera"),
                                        Serie = info.Field<string>("Serie"),
                                        Folio = info.Field<string>("Folio"),
                                        Canales = info.Field<int>("Canales"),
                                        Peso = info.Field<int>("Peso"),
                                        Lote = info.Field<string>("Lote"),
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
        /// Obtiene una mapeo de lotes sacrificio
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<PolizaSacrificioModel> ObtenerMapeoPolizasSacrificioConciliacion()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<PolizaSacrificioModel> mapPolizaSacrificioConciliacion = MapeoBasico();
                return mapPolizaSacrificioConciliacion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una mapeo de lotes sacrificio
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<PolizaSacrificioModel> ObtenerMapeoPolizasSacrificioServicio()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<PolizaSacrificioModel> mapPolizaSacrificioConciliacion = MapeoServicio();
                return mapPolizaSacrificioConciliacion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static IMapBuilderContext<PolizaSacrificioModel> MapeoServicio()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<PolizaSacrificioModel> mapLotesSacrificio =
                    MapBuilder<PolizaSacrificioModel>.MapNoProperties();
                mapLotesSacrificio.Map(x => x.Canales).ToColumn("Canales");
                mapLotesSacrificio.Map(x => x.Fecha).ToColumn("Fecha");
                mapLotesSacrificio.Map(x => x.ImporteCanal).ToColumn("ImporteCanal");
                mapLotesSacrificio.Map(x => x.ImportePiel).ToColumn("ImportePiel");
                mapLotesSacrificio.Map(x => x.ImporteViscera).ToColumn("ImporteViscera");
                mapLotesSacrificio.Map(x => x.Lote).ToColumn("Lote");
                mapLotesSacrificio.Map(x => x.LoteID).ToColumn("LoteID");
                mapLotesSacrificio.Map(x => x.OrganizacionID).ToColumn("OrganizacionID");
                mapLotesSacrificio.Map(x => x.Peso).ToColumn("Peso");
                mapLotesSacrificio.Map(x => x.PesoPiel).ToColumn("PesoPiel");
                mapLotesSacrificio.Map(x => x.Corral).ToColumn("Corral");
                mapLotesSacrificio.Map(x => x.InterfaceSalidaTraspasoDetalleID).ToColumn(
                    "InterfaceSalidaTraspasoDetalleID");
                mapLotesSacrificio.Map(x => x.Serie).WithFunc(fn =>
                                                                  {
                                                                      if (Convert.ToBoolean(fn["PolizaGenerada"])
                                                                          && Convert.ToString(fn["SerieFolio"]).Length > 0)
                                                                      {
                                                                          return
                                                                              Convert.ToString(fn["SerieFolio"]).Split(
                                                                                  '-')[0];
                                                                      }
                                                                      else
                                                                      {
                                                                          return Convert.ToString(fn["Serie"]);
                                                                      }
                                                                  });
                mapLotesSacrificio.Map(x => x.Folio).WithFunc(fn =>
                {
                    if (Convert.ToBoolean(fn["PolizaGenerada"])
                        && Convert.ToString(fn["SerieFolio"]).Length > 0
                        && Convert.ToString(fn["SerieFolio"]).IndexOf('-') >= 0)
                    {
                        return
                            Convert.ToString(fn["SerieFolio"]).Split(
                                '-')[1];
                    }
                    else
                    {
                        return Convert.ToString(fn["Folio"]);
                    }
                });

                return mapLotesSacrificio;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static IMapBuilderContext<PolizaSacrificioModel> MapeoBasico()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<PolizaSacrificioModel> mapLotesSacrificio =
                    MapBuilder<PolizaSacrificioModel>.MapNoProperties();
                mapLotesSacrificio.Map(x => x.Canales).ToColumn("Canales");
                mapLotesSacrificio.Map(x => x.Fecha).ToColumn("Fecha");
                mapLotesSacrificio.Map(x => x.Folio).ToColumn("Folio");
                mapLotesSacrificio.Map(x => x.ImporteCanal).ToColumn("ImporteCanal");
                mapLotesSacrificio.Map(x => x.ImportePiel).ToColumn("ImportePiel");
                mapLotesSacrificio.Map(x => x.ImporteViscera).ToColumn("ImporteViscera");
                mapLotesSacrificio.Map(x => x.Lote).ToColumn("Lote");
                mapLotesSacrificio.Map(x => x.LoteID).ToColumn("LoteID");
                mapLotesSacrificio.Map(x => x.OrganizacionID).ToColumn("OrganizacionID");
                mapLotesSacrificio.Map(x => x.Serie).ToColumn("Serie");
                mapLotesSacrificio.Map(x => x.Peso).ToColumn("Peso");
                return mapLotesSacrificio;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<PolizaSacrificioModel> ObtenerDatosSacrificioLucero(DataSet ds)
        {
            List<PolizaSacrificioModel> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new PolizaSacrificioModel
                         {
                             //LoteID = info.Field<int>("LoteID"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             ImporteCanal = info.Field<decimal>("ImporteCanal"),
                             ImportePiel = info.Field<decimal>("ImportePiel"),
                             ImporteViscera = info.Field<decimal>("ImporteViscera"),
                             Serie = info.Field<string>("Serie"),
                             Folio = info.Field<string>("Folio")
                             //InterfaceSalidaTraspasoDetalleID = info.Field<int>("InterfaceSalidaTraspasoDetalleID")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        internal static List<PolizaSacrificioModel> ObtenerDatosPolizaSacrificioLucero(DataSet ds)
        {
            List<PolizaSacrificioModel> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new PolizaSacrificioModel
                         {
                             LoteID = info.Field<int>("LoteID"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             ImporteCanal = info.Field<decimal>("ImporteCanal"),
                             ImportePiel = info.Field<decimal>("ImportePiel"),
                             ImporteViscera = info.Field<decimal>("ImporteViscera"),
                             Serie = info.Field<string>("Serie"),
                             Folio = info.Field<string>("Folio"),
                             Canales = info.Field<int>("Canales"),
                             Peso = info.Field<int>("Peso"),
                             Lote = info.Field<string>("Lote"),
                             InterfaceSalidaTraspasoDetalleID =
                                 info.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                             PesoPiel = info.Field<int>("PesoPiel")
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
        /// Obtiene los datos de sacrificios previos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PolizaSacrificioModel> ObtenerDatosSacrificadosLucero(DataSet ds)
        {
            List<PolizaSacrificioModel> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new PolizaSacrificioModel
                         {
                             Serie = info.Field<string>("Serie"),
                             Folio = info.Field<string>("Folio"),
                             Canales = info.Field<int>("Canales"),
                             InterfaceSalidaTraspasoDetalleID =
                                 info.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             Corral = info.Field<string>("Corral"),
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
