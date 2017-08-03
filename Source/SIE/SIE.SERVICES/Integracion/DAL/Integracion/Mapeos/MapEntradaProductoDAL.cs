using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    internal class MapEntradaProductoDAL 
    {

        /// <summary>
        /// Otiene el resultado de la consulta de todos los registros
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaProductoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaProductoInfo
                         {
                             EntradaProductoId = info.Field<int>("EntradaProductoID"),
                             Contrato = info["ContratoID"] == DBNull.Value ?
                                        new ContratoInfo() :
                                        new ContratoInfo
                                        {
                                            ContratoId = info.Field<int>("ContratoID")
                                        },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID") },
                             RegistroVigilancia = new RegistroVigilanciaInfo { RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID") },
                             Folio = info.Field<int>("Folio"),
                             Fecha = info["Fecha"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("Fecha"),
                             FechaDestara = info["FechaDestara"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaDestara"),
                             Observaciones = info.Field<string>("Observaciones"),
                             OperadorAnalista = new OperadorInfo{OperadorID = info["OperadorIDAnalista"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAnalista")},
                             PesoOrigen = info.Field<int>("PesoOrigen"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             PesoTara = info.Field<int>("PesoTara"),
                             Piezas = info.Field<int>("Piezas"),
                             TipoContrato = new TipoContratoInfo{TipoContratoId = info["TipoContratoID"] == DBNull.Value ? 0 : info.Field<int>("TipoContratoID")},
                             Estatus = new EstatusInfo{EstatusId = info.Field<int>("EstatusID")},
                             Justificacion = info.Field<string>("Justificacion"),
                             OperadorBascula = new OperadorInfo{OperadorID = info["OperadorIDBascula"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDBascula")},
                             OperadorAlmacen = new OperadorInfo{OperadorID = info["OperadorIDAlmacen"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAlmacen")},
                             OperadorAutoriza = new OperadorInfo{ OperadorID = info["OperadorIDAutoriza"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAutoriza") },
                             FechaInicioDescarga = info["FechaInicioDescarga"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicioDescarga"),
                             FechaFinDescarga = info["FechaFinDescarga"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFinDescarga"),
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID") },
                             AlmacenMovimiento = new AlmacenMovimientoInfo { AlmacenMovimientoID = info["AlmacenMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoID") },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaEntradaMateriaPrima(DataSet ds)
        {
            ResultadoInfo<EntradaProductoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EntradaProductoInfo> lista = (from info in dt.AsEnumerable()
                                         select new EntradaProductoInfo
                                         {
                                             EntradaProductoId = info.Field<int>("EntradaProductoID"),
                                             Contrato = info["ContratoID"] == DBNull.Value ?
                                                        new ContratoInfo() :
                                                        new ContratoInfo
                                                        {
                                                            ContratoId = info.Field<int>("ContratoID")
                                                        },
                                             Organizacion = new OrganizacionInfo{OrganizacionID = info.Field<int>("OrganizacionID")},
                                             Producto = new ProductoInfo{ProductoId = info.Field<int>("ProductoID"),ProductoDescripcion = info.Field<string>("ProductoDescripcion")},
                                             DescripcionProducto = info.Field<string>("ProductoDescripcion"),
                                             RegistroVigilancia = new RegistroVigilanciaInfo
                                                                {
                                                                    RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID")
                                                                },
                                             Folio = info.Field<int>("Folio"),
                                             Fecha = info.Field<DateTime>("Fecha"),
                                             FechaDestara = info["FechaDestara"] == DBNull.Value ? 
                                                            new DateTime() : 
                                                            info.Field<DateTime>("FechaDestara"),
                                             Observaciones = info["Observaciones"] == DBNull.Value ? 
                                                             string.Empty : 
                                                             info.Field<string>("Observaciones"),
                                             OperadorAnalista = new OperadorInfo
                                                                {
                                                                    OperadorID = info["OperadorIDAnalista"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAnalista")
                                                                },
                                             PesoOrigen = info.Field<int>("PesoOrigen"),
                                             PesoBruto = info.Field<int>("PesoBruto"),
                                             PesoTara = info.Field<int>("PesoTara"),
                                             Piezas = info.Field<int>("Piezas"),
                                             TipoContrato = new TipoContratoInfo
                                                                 {
                                                                     TipoContratoId = info["TipoContratoID"] == DBNull.Value ? 0 : info.Field<int>("TipoContratoID")
                                                                 },
                                             Estatus = new EstatusInfo
                                                        {
                                                            EstatusId = info.Field<int>("EstatusID")
                                                        },
                                             Justificacion = info["Justificacion"] == DBNull.Value ? 
                                                             string.Empty : 
                                                             info.Field<string>("Justificacion"),
                                             OperadorBascula = info["OperadorIDBascula"] == DBNull.Value ? 
                                                               new OperadorInfo() : 
                                                               new OperadorInfo
                                                               {
                                                                   OperadorID = info.Field<int>("OperadorIDBascula")
                                                               },
                                             OperadorAlmacen = info["OperadorIDAlmacen"] == DBNull.Value ? 
                                                                new OperadorInfo() : 
                                                                new OperadorInfo
                                                                {
                                                                    OperadorID = info.Field<int>("OperadorIDAlmacen")
                                                                },
                                             OperadorAutoriza = info["OperadorIDAutoriza"] == DBNull.Value ? 
                                                                new OperadorInfo() : 
                                                                new OperadorInfo
                                                                {
                                                                    OperadorID = info.Field<int>("OperadorIDAutoriza")
                                                                },
                                             FechaInicioDescarga = info["FechaInicioDescarga"] == DBNull.Value ? 
                                                                    new DateTime() : 
                                                                    info.Field<DateTime>("FechaInicioDescarga"),
                                             FechaFinDescarga = info["FechaFinDescarga"] == DBNull.Value ? 
                                                                new DateTime() : 
                                                                info.Field<DateTime>("FechaFinDescarga"),
                                             AlmacenInventarioLote = info["AlmacenInventarioLoteID"] == DBNull.Value ? 
                                                                    new AlmacenInventarioLoteInfo() : 
                                                                    new AlmacenInventarioLoteInfo
                                                                    {
                                                                        AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID")
                                                                    },
                                             AlmacenMovimiento = info["AlmacenMovimientoID"] == DBNull.Value ? 
                                                                new AlmacenMovimientoInfo() : 
                                                                new AlmacenMovimientoInfo
                                                                {
                                                                    AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID")
                                                                }
                                             
                                         }).ToList();
                resultado = new ResultadoInfo<EntradaProductoInfo>
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

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus(DataSet ds)
        {
            ResultadoInfo<EntradaProductoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EntradaProductoInfo> lista = (from info in dt.AsEnumerable()
                                                   select new EntradaProductoInfo
                                                   {
                                                       EntradaProductoId = info.Field<int>("EntradaProductoID"),
                                                       Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("Descripcion") },
                                                       DescripcionProducto  = info.Field<string>("Descripcion"),
                                                       RegistroVigilancia = new RegistroVigilanciaInfo
                                                       {
                                                           Camion = new CamionInfo { PlacaCamion = info.Field<string>("Placas") },
                                                           ProveedorMateriasPrimas = new ProveedorInfo { Descripcion = info.Field<string>("Proveedor") }
                                                       },
                                                       Folio = info.Field<int>("Folio")
                                                   }).ToList();
                resultado = new ResultadoInfo<EntradaProductoInfo>
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

        /// <summary>
        /// Obtiene la entrada de materia prima por folio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaProductoInfo ObtenerPorFolioEntradaMateriaPrima(DataSet ds)
        {
            EntradaProductoInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                                                   select new EntradaProductoInfo
                                                   {
                                                       EntradaProductoId = info.Field<int>("EntradaProductoID"),
                                                       Contrato = info["ContratoID"] == DBNull.Value ?
                                                                         new ContratoInfo() :
                                                                         new ContratoInfo
                                                                         {
                                                                             ContratoId = info.Field<int>("ContratoID")
                                                                         },
                                                       Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                                                       Producto = new ProductoInfo { ProductoId = info.Field<int>("OrganizacionID") },
                                                       RegistroVigilancia = new RegistroVigilanciaInfo
                                                       {
                                                           RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID")
                                                       },
                                                       Folio = info.Field<int>("Folio"),
                                                       Fecha = info.Field<DateTime>("Fecha"),
                                                       FechaDestara = info["FechaDestara"] == DBNull.Value ?
                                                                      new DateTime() :
                                                                      info.Field<DateTime>("FechaDestara"),
                                                       Observaciones = info["Observaciones"] == DBNull.Value ?
                                                                       string.Empty :
                                                                       info.Field<string>("Observaciones"),
                                                       OperadorAnalista = new OperadorInfo
                                                                         {
                                                                             OperadorID = info["OperadorIDAnalista"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAnalista")
                                                                         },
                                                       PesoOrigen = info.Field<int>("PesoOrigen"),
                                                       PesoBruto = info.Field<int>("PesoBruto"),
                                                       PesoTara = info.Field<int>("PesoTara"),
                                                       Piezas = info.Field<int>("Piezas"),
                                                       TipoContrato = info["TipoContratoID"] == DBNull.Value ?
                                                                         new TipoContratoInfo() :
                                                                         new TipoContratoInfo
                                                                         {
                                                                             TipoContratoId = info.Field<int>("TipoContratoID")
                                                                         },
                                                       Estatus = new EstatusInfo
                                                       {
                                                           EstatusId = info.Field<int>("EstatusID")
                                                       },
                                                       Justificacion = info["Justificacion"] == DBNull.Value ?
                                                                       string.Empty :
                                                                       info.Field<string>("Justificacion"),
                                                       OperadorBascula = info["OperadorIDBascula"] == DBNull.Value ?
                                                                         new OperadorInfo() :
                                                                         new OperadorInfo
                                                                         {
                                                                             OperadorID = info.Field<int>("OperadorIDBascula")
                                                                         },
                                                       OperadorAlmacen = info["OperadorIDAlmacen"] == DBNull.Value ?
                                                                          new OperadorInfo() :
                                                                          new OperadorInfo
                                                                          {
                                                                              OperadorID = info.Field<int>("OperadorIDAlmacen")
                                                                          },
                                                       OperadorAutoriza = info["OperadorIDAutoriza"] == DBNull.Value ?
                                                                          new OperadorInfo() :
                                                                          new OperadorInfo
                                                                          {
                                                                              OperadorID = info.Field<int>("OperadorIDAutoriza")
                                                                          },
                                                       FechaInicioDescarga = info["FechaInicioDescarga"] == DBNull.Value ?
                                                                              new DateTime() :
                                                                              info.Field<DateTime>("FechaInicioDescarga"),
                                                       FechaFinDescarga = info["FechaFinDescarga"] == DBNull.Value ?
                                                                          new DateTime() :
                                                                          info.Field<DateTime>("FechaFinDescarga"),
                                                       AlmacenInventarioLote = info["AlmacenInventarioLoteID"] == DBNull.Value ?
                                                                              new AlmacenInventarioLoteInfo() :
                                                                              new AlmacenInventarioLoteInfo
                                                                              {
                                                                                  AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID")
                                                                              },
                                                       AlmacenMovimiento = info["AlmacenMovimientoID"] == DBNull.Value ?
                                                                          new AlmacenMovimientoInfo() :
                                                                          new AlmacenMovimientoInfo
                                                                          {
                                                                              AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID")
                                                                          }

                                                   }).First();
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene la entrada producto por folio y organizacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaProductoInfo ObtenerEntradaProductoPorFolio(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                EntradaProductoInfo entrada =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaProductoInfo
                         {
                             EntradaProductoId = info.Field<int>("EntradaProductoID"),
                             Contrato = info["ContratoID"] == DBNull.Value ?
                                        new ContratoInfo() :
                                        new ContratoInfo
                                        {
                                            ContratoId = info.Field<int>("ContratoID")
                                        },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID") },
                             RegistroVigilancia = new RegistroVigilanciaInfo { RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID") },
                             Folio = info.Field<int>("Folio"),
                             Fecha = info["Fecha"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("Fecha"),
                             FechaDestara = info["FechaDestara"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaDestara"),
                             Observaciones = info.Field<string>("Observaciones"),
                             OperadorAnalista = new OperadorInfo { OperadorID = info["OperadorIDAnalista"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAnalista") },
                             PesoOrigen = info.Field<int>("PesoOrigen"),
                             PesoBonificacion = info.Field<int>("PesoBonificacion"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             PesoTara = info.Field<int>("PesoTara"),
                             Piezas = info.Field<int>("Piezas"),
                             TipoContrato = new TipoContratoInfo { TipoContratoId = info["TipoContratoID"] == DBNull.Value ? 0 : info.Field<int>("TipoContratoID") },
                             Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID") },
                             Justificacion = info.Field<string>("Justificacion"),
                             OperadorBascula = new OperadorInfo { OperadorID = info["OperadorIDBascula"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDBascula") },
                             OperadorAlmacen = new OperadorInfo { OperadorID = info["OperadorIDAlmacen"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAlmacen") },
                             OperadorAutoriza = new OperadorInfo { OperadorID = info["OperadorIDAutoriza"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAutoriza") },
                             FechaInicioDescarga = info["FechaInicioDescarga"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicioDescarga"),
                             FechaFinDescarga = info["FechaFinDescarga"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFinDescarga"),
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID") },
                             AlmacenMovimiento = new AlmacenMovimientoInfo { AlmacenMovimientoID = info["AlmacenMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoID") },
                             AlmacenMovimientoSalida = new AlmacenMovimientoInfo { AlmacenMovimientoID = info["AlmacenMovimientoSalidaID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoSalidaID") },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                             datosOrigen = new DatosOrigen
                             {
                                 folioOrigen = info["FolioOrigen"] == DBNull.Value ? 0 : info.Field<int>("FolioOrigen"),
                                 fechaOrigen = info["FechaEmbarque"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaEmbarque")
                             },
                             PesoDescuento = info.Field<decimal?>("PesoDescuento") ?? 0,
                             NotaDeVenta = dt.Columns.Contains("NotaVenta") ? (info["NotaVenta"] == DBNull.Value ? "" : info.Field<string>("NotaVenta").Trim()) : "" 
                         }).First();
                return entrada;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene las entradas de producto para la ayuda de folios.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaProductoInfo> ObtenerEntradasAyuda(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaProductoInfo
                         {
                             EntradaProductoId = info.Field<int>("EntradaProductoID"),
                             Contrato = new ContratoInfo { 
                                 ContratoId = info["ContratoID"] == DBNull.Value ? 0 : info.Field<int>("ContratoID"),
                                 Folio = info["FolioContrato"] == DBNull.Value ? 0 : info.Field<int>("FolioContrato"),
                             },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("DescripcionProducto"), ProductoDescripcion = info.Field<string>("DescripcionProducto") },
                             RegistroVigilancia = new RegistroVigilanciaInfo { RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID"), ProveedorMateriasPrimas = new ProveedorInfo { Descripcion = info.Field<string>("DescripcionProveedor") } },
                             Folio = info.Field<int>("Folio"),
                             Fecha = info["Fecha"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("Fecha"),
                             FechaDestara = info["FechaDestara"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaDestara"),
                             Observaciones = info.Field<string>("Observaciones"),
                             OperadorAnalista = new OperadorInfo { OperadorID = info["OperadorIDAnalista"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAnalista") },
                             PesoOrigen = info.Field<int>("PesoOrigen"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             PesoTara = info.Field<int>("PesoTara"),
                             Piezas = info.Field<int>("Piezas"),
                             TipoContrato = new TipoContratoInfo { TipoContratoId = info["TipoContratoID"] == DBNull.Value ? 0 : info.Field<int>("TipoContratoID") },
                             Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID") },
                             Justificacion = info.Field<string>("Justificacion"),
                             OperadorBascula = new OperadorInfo { OperadorID = info["OperadorIDBascula"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDBascula") },
                             OperadorAlmacen = new OperadorInfo { OperadorID = info["OperadorIDAlmacen"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAlmacen") },
                             OperadorAutoriza = new OperadorInfo { OperadorID = info["OperadorIDAutoriza"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAutoriza") },
                             FechaInicioDescarga = info["FechaInicioDescarga"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicioDescarga"),
                             FechaFinDescarga = info["FechaFinDescarga"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFinDescarga"),
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID") },
                             AlmacenMovimiento = new AlmacenMovimientoInfo { AlmacenMovimientoID = info["AlmacenMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoID") },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de entradas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaProductoInfo> ObtenerPorContratoOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaProductoInfo
                             {
                                 EntradaProductoId = info.Field<int>("EntradaProductoID"),
                                 Organizacion = new OrganizacionInfo
                                                    {
                                                        OrganizacionID = info.Field<int>("OrganizacionID")
                                                    },
                                 RegistroVigilancia = new RegistroVigilanciaInfo
                                                          {
                                                              RegistroVigilanciaId =
                                                                  info.Field<int>("RegistroVigilanciaID")
                                                          },
                                 Folio = info.Field<int>("Folio"),
                                 Fecha = info.Field<DateTime>("Fecha"),
                                 Observaciones = info.Field<string>("Observaciones"),
                                 PesoOrigen = info.Field<int>("PesoOrigen"),
                                 PesoBruto = info.Field<int>("PesoBruto"),
                                 PesoTara = info.Field<int>("PesoTara"),
                                 Piezas = info.Field<int>("Piezas"),
                                 TipoContrato = new TipoContratoInfo
                                                    {
                                                        TipoContratoId =
                                                            info["TipoContratoID"] == DBNull.Value
                                                                ? 0
                                                                : info.Field<int>("TipoContratoID")
                                                    },
                                 Producto = new ProductoInfo
                                                {
                                                    ProductoId = info.Field<int>("ProductoID")
                                                }
                             }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un contenedor de entrada de materia prima
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ContenedorEntradaMateriaPrimaInfo ObtenerPorFolioEntradaContrato(DataSet ds)
        {
            ContenedorEntradaMateriaPrimaInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                DataTable dtCostos = ds.Tables[ConstantesDAL.DtCostos];
                DataTable dtCostosEntrada = ds.Tables[ConstantesDAL.DtCostosEntrada];

                resultado = (from info in dt.AsEnumerable()
                             select new ContenedorEntradaMateriaPrimaInfo
                                        {
                                            Contrato = new ContratoInfo
                                                           {
                                                               ContratoId = info.Field<int?>("ContratoID") ?? 0,
                                                               TipoContrato = new TipoContratoInfo
                                                                                  {
                                                                                      TipoContratoId =
                                                                                          info.Field<int?>(
                                                                                              "TipoContratoId") ?? 0
                                                                                  },
                                                               Organizacion = new OrganizacionInfo
                                                                                  {
                                                                                      OrganizacionID =
                                                                                          info.Field<int?>(
                                                                                              "OrganizacionID") ?? 0
                                                                                  },
                                                               Folio = info.Field<int?>("Folio") ?? 0,
                                                               Proveedor = new ProveedorInfo
                                                                               {
                                                                                   ProveedorID = info.Field<int?>("ProveedorID") ?? 0,
                                                                                   CodigoSAP = info.Field<string>("CodigoSAP"),
                                                                                   Descripcion = info.Field<string>("Proveedor")
                                                                               },
                                                               PesoNegociar = info.Field<string>("PesoNegociar"),
                                                               TipoCambio = new TipoCambioInfo
                                                                                {
                                                                                    TipoCambioId = info.Field<int?>("TipoCambioID") ?? 0
                                                                                },
                                                               Cuenta = new CuentaSAPInfo
                                                                            {
                                                                                CuentaSAPID = info.Field<int?>("CuentaSAPID") ?? 0
                                                                            }
                                                           },
                                            EntradaProducto = new EntradaProductoInfo
                                                                  {
                                                                      Folio = info.Field<int>("Folio"),
                                                                      Producto = new ProductoInfo
                                                                                     {
                                                                                         ProductoId =
                                                                                             info.Field<int>(
                                                                                                 "ProductoID"),
                                                                                         Descripcion =
                                                                                             info.Field<string>(
                                                                                                 "Producto"),
                                                                                         UnidadMedicion =
                                                                                             new UnidadMedicionInfo
                                                                                                 {
                                                                                                     UnidadID =
                                                                                                         info.Field<int>
                                                                                                         ("UnidadID")
                                                                                                 }
                                                                                     },
                                                                      Organizacion = new OrganizacionInfo
                                                                                         {
                                                                                             OrganizacionID =
                                                                                                 info.Field<int>(
                                                                                                     "OrganizacionID")
                                                                                         },
                                                                      Fecha = info.Field<DateTime>("Fecha"),
                                                                      Observaciones =
                                                                          info.Field<string>("Observaciones"),
                                                                      PesoOrigen = info.Field<int>("PesoOrigen"),
                                                                      PesoBruto = info.Field<int>("PesoBruto"),
                                                                      PesoTara = info.Field<int>("PesoTara"),
                                                                      PesoDescuento = info.Field<int>("PesoDescuento"),
                                                                      PesoBonificacion = info.Field<int>("PesoBonificacion"),
                                                                      AlmacenMovimiento = new AlmacenMovimientoInfo
                                                                                              {
                                                                                                  AlmacenMovimientoID =
                                                                                                      info.Field<long>(
                                                                                                          "AlmacenMovimientoID"),
                                                                                                  Almacen =
                                                                                                      new AlmacenInfo
                                                                                                          {
                                                                                                              AlmacenID
                                                                                                                  =
                                                                                                                  info.
                                                                                                                  Field
                                                                                                                  <int>(
                                                                                                                      "AlmacenID")
                                                                                                          },
                                                                                                  FolioMovimiento =
                                                                                                      info.Field<long>(
                                                                                                          "FolioMovimiento"),
                                                                                                  FechaMovimiento =
                                                                                                      info.Field
                                                                                                      <DateTime>(
                                                                                                          "FechaMovimiento"),
                                                                                                  ProveedorId =
                                                                                                      info.Field<int>(
                                                                                                          "ProveedorID"),
                                                                                              },
                                                                      AlmacenInventarioLote =
                                                                          new AlmacenInventarioLoteInfo
                                                                              {
                                                                                  Lote = info.Field<int>("Lote")
                                                                              }
                                                                  },
                                        }).First();
                List<AlmacenMovimientoDetalle> almacenMovimientoDetalles = (from det in dtDetalle.AsEnumerable()
                                                                            select new AlmacenMovimientoDetalle
                                                                                       {
                                                                                           Producto = new ProductoInfo
                                                                                                          {
                                                                                                              ProductoId
                                                                                                                  =
                                                                                                                  det.
                                                                                                                  Field
                                                                                                                  <int>(
                                                                                                                      "ProductoID")
                                                                                                          },
                                                                                           Precio =
                                                                                               det.Field<decimal>(
                                                                                                   "Precio"),
                                                                                           Cantidad =
                                                                                               det.Field<decimal>(
                                                                                                   "Cantidad"),
                                                                                           Importe =
                                                                                               det.Field<decimal>(
                                                                                                   "Importe"),
                                                                                       }).ToList();
                resultado.EntradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle = almacenMovimientoDetalles;
                IList<CostoEntradaMateriaPrimaInfo> costos = (from costo in dtCostos.AsEnumerable()
                                                              join costoEntrada in dtCostosEntrada.AsEnumerable() on
                                                                  costo.Field<int>("CostoID") equals
                                                                  costoEntrada.Field<int>("CostoID")
                                                              select new CostoEntradaMateriaPrimaInfo
                                                                         {
                                                                             Provedor = new ProveedorInfo
                                                                                            {
                                                                                                ProveedorID =
                                                                                                    costo.Field<int?>(
                                                                                                        "ProveedorID") ?? 0,
                                                                                                Descripcion =
                                                                                                    costoEntrada.Field
                                                                                                    <string>(
                                                                                                        "Descripcion"),
                                                                                                CodigoSAP =
                                                                                                    costoEntrada.Field
                                                                                                    <string>("CodigoSAP")
                                                                                            },
                                                                             Costos = new CostoInfo
                                                                                          {
                                                                                              CostoID =
                                                                                                  costo.Field<int>(
                                                                                                      "CostoID")
                                                                                          },
                                                                             Importe = costo.Field<decimal>("Importe"),
                                                                             TieneCuenta =
                                                                                 costoEntrada.Field<bool>("TieneCuenta"),
                                                                             Iva = costoEntrada.Field<bool>("Iva"),
                                                                             Retencion =
                                                                                 costoEntrada.Field<bool>("Retencion"),
                                                                             Observaciones =
                                                                                 costoEntrada.Field<string>(
                                                                                     "Observaciones"),
                                                                             CuentaSap = costoEntrada.Field<string>("CuentaProvision")
                                                                         }).ToList();
                costos =
                    costos.GroupBy(grupo => new { grupo.Costos.CostoID, grupo.Importe, grupo.Provedor.ProveedorID }).
                        Select(x => new CostoEntradaMateriaPrimaInfo
                        {
                            Provedor = new ProveedorInfo
                            {
                                ProveedorID = x.Key.ProveedorID,
                                Descripcion =
                                    x.Select(prov => prov.Provedor.Descripcion).
                                    FirstOrDefault(),
                                CodigoSAP =
                                    x.Select(prov => prov.Provedor.CodigoSAP).
                                    FirstOrDefault()
                            },
                            Costos = new CostoInfo
                            {
                                CostoID = x.Key.CostoID
                            },
                            Importe = x.Select(imp => imp.Importe).FirstOrDefault(),
                            TieneCuenta = x.Select(cuenta => cuenta.TieneCuenta).FirstOrDefault(),
                            Iva = x.Select(iva => iva.Iva).FirstOrDefault(),
                            Retencion = x.Select(ret => ret.Retencion).FirstOrDefault(),
                            Observaciones = x.Select(obs => obs.Observaciones).FirstOrDefault(),
                            CuentaSap = x.Select(cuenta => cuenta.CuentaSap).FirstOrDefault(),
                            //AlmacenMovimientoID = x.Select(alm => alm.AlmacenMovimientoID).FirstOrDefault()
                            //AlmacenMovimientoID = x.Select(alm => alm.AlmacenMovimientoID).FirstOrDefault()
                        }).ToList();
                var hashCostos = new HashSet<CostoEntradaMateriaPrimaInfo>(costos);
                resultado.ListaCostoEntradaMateriaPrima =
                    new ObservableCollection<CostoEntradaMateriaPrimaInfo>(hashCostos);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        
        internal static List<DiferenciasIndicadoresMuestraContrato> ObtenerDiferenciasIndicadoresMuestraContratoPorEntradaID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<DiferenciasIndicadoresMuestraContrato> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new DiferenciasIndicadoresMuestraContrato
                         {
                            IndicadorID = info.Field<int>("IndicadorID"),
                            Descripcion = info.Field<string>("Descripcion"),
                            PorcentajeContrato = info.Field<decimal>("PorcentajeContrato"),
                            PorcentajeMuestra = info.Field<decimal>("PorcentajeMuestra")
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Mapea los datos de las entradas de productos.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaProductoInfo> ObtenerEntradaProductoAyuda(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaProductoInfo
                         {
                             EntradaProductoId = info.Field<int>("EntradaProductoID"),
                             Contrato = info["ContratoID"] == DBNull.Value ?
                                        new ContratoInfo() :
                                        new ContratoInfo
                                        {
                                            ContratoId = info.Field<int>("ContratoID"),
                                            Folio = info.Field<int>("FolioContrato"),
                                        },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("NombreProducto") },
                             RegistroVigilancia = new RegistroVigilanciaInfo { RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID"), Transportista = info.Field<string>("Transportista") },
                             Folio = info.Field<int>("Folio")
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de entradas por producto
        /// </summary>
        internal static IMapBuilderContext<EntradaProductoInfo> ObtenerMapeoEntradaProducto()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<EntradaProductoInfo> mapeoEntradaProducto =
                    MapBuilder<EntradaProductoInfo>.MapNoProperties();
                MapeoBasico(mapeoEntradaProducto);
                MapeoOrganizacion(mapeoEntradaProducto);
                MapeoAlmacenMovimiento(mapeoEntradaProducto);
                MapeoProducto(mapeoEntradaProducto);
                return mapeoEntradaProducto;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static void MapeoBasico(IMapBuilderContext<EntradaProductoInfo> mapeo)
        {
            try
            {
                Logger.Info();
                mapeo.Map(x => x.EntradaProductoId).ToColumn("EntradaProductoID");
                mapeo.Map(x => x.Folio).ToColumn("Folio");
                mapeo.Map(x => x.Fecha).ToColumn("Fecha");
                mapeo.Map(x => x.Observaciones).ToColumn("Observaciones");
                mapeo.Map(x => x.PesoBonificacion).ToColumn("PesoBonificacion");
                mapeo.Map(x => x.PesoOrigen).ToColumn("PesoOrigen");
                mapeo.Map(x => x.PesoBruto).ToColumn("PesoBruto");
                mapeo.Map(x => x.PesoTara).ToColumn("PesoTara");
                mapeo.Map(x => x.PesoDescuento).ToColumn("PesoDescuento");
                mapeo.Map(x => x.Piezas).ToColumn("Piezas");
                mapeo.Map(x => x.Justificacion).ToColumn("Justificacion");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static void MapeoOrganizacion(IMapBuilderContext<EntradaProductoInfo> mapeo)
        {
            try
            {
                Logger.Info();
                mapeo.Map(x => x.Organizacion).WithFunc(x => new OrganizacionInfo
                                                                 {
                                                                     OrganizacionID = Convert.ToInt32(x["OrganizacionID"])
                                                                 });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static void MapeoProducto(IMapBuilderContext<EntradaProductoInfo> mapeo)
        {
            try
            {
                Logger.Info();
                mapeo.Map(x => x.Producto).WithFunc(x => new ProductoInfo
                {
                    ProductoId = Convert.ToInt32(x["ProductoID"]),
                    Descripcion = Convert.ToString(x["Producto"])
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static void MapeoAlmacenMovimiento(IMapBuilderContext<EntradaProductoInfo> mapeo)
        {
            try
            {
                Logger.Info();
                mapeo.Map(x => x.AlmacenMovimiento).WithFunc(x => new AlmacenMovimientoInfo
                                                                      {
                                                                          AlmacenMovimientoID =
                                                                              Convert.ToInt64(x["AlmacenMovimientoID"]),
                                                                          Almacen = new AlmacenInfo
                                                                                        {
                                                                                            AlmacenID =
                                                                                                Convert.ToInt32(
                                                                                                    x["AlmacenID"])
                                                                                        }
                                                                      });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

		internal static ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(DataSet ds)
        {
            ResultadoInfo<EntradaProductoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EntradaProductoInfo> lista = (from info in dt.AsEnumerable()
                                                   select new EntradaProductoInfo
                                                   {
                                                       EntradaProductoId = info.Field<int>("EntradaProductoID"),
                                                       Contrato = info["ContratoID"] == DBNull.Value ?
                                                                  new ContratoInfo() :
                                                                  new ContratoInfo
                                                                  {
                                                                      ContratoId = info.Field<int>("ContratoID")
                                                                  },
                                                       Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                                                       Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("ProductoDescripcion") },
                                                       DescripcionProducto = info.Field<string>("ProductoDescripcion"),
                                                       RegistroVigilancia = new RegistroVigilanciaInfo
                                                       {
                                                           RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID")
                                                       },
                                                       Folio = info.Field<int>("Folio"),
                                                       Fecha = info.Field<DateTime>("Fecha"),
                                                       FechaDestara = info["FechaDestara"] == DBNull.Value ?
                                                                      new DateTime() :
                                                                      info.Field<DateTime>("FechaDestara"),
                                                       Observaciones = info["Observaciones"] == DBNull.Value ?
                                                                       string.Empty :
                                                                       info.Field<string>("Observaciones"),
                                                       OperadorAnalista = new OperadorInfo
                                                       {
                                                           OperadorID = info["OperadorIDAnalista"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAnalista")
                                                       },
                                                       PesoOrigen = info.Field<int>("PesoOrigen"),
                                                       PesoBruto = info.Field<int>("PesoBruto"),
                                                       PesoTara = info.Field<int>("PesoTara"),
                                                       Piezas = info.Field<int>("Piezas"),
                                                       TipoContrato = new TipoContratoInfo
                                                       {
                                                           TipoContratoId = info["TipoContratoID"] == DBNull.Value ? 0 : info.Field<int>("TipoContratoID")
                                                       },
                                                       Estatus = new EstatusInfo
                                                       {
                                                           EstatusId = info.Field<int>("EstatusID")
                                                       },
                                                       Justificacion = info["Justificacion"] == DBNull.Value ?
                                                                       string.Empty :
                                                                       info.Field<string>("Justificacion"),
                                                       OperadorBascula = info["OperadorIDBascula"] == DBNull.Value ?
                                                                         new OperadorInfo() :
                                                                         new OperadorInfo
                                                                         {
                                                                             OperadorID = info.Field<int>("OperadorIDBascula")
                                                                         },
                                                       OperadorAlmacen = info["OperadorIDAlmacen"] == DBNull.Value ?
                                                                          new OperadorInfo() :
                                                                          new OperadorInfo
                                                                          {
                                                                              OperadorID = info.Field<int>("OperadorIDAlmacen")
                                                                          },
                                                       OperadorAutoriza = info["OperadorIDAutoriza"] == DBNull.Value ?
                                                                          new OperadorInfo() :
                                                                          new OperadorInfo
                                                                          {
                                                                              OperadorID = info.Field<int>("OperadorIDAutoriza")
                                                                          },
                                                       FechaInicioDescarga = info["FechaInicioDescarga"] == DBNull.Value ?
                                                                              new DateTime() :
                                                                              info.Field<DateTime>("FechaInicioDescarga"),
                                                       FechaFinDescarga = info["FechaFinDescarga"] == DBNull.Value ?
                                                                          new DateTime() :
                                                                          info.Field<DateTime>("FechaFinDescarga"),
                                                       AlmacenInventarioLote = info["AlmacenInventarioLoteID"] == DBNull.Value ?
                                                                              new AlmacenInventarioLoteInfo() :
                                                                              new AlmacenInventarioLoteInfo
                                                                              {
                                                                                  AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID")
                                                                              },
                                                       AlmacenMovimiento = info["AlmacenMovimientoID"] == DBNull.Value ?
                                                                          new AlmacenMovimientoInfo() :
                                                                          new AlmacenMovimientoInfo
                                                                          {
                                                                              AlmacenID = info.Field<int>("AlmacenID"),
                                                                              AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                                                              TipoMovimiento = new TipoMovimientoInfo { TipoMovimientoID = info.Field<int>("TipoMovimientoID") }
                                                                             // ListaAlmacenMovimientoDetalle = new List<AlmacenMovimientoDetalle>
                                                                          }

                                                   }).ToList();
                resultado = new ResultadoInfo<EntradaProductoInfo>
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

        /// <summary>
        /// Obtiene un enumerable de entrada producto
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static List<EntradaProductoInfo> ObtenerNotificacionesAutorizadas(IDataReader reader)
        {
            var resultado = new List<EntradaProductoInfo>();
            try
            {
                Logger.Info();
                EntradaProductoInfo entradaProducto;
                while (reader.Read())
                {
                    entradaProducto = new EntradaProductoInfo
                                          {
                                              EntradaProductoId = Convert.ToInt32(reader["EntradaProductoID"]),
                                              Folio = Convert.ToInt32(reader["Folio"]),
                                              Fecha = Convert.ToDateTime(reader["Fecha"]),
                                              Producto = new ProductoInfo
                                                             {
                                                                 ProductoId = Convert.ToInt32(reader["ProductoID"]),
                                                                 Descripcion = Convert.ToString(reader["Producto"])
                                                             },
                                              Contrato = new ContratoInfo
                                                             {
                                                                 Proveedor = new ProveedorInfo
                                                                                 {
                                                                                     ProveedorID =
                                                                                         Convert.ToInt32(
                                                                                             reader["ProveedorID"]),
                                                                                     Descripcion =
                                                                                         Convert.ToString(
                                                                                             reader["Proveedor"])
                                                                                 }
                                                             }
                                          };
                    resultado.Add(entradaProducto);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Otiene el resultado de la consulta de todos los registros
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaProductoInfo> ObtenerTodosAyuda(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaProductoInfo
                         {
                             EntradaProductoId = info.Field<int>("EntradaProductoID"),
                             Contrato = info["ContratoID"] == DBNull.Value ?
                                        new ContratoInfo() :
                                        new ContratoInfo
                                        {
                                            ContratoId = info.Field<int>("ContratoID")
                                        },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID") },
                             RegistroVigilancia = new RegistroVigilanciaInfo { RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID") },
                             Folio = info.Field<int>("Folio"),
                             Fecha = info["Fecha"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("Fecha"),
                             FechaDestara = info["FechaDestara"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaDestara"),
                             Observaciones = info.Field<string>("Observaciones"),
                             OperadorAnalista = new OperadorInfo { OperadorID = info["OperadorIDAnalista"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAnalista") },
                             PesoOrigen = info.Field<int>("PesoOrigen"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             PesoTara = info.Field<int>("PesoTara"),
                             Piezas = info.Field<int>("Piezas"),
                             TipoContrato = new TipoContratoInfo { TipoContratoId = info["TipoContratoID"] == DBNull.Value ? 0 : info.Field<int>("TipoContratoID") },
                             Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Descripcion") },
                             Justificacion = info.Field<string>("Justificacion"),
                             OperadorBascula = new OperadorInfo { OperadorID = info["OperadorIDBascula"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDBascula") },
                             OperadorAlmacen = new OperadorInfo { OperadorID = info["OperadorIDAlmacen"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAlmacen") },
                             OperadorAutoriza = new OperadorInfo { OperadorID = info["OperadorIDAutoriza"] == DBNull.Value ? 0 : info.Field<int>("OperadorIDAutoriza") },
                             FechaInicioDescarga = info["FechaInicioDescarga"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicioDescarga"),
                             FechaFinDescarga = info["FechaFinDescarga"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFinDescarga"),
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID") },
                             AlmacenMovimiento = new AlmacenMovimientoInfo { AlmacenMovimientoID = info["AlmacenMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoID") },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,

                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static decimal ObtenerHumedadOrigen(DataSet ds)
        {
            decimal resultado = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    resultado = dr["HumedadOrigen"] == DBNull.Value ? 0 : dr.Field<decimal>("HumedadOrigen");
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene una lista de entradas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaProductoInfo> ObtenerEntradaProductoPorContrato(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtEntradaProductoDetalle = ds.Tables[ConstantesDAL.DtEntradaProductoDetalle];
                DataTable dtEntradaProductoMuestra = ds.Tables[ConstantesDAL.DtEntradaProductoMuestra];
                List<EntradaProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaProductoInfo
                         {
                             EntradaProductoId = info.Field<int>("EntradaProductoID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID")
                             },
                             RegistroVigilancia = new RegistroVigilanciaInfo
                             {
                                 RegistroVigilanciaId =
                                     info.Field<int>("RegistroVigilanciaID")
                             },
                             Folio = info.Field<int>("Folio"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             Observaciones = info.Field<string>("Observaciones"),
                             PesoOrigen = info.Field<int>("PesoOrigen"),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             PesoTara = info.Field<int>("PesoTara"),
                             Piezas = info.Field<int>("Piezas"),
                             AlmacenMovimiento = new AlmacenMovimientoInfo
                                                 {
                                                     AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID")
                                                 },
                             TipoContrato = new TipoContratoInfo
                             {
                                 TipoContratoId =
                                     info["TipoContratoID"] == DBNull.Value
                                         ? 0
                                         : info.Field<int>("TipoContratoID")
                             },
                             Producto = new ProductoInfo
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                                 Descripcion = info.Field<string>("Producto"),
                                 SubFamilia = new SubFamiliaInfo
                                              {
                                                  SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                                  Descripcion = info.Field<string>("SubFamilia")
                                              }

                             }
                         }).ToList();

                var productosDetalle = (from det in dtEntradaProductoDetalle.AsEnumerable()
                    select new EntradaProductoDetalleInfo
                           {
                               EntradaProductoId = det.Field<int>("EntradaProductoID"),
                               EntradaProductoDetalleId = det.Field<int>("EntradaProductoDetalleId"),
                               Indicador = new IndicadorInfo
                                           {
                                               IndicadorId = det.Field<int>("IndicadorID"),
                                               Descripcion = det.Field<string>("Indicador")
                                           }
                           }).ToList();

                var productosMuestra = (from det in dtEntradaProductoMuestra.AsEnumerable()
                                        select new EntradaProductoMuestraInfo
                                        {
                                            EntradaProductoMuestraId = det.Field<int>("EntradaProductoMuestraID"),
                                            EntradaProductoDetalleId = det.Field<int>("EntradaProductoDetalleID"),
                                            Porcentaje = det.Field<decimal>("Porcentaje"),
                                            Descuento = det.Field<decimal>("Descuento"),
                                            Rechazo = det.Field<bool>("Rechazo").BoolAEnum(),
                                        }).ToList();

                productosDetalle.ForEach(det=> det.ProductoMuestras = productosMuestra.Where(mues=> mues.EntradaProductoDetalleId == det.EntradaProductoDetalleId).ToList());
                lista.ForEach(entra => entra.ProductoDetalle = productosDetalle.Where(det=> det.EntradaProductoId == entra.EntradaProductoId).ToList());

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
