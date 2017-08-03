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
    internal class MapSalidaGanadoTransito
    {
        /// <summary>
        /// metodo que mapea el resultado del SP SalidaGanadoTransito_ObtenerDatosFactura y lo retorna en un FacturaInfo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>FacturaInfo</returns>
        internal static FacturaInfo SalidaGanadoTransito_ObtenerDatosFactura(DataSet ds)
        {
            FacturaInfo factura;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                factura = (from info in dt.AsEnumerable()
                           select new FacturaInfo
                           {
                               Serie = info["Serie"] == DBNull.Value ? "" : info.Field<string>("Serie"),
                               TrxNumber = info["FolioFactura"] == DBNull.Value ? "" : info.Field<int>("FolioFactura").ToString(CultureInfo.InvariantCulture),
                               FechaFactura = info["FechaCreacion"] == DBNull.Value ? "" : info.Field<DateTime>("FechaCreacion").ToString("MM'/'dd'/'yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                               DatosCliente = new DatosCliente
                               {
                                   CodigoCliente = info["CodigoSAP"] == DBNull.Value ? "" : info.Field<string>("CodigoSAP"),
                                   Nombre = info["Descripcion"] == DBNull.Value ? "" : info.Field<string>("Descripcion"),
                                   CodigoFiscal = info["RFC"] == DBNull.Value ? "" : info.Field<string>("RFC"),
                                   Direccion1 = info["Calle"] == DBNull.Value ? "" : info.Field<string>("Calle"),
                                   CodigoPostal = info["CodigoPostal"] == DBNull.Value ? "" : info.Field<string>("CodigoPostal"),
                                   Ciudad = info["Poblacion"] == DBNull.Value ? "" : info.Field<string>("Poblacion"),
                                   Estado = info["Estado"] == DBNull.Value ? "" : info.Field<string>("Estado"),
                                   Pais = info["Pais"] == DBNull.Value ? "" : info.Field<string>("Pais"),
                                   MetodoDePago = info["MetodoPagoID"] == DBNull.Value ? "" : info.Field<int>("MetodoPagoID").ToString(CultureInfo.InvariantCulture),
                                   CondicionDePago = info["CondicionPago"] == DBNull.Value ? "" : info.Field<int>("CondicionPago").ToString(CultureInfo.InvariantCulture),
                                   DiasDePago = info["DiasPago"] == DBNull.Value ? "" : info.Field<int>("DiasPago").ToString(CultureInfo.InvariantCulture)
                               },
                               DatosDeEnvio = new DatosDeEnvio
                               {
                                   CodigoCliente = info["CodigoSAP"] == DBNull.Value ? "" : info.Field<string>("CodigoSAP"),
                                   Nombre = info["Descripcion"] == DBNull.Value ? "" : info.Field<string>("Descripcion"),
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
                                        Description = "CABEZAS(S)",

                                        QuantityInvoiced = info.Field<int>("NumCabezas").ToString(CultureInfo.InvariantCulture),
                                        PrecioNeto = (info.Field<decimal>("Importe") / info.Field<int>("NumCabezas")).ToString("0.00" ,CultureInfo.InvariantCulture),
                                        UomCode = "Kg",
                                        UnitSellingPrice = (info.Field<decimal>("Importe") / info.Field<int>("NumCabezas")).ToString("0.00" ,CultureInfo.InvariantCulture),
                                        CantidadBultos = info.Field<int>("NumCabezas").ToString(CultureInfo.InvariantCulture)
                                    }
                                }
                           }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return factura;
        }
    }
}
