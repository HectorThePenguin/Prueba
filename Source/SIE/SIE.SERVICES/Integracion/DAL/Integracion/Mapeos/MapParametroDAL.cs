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
    internal static class MapParametroDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ParametroInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ParametroInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroInfo
                             {
								ParametroID = info.Field<int>("ParametroID"),
								TipoParametro = new TipoParametroInfo
								    {
								        TipoParametroID = info.Field<int>("TipoParametroID"), 
                                        Descripcion = info.Field<string>("TipoParametro")
								    },
								Descripcion = info.Field<string>("Descripcion"),
								Clave = info.Field<string>("Clave"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ParametroInfo>
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
        internal static List<ParametroInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ParametroInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroInfo
                             {
								ParametroID = info.Field<int>("ParametroID"),
								TipoParametro = new TipoParametroInfo
								    {
								        TipoParametroID = info.Field<int>("TipoParametroID"), 
                                        Descripcion = info.Field<string>("TipoParametro")
								    },
								Descripcion = info.Field<string>("Descripcion"),
								Clave = info.Field<string>("Clave"),
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
        internal static ParametroInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroInfo
                             {
								ParametroID = info.Field<int>("ParametroID"),
								TipoParametro = new TipoParametroInfo
								    {
								        TipoParametroID = info.Field<int>("TipoParametroID"), 
                                        Descripcion = info.Field<string>("TipoParametro")
								    },
								Descripcion = info.Field<string>("Descripcion"),
								Clave = info.Field<string>("Clave"),
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
        internal static ParametroInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroInfo
                             {
								ParametroID = info.Field<int>("ParametroID"),
								TipoParametro = new TipoParametroInfo
								    {
								        TipoParametroID = info.Field<int>("TipoParametroID"), 
                                        Descripcion = info.Field<string>("TipoParametro")
								    },
								Descripcion = info.Field<string>("Descripcion"),
								Clave = info.Field<string>("Clave"),
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
        internal static ParametroInfo ObtenerPorParametroTipoParametro(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroInfo
                         {
                             ParametroID = info.Field<int>("ParametroID"),
                             TipoParametro = new TipoParametroInfo
                             {
                                 TipoParametroID = info.Field<int>("TipoParametroID"),
                                 Descripcion = info.Field<string>("TipoParametro")
                             },
                             Descripcion = info.Field<string>("Descripcion"),
                             Clave = info.Field<string>("Clave"),
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

