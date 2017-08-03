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
    public class MapProveedorRetencionDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<ProveedorRetencionInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProveedorRetencionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorRetencionInfo
                             {
                                 ProveedorRetencionID = info.Field<int>("ProveedorRetencionID"),
                                 Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("Proveedor") },
                                 Retencion = new RetencionInfo { RetencionID = info.Field<int>("RetencionID"), Descripcion = info.Field<string>("Retencion") },
                                 Iva = new IvaInfo { IvaID = info.Field<int>("IvaID"), Descripcion = info.Field<string>("Iva") },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProveedorRetencionInfo>
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
        public static List<ProveedorRetencionInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProveedorRetencionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorRetencionInfo
                             {
                                 ProveedorRetencionID = info.Field<int>("ProveedorRetencionID"),
                                 Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("Proveedor") },
                                 Retencion = new RetencionInfo { RetencionID = info.Field<int>("RetencionID"), Descripcion = info.Field<string>("Retencion") },
                                 Iva = new IvaInfo { IvaID = info.Field<int>("IvaID"), Descripcion = info.Field<string>("Iva") },
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
        public static ProveedorRetencionInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProveedorRetencionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorRetencionInfo
                             {
                                 ProveedorRetencionID = info.Field<int>("ProveedorRetencionID"),
                                 Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("Proveedor") },
                                 Retencion = new RetencionInfo { RetencionID = info.Field<int>("RetencionID"), Descripcion = info.Field<string>("Retencion") },
                                 Iva = new IvaInfo { IvaID = info.Field<int>("IvaID"), Descripcion = info.Field<string>("Iva") },
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
        public static ProveedorRetencionInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProveedorRetencionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorRetencionInfo
                             {
                                 ProveedorRetencionID = info.Field<int>("ProveedorRetencionID"),
                                 Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("Proveedor") },
                                 Retencion = new RetencionInfo { RetencionID = info.Field<int>("RetencionID"), Descripcion = info.Field<string>("Retencion") },
                                 Iva = new IvaInfo { IvaID = info.Field<int>("IvaID"), Descripcion = info.Field<string>("Iva") },
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<ProveedorRetencionInfo> ObtenerPorProveedorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProveedorRetencionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorRetencionInfo
                         {
                             ProveedorRetencionID = info.Field<int>("ProveedorRetencionID"),
                             Proveedor = new ProveedorInfo
                                         {
                                             ProveedorID = info.Field<int>("ProveedorID"),
                                             Descripcion = info.Field<string>("Proveedor")
                                         },
                             Retencion = new RetencionInfo
                                         {
                                             RetencionID = info.Field<int?>("RetencionID") ?? 0,
                                             TipoRetencion = info.Field<string>("TipoRetencion")
                                         },
                             Iva = new IvaInfo
                             {
                                 IvaID = info.Field<int?>("IvaID") ?? 0,
                                 Descripcion = info.Field<string>("DescripcionIva")
                             },
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
    }
}

