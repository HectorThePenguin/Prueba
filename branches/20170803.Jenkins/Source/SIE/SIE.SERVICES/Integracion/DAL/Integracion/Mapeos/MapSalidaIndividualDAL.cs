using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Facturas;
using SIE.Services.Info.Constantes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    class MapSalidaIndividualDAL
    {
        /// <summary>
        ///  Obtiene los datos de la factura para generarla
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static FacturaInfo ObtenerDatosFacturaSalidaIndividual(DataSet ds)
        {
            FacturaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new FacturaInfo
                         {
                             FechaFactura = info["FechaVenta"] == DBNull.Value ? "" : info.Field<DateTime>("FechaVenta").ToString("MM'/'dd'/'yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                             DatosCliente = new DatosCliente
                                {
                                    CodigoCliente = info["CodigoSap"] == DBNull.Value ? "" : info.Field<string>("CodigoSap"),
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
                                 CodigoCliente = info["CodigoSap"] == DBNull.Value ? "" : info.Field<string>("CodigoSap"),
                                 Nombre = info["NombreCliente"] == DBNull.Value ? "" : info.Field<string>("NombreCliente"),
                                 CodigoFiscal = info["RFC"] == DBNull.Value ? "" : info.Field<string>("RFC"),
                                 Direccion1 = info["Calle"] == DBNull.Value ? "" : info.Field<string>("Calle"),
                                 CodigoPostal = info["CodigoPostal"] == DBNull.Value ? "" : info.Field<string>("CodigoPostal"),
                                 Ciudad = info["Poblacion"] == DBNull.Value ? "" : info.Field<string>("Poblacion"),
                                 Estado = info["Estado"] == DBNull.Value ? "" : info.Field<string>("Estado"),
                                 Pais = info["Pais"] == DBNull.Value ? "" : info.Field<string>("Pais")
                             },
                         }).First();
                if (lista != null)
                {
                    List<ItemsFactura> items = (from info in dt.AsEnumerable()
                                                select new ItemsFactura
                                                           {
                                                               Description =
                                                                   info.Field<string>("DescripcionProducto"),
                                                               QuantityInvoiced =
                                                                  (info.Field<decimal>("PesoBruto") -
                                                                          info.Field<decimal>("PesoTara")).ToString(),
                                                               UnitSellingPrice =
                                                                   info.Field<decimal>("CausaPrecio").ToString("F2"),
                                                           }).ToList();
                    decimal pesoUnitario =
                            items.Select(peso => Convert.ToDecimal(peso.QuantityInvoiced)).FirstOrDefault() / items.Count;

                    decimal pesoTotal = items.Select(peso => Convert.ToDecimal(peso.QuantityInvoiced)).FirstOrDefault();
                    lista.ItemsFactura = items.GroupBy(tipo => new { tipo.Description, tipo.UnitSellingPrice })
                        .Select(item => new ItemsFactura
                                            {
                                                Description = item.Key.Description,
                                                QuantityInvoiced = (pesoUnitario * item.Count()).ToString("F0"),
                                                UnitSellingPrice = item.Key.UnitSellingPrice,
                                                CantidadBultos = Convert.ToString(item.Count())
                                            }).OrderBy(desc => desc.Description).ToList();
                    int numeroLinea = 0;
                    //int pesoElementos = 0;
                    decimal pesoElementos = lista.ItemsFactura.Sum(peso => Convert.ToDecimal(peso.QuantityInvoiced));
                    decimal diferencia = pesoTotal - pesoElementos;
                    bool existeDiferencia = diferencia != 0;
                    if (existeDiferencia)
                    {
                        lista.ItemsFactura[0].QuantityInvoiced =
                            (Convert.ToDecimal(lista.ItemsFactura[0].QuantityInvoiced) + diferencia).ToString();
                    }

                    lista.ItemsFactura.ForEach(precio =>
                    {
                        precio.PrecioNeto = (Convert.ToDecimal(precio.QuantityInvoiced) *
                                             Convert.ToDecimal(precio.UnitSellingPrice)).
                            ToString("F2");
                        //precio.QuantityInvoiced = (Convert.ToDecimal(precio.QuantityInvoiced)).ToString("F2");
                        precio.LineNumber = Convert.ToString(++numeroLinea);
                    });


                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static FacturaInfo ObtenerDatosFacturaSalidaIndividualIntensivo(DataSet ds)
        {
            FacturaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new FacturaInfo
                         {
                             FechaFactura = info["FechaVenta"] == DBNull.Value ? "" : info.Field<DateTime>("FechaVenta").ToString("MM'/'dd'/'yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                             DatosCliente = new DatosCliente
                             {
                                 CodigoCliente = info["CodigoSap"] == DBNull.Value ? "" : info.Field<string>("CodigoSap"),
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
                                 CodigoCliente = info["CodigoSap"] == DBNull.Value ? "" : info.Field<string>("CodigoSap"),
                                 Nombre = info["NombreCliente"] == DBNull.Value ? "" : info.Field<string>("NombreCliente"),
                                 CodigoFiscal = info["RFC"] == DBNull.Value ? "" : info.Field<string>("RFC"),
                                 Direccion1 = info["Calle"] == DBNull.Value ? "" : info.Field<string>("Calle"),
                                 CodigoPostal = info["CodigoPostal"] == DBNull.Value ? "" : info.Field<string>("CodigoPostal"),
                                 Ciudad = info["Poblacion"] == DBNull.Value ? "" : info.Field<string>("Poblacion"),
                                 Estado = info["Estado"] == DBNull.Value ? "" : info.Field<string>("Estado"),
                                 Pais = info["Pais"] == DBNull.Value ? "" : info.Field<string>("Pais")
                             },
                         }).First();
                if (lista != null)
                {
                    List<ItemsFactura> items = (from info in dt.AsEnumerable()
                                                select new ItemsFactura
                                                {
                                                    Description =
                                                        info.Field<string>("DescripcionProducto"),
                                                    QuantityInvoiced =
                                                       (info.Field<decimal>("PesoBruto") -
                                                               info.Field<decimal>("PesoTara")).ToString(),
                                                    UnitSellingPrice =
                                                        info.Field<decimal>("CausaPrecio").ToString("F2"),
                                                     CantidadBultos = info.Field<int>("Cabezas").ToString(CultureInfo.InvariantCulture),
                                                }).ToList();
                    decimal pesoUnitario =
                            items.Select(peso => Convert.ToDecimal(peso.QuantityInvoiced)).FirstOrDefault() / items.Count;

                    decimal pesoTotal = items.Select(peso => Convert.ToDecimal(peso.QuantityInvoiced)).FirstOrDefault();
                    lista.ItemsFactura = items.GroupBy(tipo => new { tipo.Description, tipo.UnitSellingPrice, tipo.CantidadBultos })
                        .Select(item => new ItemsFactura
                        {
                            Description = item.Key.Description,
                            QuantityInvoiced = (pesoUnitario * item.Count()).ToString("F0"),
                            UnitSellingPrice = item.Key.UnitSellingPrice,
                            CantidadBultos = item.Key.CantidadBultos,
                        }).OrderBy(desc => desc.Description).ToList();
                    int numeroLinea = 0;
                    //int pesoElementos = 0;
                    decimal pesoElementos = lista.ItemsFactura.Sum(peso => Convert.ToDecimal(peso.QuantityInvoiced));
                    decimal diferencia = pesoTotal - pesoElementos;
                    bool existeDiferencia = diferencia != 0;
                    if (existeDiferencia)
                    {
                        lista.ItemsFactura[0].QuantityInvoiced =
                            (Convert.ToDecimal(lista.ItemsFactura[0].QuantityInvoiced) + diferencia).ToString();
                    }

                    lista.ItemsFactura.ForEach(precio =>
                    {
                        precio.PrecioNeto = (Convert.ToDecimal(precio.QuantityInvoiced) *
                                             Convert.ToDecimal(precio.UnitSellingPrice)).
                            ToString("F2");
                        //precio.QuantityInvoiced = (Convert.ToDecimal(precio.QuantityInvoiced)).ToString("F2");
                        precio.LineNumber = Convert.ToString(++numeroLinea);
                    });


                }
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
