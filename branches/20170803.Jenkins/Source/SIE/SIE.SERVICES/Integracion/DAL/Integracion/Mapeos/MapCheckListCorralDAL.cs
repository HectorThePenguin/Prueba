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
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapCheckListCorralDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CheckListCorralInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListCorralInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListCorralInfo
                             {
								CheckListCorralID = info.Field<int>("CheckListCorralID"),
                                LoteID = info.Field<int>("LoteID"),
								PDF = info.Field<byte[]>("PDF"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CheckListCorralInfo>
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

        /// <summary>
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<CheckListCorralInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListCorralInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListCorralInfo
                             {
								CheckListCorralID = info.Field<int>("CheckListCorralID"),
                                LoteID = info.Field<int>("LoteID"),
                                PDF = info.Field<byte[]>("PDF"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CheckListCorralInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListCorralInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListCorralInfo
                             {
								CheckListCorralID = info.Field<int>("CheckListCorralID"),
                                LoteID = info.Field<int>("LoteID"),
                                PDF = info.Field<byte[]>("PDF"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CheckListCorralInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListCorralInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListCorralInfo
                             {
								CheckListCorralID = info.Field<int>("CheckListCorralID"),
                                LoteID = info.Field<int>("LoteID"),
                                PDF = info.Field<byte[]>("PDF"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CheckListCorralInfo ObtenerPorLote(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListCorralInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListCorralInfo
                         {
                             CheckListCorralID = info.Field<int>("CheckListCorralID"),
                             LoteID = info.Field<int>("LoteID"),
                             PDF = info.Field<byte[]>("PDF"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static LoteProyeccionInfo GenerarProyeccion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtReimplante = ds.Tables[ConstantesDAL.DtLoteReimplante];
                LoteProyeccionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                         {
                            LoteID = info.Field<int>("LoteID"),
                            OrganizacionID = info.Field<int>("OrganizacionID"),
                            Frame = info.Field<decimal>("Frame"),
                            GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                            ConsumoBaseHumeda = info.Field<decimal>("ConsumoBase"),
                            Conversion = info.Field<decimal>("Conversion"),
                            PesoMaduro = info.Field<int>("PesoMaduro"),
                            PesoSacrificio = info.Field<int>("PesoSacrificio"),
                            DiasEngorda = info.Field<int>("DiasEngorda"),
                            FechaEntradaZilmax = info.Field<DateTime>("EntradaZilmax"),
                            FechaSacrificio = info.Field<DateTime>("FechaSacrificio"),
                            Proyeccion = (from reimplante in dtReimplante.AsEnumerable()
                                              select new CheckListProyeccionInfo
                                                  {
                                                      PrimerReimplante = reimplante.Field<int?>("PrimerImplante") != null ? reimplante.Field<int>("PrimerImplante") : 0,
                                                      FechaProyectada1 = reimplante.Field<DateTime?>("FechaProyectada1") != null ? reimplante.Field<DateTime>("FechaProyectada1") : DateTime.MinValue,
                                                      PesoProyectado1 = reimplante.Field<int?>("PesoProyectado1") != null ? reimplante.Field<int>("PesoProyectado1") : 0,
                                                      SegundoReimplante = reimplante.Field<int?>("SegundoImplante") != null ? reimplante.Field<int>("SegundoImplante") : 0,
                                                      FechaProyectada2 = reimplante.Field<DateTime?>("FechaProyectada2") != null ? reimplante.Field<DateTime>("FechaProyectada2") : DateTime.MinValue,
                                                      PesoProyectado2 = reimplante.Field<int?>("PesoProyectado2") != null ? reimplante.Field<int>("PesoProyectado2") : 0,
                                                  }).FirstOrDefault()
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

