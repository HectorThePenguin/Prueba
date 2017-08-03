using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal static class MapEntradaGanadoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerPorID(DataSet ds)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo["CorralID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("CorralID"),                                         
                                        Lote = new LoteInfo
                                        {
                                            LoteID = entradaInfo["LoteID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("LoteID"),
                                            Lote = entradaInfo["Lote"] == DBNull.Value ? string.Empty : entradaInfo.Field<string>("Lote"),
                                        },
                                        LoteID = entradaInfo.Field<int?>("LoteID") ?? 0,
                                        Observacion = entradaInfo.Field<string>("Observacion"),
                                        ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                        Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                        TipoOrganizacionOrigen = entradaInfo.Field<string>("TipoOrganizacion"),
                                        TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID"),
                                        OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                        CodigoCorral = entradaInfo.Field<string>("Corral"),
                                        CodigoLote = entradaInfo.Field<string>("Lote"),
                                        CertificadoZoosanitario = entradaInfo.Field<string>("CertificadoZoosanitario"),
                                        PruebasTB = entradaInfo.Field<string>("PruebaTB"),
                                        PruebasTR = entradaInfo.Field<string>("PruebaTR"),
                                        CondicionJaula = new CondicionJaulaInfo
                                                            {
                                                                CondicionJaulaID = entradaInfo.Field<int?>("CondicionJaulaID") ?? 0
                                                            }
                                         }).First();

                if (entradaGanadoInfo != null)
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    entradaGanadoInfo.ListaCondicionGanado = (from entrada in dt.AsEnumerable()
                                                              select new EntradaCondicionInfo
                                                              {
                                                                  Activo = entrada.Field<bool>("Activo").BoolAEnum()
                                                                  ,
                                                                  Cabezas = entrada.Field<int>("Cabezas")
                                                                  ,
                                                                  CondicionID = entrada.Field<int>("CondicionID")
                                                                  ,
                                                                  EntradaGanadoID =
                                                                      entrada.Field<int>("EntradaGanadoID")
                                                                  ,
                                                                  EntradaCondicionID =
                                                                      entrada.Field<int>("EntradaCondicionID")
                                                                      ,
                                                                  CondicionDescripcion = entrada.Field<string>("Descripcion")
                                                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerPorIDOrganizacion(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EntradaGanadoInfo> listaEntradaGanado = (from entradainfo in dt.AsEnumerable()
                                                              select new EntradaGanadoInfo
                                                              {
                                                                  EntradaGanadoID = entradainfo.Field<int>("EntradaGanadoID"),
                                                                  FolioEntrada = entradainfo.Field<int>("FolioEntrada"),
                                                                  OrganizacionID = entradainfo.Field<int>("OrganizacionID"),
                                                                  OrganizacionOrigenID = entradainfo.Field<int>("OrganizacionOrigenID"),
                                                                  FechaEntrada = entradainfo.Field<DateTime>("FechaEntrada"),
                                                                  EmbarqueID = entradainfo.Field<int>("EmbarqueID"),
                                                                  FolioOrigen = entradainfo.Field<int>("FolioOrigen"),
                                                                  FechaSalida = entradainfo.Field<DateTime>("FechaSalida"),
                                                                  ChoferID = entradainfo.Field<int>("ChoferID"),
                                                                  JaulaID = entradainfo.Field<int>("JaulaID"),
                                                                  CamionID = entradainfo.Field<int>("CamionID"),
                                                                  CabezasOrigen = entradainfo.Field<int>("CabezasOrigen"),
                                                                  CabezasRecibidas = entradainfo.Field<int>("CabezasRecibidas"),
                                                                  OperadorID = entradainfo.Field<int>("OperadorID"),
                                                                  PesoBruto = entradainfo.Field<decimal>("PesoBruto"),
                                                                  PesoTara = entradainfo.Field<decimal>("PesoTara"),
                                                                  EsRuteo = entradainfo.Field<bool>("EsRuteo"),
                                                                  Fleje = entradainfo.Field<bool>("Fleje"),
                                                                  CheckList = entradainfo.Field<string>("CheckList"),
                                                                  CorralID = entradainfo["CorralID"] == DBNull.Value ? 0 : entradainfo.Field<int>("CorralID"),
                                                                  Lote = new LoteInfo
                                                                  {
                                                                      LoteID = entradainfo["LoteID"] == DBNull.Value ? 0 : entradainfo.Field<int>("LoteID"),
                                                                      Lote = entradainfo["Lote"] == DBNull.Value ? string.Empty : entradainfo.Field<string>("Lote"),
                                                                  },
                                                                  Observacion = entradainfo.Field<string>("Observacion"),
                                                                  ImpresionTicket = entradainfo.Field<bool>("ImpresionTicket"),
                                                                  Activo = entradainfo.Field<bool>("Activo").BoolAEnum(),
                                                                  OrganizacionOrigen = entradainfo.Field<string>("OrganizacionOrigen"),
                                                                  CodigoCorral = entradainfo.Field<string>("CodigoCorral")
                                                              }).ToList();
                resultado = new ResultadoInfo<EntradaGanadoInfo>
                {
                    Lista = listaEntradaGanado,
                    TotalRegistros = Convert.ToInt32(listaEntradaGanado.Count())
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
        /// Metodo para obtener una lista de entradas de ganado activas paginada 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerEntradasActivasPorPagina(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<EntradaGanadoInfo> lista = (from entradaInfo in dt.AsEnumerable()
                                                  select new EntradaGanadoInfo
                                                  {
                                                      EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                                      FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                                      OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                                      OrganizacionOrigenID =
                                                          entradaInfo.Field<int>("OrganizacionOrigenID"),
                                                      FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                                      EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                                      FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                                      FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                                      ChoferID = entradaInfo.Field<int>("ChoferID"),
                                                      JaulaID = entradaInfo.Field<int>("JaulaID"),
                                                      CamionID = entradaInfo.Field<int>("CamionID"),
                                                      CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                                      CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                                      OperadorID = entradaInfo.Field<int>("OperadorID"),
                                                      PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                                      PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                                      EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                                      Fleje = entradaInfo.Field<bool>("Fleje"),
                                                      CheckList = entradaInfo.Field<string>("CheckList"),
                                                      CorralID = entradaInfo.Field<int>("CorralID"),
                                                      Lote = new LoteInfo
                                                      {
                                                          LoteID = entradaInfo.Field<int>("LoteID")
                                                      },
                                                      Observacion = entradaInfo.Field<string>("Observacion"),
                                                      ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                                      Activo = entradaInfo.Field<bool>("Activo").BoolAEnum()
                                                  }).ToList();
                resultado = new ResultadoInfo<EntradaGanadoInfo>
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
        /// Obtiene la Entrada de Ganado por Pagina
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerEntradasPorPagina(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<EntradaGanadoInfo> lista = (from entradaInfo in dt.AsEnumerable()
                                                  select new EntradaGanadoInfo
                                                  {
                                                      FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                                      Observacion = entradaInfo.Field<string>("Descripcion"),
                                                      EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoId"),
                                                  }).ToList();
                resultado = new ResultadoInfo<EntradaGanadoInfo>
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
        /// Obtiene la Entrada de Ganado por Pagina
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerPorDependencias(DataSet ds)
        {
            EntradaGanadoInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from entradaInfo in dt.AsEnumerable()
                             select new EntradaGanadoInfo
                             {
                                 FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                 Observacion = entradaInfo.Field<string>("Organizacion")
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
        ///  Metodo que obtiene una Entrada de Ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerPorEmbarqueID(DataSet ds)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo["CorralID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("CorralID"),
                                         Lote = new LoteInfo
                                         {
                                             LoteID = entradaInfo["LoteID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("LoteID"),
                                         },
                                         Observacion = entradaInfo.Field<string>("Observacion"),
                                         ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                         Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                         Poliza = entradaInfo.Field<bool>("Poliza"),
                                         Factura = entradaInfo.Field<bool>("Factura"),
                                         HojaEmbarque = entradaInfo.Field<bool>("HojaEmbarque"),
                                         Guia = entradaInfo.Field<bool>("Guia"),
                                         ManejoSinEstres = entradaInfo.Field<bool>("ManejoSinEstres"),
                                         CertificadoZoosanitario = entradaInfo.Field<string>("CertificadoZoosanitario"),
                                         PruebasTB = entradaInfo.Field<string>("PruebaTB"),
                                         PruebasTR = entradaInfo.Field<string>("PruebaTR"),
                                         CondicionJaula = new CondicionJaulaInfo
                                         {
                                             CondicionJaulaID = entradaInfo.Field<int?>("CondicionJaulaID") ?? 0
                                         }
                                     }).First();

                if (entradaGanadoInfo != null)
                {
                    try
                    {
                        dt = ds.Tables[ConstantesDAL.DtCostos];
                        entradaGanadoInfo.CostosEmbarque = (from costoinfo in dt.AsEnumerable()
                                                            select new CostoInfo
                                                            {
                                                                CostoID = costoinfo.Field<int>("CostoID"),
                                                                ImporteCosto = costoinfo.Field<Decimal>("Importe")
                                                            }).ToList<CostoInfo>();
                    }
                    catch(Exception ex)
                    {
                        ex.ToString();
                    }
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    entradaGanadoInfo.ListaCondicionGanado = (from entrada in dt.AsEnumerable()
                                                              select new EntradaCondicionInfo
                                                              {
                                                                  Activo = entrada.Field<bool>("Activo").BoolAEnum()
                                                                  ,
                                                                  Cabezas = entrada.Field<int>("Cabezas")
                                                                  ,
                                                                  CondicionID = entrada.Field<int>("CondicionID")
                                                                  ,
                                                                  EntradaGanadoID = entrada.Field<int>("EntradaGanadoID")
                                                                  ,
                                                                  EntradaCondicionID = entrada.Field<int>("EntradaCondicionID")
                                                                  ,
                                                                  CondicionDescripcion = entrada.Field<string>("Descripcion")
                                                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene una Entrada de Ganado por Folio de Entrada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerPorFolioEntrada(DataSet ds)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo["CorralID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("CorralID"),
                                         Lote = new LoteInfo
                                         {
                                             LoteID = entradaInfo["LoteID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("LoteID")
                                         },
                                         Observacion = entradaInfo.Field<string>("Observacion"),
                                         ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                         Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                         TipoOrganizacionOrigen = entradaInfo.Field<string>("TipoOrganizacion"),
                                         TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID"),
                                         OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                         CodigoCorral = entradaInfo.Field<string>("Corral"),
                                         CodigoLote = entradaInfo.Field<string>("Lote"),
                                         Operador = entradaInfo.Field<string>("NombreOperador"),
                                         Proveedor = entradaInfo.Field<string>("Proveedor"),
                                         ProveedorID = entradaInfo.Field<int>("ProveedorID"),
                                         Guia = entradaInfo.Field<bool>("Guia"),
                                         Poliza = entradaInfo.Field<bool>("Poliza"),
                                         HojaEmbarque = entradaInfo.Field<bool>("HojaEmbarque"),
                                         Factura = entradaInfo.Field<bool>("Factura"),
                                         ManejoSinEstres = entradaInfo.Field<bool>("ManejoSinEstres"),
                                         CertificadoZoosanitario = entradaInfo.Field<string>("CertificadoZoosanitario"),
                                         PruebasTB = entradaInfo.Field<string>("PruebaTB"),
                                         PruebasTR = entradaInfo.Field<string>("PruebaTR"),
                                         CondicionJaula = new CondicionJaulaInfo
                                         {
                                             CondicionJaulaID = entradaInfo.Field<int?>("CondicionJaulaID") ?? 0
                                         }
                                     }).First();

                if (entradaGanadoInfo != null)
                {
                    try
                    {
                        dt = ds.Tables[ConstantesDAL.DtCostos];
                        entradaGanadoInfo.CostosEmbarque = (from costoinfo in dt.AsEnumerable()
                                                            select new CostoInfo
                                                            {
                                                                CostoID = costoinfo.Field<int>("CostoID"),
                                                                ImporteCosto = costoinfo.Field<Decimal>("Importe")
                                                            }).ToList<CostoInfo>();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    entradaGanadoInfo.ListaCondicionGanado = (from entrada in dt.AsEnumerable()
                                                              select new EntradaCondicionInfo
                                                              {
                                                                  Activo = entrada.Field<bool>("Activo").BoolAEnum()
                                                                  ,
                                                                  Cabezas = entrada.Field<int>("Cabezas")
                                                                  ,
                                                                  CondicionID = entrada.Field<int>("CondicionID")
                                                                  ,
                                                                  EntradaGanadoID = entrada.Field<int>("EntradaGanadoID")
                                                                  ,
                                                                  EntradaCondicionID = entrada.Field<int>("EntradaCondicionID")
                                                                  ,
                                                                  CondicionDescripcion = entrada.Field<string>("Descripcion")
                                                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerPorTipoCorralID(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EntradaGanadoInfo> listaEntradaGanado = (from groupinfo in dt.AsEnumerable()
                                                              select new EntradaGanadoInfo
                                                              {
                                                                  EntradaGanadoID = groupinfo.Field<int>("EntradaGanadoID"),
                                                                  FolioEntrada = groupinfo.Field<int>("FolioEntrada"),
                                                                  OrganizacionID = groupinfo.Field<int>("OrganizacionID"),
                                                                  OrganizacionOrigenID = groupinfo.Field<int>("OrganizacionOrigenID"),
                                                                  FechaEntrada = groupinfo.Field<DateTime>("FechaEntrada"),
                                                                  //ProgramacionEmbarqueID = groupinfo.Field<int>("ProgramacionEmbarqueID"),
                                                                  FolioOrigen = groupinfo.Field<int>("FolioOrigen"),
                                                                  FechaSalida = groupinfo.Field<DateTime>("FechaSalida"),
                                                                  ChoferID = groupinfo.Field<int>("ChoferID"),
                                                                  JaulaID = groupinfo.Field<int>("JaulaID"),
                                                                  CamionID = groupinfo.Field<int>("CamionID"),
                                                                  CabezasOrigen = groupinfo.Field<int>("CabezasOrigen"),
                                                                  CabezasRecibidas = groupinfo.Field<int>("CabezasRecibidas"),
                                                                  OperadorID = groupinfo.Field<int>("OperadorID"),
                                                                  PesoBruto = groupinfo.Field<decimal>("PesoBruto"),
                                                                  PesoTara = groupinfo.Field<decimal>("PesoTara"),
                                                                  EsRuteo = groupinfo.Field<bool>("EsRuteo"),
                                                                  Fleje = groupinfo.Field<bool>("Fleje"),
                                                                  CheckList = groupinfo.Field<string>("CheckList"),
                                                                  CorralID = groupinfo.Field<int>("CorralID"),
                                                                  LoteID = groupinfo.Field<int>("LoteID"),
                                                                  Observacion = groupinfo.Field<string>("Observacion"),
                                                                  ImpresionTicket = groupinfo.Field<bool>("ImpresionTicket"),
                                                                  Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                                                  OrganizacionOrigen = groupinfo.Field<string>("OrganizacionOrigen"),
                                                                  CodigoCorral = groupinfo.Field<string>("CodigoCorral"),
                                                                  Evaluador = groupinfo.Field<string>("Evaluador"),
                                                                  FechaEvaluacion = groupinfo.Field<DateTime>("FechaEvaluacion"),
                                                                  Hembras = groupinfo.Field<int>("Hembras"),
                                                                  Machos = groupinfo.Field<int>("Machos"),
                                                                  //TODO: Revisar implementacion de rechazos desde la tabla
                                                                  Rechazos = groupinfo.Field<int>("Rechazos").ToString(CultureInfo.InvariantCulture),
                                                                  PesoOrigen = groupinfo["PesoOrigen"] == DBNull.Value ? 0 : groupinfo.Field<int>("PesoOrigen"),
                                                                  PesoLlegada = (int)(groupinfo.Field<decimal>("PesoBruto") - groupinfo.Field<decimal>("PesoTara")),
                                                                  NivelGarrapata = groupinfo["NivelGarrapata"] == DBNull.Value ? 0 : groupinfo.Field<int>("NivelGarrapata").NivelGarrapataAEnum(),
                                                                  EsMetaFilaxia = groupinfo["EsMetafilaxiaOAutorizada"] == DBNull.Value ? false :
                                                                            groupinfo.Field<int>("EsMetafilaxiaOAutorizada") == 1 ? true : false
                                                              }).ToList();
                resultado = new ResultadoInfo<EntradaGanadoInfo>
                {
                    Lista = listaEntradaGanado,
                    TotalRegistros = Convert.ToInt32(listaEntradaGanado.Count())
                };

                dt = null;
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
        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EntradaGanadoInfo> lista = (from groupinfo in dt.AsEnumerable()
                                                 select new EntradaGanadoInfo
                                                 {
                                                     EntradaGanadoID = groupinfo.Field<int>("EntradaGanadoID"),
                                                     FolioEntrada = groupinfo.Field<int>("FolioEntrada"),
                                                     OrganizacionID = groupinfo.Field<int>("OrganizacionID"),
                                                     OrganizacionOrigenID = groupinfo.Field<int>("OrganizacionOrigenID"),
                                                     FechaEntrada = groupinfo.Field<DateTime>("FechaEntrada"),
                                                     EmbarqueID = groupinfo.Field<int>("EmbarqueID"),
                                                     FolioOrigen = groupinfo.Field<int>("FolioOrigen"),
                                                     FechaSalida = groupinfo.Field<DateTime>("FechaSalida"),
                                                     ChoferID = groupinfo.Field<int>("ChoferID"),
                                                     JaulaID = groupinfo.Field<int>("JaulaID"),
                                                     CamionID = groupinfo.Field<int>("CamionID"),
                                                     CabezasOrigen = groupinfo.Field<int>("CabezasOrigen"),
                                                     CabezasRecibidas = groupinfo.Field<int>("CabezasRecibidas"),
                                                     OperadorID = groupinfo.Field<int>("OperadorID"),
                                                     PesoBruto = groupinfo.Field<decimal>("PesoBruto"),
                                                     PesoTara = groupinfo.Field<decimal>("PesoTara"),
                                                     EsRuteo = groupinfo.Field<bool>("EsRuteo"),
                                                     Fleje = groupinfo.Field<bool>("Fleje"),
                                                     CheckList = groupinfo.Field<string>("CheckList"),
                                                     CorralID = groupinfo.Field<int>("CorralID"),
                                                     LoteID = groupinfo.Field<int>("LoteID"),
                                                     Observacion = groupinfo.Field<string>("Observacion"),
                                                     ImpresionTicket = groupinfo.Field<bool>("ImpresionTicket"),
                                                     Activo = groupinfo.Field<bool>("Activo").BoolAEnum()
                                                 }).ToList();

                resultado = new ResultadoInfo<EntradaGanadoInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerPartidasProgramadas(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<EntradaGanadoInfo> lista = (from groupinfo in dt.AsEnumerable()
                                                  select new EntradaGanadoInfo
                                                  {
                                                      EntradaGanadoID = groupinfo.Field<int>("EntradaGanadoID"),
                                                      FolioEntrada = groupinfo.Field<int>("FolioEntrada"),
                                                      OrganizacionID = groupinfo.Field<int>("OrganizacionID"),
                                                      OrganizacionOrigenID = groupinfo.Field<int>("OrganizacionOrigenID"),
                                                      FechaEntrada = groupinfo.Field<DateTime>("FechaEntrada"),
                                                      EmbarqueID = groupinfo.Field<int>("EmbarqueID"),
                                                      FolioOrigen = groupinfo.Field<int>("FolioOrigen"),
                                                      FechaSalida = groupinfo.Field<DateTime>("FechaSalida"),
                                                      ChoferID = groupinfo.Field<int>("ChoferID"),
                                                      JaulaID = groupinfo.Field<int>("JaulaID"),
                                                      CamionID = groupinfo.Field<int>("CamionID"),
                                                      CabezasOrigen = groupinfo.Field<int>("CabezasOrigen"),
                                                      CabezasRecibidas = groupinfo.Field<int>("CabezasRecibidas"),
                                                      OperadorID = groupinfo.Field<int>("OperadorID"),
                                                      PesoBruto = groupinfo.Field<decimal>("PesoBruto"),
                                                      PesoTara = groupinfo.Field<decimal>("PesoTara"),
                                                      EsRuteo = groupinfo.Field<bool>("EsRuteo"),
                                                      Fleje = groupinfo.Field<bool>("Fleje"),
                                                      CheckList = groupinfo.Field<string>("CheckList"),
                                                      CorralID = groupinfo.Field<int>("CorralID"),
                                                      LoteID = groupinfo.Field<int>("LoteID"),
                                                      Observacion = groupinfo.Field<string>("Observacion"),
                                                      ImpresionTicket = groupinfo.Field<bool>("ImpresionTicket"),
                                                      Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                                      CodigoCorral = groupinfo.Field<string>("Corral"),
                                                      TipoOrganizacion = groupinfo.Field<string>("TipoOrganizacion"),
                                                      OrganizacionOrigen = groupinfo.Field<string>("Organizacion"),
                                                      FechaEvaluacion = groupinfo.Field<DateTime>("FechaEvaluacion"),
                                                      EsMetaFilaxia = groupinfo.Field<bool>("EsMetaFilaxia"),
                                                      PesoOrigen = groupinfo.Field<int>("PesoOrigen"),
                                                      PesoLlegada = groupinfo.Field<int>("PesoLlegada"),
                                                      TipoOrigen = groupinfo.Field<int>("TipoOrganizacionID")
                                                  }).ToList();

                entradaGanadoInfo = new ResultadoInfo<EntradaGanadoInfo>
                {
                    Lista = lista,
                    TotalRegistros = lista.Count()
                };
                dt = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerPartidasProgramadasPorPagina(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<EntradaGanadoInfo> lista = (from groupinfo in dt.AsEnumerable()
                                                  select new EntradaGanadoInfo
                                                  {
                                                      EntradaGanadoID = groupinfo.Field<int>("EntradaGanadoID"),
                                                      FolioEntrada = groupinfo.Field<int>("FolioEntrada"),
                                                      OrganizacionID = groupinfo.Field<int>("OrganizacionID"),
                                                      OrganizacionOrigenID = groupinfo.Field<int>("OrganizacionOrigenID"),
                                                      FechaEntrada = groupinfo.Field<DateTime>("FechaEntrada"),
                                                      EmbarqueID = groupinfo.Field<int>("EmbarqueID"),
                                                      FolioOrigen = groupinfo.Field<int>("FolioOrigen"),
                                                      FechaSalida = groupinfo.Field<DateTime>("FechaSalida"),
                                                      ChoferID = groupinfo.Field<int>("ChoferID"),
                                                      JaulaID = groupinfo.Field<int>("JaulaID"),
                                                      CamionID = groupinfo.Field<int>("CamionID"),
                                                      CabezasOrigen = groupinfo.Field<int>("CabezasOrigen"),
                                                      CabezasRecibidas = groupinfo.Field<int>("CabezasRecibidas"),
                                                      OperadorID = groupinfo.Field<int>("OperadorID"),
                                                      PesoBruto = groupinfo.Field<decimal>("PesoBruto"),
                                                      PesoTara = groupinfo.Field<decimal>("PesoTara"),
                                                      EsRuteo = groupinfo.Field<bool>("EsRuteo"),
                                                      Fleje = groupinfo.Field<bool>("Fleje"),
                                                      CheckList = groupinfo.Field<string>("CheckList"),
                                                      CorralID = groupinfo.Field<int>("CorralID"),
                                                      LoteID = groupinfo.Field<int>("LoteID"),
                                                      Observacion = groupinfo.Field<string>("Observacion"),
                                                      ImpresionTicket = groupinfo.Field<bool>("ImpresionTicket"),
                                                      Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                                      CodigoCorral = groupinfo.Field<string>("Corral"),
                                                      TipoOrganizacion = groupinfo.Field<string>("TipoOrganizacion"),
                                                      OrganizacionOrigen = groupinfo.Field<string>("Organizacion"),
                                                      FechaEvaluacion = groupinfo.Field<DateTime>("FechaEvaluacion"),
                                                      EsMetaFilaxia = groupinfo.Field<bool>("EsMetaFilaxia"),
                                                      PesoOrigen = groupinfo.Field<int>("PesoOrigen"),
                                                      PesoLlegada = groupinfo.Field<int>("PesoLlegada"),
                                                      TipoOrigen = groupinfo.Field<int>("TipoOrganizacionID"),
                                                      NivelGarrapata = (NivelGarrapata)groupinfo.Field<int>("NivelGarrapata")
                                                  }).ToList();

                entradaGanadoInfo = new ResultadoInfo<EntradaGanadoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
                dt = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }
        /// <summary>
        ///     Metodo que obtiene CalidadGanado Por sexo
        /// </summary>
        /// <returns></returns>
        internal static IList<CalidadGanadoInfo> ObtenerCalidadPorSexo(DataSet ds)
        {
            IList<CalidadGanadoInfo> calidadGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                calidadGanadoInfo = (from groupinfo in dt.AsEnumerable()
                                     select new CalidadGanadoInfo
                                     {
                                         Descripcion = groupinfo.Field<string>("Descripcion"),
                                         CalidadGanadoID = groupinfo.Field<int>("CalidadGanadoID")
                                     }).ToList<CalidadGanadoInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return calidadGanadoInfo;
        }
        /// <summary>
        ///     Metodo que obtiene CalidadGanado Por Causarechazo
        /// </summary>
        /// <returns></returns>
        internal static IList<CausaRechazoInfo> ObtenerCalidadPorCausaRechazo(DataSet ds)
        {
            IList<CausaRechazoInfo> causaRechazoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                causaRechazoInfo = (from groupinfo in dt.AsEnumerable()
                                    select new CausaRechazoInfo
                                    {
                                        Descripcion = groupinfo.Field<string>("Descripcion"),
                                        CausaRechazoID = groupinfo.Field<int>("CausaRechazoID")
                                    }).ToList<CausaRechazoInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return causaRechazoInfo;
        }

        /// <summary>
        /// Metodo que obtiene el catalogo de clasificacion
        /// </summary>
        /// <returns></returns>
        internal static IList<ClasificacionGanadoInfo> ObtenerCatClasificacion(DataSet ds)
        {
            IList<ClasificacionGanadoInfo> clasificacionGanadoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                clasificacionGanadoInfo = (from groupinfo in dt.AsEnumerable()
                                           select new ClasificacionGanadoInfo
                                           {
                                               ClasificacionGanadoID =
                                                   groupinfo.Field<int>("ClasificacionGanadoID"),
                                               Descripcion = groupinfo.Field<string>("Descripcion")
                                           }).ToList<ClasificacionGanadoInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return clasificacionGanadoInfo;
        }
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerPorIDOrganizacionEVP(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EntradaGanadoInfo> listaEntradaGanado = (from entradainfo in dt.AsEnumerable()
                                                              select new EntradaGanadoInfo
                                                              {
                                                                  EntradaGanadoID = entradainfo.Field<int>("EntradaGanadoID"),
                                                                  FolioEntrada = entradainfo.Field<int>("FolioEntrada"),
                                                                  OrganizacionID = entradainfo.Field<int>("OrganizacionID"),
                                                                  OrganizacionOrigenID = entradainfo.Field<int>("OrganizacionOrigenID"),
                                                                  FechaEntrada = entradainfo.Field<DateTime>("FechaEntrada"),
                                                                  EmbarqueID = entradainfo.Field<int>("EmbarqueID"),
                                                                  FolioOrigen = entradainfo.Field<int>("FolioOrigen"),
                                                                  FechaSalida = entradainfo.Field<DateTime>("FechaSalida"),
                                                                  ChoferID = entradainfo.Field<int>("ChoferID"),
                                                                  JaulaID = entradainfo.Field<int>("JaulaID"),
                                                                  CamionID = entradainfo.Field<int>("CamionID"),
                                                                  CabezasOrigen = entradainfo.Field<int>("CabezasOrigen"),
                                                                  CabezasRecibidas = entradainfo.Field<int>("CabezasRecibidas"),
                                                                  OperadorID = entradainfo.Field<int>("OperadorID"),
                                                                  PesoBruto = entradainfo.Field<decimal>("PesoBruto"),
                                                                  PesoTara = entradainfo.Field<decimal>("PesoTara"),
                                                                  EsRuteo = entradainfo.Field<bool>("EsRuteo"),
                                                                  Fleje = entradainfo.Field<bool>("Fleje"),
                                                                  CheckList = entradainfo.Field<string>("CheckList"),
                                                                  CorralID = entradainfo["CorralID"] == DBNull.Value ? 0 : entradainfo.Field<int>("CorralID"),
                                                                  Lote = new LoteInfo
                                                                  {
                                                                      LoteID = entradainfo["LoteID"] == DBNull.Value ? 0 : entradainfo.Field<int>("LoteID"),
                                                                      Lote = entradainfo["Lote"] == DBNull.Value ? string.Empty : entradainfo.Field<string>("Lote"),
                                                                  },
                                                                  Observacion = entradainfo.Field<string>("Observacion"),
                                                                  ImpresionTicket = entradainfo.Field<bool>("ImpresionTicket"),
                                                                  Activo = entradainfo.Field<bool>("Activo").BoolAEnum(),
                                                                  OrganizacionOrigen = entradainfo.Field<string>("OrganizacionOrigen"),
                                                                  CodigoCorral = entradainfo.Field<string>("CodigoCorral"),
                                                                  PesoOrigen = entradainfo["PesoOrigen"] == DBNull.Value ? 0 : entradainfo.Field<int>("PesoOrigen")
                                                              }).ToList();
                resultado = new ResultadoInfo<EntradaGanadoInfo>
                {
                    Lista = listaEntradaGanado,
                    TotalRegistros = Convert.ToInt32(listaEntradaGanado.Count())
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
        /// Obtiene una Entrada de Ganado por Folio de Entrada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerEntradaGanadoCapturaCalidad(DataSet ds)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo["CorralID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("CorralID"),
                                         Lote = new LoteInfo
                                         {
                                             LoteID = entradaInfo["LoteID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("LoteID")
                                         },
                                         Observacion = entradaInfo.Field<string>("Observacion"),
                                         ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                         Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                         TipoOrganizacionOrigen = entradaInfo.Field<string>("TipoOrganizacion"),
                                         TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID"),
                                         OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                         CodigoCorral = entradaInfo.Field<string>("Corral"),
                                         CodigoLote = entradaInfo.Field<string>("Lote"),
                                         Operador = entradaInfo.Field<string>("NombreOperador"),
                                         Proveedor = entradaInfo.Field<string>("Proveedor"),
                                         ProveedorID = entradaInfo.Field<int>("ProveedorID"),
                                         MensajeRetornoCalificacion = entradaInfo.Field<int>("MensajeRetorno"),
                                         EstatusEmbarque = entradaInfo.Field<int>("Estatus")
                                     }).First();

                if (entradaGanadoInfo != null)
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    entradaGanadoInfo.ListaCondicionGanado = (from entrada in dt.AsEnumerable()
                                                              select new EntradaCondicionInfo
                                                              {
                                                                  Activo = entrada.Field<bool>("Activo").BoolAEnum()
                                                                  ,
                                                                  Cabezas = entrada.Field<int>("Cabezas")
                                                                  ,
                                                                  CondicionID = entrada.Field<int>("CondicionID")
                                                                  ,
                                                                  EntradaGanadoID = entrada.Field<int>("EntradaGanadoID")
                                                                  ,
                                                                  EntradaCondicionID = entrada.Field<int>("EntradaCondicionID")
                                                                  ,
                                                                  CondicionDescripcion = entrada.Field<string>("Descripcion")
                                                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Valida si un corral esta disponible
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerPorCorralDisponible(DataSet ds)
        {
            try
            {
                Logger.Info();
                int result = Convert.ToInt32(ds.Tables[ConstantesDAL.DtDatos].Rows[0]["TotalRegistros"]);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una Entrada de Ganado por Folio de Entrada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerEntradasGanadoRecibidas(DataSet ds)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo["CorralID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("CorralID"),
                                         Lote = new LoteInfo
                                         {
                                             LoteID = entradaInfo["LoteID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("LoteID")
                                         },
                                         Observacion = entradaInfo.Field<string>("Observacion"),
                                         ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                         Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                         TipoOrganizacionOrigen = entradaInfo.Field<string>("TipoOrganizacion"),
                                         TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID"),
                                         OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                         CodigoCorral = entradaInfo.Field<string>("Corral"),
                                         CodigoLote = entradaInfo.Field<string>("Lote"),
                                         Operador = entradaInfo.Field<string>("NombreOperador"),
                                         Proveedor = entradaInfo.Field<string>("Proveedor"),
                                         ProveedorID = entradaInfo.Field<int>("ProveedorID"),
                                         Guia = entradaInfo.Field<bool>("Guia"),
                                         Poliza = entradaInfo.Field<bool>("Poliza"),
                                         HojaEmbarque = entradaInfo.Field<bool>("HojaEmbarque"),
                                         Factura = entradaInfo.Field<bool>("Factura"),
                                         CabezasMuertas = entradaInfo.Field<int?>("CabezasMuertas") != null ? entradaInfo.Field<int>("CabezasMuertas") : 0
                                     }).First();

                if (entradaGanadoInfo != null)
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    entradaGanadoInfo.ListaCondicionGanado = (from entrada in dt.AsEnumerable()
                                                              select new EntradaCondicionInfo
                                                              {
                                                                  Activo = entrada.Field<bool>("Activo").BoolAEnum()
                                                                  ,
                                                                  Cabezas = entrada.Field<int>("Cabezas")
                                                                  ,
                                                                  CondicionID = entrada.Field<int>("CondicionID")
                                                                  ,
                                                                  EntradaGanadoID = entrada.Field<int>("EntradaGanadoID")
                                                                  ,
                                                                  EntradaCondicionID = entrada.Field<int>("EntradaCondicionID")
                                                                  ,
                                                                  CondicionDescripcion = entrada.Field<string>("Descripcion")
                                                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene una entrada por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerEntradaPorLote(DataSet ds)
        {
            EntradaGanadoInfo resultado = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from entradaInfo in dt.AsEnumerable()
                             select new EntradaGanadoInfo
                             {
                                 FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                 TipoOrganizacionOrigenId = entradaInfo.Field<int>("TipoOrganizacionID"),
                                 OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                 PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                 PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                 CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                 FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                 EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                 EsRuteo = entradaInfo.Field<bool>("EsRuteo")
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
        /// Obtener las entradas de ganado costeadas sin prorratear
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaGanadoInfo> ObtenerEntradasCosteadasSinProrratear(DataSet ds)
        {
            List<EntradaGanadoInfo> resultado = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from entradaInfo in dt.AsEnumerable()
                             select new EntradaGanadoInfo
                             {
                                 EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                 CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                 CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                 Lote = new LoteInfo
                                 {
                                     LoteID = entradaInfo.Field<int>("LoteID"),
                                     CabezasInicio = entradaInfo.Field<int>("CabezasInicio")
                                 },
                                 FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                 FolioEntradaAgrupado = entradaInfo.Field<string>("FolioEntradaAgrupado"),
                                 FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                 OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                 OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                 TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID")
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una entrada de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerPorFolioEntradaPorOrganizacion(DataSet ds)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo["CorralID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("CorralID"),
                                         Lote = new LoteInfo
                                         {
                                             LoteID = entradaInfo["LoteID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("LoteID"),
                                             Lote = entradaInfo["Lote"] == DBNull.Value ? string.Empty : entradaInfo.Field<string>("Lote"),
                                         },
                                         Observacion = entradaInfo.Field<string>("Observacion"),
                                         ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                         Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                         TipoOrganizacionOrigen = entradaInfo.Field<string>("TipoOrganizacion"),
                                         TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID"),
                                         OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                         CodigoCorral = entradaInfo.Field<string>("Corral"),
                                         CodigoLote = entradaInfo.Field<string>("Lote")
                                     }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerEntradasPorPaginaPoliza(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<EntradaGanadoInfo> lista = (from entradaInfo in dt.AsEnumerable()
                                                  select new EntradaGanadoInfo
                                                  {
                                                      FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                                      Observacion = entradaInfo.Field<string>("Descripcion"),
                                                      EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoId"),
                                                      FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                                      PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                                      PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                                      OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                                      CodigoCorral = entradaInfo.Field<string>("Corral"),
                                                      CodigoLote = entradaInfo.Field<string>("Lote")
                                                  }).ToList();
                resultado = new ResultadoInfo<EntradaGanadoInfo>
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
        /// Obtiene una lista de la entrada de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaGanadoInfo> ObtenerEntradasPorLoteXML(DataSet ds)
        {
            List<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         TipoOrganizacionOrigenId =
                                             entradaInfo.Field<int>("TipoOrganizacionID"),
                                         OrganizacionOrigenID =
                                             entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         LoteID = entradaInfo.Field<int>("LoteID"),
                                         CorralID = entradaInfo.Field<int>("CorralID")
                                     }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene una lista con las entradas de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaGanadoInfo> ObtenerEntradasPorIDs(DataSet ds)
        {
            List<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID =
                                             entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID =
                                             entradaInfo["CorralID"] == DBNull.Value
                                                 ? 0
                                                 : entradaInfo.Field<int>("CorralID"),
                                         Lote = new LoteInfo
                                         {
                                             LoteID =
                                                 entradaInfo["LoteID"] == DBNull.Value
                                                     ? 0
                                                     : entradaInfo.Field<int>("LoteID"),
                                             Lote =
                                                 entradaInfo["Lote"] == DBNull.Value
                                                     ? string.Empty
                                                     : entradaInfo.Field<string>("Lote"),
                                         },
                                         Observacion = entradaInfo.Field<string>("Observacion"),
                                         ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                         Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                         TipoOrganizacionOrigen =
                                             entradaInfo.Field<string>("TipoOrganizacion"),
                                         TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID"),
                                         OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                         CodigoCorral = entradaInfo.Field<string>("Corral"),
                                         CodigoLote = entradaInfo.Field<string>("Lote")
                                     }).ToList();

                if (entradaGanadoInfo != null)
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    entradaGanadoInfo.ForEach(datos =>
                    {
                        datos.ListaCondicionGanado =
                            (from entrada in dt.AsEnumerable()
                             where
                                 datos.EntradaGanadoID ==
                                 entrada.Field<int>("EntradaGanadoID")
                             select new EntradaCondicionInfo
                             {
                                 Activo =
                                     entrada.Field<bool>("Activo").BoolAEnum()
                                 ,
                                 Cabezas = entrada.Field<int>("Cabezas")
                                 ,
                                 CondicionID =
                                     entrada.Field<int>("CondicionID")
                                 ,
                                 EntradaGanadoID =
                                     entrada.Field<int>("EntradaGanadoID")
                                 ,
                                 EntradaCondicionID =
                                     entrada.Field<int>("EntradaCondicionID")
                                 ,
                                 CondicionDescripcion =
                                     entrada.Field<string>("Descripcion")
                             }).ToList();
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        internal static List<EntradaGanadoInfo> ObtenerEntradasPorEmbarqueID(DataSet ds)
        {
            List<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID =
                                             entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo.Field<int?>("CorralID") ?? 0,
                                         Lote = new LoteInfo
                                         {
                                             LoteID = entradaInfo.Field<int?>("LoteID") ?? 0,
                                             Lote = entradaInfo.Field<string>("Lote")
                                         },
                                         Observacion = entradaInfo.Field<string>("Observacion"),
                                         ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                         Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                         TipoOrganizacionOrigen =
                                             entradaInfo.Field<string>("TipoOrganizacion"),
                                         TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID"),
                                         OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                         CodigoCorral = entradaInfo.Field<string>("Corral"),
                                         CodigoLote = entradaInfo.Field<string>("Lote")
                                     }).ToList();
                if (entradaGanadoInfo != null)
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    entradaGanadoInfo.ForEach(datos =>
                    {
                        datos.ListaCondicionGanado =
                            (from entrada in dt.AsEnumerable()
                             where
                                 datos.EntradaGanadoID ==
                                 entrada.Field<int>("EntradaGanadoID")
                             select new EntradaCondicionInfo
                             {
                                 Activo =
                                     entrada.Field<bool>("Activo").BoolAEnum()
                                 ,
                                 Cabezas = entrada.Field<int>("Cabezas")
                                 ,
                                 CondicionID =
                                     entrada.Field<int>("CondicionID")
                                 ,
                                 EntradaGanadoID =
                                     entrada.Field<int>("EntradaGanadoID")
                                 ,
                                 EntradaCondicionID =
                                     entrada.Field<int>("EntradaCondicionID")
                                 ,
                                 CondicionDescripcion =
                                     entrada.Field<string>("Descripcion")
                             }).ToList();
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene una lista de entradas de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaGanadoInfo> ObtenerEntradasPorFolioOrigenXML(DataSet ds)
        {
            List<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID =
                                             entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo.Field<int?>("CorralID") ?? 0,
                                         Lote = new LoteInfo
                                         {
                                             LoteID = entradaInfo.Field<int?>("LoteID") ?? 0,
                                             Lote = entradaInfo.Field<string>("Lote")
                                         },
                                         Observacion = entradaInfo.Field<string>("Observacion"),
                                         ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                         Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                         TipoOrganizacionOrigen =
                                             entradaInfo.Field<string>("TipoOrganizacion"),
                                         TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID"),
                                         OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                         CodigoCorral = entradaInfo.Field<string>("Corral"),
                                         CodigoLote = entradaInfo.Field<string>("Lote")
                                     }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene una lista de entradas de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ImpresionTarjetaRecepcionModel> ObtenerEntradasImpresionTarjetaRecepcion(DataSet ds)
        {
            List<ImpresionTarjetaRecepcionModel> entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new ImpresionTarjetaRecepcionModel
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<string>("FolioEntrada"),
                                         Vigilante = entradaInfo.Field<string>("Vigilante"),
                                         HoraVigilancia = entradaInfo.Field<string>("HoraVigilancia"),
                                         Origen = entradaInfo.Field<string>("Origen"),
                                         Operador = entradaInfo.Field<string>("Operador"),
                                         PlacaJaula = entradaInfo.Field<string>("PlacaJaula"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         PesoNeto = entradaInfo.Field<decimal>("PesoNeto"),
                                         HoraBascula = entradaInfo.Field<string>("HoraBascula"),
                                         UsuarioRecibio = entradaInfo.Field<string>("UsuarioRecibio"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         CabezasCorral = entradaInfo.Field<int>("CabezasCorral"),
                                         Corral = entradaInfo.Field<string>("Corral"),
                                         ManejoSinEstres = entradaInfo.Field<bool>("ManejoSinEstres"),
                                         Guia = entradaInfo.Field<bool>("Guia"),
                                         Factura = entradaInfo.Field<bool>("Factura"),
                                         Poliza = entradaInfo.Field<bool>("Poliza"),
                                         HojaEmbarque = entradaInfo.Field<bool>("HojaEmbarque")
                                     }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene una lista de entradas de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ImpresionCalidadGanadoModel> ObtenerEntradasImpresionCalidadGanado(DataSet ds)
        {
            List<ImpresionCalidadGanadoModel> entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtCalidad = ds.Tables[ConstantesDAL.DtDetalle];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new ImpresionCalidadGanadoModel
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<string>("FolioEntrada"),
                                         Proveedor = entradaInfo.Field<string>("Proveedor"),
                                         Corral = entradaInfo.Field<string>("Corral"),
                                         FechaCalificacion = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         ListaCalidadGanado = (from calidad in dtCalidad.AsEnumerable()
                                                               where calidad.Field<int>("EntradaGanadoID") == entradaInfo.Field<int>("EntradaGanadoID")
                                                               select new EntradaGanadoCalidadInfo
                                                               {
                                                                   EntradaGanadoCalidadID = calidad.Field<int>("EntradaGanadoCalidadID"),
                                                                   CalidadGanado =
                                                                       new CalidadGanadoInfo
                                                                       {
                                                                           CalidadGanadoID = calidad.Field<int>("CalidadGanadoID"),
                                                                           Descripcion = calidad.Field<string>("CalidadGanado"),
                                                                           Sexo = Convert.ToChar(calidad.Field<string>("Sexo")) == 'M'
                                                                                      ? Sexo.Macho
                                                                                      : Sexo.Hembra,
                                                                           Calidad = calidad.Field<string>("Calidad")
                                                                       },
                                                                   Valor = calidad.Field<int>("Valor"),
                                                                   EntradaGanadoID = calidad.Field<int>("EntradaGanadoID"),
                                                                   Activo = calidad.Field<bool>("Activo").BoolAEnum(),
                                                               }).ToList()
                                     }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtener las entradas de ganado costeadas sin prorratear
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaGanadoInfo> ObtenerEntradasPorCorralLote(DataSet ds)
        {
            List<EntradaGanadoInfo> resultado = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from entradaInfo in dt.AsEnumerable()
                             select new EntradaGanadoInfo
                             {
                                 EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                 Lote = new LoteInfo
                                 {
                                     LoteID = entradaInfo.Field<int>("LoteID"),
                                     CabezasInicio = entradaInfo.Field<int>("CabezasInicio")
                                 },
                                 FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                 FolioEntradaAgrupado = entradaInfo.Field<string>("FolioEntradaAgrupado"),
                                 FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                 OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                 OrganizacionID = entradaInfo.Field<int>("OrganizacionID")
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una Entrada de Ganado por Folio de Entrada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerPorFolioEntradaCortadaIncompleta(DataSet ds)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         EmbarqueID = entradaInfo.Field<int>("EmbarqueID"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo["CorralID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("CorralID"),
                                         Lote = new LoteInfo
                                         {
                                             LoteID = entradaInfo["LoteID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("LoteID")
                                         },
                                         Observacion = entradaInfo.Field<string>("Observacion"),
                                         ImpresionTicket = entradaInfo.Field<bool>("ImpresionTicket"),
                                         Activo = entradaInfo.Field<bool>("Activo").BoolAEnum(),
                                         TipoOrganizacionOrigen = entradaInfo.Field<string>("TipoOrganizacion"),
                                         TipoOrigen = entradaInfo.Field<int>("TipoOrganizacionID"),
                                         OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                         CodigoCorral = entradaInfo.Field<string>("Corral"),
                                         CodigoLote = entradaInfo.Field<string>("Lote"),
                                         Operador = entradaInfo.Field<string>("NombreOperador"),
                                         Proveedor = entradaInfo.Field<string>("Proveedor"),
                                         ProveedorID = entradaInfo.Field<int>("ProveedorID"),
                                         Guia = entradaInfo.Field<bool>("Guia"),
                                         Poliza = entradaInfo.Field<bool>("Poliza"),
                                         HojaEmbarque = entradaInfo.Field<bool>("HojaEmbarque"),
                                         Factura = entradaInfo.Field<bool>("Factura"),
                                         ManejoSinEstres = entradaInfo.Field<bool>("ManejoSinEstres"),
                                         CertificadoZoosanitario = entradaInfo.Field<string>("CertificadoZoosanitario"),
                                         PruebasTB = entradaInfo.Field<string>("PruebaTB"),
                                         PruebasTR = entradaInfo.Field<string>("PruebaTR"),
                                         CondicionJaula = new CondicionJaulaInfo
                                         {
                                             CondicionJaulaID = entradaInfo.Field<int?>("CondicionJaulaID") ?? 0
                                         }
                                     }).First();

                if (entradaGanadoInfo != null)
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    entradaGanadoInfo.ListaCondicionGanado = (from entrada in dt.AsEnumerable()
                                                              select new EntradaCondicionInfo
                                                              {
                                                                  Activo = entrada.Field<bool>("Activo").BoolAEnum()
                                                                  ,
                                                                  Cabezas = entrada.Field<int>("Cabezas")
                                                                  ,
                                                                  CondicionID = entrada.Field<int>("CondicionID")
                                                                  ,
                                                                  EntradaGanadoID = entrada.Field<int>("EntradaGanadoID")
                                                                  ,
                                                                  EntradaCondicionID = entrada.Field<int>("EntradaCondicionID")
                                                                  ,
                                                                  CondicionDescripcion = entrada.Field<string>("Descripcion")
                                                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene la Entrada de Ganado por Pagina
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoPaginaCortadasIncompletas(DataSet ds)
        {
            ResultadoInfo<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<EntradaGanadoInfo> lista = (from entradaInfo in dt.AsEnumerable()
                                                  select new EntradaGanadoInfo
                                                  {
                                                      FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                                      FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                                      OrganizacionOrigen = entradaInfo.Field<string>("OrganizacionOrigen"),
                                                      EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoId"),
                                                      OrganizacionID = entradaInfo.Field<int>("OrganizacionID")
                                                  }).ToList();
                resultado = new ResultadoInfo<EntradaGanadoInfo>
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
        /// Obtiene los Aretes de la partida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<FiltroAnimalesReemplazoArete> ObtenerReemplazoAretes(DataSet ds)
        {
            List<FiltroAnimalesReemplazoArete> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from entradaInfo in dt.AsEnumerable()
                     select new FiltroAnimalesReemplazoArete
                                                  {
                                                      CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                                      CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                                      AreteCentro = entradaInfo.Field<string>("AreteCentro"),
                                                      AreteCorte = entradaInfo.Field<string>("AreteCorte"),
                                                      PesoOrigen = Convert.ToInt32(entradaInfo.Field<decimal>("PesoCompra")),
                                                      FolioEntradaCentro = entradaInfo.Field<int>("FolioEntradaCentro"),
                                                      FolioEntradaCorte = entradaInfo.Field<long?>("FolioEntradaCorte") != null ? entradaInfo.Field<long>("FolioEntradaCorte") : 0,
                                                      AreteMetalicoCentro = entradaInfo.Field<string>("AreteMetalicoCentro") ?? string.Empty,
                                                      AreteMetalicoCorte = entradaInfo.Field<string>("AreteMetalicoCorte") ?? string.Empty,
                                                      AnimalID = entradaInfo.Field<long?>("AnimalID") != null ? entradaInfo.Field<long>("AnimalID") : 0,
                                                  }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una entrada por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<CabezasPartidasModel> ObtenerCabezasEntradasRuteo(DataSet ds)
        {
            List<CabezasPartidasModel> resultado = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from entradaInfo in dt.AsEnumerable()
                             select new CabezasPartidasModel
                             {
                                 OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                 FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                 FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                 CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                 CabezasCortadas = entradaInfo.Field<int>("CabezasCortadas"),
                                 CabezasHistorico = entradaInfo.Field<int>("CabezasHistorico"),
                                 CabezasTotal = entradaInfo.Field<int>("CabezasTotal"),
                                 CabezasPendientes = entradaInfo.Field<int>("CabezasPendientes"),
                                 CabezasLote = entradaInfo.Field<int>("CabezasLote"),
                                 EmbarqueID = entradaInfo.Field<int>("EmbarqueID")
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Mapeo para Obtener entrada ganado por loteID y corralID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerEntradaGanadoLoteCorral(DataSet ds)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                entradaGanadoInfo = (from entradaInfo in dt.AsEnumerable()
                                     select new EntradaGanadoInfo
                                     {
                                         EntradaGanadoID = entradaInfo.Field<int>("EntradaGanadoID"),
                                         FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                         OrganizacionID = entradaInfo.Field<int>("OrganizacionID"),
                                         OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionOrigenID"),
                                         FechaEntrada = entradaInfo.Field<DateTime>("FechaEntrada"),
                                         FolioOrigen = entradaInfo.Field<int>("FolioOrigen"),
                                         FechaSalida = entradaInfo.Field<DateTime>("FechaSalida"),
                                         ChoferID = entradaInfo.Field<int>("ChoferID"),
                                         JaulaID = entradaInfo.Field<int>("JaulaID"),
                                         CamionID = entradaInfo.Field<int>("CamionID"),
                                         CabezasOrigen = entradaInfo.Field<int>("CabezasOrigen"),
                                         CabezasRecibidas = entradaInfo.Field<int>("CabezasRecibidas"),
                                         OperadorID = entradaInfo.Field<int>("OperadorID"),
                                         PesoBruto = entradaInfo.Field<decimal>("PesoBruto"),
                                         PesoTara = entradaInfo.Field<decimal>("PesoTara"),
                                         EsRuteo = entradaInfo.Field<bool>("EsRuteo"),
                                         Fleje = entradaInfo.Field<bool>("Fleje"),
                                         CheckList = entradaInfo.Field<string>("CheckList"),
                                         CorralID = entradaInfo["CorralID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("CorralID"),
                                         Lote = new LoteInfo
                                         {
                                             LoteID = entradaInfo["LoteID"] == DBNull.Value ? 0 : entradaInfo.Field<int>("LoteID")
                                         },
                                     }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoInfo;
        }
    }
}
 