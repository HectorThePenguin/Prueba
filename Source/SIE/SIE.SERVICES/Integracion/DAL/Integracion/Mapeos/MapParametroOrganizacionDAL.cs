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
    internal class MapParametroOrganizacionDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ParametroOrganizacionInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ParametroOrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroOrganizacionInfo
                             {
								ParametroOrganizacionID = info.Field<int>("ParametroOrganizacionID"),
								Parametro = new ParametroInfo { ParametroID = info.Field<int>("ParametroID"), Descripcion = info.Field<string>("Parametro") },
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								Valor = info.Field<string>("Valor"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ParametroOrganizacionInfo>
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
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ParametroOrganizacionInfo> ObtenerPorFiltroPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ParametroOrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroOrganizacionInfo
                         {
                             ParametroOrganizacionID = info.Field<int>("ParametroOrganizacionID"),
                             Parametro = new ParametroInfo { ParametroID = info.Field<int>("ParametroID"), Descripcion = info.Field<string>("Parametro") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             Valor = info.Field<string>("Valor"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ParametroOrganizacionInfo>
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
        internal static List<ParametroOrganizacionInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ParametroOrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroOrganizacionInfo
                             {
								ParametroOrganizacionID = info.Field<int>("ParametroOrganizacionID"),
								Parametro = new ParametroInfo { ParametroID = info.Field<int>("ParametroID"), Descripcion = info.Field<string>("Parametro") },
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
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
        internal static ParametroOrganizacionInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroOrganizacionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroOrganizacionInfo
                             {
                                 ParametroOrganizacionID = info.Field<int>("ParametroOrganizacionID"),
                                 Parametro =
                                     new ParametroInfo
                                         {
                                             ParametroID = info.Field<int>("ParametroID"),
                                             Descripcion = info.Field<string>("Parametro")
                                         },
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
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
        internal static ParametroOrganizacionInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroOrganizacionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroOrganizacionInfo
                             {
                                 ParametroOrganizacionID = info.Field<int>("ParametroOrganizacionID"),
                                 Parametro =
                                     new ParametroInfo
                                         {
                                             ParametroID = info.Field<int>("ParametroID"),
                                             Descripcion = info.Field<string>("Parametro")
                                         },
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
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
        internal static ParametroOrganizacionInfo ObtenerPorParametroOrganizacionID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroOrganizacionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroOrganizacionInfo
                         {
                             ParametroOrganizacionID = info.Field<int>("ParametroOrganizacionID"),
                             Parametro = new ParametroInfo { ParametroID = info.Field<int>("ParametroID"), Descripcion = info.Field<string>("Parametro") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
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
        internal static ParametroOrganizacionInfo ObtenerPorOrganizacionIDClaveParametro(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ParametroOrganizacionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ParametroOrganizacionInfo
                             {
                                 ParametroOrganizacionID = info.Field<int>("ParametroOrganizacionID"),
                                 Parametro =
                                     new ParametroInfo
                                         {
                                             ParametroID = info.Field<int>("ParametroID"),
                                             Descripcion = info.Field<string>("Parametro")
                                         },
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
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

        internal static ParametroOrganizacionInfo ObtenerPorOrganizacionIDClaveParametro(IDataReader reader)
        {
            try
            {
                Logger.Info();
                ParametroOrganizacionInfo entidad = null;
                while(reader.Read())
                {
                    entidad =
                        new ParametroOrganizacionInfo
                            {
                                ParametroOrganizacionID = Convert.ToInt32(reader["ParametroOrganizacionID"]),
                                Parametro =
                                    new ParametroInfo
                                        {
                                            ParametroID = Convert.ToInt32(reader["ParametroID"]),
                                            Descripcion = Convert.ToString(reader["Parametro"])
                                        },
                                Organizacion =
                                    new OrganizacionInfo
                                        {
                                            OrganizacionID = Convert.ToInt32(reader["OrganizacionID"]),
                                            Descripcion = Convert.ToString(reader["Organizacion"])
                                        },
                                Valor = Convert.ToString(reader["Valor"]),
                                Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                            };
                }
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

