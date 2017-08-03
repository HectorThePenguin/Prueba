using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    /// <summary>
    /// Clase de logica para el reporte de materia prima
    /// </summary>
    internal class ReporteInventarioMateriaPrimaBL
    {
        /// <summary>
        /// Genera el reporte de inventario de materia prima
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="productoId"></param>
        /// <param name="almacenId"></param>
        /// <param name="lote"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        internal List<ReporteInventarioMateriaPrimaInfo> GenerarReporteInventario(int organizacionId, int productoId, int almacenId, int lote, DateTime fechaInicio, DateTime fechaFin)
        {
            List<ReporteInventarioMateriaPrimaInfo> lista;
            try
            {
                Logger.Info();
                var reporteDal = new ReporteInventarioMateriaPrimaDAL();
                lista = reporteDal.GenerarReporteInventario(organizacionId, productoId, almacenId, lote, fechaInicio,
                                                            fechaFin);
                if(lista == null)
                {
                    return null;
                }

                List<long> movimientos =
                    lista.Where(
                        mov =>
                        mov.TipoMovimientoID == TipoMovimiento.PaseProceso.GetHashCode() ||
                        mov.TipoMovimientoID == TipoMovimiento.RecepcionAProceso.GetHashCode()).Select(
                            mov => mov.AlmacenMovimientoID).ToList();
                var pesajeMateriaPrimaBL = new PesajeMateriaPrimaBL();
                if (movimientos.Any())
                {
                    List<ReporteInventarioPaseProcesoModel> listaFoliosPaseProceso =
                        pesajeMateriaPrimaBL.ObtenerFoliosPaseProceso(movimientos);
                    if (listaFoliosPaseProceso != null)
                    {
                        foreach (var movimiento in lista)
                        {
                            ReporteInventarioPaseProcesoModel movimientoEntrada =
                                listaFoliosPaseProceso.FirstOrDefault(
                                    mov => mov.AlmacenMovimientoOrigenID == movimiento.AlmacenMovimientoID);

                            if (movimientoEntrada != null)
                            {
                                movimiento.Folio = movimientoEntrada.FolioPaseProceso;
                            }
                            else
                            {
                                ReporteInventarioPaseProcesoModel movimientoSalida =
                                    listaFoliosPaseProceso.FirstOrDefault(
                                        mov => mov.AlmacenMovimientoDestinoID == movimiento.AlmacenMovimientoID);
                                if (movimientoSalida != null)
                                {
                                    movimiento.Folio = movimientoSalida.FolioPaseProceso;
                                }
                            }
                        }
                    }
                }
                AsignarCostosSubProductos(lista, productoId, organizacionId);
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
        /// Asigna los costos de los subproductos al costo
        /// ya generado
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="productoId"></param>
        /// <param name="organizacionID"> </param>
        private void AsignarCostosSubProductos(List<ReporteInventarioMateriaPrimaInfo> lista, int productoId, int organizacionID)
        {
            var productoBL = new ProductoBL();
            var producto = new ProductoInfo
                               {
                                   ProductoId = productoId
                               };
            producto = productoBL.ObtenerPorID(producto);
            if (producto != null)
            {
                switch ((FamiliasEnum)producto.Familia.FamiliaID)
                {
                    case FamiliasEnum.Premezclas:
                        var organizacion = new OrganizacionInfo
                                               {
                                                   OrganizacionID = organizacionID,
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
                                                          x.FechaMovimiento,
                                                      ProductoID = productoId
                                                  });
                        productosPremezcla = almacenMovimientoBL.ObtenerMovimientosSubProductos(productosPremezcla);
                        if (productosPremezcla != null)
                        {
                            var premezclaDetalleBL = new PremezclaBL();
                            List<PremezclaInfo> premezclas =
                                premezclaDetalleBL.ObtenerPorOrganizacionDetalle(organizacion);
                            PremezclaInfo premezcla;

                            List<ReporteInventarioMateriaPrimaInfo> entradasMateriaPrima =
                                lista.Where(entrada => entrada.CostoId > 0).ToList();
                            if (entradasMateriaPrima != null)
                            {
                                ReporteInventarioMateriaPrimaInfo entradaMateriaPrima;
                                for (var index = 0; index < entradasMateriaPrima.Count; index++)
                                {
                                    premezcla =
                                        premezclas.FirstOrDefault(
                                            id => id.Producto.ProductoId == productoId);
                                    if (premezcla != null)
                                    {
                                        entradaMateriaPrima = entradasMateriaPrima[index];
                                        int almacenID =
                                            productosPremezcla.Where(
                                                almMov =>
                                                almMov.AlmacenMovimientoID == entradaMateriaPrima.AlmacenMovimientoID).
                                                Select(id => id.AlmacenID).FirstOrDefault();
                                        if (almacenes.Any(id => id.AlmacenID == almacenID))
                                        {
                                            AlmacenMovimientoSubProductosModel almacenMovimientoSubProductosModel =
                                                productosPremezcla.FirstOrDefault(
                                                    prod => prod.ProductoID == premezcla.Producto.ProductoId &&
                                                            prod.FechaMovimiento.ToShortDateString().Equals(
                                                                entradaMateriaPrima.FechaMovimiento.ToShortDateString()));
                                            if (almacenMovimientoSubProductosModel != null)
                                            {
                                                IEnumerable<AlmacenMovimientoSubProductosModel> subProductos = productosPremezcla
                                                    .Join(premezcla.ListaPremezclaDetalleInfos,
                                                          pp => pp.ProductoID, pd => pd.Producto.ProductoId,
                                                          (pp, pd) => pp).Where(
                                                              fechaInicio =>
                                                              fechaInicio.AlmacenMovimientoID ==
                                                              (almacenMovimientoSubProductosModel.AlmacenMovimientoID +
                                                               1) &&
                                                              fechaInicio.FechaMovimiento.ToShortDateString().Equals(
                                                                  entradaMateriaPrima.FechaMovimiento.ToShortDateString()));
                                                if (subProductos != null && subProductos.Any())
                                                {
                                                    entradaMateriaPrima.ImporteSubProductos =
                                                        subProductos.Sum(imp => imp.Importe);
                                                }
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
}
