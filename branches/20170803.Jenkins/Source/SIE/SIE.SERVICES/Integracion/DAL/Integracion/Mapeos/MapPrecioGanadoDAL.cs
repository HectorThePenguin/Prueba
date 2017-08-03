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
    internal static class MapPrecioGanadoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<PrecioGanadoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<PrecioGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<PrecioGanadoInfo> lista = (from info in dt.AsEnumerable()
                                                select new PrecioGanadoInfo
                                                {
                                                    PrecioGanadoID = info.Field<int>("PrecioGanadoID"),
                                                    Organizacion = new OrganizacionInfo
                                                                       {
                                                                           OrganizacionID = info.Field<int>("OrganizacionID"), 
                                                                           Descripcion = info.Field<string>("Organizacion")
                                                                       },
                                                    TipoGanado = new TipoGanadoInfo
                                                                     {
                                                                         TipoGanadoID = info.Field<int>("TipoGanadoID"), 
                                                                         Descripcion = info.Field<string>("TipoGanado")
                                                                     },
                                                    Precio = info.Field<decimal>("Precio"),
                                                    FechaVigencia = info.Field<DateTime>("FechaVigencia"),
                                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                }).ToList();
                resultado = new ResultadoInfo<PrecioGanadoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PrecioGanadoInfo ObtenerPorID(DataSet ds)
        {
            PrecioGanadoInfo precioGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                precioGanadoInfo = (from precioInfo in dt.AsEnumerable()
                                    select new PrecioGanadoInfo
                                    {
                                        PrecioGanadoID = precioInfo.Field<int>("PrecioGanadoID"),
                                        Organizacion = new OrganizacionInfo
                                        {
                                            OrganizacionID = precioInfo.Field<int>("OrganizacionID"),
                                            Descripcion = precioInfo.Field<string>("Organizacion")
                                        },
                                        Activo = precioInfo.Field<bool>("Activo").BoolAEnum(),
                                        TipoGanado = new TipoGanadoInfo
                                        {
                                            TipoGanadoID = precioInfo.Field<int>("TipoGanadoID"),
                                            Descripcion = precioInfo.Field<string>("TipoGanado")
                                        },
                                        Precio = precioInfo.Field<decimal>("Precio"),
                                        FechaVigencia = precioInfo.Field<DateTime>("FechaVigencia")
                                    }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return precioGanadoInfo;
        }

        /// <summary>
        ///   Obtiene una lista de Choferes filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<PrecioGanadoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<PrecioGanadoInfo> result =
                    (from precioInfo in dt.AsEnumerable()
                     select
                         new PrecioGanadoInfo
                         {
                             PrecioGanadoID = precioInfo.Field<int>("PrecioGanadoID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = precioInfo.Field<int>("OrganizacionID")
                             },
                             Activo = precioInfo.Field<bool>("Activo").BoolAEnum(),
                             TipoGanado = new TipoGanadoInfo
                             {
                                 TipoGanadoID = precioInfo.Field<int>("TipoGanadoID"),
                                 Descripcion = precioInfo.Field<string>("TipoGanado")
                             },
                             Precio = precioInfo.Field<decimal>("Precio"),
                             FechaVigencia = precioInfo.Field<DateTime>("FechaVigencia")
                         }).ToList();

                return result;
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
        internal static PrecioGanadoInfo ObtenerPorOrganizacionTipoGanado(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                PrecioGanadoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new PrecioGanadoInfo
                         {
                             PrecioGanadoID = info.Field<int>("PrecioGanadoID"),
                             Organizacion = new OrganizacionInfo
                                                {
                                                    OrganizacionID = info.Field<int>("OrganizacionID"), 
                                                },
                             TipoGanado = new TipoGanadoInfo
                                              {
                                                  TipoGanadoID = info.Field<int>("TipoGanadoID"), 
                                              },
                             Precio = info.Field<decimal>("Precio"),
                             FechaVigencia = info.Field<DateTime>("FechaVigencia"),
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