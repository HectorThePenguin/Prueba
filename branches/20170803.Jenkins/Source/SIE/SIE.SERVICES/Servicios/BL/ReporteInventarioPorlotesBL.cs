//--*********** BL *************
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteInventarioPorlotesBL
    {
        /// <summary>
        /// Obtiene los datos del reporte
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="familiaId"></param>
        /// <param name="tipoAlmacenId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IList<ReporteInventarioPorLotesInfo> ObtenerReporteInventarioPorlotes(int organizacionId, int familiaId,
                                                                                       int tipoAlmacenId, DateTime fecha)
        {
            IList<ReporteInventarioPorLotesInfo> lista;
            try
            {
                Logger.Info();
                var dalRepote = new ReporteInventarioPorlotesDAL();
                lista = dalRepote.ObtenerParametrosReporteInventarioPorlotes(organizacionId, familiaId, fecha);
                if (lista != null)
                {
                    if (tipoAlmacenId == 1 || FamiliasEnum.Premezclas.GetHashCode() == familiaId)
                        //inventario propio o premezclas
                    {
                        lista = lista.Where(valor =>
                                            valor.TipoAlmancenId == TipoAlmacenEnum.MateriasPrimas.GetHashCode() ||
                                            valor.TipoAlmancenId == TipoAlmacenEnum.BodegaExterna.GetHashCode() ||
                                            valor.TipoAlmancenId == TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode()
                            ).ToList();
                    }
                    else //Bodegas de terceros
                    {
                        lista = lista.Where(valor =>
                                            valor.TipoAlmancenId == TipoAlmacenEnum.BodegaDeTerceros.GetHashCode() ||
                                            valor.TipoAlmancenId == TipoAlmacenEnum.EnTránsito.GetHashCode() 
                            ).ToList();
                    }
                    //AsignarOtrosCostosPremezcla(organizacionId, familiaId, lista);
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Asigna los costos de subproductos de
        /// premezcla a los costos ya generados
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="familiaId"></param>
        /// <param name="lista"></param>
        private void AsignarOtrosCostosPremezcla(int organizacionId, int familiaId, IList<ReporteInventarioPorLotesInfo> lista)
        {
            switch ((FamiliasEnum)familiaId)
            {
                case FamiliasEnum.Premezclas:
                    var organizacion = new OrganizacionInfo
                                           {
                                               OrganizacionID = organizacionId,
                                               Activo = EstatusEnum.Activo
                                           };
                    var almacenBL = new AlmacenBL();
                    var tiposAlmacen = new List<TipoAlmacenEnum> {TipoAlmacenEnum.MateriasPrimas};
                    List<AlmacenInfo> almacenes = almacenBL.ObtenerAlmacenPorTiposAlmacen(tiposAlmacen,
                                                                                          organizacion);
                    var almacenMovimientoBL = new AlmacenMovimientoBL();
                    IEnumerable<AlmacenMovimientoSubProductosModel> productosPremezcla =
                        lista.Select(x => new AlmacenMovimientoSubProductosModel
                                              {
                                                  FechaMovimiento =
                                                      x.FechaInicio,
                                                  ProductoID = x.ProductoId
                                              });
                    productosPremezcla = almacenMovimientoBL.ObtenerMovimientosSubProductos(productosPremezcla);
                    if (productosPremezcla != null)
                    {
                        var premezclaDetalleBL = new PremezclaBL();
                        List<PremezclaInfo> premezclas =
                            premezclaDetalleBL.ObtenerPorOrganizacionDetalle(organizacion);
                        ReporteInventarioPorLotesInfo reporteInventarioPorLotes;
                        PremezclaInfo premezcla;

                        var almacenMovimientoCostoBL = new AlmacenMovimientoCostoBL();
                        IEnumerable<AlmacenMovimientoInfo> movimientosAlmacen =
                            productosPremezcla.Select(movs => new AlmacenMovimientoInfo
                                                                  {
                                                                      AlmacenMovimientoID =
                                                                          movs.AlmacenMovimientoID
                                                                  });
                        IEnumerable<AlmacenMovimientoCostoInfo> almacenMovimientoCostos =
                            almacenMovimientoCostoBL.ObtenerAlmacenMovimientoCostoPorAlmacenMovimientoXML(
                                movimientosAlmacen);
                        for (var index = 0; index < lista.Count; index++)
                        {
                            reporteInventarioPorLotes = lista[index];
                            premezcla =
                                premezclas.FirstOrDefault(
                                    id => id.Producto.ProductoId == reporteInventarioPorLotes.ProductoId);
                            if (premezcla != null)
                            {
                                if (almacenes.Any(id => id.AlmacenID == reporteInventarioPorLotes.AlmacenId))
                                {
                                    AlmacenMovimientoSubProductosModel almacenMovimientoSubProductosModel =
                                        productosPremezcla.FirstOrDefault(
                                            prod => prod.ProductoID == premezcla.Producto.ProductoId &&
                                                    prod.FechaMovimiento.ToShortDateString().Equals(
                                                        reporteInventarioPorLotes.FechaInicio.ToShortDateString()));
                                    if (almacenMovimientoSubProductosModel != null)
                                    {
                                        IEnumerable<AlmacenMovimientoSubProductosModel> subProductos = productosPremezcla
                                            .Join(premezcla.ListaPremezclaDetalleInfos,
                                                  pp => pp.ProductoID, pd => pd.Producto.ProductoId,
                                                  (pp, pd) => pp).Where(
                                                      fechaInicio =>
                                                      fechaInicio.AlmacenMovimientoID ==
                                                      (almacenMovimientoSubProductosModel.AlmacenMovimientoID + 1) &&
                                                      fechaInicio.FechaMovimiento.ToShortDateString().Equals(
                                                          reporteInventarioPorLotes.FechaInicio.ToShortDateString()));
                                        if (subProductos != null && subProductos.Any())
                                        {
                                            decimal costos =
                                                almacenMovimientoCostos.Where(
                                                    almId =>
                                                    almId.AlmacenMovimientoId ==
                                                    almacenMovimientoSubProductosModel.AlmacenMovimientoID).Sum(
                                                        imp => imp.Importe);
                                            reporteInventarioPorLotes.CostoPromedio =
                                                (subProductos.Sum(imp => imp.Importe) +
                                                 almacenMovimientoSubProductosModel.Importe + costos);
                                            reporteInventarioPorLotes.PrecioPromedio =
                                                reporteInventarioPorLotes.CostoPromedio/
                                                almacenMovimientoSubProductosModel.Cantidad;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}