using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Facturas;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapSalidaProductoDAL
    {
        /// <summary>
        /// Obtiene los datos de por pagina de salidas de productos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<SalidaProductoInfo> ObtenerFoliosPorPaginaParaSalidaProducto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SalidaProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaProductoInfo
                         {
                             SalidaProductoId = info.Field<int>("SalidaProductoID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID")
                             },
                             OrganizacionDestino = new OrganizacionInfo
                             {
                                 OrganizacionID = info["OrganizacionIDDestino"] == DBNull.Value ? 0 : info.Field<int>("OrganizacionIDDestino")
                             },
                             TipoMovimiento = new TipoMovimientoInfo
                             {
                                 TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                 Descripcion = info.Field<string>("Descripcion")
                             },
                             FolioSalida = info.Field<int>("FolioSalida"),
                             Almacen = new AlmacenInfo
                             {
                                 AlmacenID = info["AlmacenID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenID")
                             },
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                             {
                                 AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID")
                             },
                             Cliente = new ClienteInfo
                             {
                                 ClienteID = info["ClienteID"] == DBNull.Value ? 0 : info.Field<int>("ClienteID")
                             },
                             CuentaSAP = new CuentaSAPInfo
                             {
                                 CuentaSAPID = info["CuentaSAPID"] == DBNull.Value ? 0 : info.Field<int>("CuentaSAPID")
                             },
                             Observaciones = info["Observaciones"] == DBNull.Value ? string.Empty : info.Field<string>("Observaciones"),
                             Precio = info["Precio"] == DBNull.Value ? 0 : info.Field<decimal>("Precio"),
                             Importe = info["Importe"] == DBNull.Value ? 0 : info.Field<decimal>("Importe"),
                             AlmacenMovimiento = new AlmacenMovimientoInfo
                             {
                                 AlmacenMovimientoID = info["AlmacenMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoID")
                             },
                             PesoTara = info.Field<int>("PesoTara"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             Piezas = info.Field<int>("Piezas"),
                             FechaSalida = info.Field<DateTime>("FechaSalida"),
                             Chofer = new ChoferInfo
                             {
                                 ChoferID = info["ChoferID"] == DBNull.Value ? 0 : info.Field<int>("ChoferID")
                             },
                             Camion = new CamionInfo
                             {
                                 CamionID = info["CamionID"] == DBNull.Value ? 0 : info.Field<int>("CamionID")
                             },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo
                         }).ToList();

                var resultado = new ResultadoInfo<SalidaProductoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene salida de producto por folio salida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static SalidaProductoInfo ObtenerFoliosPorFolioSalida(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SalidaProductoInfo productoSalida =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaProductoInfo
                         {
                             SalidaProductoId = info.Field<int>("SalidaProductoID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID")
                             },
                             OrganizacionDestino = new OrganizacionInfo
                             {
                                OrganizacionID = info["OrganizacionIDDestino"] == DBNull.Value ? 0 : info.Field<int>("OrganizacionIDDestino")
                             },
                             TipoMovimiento = new TipoMovimientoInfo
                             {
                                 TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                 Descripcion = info.Field<string>("Descripcion")
                             },
                             FolioSalida = info.Field<int>("FolioSalida"),
                             Almacen = new AlmacenInfo
                             {
                                 AlmacenID = info["AlmacenID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenID")
                             },
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                             {
                                 AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID")
                             },
                             Cliente = new ClienteInfo
                             {
                                 ClienteID = info["ClienteID"] == DBNull.Value ? 0 : info.Field<int>("ClienteID")
                             },
                             CuentaSAP = new CuentaSAPInfo
                             {
                                 CuentaSAPID = info["CuentaSAPID"] == DBNull.Value ? 0 : info.Field<int>("CuentaSAPID")
                             },
                             Observaciones = info["Observaciones"] == DBNull.Value ? string.Empty : info.Field<string>("Observaciones"),
                             Precio = info["Precio"] == DBNull.Value ? 0 : info.Field<decimal>("Precio"),
                             Importe = info["Importe"] == DBNull.Value ? 0 : info.Field<decimal>("Importe"),
                             AlmacenMovimiento = new AlmacenMovimientoInfo
                             {
                                 AlmacenMovimientoID = info["AlmacenMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoID")
                             },
                             PesoTara = info.Field<int>("PesoTara"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             Piezas = info.Field<int>("Piezas"),
                             FechaSalida = info.Field<DateTime>("FechaSalida"),
                             Chofer = new ChoferInfo
                             {
                                 ChoferID = info["ChoferID"] == DBNull.Value ? 0 : info.Field<int>("ChoferID")
                             },
                             Camion = new CamionInfo
                             {
                                CamionID = info["CamionID"] == DBNull.Value ? 0 : info.Field<int>("CamionID")
                             },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo
                         }).First();


                return productoSalida;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene los datos de la salida de producto por folio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static SalidaProductoInfo ObtenerFoliosPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SalidaProductoInfo productoSalida =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaProductoInfo
                         {
                             SalidaProductoId = info.Field<int>("SalidaProductoID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID")
                             },
                             OrganizacionDestino = new OrganizacionInfo
                             {
                                 OrganizacionID = info["OrganizacionIDDestino"] == DBNull.Value ? 0 : info.Field<int>("OrganizacionIDDestino")
                             },
                             TipoMovimiento = new TipoMovimientoInfo
                             {
                                 TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                 Descripcion = info.Field<string>("Descripcion")
                             },
                             FolioSalida = info.Field<int>("FolioSalida"),
                             Almacen = new AlmacenInfo
                             {
                                 AlmacenID = info["AlmacenID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenID")
                             },
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                             {
                                 AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID")
                             },
                             Cliente = new ClienteInfo
                             {
                                 ClienteID = info["ClienteID"] == DBNull.Value ? 0 : info.Field<int>("ClienteID")
                             },
                             CuentaSAP = new CuentaSAPInfo
                             {
                                 CuentaSAPID = info["CuentaSAPID"] == DBNull.Value ? 0 : info.Field<int>("CuentaSAPID")
                             },
                             Observaciones = info["Observaciones"] == DBNull.Value ? string.Empty : info.Field<string>("Observaciones"),
                             Precio = info["Precio"] == DBNull.Value ? 0 : info.Field<decimal>("Precio"),
                             Importe = info["Importe"] == DBNull.Value ? 0 : info.Field<decimal>("Importe"),
                             AlmacenMovimiento = new AlmacenMovimientoInfo
                             {
                                 AlmacenMovimientoID = info["AlmacenMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoID")
                             },
                             PesoTara = info.Field<int>("PesoTara"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             Piezas = info.Field<int>("Piezas"),
                             FechaSalida = info.Field<DateTime>("FechaSalida"),
                             Chofer = new ChoferInfo
                             {
                                 ChoferID = info["ChoferID"] == DBNull.Value ? 0 : info.Field<int>("ChoferID")
                             },
                             Camion = new CamionInfo
                             {
                                 CamionID = info["CamionID"] == DBNull.Value ? 0 : info.Field<int>("CamionID")
                             },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo
                         }).First();


                return productoSalida;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para buscar los folios activos con el peso tara capturado.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<SalidaProductoInfo> ObtenerTraspasoFoliosActivos(DataSet ds)
        {
            List<SalidaProductoInfo> salidas;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                salidas =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaProductoInfo
                         {
                             SalidaProductoId = info.Field<int>("SalidaProductoID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID")
                             },
                             OrganizacionDestino = new OrganizacionInfo
                             {
                                 OrganizacionID = info["OrganizacionIDDestino"] == DBNull.Value ? 0 : info.Field<int>("OrganizacionIDDestino"),
                                 Descripcion = info["DescripcionOrganizacionDestino"] == DBNull.Value ? string.Empty : info.Field<string>("DescripcionOrganizacionDestino")
                                 
                             },
                             TipoMovimiento = new TipoMovimientoInfo
                             {
                                 TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                 Descripcion = info.Field<string>("Descripcion")
                             },
                             FolioSalida = info.Field<int>("FolioSalida"),
                             Almacen = new AlmacenInfo
                             {
                                 AlmacenID = info["AlmacenID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenID")
                             },
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                             {
                                 AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID")
                             },
                             Cliente = new ClienteInfo
                             {
                                 ClienteID = info["ClienteID"] == DBNull.Value ? 0 : info.Field<int>("ClienteID")
                             },
                             CuentaSAP = new CuentaSAPInfo
                             {
                                 CuentaSAPID = info["CuentaSAPID"] == DBNull.Value ? 0 : info.Field<int>("CuentaSAPID")
                             },
                             Observaciones = info["Observaciones"] == DBNull.Value ? string.Empty : info.Field<string>("Observaciones"),
                             Precio = info["Precio"] == DBNull.Value ? 0 : info.Field<decimal>("Precio"),
                             Importe = info["Importe"] == DBNull.Value ? 0 : info.Field<decimal>("Importe"),
                             AlmacenMovimiento = new AlmacenMovimientoInfo
                             {
                                 AlmacenMovimientoID = info["AlmacenMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoID")
                             },
                             PesoTara = info.Field<int>("PesoTara"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             Piezas = info.Field<int>("Piezas"),
                             FechaSalida = info.Field<DateTime>("FechaSalida"),
                             Chofer = new ChoferInfo
                             {
                                 ChoferID = info["ChoferID"] == DBNull.Value ? 0 : info.Field<int>("ChoferID")
                             },
                             Camion = new CamionInfo
                             {
                                 CamionID = info["CamionID"] == DBNull.Value ? 0 : info.Field<int>("CamionID")
                             },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo
                         }).ToList();


                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return salidas;
        }

        /// <summary>
        ///  Obtiene los datos de la factura para generarla
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static FacturaInfo ObtenerDatosFacturaPorFolioSalida(DataSet ds)
        {
            FacturaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new FacturaInfo
                         {
                             FechaFactura = info["FechaSalida"] == DBNull.Value ? "" : info.Field<DateTime>("FechaSalida").ToString("MM'/'dd'/'yyyy hh:mm:ss", CultureInfo.InvariantCulture),
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
                                    new ItemsFactura
                                    {
                                        Description = info["DescripcionProducto"] == DBNull.Value ? "" : info.Field<string>("DescripcionProducto"),
                                        QuantityInvoiced = info["CantidadKilos"] == DBNull.Value ? "" : info.Field<int>("CantidadKilos").ToString(CultureInfo.InvariantCulture),
                                        UnitSellingPrice = info["Precio"] == DBNull.Value ? "" : info.Field<decimal>("Precio").ToString(CultureInfo.InvariantCulture),
                                        PrecioNeto = info["Importe"] == DBNull.Value ? "" : info.Field<decimal>("Importe").ToString(CultureInfo.InvariantCulture),
                                        CantidadBultos = info["Piezas"] == DBNull.Value ? "" : info.Field<int>("Piezas").ToString(CultureInfo.InvariantCulture)
                                    }
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
        /// Obtiene un objeto de resultado con los datos
        /// paginados de salida producto
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static ResultadoInfo<SalidaProductoInfo> ObtenerFoliosPorPaginaReimpresion(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var lista = new List<SalidaProductoInfo>();
                SalidaProductoInfo elemento;
                while (reader.Read())
                {
                    elemento = new SalidaProductoInfo
                                   {
                                       SalidaProductoId = Convert.ToInt32(reader["SalidaProductoID"]),
                                       Organizacion = new OrganizacionInfo
                                                          {
                                                              OrganizacionID = Convert.ToInt32(reader["OrganizacionID"])
                                                          },
                                       OrganizacionDestino = new OrganizacionInfo
                                                                 {
                                                                     OrganizacionID =
                                                                         reader["OrganizacionIDDestino"] == DBNull.Value
                                                                             ? 0
                                                                             : Convert.ToInt32(
                                                                                 reader["OrganizacionIDDestino"])
                                                                 },
                                       TipoMovimiento = new TipoMovimientoInfo
                                                            {
                                                                TipoMovimientoID =
                                                                    Convert.ToInt32(reader["TipoMovimientoID"]),
                                                                Descripcion = Convert.ToString(reader["Descripcion"])
                                                            },
                                       Descripcion = Convert.ToString(reader["Descripcion"]),
                                       FolioSalida = Convert.ToInt32(reader["FolioSalida"]),
                                       Almacen = new AlmacenInfo
                                                     {
                                                         AlmacenID =
                                                             reader["AlmacenID"] == DBNull.Value
                                                                 ? 0
                                                                 : Convert.ToInt32(reader["AlmacenID"]),
                                                         Organizacion = new OrganizacionInfo
                                                                            {
                                                                                OrganizacionID =
                                                                                    Convert.ToInt32(
                                                                                        reader["OrganizacionID"])
                                                                            }
                                                     },
                                       AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                                                                   {
                                                                       AlmacenInventarioLoteId =
                                                                           reader["AlmacenInventarioLoteID"] ==
                                                                           DBNull.Value
                                                                               ? 0
                                                                               : Convert.ToInt32(
                                                                                   reader["AlmacenInventarioLoteID"]),
                                                                       Lote = Convert.ToInt32(reader["Lote"])
                                                                   },
                                       Cliente = new ClienteInfo
                                                     {
                                                         ClienteID =
                                                             reader["ClienteID"] == DBNull.Value
                                                                 ? 0
                                                                 : Convert.ToInt32(reader["ClienteID"]),
                                                         CodigoSAP = Convert.ToString(reader["CodigoSAP"]),
                                                         Descripcion = Convert.ToString(reader["Cliente"])
                                                     },
                                       CuentaSAP = new CuentaSAPInfo
                                                       {
                                                           CuentaSAPID =
                                                               reader["CuentaSAPID"] == DBNull.Value
                                                                   ? 0
                                                                   : Convert.ToInt32(reader["CuentaSAPID"]),
                                                           CuentaSAP = Convert.ToString(reader["CuentaSAP"])
                                                       },
                                       Observaciones =
                                           reader["Observaciones"] == DBNull.Value
                                               ? string.Empty
                                               : Convert.ToString(reader["Observaciones"]),
                                       Precio =
                                           reader["Precio"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Precio"]),
                                       Importe =
                                           reader["Importe"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Importe"]),
                                       AlmacenMovimiento = new AlmacenMovimientoInfo
                                                               {
                                                                   AlmacenMovimientoID =
                                                                       reader["AlmacenMovimientoID"] == DBNull.Value
                                                                           ? 0
                                                                           : Convert.ToInt64(
                                                                               reader["AlmacenMovimientoID"])
                                                               },
                                       PesoTara = Convert.ToInt32(reader["PesoTara"]),
                                       PesoBruto = Convert.ToInt32(reader["PesoBruto"]),
                                       Piezas = Convert.ToInt32(reader["Piezas"]),
                                       FechaSalida = Convert.ToDateTime(reader["FechaSalida"]),
                                       Chofer = new ChoferInfo
                                                    {
                                                        ChoferID =
                                                            reader["ChoferID"] == DBNull.Value
                                                                ? 0
                                                                : Convert.ToInt32(reader["ChoferID"])
                                                    },
                                       Camion = new CamionInfo
                                                    {
                                                        CamionID =
                                                            reader["CamionID"] == DBNull.Value
                                                                ? 0
                                                                : Convert.ToInt32(reader["CamionID"])
                                                    },
                                       Activo =
                                           Convert.ToBoolean(reader["Activo"])
                                               ? EstatusEnum.Activo
                                               : EstatusEnum.Inactivo,
                                       Producto = new ProductoInfo
                                                      {
                                                          ProductoId = Convert.ToInt32(reader["ProductoID"])
                                                      }
                                   };
                    lista.Add(elemento);
                }
                var totalRegistro = 0;
                reader.NextResult();
                while (reader.Read())
                {
                    totalRegistro = Convert.ToInt32(reader["TotalReg"]);
                }
                var resultado = new ResultadoInfo<SalidaProductoInfo>
                                {
                                    Lista = lista,
                                    TotalRegistros = totalRegistro
                                };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una salida por producto
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static SalidaProductoInfo ObtenerFoliosPorReimpresion(IDataReader reader)
        {
            try
            {
                Logger.Info();
                SalidaProductoInfo resultado = null;
                while (reader.Read())
                {
                    resultado = new SalidaProductoInfo
                                    {
                                        SalidaProductoId = Convert.ToInt32(reader["SalidaProductoID"]),
                                        Organizacion = new OrganizacionInfo
                                                           {
                                                               OrganizacionID =
                                                                   Convert.ToInt32(reader["OrganizacionID"])
                                                           },
                                        OrganizacionDestino = new OrganizacionInfo
                                                                  {
                                                                      OrganizacionID =
                                                                          reader["OrganizacionIDDestino"] ==
                                                                          DBNull.Value
                                                                              ? 0
                                                                              : Convert.ToInt32(
                                                                                  reader["OrganizacionIDDestino"])
                                                                  },
                                        TipoMovimiento = new TipoMovimientoInfo
                                                             {
                                                                 TipoMovimientoID =
                                                                     Convert.ToInt32(reader["TipoMovimientoID"]),
                                                                 Descripcion = Convert.ToString(reader["Descripcion"])
                                                             },
                                        Descripcion = Convert.ToString(reader["Descripcion"]),
                                        FolioSalida = Convert.ToInt32(reader["FolioSalida"]),
                                        Almacen = new AlmacenInfo
                                                      {
                                                          AlmacenID =
                                                              reader["AlmacenID"] == DBNull.Value
                                                                  ? 0
                                                                  : Convert.ToInt32(reader["AlmacenID"]),
                                                          Organizacion = new OrganizacionInfo
                                                                             {
                                                                                 OrganizacionID =
                                                                                     Convert.ToInt32(
                                                                                         reader["OrganizacionID"])
                                                                             }
                                                      },
                                        AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                                                                    {
                                                                        AlmacenInventarioLoteId =
                                                                            reader["AlmacenInventarioLoteID"] ==
                                                                            DBNull.Value
                                                                                ? 0
                                                                                : Convert.ToInt32(
                                                                                    reader["AlmacenInventarioLoteID"]),
                                                                        Lote = Convert.ToInt32(reader["Lote"])
                                                                    },
                                        Cliente = new ClienteInfo
                                                      {
                                                          ClienteID =
                                                              reader["ClienteID"] == DBNull.Value
                                                                  ? 0
                                                                  : Convert.ToInt32(reader["ClienteID"]),
                                                          CodigoSAP = Convert.ToString(reader["CodigoSAP"]),
                                                          Descripcion = Convert.ToString(reader["Cliente"])
                                                      },
                                        CuentaSAP = new CuentaSAPInfo
                                                        {
                                                            CuentaSAPID =
                                                                reader["CuentaSAPID"] == DBNull.Value
                                                                    ? 0
                                                                    : Convert.ToInt32(reader["CuentaSAPID"]),
                                                            CuentaSAP = Convert.ToString(reader["CuentaSAP"])
                                                        },
                                        Observaciones =
                                            reader["Observaciones"] == DBNull.Value
                                                ? string.Empty
                                                : Convert.ToString(reader["Observaciones"]),
                                        Precio =
                                            reader["Precio"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Precio"]),
                                        Importe =
                                            reader["Importe"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Importe"]),
                                        AlmacenMovimiento = new AlmacenMovimientoInfo
                                                                {
                                                                    AlmacenMovimientoID =
                                                                        reader["AlmacenMovimientoID"] == DBNull.Value
                                                                            ? 0
                                                                            : Convert.ToInt64(
                                                                                reader["AlmacenMovimientoID"])
                                                                },
                                        PesoTara = Convert.ToInt32(reader["PesoTara"]),
                                        PesoBruto = Convert.ToInt32(reader["PesoBruto"]),
                                        Piezas = Convert.ToInt32(reader["Piezas"]),
                                        FechaSalida = Convert.ToDateTime(reader["FechaSalida"]),
                                        Chofer = new ChoferInfo
                                                     {
                                                         ChoferID =
                                                             reader["ChoferID"] == DBNull.Value
                                                                 ? 0
                                                                 : Convert.ToInt32(reader["ChoferID"])
                                                     },
                                        Camion = new CamionInfo
                                                     {
                                                         CamionID =
                                                             reader["CamionID"] == DBNull.Value
                                                                 ? 0
                                                                 : Convert.ToInt32(reader["CamionID"])
                                                     },
                                        Activo =
                                            Convert.ToBoolean(reader["Activo"])
                                                ? EstatusEnum.Activo
                                                : EstatusEnum.Inactivo,
                                        Producto = new ProductoInfo
                                                       {
                                                           ProductoId = Convert.ToInt32(reader["ProductoID"])
                                                       }
                                    };
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una mapeo para la salida producto conciliacion
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<SalidaProductoInfo> ObtenerPolizasConciliacion()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto = MapeoBasico();
                MapeoCuentaSAP(mapSalidaProducto);
                MapeoAlmacenMovimiento(mapSalidaProducto);
                MapeoAlmacenInventarioLote(mapSalidaProducto);
                MapeoCliente(mapSalidaProducto);
                MapeoOrganizacion(mapSalidaProducto);
                MapeoTipoMovimiento(mapSalidaProducto);
                MapeoAlmacen(mapSalidaProducto);
                MapeoProducto(mapSalidaProducto);
                return mapSalidaProducto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoProducto(IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto)
        {
            try
            {
                Logger.Info();
                mapSalidaProducto.Map(x => x.Producto).WithFunc(x => new ProductoInfo
                {
                    ProductoId = Convert.ToInt32(x["ProductoID"])
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCuentaSAP(IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto)
        {
            try
            {
                Logger.Info();
                mapSalidaProducto.Map(x => x.CuentaSAP).WithFunc(x => new CuentaSAPInfo
                                                                          {
                                                                              CuentaSAP =
                                                                                  Convert.ToString(x["CuentaSAP"]),
                                                                              Descripcion =
                                                                                  Convert.ToString(x["Cuenta"]),
                                                                              CuentaSAPID =
                                                                                  Convert.ToInt32(x["CuentaSAPID"])
                                                                          });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoAlmacenMovimiento(IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto)
        {
            try
            {
                Logger.Info();
                mapSalidaProducto.Map(x => x.AlmacenMovimiento).WithFunc(x => new AlmacenMovimientoInfo
                                                                                  {
                                                                                      AlmacenMovimientoID =
                                                                                          Convert.ToInt64(
                                                                                              x["AlmacenMovimientoID"])
                                                                                  });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoAlmacenInventarioLote(IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto)
        {
            try
            {
                Logger.Info();
                mapSalidaProducto.Map(x => x.AlmacenInventarioLote).WithFunc(x => new AlmacenInventarioLoteInfo
                {
                    Lote = Convert.ToInt32(x["Lote"]),
                    AlmacenInventarioLoteId = Convert.ToInt32(x["AlmacenInventarioLoteID"])
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCliente(IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto)
        {
            try
            {
                Logger.Info();
                mapSalidaProducto.Map(x => x.Cliente).WithFunc(x => new ClienteInfo
                                                                        {
                                                                            ClienteID = Convert.ToInt32(x["ClienteID"]),
                                                                            CodigoSAP = Convert.ToString(x["Cliente"]),
                                                                        });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoOrganizacion(IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto)
        {
            try
            {
                Logger.Info();
                mapSalidaProducto.Map(x => x.Organizacion).WithFunc(x => new OrganizacionInfo
                {
                    OrganizacionID = 
                        Convert.ToInt32(
                            x["OrganizacionID"]),
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoTipoMovimiento(IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto)
        {
            try
            {
                Logger.Info();
                mapSalidaProducto.Map(x => x.TipoMovimiento).WithFunc(x => new TipoMovimientoInfo
                                                                               {
                                                                                   TipoMovimientoID =
                                                                                       Convert.ToInt32(
                                                                                           x["TipoMovimientoID"]),
                                                                               });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoAlmacen(IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto)
        {
            try
            {
                Logger.Info();
                mapSalidaProducto.Map(x => x.Almacen).WithFunc(x => new AlmacenInfo
                                                                        {
                                                                            AlmacenID = Convert.ToInt32(x["AlmacenID"]),
                                                                            Descripcion = Convert.ToString(x["Almacen"])
                                                                        });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static IMapBuilderContext<SalidaProductoInfo> MapeoBasico()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<SalidaProductoInfo> mapSalidaProducto =
                    MapBuilder<SalidaProductoInfo>.MapNoProperties();
                mapSalidaProducto.Map(x => x.SalidaProductoId).ToColumn("SalidaProductoID");
                mapSalidaProducto.Map(x => x.FolioSalida).ToColumn("FolioSalida");
                mapSalidaProducto.Map(x => x.Observaciones).ToColumn("Observaciones");
                mapSalidaProducto.Map(x => x.Precio).ToColumn("Precio");
                mapSalidaProducto.Map(x => x.Importe).ToColumn("Importe");
                mapSalidaProducto.Map(x => x.PesoBruto).ToColumn("PesoBruto");
                mapSalidaProducto.Map(x => x.PesoTara).ToColumn("PesoTara");
                mapSalidaProducto.Map(x => x.Piezas).ToColumn("Piezas");
                mapSalidaProducto.Map(x => x.FechaSalida).ToColumn("FechaSalida");
                return mapSalidaProducto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
