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
    public class MapCamionRepartoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<CamionRepartoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CamionRepartoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CamionRepartoInfo
                             {
								CamionRepartoID = info.Field<int>("CamionRepartoID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                                CentroCosto = new CentroCostoInfo { CentroCostoID = info.Field<int>("CentroCostoID"), Descripcion = info.Field<string>("CentroCosto") },
								NumeroEconomico = info.Field<string>("NumeroEconomico"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CamionRepartoInfo>
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
        public static List<CamionRepartoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CamionRepartoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CamionRepartoInfo
                             {
								CamionRepartoID = info.Field<int>("CamionRepartoID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                                CentroCosto = new CentroCostoInfo { CentroCostoID = info.Field<int>("CentroCostoUsuarioID"), Descripcion = info.Field<string>("CentroCostoUsuario") },
								NumeroEconomico = info.Field<string>("NumeroEconomico"),
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
        public static CamionRepartoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CamionRepartoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CamionRepartoInfo
                             {
								CamionRepartoID = info.Field<int>("CamionRepartoID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                                CentroCosto = new CentroCostoInfo { CentroCostoID = info.Field<int>("CentroCostoID"), Descripcion = info.Field<string>("CentroCosto") },
								NumeroEconomico = info.Field<string>("NumeroEconomico"),
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
        public static CamionRepartoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CamionRepartoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CamionRepartoInfo
                             {
								CamionRepartoID = info.Field<int>("CamionRepartoID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                                CentroCosto = new CentroCostoInfo { CentroCostoID = info.Field<int>("CentroCostoID"), Descripcion = info.Field<string>("CentroCosto") },
								NumeroEconomico = info.Field<string>("NumeroEconomico"),
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
        public static List<CamionRepartoInfo> ObtenerPorOrganizacionID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CamionRepartoInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CamionRepartoInfo
                         {
                             CamionRepartoID = info.Field<int>("CamionRepartoID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             CentroCosto = new CentroCostoInfo { CentroCostoID = info.Field<int>("CentroCostoID"), Descripcion = info.Field<string>("CentroCosto") },
                             NumeroEconomico = info.Field<string>("NumeroEconomico"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();
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

