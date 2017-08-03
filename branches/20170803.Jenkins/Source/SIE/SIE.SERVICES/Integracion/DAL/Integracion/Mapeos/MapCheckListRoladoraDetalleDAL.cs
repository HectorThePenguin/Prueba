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
    public class MapCheckListRoladoraDetalleDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<CheckListRoladoraDetalleInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraDetalleInfo
                             {
								CheckListRoladoraDetalleID = info.Field<int>("CheckListRoladoraDetalleID"),
								CheckListRoladora = new CheckListRoladoraInfo { CheckListRoladoraID = info.Field<int>("CheckListRoladoraID") },
								CheckListRoladoraRango = new CheckListRoladoraRangoInfo { CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"), Descripcion = info.Field<string>("CheckListRoladoraRango") },
								CheckListRoladoraAccion = new CheckListRoladoraAccionInfo { CheckListRoladoraAccionID = info.Field<int>("CheckListRoladoraAccionID"), Descripcion = info.Field<string>("CheckListRoladoraAccion") },
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CheckListRoladoraDetalleInfo>
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
        public static List<CheckListRoladoraDetalleInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraDetalleInfo
                             {
								CheckListRoladoraDetalleID = info.Field<int>("CheckListRoladoraDetalleID"),
								CheckListRoladora = new CheckListRoladoraInfo { CheckListRoladoraID = info.Field<int>("CheckListRoladoraID") },
								CheckListRoladoraRango = new CheckListRoladoraRangoInfo { CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"), Descripcion = info.Field<string>("CheckListRoladoraRango") },
								CheckListRoladoraAccion = new CheckListRoladoraAccionInfo { CheckListRoladoraAccionID = info.Field<int>("CheckListRoladoraAccionID"), Descripcion = info.Field<string>("CheckListRoladoraAccion") },
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
        public static CheckListRoladoraDetalleInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraDetalleInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraDetalleInfo
                             {
								CheckListRoladoraDetalleID = info.Field<int>("CheckListRoladoraDetalleID"),
								CheckListRoladora = new CheckListRoladoraInfo { CheckListRoladoraID = info.Field<int>("CheckListRoladoraID") },
								CheckListRoladoraRango = new CheckListRoladoraRangoInfo { CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"), Descripcion = info.Field<string>("CheckListRoladoraRango") },
								CheckListRoladoraAccion = new CheckListRoladoraAccionInfo { CheckListRoladoraAccionID = info.Field<int>("CheckListRoladoraAccionID"), Descripcion = info.Field<string>("CheckListRoladoraAccion") },
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
        public static CheckListRoladoraDetalleInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraDetalleInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraDetalleInfo
                             {
								CheckListRoladoraDetalleID = info.Field<int>("CheckListRoladoraDetalleID"),
								CheckListRoladora = new CheckListRoladoraInfo { CheckListRoladoraID = info.Field<int>("CheckListRoladoraID") },
								CheckListRoladoraRango = new CheckListRoladoraRangoInfo { CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"), Descripcion = info.Field<string>("CheckListRoladoraRango") },
								CheckListRoladoraAccion = new CheckListRoladoraAccionInfo { CheckListRoladoraAccionID = info.Field<int>("CheckListRoladoraAccionID"), Descripcion = info.Field<string>("CheckListRoladoraAccion") },
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

        internal static List<CheckListRoladoraDetalleInfo> ObtenerCheckListCompleto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraDetalleInfo
                             {
                                 CheckListRoladoraDetalleID = info.Field<int>("CheckListRoladoraDetalleID"),

                                 CheckListRoladoraID = info.Field<int>("CheckListRoladoraID"),
                                 CheckListRoladora =
                                     new CheckListRoladoraInfo
                                         {
                                             CheckListRoladoraID = info.Field<int>("CheckListRoladoraID"),
                                             RoladoraID = info.Field<int>("RoladoraID"),
                                             Roladora = new RoladoraInfo
                                                            {
                                                                RoladoraID = info.Field<int>("RoladoraID"),
                                                            },
                                             UsuarioIDResponsable = info.Field<int?>("UsuarioIDResponsable"),
                                             //HorometroInicial = info.Field<string>("HorometroInicial"),
                                             //HorometroFinal = info.Field<string>("HorometroFinal"),
                                             FechaCheckList = info.Field<DateTime>("FechaCheckList"),
                                             CheckListRoladoraGeneralID = info.Field<int>("CheckListRoladoraGeneralID"),
                                             CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo
                                                                            {
                                                                                CheckListRoladoraGeneralID =
                                                                                    info.Field<int>(
                                                                                        "CheckListRoladoraGeneralID"),
                                                                                Turno = info.Field<int>("Turno"),
                                                                                FechaInicio =
                                                                                    info.Field<DateTime>("FechaInicio"),
                                                                                UsuarioIDSupervisor =
                                                                                    info.Field<int?>(
                                                                                        "UsuarioIDSupervisor"),
                                                                                Observaciones =
                                                                                    info.Field<string>("Observaciones"),
                                                                                SurfactanteInicio =
                                                                                    info.Field<decimal?>(
                                                                                        "SurfactanteInicio"),
                                                                                SurfactanteFin =
                                                                                    info.Field<decimal?>(
                                                                                        "SurfactanteFin"),
                                                                                ContadorAguaInicio =
                                                                                    info.Field<decimal?>(
                                                                                        "ContadorAguaInicio"),
                                                                                ContadorAguaFin =
                                                                                    info.Field<decimal?>(
                                                                                        "ContadorAguaFin"),
                                                                                GranoEnteroFinal =
                                                                                    info.Field<decimal?>(
                                                                                        "GranoEnteroFinal"),
                                                                            },
                                                                            CheckListRoladoraHorometro = new CheckListRoladoraHorometroInfo
                                                                                {
                                                                                    CheckListRoladoraHorometroID = info.Field<int>("CheckListRoladoraHorometroID"),
                                                                                    HorometroInicial = info.Field<string>("HorometroInicial"),
                                                                                    HorometroFinal = info.Field<string>("HorometroFinal")
                                                                                }
                                         },
                                 CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"),
                                 CheckListRoladoraRango =
                                     new CheckListRoladoraRangoInfo
                                         {
                                             CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"),
                                         },
                                 CheckListRoladoraAccionID = info.Field<int?>("CheckListRoladoraAccionID"),
                                 CheckListRoladoraAccion =
                                     new CheckListRoladoraAccionInfo
                                         {
                                             CheckListRoladoraAccionID = info.Field<int?>("CheckListRoladoraAccionID"),
                                         },
                                         
                                         
                             }).ToList();
                return checkListRoladoraDetalle;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

