using System.Globalization;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Polizas.Fabrica;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AlmacenDAL = SIE.Services.Integracion.DAL.ORM.AlmacenDAL;
using SolicitudProductoDAL = SIE.Services.Integracion.DAL.ORM.SolicitudProductoDAL;
using SIE.Services.Polizas;

namespace SIE.Services.Servicios.BL
{
    public class ReimpresionPolizaBL
    {
        /// <summary>
        /// Reimprime una poliza
        /// </summary>
        /// <param name="tipoPoliza"></param>
        /// <param name="polizaModel"></param>
        /// <param name="polizaClave"> </param>
        public MemoryStream ReimprimirPoliza(TipoPoliza tipoPoliza, ReimpresionPolizaModel polizaModel
                                           , TipoPolizaInfo polizaClave)
        {
            try
            {
                Logger.Info();
                MemoryStream pdf = null;
                switch (tipoPoliza)
                {
                    case TipoPoliza.EntradaGanado:
                        pdf = ReimprimirPolizaEntrada(polizaModel.EntradaGanado, polizaClave);
                        break;
                    case TipoPoliza.ConsumoProducto:
                        //pdf = ReimprimirPolizaConsumoProducto(polizaModel.Almacen, polizaClave);
                        break;
                    case TipoPoliza.SalidaVenta:
                        pdf = ReimprimirPolizaVenta(polizaModel.VentaGanado, polizaClave);
                        break;
                    case TipoPoliza.SalidaMuerte:
                        pdf = ReimprimirPolizaMuerte(polizaModel.Animal, polizaModel.Fecha, polizaClave);
                        break;
                    case TipoPoliza.EntradaCompra:
                        pdf = ReimpresionPolizaCompra(polizaModel.ContenedorEntradaMateriaPrima, polizaClave);
                        break;
                    case TipoPoliza.SalidaTraspaso:
                        pdf = ReimpresionPolizaSalidaTraspaso(polizaModel.SolicitudProducto, polizaClave);
                        break;
                    case TipoPoliza.SalidaConsumo:
                        pdf = ReimpresionPolizaSalidaConsumo(polizaModel.SolicitudProducto, polizaClave);
                        break;
                    case TipoPoliza.ProduccionAlimento:
                        pdf = ReimpresionPolizaProduccionAlimento(polizaModel.ProduccionFormula, polizaClave);
                        break;
                    case TipoPoliza.SalidaVentaProducto:
                        pdf = ReimpresionSalidaVentaProducto(polizaModel.SalidaProducto, polizaClave);
                        break;
                }
                return pdf;
            }
            catch (ExcepcionServicio)
            {
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

        /// <summary>
        /// Reimprime una poliza
        /// </summary>
        /// <param name="tipoPoliza"></param>
        /// <param name="polizaModel"></param>
        /// <param name="polizaClave"> </param>
        public IList<ResultadoPolizaModel> ReimprimirMultiplePoliza(TipoPoliza tipoPoliza, ReimpresionPolizaModel polizaModel
                                           , TipoPolizaInfo polizaClave)
        {
            try
            {
                Logger.Info();
                IList<ResultadoPolizaModel> pdf = null;
                switch (tipoPoliza)
                {                    
                    case TipoPoliza.PaseProceso:
                        pdf = ReimpresionPolizaPaseProceso(polizaModel.Pedido, polizaClave);
                        break;
                }
                return pdf;
            }
            catch (ExcepcionServicio)
            {
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

        /// <summary>
        /// Obtiene un flujo de datos
        /// para la generacion de la reimpresion
        /// de la poliza de venta de producto
        /// </summary>
        /// <param name="salidaProductoInfo"></param>
        /// <param name="polizaClave"></param>
        /// <returns></returns>
        private MemoryStream ReimpresionSalidaVentaProducto(SalidaProductoInfo salidaProductoInfo, TipoPolizaInfo polizaClave)
        {
            MemoryStream pdf = null;

            var polizaBL = new PolizaBL();
            IList<PolizaInfo> polizasSalidaProducto = polizaBL.ObtenerPoliza(TipoPoliza.SalidaVentaProducto,
                                                                    salidaProductoInfo.Organizacion.OrganizacionID
                                                                    , salidaProductoInfo.FechaSalida,
                                                                    salidaProductoInfo.FolioSalida.ToString(),
                                                                    polizaClave.ClavePoliza, 1);
            if (polizasSalidaProducto != null && polizasSalidaProducto.Any())
            {
                var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaVentaProducto);
                polizasSalidaProducto = poliza.GeneraPoliza(salidaProductoInfo);
                if (polizasSalidaProducto != null && polizasSalidaProducto.Any())
                {
                    pdf = poliza.ImprimePoliza(salidaProductoInfo, polizasSalidaProducto);
                }
            }
            return pdf;
        }

        /// <summary>
        /// Obtiene el flujo de datos
        /// para la generacion de la reimpresion
        /// de la poliza de consumo de producto
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <param name="polizaClave"></param>
        /// <returns></returns>
        private MemoryStream ReimprimirPolizaConsumoProducto(AlmacenInfo almacenInfo, TipoPolizaInfo polizaClave)
        {
            MemoryStream pdf = null;

            //var polizaBL = new PolizaBL();
            //IList<PolizaInfo> polizasConsumoProducto = polizaBL.ObtenerPoliza(TipoPoliza.ConsumoProducto,
            //                                                                  almacenInfo.Organizacion.OrganizacionID,
            //                                                                  DateTime.Today,
            //                                                                  1.ToString(),
            //                                                                  polizaClave.ClavePoliza, 1);
            //if (polizasConsumoProducto != null && polizasConsumoProducto.Any())
            //{
                var almacenMovimientoInventarioBL = new AlmacenMovimientoBL();
                List<ContenedorAlmacenMovimientoCierreDia> contenedorMovimientoCierreDia =
                    almacenMovimientoInventarioBL.ObtenerMovimientosInventario(almacenInfo.AlmacenID,
                                                                               almacenInfo.Organizacion.OrganizacionID);
                if (contenedorMovimientoCierreDia != null && contenedorMovimientoCierreDia.Any())
                {
                    var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.ConsumoProducto);
                    IList<PolizaInfo> polizaConsumo = poliza.GeneraPoliza(contenedorMovimientoCierreDia);
                    if (polizaConsumo != null && polizaConsumo.Any())
                    {
                        //poliza.GuardarArchivoXML(polizaConsumo, almacenInfo.Organizacion.OrganizacionID);
                        //pdf = poliza.ImprimePoliza(contenedorMovimientoCierreDia, polizaConsumo);
                    }
                }
            //}
            return pdf;
        }

        /// <summary>
        /// Obtiene un flujo de datos
        /// para la generacion de la reimpresion
        /// de la poliza de pase a proceso
        /// </summary>
        /// <param name="pedidoInfo"></param>
        /// <param name="polizaClave"></param>
        /// <returns></returns>
        private IList<ResultadoPolizaModel> ReimpresionPolizaPaseProceso(PedidoInfo pedidoInfo, TipoPolizaInfo polizaClave)
        {
            var polizaBL = new PolizaBL();
            IList<PolizaInfo> polizasVenta = polizaBL.ObtenerPoliza(TipoPoliza.PaseProceso,
                                                                    pedidoInfo.Organizacion.OrganizacionID,
                                                                    pedidoInfo.FechaPedido,
                                                                    pedidoInfo.FolioPedido.ToString(),
                                                                    polizaClave.ClavePoliza, 1);
            IList<ResultadoPolizaModel> resultadoPolizaModel = null;
            if (polizasVenta != null)
            {
                List<int> foliosPedidos = new List<int>();
                foreach (var polizaventas in polizasVenta)
                {
                    //PP-348-1 22,350  COSTOS
                    int indiceFolio = 1;
                    var conceptos = polizaventas.Concepto.Split('-');

                    int folio;
                    int.TryParse(conceptos[indiceFolio], out folio);
                    if(folio > 0)
                    {
                        foliosPedidos.Add(folio);
                    }
                }
                foliosPedidos = foliosPedidos.Distinct().ToList();
                resultadoPolizaModel = new List<ResultadoPolizaModel>();
                foreach (var folio in foliosPedidos)
                {
                    var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PaseProceso);
                    var pesajeMateriaPrimaBL = new PesajeMateriaPrimaBL();
                    List<PolizaPaseProcesoModel> datosPoliza =
                        pesajeMateriaPrimaBL.ObtenerValoresPolizaPaseProcesoReimpresion(folio,
                                                                                        pedidoInfo.Organizacion.
                                                                                            OrganizacionID);
                    if (datosPoliza != null && datosPoliza.Any())
                    {
                        IList<PolizaPaseProcesoModel> polizasPaseProcesoModel;
                        

                        datosPoliza =
                            datosPoliza.GroupBy(
                                prod => new {prod.Producto.ProductoId, prod.ProveedorChofer.ProveedorChoferID, prod.PesajeMateriaPrima.Ticket})
                                .Select(grupo => new PolizaPaseProcesoModel
                                    {
                                        Organizacion =
                                            grupo.Select(org => org.Organizacion).FirstOrDefault(),
                                        Almacen = grupo.Select(alm => alm.Almacen).FirstOrDefault(),
                                        Producto = grupo.Select(pro => pro.Producto).FirstOrDefault(),
                                        Proveedor = grupo.Select(prov => prov.Proveedor).FirstOrDefault(),
                                        AlmacenMovimiento =
                                            grupo.Select(alm => alm.AlmacenMovimiento).FirstOrDefault(),
                                        AlmacenMovimientoDetalle =
                                            grupo.Select(alm => alm.AlmacenMovimientoDetalle).
                                                     FirstOrDefault(),
                                        AlmacenInventarioLote =
                                            grupo.Select(alm => alm.AlmacenInventarioLote).FirstOrDefault(),
                                        FleteInterno =
                                            grupo.Select(flete => flete.FleteInterno).FirstOrDefault(),
                                        FleteInternoCosto =
                                            grupo.Select(flete => flete.FleteInternoCosto).FirstOrDefault(),
                                        Pedido = grupo.Select(ped => ped.Pedido).FirstOrDefault(),
                                        ProveedorChofer = grupo.Select(prov => prov.ProveedorChofer).FirstOrDefault(),
                                        PesajeMateriaPrima =
                                            grupo.Select(pesaje => pesaje.PesajeMateriaPrima).
                                                     FirstOrDefault(),
                                        ProgramacionMateriaPrima =
                                            grupo.Select(prog => prog.ProgramacionMateriaPrima).
                                                     FirstOrDefault(),
                                        ListaAlmacenMovimientoCosto = grupo.Select(prog => prog.ListaAlmacenMovimientoCosto).
                                        FirstOrDefault(),
                                    }).OrderBy(ticket => ticket.PesajeMateriaPrima.Ticket).ToList();
                        for (int indexPoliza = 0; indexPoliza < datosPoliza.Count; indexPoliza++)
                        {
                            polizasPaseProcesoModel = new List<PolizaPaseProcesoModel> {datosPoliza[indexPoliza]};
                            polizasVenta = poliza.GeneraPoliza(polizasPaseProcesoModel);
                            MemoryStream stream = poliza.ImprimePoliza(polizasPaseProcesoModel, polizasVenta);

                            PolizaPaseProcesoModel polizaActual = polizasPaseProcesoModel.FirstOrDefault();
                            string nomenclaturaArchivo = string.Empty;
                            if(polizaActual != null)
                            {
                                nomenclaturaArchivo = String.Format("{0}-{1}", polizaActual.Pedido.FolioPedido,
                                                                    polizaActual.PesajeMateriaPrima.Ticket);
                            }
                            var resultadoPoliza = new ResultadoPolizaModel
                                {
                                    NomenclaturaArchivo = nomenclaturaArchivo,
                                    PDF = stream
                                };
                            //polizasVenta.ToList().ForEach(datos =>
                            //                                  {
                            //                                      datos.OrganizacionID = 1;
                            //                                      datos.UsuarioCreacionID = 1;
                            //                                  });
                            //polizaBL.Guardar(polizasVenta, TipoPoliza.PaseProceso);
                            resultadoPolizaModel.Add(resultadoPoliza);
                        }
                    }
                }
            }
            return resultadoPolizaModel;
        }

        /// <summary>
        /// Obtiene un flujo de datos
        /// con la poliza de compra
        /// </summary>
        /// <param name="contenedorEntradaMateriaPrima"></param>
        /// <param name="polizaClave"></param>
        /// <returns></returns>
        private MemoryStream ReimpresionPolizaCompra(ContenedorEntradaMateriaPrimaInfo contenedorEntradaMateriaPrima
                                                   , TipoPolizaInfo polizaClave)
        {
            var polizaBL = new PolizaBL();
            IList<PolizaInfo> polizasVenta = polizaBL.ObtenerPoliza(TipoPoliza.EntradaCompra,
                                                                    contenedorEntradaMateriaPrima.Contrato.Organizacion.
                                                                        OrganizacionID,
                                                                    contenedorEntradaMateriaPrima.Contrato.Fecha
                                                                    ,
                                                                    contenedorEntradaMateriaPrima.Contrato.Folio.
                                                                        ToString(),
                                                                    polizaClave.ClavePoliza, 1);
            MemoryStream stream = null;
            if (polizasVenta != null)
            {
                var entradaProductoBL = new EntradaProductoBL();
                contenedorEntradaMateriaPrima =
                    entradaProductoBL.ObtenerPorFolioEntradaContrato(contenedorEntradaMateriaPrima.Contrato.Folio,
                                                                     contenedorEntradaMateriaPrima.Contrato.ContratoId,
                                                                     contenedorEntradaMateriaPrima.Contrato.Organizacion.OrganizacionID);
                var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaCompra);
                polizasVenta = poliza.GeneraPoliza(contenedorEntradaMateriaPrima);
                stream = poliza.ImprimePoliza(contenedorEntradaMateriaPrima, polizasVenta);
            }
            return stream;
        }

        /// <summary>
        /// Obtiene los valores para la reimpresion de
        /// la poliza de salida por venta
        /// </summary>
        /// <param name="ventaGanado"></param>
        /// <param name="polizaClave"> </param>
        private MemoryStream ReimprimirPolizaVenta(VentaGanadoInfo ventaGanado, TipoPolizaInfo polizaClave)
        {
            var polizaBL = new PolizaBL();
            int organizacionID = ventaGanado.Lote.OrganizacionID;
            var ventaGanadoBL = new VentaGanadoBL();
            List<ContenedorVentaGanado> ventasGanado =
                       ventaGanadoBL.ObtenerVentaGanadoPorTicketPoliza(ventaGanado.FolioTicket, organizacionID);
            MemoryStream pdf = null;

            var tipoPoliza = TipoPoliza.SalidaVenta;

            IList<PolizaInfo> polizasVenta = polizaBL.ObtenerPoliza(tipoPoliza, organizacionID,
                                                                    ventaGanado.FechaVenta
                                                                    , ventaGanado.FolioTicket.ToString(),
                                                                    polizaClave.ClavePoliza, 1);
            var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(tipoPoliza);

            var polizaGenerada = false;
            if (polizasVenta == null)
            {
                if (ventasGanado != null && ventasGanado.Any())
                {
                    var animalCostoBL = new AnimalCostoBL();
                    var detalleVentas = new List<VentaGanadoDetalleInfo>();
                    ventasGanado.ForEach(det => detalleVentas.Add(det.VentaGanadoDetalle));

                    List<AnimalInfo> animalesGenerarPoliza = detalleVentas.Select(ani => new AnimalInfo
                                                                                    {
                                                                                        AnimalID = ani.Animal.AnimalID,
                                                                                        OrganizacionIDEntrada = organizacionID
                                                                                    }).ToList();
                    List<AnimalCostoInfo> animalCosto = animalCostoBL.ObtenerCostosAnimal(animalesGenerarPoliza);
                    if (animalCosto != null 
                        && !animalCosto.Any(id => id.CostoID == 1 && id.Importe == 0))
                    {
                        polizasVenta = poliza.GeneraPoliza(ventasGanado);
                        polizasVenta.ToList().ForEach(org => org.OrganizacionID = organizacionID);
                        polizaBL.GuardarServicioPI(polizasVenta, tipoPoliza);
                        polizaGenerada = true;
                    }
                }
            }
            if (polizasVenta != null && polizasVenta.Any())
            {
                if (ventasGanado != null && ventasGanado.Any())
                {
                    if (!polizaGenerada)
                    {
                        polizasVenta = poliza.GeneraPoliza(ventasGanado);
                    }
                    pdf = poliza.ImprimePoliza(ventasGanado, polizasVenta);
                }
            }
            return pdf;
        }

        /// <summary>
        /// Obtiene los valores para la reimpresion de
        /// la poliza de entrada de ganado
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <param name="polizaClave"> </param>
        private MemoryStream ReimprimirPolizaEntrada(EntradaGanadoInfo entradaGanado, TipoPolizaInfo polizaClave)
        {
            EntradaGanadoCosteoInfo entradaGanadoCosteo;
            var esGanadera = false;
            var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
            if (entradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
            {
                var interfaceSalidaTraspasoBL = new InterfaceSalidaTraspasoBL();
                entradaGanadoCosteo =
                    interfaceSalidaTraspasoBL.ObtenerDatosInterfaceSalidaTraspaso(entradaGanado.OrganizacionID, entradaGanado.FolioOrigen);
                esGanadera = true;

                EntradaGanadoCosteoInfo entradaGanadoCosteoComplementoCostos 
                        = entradaGanadoCosteoBL.ObtenerPorEntradaGanadoID(entradaGanado.EntradaGanadoID);
                entradaGanadoCosteo.ListaCostoEntrada.AddRange(entradaGanadoCosteoComplementoCostos.ListaCostoEntrada.Where(id => id.Costo.CostoID != 1));
            }
            else
            {
                entradaGanadoCosteo = entradaGanadoCosteoBL.ObtenerPorEntradaGanadoID(entradaGanado.EntradaGanadoID);
            }

            MemoryStream pdf = null;
            if (entradaGanadoCosteo != null)
            {
                if (!esGanadera)
                {
                    var cuentaPL = new CuentaBL();
                    const string CUENTA_INVENTARIO = "CTAINVTRAN";
                    entradaGanadoCosteo.ListaCostoEntrada.ForEach(costo =>
                    {
                        if (!string.IsNullOrWhiteSpace(costo.DescripcionCuenta))
                        {
                            return;
                        }
                        var claveContable = cuentaPL.ObtenerPorClaveCuentaOrganizacion(CUENTA_INVENTARIO,
                                                                                       entradaGanado.OrganizacionOrigenID);
                        if (claveContable != null)
                        {
                            costo.DescripcionCuenta = claveContable.Descripcion;
                        }
                    });
                }
                var contenedorCosteoEntrada = new ContenedorCosteoEntradaGanadoInfo
                                                  {
                                                      EntradaGanado = entradaGanado,
                                                      EntradaGanadoCosteo = entradaGanadoCosteo
                                                  };
                PolizaAbstract poliza;
                var tipoPoliza = esGanadera ? TipoPoliza.EntradaGanadoDurango : TipoPoliza.EntradaGanado;
                if (esGanadera)
                {
                    poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(tipoPoliza);
                }
                else
                {
                    poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(tipoPoliza);
                }                
                var polizaBL = new PolizaBL();
                int organizacionID = entradaGanado.OrganizacionID;

                IList<PolizaInfo> polizasEntrada = polizaBL.ObtenerPoliza(tipoPoliza, organizacionID,
                                                                          entradaGanado.FechaEntrada,
                                                                          entradaGanado.FolioEntrada.ToString(),
                                                                          polizaClave.ClavePoliza, 1);
                if (polizasEntrada != null)
                {
                    polizasEntrada = poliza.GeneraPoliza(contenedorCosteoEntrada);
                    pdf = poliza.ImprimePoliza(contenedorCosteoEntrada, polizasEntrada);
                }
            }
            return pdf;
        }

        /// <summary>
        /// Obtiene los valores para la reimpresion de
        /// la poliza de Salida por Muerte
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="fecha"></param>
        /// <param name="polizaClave"> </param>
        private MemoryStream ReimprimirPolizaMuerte(AnimalInfo animal, DateTime fecha
                                                  , TipoPolizaInfo polizaClave)
        {
            MemoryStream pdf = null;
            var animalCostoBL = new AnimalCostoBL();
            var animalesGenerarPoliza = new List<AnimalInfo> { animal };
            List<AnimalCostoInfo> animalCosto = animalCostoBL.ObtenerCostosAnimal(animalesGenerarPoliza);

            var animalCostoAgrupado = (from costo in animalCosto
                                       group costo by new {costo.AnimalID, costo.CostoID}
                                       into agrupado
                                       select new AnimalCostoInfo
                                           {
                                               AnimalID = agrupado.Key.AnimalID,
                                               CostoID = agrupado.Key.CostoID,
                                               Importe = agrupado.Sum(cos => cos.Importe),
                                               FolioReferencia =
                                                   agrupado.Select(cos => cos.FolioReferencia).FirstOrDefault(),
                                               FechaCosto = agrupado.Select(cos => cos.FechaCosto).FirstOrDefault()
                                           }).ToList();
            animalCosto = animalCostoAgrupado;
            if (animalCosto.Any())
            {
                int organizacionID = animal.OrganizacionIDEntrada;
                int usuarioID = animal.UsuarioCreacionID;
                animalCosto.ForEach(org =>
                                        {
                                            org.OrganizacionID = organizacionID;
                                            org.UsuarioCreacionID = usuarioID;
                                        });
                var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaMuerte);
                var polizaBL = new PolizaBL();
                for (int indexAnimal = 0; indexAnimal < animalesGenerarPoliza.Count; indexAnimal++)
                {
                    IList<PolizaInfo> polizaSalidaMuerte = polizaBL.ObtenerPoliza(TipoPoliza.SalidaMuerte,
                                                                                  organizacionID,
                                                                                  fecha,
                                                                                  animalesGenerarPoliza[indexAnimal].
                                                                                      AnimalID.ToString(),
                                                                                  polizaClave.ClavePoliza, 1);
                    animalCosto =
                        animalCosto.Where(id => id.AnimalID == animalesGenerarPoliza[indexAnimal].AnimalID).ToList();
                    if (animalCosto.Any())
                    {
                        animalCosto.ForEach(ani => ani.Arete = animalesGenerarPoliza[indexAnimal].Arete);
                    }
                    if (polizaSalidaMuerte == null)
                    {
                        if (animalCosto.Any(id => id.CostoID == 1))
                        {
                            organizacionID = animal.OrganizacionIDEntrada;
                            polizaSalidaMuerte = poliza.GeneraPoliza(animalCosto);
                            polizaSalidaMuerte.ToList().ForEach(datos =>
                            {
                                datos.OrganizacionID = organizacionID;
                                datos.UsuarioCreacionID = usuarioID;
                                datos.ArchivoEnviadoServidor = 1;
                            });
                            polizaBL.GuardarServicioPI(polizaSalidaMuerte, TipoPoliza.SalidaMuerte);
                        }
                    }
                    else
                    {
                        polizaSalidaMuerte = poliza.GeneraPoliza(animalCosto);
                    }
                    if ((polizaSalidaMuerte != null && polizaSalidaMuerte.Any()) 
                        && animalCosto.Any(id => id.CostoID == 1))
                    {
                        pdf = poliza.ImprimePoliza(animalCosto, polizaSalidaMuerte);
                    }
                }
            }
            return pdf;
        }

        /// <summary>
        /// Obtiene un flujo de datos
        /// para la generacion de la reimpresion
        /// de la poliza de Salida de Traspaso
        /// </summary>
        /// <param name="folioSolicitud"></param>
        /// <param name="polizaClave"></param>
        /// <returns></returns>
        private MemoryStream ReimpresionPolizaSalidaTraspaso(FolioSolicitudInfo folioSolicitud, TipoPolizaInfo polizaClave)
        {
            var polizaBL = new PolizaBL();
            IList<PolizaInfo> polizasSalidaTraspaso = polizaBL.ObtenerPoliza(TipoPoliza.SalidaTraspaso,
                                                                    folioSolicitud.OrganizacionID,
                                                                    folioSolicitud.FechaEntrega.HasValue ? folioSolicitud.FechaEntrega.Value : DateTime.MinValue,
                                                                    folioSolicitud.FolioSolicitud.ToString(CultureInfo.InvariantCulture),
                                                                    polizaClave.ClavePoliza, 1);
            MemoryStream stream = null;

            if (polizasSalidaTraspaso != null)
            {
                var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaTraspaso);
                var solicitudProductoDAL = new SolicitudProductoDAL();
                using (var almacenDAL = new AlmacenDAL())
                {
                    var almacenMovimientoBL = new AlmacenMovimientoBL();
                    SolicitudProductoInfo solicitud = solicitudProductoDAL.ObtenerPorID(folioSolicitud.FolioID);


                    if (solicitud != null)
                    {
                        AlmacenInfo almacenGeneral =
                            almacenDAL.ObtenerAlmacenGeneralOrganizacion(folioSolicitud.OrganizacionID);

                        AlmacenMovimientoInfo almacenMovimiento =
                            almacenMovimientoBL.ObtenerPorIDCompleto(solicitud.AlmacenMovimientoID.HasValue
                                                                 ? solicitud.AlmacenMovimientoID.Value
                                                                 : 0);

                        if (almacenMovimiento != null)
                        {
                            foreach (var detalle in solicitud.Detalle)
                            {
                                var movimiento = almacenMovimiento.ListaAlmacenMovimientoDetalle.FirstOrDefault(
                                    alm => alm.Producto.ProductoId == detalle.Producto.ProductoId);

                                if (movimiento == null)
                                {
                                    continue;
                                }
                                detalle.PrecioPromedio = movimiento.Precio;
                            }
                        }

                        if (almacenGeneral == null)
                        {
                            return null;
                        }
                        solicitud.AlmacenGeneralID = almacenGeneral.AlmacenID;
                        solicitud.Almacen.TipoAlmacen = new TipoAlmacenInfo
                            {
                                TipoAlmacenID = solicitud.Almacen.TipoAlmacenID
                            };

                        polizasSalidaTraspaso = poliza.GeneraPoliza(solicitud);
                        stream = poliza.ImprimePoliza(solicitud, polizasSalidaTraspaso);
                    }
                }
            }
            return stream;
        }

        /// <summary>
        /// Obtiene un flujo de datos
        /// para la generacion de la reimpresion
        /// de la poliza de Salida de Traspaso
        /// </summary>
        /// <param name="folioSolicitud"></param>
        /// <param name="polizaClave"></param>
        /// <returns></returns>
        private MemoryStream ReimpresionPolizaSalidaConsumo(FolioSolicitudInfo folioSolicitud, TipoPolizaInfo polizaClave)
        {
            var polizaBL = new PolizaBL();
            IList<PolizaInfo> polizasSalidaConsumo = polizaBL.ObtenerPoliza(TipoPoliza.SalidaConsumo,
                                                                    folioSolicitud.OrganizacionID,
                                                                    folioSolicitud.FechaEntrega.HasValue ? folioSolicitud.FechaEntrega.Value : DateTime.MinValue,
                                                                    folioSolicitud.FolioSolicitud.ToString(CultureInfo.InvariantCulture),
                                                                    polizaClave.ClavePoliza, 1);
            MemoryStream stream = null;

            if (polizasSalidaConsumo != null)
            {
                var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaConsumo);
                var solicitudProductoDAL = new SolicitudProductoDAL();
                using (var almacenDAL = new AlmacenDAL())
                {

                    var almacenMovimientoBL = new AlmacenMovimientoBL();
                    SolicitudProductoInfo solicitud = solicitudProductoDAL.ObtenerPorID(folioSolicitud.FolioID);


                    if (solicitud != null)
                    {
                        AlmacenInfo almacenGeneral =
                            almacenDAL.ObtenerAlmacenGeneralOrganizacion(folioSolicitud.OrganizacionID);

                        AlmacenMovimientoInfo almacenMovimiento =
                            almacenMovimientoBL.ObtenerPorIDCompleto(solicitud.AlmacenMovimientoID.HasValue
                                                                 ? solicitud.AlmacenMovimientoID.Value
                                                                 : 0);

                        if (almacenMovimiento != null)
                        {
                            foreach (var detalle in solicitud.Detalle)
                            {
                                var movimiento = almacenMovimiento.ListaAlmacenMovimientoDetalle.FirstOrDefault(
                                    alm => alm.Producto.ProductoId == detalle.Producto.ProductoId);

                                if (movimiento == null)
                                {
                                    continue;
                                }
                                detalle.PrecioPromedio = movimiento.Precio;
                            }
                        }

                        if (almacenGeneral == null)
                        {
                            return null;
                        }
                        solicitud.AlmacenGeneralID = almacenGeneral.AlmacenID;
                        polizasSalidaConsumo = poliza.GeneraPoliza(solicitud);
                        stream = poliza.ImprimePoliza(solicitud, polizasSalidaConsumo);
                    }
                }
            }
            return stream;
        }

        /// <summary>
        /// Obtiene un flujo de datos
        /// para la generacion de la reimpresion
        /// de la poliza de Salida de Traspaso
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <param name="polizaClave"></param>
        /// <returns></returns>
        private MemoryStream ReimpresionPolizaProduccionAlimento(ProduccionFormulaInfo produccionFormula, TipoPolizaInfo polizaClave)
        {
            var polizaBL = new PolizaBL();
            IList<PolizaInfo> polizasProduccionAlimento = polizaBL.ObtenerPoliza(TipoPoliza.ProduccionAlimento,
                                                                    produccionFormula.Organizacion.OrganizacionID,
                                                                    produccionFormula.FechaProduccion,
                                                                    produccionFormula.FolioFormula.ToString(CultureInfo.InvariantCulture),
                                                                    polizaClave.ClavePoliza, 1);
            MemoryStream stream = null;

            if (polizasProduccionAlimento != null)
            {
                var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.ProduccionAlimento);
                var produccionFormulaBL = new ProduccionFormulaBL();
                using (var almacenDAL = new AlmacenDAL())
                {
                    var almacenMovimientoBL = new AlmacenMovimientoBL();
                    ProduccionFormulaInfo produccion = produccionFormulaBL.ObtenerPorIDCompleto(produccionFormula.ProduccionFormulaId);


                    if (produccion != null)
                    {
                        AlmacenInfo almacenPlantaAlimento =
                            almacenDAL.ObtenerAlmacenOrganizacionTipo(produccionFormula.Organizacion.OrganizacionID, TipoAlmacenEnum.PlantaDeAlimentos);

                        AlmacenMovimientoInfo almacenMovimiento =
                            almacenMovimientoBL.ObtenerPorIDCompleto(produccion.AlmacenMovimientoSalidaID);

                        var produccionFormulaDetalles = new List<ProduccionFormulaDetalleInfo>();
                        if (almacenMovimiento != null)
                        {
                            foreach (var detalle in produccion.ProduccionFormulaDetalle)
                            {
                                var movimiento = almacenMovimiento.ListaAlmacenMovimientoDetalle.FirstOrDefault(
                                    alm => alm.Producto.ProductoId == detalle.Producto.ProductoId
                                           && alm.AlmacenInventarioLoteId == detalle.AlmacenInventarioLoteID);

                                if (movimiento == null)
                                {
                                    continue;
                                }
                                detalle.CantidadProducto = movimiento.Cantidad;
                                detalle.PrecioPromedio = movimiento.Precio;
                                produccionFormulaDetalles.Add(detalle);
                            }
                        }

                        if (almacenPlantaAlimento == null)
                        {
                            return null;
                        }
                        produccion.ProduccionFormulaDetalle = produccionFormulaDetalles;
                        produccion.Almacen = almacenPlantaAlimento;
                        polizasProduccionAlimento = poliza.GeneraPoliza(produccion);
                        stream = poliza.ImprimePoliza(produccion, polizasProduccionAlimento);
                    }
                }
            }
            return stream;
        }
    }
}
