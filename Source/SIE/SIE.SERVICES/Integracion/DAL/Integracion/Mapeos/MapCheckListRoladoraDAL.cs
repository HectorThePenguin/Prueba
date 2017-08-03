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
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapCheckListRoladoraDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<CheckListRoladoraInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraInfo
                             {
                                 CheckListRoladoraID = info.Field<int>("CheckListRoladoraID"),
                                 CheckListRoladoraGeneral =
                                     new CheckListRoladoraGeneralInfo
                                         {
                                             CheckListRoladoraGeneralID = info.Field<int>("CheckListRoladoraGeneralID"),
                                         },
                                 Roladora =
                                     new RoladoraInfo
                                         {
                                             RoladoraID = info.Field<int>("RoladoraID"),
                                             Descripcion = info.Field<string>("Roladora")
                                         },
                                 Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
                                 FechaCheckList = info.Field<DateTime>("FechaCheckList"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CheckListRoladoraInfo>
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
        public static List<CheckListRoladoraInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraInfo
                             {
                                 CheckListRoladoraID = info.Field<int>("CheckListRoladoraID"),
                                 CheckListRoladoraGeneral =
                                     new CheckListRoladoraGeneralInfo { CheckListRoladoraGeneralID = info.Field<int>("CheckListRoladoraGeneralID"), },
                                 Roladora =
                                     new RoladoraInfo
                                         {
                                             RoladoraID = info.Field<int>("RoladoraID"),
                                             Descripcion = info.Field<string>("Roladora")
                                         },
                                 Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), },
                                 FechaCheckList = info.Field<DateTime>("FechaCheckList"),
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
        public static CheckListRoladoraInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraInfo
                             {
                                 CheckListRoladoraID = info.Field<int>("CheckListRoladoraID"),
                                 CheckListRoladoraGeneral =
                                     new CheckListRoladoraGeneralInfo
                                         {
                                             CheckListRoladoraGeneralID = info.Field<int>("CheckListRoladoraGeneralID"),
                                         },
                                 Roladora =
                                     new RoladoraInfo
                                         {
                                             RoladoraID = info.Field<int>("RoladoraID"),
                                             Descripcion = info.Field<string>("Roladora")
                                         },
                                 Usuario =
                                     new UsuarioInfo
                                         {
                                             UsuarioID = info.Field<int>("UsuarioID"),
                                         },
                                 FechaCheckList = info.Field<DateTime>("FechaCheckList"),
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
        public static CheckListRoladoraInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraInfo
                             {
                                 CheckListRoladoraID = info.Field<int>("CheckListRoladoraID"),
                                 CheckListRoladoraGeneral =
                                     new CheckListRoladoraGeneralInfo
                                         {
                                             CheckListRoladoraGeneralID = info.Field<int>("CheckListRoladoraGeneralID"),
                                         },
                                 Roladora =
                                     new RoladoraInfo
                                         {
                                             RoladoraID = info.Field<int>("RoladoraID"),
                                             Descripcion = info.Field<string>("Roladora")
                                         },
                                 Usuario =
                                     new UsuarioInfo
                                         {
                                             UsuarioID = info.Field<int>("UsuarioID"),
                                         },
                                 FechaCheckList = info.Field<DateTime>("FechaCheckList"),
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

        internal static IList<CheckListRoladoraInfo> ObtenerNotificaciones(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraInfo
                             {
                                 CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo
                                                                {
                                                                    Turno = info.Field<int>("Turno")
                                                                },
                                 Roladora = new RoladoraInfo
                                                {
                                                    Descripcion = info.Field<string>("Roladora")
                                                },
                                 Hora = info.Field<string>("Hora")
                             }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static CheckListRoladoraInfo ObtenerPorTurno(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraInfo checkList =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraInfo
                         {
                             CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo
                             {
                                 FechaInicio = info.Field<DateTime>("FechaInicio"),
                                 UsuarioIDSupervisor = info.Field<int?>("UsuarioIDSupervisor")
                             },
                             Hora = info.Field<string>("HoraInicio")
                         }).FirstOrDefault();
                if (checkList != null)
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    checkList.Roladoras = (from info in dt.AsEnumerable()
                                           select new RoladoraInfo
                                                      {

                                                          Descripcion = info.Field<string>("Descripcion"),
                                                          RoladoraID = info.Field<int>("RoladoraID")
                                                      }).ToList();
                }
                return checkList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static dynamic ObtenerCheckList(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                dynamic datosCheckList = new
                                             {
                                                 TiempoTranscurrido = Convert.ToInt32(dt.Rows[0]["TiempoTranscurrido"])
                                                 ,
                                                 Supervisado = Convert.ToInt32(dt.Rows[0]["Supervisado"])
                                             };
                return datosCheckList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static ParametrosCheckListRoladoModel ObtenerPorGranoEnteroDieselCaldera(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametrosCheckListRoladoModel checkList =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametrosCheckListRoladoModel
                         {
                             TotalGranoEntreroPP = info.Field<decimal>("CantidadEntregada"),
                             ConsumoDieselCalderas = info.Field<decimal>("Cantidad")
                         }).FirstOrDefault();
                return checkList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static CheckListRoladoraInfo ObtenerPorValidarCheckList(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraInfo checkList =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraInfo
                             {
                                 CheckListRoladoraID = info.Field<int?>("CheckListRoladoraID") ?? 0,
                                 CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo
                                                                {
                                                                    Turno = info.Field<int>("Turno"),
                                                                    FechaInicio = info.Field<DateTime>("FechaInicio"),
                                                                    CheckListRoladoraGeneralID =
                                                                        info.Field<int>("CheckListRoladoraGeneralID"),
                                                                    UsuarioIDSupervisor = info.Field<int?>("UsuarioIDSupervisor"),
                                                                    Observaciones = info.Field<string>("Observaciones"),
                                                                    SurfactanteInicio = info.Field<decimal?>("SurfactanteInicio"),
                                                                    SurfactanteFin = info.Field<decimal?>("SurfactanteFin"),
                                                                    ContadorAguaInicio = info.Field<decimal?>("ContadorAguaInicio"),
                                                                    ContadorAguaFin = info.Field<decimal?>("ContadorAguaFin"),
                                                                    GranoEnteroFinal = info.Field<decimal?>("GranoEnteroFinal")
                                                                }
                             }).FirstOrDefault();
                return checkList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un elemeto para la impresion del
        /// check list de rolado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ImpresionCheckListRoladoModel ObtenerDatosImpresionCheckListRoladora(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ImpresionCheckListRoladoModel impresionCheckListRoladoModel =
                    (from info in dt.AsEnumerable()
                     select
                         new ImpresionCheckListRoladoModel
                             {
                                 CheckListRoladoraGeneral = new CheckListRoladoraGeneralInfo
                                                                {
                                                                    Turno = info.Field<int>("Turno"),
                                                                    FechaInicio = info.Field<DateTime>("FechaInicio"),
                                                                    Observaciones = info.Field<string>("Observaciones"),
                                                                    SurfactanteInicio =
                                                                        info.Field<decimal?>("SurfactanteInicio") ?? 0,
                                                                    SurfactanteFin =
                                                                        info.Field<decimal?>("SurfactanteFin") ?? 0,
                                                                    ContadorAguaInicio =
                                                                        info.Field<decimal?>("ContadorAguaInicio") ?? 0,
                                                                    ContadorAguaFin =
                                                                        info.Field<decimal?>("ContadorAguaFin") ?? 0,
                                                                    GranoEnteroFinal =
                                                                        info.Field<decimal?>("GranoEnteroFinal") ?? 0,
                                                                    CheckListRoladoraGeneralID =
                                                                        info.Field<int>("CheckListRoladoraGeneralID"),
                                                                    NombreUsuario = info.Field<string>("NombreUsuario")
                                                                }
                             }).FirstOrDefault();
                if (impresionCheckListRoladoModel != null)
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    impresionCheckListRoladoModel.Horometros = (from horo in dt.AsEnumerable()
                                                                select new CheckListRoladoraHorometroInfo
                                                                           {
                                                                               CheckListRoladoraHorometroID = horo.Field<int>("CheckListRoladoraHorometroID"),
                                                                               Roladora = new RoladoraInfo
                                                                                              {
                                                                                                  RoladoraID = horo.Field<int>("RoladoraID"),
                                                                                                  Descripcion = horo.Field<string>("Descripcion")
                                                                                              },
                                                                               HorometroInicial = horo.Field<string>("HorometroInicial"),
                                                                               HorometroFinal = horo.Field<string>("HorometroFinal"),
                                                                               CheckListRoladoraGeneralID = horo.Field<int>("CheckListRoladoraGeneralID")
                                                                           }).ToList();
                    dt = ds.Tables[ConstantesDAL.DtTotales];
                    impresionCheckListRoladoModel.Detalles = (from det in dt.AsEnumerable()
                                                              select new CheckListRoladoraDetalleInfo
                                                                         {
                                                                             CheckListRoladoraAccion = new CheckListRoladoraAccionInfo
                                                                                                           {
                                                                                                               CheckListRoladoraAccionID = det.Field<int?>("CheckListRoladoraAccionID") ?? 0,
                                                                                                               Descripcion = det.Field<string>("Accion")
                                                                                                           },
                                                                             CheckListRoladoraRango = new CheckListRoladoraRangoInfo
                                                                                                          {
                                                                                                              CheckListRoladoraRangoID = det.Field<int>("CheckListRoladoraRangoID"),
                                                                                                              Descripcion = det.Field<string>("Rango"),
                                                                                                              CodigoColor = det.Field<string>("CodigoColor"),
                                                                                                              Pregunta = new PreguntaInfo
                                                                                                                             {
                                                                                                                                 TipoPregunta = new TipoPreguntaInfo
                                                                                                                                                    {
                                                                                                                                                        TipoPreguntaID = det.Field<int>("TipoPreguntaID"),
                                                                                                                                                        Descripcion = det.Field<string>("TipoPregunta")
                                                                                                                                                    }
                                                                                                                             }
                                                                                                          },
                                                                             CheckListRoladora = new CheckListRoladoraInfo
                                                                                                     {
                                                                                                         FechaCheckList = det.Field<DateTime>("FechaCheckList"),
                                                                                                         RoladoraID = det.Field<int>("RoladoraID"),
                                                                                                         Roladora = new RoladoraInfo
                                                                                                                        {
                                                                                                                            RoladoraID = det.Field<int>("RoladoraID"),
                                                                                                                            Descripcion = det.Field<string>("Roladora")
                                                                                                                        }
                                                                                                     }
                                                                         }).ToList();
                }
                return impresionCheckListRoladoModel;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
