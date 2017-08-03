using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Base.Infos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapLoteDAL
    {
        /// <summary>
        /// Obtiene una Lista De Lotes en Base al Conjunto
        /// de Resultados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<LoteInfo> ObtenerTodos(DataSet ds)
        {
            List<LoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteInfo
                                    {
                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                                        Cabezas = info.Field<int>("Cabezas"),
                                        CabezasInicio = info.Field<int>("CabezasInicio"),
                                        CorralID = info.Field<int>("CorralID"),
                                        DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                                        FechaCierre = info.Field<DateTime>("FechaCierre"),
                                        FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                        FechaDisponibilidad = info.Field<DateTime>("FechaDisponibilidad"),
                                        FechaInicio = info.Field<DateTime>("FechaInicio"),
                                        LoteID = info.Field<int>("LoteID"),
                                        OrganizacionID = info.Field<int>("OrganizacionID"),
                                        TipoCorralID = info.Field<int>("TipoCorralID"),
                                        TipoProcesoID = info.Field<int>("TipoProcesoID"),
                                        UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
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
        /// Obtiene un Lote en Base al Conjunto de Resultados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteInfo ObtenerPorCorral(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                                   {
                                       Activo = info.Field<bool>("Activo").BoolAEnum(),
                                       Cabezas = info.Field<int>("Cabezas"),
                                       CabezasInicio = info.Field<int>("CabezasInicio"),
                                       CorralID = info.Field<int>("CorralID"),
                                       DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                                       FechaCierre =
                                           (info["FechaCierre"] == DBNull.Value
                                                ? DateTime.Now
                                                : info.Field<DateTime>("FechaCierre")),
                                       FechaDisponibilidad =
                                           (info["FechaDisponibilidad"] == DBNull.Value
                                                ? DateTime.Now
                                                : info.Field<DateTime>("FechaDisponibilidad")),
                                       FechaInicio = info.Field<DateTime>("FechaInicio"),
                                       LoteID = info.Field<int>("LoteID"),
                                       OrganizacionID = info.Field<int>("OrganizacionID"),
                                       TipoCorralID = info.Field<int>("TipoCorralID"),
                                       TipoProcesoID = info.Field<int>("TipoProcesoID"),

                                   }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lote;
        }

        internal static LoteInfo ObtenerPorID(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                                   {
                                       Activo = info.Field<bool>("Activo").BoolAEnum(),
                                       Cabezas = info.Field<int>("Cabezas"),
                                       CabezasInicio = info.Field<int>("CabezasInicio"),
                                       CorralID = info.Field<int>("CorralID"),
                                       DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                                       FechaCierre =
                                           (info["FechaCierre"] == DBNull.Value
                                                ? DateTime.MinValue
                                                : info.Field<DateTime>("FechaCierre")),
                                       FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                       FechaDisponibilidad =
                                           (info["FechaDisponibilidad"] == DBNull.Value
                                                ? DateTime.MinValue
                                                : info.Field<DateTime>("FechaDisponibilidad")),
                                       FechaInicio = info.Field<DateTime>("FechaInicio"),
                                       LoteID = info.Field<int>("LoteID"),
                                       OrganizacionID = info.Field<int>("OrganizacionID"),
                                       TipoCorralID = info.Field<int>("TipoCorralID"),
                                       TipoProcesoID = info.Field<int>("TipoProcesoID"),
                                       UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                       Lote = info.Field<string>("Lote"),
                                       Corral = new CorralInfo
                                           {
                                               CorralID = info.Field<int>("CorralID"),
                                               Codigo = info.Field<string>("Codigo")
                                           }
                                   }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lote;
        }

        /// <summary>
        /// Obtiene un Objeto Lote por ID y Organizacion ID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteInfo ObtenerPorIDOrganizacionID(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                                   {
                                       Activo = info.Field<bool>("Activo").BoolAEnum(),
                                       Cabezas = info.Field<int>("Cabezas"),
                                       CabezasInicio = info.Field<int>("CabezasInicio"),
                                       CorralID = info.Field<int>("CorralID"),
                                       DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                                       FechaCierre =
                                           (info["FechaCierre"] == DBNull.Value
                                                ? DateTime.Now
                                                : info.Field<DateTime>("FechaCierre")),
                                       FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                       FechaDisponibilidad =
                                           (info["FechaDisponibilidad"] == DBNull.Value
                                                ? DateTime.Now
                                                : info.Field<DateTime>("FechaDisponibilidad")),
                                       FechaInicio = info.Field<DateTime>("FechaInicio"),
                                       LoteID = info.Field<int>("LoteID"),
                                       OrganizacionID = info.Field<int>("OrganizacionID"),
                                       TipoCorralID = info.Field<int>("TipoCorralID"),
                                       TipoProcesoID = info.Field<int>("TipoProcesoID"),
                                       UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                       Lote = info.Field<string>("Lote")
                                   }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lote;
        }

        /// <summary>
        /// Genera un Lote por Organizacion ID y Lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteInfo ObtenerPorOrganizacionIDLote(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                                   {
                                       Activo = info.Field<bool>("Activo").BoolAEnum(),
                                       Cabezas = info.Field<int>("Cabezas"),
                                       CabezasInicio = info.Field<int>("CabezasInicio"),
                                       CorralID = info.Field<int>("CorralID"),
                                       DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                                       FechaCierre =
                                           (info["FechaCierre"] == DBNull.Value
                                                ? new DateTime(1900, 1, 1)
                                                : info.Field<DateTime>("FechaCierre")),
                                       FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                       FechaDisponibilidad =
                                           (info["FechaDisponibilidad"] == DBNull.Value
                                                ? new DateTime(1900, 1, 1)
                                                : info.Field<DateTime>("FechaDisponibilidad")),
                                       FechaInicio = info.Field<DateTime>("FechaInicio"),
                                       LoteID = info.Field<int>("LoteID"),
                                       OrganizacionID = info.Field<int>("OrganizacionID"),
                                       TipoCorralID = info.Field<int>("TipoCorralID"),
                                       TipoProcesoID = info.Field<int>("TipoProcesoID"),
                                       UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                       Lote = info.Field<string>("Lote")
                                   }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lote;
        }
        /// <summary>
        /// Obtiene lote con capacidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteInfo ObtenerLote(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                        {
                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                            Cabezas = info.Field<int>("Cabezas"),
                            CabezasInicio = info.Field<int>("CabezasInicio"),
                            CorralID = info.Field<int>("CorralID"),
                            DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                            FechaCierre =
                                (info["FechaCierre"] == DBNull.Value
                                     ? new DateTime(1900, 1, 1)
                                     : info.Field<DateTime>("FechaCierre")),
                            FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                            FechaDisponibilidad =
                                (info["FechaDisponibilidad"] == DBNull.Value
                                     ? new DateTime(1900, 1, 1)
                                     : info.Field<DateTime>("FechaDisponibilidad")),
                            FechaInicio = info.Field<DateTime>("FechaInicio"),
                            LoteID = info.Field<int>("LoteID"),
                            OrganizacionID = info.Field<int>("OrganizacionID"),
                            TipoCorralID = info.Field<int>("TipoCorralID"),
                            TipoProcesoID = info.Field<int>("TipoProcesoID"),
                            UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                            Lote = info.Field<string>("Lote")
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lote;
        }

        /// <summary>
        /// Obtiene el Check List del Corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>CheckListCorralInfo</returns>
        internal static List<CheckListCorralInfo> ObtenerCheckListCorral(DataSet ds)
        {
            List<CheckListCorralInfo> checkList;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                checkList = (from info in dt.AsEnumerable()
                             select new CheckListCorralInfo
                                        {
                                            Corral = info.Field<string>("Corral"),
                                            Lote = info.Field<string>("Lote"),
                                            LoteID = info.Field<int>("LoteID"),
                                            CapacidadCabezas = info.Field<int>("CapacidadCabezas"),
                                            CabezasActuales = info.Field<int>("CabezasActuales"),
                                            CabezasRestantes = info.Field<int>("CabezasRestantes"),
                                            FechaInicio = info.Field<DateTime>("FechaInicio"),
                                            FechaFin = info.Field<DateTime>("FechaFin"),
                                            FechaCerrado =
                                                info.Field<DateTime?>("FechaCierre") != null
                                                    ? info.Field<DateTime>("FechaCierre")
                                                    : DateTime.MinValue
                                        }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return checkList;
        }

        /// <summary>
        /// Obtiene el Check List del Corral Completo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>CheckListCorralInfo</returns>
        internal static CheckListCorralInfo ObtenerCheckListCorralCompleto(DataSet ds)
        {
            CheckListCorralInfo checkList;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                checkList = (from info in dt.AsEnumerable()
                             select new CheckListCorralInfo
                                        {
                                            Corral = info.Field<string>("Corral").Trim(),
                                            Lote = info.Field<string>("Lote"),
                                            LoteID = info.Field<int>("LoteID"),
                                            CabezasSistema = info.Field<int>("CabezasSistema"),
                                            FechaAbierto = info.Field<DateTime>("FechaAbierto"),
                                            Tipo = info.Field<string>("TipoGanado"),
                                            PesoCorte = info.Field<int>("PesoCorte"),
                                            CapacidadCabezas = info.Field<int>("CapacidadCorral")
                                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return checkList;
        }

        /// <summary>
        /// Obtiene una lista de Disponibilidad Lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<DisponibilidadLoteInfo> ObtenerPorDisponibilidad(DataSet ds)
        {
            List<DisponibilidadLoteInfo> disponibilidadLotes;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                disponibilidadLotes = (from info in dt.AsEnumerable()
                                       select new DisponibilidadLoteInfo
                                       {
                                           Lote = info.Field<string>("Lote"),
                                           LoteId = info.Field<int>("LoteID"),
                                           Cabezas = info.Field<int>("Cabezas"),
                                           FechaDisponibilidad = info.Field<DateTime?>("FechaDisponibilidad") != null ? info.Field<DateTime>("FechaDisponibilidad") : DateTime.MinValue,
                                           FechaDisponibilidadOriginal = info.Field<DateTime?>("FechaDisponibilidad") != null ? info.Field<DateTime>("FechaDisponibilidad") : DateTime.MinValue,
                                           FechaAsignada = info.Field<DateTime?>("FechaDisponibilidad") != null ? info.Field<DateTime>("FechaDisponibilidad") : DateTime.MinValue,
                                           CodigoCorral = info.Field<string>("Codigo"),
                                           PesoProyectado = info.Field<int>("PesoProyectado"),
                                           DiasEngorda = info.Field<int>("DiasEngorda"),
                                           FechaInicioLote = info.Field<DateTime>("FechaInicio"),
                                           GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                                           Revision = info.Field<bool>("Revision"),
                                           DatosProyector = new ReporteProyectorInfo
                                           {
                                               Clasificacion = info.Field<string>("Clasificacion"),
                                               Merma = info.Field<decimal>("Merma"),
                                               PesoOrigen = info.Field<int>("PesoOrigen"),
                                               PesoProyectado = info.Field<int>("PesoProyectado"),
                                               GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                                               DiasEngorda = info.Field<int>("DiasEngorda"),
                                               Fecha1Reimplante = info.Field<DateTime?>("FechaReimplante1") != null ? info.Field<DateTime>("FechaReimplante1").ToShortDateString() : string.Empty,
                                               Peso1Reimplante = info.Field<int>("PesoReimplante1"),
                                               Ganancia1Diaria = info.Field<decimal>("GananciaReimplante1"),
                                               Fecha2Reimplante = info.Field<DateTime?>("FechaReimplante2") != null ? info.Field<DateTime>("FechaReimplante2").ToShortDateString() : string.Empty,
                                               Peso2Reimplante = info.Field<int>("PesoReimplante2"),
                                               Ganancia2Diaria = info.Field<decimal>("GananciaReimplante2"),
                                               Fecha3Reimplante = info.Field<DateTime?>("FechaReimplante3") != null ? info.Field<DateTime>("FechaReimplante3").ToShortDateString() : string.Empty,
                                               Peso3Reimplante = info.Field<int>("PesoReimplante3"),
                                               Ganancia3Diaria = info.Field<decimal>("GananciaReimplante3"),
                                               DiasF4 = info.Field<int>("DiasF4"),
                                               DiasZilmax = info.Field<int>("DiasZilmax")
                                           }
                                       }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return disponibilidadLotes;
        }

        internal static List<LoteInfo> ObtenerPorCorraleta(DataSet ds)
        {
            List<LoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteInfo
                                    {
                                        Cabezas = info.Field<int>("Cabezas"),
                                        LoteID = info.Field<int>("LoteID"),
                                        OrganizacionID = info.Field<int>("OrganizacionID"),
                                        CorralID = info.Field<int>("CorralID"),
                                        Lote = info.Field<string>("Lote")
                                    }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static List<LoteInfo> ObtenerLotesDescripcionPorIDs(DataSet ds)
        {
            List<LoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteInfo
                                    {
                                        LoteID = info.Field<int>("LoteID"),
                                        Lote = info.Field<string>("Lote")
                                    }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static List<LoteInfo> ObtenerLotesPorOrganizacionLote(DataSet ds)
        {
            List<LoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteInfo
                                    {
                                        LoteID = info.Field<int>("LoteID"),
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

        internal static Base.Infos.ResultadoInfo<LoteInfo> ObtenerPorPagina(PaginacionInfo paginacion, DataSet ds)
        {
            Base.Infos.ResultadoInfo<LoteInfo> lista = new ResultadoInfo<LoteInfo>();
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista.Lista = (from info in dt.AsEnumerable()
                               select new LoteInfo
                                          {
                                              LoteID = info.Field<int>("LoteID"),
                                              Lote = info.Field<string>("Lote"),
                                              OrganizacionID = info.Field<int>("OrganizacionID")
                                          }).ToList();

                lista.TotalRegistros = (int)ds.Tables[ConstantesDAL.DtContador].Rows[0][0];

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Obtiene una lista de Lotes por Organizacion y Lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<LoteInfo> ObtenerPorOrganizacionLoteXML(DataSet ds)
        {
            List<LoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteInfo
                                    {
                                        LoteID = info.Field<int>("LoteID"),
                                        OrganizacionID = info.Field<int>("OrganizacionID"),
                                        Corral = new CorralInfo
                                                     {
                                                         CorralID = info.Field<int>("CorralID")
                                                     },
                                        CorralID = info.Field<int>("CorralID"),
                                        TipoCorralID = info.Field<int>("TipoCorralID"),
                                        TipoProcesoID = info.Field<int>("TipoProcesoID"),
                                        FechaInicio = info.Field<DateTime?>("FechaInicio") ?? new DateTime(),
                                        CabezasInicio = info.Field<int>("CabezasInicio"),
                                        FechaCierre = info.Field<DateTime?>("FechaCierre") ?? new DateTime(),
                                        Cabezas = info.Field<int>("Cabezas"),
                                        FechaDisponibilidad = info.Field<DateTime?>("FechaDisponibilidad") ?? new DateTime(),
                                        DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        /// Obtiene una lista de lotes con fecha de cierre
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<LoteInfo> ObtenerPorOrganizacionCorralCerradoXML(DataSet ds)
        {
            List<LoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteInfo
                         {
                             LoteID = info.Field<int>("LoteID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Corral = new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID")
                             },
                             CorralID = info.Field<int>("CorralID"),
                             TipoCorralID = info.Field<int>("TipoCorralID"),
                             TipoProcesoID = info.Field<int>("TipoProcesoID"),
                             FechaInicio = info.Field<DateTime?>("FechaInicio") ?? new DateTime(),
                             CabezasInicio = info.Field<int>("CabezasInicio"),
                             FechaCierre = info.Field<DateTime?>("FechaCierre") ?? new DateTime(),
                             Cabezas = info.Field<int>("Cabezas"),
                             FechaDisponibilidad = info.Field<DateTime?>("FechaDisponibilidad") ?? new DateTime(),
                             DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        /// Obtiene una lista de lotes con fecha de cierre
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteDescargaDataLinkModel ObtenerLoteDataLink(DataSet ds)
        {
            LoteDescargaDataLinkModel datos;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                datos = (from info in dt.AsEnumerable()
                         select new LoteDescargaDataLinkModel
                         {
                             LoteID = info.Field<int>("LoteID"),
                             FechaInicio = info.Field<DateTime>("FechaInicio"),
                             Cabezas = info.Field<int>("Cabezas"),
                             PesoInicio = info.Field<int>("PesoInicio")
                         }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return datos;
        }

        /// <summary>
        /// Obtiene una lista de Disponibilidad Lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<DisponibilidadLoteInfo> ObtenerLotesActivosConProyeccion(DataSet ds)
        {
            List<DisponibilidadLoteInfo> disponibilidadLotes;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                disponibilidadLotes = (from info in dt.AsEnumerable()
                                       select new DisponibilidadLoteInfo
                                       {
                                           LoteId = info.Field<int>("LoteID")
                                       }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return disponibilidadLotes;
        }

        /// <summary>
        /// Obtiene lote con capacidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteInfo ObtenerLoteAnteriorAnimal(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                        {
                            LoteID = info.Field<int>("LoteID")
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lote;
        }

        /// <summary>
        /// Obtiene lote con capacidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<DiasEngordaGranoModel> ObtenerDiasEngordaGrano(DataSet ds)
        {
            List<DiasEngordaGranoModel> diasEngorda;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                diasEngorda = (from info in dt.AsEnumerable()
                               select new DiasEngordaGranoModel
                        {
                            LoteID = info.Field<int>("LoteID"),
                            DiasGrano = info.Field<int>("DiasGrano")
                        }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return diasEngorda;
        }

        /// <summary>
        /// Obtiene un mapedo de lote
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<LoteInfo> ObtenerMapeoLote()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<LoteInfo> mapeoLote = MapBuilder<LoteInfo>.MapNoProperties();
                mapeoLote.Map(x => x.Lote).ToColumn("Lote");
                mapeoLote.Map(x => x.LoteID).ToColumn("LoteID");
                mapeoLote.Map(x => x.Cabezas).ToColumn("Cabezas");
                mapeoLote.Map(x => x.CabezasInicio).ToColumn("CabezasInicio");
                mapeoLote.Map(x => x.OrganizacionID).ToColumn("OrganizacionID");
                mapeoLote.Map(x => x.Activo).WithFunc(x => Convert.ToBoolean(x["LoteActivo"]).BoolAEnum());
                MapeoCorralOrganizacionTipoCorralGrupoCorral(mapeoLote);
                return mapeoLote;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static void MapeoCorralOrganizacionTipoCorralGrupoCorral(IMapBuilderContext<LoteInfo> mapeoLote)
        {
            try
            {
                Logger.Info();
                mapeoLote.Map(x => x.Corral).WithFunc(x => new CorralInfo
                {
                    CorralID = Convert.ToInt32(x["CorralID"]),
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = Convert.ToInt32(x["OrganizacionID"]),
                        Descripcion = Convert.ToString(x["Organizacion"])
                    },
                    Codigo = Convert.ToString(x["Codigo"]),
                    TipoCorral = new TipoCorralInfo
                    {
                        TipoCorralID = Convert.ToInt32(x["TipoCorralID"]),
                        Descripcion = Convert.ToString(x["TipoCorral"])
                    },
                    Capacidad = Convert.ToInt32(x["Capacidad"]),
                    MetrosLargo = Convert.ToInt32(x["MetrosLargo"]),
                    MetrosAncho = Convert.ToInt64(x["MetrosAncho"]),
                    Seccion = Convert.ToInt32(x["Seccion"]),
                    Orden = Convert.ToInt32(x["Orden"]),
                    Activo = Convert.ToBoolean(x["Activo"]).BoolAEnum(),
                    GrupoCorralInfo = new GrupoCorralInfo
                    {
                        GrupoCorralID = Convert.ToInt32(x["GrupoCorralID"]),
                        Descripcion = Convert.ToString(x["GrupoCorral"])
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<LoteInfo> ObtenerPorOrganizacionEstatus(DataSet ds)
        {
            List<LoteInfo> lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                        {
                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                            Cabezas = info.Field<int>("Cabezas"),
                            CabezasInicio = info.Field<int>("CabezasInicio"),
                            CorralID = info.Field<int>("CorralID"),
                            DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                            FechaCierre =
                                (info["FechaCierre"] == DBNull.Value
                                     ? DateTime.Now
                                     : info.Field<DateTime>("FechaCierre")),
                            FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                            FechaDisponibilidad =
                                (info["FechaDisponibilidad"] == DBNull.Value
                                     ? DateTime.Now
                                     : info.Field<DateTime>("FechaDisponibilidad")),
                            FechaInicio = info.Field<DateTime>("FechaInicio"),
                            LoteID = info.Field<int>("LoteID"),
                            OrganizacionID = info.Field<int>("OrganizacionID"),
                            TipoCorralID = info.Field<int>("TipoCorralID"),
                            TipoProcesoID = info.Field<int>("TipoProcesoID"),
                            UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                            Lote = info.Field<string>("Lote"),
                            Corral = new CorralInfo
                            {
                                CorralID = info.Field<int>("CorralID"),
                                Codigo = info.Field<string>("Codigo")
                            }
                        }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lote;
        }
        /// <summary>
        /// Obtiene lote con capacidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteInfo ObtenerLotePorCodigo(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                        {
                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                            Cabezas = info.Field<int>("Cabezas"),
                            CabezasInicio = info.Field<int>("CabezasInicio"),
                            CorralID = info.Field<int>("CorralID"),
                            DisponibilidadManual = info.Field<bool>("DisponibilidadManual"),
                            FechaCierre =
                                (info["FechaCierre"] == DBNull.Value
                                     ? new DateTime(1900, 1, 1)
                                     : info.Field<DateTime>("FechaCierre")),
                            FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                            FechaDisponibilidad =
                                (info["FechaDisponibilidad"] == DBNull.Value
                                     ? new DateTime(1900, 1, 1)
                                     : info.Field<DateTime>("FechaDisponibilidad")),
                            FechaInicio = info.Field<DateTime>("FechaInicio"),
                            LoteID = info.Field<int>("LoteID"),
                            OrganizacionID = info.Field<int>("OrganizacionID"),
                            TipoCorralID = info.Field<int>("TipoCorralID"),
                            TipoProcesoID = info.Field<int>("TipoProcesoID"),
                            Lote = info.Field<string>("Lote")
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lote;
        }

        /// <summary>
        /// Obtiene lote con capacidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteInfo ObtenerPesoCompraPorLote(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                        {
                            LoteID = info.Field<int>("LoteID"),
                            PesoCompra = info.Field<int>("PesoCompra")
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lote;
        }
        /// <summary>
        /// Genera un Lote por Organizacion ID y Lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CabezasActualizadasInfo ObtenerPorActualizarCabezasProcesadas(DataSet ds)
        {
            CabezasActualizadasInfo cabezas;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                cabezas = (from info in dt.AsEnumerable()
                           select new CabezasActualizadasInfo
                        {
                            LoteIDOrigen = info.Field<int?>("LoteIDOrigen") != null ? info.Field<int>("LoteIDOrigen") : 0,
                            LoteIDDestino = info.Field<int?>("LoteIDDestino") != null ? info.Field<int>("LoteIDDestino") : 0,
                            CabezasOrigen = info.Field<int?>("CabezasOrigen") != null ? info.Field<int>("CabezasOrigen") : 0,
                            CabezasDestino = info.Field<int?>("CabezasDestino") != null ? info.Field<int>("CabezasDestino") : 0,
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return cabezas;
        }

        /// <summary>
        /// Obtiene el estatus de determinado lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteInfo ObtenerEstatusPorLoteId(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                        {
                            LoteID = info.Field<int>("LoteID"),
                            OrganizacionID = info.Field<int>("OrganizacionID"),
                            CorralID = info.Field<int>("CorralID"),
                            Lote = info.Field<string>("Lote"),
                            Cabezas = info.Field<int>("Cabezas"),
                            Activo = info.Field<bool>("Activo").BoolAEnum()
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lote;
        }

        internal static LoteInfo ObtenerValidarCorralCompletoParaSacrificio(DataSet ds)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lote = (from info in dt.AsEnumerable()
                        select new LoteInfo
                        {
                            Corral = new CorralInfo { Codigo = info.Field<string>("Codigo") },
                            Lote = info.Field<string>("Lote"),
                            Cabezas = info.Field<int>("TotalCabezas"),
                            OrganizacionID = info.Field<int>("OrganizacionId")
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lote;
        }

        internal static List<AnimalInfo> ObtenerAretesCorralPorLoteId(DataSet ds)
        {
            var animales = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                animales = (from info in dt.AsEnumerable()
                            select new AnimalInfo()
                                       {
                                           AnimalID = info.Field<long>("AnimalID"),
                                           Arete = info.Field<string>("Arete")
                                       }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animales;
        }

        internal static List<LoteInfo> ObtenerLotesConAnimalesDisponibles(DataSet ds)
        {
            var lotes = new List<LoteInfo>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                lotes = (from info in dt.AsEnumerable()
                            select new LoteInfo()
                            {
                                LoteID = info.Field<int>("LoteID"),
                                Corral = new CorralInfo { Codigo = info.Field<string>("Codigo") }
                            }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lotes;
        }
    }
}
 