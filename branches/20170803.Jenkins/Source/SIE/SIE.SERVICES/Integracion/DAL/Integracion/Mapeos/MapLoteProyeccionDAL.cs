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
    internal class MapLoteProyeccionDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<LoteProyeccionInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<LoteProyeccionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                             {
								LoteProyeccionID = info.Field<int>("LoteProyeccionID"),
								LoteID = info.Field<int>("LoteID"),
								OrganizacionID = info.Field<int>("OrganizacionID"),
								Frame = info.Field<decimal>("Frame"),
								GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
								ConsumoBaseHumeda = info.Field<decimal>("ConsumoBaseHumeda"),
								Conversion = info.Field<decimal>("Conversion"),
								PesoMaduro = info.Field<int>("PesoMaduro"),
								PesoSacrificio = info.Field<int>("PesoSacrificio"),
								DiasEngorda = info.Field<int>("DiasEngorda"),
								FechaEntradaZilmax = info.Field<DateTime>("FechaEntradaZilmax"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<LoteProyeccionInfo>
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
        internal static List<LoteProyeccionInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<LoteProyeccionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                             {
								LoteProyeccionID = info.Field<int>("LoteProyeccionID"),
                                LoteID = info.Field<int>("LoteID"),
								OrganizacionID = info.Field<int>("OrganizacionID"),
								Frame = info.Field<decimal>("Frame"),
								GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
								ConsumoBaseHumeda = info.Field<decimal>("ConsumoBaseHumeda"),
								Conversion = info.Field<decimal>("Conversion"),
								PesoMaduro = info.Field<int>("PesoMaduro"),
								PesoSacrificio = info.Field<int>("PesoSacrificio"),
								DiasEngorda = info.Field<int>("DiasEngorda"),
								FechaEntradaZilmax = info.Field<DateTime>("FechaEntradaZilmax"),
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
        internal static LoteProyeccionInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                LoteProyeccionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                             {
								LoteProyeccionID = info.Field<int>("LoteProyeccionID"),
                                LoteID = info.Field<int>("LoteID"),
								OrganizacionID = info.Field<int>("OrganizacionID"),
								Frame = info.Field<decimal>("Frame"),
								GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
								ConsumoBaseHumeda = info.Field<decimal>("ConsumoBaseHumeda"),
								Conversion = info.Field<decimal>("Conversion"),
								PesoMaduro = info.Field<int>("PesoMaduro"),
								PesoSacrificio = info.Field<int>("PesoSacrificio"),
								DiasEngorda = info.Field<int>("DiasEngorda"),
								FechaEntradaZilmax = info.Field<DateTime>("FechaEntradaZilmax"),
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
        internal static LoteProyeccionInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                LoteProyeccionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                             {
								LoteProyeccionID = info.Field<int>("LoteProyeccionID"),
                                LoteID = info.Field<int>("LoteID"),
								OrganizacionID = info.Field<int>("OrganizacionID"),
								Frame = info.Field<decimal>("Frame"),
								GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
								ConsumoBaseHumeda = info.Field<decimal>("ConsumoBaseHumeda"),
								Conversion = info.Field<decimal>("Conversion"),
								PesoMaduro = info.Field<int>("PesoMaduro"),
								PesoSacrificio = info.Field<int>("PesoSacrificio"),
								DiasEngorda = info.Field<int>("DiasEngorda"),
								FechaEntradaZilmax = info.Field<DateTime>("FechaEntradaZilmax"),
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
        internal static LoteProyeccionInfo ObtenerPorLote(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                         {
                             LoteProyeccionID = info.Field<int>("LoteProyeccionID"),
                             LoteID = info.Field<int>("LoteID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Frame = info.Field<decimal>("Frame"),
                             GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                             ConsumoBaseHumeda = info.Field<decimal>("ConsumoBaseHumeda"),
                             Conversion = info.Field<decimal>("Conversion"),
                             PesoMaduro = info.Field<int>("PesoMaduro"),
                             PesoSacrificio = info.Field<int>("PesoSacrificio"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             FechaEntradaZilmax = info.Field<DateTime>("FechaEntradaZilmax")
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
        /// Obtiene una lista de lote proyeccion por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<LoteProyeccionInfo> ObtenerPorLoteXML(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                             {
                                 LoteProyeccionID = info.Field<int>("LoteProyeccionID"),
                                 LoteID = info.Field<int>("LoteID"),
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Frame = info.Field<decimal>("Frame"),
                                 GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                                 ConsumoBaseHumeda = info.Field<decimal>("ConsumoBaseHumeda"),
                                 Conversion = info.Field<decimal>("Conversion"),
                                 PesoMaduro = info.Field<int>("PesoMaduro"),
                                 PesoSacrificio = info.Field<int>("PesoSacrificio"),
                                 DiasEngorda = info.Field<int>("DiasEngorda"),
                                 FechaEntradaZilmax = info.Field<DateTime>("FechaEntradaZilmax")
                             }).ToList();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para obtener las proyecciones de los corrales origenes para un corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<LoteProyeccionInfo> ObtenerProyeccionDeLotesOrigen(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                         {
                             LoteID = info.Field<int>("LoteID"),
                             ConsumoBaseHumeda = info.Field<decimal>("ConsumoBaseHumeda"),
                             Conversion = info.Field<decimal>("Conversion"),
                             PesoMaduro = info.Field<int>("PesoMaduro"),
                             PesoSacrificio = info.Field<int>("PesoSacrificio"),
                             NumeroReimplante = info.Field<int>("NumeroReimplante")
                         }).ToList();
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
        internal static LoteProyeccionInfo ObtenerPorLoteCompleto(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var dtReimplante = ds.Tables[ConstantesDAL.DtDetalle];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                         {
                             LoteProyeccionID = info.Field<int>("LoteProyeccionID"),
                             LoteID = info.Field<int>("LoteID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Frame = info.Field<decimal>("Frame"),
                             GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                             ConsumoBaseHumeda = info.Field<decimal>("ConsumoBaseHumeda"),
                             Conversion = info.Field<decimal>("Conversion"),
                             PesoMaduro = info.Field<int>("PesoMaduro"),
                             PesoSacrificio = info.Field<int>("PesoSacrificio"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             FechaEntradaZilmax = info.Field<DateTime>("FechaEntradaZilmax"),
                             ListaReimplantes = (from reim in dtReimplante.AsEnumerable()
                                                     select new LoteReimplanteInfo
                                                                {
                                                                    LoteReimplanteID = reim.Field<int>("LoteReimplanteID"),
                                                                    LoteProyeccionID = reim.Field<int>("LoteProyeccionID"),
                                                                    NumeroReimplante = reim.Field<int>("NumeroReimplante"),
                                                                    FechaProyectada = reim.Field<DateTime>("FechaProyectada"),
                                                                    PesoProyectado = reim.Field<int>("PesoProyectado"),
                                                                    FechaReal = reim.Field<DateTime?>("FechaReal") != null ? reim.Field<DateTime>("FechaReal") : DateTime.MinValue,
                                                                    PesoReal = reim.Field<int?>("PesoReal") != null ? reim.Field<int>("PesoReal") : 0
                                                                }).ToList()
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
        internal static LoteProyeccionInfo ObtenerPorLoteProyeccionID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                LoteProyeccionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteProyeccionInfo
                         {
                             LoteProyeccionID = info.Field<int>("LoteProyeccionID"),
                             LoteID = info.Field<int>("LoteID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Frame = info.Field<decimal>("Frame"),
                             GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                             ConsumoBaseHumeda = info.Field<decimal>("ConsumoBaseHumeda"),
                             Conversion = info.Field<decimal>("Conversion"),
                             PesoMaduro = info.Field<int>("PesoMaduro"),
                             PesoSacrificio = info.Field<int>("PesoSacrificio"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             FechaEntradaZilmax = info.Field<DateTime>("FechaEntradaZilmax")
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

