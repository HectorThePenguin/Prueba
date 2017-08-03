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
    internal class MapFleteMermaPermitidaDAL
    {
        /// <summary>
        /// Obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static FleteMermaPermitidaInfo ObtenerConfiguracion(DataSet ds)
        {
            FleteMermaPermitidaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new FleteMermaPermitidaInfo
                         {
                             FleteMermaPermitidaID = info.Field<int>("FleteMermaPermitidaID"),
                             Organizacion = new OrganizacionInfo(){OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("DescripcionOrganizacion")},
                             SubFamilia = new SubFamiliaInfo(){SubFamiliaID = info.Field<int>("SubFamiliaID")},
                             MermaPermitida = info.Field<decimal>("MermaPermitida"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
		
		/// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<FleteMermaPermitidaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FleteMermaPermitidaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FleteMermaPermitidaInfo
                             {
                                 FleteMermaPermitidaID = info.Field<int>("FleteMermaPermitidaID"),
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 SubFamilia =
                                     new SubFamiliaInfo
                                         {
                                             SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                             Descripcion = info.Field<string>("SubFamilia"),
                                             Familia = new FamiliaInfo
                                                           {
                                                               FamiliaID = info.Field<int>("FamiliaID"),
                                                               Descripcion = info.Field<string>("Familia")
                                                           }
                                         },
                                 MermaPermitida = info.Field<decimal>("MermaPermitida"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<FleteMermaPermitidaInfo>
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
        internal static List<FleteMermaPermitidaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FleteMermaPermitidaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FleteMermaPermitidaInfo
                             {
								FleteMermaPermitidaID = info.Field<int>("FleteMermaPermitidaID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								SubFamilia = new SubFamiliaInfo { SubFamiliaID = info.Field<int>("SubFamiliaID"), Descripcion = info.Field<string>("SubFamilia") },
								MermaPermitida = info.Field<decimal>("MermaPermitida"),
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
        internal static FleteMermaPermitidaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                FleteMermaPermitidaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new FleteMermaPermitidaInfo
                             {
								FleteMermaPermitidaID = info.Field<int>("FleteMermaPermitidaID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								SubFamilia = new SubFamiliaInfo { SubFamiliaID = info.Field<int>("SubFamiliaID"), Descripcion = info.Field<string>("SubFamilia") },
								MermaPermitida = info.Field<decimal>("MermaPermitida"),
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
        internal static FleteMermaPermitidaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                FleteMermaPermitidaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new FleteMermaPermitidaInfo
                             {
								FleteMermaPermitidaID = info.Field<int>("FleteMermaPermitidaID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								SubFamilia = new SubFamiliaInfo { SubFamiliaID = info.Field<int>("SubFamiliaID"), Descripcion = info.Field<string>("SubFamilia") },
								MermaPermitida = info.Field<decimal>("MermaPermitida"),
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
