using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapSolicitudAutorizacionDAL
    {
        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static SolicitudAutorizacionInfo ObtenerDatosSolicitudAutorizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SolicitudAutorizacionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SolicitudAutorizacionInfo
                         {
                             SolicitudID = info.Field<int>("SolicitudID"),
                             EstatusSolicitud = info.Field<int>("EstatusSolicitud"),
                             Precio = info.Field<decimal>("Precio")
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
        /// Obtener datos de solicitud
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AutorizacionMateriaPrimaInfo ObtenerDatosSolicitudAutorizacionProgramacionMP(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                AutorizacionMateriaPrimaInfo datosAutorizacionInfo =
                    (from info in dt.AsEnumerable()
                     select
                         new AutorizacionMateriaPrimaInfo
                         {
                             AutorizacionMateriaPrimaID = info.Field<int>("AutorizacionMateriaPrimaID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             TipoAutorizacionID = info.Field<int>("TipoAutorizacionID"),
                             Folio = info.Field<long>("Folio"),
                             Justificacion = info.Field<string>("Justificacion"),
                             Lote = info.Field<int>("Lote"),
                             Precio = info.Field<decimal>("Precio"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             ProductoID = info.Field<int>("ProductoID"),
                             EstatusID = info.Field<int>("EstatusID"),
                             AlmacenID = info.Field<int>("AlmacenId"),
                             CantidadProgramada = info.Field<decimal>("CantidadProgramada"),
                             }).First();
                return datosAutorizacionInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        ///  Metodo para obtener la lista de solicitudes pendientes del movimiento de tipo precio de venta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesPrecioVenta(DataSet ds)
        {
            List<SolicitudAutorizacionPendientesInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new SolicitudAutorizacionPendientesInfo
                         {
                             Folio = info.Field<long>("Folio"),
                             AutorizacionID = info.Field<int>("AutorizacionMateriaPrimaID"),
                             Producto = info.Field<string>("DesProducto"),
                             Almacen = info.Field<string>("DesAlmacen"),
                             Lote = info.Field<int>("Lote"),
                             Costo = info.Field<decimal>("CostoUnitario"),
                             Precio = info.Field<decimal>("PrecioVenta"),
                             Justificacion = info.Field<string>("Justificacion")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        ///  Metodo para obtener la lista de solicitudes pendientes del movimiento de tipo uso de lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesUsoLote(DataSet ds)
        {
            List<SolicitudAutorizacionPendientesInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new SolicitudAutorizacionPendientesInfo
                         {
                             Folio = info.Field<long>("Folio"),
                             AutorizacionID = info.Field<int>("AutorizacionMateriaPrimaID"),
                             Producto = info.Field<string>("DesProducto"),
                             Almacen = info.Field<string>("DesAlmacen"),
                             Lote = info.Field<int>("Lote"),
                             Precio = info.Field<decimal>("CostoUnitario"),
                             LoteNuevo = info.Field<int>("LoteUtilizar"),
                             Justificacion = info.Field<string>("Justificacion")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        ///  Metodo para obtener la lista de solicitudes pendientes del movimiento de ajuste de inventario
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesAjusteInventario(DataSet ds)
        {
            List<SolicitudAutorizacionPendientesInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new SolicitudAutorizacionPendientesInfo
                         {
                             Folio = info.Field<long>("Folio"),
                             AutorizacionID = info.Field<int>("AutorizacionMateriaPrimaID"),
                             Producto = info.Field<string>("DesProducto"),
                             AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                             Almacen = info.Field<string>("DesAlmacen"),
                             Lote = info.Field<int>("Lote"),
                             Precio = info.Field<decimal>("CostoUnitario"),
                             CantidadAjuste = info.Field<decimal>("CantidadAjuste"),
                             PorcentajeAjuste = info.Field<decimal>("PorcentajeAjuste"),
                             Justificacion = info.Field<string>("Justificacion")
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
        /// Obtiene los ajustes pendientes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<DiferenciasDeInventariosInfo> ObtenerDiferenciasInventario(DataSet ds)
        {
            List<DiferenciasDeInventariosInfo> ajusteDeInventarioDiferenciasInventarioInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                ajusteDeInventarioDiferenciasInventarioInfo = (from info in dt.AsEnumerable()
                                                               select new DiferenciasDeInventariosInfo
                                                               {
                                                                   AlmacenMovimiento = new AlmacenMovimientoInfo()
                                                                   {
                                                                       AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                                                       FolioMovimiento = info.Field<long>("FolioMovimiento"),
                                                                       TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                                                       Status = info.Field<int>("Status"),
                                                                       Observaciones = info.Field<string>("Observaciones")
                                                                   },
                                                                   Almacen = new AlmacenInfo()
                                                                   {
                                                                       AlmacenID = info.Field<int>("AlmacenID"),
                                                                       Descripcion = info.Field<string>("DescripcionAlmacen")
                                                                   },
                                                                   Producto = new ProductoInfo()
                                                                   {
                                                                       ProductoId = info.Field<int>("ProductoID"),
                                                                       Descripcion = info.Field<string>("ProductoDescripcion"),
                                                                       ProductoDescripcion = info.Field<string>("ProductoDescripcion")
                                                                   },
                                                                   AlmacenInventarioLote = new AlmacenInventarioLoteInfo()
                                                                   {
                                                                       AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID"),
                                                                       Lote = info.Field<int>("Lote"),
                                                                       Cantidad = info.Field<decimal>("CantidadLote"),
                                                                       PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                                                                       Importe = info.Field<decimal>("Importe"),
                                                                       AlmacenInventario = new AlmacenInventarioInfo
                                                                       {
                                                                           AlmacenInventarioID = info.Field<int>("AlmacenInventarioID")
                                                                       }
                                                                   },
                                                                   Estatus = new EstatusInfo()
                                                                   {
                                                                       EstatusId = info.Field<int>("Status"),
                                                                       Descripcion = info.Field<string>("DescripcionEstatus")
                                                                   },
                                                                   AlmacenMovimientoDetalle = new AlmacenMovimientoDetalle()
                                                                   {
                                                                       AlmacenMovimientoDetalleID = info.Field<long>("AlmacenMovimientoDetalleID"),
                                                                       Cantidad = info.Field<decimal>("Cantidad"),
                                                                       Precio = info.Field<decimal>("Precio")
                                                                   },
                                                                   Guardado = true
                                                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return ajusteDeInventarioDiferenciasInventarioInfo;
        }

        /// <summary>
        /// Obtiene los ajustes pendientes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<DiferenciasDeInventariosInfo> ObtenerMovimientosAutorizacion(DataSet ds)
        {
            List<DiferenciasDeInventariosInfo> ajusteDeInventarioDiferenciasInventarioInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                ajusteDeInventarioDiferenciasInventarioInfo = (from info in dt.AsEnumerable()
                                                               select new DiferenciasDeInventariosInfo
                                                               {
                                                                   AlmacenMovimiento = new AlmacenMovimientoInfo()
                                                                   {
                                                                       AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                                                       FolioMovimiento = info.Field<long>("FolioMovimiento"),
                                                                       TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                                                       Status = info.Field<int>("Status"),
                                                                       Observaciones = info.Field<string>("Observaciones")
                                                                   },
                                                                   Almacen = new AlmacenInfo()
                                                                   {
                                                                       AlmacenID = info.Field<int>("AlmacenID"),
                                                                       Descripcion = info.Field<string>("DescripcionAlmacen")
                                                                   },
                                                                   Producto = new ProductoInfo()
                                                                   {
                                                                       ProductoId = info.Field<int>("ProductoID"),
                                                                       Descripcion = info.Field<string>("ProductoDescripcion"),
                                                                       ProductoDescripcion = info.Field<string>("ProductoDescripcion")
                                                                   },
                                                                   AlmacenInventarioLote = new AlmacenInventarioLoteInfo()
                                                                   {
                                                                       AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID"),
                                                                       Lote = info.Field<int>("Lote"),
                                                                       Cantidad = info.Field<decimal>("CantidadLote"),
                                                                       PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                                                                       Importe = info.Field<decimal>("Importe"),
                                                                       AlmacenInventario = new AlmacenInventarioInfo
                                                                       {
                                                                           AlmacenInventarioID = info.Field<int>("AlmacenInventarioID")
                                                                       }
                                                                   },
                                                                   Estatus = new EstatusInfo()
                                                                   {
                                                                       EstatusId = info.Field<int>("Status"),
                                                                       Descripcion = info.Field<string>("DescripcionEstatus")
                                                                   },
                                                                   AlmacenMovimientoDetalle = new AlmacenMovimientoDetalle()
                                                                   {
                                                                       AlmacenMovimientoDetalleID = info.Field<long>("AlmacenMovimientoDetalleID"),
                                                                       Cantidad = info.Field<decimal>("Cantidad"),
                                                                       Precio = info.Field<decimal>("Precio")
                                                                   },
                                                                   Guardado = true
                                                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return ajusteDeInventarioDiferenciasInventarioInfo;
        }
    }
}
