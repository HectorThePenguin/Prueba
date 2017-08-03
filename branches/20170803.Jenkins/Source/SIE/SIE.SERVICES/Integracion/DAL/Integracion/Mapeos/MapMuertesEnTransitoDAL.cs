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
using SIE.Services.Facturas;
using System.Globalization;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapMuertesEnTransitoDAL
    {
        
        internal static ResultadoInfo<MuertesEnTransitoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<MuertesEnTransitoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MuertesEnTransitoInfo
                         {
                             EntradaGanadoID = info.Field<int>("EntradaGanadoID"),
                             FolioEntrada = info.Field<int>("FolioEntrada"),
                             Origen = info.Field<string>("Origen"),
                             TipoOrigenID = info.Field<int>("TipoOrganizacionID"),
                             TipoOrigen = info.Field<string>("TipoOrigen"),
                             CodigoProveedor = info.Field<string>("CodigoSAP"),
                             Proveedor = info.Field<string>("Proveedor"),
                             CorralID = info.Field<int>("CorralID"),
                             Corral = info.Field<string>("Corral"),
                             LoteID = info.Field<int>("LoteID"),
                             Lote = info.Field<string>("Lote"),
                             Cabezas = info.Field<int>("CabezasRecibidas"),
                             MuertesTransito = info.Field<int>("MuertesEnTransito"),
                             MuertesRegistradas = info.Field<int>("MuertesRegistradas"),
                             CabezasLote = info.Field<int>("Cabezas"),
                             EmbarqueID = info.Field<int>("EmbarqueID")
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<MuertesEnTransitoInfo>
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

        internal static MuertesEnTransitoInfo ObtenerPorFolioEntrada(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                MuertesEnTransitoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new MuertesEnTransitoInfo
                         {
                             EntradaGanadoID = info.Field<int>("EntradaGanadoID"),
                             FolioEntrada = info.Field<int>("FolioEntrada"),
                             Origen = info.Field<string>("Origen"),
                             TipoOrigenID = info.Field<int>("TipoOrganizacionID"),
                             TipoOrigen = info.Field<string>("TipoOrigen"),
                             CodigoProveedor = info.Field<string>("CodigoSAP"),
                             Proveedor = info.Field<string>("Proveedor"),
                             CorralID = info.Field<int>("CorralID"),
                             Corral = info.Field<string>("Corral"),
                             LoteID = info.Field<int>("LoteID"),
                             Lote = info.Field<string>("Lote"),
                             Cabezas = info.Field<int>("CabezasRecibidas"),
                             MuertesTransito = info.Field<int>("MuertesEnTransito"),
                             MuertesRegistradas = info.Field<int>("MuertesRegistradas"),
                             CabezasLote = info.Field<int>("Cabezas"),
                             EmbarqueID = info.Field<int>("EmbarqueID")
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<AnimalInfo> ObtenerAretesPorFolioEntrada(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<AnimalInfo> lista = new List<AnimalInfo>();

                lista =
                (from info in dt.AsEnumerable()
                    select
                        new AnimalInfo
                        {
                            Arete = info.Field<string>("Arete")
                        }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static FacturaInfo ObtenerDatosFacturaMuertesEnTransito(DataSet ds)
        {
            FacturaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new FacturaInfo
                         {
                             FechaFactura = info["Fecha"] == DBNull.Value ? "" : info.Field<DateTime>("Fecha").ToString("MM'/'dd'/'yyyy hh:mm:ss", CultureInfo.InvariantCulture),
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
                    decimal costoPorMuerte = (from info in dt.AsEnumerable()
                                              select info.Field<decimal>("CostoMuerte")).First();
                    

                    List<ItemsFactura> items = new List<ItemsFactura>();
                    items.Add(new ItemsFactura()
                    {
                        Description = string.Format("MUERTES EN TRANSITO {0} ANIMALES",
                                                        dt.Rows.Count.ToString("F0")),
                        QuantityInvoiced = dt.Rows.Count.ToString("F0"),
                        UnitSellingPrice = costoPorMuerte.ToString("F2"),
                        CantidadBultos = dt.Rows.Count.ToString("F0"),
                        PrecioNeto = (costoPorMuerte * dt.Rows.Count).ToString("F2"),
                        LineNumber = Convert.ToString(1)
                    });
                    lista.ItemsFactura = items;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static int ObtenerTotalFoliosValidos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                int resultado = int.Parse(dt.Rows[0][0].ToString());
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static ValidacionesFolioVentaMuerte ObtenerValidacionesFolioEntrada(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                ValidacionesFolioVentaMuerte lista = new ValidacionesFolioVentaMuerte();

                lista =
                (from info in dt.AsEnumerable()
                 select
                     new ValidacionesFolioVentaMuerte
                     {
                         RegistroCondicionMuerte = info.Field<int>("RegistroCondicionMuerte") == 1 ? true : false,
                         FolioConMuertesRegistradas = info.Field<int>("FolioConMuertesRegistradas") == 1 ? true : false,
                         EstatusLote = info.Field<int>("EstatusLote") == 1 ? true : false,
                         Cabezas = info.Field<int>("Cabezas"),
                         AretesInvalidos = info.Field<string>("AretesRegistrados"),
                         AretesActivos = info.Field<string>("AretesActivos"),
                     }).First();

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
