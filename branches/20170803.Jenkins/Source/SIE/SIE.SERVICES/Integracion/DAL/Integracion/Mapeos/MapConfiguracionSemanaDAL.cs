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
    internal  class MapConfiguracionSemanaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<ConfiguracionSemanaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConfiguracionSemanaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionSemanaInfo
                             {
								ConfiguracionSemanaID = info.Field<int>("ConfiguracionSemanaID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								InicioSemana = info.Field<string>("InicioSemana").DiaSemanaAEnum(),
                                FinSemana = info.Field<string>("FinSemana").DiaSemanaAEnum(),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ConfiguracionSemanaInfo>
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
        internal  static List<ConfiguracionSemanaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConfiguracionSemanaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionSemanaInfo
                             {
								ConfiguracionSemanaID = info.Field<int>("ConfiguracionSemanaID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                                InicioSemana = info.Field<string>("InicioSemana").DiaSemanaAEnum(),
                                FinSemana = info.Field<string>("FinSemana").DiaSemanaAEnum(),
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
        internal  static ConfiguracionSemanaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ConfiguracionSemanaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionSemanaInfo
                             {
								ConfiguracionSemanaID = info.Field<int>("ConfiguracionSemanaID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                                InicioSemana = info.Field<string>("InicioSemana").DiaSemanaAEnum(),
                                FinSemana = info.Field<string>("FinSemana").DiaSemanaAEnum(),
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
        internal  static ConfiguracionSemanaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ConfiguracionSemanaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionSemanaInfo
                             {
								ConfiguracionSemanaID = info.Field<int>("ConfiguracionSemanaID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                                InicioSemana = info.Field<string>("InicioSemana").DiaSemanaAEnum(),
                                FinSemana = info.Field<string>("FinSemana").DiaSemanaAEnum(),
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
        internal  static ConfiguracionSemanaInfo ObtenerPorOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ConfiguracionSemanaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionSemanaInfo
                         {
                             ConfiguracionSemanaID = info.Field<int>("ConfiguracionSemanaID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             InicioSemana = info.Field<string>("InicioSemana").DiaSemanaAEnum(),
                             FinSemana = info.Field<string>("FinSemana").DiaSemanaAEnum(),
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
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<ConfiguracionSemanaInfo> ObtenerPorFiltroPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConfiguracionSemanaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionSemanaInfo
                         {
                             ConfiguracionSemanaID = info.Field<int>("ConfiguracionSemanaID"),
                             Organizacion = new OrganizacionInfo
                                 {
                                     OrganizacionID = info.Field<int>("OrganizacionID"), 
                                     Descripcion = info.Field<string>("Organizacion"),
                                     TipoOrganizacion = new TipoOrganizacionInfo
                                         {
                                             TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                             Descripcion = info.Field<string>("TipoOrganizacion")
                                         }
                                 },
                             InicioSemana = info.Field<string>("InicioSemana").DiaSemanaAEnum(),
                             FinSemana = info.Field<string>("FinSemana").DiaSemanaAEnum(),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ConfiguracionSemanaInfo>
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
    }
}

