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
    public class MapConfiguracionTraspasoAlmacenDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<ConfiguracionTraspasoAlmacenInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConfiguracionTraspasoAlmacenInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionTraspasoAlmacenInfo
                             {
								ConfiguracionTraspasoAlmacenID = info.Field<int>("ConfiguracionTraspasoAlmacenID"),
								TipoAlmacenOrigen = new TipoAlmacenInfo { TipoAlmacenID = info.Field<int>("TipoAlmacenID"), Descripcion = info.Field<string>("TipoAlmacen") },
								TipoAlmacenDestino = new TipoAlmacenInfo { TipoAlmacenID = info.Field<int>("TipoAlmacenID"), Descripcion = info.Field<string>("TipoAlmacen") },
                                DiferenteOrganizacion = info.Field<bool>("DiferenteOrganizacion"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ConfiguracionTraspasoAlmacenInfo>
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
        public static List<ConfiguracionTraspasoAlmacenInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConfiguracionTraspasoAlmacenInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionTraspasoAlmacenInfo
                             {
								ConfiguracionTraspasoAlmacenID = info.Field<int>("ConfiguracionTraspasoAlmacenID"),
                                TipoAlmacenOrigen = new TipoAlmacenInfo { TipoAlmacenID = info.Field<int>("TipoAlmacenOrigenID"), Descripcion = info.Field<string>("TipoAlmacenOrigen") },
                                TipoAlmacenDestino = new TipoAlmacenInfo { TipoAlmacenID = info.Field<int>("TipoAlmacenDestinoID"), Descripcion = info.Field<string>("TipoAlmacenDestino") },
                                DiferenteOrganizacion = info.Field<bool>("DiferenteOrganizacion"),
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
        public static ConfiguracionTraspasoAlmacenInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ConfiguracionTraspasoAlmacenInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionTraspasoAlmacenInfo
                             {
								ConfiguracionTraspasoAlmacenID = info.Field<int>("ConfiguracionTraspasoAlmacenID"),
                                TipoAlmacenOrigen = new TipoAlmacenInfo { TipoAlmacenID = info.Field<int>("TipoAlmacenID"), Descripcion = info.Field<string>("TipoAlmacen") },
                                TipoAlmacenDestino = new TipoAlmacenInfo { TipoAlmacenID = info.Field<int>("TipoAlmacenID"), Descripcion = info.Field<string>("TipoAlmacen") },
                                DiferenteOrganizacion = info.Field<bool>("DiferenteOrganizacion"),
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
        public static ConfiguracionTraspasoAlmacenInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ConfiguracionTraspasoAlmacenInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionTraspasoAlmacenInfo
                             {
								ConfiguracionTraspasoAlmacenID = info.Field<int>("ConfiguracionTraspasoAlmacenID"),
                                TipoAlmacenOrigen = new TipoAlmacenInfo { TipoAlmacenID = info.Field<int>("TipoAlmacenID"), Descripcion = info.Field<string>("TipoAlmacen") },
                                TipoAlmacenDestino = new TipoAlmacenInfo { TipoAlmacenID = info.Field<int>("TipoAlmacenID"), Descripcion = info.Field<string>("TipoAlmacen") },
                                DiferenteOrganizacion = info.Field<bool>("DiferenteOrganizacion"),
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

