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
    public class MapCheckListRoladoraGeneralDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<CheckListRoladoraGeneralInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraGeneralInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraGeneralInfo
                             {
								CheckListRoladoraGeneralID = info.Field<int>("CheckListRoladoraGeneralID"),
								Turno = info.Field<int>("Turno"),
								FechaInicio = info.Field<DateTime>("FechaInicio"),
								Observaciones = info.Field<string>("Observaciones"),
								SurfactanteInicio = info.Field<decimal>("SurfactanteInicio"),
								SurfactanteFin = info.Field<decimal>("SurfactanteFin"),
								ContadorAguaInicio = info.Field<decimal>("ContadorAguaInicio"),
								ContadorAguaFin = info.Field<decimal>("ContadorAguaFin"),
								GranoEnteroFinal = info.Field<decimal>("GranoEnteroFinal"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CheckListRoladoraGeneralInfo>
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
        public static List<CheckListRoladoraGeneralInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CheckListRoladoraGeneralInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraGeneralInfo
                             {
								CheckListRoladoraGeneralID = info.Field<int>("CheckListRoladoraGeneralID"),
								Turno = info.Field<int>("Turno"),
								FechaInicio = info.Field<DateTime>("FechaInicio"),
								Observaciones = info.Field<string>("Observaciones"),
								SurfactanteInicio = info.Field<decimal>("SurfactanteInicio"),
								SurfactanteFin = info.Field<decimal>("SurfactanteFin"),
								ContadorAguaInicio = info.Field<decimal>("ContadorAguaInicio"),
								ContadorAguaFin = info.Field<decimal>("ContadorAguaFin"),
								GranoEnteroFinal = info.Field<decimal>("GranoEnteroFinal"),
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
        public static CheckListRoladoraGeneralInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraGeneralInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraGeneralInfo
                             {
								CheckListRoladoraGeneralID = info.Field<int>("CheckListRoladoraGeneralID"),
								Turno = info.Field<int>("Turno"),
								FechaInicio = info.Field<DateTime>("FechaInicio"),
								Observaciones = info.Field<string>("Observaciones"),
								SurfactanteInicio = info.Field<decimal>("SurfactanteInicio"),
								SurfactanteFin = info.Field<decimal>("SurfactanteFin"),
								ContadorAguaInicio = info.Field<decimal>("ContadorAguaInicio"),
								ContadorAguaFin = info.Field<decimal>("ContadorAguaFin"),
								GranoEnteroFinal = info.Field<decimal>("GranoEnteroFinal"),
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
        public static CheckListRoladoraGeneralInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CheckListRoladoraGeneralInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CheckListRoladoraGeneralInfo
                             {
								CheckListRoladoraGeneralID = info.Field<int>("CheckListRoladoraGeneralID"),
								Turno = info.Field<int>("Turno"),
								FechaInicio = info.Field<DateTime>("FechaInicio"),
								Observaciones = info.Field<string>("Observaciones"),
								SurfactanteInicio = info.Field<decimal>("SurfactanteInicio"),
								SurfactanteFin = info.Field<decimal>("SurfactanteFin"),
								ContadorAguaInicio = info.Field<decimal>("ContadorAguaInicio"),
								ContadorAguaFin = info.Field<decimal>("ContadorAguaFin"),
								GranoEnteroFinal = info.Field<decimal>("GranoEnteroFinal"),
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

