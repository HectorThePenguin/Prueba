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
    internal  class MapConfiguracionEmbarqueDAL
    {

        /// <summary>
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<ConfiguracionEmbarqueInfo> ObtenerTodos(DataSet ds)
        {
            List<ConfiguracionEmbarqueInfo> result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                result = (from info in dt.AsEnumerable()
                         select new ConfiguracionEmbarqueInfo
                         {
                             ConfiguracionEmbarqueID = info.Field<int>("ConfiguracionEmbarqueID"),
                             OrganizacionOrigen = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionOrigenID"),
                                 Descripcion    = info.Field<string>("Origen")
                             },

                             OrganizacionDestino = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionDestinoID"),
                                 Descripcion = info.Field<string>("Destino")
                             },

                             Kilometros = info.Field<decimal>("Kilometros"),
                             Horas = info.Field<decimal>("Horas"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ConfiguracionEmbarqueInfo ObtenerPorID(DataSet ds)
        {
            ConfiguracionEmbarqueInfo result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                result = (from info in dt.AsEnumerable()
                         select new ConfiguracionEmbarqueInfo
                                    {
                                        ConfiguracionEmbarqueID = info.Field<int>("ConfiguracionEmbarqueID"),
                                        OrganizacionOrigen = new OrganizacionInfo
                                                                 {
                                                                     OrganizacionID =
                                                                         info.Field<int>("OrganizacionOrigenID"),
                                                                     Descripcion = info.Field<string>("Origen")
                                                                 },

                                        OrganizacionDestino = new OrganizacionInfo
                                                                  {
                                                                      OrganizacionID =
                                                                          info.Field<int>("OrganizacionDestinoID"),
                                                                      Descripcion = info.Field<string>("Destino")
                                                                  },

                                        Kilometros = info.Field<decimal>("Kilometros"),
                                        Horas = info.Field<decimal>("Horas"),
                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ConfiguracionEmbarqueInfo ObtenerPorOrganizacion(DataSet ds)
        {
            ConfiguracionEmbarqueInfo result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                result = (from info in dt.AsEnumerable()
                         select new ConfiguracionEmbarqueInfo
                         {
                             ConfiguracionEmbarqueID = info.Field<int>("ConfiguracionEmbarqueID"),
                             OrganizacionOrigen = new OrganizacionInfo
                             {
                                 OrganizacionID =
                                     info.Field<int>("OrganizacionOrigenID"),
                                 Descripcion = info.Field<string>("Origen")
                             },

                             OrganizacionDestino = new OrganizacionInfo
                             {
                                 OrganizacionID =
                                     info.Field<int>("OrganizacionDestinoID"),
                                 Descripcion = info.Field<string>("Destino")
                             },

                             Kilometros = info.Field<decimal>("Kilometros"),
                             Horas = info.Field<decimal>("Horas"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<ConfiguracionEmbarqueInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConfiguracionEmbarqueInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionEmbarqueInfo
                         {
                             ConfiguracionEmbarqueID = info.Field<int>("ConfiguracionEmbarqueID"),
                             OrganizacionOrigen = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionOrigenID"), Descripcion = info.Field<string>("OrganizacionOrigen") },
                             OrganizacionDestino = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionDestinoID"), Descripcion = info.Field<string>("OrganizacionDestino") },
                             Kilometros = info.Field<decimal>("Kilometros"),
                             Horas = info.Field<decimal>("Horas"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ConfiguracionEmbarqueInfo>
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
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ConfiguracionEmbarqueDetalleInfo> ObtenerRutasPorDescripcion(DataSet ds)
        {
            List<ConfiguracionEmbarqueDetalleInfo> result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                result = (from info in dt.AsEnumerable()
                          select new ConfiguracionEmbarqueDetalleInfo
                          {
                              ConfiguracionEmbarqueDetalleID = info.Field<int>("ConfiguracionEmbarqueDetalleID"),
                              Descripcion = info.Field<string>("Descripcion"),
                              Kilometros = info.Field<decimal?>("Kilometros"),
                              Horas = info.Field<TimeSpan>("Horas")
                          }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }
    }
}