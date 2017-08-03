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
    internal class MapLoteReimplanteDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<LoteReimplanteInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<LoteReimplanteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteReimplanteInfo
                             {
                                 LoteReimplanteID = info.Field<int>("LoteReimplanteID"),
                                 NumeroReimplante = info.Field<int>("NumeroReimplante"),
                                 FechaProyectada = info.Field<DateTime>("FechaProyectada"),
                                 PesoProyectado = info.Field<int>("PesoProyectado"),
                                 FechaReal = (info["FechaReal"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaReal")),
                                 PesoReal = (info["PesoReal"] == DBNull.Value ? 0 : info.Field<int>("PesoReal")),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<LoteReimplanteInfo>
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
        internal static List<LoteReimplanteInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<LoteReimplanteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteReimplanteInfo
                             {
                                 LoteReimplanteID = info.Field<int>("LoteReimplanteID"),
                                 NumeroReimplante = info.Field<int>("NumeroReimplante"),
                                 FechaProyectada = info.Field<DateTime>("FechaProyectada"),
                                 PesoProyectado = info.Field<int>("PesoProyectado"),
                                 FechaReal = (info["FechaReal"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaReal")),
                                 PesoReal = (info["PesoReal"] == DBNull.Value ? 0 : info.Field<int>("PesoReal")),
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
        internal static LoteReimplanteInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                LoteReimplanteInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteReimplanteInfo
                             {
                                 LoteReimplanteID = info.Field<int>("LoteReimplanteID"),
                                 NumeroReimplante = info.Field<int>("NumeroReimplante"),
                                 FechaProyectada = info.Field<DateTime>("FechaProyectada"),
                                 PesoProyectado = info.Field<int>("PesoProyectado"),
                                 FechaReal = (info["FechaReal"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaReal")),
                                 PesoReal = (info["PesoReal"] == DBNull.Value ? 0 : info.Field<int>("PesoReal"))
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
        internal static LoteReimplanteInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                LoteReimplanteInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteReimplanteInfo
                             {
								LoteReimplanteID = info.Field<int>("LoteReimplanteID"),
								NumeroReimplante = info.Field<int>("NumeroReimplante"),
								FechaProyectada = info.Field<DateTime>("FechaProyectada"),
								PesoProyectado = info.Field<int>("PesoProyectado"),
								FechaReal = (info["FechaReal"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaReal")),
                                PesoReal = (info["PesoReal"] == DBNull.Value ? 0 : info.Field<int>("PesoReal")),
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
        /// obtiene los datos del reimplante de un lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static LoteReimplanteInfo ObtenerPorlote(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteReimplanteInfo
                         {
                             LoteReimplanteID = info.Field<int>("LoteReimplanteID"),
                             NumeroReimplante = info.Field<int>("NumeroReimplante"),
                             FechaProyectada = info.Field<DateTime>("FechaProyectada"),
                             PesoProyectado = info.Field<int>("PesoProyectado"),
                             FechaReal = (info["FechaReal"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaReal")),
                             PesoReal = (info["PesoReal"] == DBNull.Value ? 0 : info.Field<int>("PesoReal"))

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
        /// obtiene los datos del reimplante de un lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<LoteReimplanteInfo> ObtenerListaPorlote(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteReimplanteInfo
                         {
                             LoteReimplanteID = info.Field<int>("LoteReimplanteID"),
                             NumeroReimplante = info.Field<int>("NumeroReimplante"),
                             FechaProyectada = info.Field<DateTime>("FechaProyectada"),
                             PesoProyectado = info.Field<int>("PesoProyectado"),
                             FechaReal = (info["FechaReal"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaReal")),
                             PesoReal = (info["PesoReal"] == DBNull.Value ? 0 : info.Field<int>("PesoReal"))

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
        /// Obtiene una lista de lote reimplante por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<LoteReimplanteInfo> ObtenerPorloteXML(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteReimplanteInfo
                             {
                                 LoteReimplanteID = info.Field<int>("LoteReimplanteID"),
                                 NumeroReimplante = info.Field<int>("NumeroReimplante"),
                                 FechaProyectada = info.Field<DateTime>("FechaProyectada"),
                                 PesoProyectado = info.Field<int>("PesoProyectado"),
                                 FechaReal =
                                     (info["FechaReal"] == DBNull.Value
                                          ? new DateTime()
                                          : info.Field<DateTime>("FechaReal")),
                                 PesoReal = (info["PesoReal"] == DBNull.Value ? 0 : info.Field<int>("PesoReal"))
                             }).ToList();
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

