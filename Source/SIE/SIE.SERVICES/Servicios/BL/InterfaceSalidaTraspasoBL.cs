using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.Services.Servicios.BL
{
    internal class InterfaceSalidaTraspasoBL
    {
        /// <summary>
        /// Guarda una interface salida traspaso y regresa el item insertado.
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <returns></returns>
        internal InterfaceSalidaTraspasoInfo Crear(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            InterfaceSalidaTraspasoInfo result;

            try
            {
                var interfaceSalidaTraspasoDal = new InterfaceSalidaTraspasoDAL();
                result = interfaceSalidaTraspasoDal.Crear(interfaceSalidaTraspaso);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Guarda una interface salida traspaso y regresa el item insertado.
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <returns></returns>
        internal void Actualizar(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            try
            {
                var interfaceSalidaTraspasoDal = new InterfaceSalidaTraspasoDAL();
                interfaceSalidaTraspasoDal.Actualizar(interfaceSalidaTraspaso);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Guarda una lista de interface salida traspaso.
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        internal Dictionary<long, decimal> CrearLista(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            decimal costoGanado = 0;
            try
            {
                var interfaceSalidaTraspasoDal = new InterfaceSalidaTraspasoDAL();
                using (var scope = new TransactionScope())
                {
                    interfaceSalidaTraspaso.TraspasoGanado =
                        interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Select(det => det.TraspasoGanado).
                            FirstOrDefault();
                    interfaceSalidaTraspaso.SacrificioGanado =
                        interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Select(det => det.SacrificioGanado).
                            FirstOrDefault();

                    InterfaceSalidaTraspasoInfo interfaceSalidaTraspasoGuardado;

                    if (interfaceSalidaTraspaso.InterfaceSalidaTraspasoId > 0)
                    {
                        Actualizar(interfaceSalidaTraspaso);
                        interfaceSalidaTraspasoGuardado = interfaceSalidaTraspaso;
                    }
                    else
                    {
                        var folioBL = new FolioBL();
                        interfaceSalidaTraspaso.FolioTraspaso = folioBL.ObtenerFolio(interfaceSalidaTraspaso.OrganizacionId
                                                                                   , TipoFolio.TraspasoGanado);
                        interfaceSalidaTraspasoGuardado = Crear(interfaceSalidaTraspaso);
                    }
                    var loteBL = new LoteBL();
                    if (interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Any())
                    {
                        interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.ForEach(
                            det =>
                            det.InterfaceSalidaTraspasoID = interfaceSalidaTraspasoGuardado.InterfaceSalidaTraspasoId);

                        interfaceSalidaTraspasoDal.CrearLista(interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle);
                        List<LoteInfo> lotes = interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle
                                                                      .Select(lote => new LoteInfo
                                                                      {
                                                                          LoteID = lote.Lote.LoteID,
                                                                          Cabezas = lote.Cabezas,
                                                                          UsuarioModificacionID = lote.UsuarioCreacionID
                                                                      }).ToList();
                        loteBL.AcutalizaCabezasLoteXML(lotes);
                        List<LoteInfo> lotesCabezas = lotes.GroupBy(lote => lote.LoteID).Select(x => new LoteInfo
                                                                               {
                                                                                   LoteID = x.Key,
                                                                                   Cabezas = x.Sum(cab => cab.Cabezas),
                                                                                   UsuarioModificacionID = x.Select(usu => usu.UsuarioModificacionID).FirstOrDefault()
                                                                               }).ToList();
                        List<LoteInfo> lotesDesactivar =
                            (from ist in interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle
                             from lote in lotesCabezas
                             where ist.Lote.Cabezas - lote.Cabezas == 0
                             select lote).ToList();
                        if (lotesDesactivar.Any())
                        {
                            loteBL.DesactivarLoteXML(lotesDesactivar);
                        }
                    }
                    #region POLIZA
                    if (interfaceSalidaTraspasoGuardado.PesoBruto > 0 && interfaceSalidaTraspasoGuardado.PesoTara > 0)
                    {
                        var polizaBL = new PolizaBL();
                        int organizacionID = interfaceSalidaTraspasoGuardado.OrganizacionId;
                        DateTime fechaEnvio = interfaceSalidaTraspasoGuardado.FechaEnvio;
                        long folio = interfaceSalidaTraspasoGuardado.FolioTraspaso;

                        IList<PolizaInfo> polizasGanado = 
                            polizaBL.ObtenerPolizaConDocumentoSAPPendientes(TipoPoliza.SalidaGanado, organizacionID, fechaEnvio);
                        if (polizasGanado != null)
                        {
                            polizasGanado = polizasGanado.Where(con => con.Concepto.StartsWith(string.Format("ST-{0} ", folio))).ToList();
                        }
                        if (polizasGanado == null || !polizasGanado.Any())
                        {
                            InterfaceSalidaTraspasoInfo interfaceSalidaTraspasoSacarDetalle =
                                ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(interfaceSalidaTraspasoGuardado);
                            if (interfaceSalidaTraspasoSacarDetalle != null)
                            {
                                interfaceSalidaTraspasoGuardado.ListaInterfaceSalidaTraspasoDetalle =
                                    interfaceSalidaTraspasoSacarDetalle.ListaInterfaceSalidaTraspasoDetalle;
                            }
                            List<LoteInfo> lotesAnimal = interfaceSalidaTraspasoGuardado.ListaInterfaceSalidaTraspasoDetalle
                                                                          .Select(lote => new LoteInfo
                                                                          {
                                                                              LoteID = lote.Lote.LoteID
                                                                          }).ToList();
                            var animalBL = new AnimalBL();
                            List<AnimalInfo> animales
                                    = animalBL.ObtenerAnimalesPorLoteXML(lotesAnimal, interfaceSalidaTraspaso.OrganizacionId);
                            int cabezas = interfaceSalidaTraspasoGuardado.ListaInterfaceSalidaTraspasoDetalle
                                                                         .Sum(cab => cab.Cabezas);
                            if (cabezas > animales.Count)
                            {
                                throw new ExcepcionServicio("No existe suficiente inventario para realizar el traspaso.");
                            }

                            var polizaSalidaGanado =
                                FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaGanado);
                            polizasGanado = polizaSalidaGanado.GeneraPoliza(interfaceSalidaTraspasoGuardado);
                            if (polizasGanado != null && polizasGanado.Any())
                            {
                                polizasGanado.ToList().ForEach(datos =>
                                                                   {
                                                                       datos.OrganizacionID =
                                                                           interfaceSalidaTraspaso.OrganizacionId;
                                                                       datos.UsuarioCreacionID =
                                                                           interfaceSalidaTraspaso.UsuarioModificacionID ??
                                                                           0;
                                                                       datos.ArchivoEnviadoServidor = 1;
                                                                   });
                                polizaBL.GuardarServicioPI(polizasGanado, TipoPoliza.SalidaGanado);
                            }
                        }
                        if (polizasGanado != null)
                        {
                            costoGanado =
                                polizasGanado.Where(imp => imp.Importe.IndexOf('-') < 0).Sum(
                                    imp => Convert.ToDecimal(imp.Importe));
                        }
                    }

                    #endregion POLIZA
                    scope.Complete();
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return new Dictionary<long, decimal> {{interfaceSalidaTraspaso.FolioTraspaso, costoGanado}};
        }
        /// <summary>
        /// Obtiene el listado de organizaciones que se han registrado del dia
        /// </summary>
        /// <returns></returns>
        internal List<InterfaceSalidaTraspasoInfo> ObtenerListaInterfaceSalidaTraspaso()
        {
            try
            {
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoDAL();
                var listado = interfaceSalidaTraspaspBl.ObtenerListaInterfaceSalidaTraspaso();
                if (listado != null)
                {
                    foreach (var interfaceSalidaTraspasoInfo in listado)
                    {
                        if (interfaceSalidaTraspasoInfo.OrganizacionDestino != null)
                        {
                            var organizacionBl = new OrganizacionBL();
                            interfaceSalidaTraspasoInfo.OrganizacionDestino =
                                organizacionBl.ObtenerPorID(
                                    interfaceSalidaTraspasoInfo.OrganizacionDestino.OrganizacionID);
                        }
                        if (interfaceSalidaTraspasoInfo.Formula != null)
                        {
                            var formulaBl = new FormulaBL();
                            interfaceSalidaTraspasoInfo.Formula = formulaBl.ObtenerPorID(interfaceSalidaTraspasoInfo.Formula.FormulaId);
                        }
                        if (interfaceSalidaTraspasoInfo.TipoGanado != null)
                        {
                            var tipoGanadoBl = new TipoGanadoBL();
                            interfaceSalidaTraspasoInfo.TipoGanado =
                                tipoGanadoBl.ObtenerPorID(interfaceSalidaTraspasoInfo.TipoGanado.TipoGanadoID);
                            interfaceSalidaTraspasoInfo.Sexo = interfaceSalidaTraspasoInfo.TipoGanado.Sexo;
                        }
                    }
                }
                return listado;
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
        /// Obtiene el listado de organizaciones que se han registrado del dia
        /// </summary>
        /// <returns></returns>
        internal InterfaceSalidaTraspasoInfo ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(InterfaceSalidaTraspasoInfo interfaceSalidaTraspasoInfo)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoDAL();
                return interfaceSalidaTraspaspBl.ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(interfaceSalidaTraspasoInfo);
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
        /// Obtiene el listado de organizaciones que se han registrado del dia
        /// </summary>
        /// <returns></returns>
        internal InterfaceSalidaTraspasoInfo ObtenerInterfaceSalidaTraspasoPorLote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoDAL();
                return interfaceSalidaTraspaspBl.ObtenerInterfaceSalidaTraspasoPorLote(loteInfo);
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
        /// Obtiene 
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="folioOrigen"> </param>
        /// <returns></returns>
        internal EntradaGanadoCosteoInfo ObtenerDatosInterfaceSalidaTraspaso(int organizacionID, int folioOrigen)
        {
            EntradaGanadoCosteoInfo entradaGanadoCosteo = null;
            try
            {
                Logger.Info();
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoDAL();
                InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso =
                    interfaceSalidaTraspaspBl.ObtenerDatosInterfaceSalidaTraspaso(organizacionID, folioOrigen);
                if (interfaceSalidaTraspaso != null)
                {
                    entradaGanadoCosteo = new EntradaGanadoCosteoInfo
                    {
                        ListaCalidadGanado = new List<EntradaGanadoCalidadInfo>(),
                        ListaCostoEntrada = new List<EntradaGanadoCostoInfo>(),
                        ListaEntradaDetalle = new List<EntradaDetalleInfo>(),
                    };
                    var animalBL = new AnimalBL();
                    var interfaceSalidaTraspasoCostoBL = new InterfaceSalidaTraspasoCostoBL();
                    List<InterfaceSalidaTraspasoCostoInfo> interfaceSalidaTraspasoCosto =
                        interfaceSalidaTraspasoCostoBL.ObtenerCostosInterfacePorDetalle(interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle);
                    var animalesID = new HashSet<long>(interfaceSalidaTraspasoCosto.Select(id => id.AnimalID));
                    List<AnimalInfo> animalesTraspaso = animalesID.Select(ani => new AnimalInfo
                                                                            {
                                                                                AnimalID = ani,
                                                                                Activo = false
                                                                            }).ToList();
                    List<AnimalInfo> animalesMovimientoSalidaTraspaso = animalBL.ObtenerMovimientosPorXML(animalesTraspaso);
                    if (animalesMovimientoSalidaTraspaso != null && animalesMovimientoSalidaTraspaso.Any())
                    {
                        var animalesMovimiento = new List<AnimalMovimientoInfo>();
                        animalesMovimientoSalidaTraspaso.ForEach(
                            movs => animalesMovimiento.AddRange(movs.ListaAnimalesMovimiento));

                        int pesoNeto = interfaceSalidaTraspaso.PesoBruto - interfaceSalidaTraspaso.PesoTara;
                        decimal pesoLote = 0;
                        int totalCabezas = interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Sum(cab => cab.Cabezas);

                        var costoBL = new CostoBL();
                        IList<CostoInfo> costos = costoBL.ObtenerTodos(EstatusEnum.Activo);
                        int cabezas = interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Sum(cabe => cabe.Cabezas);
                        entradaGanadoCosteo.ListaCalidadGanado.AddRange(
                            animalesMovimientoSalidaTraspaso.GroupBy(tipo => tipo.TipoGanado.TipoGanadoID).Select(
                                calidad => new EntradaGanadoCalidadInfo
                                {
                                    CalidadGanado
                                        = new CalidadGanadoInfo
                                        {
                                            CalidadGanadoID =
                                                calidad.Select(
                                                    id =>
                                                    id.CalidadGanado.CalidadGanadoID).
                                                FirstOrDefault(),
                                            Calidad = calidad.Select(cal => cal.CalidadGanado.Calidad).FirstOrDefault(),
                                            Descripcion = calidad.Select(cal => cal.CalidadGanado.Descripcion).FirstOrDefault(),
                                            Sexo =
                                                calidad.Select(
                                                    id => id.CalidadGanado.Sexo).
                                                FirstOrDefault()
                                        },
                                    Valor = calidad.Count(),
                                }));
                        pesoLote = (pesoNeto * cabezas) / totalCabezas;
                        int pesoPromedio = Convert.ToInt32(pesoLote / totalCabezas);
                        entradaGanadoCosteo.ListaEntradaDetalle.AddRange(
                            interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.GroupBy(tipo => tipo.TipoGanado.TipoGanadoID).Select(
                                detalle => new EntradaDetalleInfo
                                {
                                    TipoGanado = new TipoGanadoInfo
                                    {
                                        TipoGanadoID = detalle.Key,
                                        Descripcion = detalle.Select(des => des.TipoGanado.Descripcion).FirstOrDefault()
                                    },
                                    Cabezas = detalle.Select(des => des.Cabezas).FirstOrDefault(),
                                    PesoOrigen = detalle.Select(des => des.Cabezas).FirstOrDefault() * pesoPromedio,
                                    FechaSalidaInterface = interfaceSalidaTraspaso.FechaEnvio,
                                }));
                        int pesoTotalAgrupado = Convert.ToInt32(entradaGanadoCosteo.ListaEntradaDetalle.Sum(det => det.PesoOrigen));
                        if (pesoTotalAgrupado != pesoNeto)
                        {
                            int diferencia = pesoNeto - pesoTotalAgrupado;
                            EntradaDetalleInfo primerDetalle = entradaGanadoCosteo.ListaEntradaDetalle.FirstOrDefault();
                            if (primerDetalle != null)
                            {
                                primerDetalle.PesoOrigen = primerDetalle.PesoOrigen + diferencia;
                            }
                        }
                        //entradaGanadoCosteo.ListaEntradaDetalle.AddRange(
                        //    animalesMovimientoSalidaTraspaso.GroupBy(tipo => tipo.TipoGanado.TipoGanadoID).Select(
                        //        detalle => new EntradaDetalleInfo
                        //        {
                        //            TipoGanado = new TipoGanadoInfo
                        //            {
                        //                TipoGanadoID = detalle.Key,
                        //                Descripcion =
                        //                    detalle.Select(
                        //                        des =>
                        //                        des.TipoGanado.Descripcion).
                        //                    FirstOrDefault()
                        //            },
                        //            Cabezas = detalle.Count(),
                        //            PesoOrigen = pesoLote,
                        //            FechaSalidaInterface = interfaceSalidaTraspaso.FechaEnvio,
                        //        }));
                        entradaGanadoCosteo.ListaCostoEntrada.AddRange(interfaceSalidaTraspasoCosto
                                           .GroupBy(cos => cos.Costo.CostoID).Select(grp =>
                                               new EntradaGanadoCostoInfo
                                               {
                                                   Costo = new CostoInfo
                                                   {
                                                       CostoID = grp.Key,
                                                       ImporteCosto = grp.Sum(imp => imp.Importe),
                                                       ClaveContable = costos.Where(id => id.CostoID == grp.Key)
                                                                             .Select(clave => clave.ClaveContable)
                                                                             .FirstOrDefault(),
                                                       Descripcion = costos.Where(id => id.CostoID == grp.Key)
                                                                           .Select(desc => desc.Descripcion)
                                                                           .FirstOrDefault()
                                                   },
                                                   Importe = grp.Sum(imp => imp.Importe),
                                               }));
                        ValidarPeso(entradaGanadoCosteo, pesoNeto);
                        AgruparValoresInterfaz(entradaGanadoCosteo, organizacionID, interfaceSalidaTraspaso, interfaceSalidaTraspasoCosto);
                        CompletarCalidadGanado(entradaGanadoCosteo);
                        AsignarProveedorSuKarne300(entradaGanadoCosteo);
                    }
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
            return entradaGanadoCosteo;
        }

        /// <summary>
        /// Completa lista de Calidad de Ganado
        /// </summary>
        /// <param name="entradaGanadoCosteo"></param>
        private void CompletarCalidadGanado(EntradaGanadoCosteoInfo entradaGanadoCosteo)
        {
            var calidadGanadoBL = new CalidadGanadoBL();
            List<EntradaGanadoCalidadInfo> calidadGanado = calidadGanadoBL.ObtenerListaCalidadGanado();
            List<EntradaGanadoCalidadInfo> calidadGanadoTraspaso = entradaGanadoCosteo.ListaCalidadGanado
                                                                    .Select(cal => new EntradaGanadoCalidadInfo
                                                                                    {
                                                                                        CalidadGanado = cal.CalidadGanado,
                                                                                        Valor = cal.Valor
                                                                                    }).ToList();
            EntradaGanadoCalidadInfo calidad;
            calidadGanado.ForEach(dato =>
            {
                calidad = calidadGanadoTraspaso.FirstOrDefault(id => dato.CalidadGanado.CalidadGanadoID
                                                                  == id.CalidadGanado.CalidadGanadoID);
                if (calidad != null)
                {
                    dato.Valor = calidad.Valor;
                }
            });
            entradaGanadoCosteo.ListaCalidadGanado = calidadGanado;
        }

        /// <summary>
        /// Asigna el proveedor SuKarne al Costo del Ganado
        /// </summary>
        /// <param name="entradaGanadoCosteo"></param>
        private void AsignarProveedorSuKarne300(EntradaGanadoCosteoInfo entradaGanadoCosteo)
        {
            var parametroGeneralBL = new ParametroGeneralBL();
            ParametroGeneralInfo parametroGeneral =
                parametroGeneralBL.ObtenerPorClaveParametro(ParametrosEnum.PolizaSacrificio300.ToString());
            if (parametroGeneral != null)
            {
                var proveedor = new ProveedorInfo
                                    {
                                        CodigoSAP = parametroGeneral.Valor
                                    };
                var proveedorBL = new ProveedorBL();
                proveedor = proveedorBL.ObtenerPorCodigoSAP(proveedor);
                if (proveedor != null)
                {
                    EntradaGanadoCostoInfo entradaGanadoCosto =
                        entradaGanadoCosteo.ListaCostoEntrada.FirstOrDefault(id => id.Costo.CostoID == 1);
                    if (entradaGanadoCosto != null)
                    {
                        entradaGanadoCosto.Proveedor = proveedor;
                        entradaGanadoCosto.TieneCuenta = false;
                    }
                }
            }
        }

        /// <summary>
        /// Agrupa los costos de ganado de la interfaz
        /// </summary>
        /// <param name="entradaGanadoCosteo"></param>
        /// <param name="organizacionId"></param>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <param name="interfaceSalidaTraspasoCosto"></param>
        private void AgruparValoresInterfaz(EntradaGanadoCosteoInfo entradaGanadoCosteo, int organizacionId, InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso, List<InterfaceSalidaTraspasoCostoInfo> interfaceSalidaTraspasoCosto)
        {
            var interfaceSalidaBL = new InterfaceSalidaBL();
            var costosAgrupados = (from costos in entradaGanadoCosteo.ListaCostoEntrada
                                   group costos by costos.Costo.CostoID
                                   into costoAgrupado
                                   let interfaceSalidaCostoInfo = costoAgrupado.FirstOrDefault()
                                   let claveContable =
                                       interfaceSalidaBL.ObtenerCuentaInventario(interfaceSalidaCostoInfo.Costo,
                                                                                 organizacionId,
                                                                                 ClaveCuenta.CuentaInventarioIntensivo)
                                   where interfaceSalidaCostoInfo != null
                                   select new EntradaGanadoCostoInfo
                                              {
                                                  Costo = interfaceSalidaCostoInfo.Costo,
                                                  TieneCuenta = true,
                                                  Importe = costoAgrupado.Sum(cos => cos.Importe),
                                                  CuentaProvision = claveContable.Valor,
                                                  DescripcionCuenta = claveContable.Descripcion,
                                                  Origen = true
                                              }).ToList();
            entradaGanadoCosteo.ListaCalidadGanado = entradaGanadoCosteo.ListaCalidadGanado
                .GroupBy(calidad => calidad.CalidadGanado.CalidadGanadoID)
                .Select(calidad => new EntradaGanadoCalidadInfo
                                       {
                                           CalidadGanado
                                               = new CalidadGanadoInfo
                                                     {
                                                         CalidadGanadoID = calidad.Key,
                                                         Calidad =
                                                             calidad.Select(cal => cal.CalidadGanado.Calidad).
                                                             FirstOrDefault(),
                                                         Descripcion =
                                                             calidad.Select(cal => cal.CalidadGanado.Descripcion).
                                                             FirstOrDefault(),
                                                         Sexo =
                                                             calidad.Select(
                                                                 id => id.CalidadGanado.Sexo).
                                                             FirstOrDefault()
                                                     },
                                           Valor = calidad.Sum(cal => cal.Valor),
                                       }).ToList();
            decimal importeGanado =
                costosAgrupados.Where(imp => imp.Costo.CostoID == 1).Select(imp => imp.Importe).FirstOrDefault();
            int cabezas = entradaGanadoCosteo.ListaCalidadGanado.Sum(cab => cab.Valor);
            importeGanado = importeGanado + (320 * cabezas);

            EntradaGanadoCostoInfo costoGanado = costosAgrupados.FirstOrDefault(id => id.Costo.CostoID == 1);
            decimal costosNoGanado = costosAgrupados.Where(id => id.Costo.CostoID != 1).Sum(imp => imp.Importe);
            importeGanado += costosNoGanado;
            costoGanado.Importe = importeGanado;
            entradaGanadoCosteo.ListaCostoEntrada = costosAgrupados.Where(id => id.Costo.CostoID == 1).ToList();

            foreach (var entradaDetalle in entradaGanadoCosteo.ListaEntradaDetalle)
            {
                var interfaceDetalle =
                    interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.FirstOrDefault(
                        det => det.TipoGanado.TipoGanadoID == entradaDetalle.TipoGanado.TipoGanadoID);

                if (interfaceDetalle != null)
                {
                    decimal importeGanadoDetalle =
                        interfaceSalidaTraspasoCosto.Where(
                            cos =>
                            cos.InterfaceSalidaTraspasoDetalle.InterfaceSalidaTraspasoDetalleID ==
                            interfaceDetalle.InterfaceSalidaTraspasoDetalleID &&
                            cos.Costo.CostoID == Costo.CostoGanado.GetHashCode()).Sum(cos => cos.Importe);

                    importeGanadoDetalle = importeGanadoDetalle + (320 * entradaDetalle.Cabezas);

                    decimal importeGanadoDetalleNoGanado =
                       interfaceSalidaTraspasoCosto.Where(
                           cos =>
                           cos.InterfaceSalidaTraspasoDetalle.InterfaceSalidaTraspasoDetalleID ==
                           interfaceDetalle.InterfaceSalidaTraspasoDetalleID &&
                           cos.Costo.CostoID != Costo.CostoGanado.GetHashCode()).Sum(cos => cos.Importe);

                    importeGanadoDetalle = importeGanadoDetalle + importeGanadoDetalleNoGanado;

                    entradaDetalle.Importe = importeGanadoDetalle;
                    entradaDetalle.PrecioKilo = entradaDetalle.Importe / entradaDetalle.PesoOrigen;
                }

            }

            //entradaGanadoCosteo.ListaEntradaDetalle =
            //    entradaGanadoCosteo.ListaEntradaDetalle.GroupBy(tipo => tipo.TipoGanado.TipoGanadoID).Select(
            //        detalle => new EntradaDetalleInfo
            //                       {
            //                           TipoGanado = new TipoGanadoInfo
            //                                            {
            //                                                TipoGanadoID = detalle.Key,
            //                                                Descripcion =
            //                                                    detalle.Select(
            //                                                        des =>
            //                                                        des.TipoGanado.Descripcion).
            //                                                    FirstOrDefault()
            //                                            },
            //                           Cabezas = detalle.Sum(cab => cab.Cabezas),
            //                           PesoOrigen = detalle.Sum(peso => peso.PesoOrigen),
            //                           FechaSalidaInterface =
            //                               detalle.Select(fecha => fecha.FechaSalidaInterface).FirstOrDefault(),
            //                           PrecioKilo = importeGanado / detalle.Sum(peso => peso.PesoOrigen),
            //                           Importe =
            //                               (importeGanado / detalle.Sum(peso => peso.PesoOrigen)) *
            //                               detalle.Sum(peso => peso.PesoOrigen)
            //                       }).ToList();
        }

        /// <summary>
        /// Valida que los pesos de interface sean correctos
        /// </summary>
        /// <param name="entradaGanadoCosteo"></param>
        /// <param name="pesoNeto"></param>
        private void ValidarPeso(EntradaGanadoCosteoInfo entradaGanadoCosteo, int pesoNeto)
        {
            decimal pesoTotal = entradaGanadoCosteo.ListaEntradaDetalle.Sum(peso => peso.PesoOrigen);

            decimal diferencia = pesoNeto - pesoTotal;
            EntradaDetalleInfo entradaDetalle = entradaGanadoCosteo.ListaEntradaDetalle.FirstOrDefault();
            if (entradaDetalle == null)
            {
                entradaDetalle = new EntradaDetalleInfo();
            }
            if (diferencia >= 0)
            {
                entradaDetalle.PesoOrigen += diferencia;
            }
            else
            {
                entradaDetalle.PesoOrigen -= diferencia;
            }
        }

        /// <summary>
        /// Obtiene datos de Interface Salida Traspaso
        /// por Lotes
        /// </summary>
        /// <param name="lotes"></param>
        /// <param name="fecha"> </param>
        /// <param name="foliosTraspaso"> </param>
        /// <returns></returns>
        internal List<InterfaceSalidaTraspasoInfo> ObtenerInterfaceSalidaTraspasoLotes(List<LoteInfo> lotes, DateTime fecha, List<PolizaSacrificioModel> foliosTraspaso)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspaspDAL = new InterfaceSalidaTraspasoDAL();
                return interfaceSalidaTraspaspDAL.ObtenerInterfaceSalidaTraspasoLotes(lotes, fecha, foliosTraspaso);
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
        /// Obtiene una coleccion con los datos que han
        /// sido traspasados por medio de la interface
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaTraspasoInfo> ObtenerTraspasosPorFechaConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspaspDAL = new InterfaceSalidaTraspasoDAL();
                return interfaceSalidaTraspaspDAL.ObtenerTraspasosPorFechaConciliacion(organizacionID, fechaInicial,
                                                                                       fechaFinal);
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
        /// Obtiene datos de Interface Salida Traspaso
        /// por Lotes
        /// </summary>
        /// <param name="folioTraspaso"></param>
        internal List<InterfaceSalidaTraspasoInfo> ObtenerInterfaceSalidaTraspaso(PolizaSacrificioModel folioTraspaso)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspaspDAL = new InterfaceSalidaTraspasoDAL();
                List<InterfaceSalidaTraspasoInfo> resultado = interfaceSalidaTraspaspDAL
                                                                .ObtenerInterfaceSalidaTraspaso(folioTraspaso);
                return resultado;
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
