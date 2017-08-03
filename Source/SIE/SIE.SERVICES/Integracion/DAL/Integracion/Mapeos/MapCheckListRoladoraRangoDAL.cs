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
    public class MapCheckListRoladoraRangoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<CheckListRoladoraRangoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraRangoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraRangoInfo
                             {
								CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"),
								Pregunta = new PreguntaInfo { PreguntaID = info.Field<int>("PreguntaID"), Descripcion = info.Field<string>("Pregunta") },
								Descripcion = info.Field<string>("Descripcion"),
								CodigoColor = info.Field<string>("CodigoColor"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CheckListRoladoraRangoInfo>
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
        public static List<CheckListRoladoraRangoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraRangoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraRangoInfo
                             {
								CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"),
								Pregunta = new PreguntaInfo { PreguntaID = info.Field<int>("PreguntaID"), Descripcion = info.Field<string>("Pregunta") },
								Descripcion = info.Field<string>("Descripcion"),
								CodigoColor = info.Field<string>("CodigoColor"),
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
        public static CheckListRoladoraRangoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraRangoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraRangoInfo
                             {
								CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"),
								Pregunta = new PreguntaInfo { PreguntaID = info.Field<int>("PreguntaID"), Descripcion = info.Field<string>("Pregunta") },
								Descripcion = info.Field<string>("Descripcion"),
								CodigoColor = info.Field<string>("CodigoColor"),
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
        public static CheckListRoladoraRangoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraRangoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraRangoInfo
                             {
								CheckListRoladoraRangoID = info.Field<int>("CheckListRoladoraRangoID"),
								Pregunta = new PreguntaInfo { PreguntaID = info.Field<int>("PreguntaID"), Descripcion = info.Field<string>("Pregunta") },
								Descripcion = info.Field<string>("Descripcion"),
								CodigoColor = info.Field<string>("CodigoColor"),
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
    }
}

