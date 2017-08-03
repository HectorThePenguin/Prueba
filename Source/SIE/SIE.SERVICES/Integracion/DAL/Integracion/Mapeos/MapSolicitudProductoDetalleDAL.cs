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
    public class MapSolicitudProductoDetalleDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<SolicitudProductoDetalleInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SolicitudProductoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SolicitudProductoDetalleInfo
                             {
								SolicitudProductoDetalleID = info.Field<int>("SolicitudProductoDetalleID"),
								SolicitudProducto = new SolicitudProductoInfo { SolicitudProductoID = info.Field<int>("SolicitudProductoID")},
								Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
								Cantidad = info.Field<decimal>("Cantidad"),
								CamionReparto = new CamionRepartoInfo { CamionRepartoID = info.Field<int>("CamionRepartoID")},
								Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<SolicitudProductoDetalleInfo>
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
        public static List<SolicitudProductoDetalleInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SolicitudProductoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SolicitudProductoDetalleInfo
                             {
								SolicitudProductoDetalleID = info.Field<int>("SolicitudProductoDetalleID"),
                                SolicitudProducto = new SolicitudProductoInfo { SolicitudProductoID = info.Field<int>("SolicitudProductoID") },
                                Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                                Cantidad = info.Field<decimal>("Cantidad"),
                                CamionReparto = new CamionRepartoInfo { CamionRepartoID = info.Field<int>("CamionRepartoID") },
                                Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
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
        public static SolicitudProductoDetalleInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SolicitudProductoDetalleInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SolicitudProductoDetalleInfo
                             {
								SolicitudProductoDetalleID = info.Field<int>("SolicitudProductoDetalleID"),
                                SolicitudProducto = new SolicitudProductoInfo { SolicitudProductoID = info.Field<int>("SolicitudProductoID") },
                                Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                                Cantidad = info.Field<decimal>("Cantidad"),
                                CamionReparto = new CamionRepartoInfo { CamionRepartoID = info.Field<int>("CamionRepartoID") },
                                Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
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
        public static SolicitudProductoDetalleInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SolicitudProductoDetalleInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SolicitudProductoDetalleInfo
                             {
								SolicitudProductoDetalleID = info.Field<int>("SolicitudProductoDetalleID"),
                                SolicitudProducto = new SolicitudProductoInfo { SolicitudProductoID = info.Field<int>("SolicitudProductoID") },
                                Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                                Cantidad = info.Field<decimal>("Cantidad"),
                                CamionReparto = new CamionRepartoInfo { CamionRepartoID = info.Field<int>("CamionRepartoID") },
                                Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
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

