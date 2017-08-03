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
    public class MapCheckListRoladoraAccionDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<CheckListRoladoraAccionInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraAccionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraAccionInfo
                             {
								CheckListRoladoraAccionID = info.Field<int>("CheckListRoladoraAccionID"),
								CheckListRoladoraRango = new CheckListRoladoraRangoInfo { CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"), Descripcion = info.Field<string>("CheckListRoladoraRango") },
								Descripcion = info.Field<string>("Descripcion"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CheckListRoladoraAccionInfo>
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
        public static List<CheckListRoladoraAccionInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraAccionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraAccionInfo
                             {
								CheckListRoladoraAccionID = info.Field<int>("CheckListRoladoraAccionID"),
								CheckListRoladoraRango = new CheckListRoladoraRangoInfo { CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"), Descripcion = info.Field<string>("CheckListRoladoraRango") },
								Descripcion = info.Field<string>("Descripcion"),
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
        public static CheckListRoladoraAccionInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraAccionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraAccionInfo
                             {
								CheckListRoladoraAccionID = info.Field<int>("CheckListRoladoraAccionID"),
								CheckListRoladoraRango = new CheckListRoladoraRangoInfo { CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"), Descripcion = info.Field<string>("CheckListRoladoraRango") },
								Descripcion = info.Field<string>("Descripcion"),
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
        public static CheckListRoladoraAccionInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraAccionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraAccionInfo
                             {
								CheckListRoladoraAccionID = info.Field<int>("CheckListRoladoraAccionID"),
								CheckListRoladoraRango = new CheckListRoladoraRangoInfo { CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"), Descripcion = info.Field<string>("CheckListRoladoraRango") },
								Descripcion = info.Field<string>("Descripcion"),
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

        internal static IList<CheckListRoladoraAccionInfo> ObtenerParametros(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraAccionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraAccionInfo
                             {
                                 Indice = info.Field<long>("Indice"),
                                 CheckListRoladoraAccionID = info.Field<int>("CheckListRoladoraAccionID"),
                                 CheckListRoladoraRango =
                                     new CheckListRoladoraRangoInfo
                                         {
                                             CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"),
                                             Descripcion = info.Field<string>("DescripcionRango"),
                                             Pregunta = new PreguntaInfo
                                                            {
                                                                PreguntaID = info.Field<int>("PreguntaID"),
                                                                Descripcion = info.Field<string>("Pregunta"),
                                                                TipoPregunta = new TipoPreguntaInfo
                                                                                   {
                                                                                       TipoPreguntaID = info.Field<int>("TipoPreguntaID"),
                                                                                       Descripcion = info.Field<string>("TipoPregunta")
                                                                                   }
                                                            },
                                             CodigoColor = info.Field<string>("CodigoColor")
                                         },
                                 Descripcion = info.Field<string>("DescripcionAccion"),
                             }).ToList();
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
