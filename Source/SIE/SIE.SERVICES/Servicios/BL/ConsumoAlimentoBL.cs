using System.Data.SqlClient;
using System.Globalization;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using System;
using System.Reflection;
using SIE.Services.Info.Info;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Polizas.Fabrica;
using Convert = System.Convert;

namespace SIE.Services.Servicios.BL
{
    public class ConsumoAlimentoBL
    {
        private List<RepartoDetalleInfo> listaRepartosDetalleGuardar = new List<RepartoDetalleInfo>();
        private List<AnimalCostoInfo> listaAnimalCostoGuardar = new List<AnimalCostoInfo>();
        private List<AnimalConsumoInfo> listaAnimalConsumoGuardar = new List<AnimalConsumoInfo>();


        /// <summary>
        /// Funcion que genera el consumo de alimento
        /// </summary>
        /// <returns>Booleano que indica si se ejecuto correctamente</returns>
        internal void GenerarConsumoAlimento(int organizacionID, DateTime fecha)
        {
            //ResultadoPolizaModel resultadoPolizaModel = null;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                var usuarioBL = new UsuarioBL();
                var animalConsumoBLTemporal = new AnimalConsumoBL();

                //Se obtiene el usuario del proceso de alimentacion
                UsuarioInfo usuario = usuarioBL.ObtenerPorActiveDirectory(UsuarioProcesosEnum.ConsumoAlimento.ToString());

                //Consulta todas las organizaciones que esten activas
                List<OrganizacionInfo> listaOrganizaciones = organizacionBL.ObtenerTodos(EstatusEnum.Activo).ToList();

                //Si se encontro el usuario
                if (usuario != null)
                {
                    if (listaOrganizaciones.Any())
                    {
                        //Se obtienen las organizaciones que son de tipo ganadera
                        List<OrganizacionInfo> listaOrganizacionesFiltrada =
                            listaOrganizaciones.Where(
                                organizacion =>
                                    organizacion.TipoOrganizacion.TipoOrganizacionID == (int)TipoOrganizacion.Ganadera)
                                .ToList();
                        if (listaOrganizacionesFiltrada.Count > 0)
                        {
                            //using (var scope = new TransactionScope())
                            //{

                            foreach (OrganizacionInfo organizacion in listaOrganizacionesFiltrada.Where(or => or.OrganizacionID == organizacionID || organizacionID == 0))
                            {

                                animalConsumoBLTemporal.CrearTablasTemporalesConsumo();
                                listaRepartosDetalleGuardar = new List<RepartoDetalleInfo>();
                                listaAnimalConsumoGuardar = new List<AnimalConsumoInfo>();
                                listaAnimalCostoGuardar = new List<AnimalCostoInfo>();
                                bool enviarBitacoraCorreo = false;
                                var almacenBL = new AlmacenBL();

                                //Se obtiene la lista de almacenes de la organizacion 
                                var listaAlmacenes =
                                    (List<AlmacenInfo>)
                                    almacenBL.ObtenerAlmacenPorOrganizacion(organizacion.OrganizacionID);

                                if (listaAlmacenes != null)
                                {
                                    //Se obtienen los almacenes de tipo general ganadera
                                    var listaAlmacenesFiltrados =
                                        listaAlmacenes.Where(
                                            almacen =>
                                            almacen.TipoAlmacen.TipoAlmacenID ==
                                            (int)TipoAlmacenEnum.PlantaDeAlimentos).ToList();


                                    if (listaAlmacenesFiltrados.Count > 0)
                                    {
                                        foreach (AlmacenInfo almacen in listaAlmacenesFiltrados)
                                        {

                                            //Se obtiene el inventario del almacen
                                            List<AlmacenInventarioInfo> almacenInventario =
                                                almacenBL.ObtenerProductosAlmacenInventario(almacen, organizacion);

                                            if (almacenInventario != null)
                                            {
                                                //Se obtienen los movimientos del almacen
                                                List<AlmacenMovimientoDetalle> almacenMovimiento =
                                                    almacenBL.ObtenerAlmacenMovimientoPorAlmacenID(almacen);

                                                if (almacenMovimiento != null)
                                                {
                                                    //Obtiene los movimientos con importe 0
                                                    almacenMovimiento =
                                                        almacenMovimiento.Where(
                                                            almacenmov => almacenmov.Importe == (decimal)0).ToList();

                                                    if (almacenMovimiento.Count > 0)
                                                    {
                                                        //Obtiene la lista de movimientos agrupados por producto
                                                        List<AlmacenMovimientoDetalle> almacenMovimientoAgrupado =
                                                            (from almacenmov in almacenMovimiento
                                                             group almacenmov by almacenmov.ProductoID
                                                                 into productosAgrupados
                                                                 select new AlmacenMovimientoDetalle
                                                                 {
                                                                     ProductoID =
                                                                         productosAgrupados.First().
                                                                         ProductoID,
                                                                     Cantidad =
                                                                         productosAgrupados.Sum(
                                                                             producto => producto.Cantidad),
                                                                     Importe =
                                                                         productosAgrupados.Sum(
                                                                             producto => producto.Importe)
                                                                 }).ToList();

                                                        if (almacenMovimientoAgrupado != null)
                                                        {
                                                            foreach (
                                                                AlmacenInventarioInfo almacenInventarioInfo in
                                                                    almacenInventario)
                                                            {
                                                                //Obtiene el producto del inventario
                                                                AlmacenMovimientoDetalle almacenProducto =
                                                                    (AlmacenMovimientoDetalle)
                                                                    almacenMovimientoAgrupado.FirstOrDefault(producto => producto.ProductoID ==
                                                                                                                         almacenInventarioInfo.ProductoID);

                                                                if (almacenProducto != null)
                                                                {
                                                                    /*Si el inventario es menor a los movimientos se envia el correo 
                                                                    y se detiene el proceso para la organizacion*/
                                                                    if (almacenInventarioInfo.Cantidad <
                                                                        almacenProducto.Cantidad)
                                                                    {
                                                                        enviarBitacoraCorreo = true;
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //Alguna cantidad no fue mayor o igual a la usada
                                                if (enviarBitacoraCorreo)
                                                {
                                                    //Registrar bitacora
                                                    RegistrarError(organizacion,
                                                                   "El inventario de productos es menor a los movimientos, almacen: " +
                                                                   almacen.Descripcion);
                                                }
                                                else
                                                {
                                                    //Seguira con el proceso para la organizacion
                                                    var repartoBL = new RepartoBL();
                                                    List<RepartoDetalleInfo> repartoDetalle =
                                                        repartoBL.ObtenerRepartoDetallePorOrganizacionID(
                                                            organizacion, fecha);

                                                    if (repartoDetalle != null && repartoDetalle.Count > 0)
                                                    {
                                                        var formulaBL = new FormulaBL();

                                                        //Se suman por formula servida
                                                        var repartoAgrupadoFormula =
                                                            (from repartoDet in repartoDetalle
                                                             where repartoDet.FormulaIDServida > 0 &&
                                                                   repartoDet.Importe == (decimal)0 &&
                                                                   repartoDet.Servido
                                                             group repartoDet by repartoDet.FormulaIDServida
                                                                 into productosAgrupados
                                                                 select new
                                                                 {
                                                                     FormulaIDServida = productosAgrupados.Key,
                                                                     CantidadServida = productosAgrupados.Sum(producto => producto.CantidadServida),
                                                                     Importe = productosAgrupados.Sum(producto => producto.Importe),
                                                                     Producto = formulaBL.ObtenerPorID(productosAgrupados.Key).Producto
                                                                 }).ToList();

                                                        var listaActualizadaProductos =
                                                            new List<AlmacenInventarioInfo>();
                                                        AlmacenInventarioInfo productoActualizar = null;
                                                        var almacenDetalleLista =
                                                            new List<AlmacenMovimientoDetalle>();


                                                        List<AlmacenInventarioInfo> repartoInventario = null;
                                                        repartoInventario =
                                                            (from repartoagrupado in repartoAgrupadoFormula
                                                             from inventarioproducto in almacenInventario
                                                             where repartoagrupado.Producto.ProductoId ==
                                                                 inventarioproducto.ProductoID &&
                                                                 repartoagrupado.CantidadServida >
                                                                 inventarioproducto.Cantidad
                                                             select inventarioproducto).ToList();


                                                        if (repartoInventario.Count > 0)
                                                        {
                                                            //Registrar bitacora
                                                            RegistrarError(organizacion,
                                                                           "El inventario de productos es menor a la cantidad repartida, almacen: " +
                                                                           almacen.Descripcion + ", organización: " +
                                                                           organizacion.Descripcion);
                                                        }
                                                        else
                                                        {
                                                            if (repartoDetalle != null)
                                                            {
                                                                //Se asigna el usuario de alimentacion
                                                                organizacion.UsuarioCreacionID = usuario.UsuarioID;

                                                                repartoDetalle =
                                                                    GenerarImporteAnimalCosto(organizacion,
                                                                                              almacenInventario,
                                                                                              repartoDetalle);
                                                                if (repartoDetalle == null)
                                                                {
                                                                    RegistrarError(organizacion,
                                                                          "Ocurrio un error al intentar generar el prorrateo del Reparto, revisar inventarios" +
                                                                          almacen.Descripcion + ", organización: " +
                                                                          organizacion.Descripcion);
                                                                    continue;
                                                                }

                                                                repartoAgrupadoFormula =
                                                                    (from repartoDet in repartoDetalle
                                                                     where repartoDet.FormulaIDServida > 0 &&
                                                                           repartoDet.AlmacenMovimientoID == 0 &&
                                                                           repartoDet.Servido
                                                                     group repartoDet by repartoDet.FormulaIDServida
                                                                         into productosAgrupados
                                                                         select new
                                                                         {
                                                                             FormulaIDServida = productosAgrupados.Key,
                                                                             CantidadServida = productosAgrupados.Sum(producto => producto.CantidadServida),
                                                                             Importe = productosAgrupados.Sum(producto => producto.Importe),
                                                                             Producto = formulaBL.ObtenerPorID(productosAgrupados.Key).Producto
                                                                         }).ToList();
                                                            }

                                                            foreach (var reparto in repartoAgrupadoFormula)
                                                            {
                                                                if (reparto.Producto != null)
                                                                {
                                                                    AlmacenInventarioInfo almacenProducto =
                                                                        almacenInventario.FirstOrDefault(
                                                                            almacenInven =>
                                                                            almacenInven.ProductoID ==
                                                                            reparto.Producto.ProductoId);

                                                                    if (almacenProducto == null)
                                                                    {
                                                                        TraceService(string.Format("no hay inventario para el producto {0} ", reparto.Producto.ProductoId));
                                                                        return;
                                                                    }

                                                                    productoActualizar = new AlmacenInventarioInfo();
                                                                    productoActualizar.ProductoID = reparto.Producto.ProductoId;
                                                                    productoActualizar.Cantidad = almacenProducto.Cantidad - reparto.CantidadServida;
                                                                    productoActualizar.Importe = almacenProducto.Cantidad * almacenProducto.PrecioPromedio;
                                                                    productoActualizar.AlmacenID = almacenProducto.AlmacenID;
                                                                    productoActualizar.PrecioPromedio = almacenProducto.PrecioPromedio;

                                                                    listaActualizadaProductos.Add(
                                                                        productoActualizar);

                                                                }
                                                            }

                                                            var animalBL = new AnimalBL();
                                                            var opciones = new TransactionOptions();
                                                            opciones.IsolationLevel = IsolationLevel.ReadUncommitted;
                                                            opciones.Timeout = new TimeSpan(2, 0, 0);
                                                            using (var scope = new TransactionScope(TransactionScopeOption.Required, opciones))
                                                            {
                                                                var movimientoGuardado = new AlmacenMovimientoInfo();
                                                                if (listaActualizadaProductos.Count > 0)
                                                                {
                                                                    movimientoGuardado = GuardarMovimientosAlmacen(almacen, repartoDetalle, almacenInventario, almacenDetalleLista);
                                                                }
                                                                GenerarPoliza(repartoDetalle, organizacion, movimientoGuardado, almacenDetalleLista, usuario, almacenMovimiento);
                                                                if (listaRepartosDetalleGuardar.Any())
                                                                {
                                                                    repartoBL.GuardarImporteXML(
                                                                        listaRepartosDetalleGuardar);
                                                                }
                                                                if (listaAnimalConsumoGuardar.Any())
                                                                {
                                                                    animalBL.GuardarAnimalConsumoBulkCopy(listaAnimalConsumoGuardar);
                                                                }
                                                                if (listaAnimalCostoGuardar.Any())
                                                                {
                                                                    animalBL.GuardarAnimalCostoBulkCopy(
                                                                        listaAnimalCostoGuardar);
                                                                }

                                                                scope.Complete();
                                                            }
                                                            opciones = new TransactionOptions();
                                                            opciones.IsolationLevel = IsolationLevel.ReadUncommitted;
                                                            opciones.Timeout = new TimeSpan(2, 0, 0);
                                                            using (var scope = new TransactionScope(TransactionScopeOption.Required, opciones))
                                                            {
                                                                animalBL.EnviarAnimalConsumoDeTemporal();
                                                                animalBL.EnviarAnimalCostoDeTemporal();
                                                                scope.Complete();
                                                            }

                                                            //var ts1 = new TimeSpan(2, 0, 0);




                                                        }
                                                    }
                                                    else
                                                    {
                                                        TraceService("No se encontraron repartos para el almacen " +
                                                                     almacen.Descripcion + " " +
                                                                     DateTime.Now.ToString(
                                                                         CultureInfo.InvariantCulture));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                TraceService("No se encontró el inventario para el almacen " +
                                                             almacen.Descripcion + organizacion.Descripcion + " " +
                                                             DateTime.Now.ToString(CultureInfo.InvariantCulture));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        TraceService(
                                            "No se encontraron almacenes del tipo correcto para la organizacion " +
                                            organizacion.Descripcion + " " +
                                            DateTime.Now.ToString(CultureInfo.InvariantCulture));
                                    }
                                }
                                else
                                {
                                    TraceService("No se encontraron almacenes para la organizacion " +
                                                 organizacion.Descripcion + " " +
                                                 DateTime.Now.ToString(CultureInfo.InvariantCulture));
                                }
                            }

                        }
                        else
                        {
                            TraceService("No se encontraron organizaciones del tipo correcto " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        }
                    }
                    else
                    {
                        TraceService("No se encontraron organizaciones " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    }
                }
                else
                {
                    TraceService("No se encontro usuario " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                }
                animalConsumoBLTemporal.CrearTablasTemporalesConsumo();
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera el importe en animal costo para los animales
        /// </summary>
        /// <param name="organizacion"></param>
        /// <param name="almacenInventario"></param>
        /// <param name="repartoDetalle"></param>
        /// <returns>booleano que indica si todo el proceso se ejecuto correctamente</returns>
        private List<RepartoDetalleInfo> GenerarImporteAnimalCosto(OrganizacionInfo organizacion, List<AlmacenInventarioInfo> almacenInventario, List<RepartoDetalleInfo> repartoDetalle)
        {
            try
            {
                var repartoBL = new RepartoBL();
                var loteBL = new LoteBL();
                var corralBL = new CorralBL();
                var animalBL = new AnimalBL();
                var formulaBL = new FormulaBL();
                var usuarioBL = new UsuarioBL();

                var usuario = new UsuarioInfo();

                usuario = usuarioBL.ObtenerPorActiveDirectory(UsuarioProcesosEnum.ConsumoAlimento.ToString());

                List<long> listaRepartosID = repartoDetalle.Select(det => det.RepartoID).Distinct().ToList();

                List<RepartoInfo> listaRepartos = repartoBL.ObtenerPorRepartosID(listaRepartosID,
                                                                                 organizacion.OrganizacionID);

                if (listaRepartos == null || !listaRepartos.Any())
                {
                    TraceService("Ocurrio un error al consultar los repartos" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    return null;
                }

                var corralesID = new List<CorralInfo>();
                listaRepartos.ForEach(rep => corralesID.Add(new CorralInfo { CorralID = rep.Corral.CorralID }));

                IList<CorralInfo> corralesReparto = corralBL.ObtenerPorCorralIdXML(corralesID);

                if (corralesReparto == null || !corralesReparto.Any())
                {
                    TraceService("Ocurrio un error al consultar los corrales" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    return null;
                }

                IList<LoteInfo> lotesReparto = loteBL.ObtenerPorOrganizacionLoteXML(organizacion.OrganizacionID,
                                                                                    corralesReparto.ToList());

                IList<FormulaInfo> formulasReparto = formulaBL.ObtenerTodos(EstatusEnum.Activo);

                if (usuario != null)
                {
                    foreach (RepartoDetalleInfo repartoDetalleInfo in repartoDetalle)
                    {
                        var reparto = new RepartoInfo();
                        var lote = new LoteInfo();
                        var corral = new CorralInfo();
                        var formula = new FormulaInfo();
                        List<AnimalInfo> animalesLote = null;

                        reparto.RepartoID = repartoDetalleInfo.RepartoID;
                        reparto.OrganizacionID = organizacion.OrganizacionID;
                        //reparto = repartoBL.ObtenerPorId(reparto);
                        reparto = listaRepartos.FirstOrDefault(rep => rep.RepartoID == reparto.RepartoID);

                        if (almacenInventario != null)
                        {
                            formula =
                                    formulasReparto.FirstOrDefault(
                                        fo => fo.FormulaId == repartoDetalleInfo.FormulaIDServida);
                            if (formula == null)
                            {
                                TraceService(string.Format("No se encontro la formula {0} ", repartoDetalleInfo.FormulaIDServida));
                                return null;
                            }

                            AlmacenInventarioInfo inventarioInfo = almacenInventario.FirstOrDefault(
                                                       producto =>
                                                           producto.ProductoID ==
                                                           formula.Producto.ProductoId);

                            if (inventarioInfo == null)
                            {
                                TraceService(string.Format("no hay inventario para el producto {0} ", formula.Producto.ProductoId));
                                return null;
                            }

                            if (reparto != null)
                            {
                                //corral = corralBL.ObtenerPorId(reparto.Corral.CorralID);
                                corral = corralesReparto.FirstOrDefault(cor => cor.CorralID == reparto.Corral.CorralID);
                                if (corral == null)
                                {
                                    continue;
                                }

                                if (reparto.LoteID <= 0)
                                {
                                    // si no tiene lote validar si el corral ya tiene lote
                                    var loteInfo = new LoteInfo
                                    {
                                        OrganizacionID = corral.OrganizacionId,
                                        CorralID = corral.CorralID
                                    };
                                    //lote = loteBL.ObtenerPorCorralID(loteInfo);
                                    lote = lotesReparto.FirstOrDefault(lot => lot.CorralID == loteInfo.CorralID);
                                    if (lote == null)
                                    {
                                        repartoDetalleInfo.Aplicado = true; //Se marca como aplicado para que se realice el descuento del inventario
                                        repartoDetalleInfo.Prorrateo = false;
                                        inventarioInfo.UsuarioModificacionID = usuario.UsuarioID;
                                        repartoDetalleInfo.LoteID = 0;
                                        repartoDetalleInfo.PrecioPromedio = inventarioInfo.PrecioPromedio;
                                        repartoDetalleInfo.UsuarioCreacionID =
                                            inventarioInfo.UsuarioModificacionID;
                                        listaRepartosDetalleGuardar.Add(repartoDetalleInfo);
                                        /* Si no tiene lote sigue con el siguiente reparto */
                                        continue;
                                    }
                                }
                                else
                                {
                                    //lote = loteBL.ObtenerPorID(reparto.LoteID);
                                    lote = lotesReparto.FirstOrDefault(lot => lot.LoteID == reparto.LoteID);
                                    if (lote == null)
                                    {
                                        repartoDetalleInfo.Aplicado = true; //Se marca como aplicado para que se realice el descuento del inventario
                                        repartoDetalleInfo.Prorrateo = false;
                                        inventarioInfo.UsuarioModificacionID = usuario.UsuarioID;
                                        repartoDetalleInfo.LoteID = 0;
                                        repartoDetalleInfo.PrecioPromedio = inventarioInfo.PrecioPromedio;
                                        repartoDetalleInfo.UsuarioCreacionID =
                                            inventarioInfo.UsuarioModificacionID;
                                        listaRepartosDetalleGuardar.Add(repartoDetalleInfo);
                                        /* Si no tiene lote sigue con el siguiente reparto */
                                        continue;
                                    }
                                }

                                //corral = corralBL.ObtenerCorralPorLoteID(lote.LoteID, organizacion.OrganizacionID);
                                //formula = formulaBL.ObtenerPorID(repartoDetalleInfo.FormulaIDServida);

                                if (formula != null)
                                {
                                    if (lote.TipoCorralID != (int)TipoCorral.Recepcion &&
                                        corral.TipoCorral.TipoCorralID != (int)TipoCorral.Recepcion)
                                    {
                                        if (repartoDetalleInfo.Importe == 0 && repartoDetalleInfo.Servido &&
                                            !repartoDetalleInfo.Prorrateo)
                                        {
                                            animalesLote = animalBL.ObtenerAnimalesPorLoteID(lote);


                                            if (inventarioInfo != null)
                                            {
                                                if (animalesLote != null && animalesLote.Count > 0)
                                                {
                                                    repartoDetalleInfo.UsuarioCreacionID = usuario.UsuarioID;
                                                    AplicarCostosYConsumosPorAnimal(lote, repartoDetalleInfo, animalesLote, inventarioInfo, reparto);
                                                }
                                                else
                                                {
                                                    repartoDetalleInfo.Prorrateo = false;
                                                    inventarioInfo.UsuarioModificacionID = usuario.UsuarioID;
                                                    repartoDetalleInfo.LoteID = lote.LoteID;
                                                    repartoDetalleInfo.PrecioPromedio = inventarioInfo.PrecioPromedio;
                                                    repartoDetalleInfo.UsuarioCreacionID =
                                                        inventarioInfo.UsuarioModificacionID;
                                                    listaRepartosDetalleGuardar.Add(repartoDetalleInfo);
                                                    //repartoBL.GuardarImporte(repartoDetalleInfo, inventarioInfo, lote);
                                                    repartoDetalleInfo.Aplicado = true;
                                                }
                                            }
                                            else
                                            {
                                                repartoDetalleInfo.Aplicado = false;
                                            }
                                        }
                                        else
                                        {
                                            repartoDetalleInfo.Aplicado = false;
                                        }
                                    }
                                    else
                                    {
                                        //AlmacenInventarioInfo inventarioInfo = almacenInventario.First(
                                        //        producto =>
                                        //            producto.ProductoID ==
                                        //            formula.Producto.ProductoId);

                                        if (inventarioInfo != null)
                                        {
                                            repartoDetalleInfo.Aplicado = false;

                                            /* Si no se a aplicado el decremento del consumo en el inventario se aplica */
                                            if (repartoDetalleInfo.Importe == 0 && repartoDetalleInfo.Servido)
                                            {
                                                repartoDetalleInfo.Prorrateo = false;
                                                inventarioInfo.UsuarioModificacionID = usuario.UsuarioID;
                                                repartoDetalleInfo.LoteID = lote.LoteID;
                                                repartoDetalleInfo.PrecioPromedio = inventarioInfo.PrecioPromedio;
                                                repartoDetalleInfo.UsuarioCreacionID =
                                                    inventarioInfo.UsuarioModificacionID;
                                                listaRepartosDetalleGuardar.Add(repartoDetalleInfo);

                                                //repartoBL.GuardarImporte(repartoDetalleInfo, inventarioInfo, lote);
                                                repartoDetalleInfo.Aplicado = true;

                                                /* Se calcula el importe*/
                                                repartoDetalleInfo.Importe = repartoDetalleInfo.CantidadServida * inventarioInfo.PrecioPromedio;

                                            }

                                            /* Prorratear los costos si ya se cortaron todas las cabezas de ese corral */
                                            if (repartoDetalleInfo.Importe > 0 && repartoDetalleInfo.Servido &&
                                                    !repartoDetalleInfo.Prorrateo)
                                            {
                                                var entradaGanadoBL = new EntradaGanadoBL();
                                                var listaEntradaGanado = new List<EntradaGanadoInfo>();

                                                /* Obtener la entrada de ganado */
                                                listaEntradaGanado = entradaGanadoBL.ObtenerEntradasPorCorralLote(lote);

                                                if (listaEntradaGanado != null && listaEntradaGanado.Count > 0)
                                                {
                                                    var entradaGanadoInfo = listaEntradaGanado.FirstOrDefault();
                                                    //Obtener los animales del inventario para cada folioEntrada
                                                    if (entradaGanadoInfo != null)
                                                    {
                                                        entradaGanadoInfo.ListaAnimal =
                                                            animalBL.ObtenerInventarioAnimalesPorFolioEntrada(
                                                                entradaGanadoInfo.FolioEntradaAgrupado,
                                                                entradaGanadoInfo.OrganizacionID);
                                                        /* Si tenemos animales en el inventario se aplica el costo */
                                                        if (entradaGanadoInfo.ListaAnimal != null &&
                                                            entradaGanadoInfo.ListaAnimal.Count > 0)
                                                        {
                                                            repartoDetalleInfo.UsuarioCreacionID = usuario.UsuarioID;
                                                            AplicarCostosYConsumosPorAnimal(lote, repartoDetalleInfo, (List<AnimalInfo>)entradaGanadoInfo.ListaAnimal, inventarioInfo, reparto);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    repartoDetalleInfo.Aplicado = false;
                                }


                            }
                            else
                            {
                                repartoDetalleInfo.Aplicado = false;
                            }
                        }
                        else
                        {
                            repartoDetalleInfo.Aplicado = false;
                        }
                    }
                    return repartoDetalle.Where(registro => registro.Aplicado).ToList();
                }
                else
                {
                    TraceService("No se encontro usuario " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return null;
        }

        /// <summary>
        /// Metodo para aplicar costos y consumos por animal
        /// </summary>
        private void AplicarCostosYConsumosPorAnimal(LoteInfo lote, RepartoDetalleInfo repartoDetalleInfo, List<AnimalInfo> animalesLote, AlmacenInventarioInfo inventarioInfo, RepartoInfo reparto)
        {
            try
            {
                //var repartoBL = new RepartoBL();
                //var animalBL = new AnimalBL();

                decimal consumoAnimal =
                            Convert.ToDecimal(Convert.ToDecimal(repartoDetalleInfo.CantidadServida) /
                                Convert.ToDecimal(animalesLote.Count));

                decimal consumoImporteAnimal = consumoAnimal *
                                                inventarioInfo.PrecioPromedio;
                AnimalCostoInfo animalCosto = null;
                AnimalConsumoInfo animalConsumo = null;
                //var listaAnimalCosto = new List<AnimalCostoInfo>();
                //var listaAnimalConsumo = new List<AnimalConsumoInfo>();
                foreach (AnimalInfo animal in animalesLote)
                {
                    /* Generar AnimalCosto */
                    animalCosto = new AnimalCostoInfo
                    {
                        CostoID = (int)Costo.Alimento,
                        TipoReferencia = TipoReferenciaAnimalCosto.Reparto,
                        FolioReferencia = repartoDetalleInfo.RepartoID,
                        AnimalID = animal.AnimalID,
                        FechaCosto = reparto.Fecha,//Se agrega la fecha del Costo que tome la del Reparto
                        Importe = consumoImporteAnimal,
                        UsuarioCreacionID = repartoDetalleInfo.UsuarioCreacionID
                    };
                    listaAnimalCostoGuardar.Add(animalCosto);
                    //listaAnimalCosto.Add(animalCosto);
                    //animalBL.GuardarAnimalCosto(animalCosto);


                    /* Generar AnimalConsumo */
                    animalConsumo = new AnimalConsumoInfo
                    {
                        AnimalId = animal.AnimalID,
                        RepartoId = repartoDetalleInfo.RepartoID,
                        FormulaServidaId = repartoDetalleInfo.FormulaIDServida,
                        Cantidad = consumoAnimal,
                        TipoServicio = (TipoServicioEnum)repartoDetalleInfo.TipoServicioID,
                        Fecha = reparto.Fecha,
                        Activo = EstatusEnum.Activo,
                        UsuarioCreacionId = repartoDetalleInfo.UsuarioCreacionID,
                        UsuarioModificacionId = repartoDetalleInfo.UsuarioCreacionID
                    };
                    listaAnimalConsumoGuardar.Add(animalConsumo);
                    //listaAnimalConsumo.Add(animalConsumo);
                }
                /* Almacenar AnimalCosto */
                //if (listaAnimalCosto != null && listaAnimalCosto.Any())
                //{
                //    animalBL.GuardarAnimalCostoXML(listaAnimalCosto);
                //}
                // Almacenar AnimalConsumoInfo
                //if (listaAnimalConsumo != null && listaAnimalConsumo.Any())
                //{
                //    animalBL.GuardarAnimalConsumoXml(listaAnimalConsumo);
                //}

                /* Aplica costos en los repartos */
                repartoDetalleInfo.Prorrateo = true;
                inventarioInfo.UsuarioModificacionID = repartoDetalleInfo.UsuarioCreacionID;
                repartoDetalleInfo.LoteID = lote.LoteID;
                repartoDetalleInfo.PrecioPromedio = inventarioInfo.PrecioPromedio;
                repartoDetalleInfo.UsuarioCreacionID = inventarioInfo.UsuarioModificacionID;

                listaRepartosDetalleGuardar.Add(repartoDetalleInfo);
                //repartoBL.GuardarImporte(repartoDetalleInfo, inventarioInfo, lote);
                repartoDetalleInfo.Aplicado = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Registra el error en la bitacora
        /// </summary>
        /// <param name="organizacion"></param>
        /// <param name="mensaje"></param>
        /// <returns>booleano que se ejecuta correctamente</returns>
        private bool RegistrarError(OrganizacionInfo organizacion, string mensaje)
        {
            bool retorno = true;
            try
            {
                var usuarioBL = new UsuarioBL();
                UsuarioInfo usuario = new UsuarioInfo();
                usuario = usuarioBL.ObtenerPorActiveDirectory(UsuarioProcesosEnum.ConsumoAlimento.ToString());

                if (usuario != null)
                {
                    /*//Esta funcion enviara el correo y generara la bitacora
                    var correoBL = new CorreoBL();
                    var correo = new CorreoInfo
                    {
                        Asunto = "Consumo Alimento",
                        Mensaje = mensaje,
                        NombreOrigen = "Sukarne",
                        AccionSiap = AccionesSIAPEnum.SerConsAli
                    };
                    correoBL.EnviarCorreoElectronicoInsidencia(organizacion, correo);*/

                    var bitacoraBL = new BitacoraIncidenciasBL();
                    BitacoraErroresInfo bitacora = new BitacoraErroresInfo();
                    bitacora.AccionesSiapID = AccionesSIAPEnum.SerConsAli;
                    bitacora.Mensaje = mensaje;
                    bitacora.UsuarioCreacionID = usuario.UsuarioID;
                    bitacoraBL.GuardarError(bitacora);
                }
                else
                {
                    TraceService("No se encontro usuario " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception ex)
            {
                retorno = false;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return retorno;
        }

        /// <summary>
        /// Guarda los movimientos del almacen
        /// </summary>
        /// <param name="almacen"></param>
        /// <param name="repartoDetalle"></param>
        /// <param name="almacenInventario"></param>
        /// <param name="listaMovimientosFinal"></param>
        /// <returns>booleano si se ejecuta correctamente</returns>
        private AlmacenMovimientoInfo GuardarMovimientosAlmacen(AlmacenInfo almacen, List<RepartoDetalleInfo> repartoDetalle, List<AlmacenInventarioInfo> almacenInventario, List<AlmacenMovimientoDetalle> listaMovimientosFinal)
        {
            var movimientoGuardado = new AlmacenMovimientoInfo();
            try
            {
                var almacenBL = new AlmacenBL();
                var usuarioBL = new UsuarioBL();
                var formulaBL = new FormulaBL();
                var repartoBL = new RepartoBL();
                UsuarioInfo usuario = usuarioBL.ObtenerPorActiveDirectory(UsuarioProcesosEnum.ConsumoAlimento.ToString());

                List<DateTime> fechasReparto = repartoDetalle.Select(det => det.FechaReparto).Distinct().ToList();

                if (usuario != null)
                {
                    foreach (var fecha in fechasReparto)
                    {
                        var almacenDetalleLista = new List<AlmacenMovimientoDetalle>();
                        var repartoAgrupadoFormula =
                                                                   (from repartoDet in repartoDetalle
                                                                    where repartoDet.FormulaIDServida > 0 &&
                                                                        //repartoDet.Importe == (decimal)0 &&
                                                                          repartoDet.AlmacenMovimientoID == 0 &&
                                                                          repartoDet.Servido &&
                                                                          repartoDet.FechaReparto == fecha
                                                                    group repartoDet by repartoDet.FormulaIDServida
                                                                        into productosAgrupados
                                                                        select new
                                                                        {
                                                                            FormulaIDServida = productosAgrupados.Key,
                                                                            CantidadServida = productosAgrupados.Sum(producto => producto.CantidadServida),
                                                                            Importe = productosAgrupados.Sum(producto => producto.Importe),
                                                                            Producto = formulaBL.ObtenerPorID(productosAgrupados.Key).Producto
                                                                        }).ToList();

                        var almacenMovimientoNuevo = new AlmacenMovimientoInfo
                        {
                            AlmacenID = almacen.AlmacenID,
                            TipoMovimientoID = (int)TipoMovimiento.AplicacionAlimento,
                            Status = (int)Estatus.AplicadoInv,
                            Observaciones = "",
                            UsuarioCreacionID = usuario.UsuarioID,
                            FechaMovimiento = fecha
                        };

                        AlmacenMovimientoInfo almacenMovimientoGenerado =
                            almacenBL.GuardarAlmacenMovimientoConFecha(almacenMovimientoNuevo);
                        movimientoGuardado = almacenMovimientoGenerado;

                        foreach (var reparto in repartoAgrupadoFormula)
                        {
                            if (reparto.Producto != null)
                            {
                                AlmacenInventarioInfo almacenProducto =
                                    almacenInventario.First(
                                        almacenInven =>
                                        almacenInven.ProductoID ==
                                        reparto.Producto.ProductoId);

                                if (almacenProducto != null)
                                {
                                    var almacenDetalle = new AlmacenMovimientoDetalle
                                    {
                                        AlmacenMovimientoID = almacenMovimientoGenerado.AlmacenMovimientoID,
                                        ProductoID = almacenProducto.ProductoID,
                                        Precio = almacenProducto.PrecioPromedio,
                                        Cantidad = reparto.CantidadServida,
                                        Importe = reparto.CantidadServida * almacenProducto.PrecioPromedio,
                                        UsuarioCreacionID = usuario.UsuarioID
                                    };
                                    almacenDetalleLista.Add(almacenDetalle);
                                    listaMovimientosFinal.Add(almacenDetalle);
                                }
                            }
                        }
                        almacenBL.GuardarAlmacenMovimientoDetalle(almacenDetalleLista, almacen.AlmacenID);

                        //Se agrega para que se actualice el AlmacenMovimiento a todos los Repartos Detalle
                        repartoDetalle.ForEach(reparto =>
                        {
                            if (reparto.AlmacenMovimientoID == 0 && reparto.FechaReparto == fecha)
                            {
                                reparto.AlmacenMovimientoID = almacenMovimientoGenerado.AlmacenMovimientoID;
                                reparto.UsuarioCreacionID = usuario.UsuarioID;
                            }
                            else
                            {
                                reparto.Aplicado = true;
                            }
                        });
                        repartoBL.ActualizarAlmacenMovimientoReparto(repartoDetalle.Where(det => det.Aplicado && det.FechaReparto == fecha).ToList());
                    }
                }
                else
                {
                    TraceService("No se encontro usuario " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception ex)
            {
                movimientoGuardado = null;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return movimientoGuardado;
        }

        /// <summary>
        /// Trazar servicio
        /// </summary>
        /// <param name="content"></param>
        public void TraceService(string content)
        {
            var fs = new FileStream(@"c:\ServicioSK_ConsumoAlimento.txt", FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }

        private void GenerarPoliza(List<RepartoDetalleInfo> repartoDetalle, OrganizacionInfo organizacion, AlmacenMovimientoInfo almacenMovimientoGenerado, List<AlmacenMovimientoDetalle> almacenDetalleLista, UsuarioInfo usuario, List<AlmacenMovimientoDetalle> almacenMovimiento)
        {
            #region Poliza

            List<DateTime> fechasReparto = repartoDetalle.Select(det => det.FechaReparto).Distinct().ToList();

            var formulaBL = new FormulaBL();
            foreach (var fecha in fechasReparto)
            {
                var repartoAgrupadoFormula = (from repartoDet in repartoDetalle
                                              where
                                                  repartoDet.FormulaIDServida > 0 &&
                                                  repartoDet.Servido && repartoDet.FechaReparto == fecha
                                              group repartoDet by repartoDet.FormulaIDServida
                                                  into productosAgrupados
                                                  select new
                                                  {
                                                      FormulaIDServida = productosAgrupados.Key,
                                                      CantidadServida =
                                                  productosAgrupados.Sum(producto => producto.CantidadServida),
                                                      Importe = productosAgrupados.Sum(producto => producto.Importe),
                                                      Producto = formulaBL.ObtenerPorID(productosAgrupados.Key).Producto
                                                  }).ToList();

                var produccionFormulas =
                    new List<ProduccionFormulaDetalleInfo>();
                repartoAgrupadoFormula.ForEach(grupo =>
                {
                    var produccionFormulaDetalle = new ProduccionFormulaDetalleInfo
                    {
                        ProduccionFormulaId = grupo.FormulaIDServida,
                        Producto = grupo.Producto,
                        OrganizacionID = organizacion.OrganizacionID,
                        AlmacenID = almacenMovimientoGenerado.AlmacenID
                    };
                    produccionFormulas.Add(produccionFormulaDetalle);
                });
                repartoDetalle = almacenDetalleLista.GroupBy(grupo => grupo.ProductoID).Select(det =>
                            new RepartoDetalleInfo
                            {
                                Importe = det.Sum(imp => imp.Importe),
                                FormulaIDServida = det.Key,
                                OrganizacionID = organizacion.OrganizacionID
                            }
                    ).ToList();
                repartoDetalle.ForEach(org => org.OrganizacionID = organizacion.OrganizacionID);
                var polizaConsumoAlimentoModel = new PolizaConsumoAlimentoModel
                {
                    Reparto = new RepartoInfo
                    {
                        Fecha = fecha,
                        DetalleReparto = repartoDetalle
                    },
                    ProduccionFormula = new ProduccionFormulaInfo
                    {
                        ProduccionFormulaDetalle = produccionFormulas
                    },
                    AlmacenMovimiento = almacenMovimientoGenerado
                };
                var polizaConsumo = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.ConsumoAlimento);
                IList<PolizaInfo> poliza = polizaConsumo.GeneraPoliza(polizaConsumoAlimentoModel);
                if (poliza != null)
                {
                    var polizaBL = new PolizaBL();
                    poliza.ToList().ForEach(datos =>
                    {
                        datos.OrganizacionID = organizacion.OrganizacionID;
                        datos.UsuarioCreacionID = usuario.UsuarioID;
                        datos.ArchivoEnviadoServidor = 1;
                    });
                    polizaBL.GuardarServicioPI(poliza, TipoPoliza.ConsumoAlimento);
                    var contenedor = new List<ContenedorAlmacenMovimientoCierreDia>();
                    if (almacenMovimiento != null && almacenMovimiento.Any())
                    {
                        almacenMovimiento.ForEach(movs =>
                        {
                            var almacenMovimientoCierreDia = new ContenedorAlmacenMovimientoCierreDia
                            {
                                AlmacenMovimiento = new AlmacenMovimientoInfo
                                {
                                    AlmacenMovimientoID = movs.AlmacenMovimientoID
                                }
                            };
                            contenedor.Add(almacenMovimientoCierreDia);
                        });
                        var almacenMovimientoBL = new AlmacenMovimientoBL();
                        contenedor.ForEach(alm =>
                        {
                            alm.Almacen = new AlmacenInfo
                            {
                                UsuarioModificacionID = usuario.UsuarioID
                            };
                        });
                        almacenMovimientoBL.ActualizarGeneracionPoliza(contenedor);
                    }
                }
            }

            #endregion Poliza
        }
    }
}
