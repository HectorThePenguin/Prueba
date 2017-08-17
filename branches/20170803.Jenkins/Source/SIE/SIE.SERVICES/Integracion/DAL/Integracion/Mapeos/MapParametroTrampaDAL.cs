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
    internal class MapParametroTrampaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ParametroTrampaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ParametroTrampaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroTrampaInfo
                             {
                                 ParametroTrampaID = info.Field<int>("ParametroTrampaID"),
                                 Parametro = new ParametroInfo
                                                 {
                                                     ParametroID = info.Field<int>("ParametroID"),
                                                     Descripcion = info.Field<string>("Parametro"),
                                                     TipoParametro = new TipoParametroInfo
                                                                         {
                                                                             TipoParametroID =
                                                                                 info.Field<int>("TipoParametroID"),
                                                                             Descripcion =
                                                                                 info.Field<string>("TipoParametro")
                                                                         }
                                                 },
                                 Trampa =
                                     new TrampaInfo
                                         {
                                             TrampaID = info.Field<int>("TrampaID"),
                                             Descripcion = info.Field<string>("Trampa")
                                         },
                                 Valor = info.Field<string>("Valor"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ParametroTrampaInfo>
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
        internal static List<ParametroTrampaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ParametroTrampaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroTrampaInfo
                             {
								ParametroTrampaID = info.Field<int>("ParametroTrampaID"),
								Parametro = new ParametroInfo { ParametroID = info.Field<int>("ParametroID"), Descripcion = info.Field<string>("Parametro") },
								Trampa = new TrampaInfo { TrampaID = info.Field<int>("TrampaID"), Descripcion = info.Field<string>("Trampa") },
								Valor = info.Field<string>("Valor"),
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
        internal static ParametroTrampaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroTrampaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroTrampaInfo
                             {
                                 ParametroTrampaID = info.Field<int>("ParametroTrampaID"),
                                 Parametro =
                                     new ParametroInfo
                                         {
                                             ParametroID = info.Field<int>("ParametroID"),
                                             Descripcion = info.Field<string>("Parametro")
                                         },
                                 Trampa =
                                     new TrampaInfo
                                         {
                                             TrampaID = info.Field<int>("TrampaID"),
                                             Descripcion = info.Field<string>("Trampa")
                                         },
                                 Valor = info.Field<string>("Valor"),
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
        internal static ParametroTrampaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroTrampaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroTrampaInfo
                             {
								ParametroTrampaID = info.Field<int>("ParametroTrampaID"),
								Parametro = new ParametroInfo { ParametroID = info.Field<int>("ParametroID"), Descripcion = info.Field<string>("Parametro") },
								Trampa = new TrampaInfo { TrampaID = info.Field<int>("TrampaID"), Descripcion = info.Field<string>("Trampa") },
								Valor = info.Field<string>("Valor"),
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
        /// Obtiene un ParametroTrampaInfo por Parametro y Trampa
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ParametroTrampaInfo ObtenerPorParametroTrampa(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroTrampaInfo resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroTrampaInfo
                             {
                                 ParametroTrampaID = info.Field<int>("ParametroTrampaID"),
                                 Parametro = new ParametroInfo
                                                 {
                                                     ParametroID = info.Field<int>("ParametroID"),
                                                     Descripcion = info.Field<string>("Parametro"),
                                                     TipoParametro = new TipoParametroInfo
                                                                         {
                                                                             TipoParametroID =
                                                                                 info.Field<int>("TipoParametroID"),
                                                                             Descripcion =
                                                                                 info.Field<string>("TipoParametro")
                                                                         }
                                                 },
                                 Trampa =
                                     new TrampaInfo
                                         {
                                             TrampaID = info.Field<int>("TrampaID"),
                                             Descripcion = info.Field<string>("Trampa")
                                         },
                                 Valor = info.Field<string>("Valor"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).FirstOrDefault();
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
