using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapInterfaceSalidaTraspasoDAL
    {
        /// <summary>
        /// Mapea los datos de la InterfaceSalidaTraspaso por su identificador.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static InterfaceSalidaTraspasoInfo ObtenerPorId(DataSet ds)
        {
            InterfaceSalidaTraspasoInfo result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                result = (from info in dt.AsEnumerable()
                          select new InterfaceSalidaTraspasoInfo
                         {
                             InterfaceSalidaTraspasoId = info.Field<int>("InterfaceSalidaTraspasoID"),
                             OrganizacionId = info.Field<int>("OrganizacionID"),
                             OrganizacionDestino = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionIDDestino") },
                             FolioTraspaso = info.Field<int>("FolioTraspaso"),
                             CabezasEnvio = info.Field<int>("CabezasEnvio"),
                             FechaEnvio = info["FechaEnvio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaEnvio"),
                             PesoTara = info.Field<int?>("PesoTara") != null ? info.Field<int>("PesoTara") : 0,
                             PesoBruto = info.Field<int?>("PesoBruto") != null ? info.Field<int>("PesoBruto") : 0,
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }
        /// <summary>
        /// Mapea los datos de la lista obtenida de la salida traspaso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaTraspasoInfo> ObtenerListaInterfaceSalidaTraspaso(DataSet ds)
        {
            List<InterfaceSalidaTraspasoInfo> result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                result = (from info in dt.AsEnumerable()
                          select new InterfaceSalidaTraspasoInfo
                          {
                              OrganizacionId = info.Field<int>("OrganizacionID"),
                              OrganizacionDestino = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionIDDestino") },
                              FolioTraspaso = info.Field<int>("FolioTraspaso"),
                              Corral = info.Field<string>("Corral"),
                              Lote = info.Field<string>("Lote"),
                              TipoGanado = new TipoGanadoInfo { TipoGanadoID = info.Field<int>("TipoGanadoID") },
                              PesoProyectado = info.Field<int>("PesoProyectado"),
                              GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                              DiasEngorda = info.Field<int>("DiasEngorda"),
                              Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID") },
                              DiasFormula = info.Field<int>("DiasFormula"),
                              Cabezas = info.Field<int>("Cabezas"),
                              FechaEnvio = info["FechaEnvio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaEnvio"),
                              Activo = info.Field<bool>("Activo").BoolAEnum(),
                              Registrado = true
                          }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Mapea los datos de la lista obtenida de la salida traspaso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static InterfaceSalidaTraspasoInfo ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(DataSet ds)
        {
            InterfaceSalidaTraspasoInfo result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                result = (from info in dt.AsEnumerable()
                          select new InterfaceSalidaTraspasoInfo
                                     {
                                         InterfaceSalidaTraspasoId = info.Field<int>("InterfaceSalidaTraspasoID"),
                                         OrganizacionId = info.Field<int>("OrganizacionID"),
                                         OrganizacionDestino =
                                             new OrganizacionInfo
                                                 {
                                                     OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                                     Descripcion = info.Field<string>("Organizacion")
                                                 },
                                         FolioTraspaso = info.Field<int>("FolioTraspaso"),
                                         FechaEnvio = info.Field<DateTime?>("FechaEnvio") ?? new DateTime(),
                                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                                         CabezasEnvio = info.Field<int>("CabezasEnvio"),
                                         TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                                         SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                                         PesoTara =
                                             info.Field<int?>("PesoTara") != null ? info.Field<int>("PesoTara") : 0,
                                         PesoBruto =
                                             info.Field<int?>("PesoBruto") != null ? info.Field<int>("PesoBruto") : 0,

                                         ListaInterfaceSalidaTraspasoDetalle = (from detalle in dtDetalle.AsEnumerable()
                                                                                select new InterfaceSalidaTraspasoDetalleInfo
                                                                                           {
                                                                                               InterfaceSalidaTraspasoID = info.Field<int>("InterfaceSalidaTraspasoID"),
                                                                                               InterfaceSalidaTraspasoDetalleID = detalle.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                                                                               OrganizacionDestino =
                                                                                                            new OrganizacionInfo
                                                                                                            {
                                                                                                                OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                                                                                                Descripcion = info.Field<string>("Organizacion")
                                                                                                            },
                                                                                               Lote = new LoteInfo
                                                                                                          {
                                                                                                              LoteID = detalle.Field<int>("LoteID"),
                                                                                                              Lote = detalle.Field<string>("Lote")
                                                                                                          },
                                                                                               Corral = new CorralInfo
                                                                                                            {
                                                                                                                CorralID = detalle.Field<int>("CorralID"),
                                                                                                                Codigo = detalle.Field<string>("Codigo")
                                                                                                            },
                                                                                               TipoGanado = new TipoGanadoInfo
                                                                                                                {
                                                                                                                    TipoGanadoID = detalle.Field<int>("TipoGanadoID"),
                                                                                                                    Descripcion = detalle.Field<string>("TipoGanado"),
                                                                                                                    PesoSalida = detalle.Field<int>("PesoSalida")
                                                                                                                },
                                                                                               PesoProyectado = detalle.Field<int>("PesoProyectado"),
                                                                                               GananciaDiaria = detalle.Field<decimal>("GananciaDiaria"),
                                                                                               DiasEngorda = detalle.Field<int>("DiasEngorda"),
                                                                                               Formula = new FormulaInfo
                                                                                                             {
                                                                                                                 FormulaId = detalle.Field<int>("FormulaID"),
                                                                                                                 Descripcion = detalle.Field<string>("Formula")
                                                                                                             },
                                                                                               DiasFormula = detalle.Field<int>("DiasFormula"),
                                                                                               Cabezas = detalle.Field<int>("Cabezas"),
                                                                                               TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                                                                                               SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                                                                                               Registrado = true
                                                                                           }).ToList()


                                     }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Obtiene los datos de la interface salida traspaso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static InterfaceSalidaTraspasoInfo ObtenerDatosInterfaceSalidaTraspaso(DataSet ds)
        {
            InterfaceSalidaTraspasoInfo result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                result = (from info in dt.AsEnumerable()
                          select new InterfaceSalidaTraspasoInfo
                          {
                              InterfaceSalidaTraspasoId = info.Field<int>("InterfaceSalidaTraspasoID"),
                              OrganizacionId = info.Field<int>("OrganizacionID"),
                              OrganizacionDestino =
                                  new OrganizacionInfo
                                  {
                                      OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                      Descripcion = info.Field<string>("Organizacion")
                                  },
                              FolioTraspaso = info.Field<int>("FolioTraspaso"),
                              FechaEnvio = info.Field<DateTime?>("FechaEnvio") ?? new DateTime(),
                              CabezasEnvio = info.Field<int>("CabezasEnvio"),
                              TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                              SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                              PesoTara =
                                  info.Field<int?>("PesoTara") ?? 0,
                              PesoBruto =
                                  info.Field<int?>("PesoBruto") ?? 0,
                              ListaInterfaceSalidaTraspasoDetalle = (from detalle in dtDetalle.AsEnumerable()
                                                                     select new InterfaceSalidaTraspasoDetalleInfo
                                                                     {
                                                                         InterfaceSalidaTraspasoID = info.Field<int>("InterfaceSalidaTraspasoID"),
                                                                         InterfaceSalidaTraspasoDetalleID = detalle.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                                                         OrganizacionDestino =
                                                                                      new OrganizacionInfo
                                                                                      {
                                                                                          OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                                                                          Descripcion = info.Field<string>("Organizacion")
                                                                                      },
                                                                         Lote = new LoteInfo
                                                                         {
                                                                             LoteID = detalle.Field<int>("LoteID"),
                                                                         },
                                                                         TipoGanado = new TipoGanadoInfo
                                                                         {
                                                                             TipoGanadoID = detalle.Field<int>("TipoGanadoID"),
                                                                         },
                                                                         PesoProyectado = detalle.Field<int>("PesoProyectado"),
                                                                         GananciaDiaria = detalle.Field<decimal>("GananciaDiaria"),
                                                                         DiasEngorda = detalle.Field<int>("DiasEngorda"),
                                                                         Formula = new FormulaInfo
                                                                         {
                                                                             FormulaId = detalle.Field<int>("FormulaID"),
                                                                         },
                                                                         DiasFormula = detalle.Field<int>("DiasFormula"),
                                                                         Cabezas = detalle.Field<int>("Cabezas"),
                                                                         TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                                                                         SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                                                                         Registrado = true
                                                                     }).ToList()
                          }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Mapea los datos de la lista obtenida de la salida traspaso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static InterfaceSalidaTraspasoInfo ObtenerInterfaceSalidaTraspasoPorLote(DataSet ds)
        {
            InterfaceSalidaTraspasoInfo result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                result = (from info in dt.AsEnumerable()
                          select new InterfaceSalidaTraspasoInfo
                          {
                              InterfaceSalidaTraspasoId = info.Field<int>("InterfaceSalidaTraspasoID"),
                              OrganizacionId = info.Field<int>("OrganizacionID"),
                              OrganizacionDestino =
                                  new OrganizacionInfo
                                  {
                                      OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                      Descripcion = info.Field<string>("Organizacion")
                                  },
                              FolioTraspaso = info.Field<int>("FolioTraspaso"),
                              FechaEnvio = info.Field<DateTime?>("FechaEnvio") ?? new DateTime(),
                              Activo = info.Field<bool>("Activo").BoolAEnum(),
                              CabezasEnvio = info.Field<int>("CabezasEnvio"),
                              TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                              SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                              PesoTara =
                                  info.Field<int?>("PesoTara") != null ? info.Field<int>("PesoTara") : 0,
                              PesoBruto =
                                  info.Field<int?>("PesoBruto") != null ? info.Field<int>("PesoBruto") : 0,

                              ListaInterfaceSalidaTraspasoDetalle = (from detalle in dtDetalle.AsEnumerable()
                                                                     select new InterfaceSalidaTraspasoDetalleInfo
                                                                     {
                                                                         InterfaceSalidaTraspasoID = info.Field<int>("InterfaceSalidaTraspasoID"),
                                                                         InterfaceSalidaTraspasoDetalleID = detalle.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                                                         OrganizacionDestino =
                                                                                      new OrganizacionInfo
                                                                                      {
                                                                                          OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                                                                          Descripcion = info.Field<string>("Organizacion")
                                                                                      },
                                                                         Lote = new LoteInfo
                                                                         {
                                                                             LoteID = detalle.Field<int>("LoteID"),
                                                                             Lote = detalle.Field<string>("Lote")
                                                                         },
                                                                         Corral = new CorralInfo
                                                                         {
                                                                             CorralID = detalle.Field<int>("CorralID"),
                                                                             Codigo = detalle.Field<string>("Codigo")
                                                                         },
                                                                         TipoGanado = new TipoGanadoInfo
                                                                         {
                                                                             TipoGanadoID = detalle.Field<int>("TipoGanadoID"),
                                                                             Descripcion = detalle.Field<string>("TipoGanado"),
                                                                             PesoSalida = detalle.Field<int>("PesoSalida")
                                                                         },
                                                                         PesoProyectado = detalle.Field<int>("PesoProyectado"),
                                                                         GananciaDiaria = detalle.Field<decimal>("GananciaDiaria"),
                                                                         DiasEngorda = detalle.Field<int>("DiasEngorda"),
                                                                         Formula = new FormulaInfo
                                                                         {
                                                                             FormulaId = detalle.Field<int>("FormulaID"),
                                                                             Descripcion = detalle.Field<string>("Formula")
                                                                         },
                                                                         DiasFormula = detalle.Field<int>("DiasFormula"),
                                                                         Cabezas = detalle.Field<int>("Cabezas"),
                                                                         TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                                                                         SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                                                                         Registrado = true
                                                                     }).ToList()


                          }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Obtiene los datos de facturacion para los
        /// lotes de traspaso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaTraspasoInfo> ObtenerInterfaceSalidaTraspasoPorLotes(DataSet ds)
        {
            List<InterfaceSalidaTraspasoInfo> result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                result = (from info in dt.AsEnumerable()
                          select new InterfaceSalidaTraspasoInfo
                          {
                              InterfaceSalidaTraspasoId = info.Field<int>("InterfaceSalidaTraspasoID"),
                              OrganizacionId = info.Field<int>("OrganizacionID"),
                              OrganizacionDestino =
                                  new OrganizacionInfo
                                  {
                                      OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                  },
                              FolioTraspaso = info.Field<int>("FolioTraspaso"),
                              FechaEnvio = info.Field<DateTime?>("FechaEnvio") ?? new DateTime(),
                              CabezasEnvio = info.Field<int>("CabezasEnvio"),
                              TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                              SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                              PesoTara = info.Field<int?>("PesoTara") ?? 0,
                              PesoBruto = info.Field<int?>("PesoBruto") ?? 0,
                          }).ToList();

                List<InterfaceSalidaTraspasoDetalleInfo> detalles = (from info in dtDetalle.AsEnumerable()
                                                                     select new InterfaceSalidaTraspasoDetalleInfo
                                                                     {
                                                                         InterfaceSalidaTraspasoID = info.Field<int>("InterfaceSalidaTraspasoID"),
                                                                         InterfaceSalidaTraspasoDetalleID = info.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                                                         OrganizacionDestino =
                                                                                      new OrganizacionInfo
                                                                                      {
                                                                                          OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                                                                      },
                                                                         Lote = new LoteInfo
                                                                         {
                                                                             LoteID = info.Field<int>("LoteID"),
                                                                         },
                                                                         Corral = new CorralInfo
                                                                         {
                                                                             CorralID = info.Field<int>("CorralID"),
                                                                         },
                                                                         TipoGanado = new TipoGanadoInfo
                                                                         {
                                                                             TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                                         },
                                                                         PesoProyectado = info.Field<int>("PesoProyectado"),
                                                                         GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                                                                         DiasEngorda = info.Field<int>("DiasEngorda"),
                                                                         Formula = new FormulaInfo
                                                                         {
                                                                             FormulaId = info.Field<int>("FormulaID"),
                                                                         },
                                                                         DiasFormula = info.Field<int>("DiasFormula"),
                                                                         Cabezas = info.Field<int>("Cabezas"),
                                                                         TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                                                                         SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                                                                     }).ToList();
                result.ForEach(datos =>
                                   {
                                       datos.ListaInterfaceSalidaTraspasoDetalle =
                                           detalles.Where(
                                               id => id.InterfaceSalidaTraspasoID == datos.InterfaceSalidaTraspasoId).
                                               ToList();
                                   });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista de traspasos para conciliar
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaTraspasoInfo> ObtenerInterfaceSalidaTraspasoConciliacion(DataSet ds)
        {
            List<InterfaceSalidaTraspasoInfo> result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                result = (from info in dt.AsEnumerable()
                          select new InterfaceSalidaTraspasoInfo
                          {
                              InterfaceSalidaTraspasoId = info.Field<int>("InterfaceSalidaTraspasoID"),
                              OrganizacionId = info.Field<int>("OrganizacionID"),
                              OrganizacionDestino =
                                  new OrganizacionInfo
                                  {
                                      OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                  },
                              FolioTraspaso = info.Field<int>("FolioTraspaso"),
                              FechaEnvio = info.Field<DateTime?>("FechaEnvio") ?? new DateTime(),
                              CabezasEnvio = info.Field<int>("CabezasEnvio"),
                              TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                              SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                              PesoTara = info.Field<int?>("PesoTara") ?? 0,
                              PesoBruto = info.Field<int?>("PesoBruto") ?? 0,
                          }).ToList();

                List<InterfaceSalidaTraspasoDetalleInfo> detalles = (from info in dtDetalle.AsEnumerable()
                                                                     select new InterfaceSalidaTraspasoDetalleInfo
                                                                     {
                                                                         InterfaceSalidaTraspasoID = info.Field<int>("InterfaceSalidaTraspasoID"),
                                                                         InterfaceSalidaTraspasoDetalleID = info.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                                                         OrganizacionDestino =
                                                                                      new OrganizacionInfo
                                                                                      {
                                                                                          OrganizacionID = info.Field<int>("OrganizacionIDDestino"),
                                                                                      },
                                                                         Lote = new LoteInfo
                                                                         {
                                                                             LoteID = info.Field<int>("LoteID"),
                                                                         },
                                                                         Corral = new CorralInfo
                                                                         {
                                                                             CorralID = info.Field<int>("CorralID"),
                                                                         },
                                                                         TipoGanado = new TipoGanadoInfo
                                                                         {
                                                                             TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                                         },
                                                                         PesoProyectado = info.Field<int>("PesoProyectado"),
                                                                         GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                                                                         DiasEngorda = info.Field<int>("DiasEngorda"),
                                                                         Formula = new FormulaInfo
                                                                         {
                                                                             FormulaId = info.Field<int>("FormulaID"),
                                                                         },
                                                                         DiasFormula = info.Field<int>("DiasFormula"),
                                                                         Cabezas = info.Field<int>("Cabezas"),
                                                                         TraspasoGanado = info.Field<bool>("TraspasoGanado"),
                                                                         SacrificioGanado = info.Field<bool>("SacrificioGanado"),
                                                                     }).ToList();
                result.ForEach(datos =>
                {
                    datos.ListaInterfaceSalidaTraspasoDetalle =
                        detalles.Where(
                            id => id.InterfaceSalidaTraspasoID == datos.InterfaceSalidaTraspasoId).
                            ToList();
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
    }
}
 