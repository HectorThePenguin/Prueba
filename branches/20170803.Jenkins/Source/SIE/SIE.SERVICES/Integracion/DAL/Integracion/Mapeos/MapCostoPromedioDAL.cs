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
    internal  class MapCostoPromedioDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CostoPromedioInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CostoPromedioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CostoPromedioInfo
                             {
								CostoPromedioID = info.Field<int>("CostoPromedioID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								Costo = new CostoInfo
								            {
								                CostoID = info.Field<int>("CostoID"), 
                                                Descripcion = info.Field<string>("Costo"),
                                                ClaveContable = info.Field<string>("ClaveContable")
								            },
								Importe = info.Field<decimal>("Importe"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CostoPromedioInfo>
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
        internal  static List<CostoPromedioInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CostoPromedioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CostoPromedioInfo
                             {
                                 CostoPromedioID = info.Field<int>("CostoPromedioID"),
                                 Organizacion = new OrganizacionInfo
                                                    {
                                                        OrganizacionID = info.Field<int>("OrganizacionID"),
                                                    },
                                 Costo = new CostoInfo
                                             {
                                                 CostoID = info.Field<int>("CostoID"),
                                             },
                                 Importe = info.Field<decimal>("Importe"),
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
        internal  static CostoPromedioInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CostoPromedioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CostoPromedioInfo
                             {
                                 CostoPromedioID = info.Field<int>("CostoPromedioID"),
                                 Organizacion = new OrganizacionInfo
                                                    {
                                                        OrganizacionID = info.Field<int>("OrganizacionID"),
                                                    },
                                 Costo = new CostoInfo
                                             {
                                                 CostoID = info.Field<int>("CostoID"),
                                             },
                                 Importe = info.Field<decimal>("Importe"),
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
        internal  static CostoPromedioInfo ObtenerPorOrganizacionCosto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CostoPromedioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CostoPromedioInfo
                             {
								CostoPromedioID = info.Field<int>("CostoPromedioID"),
								Organizacion = new OrganizacionInfo
								                   {
								                       OrganizacionID = info.Field<int>("OrganizacionID"), 
								                   },
								Costo = new CostoInfo
								            {
								                CostoID = info.Field<int>("CostoID"), 
								            },
								Importe = info.Field<decimal>("Importe"),
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

