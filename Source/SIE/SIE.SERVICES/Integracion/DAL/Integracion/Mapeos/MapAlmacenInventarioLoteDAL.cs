using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapAlmacenInventarioLoteDAL
    {
        /// <summary>
        /// Obtiene el almacen invetario lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLoteId(DataSet ds)
        {
            AlmacenInventarioLoteInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioLoteInfo
                         {
                             AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                             AlmacenInventario = new AlmacenInventarioInfo { AlmacenInventarioID = info.Field<int>("AlmacenInventarioID") },
                             Lote = info.Field<int>("Lote"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Piezas = info.Field<int>("Piezas"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                             FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                             TipoAlmacenId = info["TipoAlmacenID"] == DBNull.Value ? 0 : info.Field<int>("TipoAlmacenID"),
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                             OrganizacionId = info.Field<int>("OrganizacionID"),
                             ProductoId = info.Field<int>("ProductoID")
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
        /// Obtiene el listado de lotes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenInventarioLoteInfo> ObtenerListadoAlmacenInventarioLote(DataSet ds)
        {
            List<AlmacenInventarioLoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioLoteInfo
                         {
                             AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                             AlmacenInventario = new AlmacenInventarioInfo { AlmacenInventarioID = info.Field<int>("AlmacenInventarioID") },
                             Lote = info.Field<int>("Lote"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Piezas = info.Field<int>("Piezas"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                             FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene el listado de lotes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenInventarioLoteInfo> ObtenerListadoAlmacenInventarioLoteTipoAlmacen(DataSet ds)
        {
            List<AlmacenInventarioLoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioLoteInfo
                         {
                             AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                             AlmacenInventario = new AlmacenInventarioInfo { AlmacenInventarioID = info.Field<int>("AlmacenInventarioID")},
                             Lote = info.Field<int>("Lote"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Piezas = info.Field<int>("Piezas"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                             FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                             TipoAlmacenId = info["TipoAlmacenID"] == DBNull.Value ? 0 : info.Field<int>("TipoAlmacenID"),
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(DataSet ds)
        {
            ResultadoInfo<AlmacenInventarioLoteInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioLoteInfo
                         {
                             AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                             AlmacenInventario = new AlmacenInventarioInfo { AlmacenInventarioID = info.Field<int>("AlmacenInventarioID") },
                             Lote = info.Field<int>("Lote"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Piezas = info.Field<int>("Piezas"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                             FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                             TipoAlmacenId = info["TipoAlmacenID"] == DBNull.Value ? 0 : info.Field<int>("TipoAlmacenID"),
                             CantidadCadena = info.Field<decimal>("Cantidad").ToString(CultureInfo.InvariantCulture),
                             OrganizacionId = info.Field<int>("OrganizacionID"),
                             ProductoId = info.Field<int>("ProductoID"),
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                         }).ToList();

                resultado = new ResultadoInfo<AlmacenInventarioLoteInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        /// Obtiene un almaceninventarioloteinfo por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorLote(DataSet ds)
        {
            AlmacenInventarioLoteInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioLoteInfo
                         {
                             AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                             AlmacenInventario = new AlmacenInventarioInfo { AlmacenInventarioID = info.Field<int>("AlmacenInventarioID") },
                             Lote = info.Field<int>("Lote"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Piezas = info.Field<int>("Piezas"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                             FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                             TipoAlmacenId = info["TipoAlmacenID"] == DBNull.Value ? 0 : info.Field<int>("TipoAlmacenID"),
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
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
        /// Obtiene una lista de almacen inventario lote por folio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<AlmacenInventarioLoteInfo> ObtenerAlmacenInventarioPorLote(DataSet ds)
        {
            IList<AlmacenInventarioLoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioLoteInfo
                         {
                             AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID"),
                             Lote = info.Field<int>("Lote"),
                             Cantidad = info.Field<decimal>("Cantidad")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene el listado de lotes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProductoEnCeros(DataSet ds)
        {
            List<AlmacenInventarioLoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioLoteInfo
                         {
                             AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                             AlmacenInventario = new AlmacenInventarioInfo { AlmacenInventarioID = info.Field<int>("AlmacenInventarioID") },
                             Lote = info.Field<int>("Lote"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Piezas = info.Field<int>("Piezas"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                             FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene un almaceninventarioloteinfo por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLoteContratoID(DataSet ds)
        {
            AlmacenInventarioLoteInfo result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                result = (from info in dt.AsEnumerable()
                          select new AlmacenInventarioLoteInfo
                          {
                              AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                              AlmacenInventario = new AlmacenInventarioInfo { AlmacenInventarioID = info.Field<int>("AlmacenInventarioID") },
                              Lote = info.Field<int>("Lote"),
                              Cantidad = info.Field<decimal>("Cantidad"),
                              PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                              Piezas = info.Field<int>("Piezas"),
                              Importe = info.Field<decimal>("Importe"),
                              FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                              FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                              TipoAlmacenId = info["TipoAlmacenID"] == DBNull.Value ? 0 : info.Field<int>("TipoAlmacenID"),
                              Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                          }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Obtiene un almaceninventarioloteinfo por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLoteAlmacenCodigo(DataSet ds)
        {
            AlmacenInventarioLoteInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioLoteInfo
                         {
                             AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                             AlmacenInventario = new AlmacenInventarioInfo { AlmacenInventarioID = info.Field<int>("AlmacenInventarioID") },
                             Lote = info.Field<int>("Lote"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Piezas = info.Field<int>("Piezas"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                             FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                             TipoAlmacenId = info["TipoAlmacenID"] == DBNull.Value ? 0 : info.Field<int>("TipoAlmacenID"),
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerAlmacenInventarioLoteAlmacenPaginado(DataSet ds)
        {
            ResultadoInfo<AlmacenInventarioLoteInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista = (from info in dt.AsEnumerable()
                             select new AlmacenInventarioLoteInfo
                             {
                                 AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                                 AlmacenInventario = new AlmacenInventarioInfo
                                                         {
                                                             AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                                                             Almacen =  new AlmacenInfo
                                                                            {
                                                                                AlmacenID = info.Field<int>("AlmacenID")
                                                                            }
                                                         },
                                 Lote = info.Field<int>("Lote"),
                                 Cantidad = info.Field<decimal>("Cantidad"),
                                 PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                                 Piezas = info.Field<int>("Piezas"),
                                 Importe = info.Field<decimal>("Importe"),
                                 FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                                 FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                                 TipoAlmacenId = info["TipoAlmacenID"] == DBNull.Value ? 0 : info.Field<int>("TipoAlmacenID"),
                                 CantidadCadena = info.Field<decimal>("Cantidad").ToString(CultureInfo.InvariantCulture),
                                 OrganizacionId = info.Field<int>("OrganizacionID"),
                                 ProductoId = info.Field<int>("ProductoID"),
                                 Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                             }).ToList();

                resultado = new ResultadoInfo<AlmacenInventarioLoteInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        /// Obtiene el listado de lotes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenInventarioLoteInfo> ObtenerLotesPorAlmacenInventarioXML(DataSet ds)
        {
            List<AlmacenInventarioLoteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioLoteInfo
                         {
                             AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteId"),
                             AlmacenInventario = new AlmacenInventarioInfo { AlmacenInventarioID = info.Field<int>("AlmacenInventarioID") },
                             Lote = info.Field<int>("Lote"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Piezas = info.Field<int>("Piezas"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaInicio = info["FechaInicio"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaInicio"),
                             FechaFin = info["FechaFin"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaFin"),
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }
    }
}
