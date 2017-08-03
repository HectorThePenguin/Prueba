using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;

namespace SIE.Services.Servicios.BL
{
    public class CierreDiaInventarioBL
    {
        /// <summary>
        /// guardar cierre dia inventario
        /// </summary>
        /// <param name="datosGrid"></param>
        /// <param name="almacenCierreDiaInventario"></param>
        /// <returns></returns>
        public int GuardarCierreDiaInventario(IList<AlmacenCierreDiaInventarioInfo> datosGrid, AlmacenCierreDiaInventarioInfo almacenCierreDiaInventario)
        {
            try
            {
                int result = 0;

                int usuarioCreacionID = almacenCierreDiaInventario.UsuarioCreacionId;
                var almacenCierreFolio = new AlmacenCierreDiaInventarioInfo
                {
                    Almacen = almacenCierreDiaInventario.Almacen,
                    TipoMovimiento = TipoMovimiento.InventarioFisico.GetHashCode(),
                    FolioAlmacen = almacenCierreDiaInventario.FolioAlmacen,
                    Observaciones = almacenCierreDiaInventario.Observaciones,
                    Estatus = EstatusInventario.Pendiente.GetHashCode(),
                    UsuarioCreacionId = usuarioCreacionID,
                };

                int organizacionID = almacenCierreDiaInventario.OrganizacionId;

                List<AnimalMovimientoInfo> animalesNoReimplantados = null;
                List<ProgramacionReinplanteInfo> corralesProgramados = null;
                List<LoteInfo> lotesDestino = null;
                List<LoteInfo> lotesOrigen = null;

                var animalMovimientoBL = new AnimalMovimientoBL();
                var loteBL = new LoteBL();
                if (almacenCierreFolio.Almacen != null &&
                    almacenCierreFolio.Almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ReimplanteGanado.GetHashCode())
                {
                    var reimplanteBL = new ReimplanteBL();
                    corralesProgramados = reimplanteBL.ObtenerCorralesParaAjuste(organizacionID);
                    if (corralesProgramados != null && corralesProgramados.Any())
                    {
                        corralesProgramados.ForEach(dato =>
                        {
                            dato.OrganizacionID = organizacionID;
                            dato.UsuarioModificacionID = usuarioCreacionID;
                        });
                        List<LoteInfo> lotes = corralesProgramados.Select(id => new LoteInfo
                        {
                            LoteID = id.LoteID
                        }).ToList();
                        animalesNoReimplantados =
                                    animalMovimientoBL.ObtenerAnimalesNoReimplantadosXML(organizacionID, lotes);
                        if (animalesNoReimplantados != null && animalesNoReimplantados.Any())
                        {
                            animalesNoReimplantados.ForEach(dato =>
                            {
                                dato.TipoMovimientoID = TipoMovimiento.EntradaPorAjusteAnimal.GetHashCode();
                                dato.UsuarioCreacionID = usuarioCreacionID;
                            });
                            lotesDestino = animalesNoReimplantados.Select(lote => new LoteInfo
                                            {
                                                LoteID = lote.LoteID
                                            }).ToList();
                            lotesOrigen = animalesNoReimplantados.Select(lote => new LoteInfo
                                            {
                                                LoteID = lote.LoteIDOrigen
                                            }).ToList();
                            IEnumerable<LoteInfo> lotesEnumerable = loteBL.ObtenerPorLoteXML(lotesDestino);
                            if (lotesEnumerable != null)
                            {
                                lotesDestino = lotesEnumerable.ToList();
                            }
                            lotesEnumerable = loteBL.ObtenerPorLoteXML(lotesOrigen);
                            if (lotesEnumerable != null)
                            {
                                lotesOrigen = lotesEnumerable.ToList();
                            }
                            List<AnimalMovimientoInfo> animalesPorLoteDestino = animalesNoReimplantados
                                                                                .Select(ani => new AnimalMovimientoInfo
                                                                                                {
                                                                                                    LoteID = ani.LoteID
                                                                                                }).ToList();
                            List<AnimalMovimientoInfo> animalesPorLoteOrigen = animalesNoReimplantados
                                                                                .Select(ani => new AnimalMovimientoInfo
                                                                                {
                                                                                    LoteID = ani.LoteID
                                                                                }).ToList();
                            lotesDestino.ForEach(id =>
                                        {
                                            int cabezas = animalesPorLoteDestino.Count(loteID => loteID.LoteID == id.LoteID);
                                            id.Cabezas += cabezas;
                                            id.CabezasInicio += cabezas;
                                            id.UsuarioModificacionID = usuarioCreacionID;
                                        });
                            lotesOrigen.ForEach(id =>
                                        {
                                            int cabezas = animalesPorLoteOrigen.Count(loteID => loteID.LoteID == id.LoteID);
                                            id.Cabezas -= cabezas;
                                            id.UsuarioModificacionID = usuarioCreacionID;
                                        });
                        }
                    }
                }

                var transactionOption = new TransactionOptions();
                transactionOption.IsolationLevel = IsolationLevel.ReadUncommitted;
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                {
                    Logger.Info();
                    var cierreDiaInventarioDAL = new CierreDiaInventarioDAL();
                    var programacionReimplanteBl = new ProgramacionReimplanteBL();
                    #region AjusteDeCorralesReimplante

                    if (animalesNoReimplantados != null && animalesNoReimplantados.Any())
                    {
                        animalMovimientoBL.GuardarAnimalMovimientoXML(animalesNoReimplantados);
                        ////Se actualizan las cabezas que tiene el lote
                        loteBL.ActualizaNoCabezasEnLoteXML(lotesDestino, lotesOrigen);

                        //Se elimina la programacin reimplante del lote

                        programacionReimplanteBl.EliminarProgramacionReimplanteXML(corralesProgramados);
                    }

                    if (almacenCierreFolio.Almacen != null &&
                  almacenCierreFolio.Almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ReimplanteGanado.GetHashCode())
                    {
                        programacionReimplanteBl.CerrarProgramacionReimplante(organizacionID, usuarioCreacionID);
                    }

                    #endregion AjusteDeCorralesReimplante

                    #region GuardarCierreDeDia

                    AlmacenCierreDiaInventarioInfo resultadoAlmacenMovimiento =
                        cierreDiaInventarioDAL.GuardarAlmacenMovimiento(almacenCierreFolio);

                    almacenCierreFolio.UsuarioCreacionId = resultadoAlmacenMovimiento.UsuarioCreacionId;
                    almacenCierreFolio.AlmacenMovimientoID = resultadoAlmacenMovimiento.AlmacenMovimientoID;

                    result = cierreDiaInventarioDAL.GuardarProductosCierreDiaInventario(datosGrid,
                                                                                        almacenCierreFolio);

                    #endregion GuardarCierreDeDia

                    #region POLIZA
                    var almacenMovimientoInventarioBL = new AlmacenMovimientoBL();
                    List<ContenedorAlmacenMovimientoCierreDia> contenedorMovimientoCierreDia =
                        almacenMovimientoInventarioBL.ObtenerMovimientosInventario(almacenCierreDiaInventario.Almacen.AlmacenID,
                                                                                   almacenCierreDiaInventario.OrganizacionId);
                    if (contenedorMovimientoCierreDia != null && contenedorMovimientoCierreDia.Any())
                    {
                        contenedorMovimientoCierreDia.ForEach(
                            x => x.FolioAlmacen = almacenCierreDiaInventario.FolioAlmacen);
                        contenedorMovimientoCierreDia =
                            contenedorMovimientoCierreDia.Join(datosGrid, contenedor => contenedor.Producto.ProductoId,
                                                               grid => grid.ProductoID, (con, grd) => con).ToList();
                        var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.ConsumoProducto);
                        List<DateTime> foliosFechas =
                            contenedorMovimientoCierreDia.Select(
                                x => Convert.ToDateTime(x.AlmacenMovimiento.FechaMovimiento.ToShortDateString())).
                                Distinct().OrderBy(
                                    fecha => fecha).ToList();
                        List<ContenedorAlmacenMovimientoCierreDia> movimientosPorDia;
                        var cierreDiaFechas = new List<ContenedorAlmacenMovimientoCierreDia>();
                        for (var indexFecha = 0; indexFecha < foliosFechas.Count; indexFecha++)
                        {
                            movimientosPorDia =
                                contenedorMovimientoCierreDia.Where(
                                    x =>
                                    Convert.ToDateTime(x.AlmacenMovimiento.FechaMovimiento.ToShortDateString()).Equals(
                                        foliosFechas[indexFecha])).ToList();
                            IList<PolizaInfo> polizaConsumo = poliza.GeneraPoliza(movimientosPorDia);
                            if (polizaConsumo != null && polizaConsumo.Any())
                            {
                                var polizaBL = new PolizaBL();
                                polizaConsumo.ToList().ForEach(datos =>
                                                                   {
                                                                       datos.UsuarioCreacionID =
                                                                           almacenCierreDiaInventario.UsuarioCreacionId;
                                                                       datos.OrganizacionID =
                                                                           almacenCierreDiaInventario.OrganizacionId;
                                                                       datos.ArchivoEnviadoServidor = 1;
                                                                   });
                                polizaBL.GuardarServicioPI(polizaConsumo, TipoPoliza.ConsumoProducto);
                                cierreDiaFechas.AddRange(movimientosPorDia);
                            }
                        }
                        var almacenMovimientoBL = new AlmacenMovimientoBL();
                        cierreDiaFechas.ForEach(
                            datos => datos.Almacen.UsuarioModificacionID = almacenCierreDiaInventario.UsuarioCreacionId);
                        almacenMovimientoBL.ActualizarGeneracionPoliza(cierreDiaFechas);
                    }
                    #endregion POLIZA

                    transaction.Complete();
                }
                return result;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
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
        }
    }
}
