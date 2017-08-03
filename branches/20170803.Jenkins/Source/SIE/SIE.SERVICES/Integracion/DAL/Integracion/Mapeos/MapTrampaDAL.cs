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
    internal class MapTrampaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TrampaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TrampaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TrampaInfo
                             {
								TrampaID = info.Field<int>("TrampaID"),
								Descripcion = info.Field<string>("Descripcion"),
								Organizacion = new OrganizacionInfo
								                   {
								                       OrganizacionID = info.Field<int>("OrganizacionID"), 
                                                       Descripcion = info.Field<string>("Organizacion")
								                   },
                                TipoTrampa = Convert.ToChar(info.Field<string>("TipoTrampa")),
								HostName = info.Field<string>("HostName"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TrampaInfo>
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
        internal static List<TrampaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TrampaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TrampaInfo
                             {
								TrampaID = info.Field<int>("TrampaID"),
								Descripcion = info.Field<string>("Descripcion"),
								Organizacion = new OrganizacionInfo
								                   {
								                       OrganizacionID = info.Field<int>("OrganizacionID"), 
								                   },
                                TipoTrampa = Convert.ToChar(info.Field<string>("TipoTrampa")),
								HostName = info.Field<string>("HostName"),
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
        internal static TrampaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TrampaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TrampaInfo
                             {
								TrampaID = info.Field<int>("TrampaID"),
								Descripcion = info.Field<string>("Descripcion"),
								Organizacion = new OrganizacionInfo
								                   {
								                       OrganizacionID = info.Field<int>("OrganizacionID"), 
                                                       Descripcion = info.Field<string>("Organizacion")
								                   },
                                TipoTrampa = Convert.ToChar(info.Field<string>("TipoTrampa")),
								HostName = info.Field<string>("HostName"),
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
        internal static TrampaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TrampaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TrampaInfo
                             {
								TrampaID = info.Field<int>("TrampaID"),
								Descripcion = info.Field<string>("Descripcion"),
								Organizacion = new OrganizacionInfo
								                   {
								                       OrganizacionID = info.Field<int>("OrganizacionID"), 
								                   },
                                TipoTrampa = Convert.ToChar(info.Field<string>("TipoTrampa")),
								HostName = info.Field<string>("HostName"),
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
        internal static List<TrampaInfo> ObtenerPorOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TrampaInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new TrampaInfo
                             {
                                 TrampaID = info.Field<int>("TrampaID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Organizacion = new OrganizacionInfo
                                                    {
                                                        OrganizacionID = info.Field<int>("OrganizacionID"),
                                                        Descripcion = info.Field<string>("Organizacion")
                                                    },
                                 TipoTrampa = Convert.ToChar(info["TipoTrampa"]),
                                 HostName = info.Field<string>("HostName"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Método asigna el registro de la trampa obtenido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TrampaInfo ObtenerObtenerTrampa(DataSet ds)
        {
            var trampaInfo = new TrampaInfo();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    trampaInfo.TrampaID = Convert.ToInt32(dr["TrampaID"]);
                    trampaInfo.Descripcion = Convert.ToString(dr["Descripcion"]);
                    trampaInfo.Organizacion =
                        new OrganizacionInfo
                        {
                            OrganizacionID = dr.Field<int>("OrganizacionID")
                        };
                    trampaInfo.TipoTrampa = Convert.ToChar(dr["TipoTrampa"]);
                    trampaInfo.HostName = Convert.ToString(dr["HostName"]);
                    trampaInfo.Activo = dr.Field<bool>("Activo").BoolAEnum();
                    trampaInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    trampaInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return trampaInfo;
        }

        
    }
}

