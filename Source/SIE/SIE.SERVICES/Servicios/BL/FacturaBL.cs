using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Facturas;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class FacturaBL
    {
        /// <summary>
        /// Genera el xml de la factura de venta de ganado
        /// </summary>
        /// <param name="folioTicket"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal void GenerarDatosFacturaVentaDeGanado(int folioTicket, int organizacionId)
        {
            try
            {
                Logger.Info();
                var salidaIndividualDAL = new SalidaIndividualDAL();
                var facturaInfo = salidaIndividualDAL.ObtenerDatosFacturaVentaDeGanado(folioTicket, organizacionId);

                facturaInfo = ObtenerDatosDeConfiguracion(facturaInfo, organizacionId);
                GenerarFactura(facturaInfo, organizacionId);
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los datos de la venta de ganado intensivo para generar la factura
        /// </summary>
        /// <param name="folioTicket"></param>
        /// <param name="organizacionId"></param>
        internal void GenerarDatosFacturaVentaDeGanadoIntensivo(int folioTicket, int organizacionId)
        {
            try
            {
                Logger.Info();
                var salidaIndividualDAL = new SalidaIndividualDAL();
                var facturaInfo = salidaIndividualDAL.ObtenerDatosFacturaVentaDeGanadoIntensivo(folioTicket, organizacionId);

                facturaInfo = ObtenerDatosDeConfiguracion(facturaInfo, organizacionId);
                GenerarFactura(facturaInfo, organizacionId);
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal void GenerarDatosFacturaMuertesEnTransito(long folioMuerte, int organizacionId)
        {
            try
            {
                Logger.Info();
                var muertesEnTransitoDAL = new MuertesEnTransitoDAL();
                var facturaInfo = muertesEnTransitoDAL.ObtenerDatosFacturaMuertesEnTransito(folioMuerte, organizacionId);

                facturaInfo = ObtenerDatosDeConfiguracion(facturaInfo, organizacionId);
                GenerarFactura(facturaInfo, organizacionId);
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera el xml de la factura de venta de materia prima
        /// </summary>
        /// <param name="salidaProducto"></param>
        internal void GenerarDatosFacturaVentaDeMateriaPrima(SalidaProductoInfo salidaProducto)
        {
            try
            {
                Logger.Info();
                var salidaIndividualDAL = new SalidaProductoDAL();
                var facturaInfo = salidaIndividualDAL.ObtenerDatosFacturaPorFolioSalida(salidaProducto);

                facturaInfo = ObtenerDatosDeConfiguracion(facturaInfo, salidaProducto.Organizacion.OrganizacionID);
                GenerarFactura(facturaInfo, salidaProducto.Organizacion.OrganizacionID);
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Genera el xml de la factura de sacrificio
        /// </summary>
        /// <param name="loteSacrificioInfo"></param>
        internal void GenerarDatosFacturaSacrificio(LoteSacrificioInfo loteSacrificioInfo)
        {
            try
            {
                Logger.Info();
                var loteSacrificioDAL = new LoteSacrificioDAL();
                var listaFactura = loteSacrificioDAL.ObtenerDatosFacturaPorOrdenSacrificio(loteSacrificioInfo);
                List<FacturaInfo> facturas = new List<FacturaInfo>();
                FacturaInfo facturaFinal = new FacturaInfo();

                if (listaFactura != null)
                {
                    foreach (var facturaInfo in listaFactura)
                    {
                        facturas.Add(facturaInfo);
                    }
                    facturaFinal.Serie = listaFactura.FirstOrDefault().Serie;
                    facturaFinal.TrxNumber = listaFactura.FirstOrDefault().TrxNumber;
                    facturaFinal.FechaFactura = listaFactura.FirstOrDefault().FechaFactura;
                    facturaFinal.DatosCliente = listaFactura.FirstOrDefault().DatosCliente;
                    facturaFinal.DatosDeEnvio = listaFactura.FirstOrDefault().DatosDeEnvio;
                    facturaFinal = ObtenerDatosDeConfiguracionSinSerieFolio(facturaFinal,
                            loteSacrificioInfo.OrganizacionId);

                    facturaFinal.ItemsFactura = AgrupaFacturaTotales(facturas);
                    GenerarFactura(facturaFinal, loteSacrificioInfo.OrganizacionId);

                }
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera el xml de la factura de sacrificio
        /// </summary>
        /// <param name="loteSacrificioInfo"></param>
        internal List<ItemsFactura> AgrupaFacturaTotales(List<FacturaInfo> facturasInfo)
        {
            FacturaInfo facturaFinal = new FacturaInfo();
            //bool guardaCabecero = true;
            List<ItemsFactura> items = new List<ItemsFactura>();
            foreach (var facturaInfo in facturasInfo)
            {
                foreach (ItemsFactura item in facturaInfo.ItemsFactura)
                {
                    items.Add(item);
                }
            }
            String tipoGanado = String.Empty;
            int linea = 0;
            facturaFinal.ItemsFactura = new List<ItemsFactura>();
            items.OrderBy(x => x.Description).ThenBy(c => c.LineNumber).ToList().ForEach(a =>
            {
                if (tipoGanado == a.Description)
                {
                    if (Convert.ToInt16(a.LineNumber) == linea)
                    {
                        facturaFinal = SumaImportesTipoGanado(facturaFinal, a);
                    }
                    else
                    {
                        facturaFinal.ItemsFactura.Add(a);
                        tipoGanado = a.Description;
                        linea = Convert.ToInt16(a.LineNumber);
                    }
                }
                else
                {
                    facturaFinal.ItemsFactura.Add(a);
                    tipoGanado = a.Description;
                    linea = Convert.ToInt16(a.LineNumber);
                }
            });
            decimal unit;
            facturaFinal.ItemsFactura.ForEach(f =>
            {
                unit = Convert.ToDecimal(f.PrecioNeto) / Convert.ToDecimal(f.QuantityInvoiced);
                f.UnitSellingPrice = Convert.ToString(Math.Round(unit,4));
            });

            return facturaFinal.ItemsFactura;
        }

        internal FacturaInfo SumaImportesTipoGanado(FacturaInfo FacturaFinal, ItemsFactura item)
        {
            FacturaFinal.ItemsFactura.ForEach(x =>
            {
                if (x.LineNumber == item.LineNumber && x.Description == item.Description)
                {
                    x.QuantityInvoiced = Convert.ToString(Convert.ToDecimal(x.QuantityInvoiced) + Convert.ToDecimal(item.QuantityInvoiced));
                    x.PrecioNeto = Convert.ToString(Convert.ToDecimal(x.PrecioNeto) + Convert.ToDecimal(item.PrecioNeto));
                    x.CantidadBultos = Convert.ToString(Convert.ToDecimal(x.CantidadBultos) + Convert.ToDecimal(item.CantidadBultos));
                }
            });
            return FacturaFinal;
        }


        /// <summary>
        /// Obtiene los parametros de la factura para generarla
        /// </summary>
        /// <param name="facturaInfo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal FacturaInfo ObtenerDatosDeConfiguracion(FacturaInfo facturaInfo, int organizacionId)
        {
            try
            {
                Logger.Info();

                if (facturaInfo != null)
                {
                    var parametrosBl = new ParametroOrganizacionBL();
                    var parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId, ParametrosEnum.SerieFactura.ToString());

                    if (parametroInfo != null)
                    {
                        facturaInfo.Serie = parametroInfo.Valor;

                        parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId, ParametrosEnum.FolioFactura.ToString());

                        if (parametroInfo != null)
                        {
                            facturaInfo.TrxNumber = parametroInfo.Valor;

                            parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                                ParametrosEnum.SELLER_ID.ToString());

                            if (parametroInfo != null)
                            {
                                facturaInfo.SellerId = parametroInfo.Valor;

                                parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                                    ParametrosEnum.SHIP_FROM.ToString());

                                if (parametroInfo != null)
                                {
                                    facturaInfo.ShipFrom = parametroInfo.Valor;
                                    facturaInfo.BuyerId = facturaInfo.DatosCliente.PartyId;
                                    facturaInfo.ShipTo = facturaInfo.DatosCliente.PartyId;
                                }
                                else
                                {
                                    throw new ExcepcionServicio(
                                        Properties.ResourceServices.FacturaElectronica_MensajeErrorParametros);
                                }
                            }
                            else
                            {
                                throw new ExcepcionServicio(
                                    Properties.ResourceServices.FacturaElectronica_MensajeErrorParametros);
                            }
                        }
                        else
                        {
                            throw new ExcepcionServicio(
                                Properties.ResourceServices.FacturaElectronica_MensajeErrorFolioFactura);
                        }
                    }
                    else
                    {
                        throw new ExcepcionServicio(Properties.ResourceServices.FacturaElectronica_MensajeErrorSerie);
                    }
                }
                else
                {
                    throw new ExcepcionServicio(Properties.ResourceServices.FacturaElectronica_MensajeErrorAlConsultarDatos);
                }

                return facturaInfo;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Genera la factura en la ruta especificada
        /// </summary>
        /// <param name="datosFactura"></param>
        /// <param name="organizacionId"></param>
        internal void GenerarFactura(FacturaInfo datosFactura, int organizacionId)
        {
            var doc = new XDocument(new XDeclaration("1.0", "ISO-8859-1", ""));
            try
            {
                var parametrosBl = new ParametroOrganizacionBL();
                var parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId, ParametrosEnum.RutaCFDI.ToString());

                if (parametroInfo != null)
                {
                    string rutaRepositorio = parametroInfo.Valor;

                    if (datosFactura != null && rutaRepositorio != "")
                    {
                        doc.Add(
                            new XElement("TRANSACTIONS", new XAttribute("EMITER", "VIZ-MPEIRO"),
                                new XElement("TRANSACTION",
                                    new XElement("SERIE", datosFactura.Serie),
                                    new XElement("TRX_NUMBER", datosFactura.TrxNumber),
                                    new XElement("CUST_TRX_TYPE_ID", datosFactura.TipoCliente),
                                    new XElement("TRX_DATE", datosFactura.FechaFactura),
                                    new XElement("BILL_TO_CUSTOMER",
                                        new XElement("PARTY",
                                            new XElement("CODIGO_CLIENTE", datosFactura.DatosDeEnvio.CodigoCliente),
                                            new XElement("PARTY_ID", datosFactura.DatosDeEnvio.PartyId),
                                            datosFactura.DatosDeEnvio.Nombre == null
                                                ? new XElement("PARTY_NAME")
                                                : new XElement("PARTY_NAME",
                                                    new XCData(datosFactura.DatosDeEnvio.Nombre)),
                                            new XElement("PARTY_TYPE", datosFactura.DatosDeEnvio.PartyType),
                                            datosFactura.DatosDeEnvio.CodigoFiscal == null
                                                ? new XElement("JGZZ_FISCAL_CODE")
                                                : new XElement("JGZZ_FISCAL_CODE",
                                                    new XCData(datosFactura.DatosDeEnvio.CodigoFiscal)),
                                            datosFactura.DatosDeEnvio.Direccion1 == null
                                                ? new XElement("ADDRESS1")
                                                : new XElement("ADDRESS1",
                                                    new XCData(datosFactura.DatosDeEnvio.Direccion1)),
                                            datosFactura.DatosDeEnvio.Direccion2 == null
                                                ? new XElement("ADDRESS2")
                                                : new XElement("ADDRESS2",
                                                    new XCData(datosFactura.DatosDeEnvio.Direccion2)),
                                            datosFactura.DatosDeEnvio.Direccion3 == null
                                                ? new XElement("ADDRESS3")
                                                : new XElement("ADDRESS3",
                                                    new XCData(datosFactura.DatosDeEnvio.Direccion3)),
                                            datosFactura.DatosDeEnvio.Direccion4 == null
                                                ? new XElement("ADDRESS4")
                                                : new XElement("ADDRESS4",
                                                    new XCData(datosFactura.DatosDeEnvio.Direccion4)),

                                            new XElement("POSTAL_CODE", datosFactura.DatosDeEnvio.CodigoPostal),
                                            datosFactura.DatosDeEnvio.Ciudad == null
                                                ? new XElement("CITY")
                                                : new XElement("CITY", new XCData(datosFactura.DatosDeEnvio.Ciudad)),
                                            datosFactura.DatosDeEnvio.Estado == null
                                                ? new XElement("STATE")
                                                : new XElement("STATE", new XCData(datosFactura.DatosDeEnvio.Estado)),
                                            datosFactura.DatosDeEnvio.Pais == null
                                                ? new XElement("COUNTRY")
                                                : new XElement("COUNTRY", new XCData(datosFactura.DatosDeEnvio.Pais)),
                                            new XElement("METODO_PAGO", datosFactura.DatosCliente.MetodoDePago),
                                            new XElement("COND_PAGO", datosFactura.DatosCliente.CondicionDePago),
                                            new XElement("DIAS_PAGO", datosFactura.DatosCliente.DiasDePago)
                                            )
                                        ),
                                    new XElement("TIPO_MONEDA", datosFactura.TipoMoneda),
                                    new XElement("TASA_DE_CAMBIO", datosFactura.TasaDeCambio),
                                    new XElement("SELLER_ID", datosFactura.SellerId),
                                    new XElement("BUYER_ID", datosFactura.BuyerId),
                                    new XElement("SHIP_FROM", datosFactura.ShipFrom),
                                    new XElement("SHIP_TO", datosFactura.ShipTo),
                                    new XElement("ACCOUNT_NUMBER", datosFactura.NumeroDeCuenta),
                                    new XElement("SHIP_TO_CUSTOMER",
                                        new XElement("PARTY",
                                            new XElement("CODIGO_CLIENTE", datosFactura.DatosDeEnvio.CodigoCliente),
                                            new XElement("PARTY_ID", datosFactura.DatosDeEnvio.PartyId),
                                            datosFactura.DatosDeEnvio.Nombre == null
                                                ? new XElement("PARTY_NAME", "")
                                                : new XElement("PARTY_NAME",
                                                    new XCData(datosFactura.DatosDeEnvio.Nombre)),
                                            new XElement("PARTY_TYPE", datosFactura.DatosDeEnvio.PartyType),
                                            datosFactura.DatosDeEnvio.CodigoFiscal == null
                                                ? new XElement("JGZZ_FISCAL_CODE")
                                                : new XElement("JGZZ_FISCAL_CODE",
                                                    new XCData(datosFactura.DatosDeEnvio.CodigoFiscal)),
                                            datosFactura.DatosDeEnvio.Direccion1 == null
                                                ? new XElement("ADDRESS1")
                                                : new XElement("ADDRESS1",
                                                    new XCData(datosFactura.DatosDeEnvio.Direccion1)),
                                            datosFactura.DatosDeEnvio.Direccion2 == null
                                                ? new XElement("ADDRESS2")
                                                : new XElement("ADDRESS2",
                                                    new XCData(datosFactura.DatosDeEnvio.Direccion2)),
                                            datosFactura.DatosDeEnvio.Direccion3 == null
                                                ? new XElement("ADDRESS3")
                                                : new XElement("ADDRESS3",
                                                    new XCData(datosFactura.DatosDeEnvio.Direccion3)),
                                            datosFactura.DatosDeEnvio.Direccion4 == null
                                                ? new XElement("ADDRESS4")
                                                : new XElement("ADDRESS4",
                                                    new XCData(datosFactura.DatosDeEnvio.Direccion4)),

                                            new XElement("POSTAL_CODE", datosFactura.DatosDeEnvio.CodigoPostal),
                                            datosFactura.DatosDeEnvio.Ciudad == null
                                                ? new XElement("CITY")
                                                : new XElement("CITY", new XCData(datosFactura.DatosDeEnvio.Ciudad)),
                                            datosFactura.DatosDeEnvio.Estado == null
                                                ? new XElement("STATE")
                                                : new XElement("STATE", new XCData(datosFactura.DatosDeEnvio.Estado)),
                                            datosFactura.DatosDeEnvio.Pais == null
                                                ? new XElement("COUNTRY")
                                                : new XElement("COUNTRY", new XCData(datosFactura.DatosDeEnvio.Pais))
                                            )
                                        ),
                                    new XElement("STATUS_FACTURA", datosFactura.EstatusFactura),
                                    new XElement("TRANSACTION_LINES",
                                        from transaction in datosFactura.ItemsFactura
                                        select new XElement("TRANSACTION_LINE",
                                            new XElement("LINE_NUMBER", transaction.LineNumber),
                                            new XElement("LINE_TYPE", transaction.LineType),
                                            new XElement("INVENTORY_ITEM_ID", transaction.InventoryItemId),
                                            new XElement("ITEM_EAN_NUMBER", transaction.ItemEanNumber),
                                            new XElement("SERIAL", transaction.Serial),
                                            new XElement("DESCRIPTION", new XCData(transaction.Description)),
                                            new XElement("QUANTITY_INVOICED", transaction.QuantityInvoiced),
                                            new XElement("QUANTITY_CREDITED", transaction.QuantityCredited),
                                            new XElement("UNIT_SELLING_PRICE", transaction.UnitSellingPrice),
                                            new XElement("UOM_CODE", transaction.UomCode),
                                            new XElement("TAX_RATE", transaction.TaxRate),
                                            new XElement("TAXABLE_AMOUNT", transaction.TaxableAmount),
                                            new XElement("PRECIO_NETO", transaction.PrecioNeto),
                                            new XElement("DESCUENTO", transaction.Descuento),
                                            new XElement("VAT_TAX_ID", transaction.VatTaxId),
                                            new XElement("CANTIDAD_BULTOS", transaction.CantidadBultos)
                                            )
                                        ),
                                    new XElement("ADDENDA", new XAttribute("TYPE", "NONE")),
                                    new XElement("COMPLEMENTO", new XAttribute("TYPE", "NONE")),
                                    new XElement("IMPRESION", new XAttribute("ORIGINAL", "1"),
                                        new XAttribute("PREFACTURA", "0"),
                                        new XAttribute("BN", "false"), new XAttribute("PRINTER", "2")),
                                    new XElement("PRINTER", new XAttribute("ROUTE", "Finanzas FE"))
                                    )
                                )
                            );
                    }
                    if (Directory.Exists(rutaRepositorio))
                    {
                        string nombreCompleto = String.Format("{0}\\{1}", rutaRepositorio, datosFactura.NombreFactura);
                        doc.Save(nombreCompleto);
                    }
                    else
                    {
                        throw new ExcepcionServicio(
                            Properties.ResourceServices.FacturaElectronica_MensajeErrorDirectorioNoExiste);
                    }
                }
                else
                {
                    throw new ExcepcionServicio(
                        Properties.ResourceServices.FacturaElectronica_MensajeErrorRutaXml);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros de la factura para generarla esto teniendo la factura o la serie desde los datos anteriores
        /// </summary>
        /// <param name="facturaInfo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal FacturaInfo ObtenerDatosDeConfiguracionSinSerieFolio(FacturaInfo facturaInfo, int organizacionId)
        {
            try
            {
                Logger.Info();

                if (facturaInfo != null)
                {
                    var parametrosBl = new ParametroOrganizacionBL();

                    var parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId, ParametrosEnum.SerieFactura.ToString());

                    if (parametroInfo != null)
                    {
                        parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                            ParametrosEnum.FolioFactura.ToString());

                        if (parametroInfo != null)
                        {
                            parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                                ParametrosEnum.SELLER_ID.ToString());

                            if (parametroInfo != null)
                            {
                                facturaInfo.SellerId = parametroInfo.Valor;

                                parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                                    ParametrosEnum.SHIP_FROM.ToString());

                                if (parametroInfo != null)
                                {
                                    facturaInfo.ShipFrom = parametroInfo.Valor;
                                    facturaInfo.BuyerId = facturaInfo.DatosCliente.PartyId;
                                    facturaInfo.ShipTo = facturaInfo.DatosCliente.PartyId;
                                }
                                else
                                {
                                    throw new ExcepcionServicio(
                                        Properties.ResourceServices.FacturaElectronica_MensajeErrorParametros);
                                }
                            }
                            else
                            {
                                throw new ExcepcionServicio(
                                    Properties.ResourceServices.FacturaElectronica_MensajeErrorParametros);
                            }
                        }
                        else
                        {
                            throw new ExcepcionServicio(
                                Properties.ResourceServices.FacturaElectronica_MensajeErrorFolioFactura);
                        }
                    }
                    else
                    {
                        throw new ExcepcionServicio(Properties.ResourceServices.FacturaElectronica_MensajeErrorSerie);
                    }
                }
                else
                {
                    throw new ExcepcionServicio(
                        Properties.ResourceServices.FacturaElectronica_MensajeErrorAlConsultarDatos);
                }

                return facturaInfo;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Valida que existan los parametros de la factura para generarla
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public void ValidarDatosDeConfiguracion(int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametrosBl = new ParametroOrganizacionBL();
                var parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                    ParametrosEnum.SerieFactura.ToString());

                if (parametroInfo != null)
                {
                    parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                        ParametrosEnum.FolioFactura.ToString());

                    if (parametroInfo != null)
                    {
                        parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                            ParametrosEnum.SELLER_ID.ToString());

                        if (parametroInfo != null)
                        {
                            parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                                ParametrosEnum.SHIP_FROM.ToString());

                            if (parametroInfo == null)
                            {
                                throw new ExcepcionServicio(
                                    Properties.ResourceServices.FacturaElectronica_MensajeErrorAlGenerarFactura);
                            }
                        }
                        else
                        {
                            throw new ExcepcionServicio(
                                Properties.ResourceServices.FacturaElectronica_MensajeErrorParametros);
                        }
                    }
                    else
                    {
                        throw new ExcepcionServicio(
                            Properties.ResourceServices.FacturaElectronica_MensajeErrorFolioFactura);
                    }
                }
                else
                {
                    throw new ExcepcionServicio(Properties.ResourceServices.FacturaElectronica_MensajeErrorSerie);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera el xml de la factura de sacrificio
        /// </summary>
        /// <param name="loteSacrificioInfo"></param>
        /// <param name="lotesSacrificioFolios"> </param>
        public void GenerarDatosFacturaSacrificioLucero(LoteSacrificioInfo loteSacrificioInfo
                                                        , List<PolizaSacrificioModel> lotesSacrificioFolios)
        {
            try
            {
                Logger.Info();
                var loteSacrificioDAL = new LoteSacrificioDAL();
                List<FacturaInfo> listaFactura =
                    loteSacrificioDAL.ObtenerDatosFacturaPorOrdenSacrificioLucero(loteSacrificioInfo,
                                                                                  lotesSacrificioFolios);
                if (listaFactura != null)
                {
                    foreach (var facturaInfo in listaFactura)
                    {
                        var factura = ObtenerDatosDeConfiguracionSinSerieFolio(facturaInfo,
                            loteSacrificioInfo.OrganizacionId);
                        GenerarFactura(factura, loteSacrificioInfo.OrganizacionId);
                    }
                }
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera la factura para las salidas de ganado en transito venta
        /// </summary>
        /// <param name="folio">el folio que se acava de registrar actualmente</param>
        /// <param name="organizacionId">Organizacion del usuario</param>
        /// <param name="activo">El SP valida que activo (Venta) sea igual a true</param>
        /// <returns></returns>
        internal object SalidaGanadoTransito_ObtenerDatosFactura(int folio, int organizacionId, bool activo)
        {
            FacturaInfo factura;
            try
            {
                Logger.Info();

                var loteSacrificioDal = new SalidaGanadoEnTransitoDAL();
                factura = loteSacrificioDal.SalidaGanadoTransito_ObtenerDatosFactura(folio, activo);
                if (factura != null)
                {
                    //Mandar crear la factura
                    var facturaSalidaVentaTransito = ObtenerDatosDeFacturacionSalidaVentaTransito(factura, organizacionId);
                    GenerarFactura(facturaSalidaVentaTransito, organizacionId);
                }
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return factura;
        }

        internal FacturaInfo ObtenerDatosDeFacturacionSalidaVentaTransito(FacturaInfo facturaInfo, int organizacionId)
        {
            try
            {
                Logger.Info();

                if (facturaInfo != null)
                {
                    var parametrosBl = new ParametroOrganizacionBL();

                    var ruta = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId, ParametrosEnum.RutaCFDI.ToString());

                    if (ruta != null)
                    {
                        var parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                            ParametrosEnum.SerieFactura.ToString());

                        if (parametroInfo != null)
                        {
                            parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                                ParametrosEnum.SELLER_ID.ToString());

                            if (parametroInfo != null)
                            {
                                facturaInfo.SellerId = parametroInfo.Valor;

                                parametroInfo = parametrosBl.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                                    ParametrosEnum.SHIP_FROM.ToString());

                                if (parametroInfo != null)
                                {
                                    facturaInfo.ShipFrom = parametroInfo.Valor;
                                    facturaInfo.BuyerId = facturaInfo.DatosCliente.PartyId;
                                    facturaInfo.ShipTo = facturaInfo.DatosCliente.PartyId;
                                }
                                else
                                {
                                    throw new ExcepcionServicio(
                                        Properties.ResourceServices.FacturaElectronica_MensajeErrorParametros);
                                }
                            }
                            else
                            {
                                throw new ExcepcionServicio(
                                    Properties.ResourceServices.FacturaElectronica_MensajeErrorParametros);
                            }
                        }
                        else
                        {
                            throw new ExcepcionServicio(
                                Properties.ResourceServices.FacturaElectronica_MensajeErrorFolioFactura);
                        }
                    }
                    else
                    {
                        throw new ExcepcionServicio(Properties.ResourceServices.FacturaElectronica_MensajeErrorSerie);
                    }
                }
                else
                {
                    throw new ExcepcionServicio(
                        Properties.ResourceServices.FacturaElectronica_MensajeErrorAlConsultarDatos);
                }

                return facturaInfo;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
