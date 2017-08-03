using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapContratoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContratoInfo> ObtenerContratosPorEstado(DataSet ds)
        {
            List<ContratoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new ContratoInfo
                         {
                             ContratoId = info.Field<int>("ContratoID"),
                             Organizacion = new OrganizacionInfo() { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Folio = info.Field<int>("Folio"),
                             FolioCadena = info.Field<int>("Folio").ToString(),
                             Producto = new ProductoInfo() { ProductoId = info.Field<int>("ProductoId") },
                             TipoContrato = new TipoContratoInfo() { TipoContratoId = info.Field<int>("TipoContratoID") },
                             TipoFlete = new TipoFleteInfo() { TipoFleteId = info.Field<int>("TipoFleteID") },
                             Proveedor = new ProveedorInfo() { ProveedorID = info.Field<int>("ProveedorID") },
                             Precio = info.Field<Decimal>("Precio"),
                             TipoCambio = new TipoCambioInfo() { TipoCambioId = info["TipoCambioID"] == DBNull.Value ? 0 : info.Field<int>("TipoCambioID") },
                             Cantidad = info.Field<int>("Cantidad"),
                             Merma = info.Field<Decimal>("Merma"),
                             PesoNegociar = info.Field<string>("PesoNegociar"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             FechaVigencia = info.Field<DateTime>("FechaVigencia"),
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
        /// Obtiene un contrato por id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>ContratoInfo</returns>
        internal static ContratoInfo ObtenerPorId(DataSet ds)
        {
            ContratoInfo contrato;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                contrato = new ContratoInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    contrato.ContratoId = Convert.ToInt32(dr["ContratoID"]);
                    contrato.Organizacion = new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]) };
                    contrato.Folio = Convert.ToInt32(dr["Folio"]);
                    contrato.FolioCadena = Convert.ToInt32(dr["Folio"]).ToString();
                    contrato.Producto = new ProductoInfo() { ProductoId = Convert.ToInt32(dr["ProductoID"]) };
                    contrato.TipoContrato = new TipoContratoInfo() { TipoContratoId = Convert.ToInt32(dr["TipoContratoID"]) };
                    if (dr["TipoFleteID"] != DBNull.Value && dr["TipoFleteID"] != null)
                    {
                        contrato.TipoFlete = new TipoFleteInfo() { TipoFleteId = Convert.ToInt32(dr["TipoFleteID"]) };
                    }
                    contrato.Proveedor = new ProveedorInfo() { ProveedorID = Convert.ToInt32(dr["ProveedorID"]) };
                    contrato.Precio = Convert.ToDecimal(dr["Precio"]);
                    contrato.PrecioConvertido = dr["PrecioConvertido"] != DBNull.Value
                        ? Convert.ToDecimal(dr["PrecioConvertido"])
                        : 0;
                    if (dr["TipoCambioID"] != DBNull.Value && dr["TipoCambioID"] != null)
                    {
                        contrato.TipoCambio = new TipoCambioInfo() { TipoCambioId = Convert.ToInt32(dr["TipoCambioID"]) };
                    }
                    contrato.Cantidad = Convert.ToInt32(dr["Cantidad"]);
                    contrato.Merma = Convert.ToDecimal(dr["Merma"]);
                    contrato.PesoNegociar = dr["PesoNegociar"].ToString();
                    contrato.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    contrato.FechaVigencia = Convert.ToDateTime(dr["FechaVigencia"]);
                    contrato.Tolerancia = Convert.ToDecimal(dr["Tolerancia"]);
                    contrato.Parcial = Convert.ToBoolean(dr["Parcial"]).BoolCompraParcialAEnum();
                    if (dr["CuentaSAPID"] != DBNull.Value && dr["CuentaSAPID"] != null)
                    {
                        contrato.Cuenta = new CuentaSAPInfo() { CuentaSAPID = Convert.ToInt32(dr["CuentaSAPID"]) };
                    }
                    contrato.Estatus = new EstatusInfo() { EstatusId = Convert.ToInt32(dr["EstatusID"]) };
                    contrato.Activo = Convert.ToBoolean(dr["Activo"]).BoolAEnum();
                    contrato.UsuarioCreacionId = Convert.ToInt32(dr["UsuarioCreacionID"]);
                    contrato.CalidadOrigen = Convert.ToInt32(dr["CalidadOrigen"]);
                    contrato.Guardado = true;
                    contrato.AplicaDescuento = Convert.ToInt32(dr["AplicaDescuento"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return contrato;
        }

        /// <summary>
        /// Obtiene un contrato por id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>ContratoInfo</returns>
        internal static ContratoInfo ObtenerPorFolio(DataSet ds)
        {
            ContratoInfo contrato;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                contrato = new ContratoInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    contrato.ContratoId = Convert.ToInt32(dr["ContratoID"]);
                    contrato.Organizacion = new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]) };
                    contrato.Folio = Convert.ToInt32(dr["Folio"]);
                    contrato.FolioCadena = Convert.ToInt32(dr["Folio"]).ToString();
                    contrato.Producto = new ProductoInfo() { ProductoId = Convert.ToInt32(dr["ProductoID"]) };
                    contrato.TipoContrato = new TipoContratoInfo() { TipoContratoId = Convert.ToInt32(dr["TipoContratoID"]) };
                    contrato.TipoFlete = new TipoFleteInfo() { TipoFleteId = Convert.ToInt32(dr["TipoFleteID"]) };
                    contrato.Proveedor = new ProveedorInfo() { ProveedorID = Convert.ToInt32(dr["ProveedorID"]) };
                    contrato.Precio = Convert.ToDecimal(dr["Precio"]);
                    contrato.TipoCambio = new TipoCambioInfo() { TipoCambioId = dr["TipoCambioID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TipoCambioID"]) };
                    contrato.Cantidad = Convert.ToInt32(dr["Cantidad"]);
                    contrato.Merma = Convert.ToDecimal(dr["Merma"]);
                    contrato.PesoNegociar = dr["Merma"].ToString();
                    contrato.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    contrato.FechaVigencia = Convert.ToDateTime(dr["FechaVigencia"]);
                    contrato.Activo = Convert.ToBoolean(dr["Activo"]).BoolAEnum();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return contrato;
        }

        /// <summary>
        /// Obtiene una lista de contratos por proveedor
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContratoInfo> ObtenerContratosPorProveedorId(DataSet ds)
        {
            List<ContratoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new ContratoInfo
                         {
                             ContratoId = info.Field<int>("ContratoID"),
                             Organizacion = new OrganizacionInfo() { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Folio = info.Field<int>("Folio"),
                             FolioCadena = info.Field<int>("Folio").ToString(),
                             Producto = new ProductoInfo() { ProductoId = info.Field<int>("ProductoId") },
                             TipoContrato = new TipoContratoInfo() { TipoContratoId = info.Field<int>("TipoContratoID") },
                             TipoFlete = new TipoFleteInfo() { TipoFleteId = info.Field<int>("TipoFleteID") },
                             Proveedor = new ProveedorInfo() { ProveedorID = info.Field<int>("ProveedorID") },
                             Precio = info.Field<Decimal>("Precio"),
                             TipoCambio = new TipoCambioInfo() { TipoCambioId = info["TipoCambioID"] == DBNull.Value ? 0 : info.Field<int>("TipoCambioID") },
                             Cantidad = info.Field<int>("Cantidad"),
                             Merma = info.Field<Decimal>("Merma"),
                             PesoNegociar = info.Field<string>("PesoNegociar"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             FechaVigencia = info.Field<DateTime>("FechaVigencia"),
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
        /// Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ContratoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<ContratoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ContratoInfo> lista = (from info in dt.AsEnumerable()
                                            select new ContratoInfo
                                         {
                                             ContratoId = info.Field<int>("ContratoID"),
                                             Organizacion = new OrganizacionInfo() { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("OrganizacionDescripcion") },
                                             Producto = new ProductoInfo() { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("ProductoDescripcion"), Descripcion = info.Field<string>("ProductoDescripcion") },
                                             Cuenta = new CuentaSAPInfo(){CuentaSAPID = info["CuentaSAPID"] == DBNull.Value ? 0 : info.Field<int>("CuentaSAPID"), CuentaSAP = info["CuentaSAP"] == DBNull.Value ? "" : info.Field<string>("CuentaSAP"), 
                                                      Descripcion = info["CuentaDescripcion"] == DBNull.Value ? "" : info.Field<string>("CuentaDescripcion")},
                                             Folio = info.Field<int>("Folio"),
                                             FolioCadena = info.Field<int>("Folio").ToString(),
                                             TipoContrato = new TipoContratoInfo(){TipoContratoId = info.Field<int>("TipoContratoID"), Descripcion = info.Field<string>("TipoContratoDescripcion")},
                                             TipoFlete = new TipoFleteInfo(){TipoFleteId = info.Field<int>("TipoFleteID"), Descripcion = info.Field<string>("TipoFleteDescripcion")},
                                             Proveedor = new ProveedorInfo() { ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("ProveedorDescripcion"), CodigoSAP = info.Field<string>("CodigoSAP") },
                                             Precio = info.Field<decimal>("Precio"),
                                             TipoCambio = new TipoCambioInfo(){TipoCambioId = info.Field<int>("TipoCambioID"), Descripcion = info.Field<string>("TipoCambioDescripcion"), Cambio = info.Field<decimal>("Cambio")},
                                             Estatus = new EstatusInfo(){EstatusId = info.Field<int>("TipoCambioID"), Descripcion = info.Field<string>("EstatusDescripcion"), TipoEstatus = (TipoEstatus) info.Field<int>("TipoEstatus")},
                                             Cantidad = info.Field<int>("Cantidad"),
                                             Tolerancia = info.Field<decimal>("Tolerancia"),
                                             Parcial = info.Field<bool>("Parcial").BoolCompraParcialAEnum(),
                                             Merma = info.Field<decimal>("Merma"),
                                             PesoNegociar = info.Field<string>("PesoNegociar"),
                                             Fecha = info.Field<DateTime>("Fecha"),
                                             FechaVigencia = info.Field<DateTime>("FechaVigencia"),
                                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                                             Guardado = true,
                                             ProductoDescripcion = info.Field<string>("ProductoDescripcion"),
                                             CantidadSurtida = info.Field<int?>("CantidadSurtida") != null ? info.Field<int>("CantidadSurtida") : 0,
                                             CalidadOrigen = Convert.ToInt32(info["CalidadOrigen"]),
                                             FolioAserca = info.Field<string>("FolioAserca"),
                                             FolioCobertura = info.Field<int?>("FolioCobertura") != null ? info.Field<int>("FolioCobertura") : 0,
                                             CostoSecado = Convert.ToInt32(info["CostoSecado"]),
                                             AplicaDescuento = Convert.ToInt32(info["AplicaDescuento"])
                                         }).ToList();
                resultado = new ResultadoInfo<ContratoInfo>
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

        internal static ContratoInfo ObtenerPorContenedor(DataSet ds)
        {
            ContratoInfo contenedor;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                contenedor = (from info in dt.AsEnumerable()
                              select new ContratoInfo
                              {
                                  ContratoId = info.Field<int>("ContratoID"),
                                  Cantidad = info.Field<int>("Cantidad"),
                                  Merma = info.Field<decimal>("Merma"),
                                  Folio = info.Field<int>("Folio"),
                                  FolioCadena = info.Field<int>("Folio").ToString(),
                                  Fecha = info.Field<DateTime>("Fecha"),
                                  PesoNegociar = info.Field<string>("Proveedor"), //info.Field<string>("PesoNegociar"),
                                  Precio = info.Field<decimal>("Precio"),
                                  Organizacion = new OrganizacionInfo
                                  {
                                      OrganizacionID =
                                          info.Field<int>(
                                              "OrganizacionID")
                                  },
                                  Proveedor = new ProveedorInfo
                                  {
                                      ProveedorID =
                                          info.Field<int>("ProveedorID"),
                                      Descripcion =
                                          info.Field<string>("Proveedor"),
                                      CodigoSAP =
                                          info.Field<string>("CodigoSAP")
                                  },
                                  Producto = new ProductoInfo
                                  {
                                      ProductoId = info.Field<int>("ProductoID")
                                  }
                              }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return contenedor;
        }

        internal static ResultadoInfo<ContratoInfo> ObtenerPorContenedorPaginado(DataSet ds)
        {
            ResultadoInfo<ContratoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ContratoInfo> contratos = (from info in dt.AsEnumerable()
                                                select new ContratoInfo
                                                {
                                                    ContratoId = info.Field<int>("ContratoID"),
                                                    Cantidad = info.Field<int>("Cantidad"),
                                                    Merma = info.Field<decimal>("Merma"),
                                                    Folio = info.Field<int>("Folio"),
                                                    FolioCadena = info.Field<int>("Folio").ToString(),
                                                    Fecha = info.Field<DateTime>("Fecha"),
                                                    PesoNegociar = info.Field<string>("Proveedor"), //info.Field<string>("PesoNegociar"),
                                                    Precio = info.Field<decimal>("Precio"),
                                                    Organizacion = new OrganizacionInfo
                                                    {
                                                        OrganizacionID =
                                                            info.Field<int>(
                                                                "OrganizacionID")
                                                    },
                                                    Proveedor = new ProveedorInfo
                                                    {
                                                        ProveedorID =
                                                            info.Field<int>("ProveedorID"),
                                                        Descripcion =
                                                            info.Field<string>("Proveedor"),
                                                        CodigoSAP =
                                                            info.Field<string>("CodigoSAP")
                                                    },
                                                    Producto = new ProductoInfo
                                                    {
                                                        ProductoId = info.Field<int>("ProductoID")
                                                    },
                                                }).ToList();
                resultado = new ResultadoInfo<ContratoInfo>
                {
                    Lista = contratos,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtDetalle].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal static IMapBuilderContext<ContratoInfo> ObtenerPorXML()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<ContratoInfo> mapContrato = MapeoBasico();
                MapeoCuentaSAP(mapContrato);
                MapeoProveedor(mapContrato);
                MapeoOrganizacion(mapContrato);
                MapeoTipoCambio(mapContrato);
                MapeoTipoContrato(mapContrato);
                MapeoProducto(mapContrato);
                return mapContrato;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoProducto(IMapBuilderContext<ContratoInfo> mapContrato)
        {
            try
            {
                Logger.Info();
                mapContrato.Map(x => x.Producto).WithFunc(x => new ProductoInfo
                {
                    ProductoId = Convert.ToInt32(x["ProductoID"]),
                    Descripcion = Convert.ToString(x["Producto"]),
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoTipoContrato(IMapBuilderContext<ContratoInfo> mapContrato)
        {
            try
            {
                Logger.Info();
                mapContrato.Map(x => x.TipoContrato).WithFunc(x => new TipoContratoInfo
                {
                    TipoContratoId = Convert.ToInt32(x["TipoContratoID"]),
                    Descripcion = Convert.ToString(x["TipoContrato"]),
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCuentaSAP(IMapBuilderContext<ContratoInfo> mapeoAlmacenMoviiento)
        {
            try
            {
                Logger.Info();
                mapeoAlmacenMoviiento.Map(x => x.Cuenta).WithFunc(x => new CuentaSAPInfo
                {
                    CuentaSAPID =
                        Convert.ToInt32(x["CuentaSAPID"]),
                    CuentaSAP =
                        Convert.ToString(x["CuentaSAP"]),
                    Descripcion = Convert.ToString(x["DescripcionCuentaSAP"]),
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoTipoCambio(IMapBuilderContext<ContratoInfo> mapeoAlmacenMoviiento)
        {
            try
            {
                Logger.Info();
                mapeoAlmacenMoviiento.Map(x => x.TipoCambio).WithFunc(x => new TipoCambioInfo
                {
                    TipoCambioId =
                        Convert.ToInt32(x["TipoCambioID"]),
                    Descripcion = Convert.ToString(x["TipoCambio"]),
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoProveedor(IMapBuilderContext<ContratoInfo> mapeoAlmacenMoviiento)
        {
            try
            {
                Logger.Info();
                mapeoAlmacenMoviiento.Map(x => x.Proveedor).WithFunc(x => new ProveedorInfo
                {
                    ProveedorID =
                        Convert.ToInt32(x["ProveedorID"]),
                    Descripcion =
                        Convert.ToString(x["Proveedor"]),
                    CodigoSAP =
                        Convert.ToString(x["CodigoSAP"])
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoOrganizacion(IMapBuilderContext<ContratoInfo> mapeoAlmacenMoviiento)
        {
            try
            {
                Logger.Info();
                mapeoAlmacenMoviiento.Map(x => x.Organizacion).WithFunc(x => new OrganizacionInfo
                {
                    OrganizacionID =
                        Convert.ToInt32(x["OrganizacionID"]),
                    Descripcion =
                        Convert.ToString(x["Organizacion"]),
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un mapeo basico de la clase Almacen Movimiento Costo
        /// </summary>
        /// <returns></returns>
        private static IMapBuilderContext<ContratoInfo> MapeoBasico()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<ContratoInfo> mapContrato = MapBuilder<ContratoInfo>.MapNoProperties();
                mapContrato.Map(x => x.ContratoId).ToColumn("ContratoID");
                mapContrato.Map(x => x.Folio).ToColumn("Folio");
                mapContrato.Map(x => x.Precio).ToColumn("Precio");
                mapContrato.Map(x => x.Cantidad).ToColumn("Cantidad");
                mapContrato.Map(x => x.Merma).ToColumn("Merma");
                mapContrato.Map(x => x.PesoNegociar).ToColumn("PesoNegociar");
                mapContrato.Map(x => x.Fecha).ToColumn("Fecha");
                mapContrato.Map(x => x.FechaVigencia).ToColumn("FechaVigencia");
                mapContrato.Map(x => x.Tolerancia).ToColumn("Tolerancia");
                mapContrato.Map(x => x.Parcial).WithFunc(x => Convert.ToBoolean(x["Parcial"]).BoolCompraParcialAEnum());
                return mapContrato;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de contratos
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static List<ContratoInfo> ObtenerContratosPorFechasConciliacion(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var contratos = new List<ContratoInfo>();
                ContratoInfo contrato;
                while (reader.Read())
                {
                    contrato = new ContratoInfo
                    {
                        ContratoId = Convert.ToInt32(reader["ContratoID"]),
                        Cantidad = Convert.ToDecimal(reader["Cantidad"]),
                        Merma = Convert.ToDecimal(reader["Merma"]),
                        PesoNegociar = Convert.ToString(reader["PesoNegociar"]),
                        Fecha = Convert.ToDateTime(reader["Fecha"]),
                        Tolerancia = Convert.ToDecimal(reader["Tolerancia"]),
                        Parcial = Convert.ToBoolean(reader["Parcial"]).BoolCompraParcialAEnum(),
                        Cuenta = new CuentaSAPInfo
                        {
                            CuentaSAPID = Convert.ToInt32(reader["CuentaSAPID"]),
                            CuentaSAP = Convert.ToString(reader["CuentaSAP"]),
                            Descripcion = Convert.ToString(reader["Cuenta"])
                        },
                        Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID = Convert.ToInt32(reader["OrganizacionID"])
                        },
                        Folio = Convert.ToInt32(reader["Folio"]),
                        TipoContrato = new TipoContratoInfo
                        {
                            TipoContratoId = Convert.ToInt32(reader["TipoContratoID"]),
                            Descripcion = Convert.ToString(reader["TipoContrato"])
                        },
                        Precio = Convert.ToDecimal(reader["Precio"]),
                        TipoCambio = new TipoCambioInfo
                        {
                            TipoCambioId = Convert.ToInt32(reader["TipoCambioID"]),
                            Cambio = Convert.ToDecimal(reader["Cambio"])
                        },
                        TipoFlete = new TipoFleteInfo
                        {
                            TipoFleteId = Convert.ToInt32(reader["TipoFleteID"])
                        },
                        Producto = new ProductoInfo
                        {
                            ProductoId = Convert.ToInt32(reader["ProductoID"]),
                            Descripcion = Convert.ToString(reader["Producto"]),
                        },
                        Proveedor = new ProveedorInfo
                        {
                            ProveedorID = Convert.ToInt32(reader["ProveedorID"]),
                            CodigoSAP = Convert.ToString(reader["CodigoSAP"])
                        },
                        ListaContratoParcial = new List<ContratoParcialInfo>()
                    };
                    contratos.Add(contrato);
                }
                reader.NextResult();
                ContratoParcialInfo contratoParcial;
                while (reader.Read())
                {
                    contrato = contratos.FirstOrDefault(id => id.ContratoId == Convert.ToInt32(reader["ContratoID"]));
                    contratoParcial = new ContratoParcialInfo
                    {
                        ContratoId = Convert.ToInt32(reader["ContratoID"]),
                        ContratoParcialId = Convert.ToInt32(reader["ContratoParcialID"]),
                        Cantidad = Convert.ToDecimal(reader["Cantidad"]),
                        Importe = Convert.ToDecimal(reader["Importe"]),
                        TipoCambio = new TipoCambioInfo
                        {
                            TipoCambioId =
                                Convert.ToInt32(reader["TipoCambioID"]),
                            Cambio = Convert.ToDecimal(reader["Cambio"]),
                            Descripcion = Convert.ToString(reader["TipoCambio"])
                        },
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                        Contrato = new ContratoInfo
                        {
                            Folio = contrato.Folio,
                            Fecha = Convert.ToDateTime(reader["FechaCreacion"]),
                            TipoContrato = contrato.TipoContrato,
                            Producto = contrato.Producto,
                            Proveedor = contrato.Proveedor,
                            Organizacion = contrato.Organizacion,
                            Cuenta = contrato.Cuenta,
                        }
                    };
                    contrato.ListaContratoParcial.Add(contratoParcial);
                }
                return contratos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de contratos por proveedor
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContratoInfo> ObtenerContratosPorProveedorAlmacenID(DataSet ds)
        {
            List<ContratoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new ContratoInfo
                         {
                             ContratoId = info.Field<int>("ContratoID"),
                             Organizacion = new OrganizacionInfo() { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Folio = info.Field<int>("Folio"),
                             FolioCadena = info.Field<int>("Folio").ToString(),
                             Producto = new ProductoInfo() { ProductoId = info.Field<int>("ProductoId") },
                             TipoContrato = new TipoContratoInfo() { TipoContratoId = info.Field<int>("TipoContratoID") },
                             TipoFlete = new TipoFleteInfo() { TipoFleteId = info.Field<int>("TipoFleteID") },
                             Proveedor = new ProveedorInfo() { ProveedorID = info.Field<int>("ProveedorID") },
                             Precio = info.Field<Decimal>("Precio"),
                             TipoCambio = new TipoCambioInfo() { TipoCambioId = info["TipoCambioID"] == DBNull.Value ? 0 : info.Field<int>("TipoCambioID") },
                             Cantidad = info.Field<int>("Cantidad"),
                             Merma = info.Field<Decimal>("Merma"),
                             PesoNegociar = info.Field<string>("PesoNegociar"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             FechaVigencia = info.Field<DateTime>("FechaVigencia"),
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
        /// Obtiene una lista de contratos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContratoInfo> ObtenerContratoPorProveedorEntradaProducto(DataSet ds)
        {
            List<ContratoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new ContratoInfo
                         {
                             ContratoId = info.Field<int>("ContratoID"),
                             Folio = info.Field<int>("Folio"),
                             FolioAserca = info.Field<string>("FolioAserca"),
                             FolioCobertura = info.Field<int?>("FolioCobertura") ?? 0,
                             Producto = new ProductoInfo
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                                 Descripcion = info.Field<string>("Producto")
                             },
                             Proveedor = new ProveedorInfo
                             {
                                 ProveedorID = info.Field<int>("ProveedorID"),
                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                                 Descripcion = info.Field<string>("Proveedor"),
                                 OrganizacionID = info.Field<int>("OrganizacionID")
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
    }
}
