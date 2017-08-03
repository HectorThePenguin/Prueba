using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;
using System.Reflection;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ConciliacionMovimientosSIAP.xaml
    /// </summary>
    public partial class ConciliacionMovimientosSIAP
    {
        #region PROPIEDADES

        private ConciliacionMovimientosSIAPModel Contexto
        {
            get { return (ConciliacionMovimientosSIAPModel)DataContext; }
            set { DataContext = value; }
        }

        #endregion PROPIEDADES

        #region VARIABLES

        private IList<TipoPolizaInfo> tiposPoliza;
        private PolizaAbstract polizaAbstract;

        #endregion VARIABLES

        #region CONSTRUCTORES

        public ConciliacionMovimientosSIAP()
        {
            InitializeComponent();
            InicializaContexto();
        }

        #endregion CONSTRUCTORES

        #region EVENTOS

        /// <summary>
        /// Se lanza al momento de terminar de cargar
        /// el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            dtpFechaInicio.Focus();
            var tipoPolizaPL = new TipoPolizaPL();
            tiposPoliza = tipoPolizaPL.ObtenerTodos();
        }

        /// <summary>
        /// Genera la conciliacion de los movimientos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConciliarClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime fechaInicial = dtpFechaInicio.SelectedDate.HasValue ? dtpFechaInicio.SelectedDate.Value : DateTime.Now;
                DateTime fechaFinal = dtpFechaFin.SelectedDate.HasValue ? dtpFechaFin.SelectedDate.Value : DateTime.Now;

                bool fechasValidas = ValidarFechas(fechaInicial, fechaFinal);
                if (!fechasValidas)
                {
                    return;
                }

                LimpiarArbol();

                List<PolizaInfo> polizasSIAP = ObtenerPolizas(fechaInicial, fechaFinal);
                Contexto.PolizasCompletas.Clear();
                Contexto.PolizasCompletas.AddRange(polizasSIAP);
                Contexto.PolizasMovimientos.Clear();

                ObtenerMovimientosAlmacenConciliacion(fechaInicial, fechaFinal);
                if (Contexto.Ganado)
                {
                    ObtenerEntradasGanado(fechaInicial, fechaFinal);
                    //ObtenerVentasGanado(fechaInicial, fechaFinal);
                    //ObtenerMuertesGanado(fechaInicial, fechaFinal);
                    //ObtenerSacrificioGanado(fechaInicial, fechaFinal);
                    //ObtenerGastosInventario(fechaInicial, fechaFinal);
                    //ValidarConsumoAlimento();
                    //ObtenerTraspasosGanado(fechaInicial, fechaFinal);
                    //ObtenerSacrificiosPorTraspasosGanado();
                }

                if (Contexto.Almacen)
                {
                    ObtenerConciliacionEntradaMateriaPrima(fechaInicial, fechaFinal);
                    ValidarEntradaMateriaPrima();

                    ObtenerSolicitudProducto();
                    
                    ObtenerMovimientosSalidaPorConsumo();

                    ValidarSalidaTranspaso();
                    ValidarConsumoProducto();
                }

                if (Contexto.MateriaPrima)
                {
                    ObtenerConciliacionEntradaMateriaPrima(fechaInicial, fechaFinal);
                    ValidarEntradaMateriaPrima();

                    ObtenerConciliacionPaseProceso(fechaInicial, fechaFinal);
                    ValidarPasesProceso();

                    ObtenerDatosContrato(fechaInicial, fechaFinal);
                    GenerarConciliacionContratoBodegaTercerosCompraTotal();
                    GenerarConciliacionContratoTransitoCompraTotal();
                    GenerarConciliacionContratoParcial();
                    ValidarContratoOtrosCostos();


                    ObtenerProduccionAlimento(fechaInicial, fechaFinal);
                    ValidarProduccionAlimento();

                    ObtenerPremezclaDistribucion(fechaInicial, fechaFinal);
                    GenerarMovimientosPremezcla();

                    ObtenerEntradasCompraMateriaPrima();
                    ValidaEntradaCompraMateriaPrima();

                    ValidarSalidaTranspaso();
                    ValidaEntradaPorAjuste();

                    ObtenerMovimientosSubProductos();
                    ValidarSubProductos();

                    ObtenerSalidasVentaProducto();
                    ValidaSalidasVentaProducto();

                    ObtenerGastosMateriaPrima();
                    ValidaGastosMateriaPrima();

                    ObtenerEntradaPorAjusteCierreDiaPA();
                    ObtenerSalidaPorAjusteCierreDiaPA();
                    
                }

                GenerarConciliacion();
                if (Contexto.Polizas != null && Contexto.Polizas.Any())
                {
                    GenerarArbolConFaltantes(Contexto.Polizas);
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ConciliacionSAP_NoExistenPendietesPorEnviar,
                                      MessageBoxButton.OK,
                                      MessageImage.Warning);
                    LimpiarArbol();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.ConciliacionSAP_ErrorConciliar,
                                     MessageBoxButton.OK,
                                     MessageImage.Error);
            }
        }

        private bool ValidarFechas(DateTime fechaInicial, DateTime fechaFinal)
        {
            var fechasValidas = true;
            if (fechaInicial > fechaFinal)
            {
                fechasValidas = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionSAP_FechaInicialMayorFechaFinal,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
            else
            {
                if (fechaFinal < fechaInicial)
                {
                    fechasValidas = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ConciliacionSAP_FechaFinalMenorFechaInicial,
                                      MessageBoxButton.OK,
                                      MessageImage.Warning);
                }
            }
            return fechasValidas;
        }

        /// <summary>
        /// Regresa la pantalla a su estado inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiarClick(object sender, RoutedEventArgs e)
        {
            InicializaContexto();
            dtpFechaInicio.Focus();
        }

        /// <summary>
        /// Guarda los movimientos seleccionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar(object sender, RoutedEventArgs e)
        {
            List<PolizaInfo> polizasPorReenviar = Contexto.Polizas.Where(sel => sel.Generar).ToList();
            if (polizasPorReenviar != null && polizasPorReenviar.Any())
            {
                Guardar(polizasPorReenviar);
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionSAP_SeleccionarPoliza, MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Inicializa el contexto de la pantalla
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ConciliacionMovimientosSIAPModel
            {
                Ruta = string.Empty,
                Polizas = new ObservableCollection<PolizaInfo>(),
                PolizasCompletas = new List<PolizaInfo>(),
                PasesProceso = new List<PolizaPaseProcesoModel>(),
                AlmacenesMovimiento = new ConciliacionMovimientosAlmacenModel
                                          {
                                              AlmacenesMovimientos = new List<AlmacenMovimientoInfo>(),
                                              AlmacenesMovimientosDetalle = new List<AlmacenMovimientoDetalle>()
                                          },
                PolizasMovimientos = new ObservableCollection<ConciliacionPolizasMovimientosModel>(),
                ContenedorEntradasMateriasPrima = new List<ContenedorEntradaMateriaPrimaInfo>(),
                ConsumosAlimento = new List<PolizaConsumoAlimentoModel>(),
                ConsumosProducto = new List<ContenedorAlmacenMovimientoCierreDia>(),
                PolizasContrato = new List<PolizaContratoModel>(),
                RecepcionProductos = new List<RecepcionProductoInfo>(),
                DistribucionIngredientes = new List<DistribucionDeIngredientesInfo>(),
                SolicitudProductos = new List<SolicitudProductoInfo>(),
                EntradasPorAjuste = new List<PolizaEntradaSalidaPorAjusteModel>(),
                SalidasPorAjuste = new List<PolizaEntradaSalidaPorAjusteModel>(),
                SubProductos = new List<ContenedorEntradaMateriaPrimaInfo>(),
                ProduccionesFormula = new List<ProduccionFormulaInfo>(),
                SalidasVentaProductos = new List<SalidaProductoInfo>(),
                GastosMateriaPrima = new List<GastoMateriaPrimaInfo>(),
                VentasGanado = new List<ContenedorVentaGanado>(),
                AnimalesCosto = new List<AnimalCostoInfo>(),
                LotesSacrificio = new List<PolizaSacrificioModel>(),
                EntradasGanado = new List<ContenedorCosteoEntradaGanadoInfo>(),
                GastosInventario = new List<GastoInventarioInfo>(),
                InterfaceSalidasTraspasos = new List<InterfaceSalidaTraspasoInfo>(),
                Ganado = true
            };
            LimpiarArbol();
        }

        /// <summary>
        /// Obtiene las polizas de la base de datos
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        private List<PolizaInfo> ObtenerPolizas(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                var polizaPL = new PolizaPL();
                string claseDocumento = Contexto.Ganado ? "GF" : "AG";
                IEnumerable<PolizaInfo> polizas =
                    polizaPL.ObtenerPolizasConciliacion(AuxConfiguracion.ObtenerOrganizacionUsuario(), fechaInicial,
                                                        fechaFinal, claseDocumento);
                return polizas.ToList();
            }
            catch (Exception ex)
            {
                string mensaje = Properties.Resources.ConciliacionSAP_ErrorObtenerPolizas;
                bool tiempoEspera = ValidaExcepcionTiempoEspera(ex);
                if (tiempoEspera)
                {
                    mensaje = "Ocurrió un error con la conexión";
                }
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal]
                                  , mensaje, MessageBoxButton.OK, MessageImage.Error);
                throw;
            }
        }

        private void ObtenerSolicitudProducto()
        {
            Contexto.SolicitudProductos.Clear();
            using (var solicitudProductoBL = new SolicitudProductoBL())
            {
                var almacenPL = new AlmacenPL();
                IList<AlmacenInfo> almacenes = almacenPL.ObtenerTodos();
                almacenes =
                    almacenes.Where(
                        org => org.Organizacion.OrganizacionID == AuxConfiguracion.ObtenerOrganizacionUsuario()).ToList();
                AlmacenInfo almacenGeneral =
                    almacenes.FirstOrDefault(
                        org => org.Organizacion.OrganizacionID == AuxConfiguracion.ObtenerOrganizacionUsuario()
                               && org.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.GeneralGanadera.GetHashCode());

                List<AlmacenMovimientoInfo> movimientosSalidaTraspaso =
                    Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                        id => (id.TipoMovimiento.TipoMovimientoID == TipoMovimiento.SalidaPorTraspaso.GetHashCode()
                               || id.TipoMovimiento.TipoMovimientoID == TipoMovimiento.ProductoSalidaTraspaso.GetHashCode())
                              && id.Status == Estatus.AplicadoInv.GetHashCode()).ToList();
                List<SolicitudProductoInfo> salidasProducto =
                    solicitudProductoBL.ObtenerConciliacionPorAlmacenMovimientoXML(movimientosSalidaTraspaso);
                salidasProducto.ForEach(general => general.AlmacenGeneralID = almacenGeneral.AlmacenID);
                Contexto.SolicitudProductos.AddRange(salidasProducto);
            }
        }

        #region GENERACION DE ARBOL

        /// <summary>
        /// Reestablece los valores del arbol
        /// </summary>
        private void LimpiarArbol()
        {
            treePolizas.Items.Clear();
            GenerarArbolConFaltantes(new ObservableCollection<PolizaInfo>());
        }

        /// <summary>
        /// Genera un treeview con los movimientos faltantes por enviar a SAP
        /// </summary>
        /// <param name="polizasFaltantesPorEnviar"></param>
        private void GenerarArbolConFaltantes(ObservableCollection<PolizaInfo> polizasFaltantesPorEnviar)
        {
            System.Windows.Controls.StackPanel stack = GenerarEncabezado();
            var treeHeader = new System.Windows.Controls.TreeViewItem
            {
                Header = stack,
            };
            if (!polizasFaltantesPorEnviar.Any())
            {
                treePolizas.Items.Add(treeHeader);
            }

            treeHeader.DataContext = Contexto.PolizasMovimientos;

            List<string> cuentas =
                polizasFaltantesPorEnviar.Where(x => !string.IsNullOrWhiteSpace(x.Cuenta)).OrderBy(x => x.Cuenta).Select
                    (x => x.Cuenta).Distinct().ToList();
            if (cuentas.Any())
            {
                List<PolizaInfo> polizasAgrupadas;
                IList<TipoPolizaInfo> tiposPoliza = ObtenerTiposPoliza();
                for (var indexCuentas = 0; indexCuentas < cuentas.Count; indexCuentas++)
                {
                    stack = new System.Windows.Controls.StackPanel
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 1050,
                    };
                    var lbl = new System.Windows.Controls.Label
                    {
                        Content = cuentas[indexCuentas],
                    };
                    stack.Children.Add(lbl);
                    treeHeader = new System.Windows.Controls.TreeViewItem
                    {
                        Header = stack,
                    };
                    treePolizas.Items.Add(treeHeader);
                    polizasAgrupadas =
                        polizasFaltantesPorEnviar.Where(x => x.Cuenta.Equals(cuentas[indexCuentas])).ToList();
                    if (polizasAgrupadas.Any())
                    {
                        for (var indexAgrupado = 0; indexAgrupado < polizasAgrupadas.Count; indexAgrupado++)
                        {
                            stack = ObtenerPanelItem(indexCuentas, polizasAgrupadas[indexAgrupado], tiposPoliza);
                            var treeItem = new System.Windows.Controls.TreeViewItem { Header = stack };
                            treeHeader.Items.Add(treeItem);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un StackPanel que para mostrar las
        /// cuentas
        /// </summary>
        /// <param name="indexFormularios"></param>
        /// <param name="poliza"></param>
        /// <param name="tiposPoliza"> </param>
        /// <returns></returns>
        private System.Windows.Controls.StackPanel ObtenerPanelItem(int indexFormularios, PolizaInfo poliza, IList<TipoPolizaInfo> tiposPoliza)
        {
            Brush colorItem;
            if (indexFormularios % 2 == 0)
            {
                colorItem = new SolidColorBrush(Colors.LavenderBlush);
            }
            else
            {
                colorItem = new SolidColorBrush(Colors.White);
            }

            var grdDetalles = new System.Windows.Controls.Grid();
            grdDetalles.SetValue(System.Windows.Controls.Grid.BackgroundProperty, colorItem);
            var columnaDescripcion = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(300) };
            var columnaLectura = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(385) };
            var columnaFaltante = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(100) };
            var columnaInconsistencia = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(100) };
            var columnaEscritura = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(100) };

            grdDetalles.ColumnDefinitions.Add(columnaDescripcion);
            grdDetalles.ColumnDefinitions.Add(columnaLectura);
            grdDetalles.ColumnDefinitions.Add(columnaFaltante);
            grdDetalles.ColumnDefinitions.Add(columnaInconsistencia);
            grdDetalles.ColumnDefinitions.Add(columnaEscritura);

            var row = new System.Windows.Controls.RowDefinition { Height = new GridLength(20) };
            grdDetalles.RowDefinitions.Add(row);

            TipoPolizaInfo tipoPoliza = tiposPoliza.FirstOrDefault(x => x.TipoPolizaID == poliza.TipoPolizaID);
            if (tipoPoliza == null)
            {
                tipoPoliza = new TipoPolizaInfo();
            }

            var lbl = new System.Windows.Controls.Label
            {
                Content =
                    string.Format("{0}\t\t{1}\t{2}\t{3}", poliza.FechaDocumento, tipoPoliza.Descripcion,
                                  poliza.Concepto,
                                  Math.Abs(Convert.ToDecimal(poliza.Importe)).ToString("C2"))
            };
            System.Windows.Controls.Grid.SetColumn(lbl, 0);
            System.Windows.Controls.Grid.SetColumnSpan(lbl, 4);
            System.Windows.Controls.Grid.SetRow(lbl, 0);

            var chkFaltante = new System.Windows.Controls.CheckBox
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                IsEnabled = false
            };
            var bindFaltante = new Binding("Faltante")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = poliza,
            };
            chkFaltante.SetBinding(System.Windows.Controls.CheckBox.IsCheckedProperty, bindFaltante);

            System.Windows.Controls.Grid.SetColumn(chkFaltante, 2);
            System.Windows.Controls.Grid.SetRow(chkFaltante, 0);

            var chkInconsistencia = new System.Windows.Controls.CheckBox
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                IsEnabled = false
            };
            var bindInconsistencia = new Binding("Inconcistencia")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = poliza,
            };
            chkInconsistencia.SetBinding(System.Windows.Controls.CheckBox.IsCheckedProperty, bindInconsistencia);

            System.Windows.Controls.Grid.SetColumn(chkInconsistencia, 3);
            System.Windows.Controls.Grid.SetRow(chkInconsistencia, 0);

            var chkGenera = new System.Windows.Controls.CheckBox
            {
                HorizontalAlignment = HorizontalAlignment.Right,
            };
            var bindGenera = new Binding("Generar")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = poliza,
            };
            chkGenera.SetBinding(System.Windows.Controls.CheckBox.IsCheckedProperty, bindGenera);

            System.Windows.Controls.Grid.SetColumn(chkGenera, 4);
            System.Windows.Controls.Grid.SetRow(chkGenera, 0);

            grdDetalles.Children.Add(lbl);
            grdDetalles.Children.Add(chkGenera);
            grdDetalles.Children.Add(chkInconsistencia);
            grdDetalles.Children.Add(chkFaltante);

            var stack = new System.Windows.Controls.StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 1050,
            };
            stack.Children.Add(grdDetalles);

            return stack;
        }

        /// <summary>
        /// Genera encabezado
        /// </summary>
        /// <returns></returns>
        private System.Windows.Controls.StackPanel GenerarEncabezado()
        {
            var stack = new System.Windows.Controls.StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 1070
            };
            var grdEncabezado = new System.Windows.Controls.Grid();
            var columnaDescripcion = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(470) };
            var columnaLectura = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(300) };
            var columnaFaltante = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(100) };
            var columnaInconsistencia = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(100) };
            var columnaEscritura = new System.Windows.Controls.ColumnDefinition { Width = new GridLength(100) };

            grdEncabezado.ColumnDefinitions.Add(columnaDescripcion);
            grdEncabezado.ColumnDefinitions.Add(columnaLectura);
            grdEncabezado.ColumnDefinitions.Add(columnaFaltante);
            grdEncabezado.ColumnDefinitions.Add(columnaInconsistencia);
            grdEncabezado.ColumnDefinitions.Add(columnaEscritura);

            grdEncabezado.RowDefinitions.Add(new System.Windows.Controls.RowDefinition());

            var lblPantalla = new System.Windows.Controls.Label
            {
                Content = Properties.Resources.ConciliacionSAP_lblCuenta,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblPantalla);
            System.Windows.Controls.Grid.SetColumn(lblPantalla, 0);
            System.Windows.Controls.Grid.SetRow(lblPantalla, 0);

            var lblLectura = new System.Windows.Controls.Label
            {
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblLectura);
            System.Windows.Controls.Grid.SetColumn(lblLectura, 1);
            System.Windows.Controls.Grid.SetRow(lblLectura, 0);

            var lblFaltante = new System.Windows.Controls.Label
            {
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold"),
                Content = Properties.Resources.ConciliacionSAP_TituloFaltante
            };
            grdEncabezado.Children.Add(lblFaltante);
            System.Windows.Controls.Grid.SetColumn(lblFaltante, 2);
            System.Windows.Controls.Grid.SetRow(lblFaltante, 0);

            var lblIncosistencia = new System.Windows.Controls.Label
            {
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold"),
                Content = Properties.Resources.ConciliacionSAP_TituloInconsistencia
            };
            grdEncabezado.Children.Add(lblIncosistencia);
            System.Windows.Controls.Grid.SetColumn(lblIncosistencia, 3);
            System.Windows.Controls.Grid.SetRow(lblIncosistencia, 0);

            var lblEscritura = new System.Windows.Controls.Label
            {
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold"),
                Content = Properties.Resources.ConciliacionSAP_TituloGenerar
            };
            grdEncabezado.Children.Add(lblEscritura);
            System.Windows.Controls.Grid.SetColumn(lblEscritura, 4);
            System.Windows.Controls.Grid.SetRow(lblEscritura, 0);

            stack.Children.Add(grdEncabezado);

            return stack;
        }

        #endregion GENERACION DE ARBOL

        #region CONCILIACION

        /// <summary>
        /// Genera la conciliacion de SAP contra SIAP
        /// </summary>
        private void GenerarConciliacion()
        {
            OrganizacionInfo organizacion = ObtenerOrganizacion();
            var conjuntoPolizas = new HashSet<string>();
            HashSet<string> prefijosCuenta = ObtenerPrefijos();

            ObservableCollection<ConciliacionPolizasMovimientosModel> polizasConciliacion = Contexto.PolizasMovimientos;
            var polizasMovimientos = new List<PolizaInfo>();
            polizasConciliacion.ToList().ForEach(datos => polizasMovimientos.AddRange(datos.PolizasMovimientosDiferentes));
            polizasConciliacion.ToList().ForEach(datos => polizasMovimientos.AddRange(datos.PolizasFaltantes));

            List<PolizaInfo> polizasSIAP =
                polizasMovimientos.Where(
                    x =>
                    !string.IsNullOrWhiteSpace(x.Cuenta) && x.Cuenta.Length > 0 &&
                    (prefijosCuenta.Contains(x.Cuenta.Substring(0, 4)) ||
                                             prefijosCuenta.Contains(x.Cuenta.Substring(0, 3)))).ToList();
            PolizaInfo polizaSIAP;
            List<PolizaInfo> polizasFaltantes;
            StringBuilder sb;
            for (var indexPolizaSIAP = 0; indexPolizaSIAP < polizasSIAP.Count; indexPolizaSIAP++)
            {
                polizaSIAP = polizasSIAP[indexPolizaSIAP];
                sb = new StringBuilder();
                polizasFaltantes = ValidaPolizaFaltante(sb, polizaSIAP, polizasSIAP, organizacion);
                if (polizasFaltantes == null || !polizasFaltantes.Any())
                {
                    if (conjuntoPolizas.Contains(polizaSIAP.Referencia3))
                    {
                        continue;
                    }
                    Contexto.Polizas.Add(polizaSIAP);
                }
                conjuntoPolizas.Add(polizaSIAP.Referencia3);
            }
        }

        private List<PolizaInfo> ValidaPolizaFaltante(StringBuilder sb, PolizaInfo polizaSIAP
                                                    , List<PolizaInfo> polizasSIAP, OrganizacionInfo organizacion)
        {
            sb.AppendFormat("{0}.{1}.{2}", polizaSIAP.FechaDocumento.Substring(6, 2),
                            polizaSIAP.FechaDocumento.Substring(4, 2), polizaSIAP.FechaDocumento.Substring(0, 4));
            List<PolizaInfo> polizasFaltantes = polizasSIAP.Where(
                sap => sap.Cuenta.Trim().Equals(polizaSIAP.Cuenta.Trim())
                       && !sap.Cuenta.Trim().Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta)
                       && sap.ClaseDocumento.Trim().Equals(polizaSIAP.ClaseDocumento.Trim())
                       && polizaSIAP.Concepto.Trim().Contains(sap.Concepto.Trim())
                       && sap.Sociedad.Trim().Equals(organizacion.Sociedad.Trim())
                       && sap.Division.Trim().Equals(organizacion.Division.Trim())
                       && sap.FechaDocumento.Trim().Equals(sb.ToString())).ToList();
            return polizasFaltantes;
        }

        private HashSet<string> ObtenerPrefijos()
        {
            HashSet<string> prefijosCuenta;
            switch (Contexto.TipoCuenta)
            {
                case 1:
                    prefijosCuenta = new HashSet<string>
                                         {
                                             "1151",
                                             "2104",
                                             "4001",
                                             "5001",
                                         };
                    break;
                case 2:
                    prefijosCuenta = new HashSet<string>
                                         {
                                             "1154",
                                         };
                    break;
                default:
                    prefijosCuenta = new HashSet<string>
                                         {
                                             "1153",
                                             "1156",
                                         };
                    break;
            }
            return prefijosCuenta;
        }

        #endregion CONCILIACION

        #region METODOS CONFIGURACION

        /// <summary>
        /// Obtiene los tipos de poliza
        /// </summary>
        /// <returns></returns>
        private IList<TipoPolizaInfo> ObtenerTiposPoliza()
        {
            var tipoPolizaPL = new TipoPolizaPL();
            IList<TipoPolizaInfo> tiposPoliza = tipoPolizaPL.ObtenerTodos();
            return tiposPoliza;
        }

        /// <summary>
        /// Obtiene la organizacion
        /// </summary>
        /// <returns></returns>
        private OrganizacionInfo ObtenerOrganizacion()
        {
            try
            {
                var organizacionPL = new OrganizacionPL();
                OrganizacionInfo organizacion = organizacionPL.ObtenerPorIdConIva(AuxConfiguracion.ObtenerOrganizacionUsuario());
                return organizacion;
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConciliacionSAP_ErrorObtenerOrganizacion, MessageBoxButton.OK,
                                  MessageImage.Error);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los datos a conciliar entradas materia prima
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        private void ObtenerConciliacionEntradaMateriaPrima(DateTime fechaInicial, DateTime fechaFinal)
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var conciliacionBL = new ConciliacionBL();
            List<ContenedorEntradaMateriaPrimaInfo> entradaMateriaPrima =
                conciliacionBL.ObtenerEntradasMateriaPrimaConciliacion(organizacionID, fechaInicial, fechaFinal);
            if (entradaMateriaPrima == null)
            {
                entradaMateriaPrima = new List<ContenedorEntradaMateriaPrimaInfo>();
            }
            Contexto.ContenedorEntradasMateriasPrima.Clear();
            Contexto.ContenedorEntradasMateriasPrima.AddRange(entradaMateriaPrima);
        }

        /// <summary>
        /// Obtiene los datos a conciliar de pases a proceso
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        private void ObtenerConciliacionPaseProceso(DateTime fechaInicial, DateTime fechaFinal)
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var conciliacionBL = new ConciliacionBL();
            List<PolizaPaseProcesoModel> pasesProceso = conciliacionBL.ObtenerConciliacionPaseProceso(organizacionID,
                                                                                                      fechaInicial,
                                                                                                      fechaFinal);
            if (pasesProceso == null)
            {
                pasesProceso = new List<PolizaPaseProcesoModel>();
            }
            Contexto.PasesProceso.Clear();
            Contexto.PasesProceso.AddRange(pasesProceso);
        }

        /// <summary>
        /// Obtiene los movimientos de almacen para su conciliacion
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        private void ObtenerMovimientosAlmacenConciliacion(DateTime fechaInicial, DateTime fechaFinal)
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var conciliacionBL = new ConciliacionBL();
            ConciliacionMovimientosAlmacenModel movimientosAlmacen =
                conciliacionBL.ObtenerMovimientosAlmacenConciliacion(organizacionID, fechaInicial, fechaFinal);
            if (movimientosAlmacen == null)
            {
                movimientosAlmacen = new ConciliacionMovimientosAlmacenModel
                                         {
                                             AlmacenesMovimientos = new List<AlmacenMovimientoInfo>(),
                                             AlmacenesMovimientosDetalle = new List<AlmacenMovimientoDetalle>()
                                         };
            }
            movimientosAlmacen.AlmacenesMovimientos =
                movimientosAlmacen.AlmacenesMovimientos.Where(
                    org => org.Almacen.Organizacion.OrganizacionID == organizacionID).ToList();
            movimientosAlmacen.AlmacenesMovimientosDetalle =
                movimientosAlmacen.AlmacenesMovimientosDetalle.Join(movimientosAlmacen.AlmacenesMovimientos,
                                                                    detalle => detalle.AlmacenMovimientoID,
                                                                    cab => cab.AlmacenMovimientoID, (det, cab) => det).
                    ToList();
            Contexto.AlmacenesMovimiento = movimientosAlmacen;
        }

        #endregion METODOS CONFIGURACION

        #region ENTRADA MATERIA PRIMA

        /// <summary>
        /// Valida Entrada Materia Prima
        /// </summary>
        private void ValidarEntradaMateriaPrima()
        {
            List<PolizaInfo> polizasPasesProcesoExistentes = GenerarMovimientosPolizaEntradaMateriaPrima();
            GenerarConciliacionMovimientos(polizasPasesProcesoExistentes, new List<PolizaInfo>(), 
                                           TipoPoliza.EntradaCompra);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaEntradaMateriaPrima()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaCompra);
            var polizasPasesProceso = new List<PolizaInfo>();
            polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(Contexto.ContenedorEntradasMateriasPrima));
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.EntradaCompra.GetHashCode());
            return polizasPasesProceso;
        }

        #endregion ENTRADA MATERIA PRIMA

        #region PASE PROCESO
        /// <summary>
        /// Valida pases a proceso
        /// </summary>
        private void ValidarPasesProceso()
        {
            List<PolizaInfo> polizasPasesProcesoExistentes =
                GenerarMovimientosPolizaPaseProceso(Contexto.PasesProceso);
            GenerarConciliacionMovimientos(polizasPasesProcesoExistentes, new List<PolizaInfo>(), 
                                           TipoPoliza.PaseProceso);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaPaseProceso(List<PolizaPaseProcesoModel> movimientosGenerarPoliza)
        {
            List<PolizaPaseProcesoModel> pasesProgramacion;
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PaseProceso);
            var polizasPasesProceso = new List<PolizaInfo>();

            movimientosGenerarPoliza =
                            movimientosGenerarPoliza.GroupBy(
                                prod => new { prod.Producto.ProductoId, prod.ProveedorChofer.ProveedorChoferID,prod.Pedido.FolioPedido, prod.PesajeMateriaPrima.Ticket })
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

            for (var i = 0; i < movimientosGenerarPoliza.Count; i++)
            {
                pasesProgramacion = new List<PolizaPaseProcesoModel> {movimientosGenerarPoliza[i]};
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(pasesProgramacion));
            }
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.PaseProceso.GetHashCode());
            return polizasPasesProceso;
        }
        
        #endregion PASE PROCESO

        #region CONSUMO ALIMENTO

        /// <summary>
        /// Valida pases a proceso
        /// </summary>
        private void ValidarConsumoAlimento()
        {
            Contexto.ConsumosAlimento.Clear();

            List<PolizaConsumoAlimentoModel> movimientosGenerarPoliza = ObtenerPolizasConsimoAlimentoExistentes();
            List<PolizaInfo> polizasPasesProcesoExistentes =
                GenerarMovimientosConsumoAlimento(movimientosGenerarPoliza);

            Contexto.ConsumosAlimento = new List<PolizaConsumoAlimentoModel>(movimientosGenerarPoliza);

            List<RepartoInfo> reparto = movimientosGenerarPoliza.Select(alm => alm.Reparto).ToList();
            List<RepartoDetalleInfo> detalle = reparto.Select(det => det.DetalleReparto.FirstOrDefault()).ToList();
            List<long> movimientosExistentes = detalle.Select(movs => movs.AlmacenMovimientoID).ToList();

            movimientosGenerarPoliza = ObtenerPolizasConsumoAlimentoNoExistentes(movimientosExistentes);
            List<PolizaInfo> polizasPasesProcesoNoExistenten =
                GenerarMovimientosConsumoAlimento(movimientosGenerarPoliza);
            Contexto.ConsumosAlimento.AddRange(movimientosGenerarPoliza);

            GenerarConsumosAlimentoAgrupado();
            
            GenerarConciliacionMovimientos(polizasPasesProcesoExistentes, polizasPasesProcesoNoExistenten,
                                           TipoPoliza.ConsumoAlimento);
        }

        private void GenerarConsumosAlimentoAgrupado()
        {
            List<DateTime> fechasReparto =
                Contexto.ConsumosAlimento.Select(det => det.Reparto.Fecha).Distinct().ToList();
            var formulaPL = new FormulaPL();
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var polizasConsumoAlimentoModel = new List<PolizaConsumoAlimentoModel>();
            foreach (var fecha in fechasReparto)
            {
                List<IList<RepartoDetalleInfo>> repartoDetalle =
                    Contexto.ConsumosAlimento.Where(fechaDetalle => fechaDetalle.Reparto.Fecha == fecha).Select(
                        det => det.Reparto.DetalleReparto).ToList();
                foreach (var detalle in repartoDetalle)
                {
                    var repartoAgrupadoFormula = (from repartoDet in detalle
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
                                                                 Importe =
                                                      productosAgrupados.Sum(producto => producto.Importe),
                                                                 formulaPL.ObtenerPorID(productosAgrupados.Key).Producto
                                                             }).ToList();
                    var produccionFormulas = new List<ProduccionFormulaDetalleInfo>();
                    repartoAgrupadoFormula.ForEach(grupo =>
                                                       {
                                                           var produccionFormulaDetalle = new ProduccionFormulaDetalleInfo
                                                                                              {
                                                                                                  ProduccionFormulaId =
                                                                                                      grupo.
                                                                                                      FormulaIDServida,
                                                                                                  Producto =
                                                                                                      grupo.Producto,
                                                                                                  OrganizacionID =
                                                                                                      organizacionID,
                                                                                              };
                                                           produccionFormulas.Add(produccionFormulaDetalle);
                                                       });
                    var repartoDetalleAlmacen =
                        Contexto.AlmacenesMovimiento.AlmacenesMovimientosDetalle.GroupBy(grupo => grupo.ProductoID).
                            Select(det =>
                                   new RepartoDetalleInfo
                                       {
                                           Importe = det.Sum(imp => imp.Importe),
                                           FormulaIDServida = det.Key,
                                           OrganizacionID = organizacionID
                                       }
                            ).ToList();
                    repartoDetalleAlmacen.ForEach(org => org.OrganizacionID = organizacionID);
                    var polizaConsumoAlimentoModel = new PolizaConsumoAlimentoModel
                                                         {
                                                             Reparto = new RepartoInfo
                                                                           {
                                                                               Fecha = fecha,
                                                                               DetalleReparto = repartoDetalleAlmacen
                                                                           },
                                                             ProduccionFormula = new ProduccionFormulaInfo
                                                                                     {
                                                                                         ProduccionFormulaDetalle =
                                                                                             produccionFormulas
                                                                                     }
                                                         };
                    polizasConsumoAlimentoModel.Add(polizaConsumoAlimentoModel);
                    break;
                }
            }
            Contexto.ConsumosAlimento = new List<PolizaConsumoAlimentoModel>(polizasConsumoAlimentoModel);
        }

        private List<PolizaConsumoAlimentoModel> ObtenerPolizasConsumoAlimentoNoExistentes(List<long> movimientosExistentes)
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            List<AlmacenMovimientoInfo> movimientosConsumo = Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                datos =>
                datos.OrganizacionID == organizacionID &&
                datos.TipoMovimiento.TipoMovimientoID == TipoMovimiento.AplicacionAlimento.GetHashCode() &&
                datos.Status == Estatus.AplicadoInv.GetHashCode()).ToList();
            List<long> movimientosTotales = movimientosConsumo.Select(movs => movs.AlmacenMovimientoID).ToList();
            movimientosTotales = movimientosTotales.Except(movimientosExistentes).ToList();

            movimientosConsumo =
                movimientosConsumo.Join(movimientosTotales, mc => mc.AlmacenMovimientoID, mt => mt, (mc, mt) => mc).
                    ToList();
            List<PolizaConsumoAlimentoModel> movimientosGenerarPoliza =
                     movimientosConsumo.Join(Contexto.AlmacenesMovimiento.AlmacenesMovimientosDetalle,
                                               movs => movs.AlmacenMovimientoID,
                                               pp => pp.AlmacenMovimientoID, (pp, mov) => new PolizaConsumoAlimentoModel
                                               {
                                                   Reparto = new RepartoInfo
                                                                 {
                                                                     Fecha = pp.FechaMovimiento,
                                                                     DetalleReparto = new List<RepartoDetalleInfo>
                                                                                          {
                                                                                              new RepartoDetalleInfo
                                                                                                  {
                                                                                                      OrganizacionID = organizacionID,
                                                                                                      Importe = mov.Importe,
                                                                                                      FormulaIDServida = mov.ProductoID,
                                                                                                      AlmacenMovimientoID = mov.AlmacenMovimientoID
                                                                                                  }
                                                                                          }
                                                                 },
                                                   ProduccionFormula = new ProduccionFormulaInfo
                                                                           {
                                                                               ProduccionFormulaDetalle = new List<ProduccionFormulaDetalleInfo>
                                                                                                              {
                                                                                                                  new ProduccionFormulaDetalleInfo
                                                                                                                      {
                                                                                                                          ProduccionFormulaId = mov.ProductoID,
                                                                                                                          OrganizacionID = organizacionID,
                                                                                                                          AlmacenID = pp.AlmacenID,
                                                                                                                      }
                                                                                                              }
                                                                           }
                                               }).ToList();
            return movimientosGenerarPoliza;
        }

        private List<PolizaInfo> GenerarMovimientosConsumoAlimento(List<PolizaConsumoAlimentoModel> movimientosGenerarPoliza)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.ConsumoAlimento);
            var polizasPasesProceso = new List<PolizaInfo>();
            for (var i = 0; i < movimientosGenerarPoliza.Count; i++)
            {
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(movimientosGenerarPoliza[i]));
            }
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.ConsumoAlimento.GetHashCode());
            return polizasPasesProceso;
        }

        private List<PolizaConsumoAlimentoModel> ObtenerPolizasConsimoAlimentoExistentes()
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            List<AlmacenMovimientoInfo> movimientosConsumo = Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                datos =>
                datos.OrganizacionID == organizacionID &&
                datos.TipoMovimiento.TipoMovimientoID == TipoMovimiento.AplicacionAlimento.GetHashCode() &&
                datos.Status == Estatus.AplicadoInv.GetHashCode()).ToList();
            List<PolizaConsumoAlimentoModel> movimientosGenerarPoliza =
                    movimientosConsumo.Join(Contexto.AlmacenesMovimiento.AlmacenesMovimientosDetalle,
                                               movs => movs.AlmacenMovimientoID,
                                               pp => pp.AlmacenMovimientoID, (pp, mov) => new PolizaConsumoAlimentoModel
                                               {
                                                   Reparto = new RepartoInfo
                                                                 {
                                                                     Fecha = pp.FechaMovimiento,
                                                                     DetalleReparto = new List<RepartoDetalleInfo>
                                                                                          {
                                                                                              new RepartoDetalleInfo
                                                                                                  {
                                                                                                      OrganizacionID = organizacionID,
                                                                                                      Importe = mov.Importe,
                                                                                                      FormulaIDServida = mov.ProductoID,
                                                                                                      AlmacenMovimientoID = mov.AlmacenMovimientoID
                                                                                                  }
                                                                                          }
                                                                 },
                                                   ProduccionFormula = new ProduccionFormulaInfo
                                                                           {
                                                                               ProduccionFormulaDetalle = new List<ProduccionFormulaDetalleInfo>
                                                                                                              {
                                                                                                                  new ProduccionFormulaDetalleInfo
                                                                                                                      {
                                                                                                                          ProduccionFormulaId = mov.ProductoID,
                                                                                                                          OrganizacionID = organizacionID,
                                                                                                                          AlmacenID = pp.AlmacenID,
                                                                                                                      }
                                                                                                              }
                                                                           },
                                                                           AlmacenMovimiento = pp
                                               }).ToList();
            return movimientosGenerarPoliza;
        }

        #endregion CONSUMO ALIMENTO

        #region CONSUMO PRODUCTO

        private void ValidarConsumoProducto()
        {
            Contexto.ConsumosProducto.Clear();

            List<ContenedorAlmacenMovimientoCierreDia> movimientosGenerarPoliza = ObtenerPolizasConsimoProductoExistentes();
            List<PolizaInfo> polizasPasesProcesoExistentes =
                GenerarMovimientosConsumoProducto(movimientosGenerarPoliza);

            Contexto.ConsumosProducto = new List<ContenedorAlmacenMovimientoCierreDia>(movimientosGenerarPoliza);

            List<long> movimientosExistentes =
                movimientosGenerarPoliza.Select(alm => alm.AlmacenMovimiento.AlmacenMovimientoID).ToList();

            movimientosGenerarPoliza = ObtenerPolizasConsumoProductoNoExistentes(movimientosExistentes);
            List<PolizaInfo> polizasPasesProcesoNoExistenten =
                GenerarMovimientosConsumoProducto(movimientosGenerarPoliza);
            Contexto.ConsumosProducto.AddRange(movimientosGenerarPoliza);
            GenerarConciliacionMovimientos(polizasPasesProcesoExistentes, polizasPasesProcesoNoExistenten,
                                           TipoPoliza.ConsumoProducto);
        }

        private List<PolizaInfo> GenerarMovimientosConsumoProducto(List<ContenedorAlmacenMovimientoCierreDia> movimientosGenerarPoliza)
        {
            List<DateTime> foliosFechas =
                movimientosGenerarPoliza.Select(
                    x => x.AlmacenMovimiento.FechaMovimiento).Distinct().OrderBy(fecha => fecha).ToList();
            List<ContenedorAlmacenMovimientoCierreDia> movimientosPorDia;
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.ConsumoProducto);
            var polizasPasesProceso = new List<PolizaInfo>();
            for (var indexFecha = 0; indexFecha < foliosFechas.Count; indexFecha++)
            {
                movimientosPorDia =
                    movimientosGenerarPoliza.Where(
                        x => x.AlmacenMovimiento.FechaMovimiento.Equals(foliosFechas[indexFecha])).ToList();
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(movimientosPorDia));
            }            
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.ConsumoProducto.GetHashCode());
            return polizasPasesProceso;
        }

        private List<ContenedorAlmacenMovimientoCierreDia> ObtenerPolizasConsumoProductoNoExistentes(List<long> movimientosExistentes)
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            List<AlmacenMovimientoInfo> movimientosConsumo = Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                datos =>
                datos.OrganizacionID == organizacionID &&
                datos.TipoMovimiento.TipoMovimientoID == TipoMovimiento.SalidaPorConsumo.GetHashCode() &&
                datos.Status == Estatus.AplicadoInv.GetHashCode()).ToList();
            var productoPL = new ProductoPL();
            List<ProductoInfo> productos = productoPL.ObtenerPorEstados(EstatusEnum.Activo);

            List<long> movimientosTotales = movimientosConsumo.Select(movs => movs.AlmacenMovimientoID).ToList();
            movimientosTotales = movimientosTotales.Except(movimientosExistentes).ToList();

            movimientosConsumo =
                movimientosConsumo.Join(movimientosTotales, mc => mc.AlmacenMovimientoID, mt => mt, (mc, mt) => mc).
                    ToList();
            List<ContenedorAlmacenMovimientoCierreDia> movimientosGenerarPoliza =
                movimientosConsumo.Join(Contexto.AlmacenesMovimiento.AlmacenesMovimientosDetalle,
                                        movs => movs.AlmacenMovimientoID,
                                        pp => pp.AlmacenMovimientoID,
                                        (pp, mov) => new ContenedorAlmacenMovimientoCierreDia
                                                         {
                                                             Almacen = pp.Almacen,
                                                             AlmacenMovimientoDetalle = mov,
                                                             AlmacenMovimiento = new AlmacenMovimientoInfo
                                                                                     {
                                                                                         OrganizacionID = organizacionID,
                                                                                         AlmacenMovimientoID =
                                                                                             pp.AlmacenMovimientoID,
                                                                                         TipoMovimientoID =
                                                                                             pp.TipoMovimientoID,
                                                                                         FechaMovimiento =
                                                                                             pp.FechaMovimiento
                                                                                     },
                                                             Producto =
                                                                 productos.FirstOrDefault(
                                                                     id => id.ProductoId == mov.ProductoID)
                                                         }).ToList();
            return movimientosGenerarPoliza;
        }

        private List<ContenedorAlmacenMovimientoCierreDia> ObtenerPolizasConsimoProductoExistentes()
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            List<AlmacenMovimientoInfo> movimientosConsumo = Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                datos =>
                datos.OrganizacionID == organizacionID &&
                datos.TipoMovimiento.TipoMovimientoID == TipoMovimiento.SalidaPorConsumo.GetHashCode() &&
                datos.Status == Estatus.AplicadoInv.GetHashCode()).ToList();
            var productoPL = new ProductoPL();
            List<ProductoInfo> productos = productoPL.ObtenerPorEstados(EstatusEnum.Activo);
            List<ContenedorAlmacenMovimientoCierreDia> movimientosGenerarPoliza =
                movimientosConsumo.Join(Contexto.AlmacenesMovimiento.AlmacenesMovimientosDetalle,
                                        movs => movs.AlmacenMovimientoID,
                                        pp => pp.AlmacenMovimientoID,
                                        (pp, mov) => new ContenedorAlmacenMovimientoCierreDia
                                                         {
                                                             Almacen = pp.Almacen,
                                                             AlmacenMovimientoDetalle = mov,
                                                             AlmacenMovimiento = new AlmacenMovimientoInfo
                                                                                     {
                                                                                         OrganizacionID = organizacionID,
                                                                                         AlmacenMovimientoID =
                                                                                             pp.AlmacenMovimientoID,
                                                                                         TipoMovimientoID =
                                                                                             pp.TipoMovimientoID,
                                                                                         FechaMovimiento =
                                                                                             pp.FechaMovimiento
                                                                                     },
                                                             Producto =
                                                                 productos.FirstOrDefault(
                                                                     id => id.ProductoId == mov.ProductoID)
                                                         }).ToList();
            return movimientosGenerarPoliza;
        }

        #endregion CONSUMO PRODUCTO
        
        #region CONTRATO OTROS COSTOS

        private void ValidarContratoOtrosCostos()
        {
            List<PolizaInfo> polizasExistentes =
                GenerarMovimientosContratoOtrosCostos(Contexto.PolizasContrato);
            GenerarConciliacionMovimientos(polizasExistentes, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaContratoOtrosCostos);
        }

        private List<PolizaInfo> GenerarMovimientosContratoOtrosCostos(List<PolizaContratoModel> polizasContrato)
        {
            var polizasContratoOtrosCostos = new List<PolizaInfo>();
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PolizaContratoOtrosCostos);
            for (var indexPolizasContrato = 0; indexPolizasContrato < polizasContrato.Count; indexPolizasContrato++)
            {
                polizasContratoOtrosCostos.AddRange(polizaAbstract.GeneraPoliza(polizasContrato[indexPolizasContrato]));
            }
            polizasContratoOtrosCostos.ForEach(
                tipo => tipo.TipoPolizaID = TipoPoliza.PolizaContratoOtrosCostos.GetHashCode());
            return polizasContratoOtrosCostos;
        }

        #endregion CONTRATO OTROS COSTOS        

        #region ENTRADA COMPRA MATERIA PRIMA

        private void ObtenerEntradasCompraMateriaPrima()
        {
            Contexto.RecepcionProductos.Clear();
            var recepcionProductoPL = new RecepcionProductoPL();
            List<RecepcionProductoInfo> recepcionProductos =
                recepcionProductoPL.ObtenerRecepcionProductoConciliacionPorAlmacenMovimiento(
                    Contexto.AlmacenesMovimiento.AlmacenesMovimientos);
            if (recepcionProductos == null)
            {
                recepcionProductos = new List<RecepcionProductoInfo>();
            }
            Contexto.RecepcionProductos.AddRange(recepcionProductos);
        }

        private void ValidaEntradaCompraMateriaPrima()
        {
            List<RecepcionProductoInfo> movimientosGenerarPoliza = ObtenerPolizasCompraMateriaPrimaExistentes();
            List<PolizaInfo> polizasPasesProcesoExistentes =
                GenerarMovimientosCompraMateriaPrima(movimientosGenerarPoliza);

            List<long> movimientosExistentes =
                movimientosGenerarPoliza.Select(movs => movs.AlmacenMovimientoId).ToList();

            movimientosGenerarPoliza = ObtenerPolizasCompraMateriaPrimaNoExistentes(movimientosExistentes);
            List<PolizaInfo> polizasPasesProcesoNoExistenten =
                GenerarMovimientosCompraMateriaPrima(movimientosGenerarPoliza);

            GenerarConciliacionMovimientos(polizasPasesProcesoExistentes, polizasPasesProcesoNoExistenten,
                                           TipoPoliza.EntradaCompraMateriaPrima);
        }

        private List<PolizaInfo> GenerarMovimientosCompraMateriaPrima(List<RecepcionProductoInfo> movimientosGenerarPoliza)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaCompraMateriaPrima);
            var polizasPasesProceso = new List<PolizaInfo>();
            for (var i = 0; i < movimientosGenerarPoliza.Count; i++)
            {
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(movimientosGenerarPoliza[i]));
            }
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.EntradaCompraMateriaPrima.GetHashCode());
            return polizasPasesProceso;
        }

        private List<RecepcionProductoInfo> ObtenerPolizasCompraMateriaPrimaNoExistentes(List<long> movimientosAlmacen)
        {
            List<long> movimientosTotales =
                Contexto.RecepcionProductos.Select(movs => movs.AlmacenMovimientoId).ToList();
            movimientosTotales = movimientosTotales.Except(movimientosAlmacen).ToList();
            List<RecepcionProductoInfo> movimientosGenerarPoliza = Contexto.RecepcionProductos.Join(movimientosTotales,
                                               movs => movs.AlmacenMovimientoId,
                                               pp => pp, (pp, mov) => new RecepcionProductoInfo
                                               {
                                                   Almacen = pp.Almacen,
                                                   AlmacenMovimientoId = pp.AlmacenMovimientoId,
                                                   Factura = pp.Factura,
                                                   FechaRecepcion = pp.FechaRecepcion,
                                                   FechaSolicitud = pp.FechaSolicitud,
                                                   FolioOrdenCompra = pp.FolioOrdenCompra,
                                                   FolioRecepcion = pp.FolioRecepcion,
                                                   ListaRecepcionProductoDetalle = pp.ListaRecepcionProductoDetalle,
                                                   Proveedor = pp.Proveedor,
                                                   RecepcionProductoId = pp.RecepcionProductoId,
                                               }).ToList();
            return movimientosGenerarPoliza;
        }

        private List<RecepcionProductoInfo> ObtenerPolizasCompraMateriaPrimaExistentes()
        {
            List<RecepcionProductoInfo> movimientosGenerarPoliza =
                    Contexto.RecepcionProductos.Join(Contexto.AlmacenesMovimiento.AlmacenesMovimientos,
                                               movs => movs.AlmacenMovimientoId,
                                               pp => pp.AlmacenMovimientoID, (pp, mov) => new RecepcionProductoInfo
                                               {
                                                   Almacen = pp.Almacen,
                                                   AlmacenMovimientoId = pp.AlmacenMovimientoId,
                                                   Factura = pp.Factura,
                                                   FechaRecepcion = pp.FechaRecepcion,
                                                   FechaSolicitud = pp.FechaSolicitud,
                                                   FolioOrdenCompra = pp.FolioOrdenCompra,
                                                   FolioRecepcion = pp.FolioRecepcion,
                                                   ListaRecepcionProductoDetalle = pp.ListaRecepcionProductoDetalle,
                                                   Proveedor = pp.Proveedor,
                                                   RecepcionProductoId = pp.RecepcionProductoId,
                                               }).ToList();
            return movimientosGenerarPoliza;
        }

        #endregion ENTRADA COMPRA MATERIA PRIMA

        #region PREMEZCLA

        private void ObtenerPremezclaDistribucion(DateTime fechaInicio, DateTime fechaFinal)
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var premezclaDistribucionPL = new PremezclaDistribucionPL();
            List<DistribucionDeIngredientesInfo> distribucionDeIngredientes =
                premezclaDistribucionPL.ObtenerPremezclaDistribucionConciliacion(organizacionID, fechaInicio, fechaFinal);
            if (distribucionDeIngredientes == null)
            {
                distribucionDeIngredientes = new List<DistribucionDeIngredientesInfo>();
            }
            Contexto.DistribucionIngredientes.Clear();
            Contexto.DistribucionIngredientes.AddRange(distribucionDeIngredientes);
        }

        private void GenerarMovimientosPremezcla()
        {
            List<PolizaInfo> polizasPasesProcesoExistentes =
                GenerarMovimientosPolizaPremezcla();
            GenerarConciliacionMovimientos(polizasPasesProcesoExistentes, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaPremezcla);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaPremezcla()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PolizaPremezcla);
            var polizasPasesProceso = new List<PolizaInfo>();
            List<DistribucionDeIngredientesInfo> distribucionDeIngredientes = Contexto.DistribucionIngredientes;
            for (var i = 0; i < distribucionDeIngredientes.Count; i++)
            {
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(distribucionDeIngredientes[i]));
            }
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.PolizaPremezcla.GetHashCode());
            return polizasPasesProceso;
        }

        #endregion PREMEZCLA

        #region SALIDA POR TRASPASO

        /// <summary>
        /// Valida pases a proceso
        /// </summary>
        private void ValidarSalidaTranspaso()
        {
            List<PolizaInfo> polizasPasesProcesoExistentes =
                GenerarMovimientosPolizaSalidaTraspaso(Contexto.SolicitudProductos);
            GenerarConciliacionMovimientos(polizasPasesProcesoExistentes, new List<PolizaInfo>(), 
                                           TipoPoliza.SalidaTraspaso);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaSalidaTraspaso(List<SolicitudProductoInfo> movimientosGenerarPoliza)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaTraspaso);
            var polizasPasesProceso = new List<PolizaInfo>();
            for (var i = 0; i < movimientosGenerarPoliza.Count; i++)
            {
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(movimientosGenerarPoliza[i]));
            }
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.SalidaTraspaso.GetHashCode());
            return polizasPasesProceso;
        }

        #endregion SALIDA POR TRASPASO

        #region ENTRADA POR AJUSTE

        #region CIERRE DIA PA
        private void ObtenerEntradaPorAjusteCierreDiaPA()
        {
            Contexto.EntradasPorAjuste.Clear();

            List<AlmacenMovimientoInfo> movimientosInventarioFisico = MovimientosInventarioFisico(TipoMovimiento.EntradaPorAjuste);
            List<AlmacenMovimientoInfo> movimientosEntrada = ObtenerMovimientosEntradaSalida(movimientosInventarioFisico);
            List<AlmacenMovimientoInfo> movimientosEntradaSinGastosMateria = (from movimiento in movimientosEntrada
                                                                              let gastoMateriaPrima = Contexto.GastosMateriaPrima.FirstOrDefault(gas => gas.AlmacenMovimientoID == movimiento.AlmacenMovimientoID)
                                                                              where gastoMateriaPrima == null
                                                                              select movimiento).ToList();
            List<PolizaEntradaSalidaPorAjusteModel> entradaSalidaPorAjuste =
                ObtenerPolizaEntradaSalidaPorAjuste(movimientosEntradaSinGastosMateria);
            List<PolizaInfo> polizasEntradaPorAjuste = GenerarMovimientosPolizaEntradasPorAjuste(entradaSalidaPorAjuste);
            if (entradaSalidaPorAjuste == null)
            {
                entradaSalidaPorAjuste = new List<PolizaEntradaSalidaPorAjusteModel>();
            }
            Contexto.EntradasPorAjuste.AddRange(entradaSalidaPorAjuste);
            GenerarConciliacionMovimientos(polizasEntradaPorAjuste, new List<PolizaInfo>(),
                                           TipoPoliza.EntradaAjuste);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaEntradasPorAjuste(List<PolizaEntradaSalidaPorAjusteModel> entradaSalidaPorAjuste)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaAjuste);
            var polizasGastosMateriaPrima = new List<PolizaInfo>();
            var entradaSalidaPorAjusteDiaFolio =
                entradaSalidaPorAjuste.GroupBy(x => new {x.FechaMovimiento, x.FolioMovimiento}).Select(
                    x => new {x.Key.FechaMovimiento, x.Key.FolioMovimiento}).ToList();
            List<PolizaEntradaSalidaPorAjusteModel> polizasGenerar;
            for (var index = 0; index < entradaSalidaPorAjusteDiaFolio.Count; index++)
            {
                polizasGenerar =
                    entradaSalidaPorAjuste.Where(
                        x => x.FechaMovimiento.Equals(entradaSalidaPorAjusteDiaFolio[index].FechaMovimiento)
                          && x.FolioMovimiento.Equals(entradaSalidaPorAjusteDiaFolio[index].FolioMovimiento)).ToList();
                if (polizasGenerar != null && polizasGenerar.Any())
                {
                    polizasGastosMateriaPrima.AddRange(polizaAbstract.GeneraPoliza(polizasGenerar));
                }
            }
            polizasGastosMateriaPrima.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.EntradaAjuste.GetHashCode());
            return polizasGastosMateriaPrima;
        }
        #endregion CIERRE DIA PA

        private List<PolizaEntradaSalidaPorAjusteModel> ObtenerPolizaEntradaSalidaPorAjuste(List<AlmacenMovimientoInfo> movimientos)
        {
            List<PolizaEntradaSalidaPorAjusteModel> entradasSalidaPorAjusteModel = (from movs in movimientos
                                                                                    from det in
                                                                                        Contexto.AlmacenesMovimiento.
                                                                                        AlmacenesMovimientosDetalle
                                                                                    where
                                                                                        movs.AlmacenMovimientoID ==
                                                                                        det.AlmacenMovimientoID
                                                                                    select
                                                                                        new PolizaEntradaSalidaPorAjusteModel
                                                                                            {
                                                                                                AlmacenMovimientoDetalleID = det.AlmacenMovimientoDetalleID,
                                                                                                FolioMovimiento = movs.FolioMovimiento,
                                                                                                Cantidad = det.Cantidad,
                                                                                                Importe = det.Importe,
                                                                                                Precio = det.Precio,
                                                                                                ProductoID = det.ProductoID,
                                                                                                FechaMovimiento = movs.FechaMovimiento,
                                                                                            }).ToList();
            return entradasSalidaPorAjusteModel;
        }

        private List<AlmacenMovimientoInfo> MovimientosInventarioFisico(TipoMovimiento tipoMovimiento)
        {
            List<AlmacenMovimientoInfo> movimientosInventarioFisico =
                Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                    tipo => tipo.TipoMovimiento.TipoMovimientoID == tipoMovimiento.GetHashCode()
                            && (tipo.Status == Estatus.AutorizadoInv.GetHashCode()
                                || tipo.Status == Estatus.DifInvAplicado.GetHashCode()
                                || tipo.Status == Estatus.AplicadoInv.GetHashCode())).Distinct().ToList();
            return movimientosInventarioFisico;
        }

        private List<AlmacenMovimientoInfo> ObtenerMovimientosEntradaSalida(List<AlmacenMovimientoInfo> movimientosInventarioFisico)
        {
            List<AlmacenMovimientoInfo> movimientosEntrada = (from movFisico in movimientosInventarioFisico
                                                              from movs in
                                                                  Contexto.AlmacenesMovimiento.AlmacenesMovimientos
                                                              where
                                                                  movFisico.AlmacenMovimientoID ==
                                                                  movs.AlmacenMovimientoID
                                                              select movs).ToList();
            return movimientosEntrada;
        }

        private void ValidaEntradaPorAjuste()
        {
            List<PolizaInfo> polizasEntradasPorAjuste =
                GenerarMovimientosPolizaEntradaPorAjuste(Contexto.EntradasPorAjuste);
            GenerarConciliacionMovimientos(polizasEntradasPorAjuste, new List<PolizaInfo>(), 
                                           TipoPoliza.EntradaAjuste);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaEntradaPorAjuste(List<PolizaEntradaSalidaPorAjusteModel> entradasPorAjuste)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaAjuste);
            var polizasPasesProceso = new List<PolizaInfo>();
            List<long> foliosMovimiento = entradasPorAjuste.Select(folio => folio.FolioMovimiento).ToList();
            List<PolizaEntradaSalidaPorAjusteModel> polizaEntradaSalidaPorAjuste;
            for (var indexFolio = 0; indexFolio < foliosMovimiento.Count; indexFolio++)
            {
                polizaEntradaSalidaPorAjuste =
                    entradasPorAjuste.Where(folio => folio.FolioMovimiento == foliosMovimiento[indexFolio]).ToList();
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(polizaEntradaSalidaPorAjuste));

            }
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.EntradaAjuste.GetHashCode());
            return polizasPasesProceso;
        }

        #endregion ENTRADA POR AJUSTE

        #region SALIDA POR AJUSTE

        #region CIERRE DIA PA
        private void ObtenerSalidaPorAjusteCierreDiaPA()
        {
            Contexto.SalidasPorAjuste.Clear();

            List<AlmacenMovimientoInfo> movimientosInventarioFisico = MovimientosInventarioFisico(TipoMovimiento.SalidaPorAjuste);
            List<AlmacenMovimientoInfo> movimientosEntrada = ObtenerMovimientosEntradaSalida(movimientosInventarioFisico);
            List<AlmacenMovimientoInfo> movimientosEntradaSinGastosMateria = (from movimiento in movimientosEntrada 
                                                      let gastoMateriaPrima = Contexto.GastosMateriaPrima.FirstOrDefault(gas => gas.AlmacenMovimientoID == movimiento.AlmacenMovimientoID) 
                                                      where gastoMateriaPrima == null 
                                                      select movimiento).ToList();
            List<PolizaEntradaSalidaPorAjusteModel> entradaSalidaPorAjuste =
                ObtenerPolizaEntradaSalidaPorAjuste(movimientosEntradaSinGastosMateria);
            List<PolizaInfo> polizasEntradaPorAjuste = GenerarMovimientosPolizaSalidasPorAjuste(entradaSalidaPorAjuste);

            Contexto.SalidasPorAjuste.AddRange(entradaSalidaPorAjuste);
            GenerarConciliacionMovimientos(polizasEntradaPorAjuste, new List<PolizaInfo>(),
                                           TipoPoliza.SalidaAjuste);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaSalidasPorAjuste(List<PolizaEntradaSalidaPorAjusteModel> entradaSalidaPorAjuste)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaAjuste);
            var polizasGastosMateriaPrima = new List<PolizaInfo>();
            var entradaSalidaPorAjusteDiaFolio =
                entradaSalidaPorAjuste.GroupBy(x => new { x.FechaMovimiento, x.FolioMovimiento, x.ProductoID }).Select(
                    x => new { x.Key.FechaMovimiento, x.Key.FolioMovimiento, x.Key.ProductoID }).ToList();
            List<PolizaEntradaSalidaPorAjusteModel> polizasGenerar;
            IList<PolizaInfo> polizasGeneradas;
            for (var index = 0; index < entradaSalidaPorAjusteDiaFolio.Count; index++)
            {
                polizasGenerar =
                    entradaSalidaPorAjuste.Where(
                        x => x.FechaMovimiento.Equals(entradaSalidaPorAjusteDiaFolio[index].FechaMovimiento)
                          && x.FolioMovimiento.Equals(entradaSalidaPorAjusteDiaFolio[index].FolioMovimiento)
                          && x.ProductoID.Equals(entradaSalidaPorAjusteDiaFolio[index].ProductoID)).ToList();
                if (polizasGenerar != null && polizasGenerar.Any())
                {
                    polizasGeneradas = polizaAbstract.GeneraPoliza(polizasGenerar);
                    polizasGastosMateriaPrima.AddRange(polizasGeneradas);
                }
            }
            polizasGastosMateriaPrima.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.SalidaAjuste.GetHashCode());
            return polizasGastosMateriaPrima;
        }
        #endregion CIERRE DIA PA

        #endregion SALIDA POR AJUSTE
        
        #region PRODUCCION ALIMENTO

        private void ObtenerProduccionAlimento(DateTime fechaInicio, DateTime fechaFinal)
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var produccionFOrmulaPL = new ProduccionFormulaPL();
            List<ProduccionFormulaInfo> produccionesFormula =
                produccionFOrmulaPL.ObtenerProduccionFormulaConciliacion(organizacionID, fechaInicio, fechaFinal);
            if (produccionesFormula == null)
            {
                produccionesFormula = new List<ProduccionFormulaInfo>();
            }
            Contexto.ProduccionesFormula.Clear();
            Contexto.ProduccionesFormula.AddRange(produccionesFormula);
        }        

        private void ValidarProduccionAlimento()
        {
            List<PolizaInfo> polizasProduccionAlimento = GenerarMovimientosPolizaProduccionAlimento();
            GenerarConciliacionMovimientos(polizasProduccionAlimento, new List<PolizaInfo>(),
                                           TipoPoliza.ProduccionAlimento);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaProduccionAlimento()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.ProduccionAlimento);
            var polizasPasesProceso = new List<PolizaInfo>();
            List<ProduccionFormulaInfo> distribucionDeIngredientes = Contexto.ProduccionesFormula;
            for (var i = 0; i < distribucionDeIngredientes.Count; i++)
            {
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(distribucionDeIngredientes[i]));
            }
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.ProduccionAlimento.GetHashCode());
            return polizasPasesProceso;
        }

        #endregion PRODUCCION ALIMENTO

        #region SALIDA CONSUMO

        private void ObtenerMovimientosSalidaPorConsumo()
        {
            var solicitudProductosArray = new SolicitudProductoInfo[Contexto.SolicitudProductos.Count];
            var solicitudProductosOriginal = new SolicitudProductoInfo[Contexto.SolicitudProductos.Count];
            Contexto.SolicitudProductos.CopyTo(solicitudProductosArray);
            Contexto.SolicitudProductos.CopyTo(solicitudProductosOriginal);
            List<SolicitudProductoInfo> solicitudProductosList = solicitudProductosArray.ToList();

            List<PolizaInfo> polizasSalidaConsumo = GenerarPolizasSalidaConsumo(solicitudProductosList);
            Contexto.SolicitudProductos = solicitudProductosList;
            GenerarConciliacionMovimientos(polizasSalidaConsumo, new List<PolizaInfo>(), TipoPoliza.SalidaConsumo);
            Contexto.SolicitudProductos = solicitudProductosOriginal.ToList();
        }

        private List<PolizaInfo> GenerarPolizasSalidaConsumo(List<SolicitudProductoInfo> solicitudesProducto)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaConsumo);
            var polizasSalidaConsumo = new List<PolizaInfo>();
            for (var indexSolicitud = 0; indexSolicitud < solicitudesProducto.Count; indexSolicitud++)
            {
                solicitudesProducto[indexSolicitud].Detalle = solicitudesProducto[indexSolicitud].Detalle.Where(
                    pro =>
                    pro.Producto.FamiliaId != (int) FamiliasEnum.HerramientaYEquipo &&
                    pro.Producto.FamiliaId != (int) FamiliasEnum.Combustibles).ToList();
                polizasSalidaConsumo.AddRange(polizaAbstract.GeneraPoliza(solicitudesProducto[indexSolicitud]));
            }
            polizasSalidaConsumo.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.SalidaConsumo.GetHashCode());
            return polizasSalidaConsumo;
        }

        #endregion SALIDA CONSUMO

        #region SALIDA VENTA PRODUCTO

        private void ObtenerSalidasVentaProducto()
        {
            Contexto.SalidasVentaProductos.Clear();
            var salidaVentaProductoPL = new SalidaProductoPL();
            List<AlmacenMovimientoInfo> movimientosSalidaVentaTraspaso =
                Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                    tipo => tipo.TipoMovimiento.TipoMovimientoID == TipoMovimiento.ProductoSalidaVenta.GetHashCode()
                            || tipo.TipoMovimiento.TipoMovimientoID == TipoMovimiento.ProductoSalidaTraspaso.GetHashCode()).
                    ToList();
            IEnumerable<SalidaProductoInfo> salidasProducto =
                salidaVentaProductoPL.ObtenerSalidasProductioConciliacionPorAlmacenMovimientoXML(
                    movimientosSalidaVentaTraspaso);
            if (salidasProducto == null)
            {
                salidasProducto = new List<SalidaProductoInfo>();
            }
            Contexto.SalidasVentaProductos.AddRange(salidasProducto);
        }

        private void ValidaSalidasVentaProducto()
        {
            List<PolizaInfo> polizasProduccionAlimento = GenerarMovimientosPolizaSalidasVentaProducto();
            GenerarConciliacionMovimientos(polizasProduccionAlimento, new List<PolizaInfo>(),
                                           TipoPoliza.SalidaVentaProducto);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaSalidasVentaProducto()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaVentaProducto);
            var polizasPasesProceso = new List<PolizaInfo>();
            List<SalidaProductoInfo> salidasProducto = Contexto.SalidasVentaProductos;
            for (var i = 0; i < salidasProducto.Count; i++)
            {
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(salidasProducto[i]));
            }
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.SalidaVentaProducto.GetHashCode());
            return polizasPasesProceso;
        }

        #endregion SALIDA VENTA PRODUCTO

        #region GASTOS MATERIA PRIMA

        private void ObtenerGastosMateriaPrima()
        {
            List<AlmacenMovimientoInfo> almacenesMovimiento =
                Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                    tipo => tipo.TipoMovimiento.TipoMovimientoID == TipoMovimiento.EntradaPorAjuste.GetHashCode()
                            || tipo.TipoMovimiento.TipoMovimientoID == TipoMovimiento.SalidaPorAjuste.GetHashCode()).
                    ToList();
            var gastoMateriaPrimaPL = new GastoMateriaPrimaPL();
            Contexto.GastosMateriaPrima.Clear();
            IEnumerable<GastoMateriaPrimaInfo> gastosMateriaPrima =
                gastoMateriaPrimaPL.ObtenerGastosMateriaPrimaPorAlmacenMovimientoXML(almacenesMovimiento);
            if (gastosMateriaPrima == null)
            {
                gastosMateriaPrima = new List<GastoMateriaPrimaInfo>();
            }
            Contexto.GastosMateriaPrima.AddRange(gastosMateriaPrima);
        }

        private void ValidaGastosMateriaPrima()
        {
            List<PolizaInfo> polizasGastosMateriaPrima = GenerarMovimientosPolizaGastosMateriaprima();
            GenerarConciliacionMovimientos(polizasGastosMateriaPrima, new List<PolizaInfo>(),
                                           TipoPoliza.GastosMateriaPrima);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaGastosMateriaprima()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.GastosMateriaPrima);
            var polizasGastosMateriaPrima = new List<PolizaInfo>();
            List<GastoMateriaPrimaInfo> gastosMateriaPrima = Contexto.GastosMateriaPrima;
            for (var i = 0; i < gastosMateriaPrima.Count; i++)
            {
                polizasGastosMateriaPrima.AddRange(polizaAbstract.GeneraPoliza(gastosMateriaPrima[i]));
            }
            polizasGastosMateriaPrima.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.GastosMateriaPrima.GetHashCode());
            return polizasGastosMateriaPrima;
        }

        #endregion GASTOS MATERIA PRIMA

        #region SUB PRODUCTOS

        private void ObtenerMovimientosSubProductos()
        {
            List<AlmacenMovimientoInfo> movimientosEntradaCompra =
                Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                    tipo =>
                    tipo.TipoMovimiento.TipoMovimientoID == TipoMovimiento.EntradaPorCompra.GetHashCode() &&
                    tipo.Status == Estatus.AplicadoInv.GetHashCode()).ToList();
            List<AlmacenMovimientoInfo> movimientosSalidaAlmacen =
                Contexto.AlmacenesMovimiento.AlmacenesMovimientos.Where(
                    tipo => tipo.TipoMovimiento.TipoMovimientoID == TipoMovimiento.SalidaAlmacen.GetHashCode() &&
                            tipo.Status == Estatus.AplicadoInv.GetHashCode()).ToList();
            var movimientosDetalle = (from movEntrada in movimientosEntradaCompra
                                      from detEntrada in
                                          Contexto.AlmacenesMovimiento.
                                          AlmacenesMovimientosDetalle
                                      where movEntrada.AlmacenMovimientoID == detEntrada.AlmacenMovimientoID
                                      let entradas =
                                          new
                                              {
                                                  movEntrada.AlmacenMovimientoID,
                                                  detEntrada.AlmacenMovimientoDetalleID,
                                                  detEntrada.ProductoID,
                                                  movEntrada.FechaMovimiento
                                              }
                                      from movSalida in movimientosSalidaAlmacen
                                      from detSalida in
                                          Contexto.AlmacenesMovimiento.
                                          AlmacenesMovimientosDetalle
                                      where movSalida.AlmacenMovimientoID == detSalida.AlmacenMovimientoID
                                            && movSalida.FechaMovimiento.Equals(entradas.FechaMovimiento)
                                            && movSalida.AlmacenMovimientoID == (entradas.AlmacenMovimientoID + 1)
                                      let salidas =
                                          new
                                              {
                                                  AlmacenMovimientoEntrada = movEntrada.AlmacenMovimientoID,
                                                  movSalida.AlmacenMovimientoID,
                                                  detSalida.AlmacenMovimientoDetalleID,
                                                  detSalida.Precio,
                                                  detSalida.Importe,
                                                  detSalida.Cantidad,
                                                  detSalida.ProductoID
                                              }
                                      select new {Salidas = salidas}).ToList();
            var movimientosSubProductos =
                movimientosDetalle.GroupBy(id => id.Salidas.AlmacenMovimientoEntrada).Select(
                    id =>
                    new
                        {
                            AlmacenMovimientoEntrada =
                        id.Select(entr => entr.Salidas.AlmacenMovimientoEntrada).FirstOrDefault(),
                            Detalle =
                        id.Select(
                            det =>
                            new
                                {
                                    det.Salidas.AlmacenMovimientoID,
                                    det.Salidas.AlmacenMovimientoDetalleID,
                                    det.Salidas.Precio,
                                    det.Salidas.Importe,
                                    det.Salidas.Cantidad,
                                    det.Salidas.ProductoID
                                }).ToList()
                        }).ToList();
            List<EntradaProductoInfo> entradasProducto = ObtenerEntradasProducto(movimientosSubProductos);
            GenerarContenedorEntradaMateriaPrima(movimientosSubProductos, entradasProducto);
        }

        private List<EntradaProductoInfo> ObtenerEntradasProducto(IEnumerable<dynamic> movimientosSubProductos)
        {
            List<AlmacenMovimientoInfo> movimientosEntrada =
                movimientosSubProductos.Select(id => new AlmacenMovimientoInfo
                                                         {
                                                             AlmacenMovimientoID = id.AlmacenMovimientoEntrada
                                                         }).ToList();
            var entradaProductoPL = new EntradaProductoPL();
            List<EntradaProductoInfo> entradasProducto =
                entradaProductoPL.ObtenerEntradasPorAlmacenMovimientoXML(movimientosEntrada);
            return entradasProducto;
        }

        private void GenerarContenedorEntradaMateriaPrima(IEnumerable<dynamic> movimientosSubProductos, List<EntradaProductoInfo> entradasProducto)
        {
            Contexto.SubProductos.Clear();
            List<ContenedorEntradaMateriaPrimaInfo> contenedor =
                entradasProducto.Select(ep => new ContenedorEntradaMateriaPrimaInfo
                                                  {
                                                      EntradaProducto = new EntradaProductoInfo
                                                                            {
                                                                                PremezclaInfo = new PremezclaInfo
                                                                                                    {
                                                                                                        ListaPremezclaDetalleInfos = new List<PremezclaDetalleInfo>()
                                                                                                    },
                                                                                Folio = ep.Folio,
                                                                                EntradaProductoId = ep.EntradaProductoId,
                                                                                AlmacenMovimiento = ep.AlmacenMovimiento,
                                                                                Organizacion = ep.Organizacion,
                                                                                Fecha = ep.Fecha,
                                                                                Producto = ep.Producto
                                                                            }
                                                  }).ToList();
            var productoPL = new ProductoPL();
            IList<ProductoInfo> productos = productoPL.ObtenerPorEstados(EstatusEnum.Activo);
            var unidadMedicionPL = new UnidadMedicionPL();
            IList<UnidadMedicionInfo> unidadesMedicion = unidadMedicionPL.ObtenerTodos(EstatusEnum.Activo);
            PremezclaDetalleInfo premezclaDetalle;
            ProductoInfo producto;
            contenedor.ForEach(datos =>
                                   {
                                       dynamic movimiento = movimientosSubProductos.FirstOrDefault(
                                               mov =>
                                               mov.AlmacenMovimientoEntrada ==
                                               datos.EntradaProducto.AlmacenMovimiento.AlmacenMovimientoID);
                                        if (movimiento != null)
                                        {
                                            for (int indexDetalle = 0; indexDetalle < movimiento.Detalle.Count; indexDetalle++)
                                            {
                                                producto = productos.FirstOrDefault(
                                                    id =>
                                                    id.ProductoId ==
                                                    movimiento.Detalle[indexDetalle].
                                                        ProductoID);
                                                producto.Descripcion = producto.ProductoDescripcion;
                                                producto.UnidadMedicion =
                                                    unidadesMedicion.FirstOrDefault(
                                                        id => id.UnidadID == producto.UnidadId);
                                                premezclaDetalle = new PremezclaDetalleInfo
                                                                       {
                                                                           Producto = producto,
                                                                           Kilogramos =
                                                                               movimiento.Detalle[indexDetalle].Cantidad,
                                                                           Lote = new AlmacenInventarioLoteInfo
                                                                                      {
                                                                                          PrecioPromedio =
                                                                                              movimiento.Detalle[
                                                                                                  indexDetalle].Precio
                                                                                      },
                                                                       };
                                                datos.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos.Add(premezclaDetalle);
                                            }
                                        }
                                   });
            Contexto.SubProductos.AddRange(contenedor);
        }

        private void ValidarSubProductos()
        {
            List<PolizaInfo> polizasSubProductos =
                GenerarMovimientosPolizaSubProducto(Contexto.SubProductos);
            GenerarConciliacionMovimientos(polizasSubProductos, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaSubProducto);
        }

        private List<PolizaInfo> GenerarMovimientosPolizaSubProducto(List<ContenedorEntradaMateriaPrimaInfo> subProductos)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PolizaSubProducto);
            var polizasPasesProceso = new List<PolizaInfo>();
            for (var indexFolio = 0; indexFolio < subProductos.Count; indexFolio++)
            {
                polizasPasesProceso.AddRange(polizaAbstract.GeneraPoliza(subProductos[indexFolio]));
            }
            polizasPasesProceso.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.PolizaSubProducto.GetHashCode());
            return polizasPasesProceso;
        }

        #endregion SUB PRODUCTOS

        #region CONTRATO

        private void ObtenerDatosContrato(DateTime fechaInicio, DateTime fechaFinal)
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var contratoPL = new ContratoPL();
            List<ContratoInfo> contratosParciales = contratoPL.ObtenerPorFechasConciliacion(organizacionID, fechaInicio, fechaFinal);

            List<AlmacenMovimientoDetalle> movimientosConContrato =
                Contexto.AlmacenesMovimiento.AlmacenesMovimientosDetalle.Join(contratosParciales,
                                                                              movs => movs.ContratoId,
                                                                              cont => cont.ContratoId,
                                                                              (movs, cont) => movs).ToList();

            var almacenMovimientoOtrosCostosPL = new AlmacenMovimientoCostoPL();
            IEnumerable<AlmacenMovimientoCostoInfo> almacenMovimientoCostos = almacenMovimientoOtrosCostosPL.
                ObtenerAlmacenMovimientoCostoPorContratoXML(contratosParciales);

            Contexto.PolizasContrato = new List<PolizaContratoModel>();
            PolizaContratoModel polizaContrato;
            contratosParciales.ToList().ForEach(datos =>
                                                    {
                                                        polizaContrato = new PolizaContratoModel {Contrato = datos};

                                                        polizaContrato.AlmacenMovimiento =
                                                            (from amd in movimientosConContrato
                                                             from am in
                                                                 Contexto.AlmacenesMovimiento.AlmacenesMovimientos
                                                             where amd.AlmacenMovimientoID == am.AlmacenMovimientoID
                                                             select am).FirstOrDefault();
                                                        if (polizaContrato.AlmacenMovimiento == null)
                                                        {
                                                            polizaContrato.AlmacenMovimiento = new AlmacenMovimientoInfo();
                                                        }
                                                        polizaContrato.OtrosCostos =
                                                            almacenMovimientoCostos.Where(
                                                                alm =>
                                                                alm.Contrato.ContratoId ==
                                                                datos.ContratoId).
                                                                Select(
                                                                    costo => new CostoInfo
                                                                                 {
                                                                                     CostoID = costo.Costo.CostoID,
                                                                                     Descripcion =
                                                                                         costo.Costo.Descripcion,
                                                                                     ToneladasCosto =
                                                                                         costo.Cantidad/1000,
                                                                                     ImporteCosto = costo.Importe,
                                                                                     Proveedor = costo.Proveedor,
                                                                                     CuentaSap = costo.CuentaSap,
                                                                                     TieneCuenta = costo.TieneCuenta,
                                                                                     FechaCosto = costo.Costo.FechaCosto,
                                                                                 }).ToList();
                                                        Contexto.PolizasContrato.Add(polizaContrato);
                                                    });
        }

        private void GenerarConciliacionContratoBodegaTercerosCompraTotal()
        {
            List<PolizaContratoModel> contratoBodegaTerceros =
                Contexto.PolizasContrato.Where(
                    tipo => tipo.Contrato.Parcial == CompraParcialEnum.Total
                            && tipo.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaTercero.GetHashCode())
                    .ToList();
            List<PolizaInfo> polizasContratoBodegaTerceros = GenerarPolizaBodegaTercerosTransito(
                contratoBodegaTerceros, TipoPoliza.PolizaContratoTerceros);
            GenerarConciliacionMovimientos(polizasContratoBodegaTerceros, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaContratoTerceros);
        }

        private void GenerarConciliacionContratoTransitoCompraTotal()
        {
            List<PolizaContratoModel> contratoBodegaTerceros =
                Contexto.PolizasContrato.Where(
                    tipo => tipo.Contrato.Parcial == CompraParcialEnum.Total
                            && tipo.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.EnTransito.GetHashCode())
                    .ToList();
            List<PolizaInfo> polizasContratoBodegaTerceros = GenerarPolizaBodegaTercerosTransito(
                contratoBodegaTerceros, TipoPoliza.PolizaContratoTransito);
            GenerarConciliacionMovimientos(polizasContratoBodegaTerceros, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaContratoTransito);
        }

        private void GenerarConciliacionContratoParcial()
        {
            List<PolizaContratoModel> contratoBodegaTerceros =
                Contexto.PolizasContrato.Where(
                    tipo =>
                    tipo.Contrato.Parcial == CompraParcialEnum.Parcial &&
                    tipo.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaTercero.GetHashCode()).ToList();
            List<PolizaInfo> polizasContratoBodegaTerceros = GenerarPolizaContratoParcialidad(
                contratoBodegaTerceros, TipoPoliza.PolizaContratoParcialidades);
            polizasContratoBodegaTerceros.ForEach(
                tipo => tipo.TipoPolizaID = TipoPoliza.PolizaContratoTerceros.GetHashCode());
            GenerarConciliacionMovimientos(polizasContratoBodegaTerceros, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaContratoParcialidades);

            contratoBodegaTerceros =
                Contexto.PolizasContrato.Where(
                    tipo =>
                    tipo.Contrato.Parcial == CompraParcialEnum.Parcial &&
                    tipo.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.EnTransito.GetHashCode()).ToList();
            polizasContratoBodegaTerceros = GenerarPolizaContratoParcialidad(
                contratoBodegaTerceros, TipoPoliza.PolizaContratoParcialidades);
            polizasContratoBodegaTerceros.ForEach(
                tipo => tipo.TipoPolizaID = TipoPoliza.PolizaContratoTransito.GetHashCode());
            GenerarConciliacionMovimientos(polizasContratoBodegaTerceros, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaContratoParcialidades);
        }

        private List<PolizaInfo> GenerarPolizaBodegaTercerosTransito(List<PolizaContratoModel> contratoBodegaTerceros, TipoPoliza tipoPoliza)
        {
            var polizasContratoOtrosCostos = new List<PolizaInfo>();
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(tipoPoliza);
            for (var indexPolizasContrato = 0; indexPolizasContrato < contratoBodegaTerceros.Count; indexPolizasContrato++)
            {
                polizasContratoOtrosCostos.AddRange(
                    polizaAbstract.GeneraPoliza(contratoBodegaTerceros[indexPolizasContrato]));
            }
            polizasContratoOtrosCostos.ForEach(tipo => tipo.TipoPolizaID = tipoPoliza.GetHashCode());
            return polizasContratoOtrosCostos;
        }

        private List<PolizaInfo> GenerarPolizaContratoParcialidad(List<PolizaContratoModel> contratoBodegaTerceros, TipoPoliza tipoPoliza)
        {
            var polizasContratoOtrosCostos = new List<PolizaInfo>();
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(tipoPoliza);
            PolizaContratoModel polizaContratoModel;
            var conceptos = new HashSet<string>();
            IList<PolizaInfo> polizaGenerada;
            List<string> conceptosPoliza;
            for (var indexPolizasContrato = 0; indexPolizasContrato < contratoBodegaTerceros.Count; indexPolizasContrato++)
            {
                for (var indexParcialidad = 0
                    ; indexParcialidad < contratoBodegaTerceros[indexPolizasContrato].Contrato.ListaContratoParcial.Count
                    ; indexParcialidad++)
                {
                    contratoBodegaTerceros[indexPolizasContrato].Contrato.Fecha =
                        contratoBodegaTerceros[indexPolizasContrato].Contrato.
                            ListaContratoParcial[indexParcialidad].Contrato.Fecha;
                    polizaContratoModel = new PolizaContratoModel
                                              {
                                                  Contrato = contratoBodegaTerceros[indexPolizasContrato].Contrato,
                                                  ContratoParcial =
                                                      contratoBodegaTerceros[indexPolizasContrato].Contrato.
                                                      ListaContratoParcial[indexParcialidad],
                                                  AlmacenMovimiento = contratoBodegaTerceros[indexPolizasContrato].AlmacenMovimiento
                                              };
                    polizaGenerada = polizaAbstract.GeneraPoliza(polizaContratoModel);
                    conceptosPoliza = polizaGenerada.Select(con => con.Concepto).ToList();
                    for (var indexConceptos = 0; indexConceptos < conceptosPoliza.Count; indexConceptos++)
                    {
                        if (!conceptos.Contains(conceptosPoliza[indexConceptos]))
                        {
                            conceptos.Add(conceptosPoliza[indexConceptos]);
                            polizasContratoOtrosCostos.AddRange(
                                polizaGenerada.Where(con => con.Concepto.Equals(conceptosPoliza[indexConceptos])));
                        }
                    }
                }
            }
            polizasContratoOtrosCostos.ForEach(tipo => tipo.TipoPolizaID = tipoPoliza.GetHashCode());
            return polizasContratoOtrosCostos;
        }

        #endregion CONTRATO        

        #region ENTRADA GANADO

        private void ObtenerEntradasGanado(DateTime fechaInicial, DateTime fechaFinal)
        {
            var entradaGanadoCosteoPL = new EntradaGanadoCosteoPL();
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            List<EntradaGanadoCosteoInfo> entradasGanadoCosteo =
                entradaGanadoCosteoPL.ObtenerEntradasPorFechasConciliacion(organizacionID, fechaInicial, fechaFinal);
            if (entradasGanadoCosteo == null)
            {
                entradasGanadoCosteo = new List<EntradaGanadoCosteoInfo>();
            }
            List<ContenedorCosteoEntradaGanadoInfo> contenedorEntradasGanado =
                GeneraContenedorEntradaGanado(entradasGanadoCosteo);
            Contexto.EntradasGanado.Clear();
            if (contenedorEntradasGanado == null)
            {
                contenedorEntradasGanado = new List<ContenedorCosteoEntradaGanadoInfo>();
            }
            Contexto.EntradasGanado.AddRange(contenedorEntradasGanado);
            List<PolizaInfo> polizasEntrada = GenerarPolizasEntradaGanado(contenedorEntradasGanado);
            GenerarConciliacionMovimientos(polizasEntrada, new List<PolizaInfo>(), TipoPoliza.EntradaGanado);
        }

        private List<ContenedorCosteoEntradaGanadoInfo> GeneraContenedorEntradaGanado(List<EntradaGanadoCosteoInfo> entradasGanadoCosteo)
        {
            var cuentaPL = new CuentaPL();
            const string CUENTA_INVENTARIO = "CTAINVTRAN";
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();

            var contenedorEntradasGananado = new List<ContenedorCosteoEntradaGanadoInfo>();
            ContenedorCosteoEntradaGanadoInfo contenedor;

            var entradaGanadoPL = new EntradaGanadoPL();
            List<int> entradas = entradasGanadoCosteo.Select(id => id.EntradaGanadoID).ToList();
            List<EntradaGanadoInfo> entradasGanado = entradaGanadoPL.ObtenerEntradasPorIDs(entradas);

            for (var indexEntradas = 0; indexEntradas < entradasGanadoCosteo.Count; indexEntradas++)
            {
                entradasGanadoCosteo[indexEntradas].ListaCostoEntrada.ForEach(costo =>
                {
                    if (!string.IsNullOrWhiteSpace(costo.DescripcionCuenta))
                    {
                        return;
                    }
                    var claveContable = cuentaPL.ObtenerPorClaveCuentaOrganizacion(CUENTA_INVENTARIO, organizacionID);
                    if (claveContable != null)
                    {
                        costo.DescripcionCuenta = claveContable.Descripcion;
                    }
                });
                contenedor = new ContenedorCosteoEntradaGanadoInfo
                {
                    EntradaGanado = entradasGanado.FirstOrDefault(id => id.EntradaGanadoID == entradasGanadoCosteo[indexEntradas].EntradaGanadoID),
                    EntradaGanadoCosteo = entradasGanadoCosteo[indexEntradas]
                };
                contenedorEntradasGananado.Add(contenedor);
            }
            contenedorEntradasGananado = contenedorEntradasGananado.Where(gan => gan.EntradaGanado.OrganizacionOrigenID != 5).ToList();
            return contenedorEntradasGananado;
        }

        private List<PolizaInfo> GenerarPolizasEntradaGanado(List<ContenedorCosteoEntradaGanadoInfo> contenedorCosteo)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaGanado);
            var polizasVentaGanado = new List<PolizaInfo>();
            for (var indexVenta = 0; indexVenta < contenedorCosteo.Count; indexVenta++)
            {
                polizasVentaGanado.AddRange(polizaAbstract.GeneraPoliza(contenedorCosteo[indexVenta]));
            }
            polizasVentaGanado.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.EntradaGanado.GetHashCode());
            return polizasVentaGanado;
        }

        #endregion ENTRADA GANADO

        #region SACRIFICIO

        private void ObtenerSacrificioGanado(DateTime fechaInicial, DateTime fechaFinal)
        {
            var loteSacrificioPL = new LoteSacrificioPL();
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            IEnumerable<PolizaSacrificioModel> lotesSacrificio =
                loteSacrificioPL.ObtenerPolizasSacrificioConciliacion(organizacionID, fechaInicial, fechaFinal);
            Contexto.LotesSacrificio.Clear();
            if (lotesSacrificio == null)
            {
                lotesSacrificio = new List<PolizaSacrificioModel>();
            }
            Contexto.LotesSacrificio.AddRange(lotesSacrificio);

            List<PolizaInfo> polizasSacrificioGanado212 =
                GenerarPolizasSacrificioGanado(ParametrosEnum.PolizaSacrificio212);
            List<PolizaInfo> polizasSacrificioGanado300 =
                GenerarPolizasSacrificioGanado(ParametrosEnum.PolizaSacrificio300);
            GenerarConciliacionMovimientos(polizasSacrificioGanado212, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaSacrificio);
            GenerarConciliacionMovimientos(polizasSacrificioGanado300, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaSacrificio);
        }

        private List<PolizaInfo> GenerarPolizasSacrificioGanado(ParametrosEnum division)
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PolizaSacrificio);
            var polizasVentaGanado = new List<PolizaInfo>();
            List<PolizaSacrificioModel> sacrificiosGanado = Contexto.LotesSacrificio;
            List<DateTime> ordenesSacrificio = sacrificiosGanado.Select(muerte => muerte.Fecha).Distinct().ToList();
            List<PolizaSacrificioModel> sacrificiosGanadoPorDia;
            sacrificiosGanado.ForEach(prov => prov.ParametroProveedor = division.ToString());
            for (var indexVenta = 0; indexVenta < ordenesSacrificio.Count; indexVenta++)
            {
                sacrificiosGanadoPorDia =
                    sacrificiosGanado.Where(folio => folio.Fecha.Equals(ordenesSacrificio[indexVenta])).ToList();
                polizasVentaGanado.AddRange(polizaAbstract.GeneraPoliza(sacrificiosGanadoPorDia));
            }
            polizasVentaGanado.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.PolizaSacrificio.GetHashCode());
            return polizasVentaGanado;
        }

        #endregion SACRIFICIO

        #region SALIDA MUERTE

        private void ObtenerMuertesGanado(DateTime fechaInicial, DateTime fechaFinal)
        {
            var animalMovimientoPL = new AnimalMovimientoPL();
            IEnumerable<AnimalMovimientoInfo> animalesMovimientoMuerte = animalMovimientoPL.ObtenerAnimalesMuertos(
                AuxConfiguracion.ObtenerOrganizacionUsuario(), fechaInicial, fechaFinal);

            var animalCostoBL = new AnimalCostoBL();
            List<AnimalInfo> animales = animalesMovimientoMuerte.Select(x => new AnimalInfo
                                                                                 {
                                                                                     AnimalID = x.AnimalID,
                                                                                     Arete = x.Arete
                                                                                 }).ToList();
            List<AnimalCostoInfo> animalesCosto = animalCostoBL.ObtenerCostosAnimal(animales);
            List<AnimalCostoInfo> animalCostoAgrupado = (from costo in animalesCosto
                                                         group costo by new {costo.AnimalID, costo.CostoID}
                                                         into agrupado
                                                         let animalLet = animales.FirstOrDefault(cos=> cos.AnimalID == agrupado.Key.AnimalID)
                                                         select new AnimalCostoInfo
                                                                    {
                                                                        AnimalID = agrupado.Key.AnimalID,
                                                                        CostoID = agrupado.Key.CostoID,
                                                                        Importe = agrupado.Sum(cos => cos.Importe),
                                                                        FolioReferencia =
                                                                            agrupado.Select(cos => cos.FolioReferencia).
                                                                            FirstOrDefault(),
                                                                        FechaCosto =
                                                                            agrupado.Select(cos => cos.FechaCosto).
                                                                            FirstOrDefault(),
                                                                            Arete = animalLet != null ? animalLet.Arete : string.Empty
                                                                    }).ToList();
            AnimalMovimientoInfo animalMovimiento;
            animalCostoAgrupado.ForEach(x =>
                                            {
                                                animalMovimiento =
                                                    animalesMovimientoMuerte.FirstOrDefault(
                                                        id => id.AnimalID == x.AnimalID);
                                                x.FechaCosto = animalMovimiento.FechaMovimiento;
                                                x.OrganizacionID = animalMovimiento.OrganizacionID;
                                            });
            Contexto.AnimalesCosto.Clear();
            Contexto.AnimalesCosto.AddRange(animalCostoAgrupado);

            List<PolizaInfo> polizasMuerte = GenerarPolizasMuertesGanado();
            GenerarConciliacionMovimientos(polizasMuerte, new List<PolizaInfo>(), TipoPoliza.SalidaMuerte);
        }

        private List<PolizaInfo> GenerarPolizasMuertesGanado()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaMuerte);
            var polizasVentaGanado = new List<PolizaInfo>();
            List<AnimalCostoInfo> costosGanado = Contexto.AnimalesCosto;
            List<long> animalesCosto = costosGanado.Select(muerte => muerte.AnimalID).Distinct().ToList();
            List<AnimalCostoInfo> costosPorAnimalMuerto;
            for (var indexVenta = 0; indexVenta < animalesCosto.Count; indexVenta++)
            {
                costosPorAnimalMuerto =
                    costosGanado.Where(folio => folio.AnimalID == animalesCosto[indexVenta]).ToList();
                polizasVentaGanado.AddRange(polizaAbstract.GeneraPoliza(costosPorAnimalMuerto));
            }
            polizasVentaGanado.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.SalidaMuerte.GetHashCode());
            return polizasVentaGanado;
        }

        #endregion SALIDA MUERTE

        #region SALIDA VENTA

        private void ObtenerVentasGanado(DateTime fechaInicial, DateTime fechaFinal)
        {
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var ventaGanadoPL = new VentaGanadoPL();
            List<ContenedorVentaGanado> ventasGanado = ventaGanadoPL.ObtenerPorFechaConciliacion(fechaInicial,
                                                                                                 fechaFinal,
                                                                                                 organizacionID);
            if (ventasGanado == null)
            {
                ventasGanado = new List<ContenedorVentaGanado>();
            }
            Contexto.VentasGanado.Clear();
            Contexto.VentasGanado.AddRange(ventasGanado);

            List<PolizaInfo> polizasVentaGanado = GenerarPolizasVentasGanado();
            GenerarConciliacionMovimientos(polizasVentaGanado, new List<PolizaInfo>(),
                                           TipoPoliza.SalidaVenta);
        }

        private List<PolizaInfo> GenerarPolizasVentasGanado()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaVenta);
            var polizasVentaGanado = new List<PolizaInfo>();
            List<ContenedorVentaGanado> ventasGanado = Contexto.VentasGanado;
            List<int> foliosVenta = ventasGanado.Select(venta => venta.VentaGanado.VentaGanadoID).Distinct().ToList();
            List<ContenedorVentaGanado> ventasGanadoFolio;
            for (var indexVenta = 0; indexVenta < foliosVenta.Count; indexVenta++)
            {
                ventasGanadoFolio =
                    ventasGanado.Where(folio => folio.VentaGanado.VentaGanadoID == foliosVenta[indexVenta]).ToList();
                polizasVentaGanado.AddRange(polizaAbstract.GeneraPoliza(ventasGanadoFolio));
            }
            polizasVentaGanado.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.SalidaVenta.GetHashCode());
            return polizasVentaGanado;
        }

        #endregion SALIDA VENTA

        #region GASTOS INVENTARIO

        private void ObtenerGastosInventario(DateTime fechaInicial, DateTime fechaFinal)
        {
            var gastoInventarioPL = new GastoInventarioPL();
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            IEnumerable<GastoInventarioInfo> gastosInventario =
                gastoInventarioPL.ObtenerGastosInventarioPorFechaConciliacion(organizacionID, fechaInicial, fechaFinal);
            if (gastosInventario == null)
            {
                gastosInventario = new List<GastoInventarioInfo>();
            }
            Contexto.GastosInventario.Clear();
            Contexto.GastosInventario.AddRange(gastosInventario);

            List<PolizaInfo> polizasGastosInventario = GenerarPolizasGastosInventario();
            GenerarConciliacionMovimientos(polizasGastosInventario, new List<PolizaInfo>(), TipoPoliza.GastosInventario);
        }

        private List<PolizaInfo> GenerarPolizasGastosInventario()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.GastosInventario);
            var polizasGastosInventario = new List<PolizaInfo>();
            List<GastoInventarioInfo> gastosInventario = Contexto.GastosInventario;
            for (var indexVenta = 0; indexVenta < gastosInventario.Count; indexVenta++)
            {
                polizasGastosInventario.AddRange(polizaAbstract.GeneraPoliza(gastosInventario[indexVenta]));
            }
            polizasGastosInventario.ForEach(tipo => tipo.TipoPolizaID = TipoPoliza.GastosInventario.GetHashCode());
            return polizasGastosInventario;
        }

        #endregion GASTOS INVENTARIO

        #region TRASPASO GANADO

        private void ObtenerSacrificiosPorTraspasosGanado()
        {
            var loteSacrificioPL = new LoteSacrificioPL();
            List<PolizaSacrificioModel> lotesSacrificiosTraspasos =
                loteSacrificioPL.ObtenerDatosConciliacionSacrificioTraspasoGanado(Contexto.InterfaceSalidasTraspasos);
            if (lotesSacrificiosTraspasos != null)
            {
                Contexto.InterfaceSalidasTraspasos.ForEach(fecha => fecha.FechaEnvioTraspaso = fecha.FechaEnvio);
                Contexto.InterfaceSalidasTraspasos.ForEach(fecha => fecha.ListaInterfaceSalidaTraspasoDetalle.ForEach(
                    fec =>
                        {
                            fecha.FechaEnvio =
                                lotesSacrificiosTraspasos.Where(
                                    id =>
                                    id.
                                        InterfaceSalidaTraspasoDetalleID ==
                                    fec.
                                        InterfaceSalidaTraspasoDetalleID)
                                    .Select(fecSacr => fecSacr.Fecha)
                                    .FirstOrDefault();
                        }));
            }

            List<PolizaInfo> polizasTraspasoGanado = GenerarPolizasSacrificioTraspasoGanado();
            GenerarConciliacionMovimientos(polizasTraspasoGanado, new List<PolizaInfo>(),
                                           TipoPoliza.PolizaSacrificioTraspasoGanado);
        }

        private void ObtenerTraspasosGanado(DateTime fechaInicial, DateTime fechaFinal)
        {
            var interfaceSalidaTraspasoPL = new InterfaceSalidaTraspasoPL();
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            List<InterfaceSalidaTraspasoInfo> traspasosGanado =
                interfaceSalidaTraspasoPL.ObtenerTraspasosPorFechaConciliacion(organizacionID, fechaInicial, fechaFinal);
            if (traspasosGanado == null)
            {
                traspasosGanado = new List<InterfaceSalidaTraspasoInfo>();
            }
            Contexto.InterfaceSalidasTraspasos.Clear();
            Contexto.InterfaceSalidasTraspasos.AddRange(traspasosGanado);

            List<PolizaInfo> polizasTraspasoGanado = GenerarPolizasTraspasoGanado();
            GenerarConciliacionMovimientos(polizasTraspasoGanado, new List<PolizaInfo>(),
                                           TipoPoliza.SalidaGanado);
        }

        private List<PolizaInfo> GenerarPolizasTraspasoGanado()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaGanado);
            var polizasTraspasoGanado = new List<PolizaInfo>();
            List<InterfaceSalidaTraspasoInfo> traspasosGanado = Contexto.InterfaceSalidasTraspasos;
            for (var indexVenta = 0; indexVenta < traspasosGanado.Count; indexVenta++)
            {
                polizasTraspasoGanado.AddRange(polizaAbstract.GeneraPoliza(traspasosGanado[indexVenta]));
            }
            polizasTraspasoGanado.ForEach(
                tipo => tipo.TipoPolizaID = TipoPoliza.SalidaGanado.GetHashCode());
            return polizasTraspasoGanado;
        }

        private List<PolizaInfo> GenerarPolizasSacrificioTraspasoGanado()
        {
            polizaAbstract = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PolizaSacrificioTraspasoGanado);
            var polizasTraspasoGanado = new List<PolizaInfo>();
            List<InterfaceSalidaTraspasoInfo> traspasosGanado = Contexto.InterfaceSalidasTraspasos;
            for (var indexVenta = 0; indexVenta < traspasosGanado.Count; indexVenta++)
            {
                var polizas = polizaAbstract.GeneraPoliza(traspasosGanado[indexVenta]);
                if (polizas != null && polizas.Any())
                {
                    polizasTraspasoGanado.AddRange(polizas);
                }
            }
            polizasTraspasoGanado.ForEach(
                tipo => tipo.TipoPolizaID = TipoPoliza.PolizaSacrificioTraspasoGanado.GetHashCode());
            return polizasTraspasoGanado;
        }

        #endregion TRASPASO GANADO

        #region GENERAR CONCILIACION

        private void GenerarConciliacionMovimientos(List<PolizaInfo> polizasPasesProcesoExistentes
                                                  , List<PolizaInfo> polizasPasesProcesoNoExistenten
                                                  , TipoPoliza tipoPoliza)
        {
            try
            {
                List<PolizaInfo> polizasDePP = ObtenerPolizasPorTipo(tipoPoliza, polizasPasesProcesoExistentes);

                if (tipoPoliza == TipoPoliza.GastosMateriaPrima)
                {
                    polizasDePP.AddRange(ObtenerPolizasPorTipo(TipoPoliza.EntradaAjuste, polizasPasesProcesoExistentes));
                    polizasDePP.AddRange(ObtenerPolizasPorTipo(TipoPoliza.SalidaAjuste, polizasPasesProcesoExistentes));
                }

                dynamic polizasPasesProcesoModel;
                dynamic polizaPaseProcesoModel;
                string prefijoPoliza;
                List<PropertyInfo> propiedades;
                var propiedadesValor = new Dictionary<string, string>();
                var fechaValor = new Dictionary<string, string>();
                polizasPasesProcesoModel = ObtenerColeccionConciliacionMovimientos(tipoPoliza, propiedadesValor,
                                                                                   fechaValor, out prefijoPoliza,
                                                                                   out propiedades);
                List<PolizaInfo> validarPoliza;
                List<PolizaInfo> polizaBaseDatos;
                var conciliacionPolizasMovimientosModel = new ConciliacionPolizasMovimientosModel
                {
                    PolizasFaltantes = new List<PolizaInfo>(),
                    PolizasMovimientosDiferentes = new List<PolizaInfo>()
                };
                StringBuilder sb;
                StringBuilder fecha;
                for (var indexFolios = 0; indexFolios < polizasPasesProcesoModel.Count; indexFolios++)
                {
                    polizaPaseProcesoModel = polizasPasesProcesoModel[indexFolios];
                    prefijoPoliza = ValidaPrefijoEntradaSalida(tipoPoliza, prefijoPoliza, polizaPaseProcesoModel);
                    sb =
                        new StringBuilder(ObtenerLlavePoliza(prefijoPoliza, propiedades, polizaPaseProcesoModel,
                                                             propiedadesValor));
                    fecha = new StringBuilder(ObtenerFechaPoliza(polizaPaseProcesoModel, fechaValor));
                    validarPoliza = ObtenerPolizasPorFiltro(polizasPasesProcesoExistentes, sb.ToString(),
                                                            fecha.ToString());
                    if (validarPoliza == null || !validarPoliza.Any())
                    {
                        validarPoliza =
                            ObtenerPolizasPorFiltro(polizasPasesProcesoNoExistenten, sb.ToString(), fecha.ToString());
                    }
                    if (validarPoliza == null || !validarPoliza.Any())
                    {
                        continue;
                    }
                    polizaBaseDatos = ObtenerPolizasPorFiltro(polizasDePP, sb.ToString(), fecha.ToString());
                    if (polizaBaseDatos == null || !polizaBaseDatos.Any())
                    {
                        validarPoliza.ForEach(falta => falta.Faltante = true);
                        conciliacionPolizasMovimientosModel.PolizasFaltantes.AddRange(validarPoliza);
                    }
                    else
                    {
                        if (ValidarImportes(polizaBaseDatos, validarPoliza, fecha.ToString()))
                        {
                            validarPoliza.ForEach(inco => inco.Inconcistencia = true);
                            conciliacionPolizasMovimientosModel.PolizasMovimientosDiferentes.AddRange(validarPoliza);
                        }
                    }
                }
                Contexto.PolizasMovimientos.Add(conciliacionPolizasMovimientosModel);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        private List<PolizaInfo> ObtenerPolizasPorTipo(TipoPoliza tipoPoliza, List<PolizaInfo> polizasTipo)
        {
            IQueryable<PolizaInfo> query = Contexto.PolizasCompletas.AsQueryable();
            string sociedad = polizasTipo.Select(soc => soc.Sociedad).FirstOrDefault();
            switch (tipoPoliza)
            {
                case TipoPoliza.PolizaContratoParcialidades:
                    tipoPoliza = (TipoPoliza) polizasTipo.Select(tipo => tipo.TipoPolizaID).FirstOrDefault();
                    break;
            }
            query = query.Where(tipo => tipo.TipoPolizaID == tipoPoliza.GetHashCode());
            query = query.Where(tipo => tipo.Sociedad.Equals(sociedad));
            return query.ToList();
        }

        private List<PolizaInfo> ObtenerPolizasPorFiltro(List<PolizaInfo> polizas, string concepto, string fecha)
        {
            IQueryable<PolizaInfo> query = polizas.AsQueryable();
            string sociedad = polizas.Select(soc => soc.Sociedad).FirstOrDefault();
            query = query.Where(folio => folio.Concepto.Split(' ')[0].Equals(concepto,
                                                                             StringComparison.
                                                                                 InvariantCultureIgnoreCase)
                                         || folio.Concepto.Split('-')[0].Equals(concepto,
                                                                                StringComparison.
                                                                                    InvariantCultureIgnoreCase)
                                         || folio.Concepto.Split(',')[0].Equals(concepto,
                                                                                StringComparison.
                                                                                    InvariantCultureIgnoreCase));
            query = query.Where(soc => soc.Sociedad.Equals(sociedad));
            query = query.Where(fech => fech.FechaDocumento.Equals(fecha));
            return query.ToList();
        }

        private bool ValidarImportes(List<PolizaInfo> polizaBaseDatos, List<PolizaInfo> polizaGanerada, string fecha)
        {
            decimal cargoBaseDatos = Math.Round(polizaBaseDatos.Where(x => !x.Importe.StartsWith("-") && x.FechaDocumento.Equals(fecha)).Sum(x => Convert.ToDecimal(x.Importe)), 2);
            decimal abonoBaseDatos = Math.Round(polizaBaseDatos.Where(x => x.Importe.StartsWith("-") && x.FechaDocumento.Equals(fecha)).Sum(x => Convert.ToDecimal(x.Importe)), 2);

            decimal cargoGenerado = Math.Round(polizaGanerada.Where(x => !x.Importe.StartsWith("-") && x.FechaDocumento.Equals(fecha)).Sum(x => Convert.ToDecimal(x.Importe)), 2);
            decimal abonoGenerado = Math.Round(polizaGanerada.Where(x => x.Importe.StartsWith("-") && x.FechaDocumento.Equals(fecha)).Sum(x => Convert.ToDecimal(x.Importe)), 2);

            decimal diferenciaCargo = Math.Abs(cargoBaseDatos - cargoGenerado);
            decimal diferenciaABono = Math.Abs(abonoBaseDatos - abonoGenerado);

            return (diferenciaCargo > 1 || diferenciaABono > 1);
        }

        private string ValidaPrefijoEntradaSalida(TipoPoliza tipoPoliza, string prefijo, dynamic poliza)
        {
            switch (tipoPoliza)
            {
                case TipoPoliza.GastosMateriaPrima:
                    prefijo = poliza.EsEntrada ? "EA" : "SA";
                    break;
                case TipoPoliza.PolizaContratoParcialidades:
                    var tipoContrato = (TipoContratoEnum)poliza.Contrato.TipoContrato.TipoContratoId;
                    var tipoPolizaEnum = TipoPoliza.PolizaContratoTerceros;
                    switch (tipoContrato)
                    {
                        case TipoContratoEnum.EnTransito:
                            tipoPolizaEnum = TipoPoliza.PolizaContratoTransito;
                            break;
                    }
                    TipoPolizaInfo tipoPolizaParcial =
                        tiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == tipoPolizaEnum.GetHashCode());
                    if (tipoPolizaParcial != null)
                    {
                        prefijo = tipoPolizaParcial.ClavePoliza;
                    }
                    break;
                case TipoPoliza.PolizaSacrificio:
                    prefijo = string.Empty;
                    break;
            }
            return prefijo;
        }

        private dynamic ObtenerColeccionConciliacionMovimientos(TipoPoliza tipoPoliza
                                                              , Dictionary<string, string> propiedadesValor
                                                              , Dictionary<string, string> fechaValor
                                                              , out string prefijoPoliza
                                                              , out List<PropertyInfo> propiedades)
        {
            dynamic polizasPasesProcesoModel = null;
            prefijoPoliza =
                tiposPoliza.Where(tipo => tipo.TipoPolizaID == tipoPoliza.GetHashCode()).Select(tipo => tipo.ClavePoliza)
                    .FirstOrDefault();
            propiedades = new List<PropertyInfo>();
            switch (tipoPoliza)
            {
                case TipoPoliza.PaseProceso:
                    polizasPasesProcesoModel = Contexto.PasesProceso;
                    propiedades = (from prp in Contexto.PasesProceso
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("Pedido") || p.Name.Equals("PesajeMateriaPrima")
                                   select p).Distinct().OrderBy(nombre => nombre.Name).ToList();
                    propiedadesValor.Add("Pedido", "FolioPedido");
                    propiedadesValor.Add("PesajeMateriaPrima", "Ticket");
                    fechaValor.Add("Pedido", "FechaPedido");
                    break;
                case TipoPoliza.EntradaCompra:
                    polizasPasesProcesoModel = Contexto.ContenedorEntradasMateriasPrima;
                    propiedades = (from prp in Contexto.ContenedorEntradasMateriasPrima
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("Contrato")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("Contrato", "Folio");
                    fechaValor.Add("EntradaProducto", "Fecha");
                    break;
                case TipoPoliza.ConsumoAlimento:
                    polizasPasesProcesoModel = Contexto.ConsumosAlimento;
                    propiedades = new List<PropertyInfo>();
                    fechaValor.Add("Reparto", "Fecha");
                    break;
                case TipoPoliza.ConsumoProducto:
                    polizasPasesProcesoModel = Contexto.ConsumosProducto;
                    propiedades = new List<PropertyInfo>();
                    fechaValor.Add("AlmacenMovimiento", "FechaMovimiento");
                    break;
                case TipoPoliza.PolizaContratoOtrosCostos:
                    List<PolizaContratoModel> polizasContratoCostos = ObtenerPolizasContratoCostos();
                    polizasPasesProcesoModel = polizasContratoCostos;
                    propiedades = (from prp in polizasContratoCostos
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("AlmacenMovimiento")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("AlmacenMovimiento", "FolioMovimiento");
                    fechaValor.Add("Contrato", "Fecha");
                    break;
                case TipoPoliza.EntradaCompraMateriaPrima:
                    polizasPasesProcesoModel = Contexto.RecepcionProductos;
                    propiedades = (from prp in Contexto.RecepcionProductos
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("FolioRecepcion")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("FolioRecepcion", "FolioRecepcion");
                    fechaValor.Add("FechaRecepcion", "FechaRecepcion");
                    break;
                case TipoPoliza.PolizaPremezcla:
                    polizasPasesProcesoModel = Contexto.DistribucionIngredientes;
                    propiedades = (from prp in Contexto.DistribucionIngredientes
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("Producto")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("Producto", "ProductoId");
                    fechaValor.Add("FechaEntrada", "FechaEntrada");
                    break;
                case TipoPoliza.SalidaTraspaso:
                    polizasPasesProcesoModel = Contexto.SolicitudProductos;
                    propiedades = (from prp in Contexto.SolicitudProductos
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("FolioSolicitud")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("FolioSolicitud", "FolioSolicitud");
                    fechaValor.Add("FechaEntrega", "FechaEntrega");
                    break;
                case TipoPoliza.EntradaAjuste:
                case TipoPoliza.SalidaAjuste:
                    polizasPasesProcesoModel = tipoPoliza == TipoPoliza.EntradaAjuste
                                                   ? Contexto.EntradasPorAjuste
                                                   : Contexto.SalidasPorAjuste;
                    List<PolizaEntradaSalidaPorAjusteModel> polizaSalidaEntrada = tipoPoliza == TipoPoliza.EntradaAjuste
                                                                                      ? Contexto.EntradasPorAjuste
                                                                                      : Contexto.SalidasPorAjuste;
                    propiedades = (from prp in polizaSalidaEntrada
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("FolioMovimiento")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("FolioMovimiento", "FolioMovimiento");
                    fechaValor.Add("FechaMovimiento", "FechaMovimiento");
                    break;
                case TipoPoliza.PolizaSubProducto:
                    polizasPasesProcesoModel = Contexto.SubProductos;
                    propiedades = (from prp in Contexto.SubProductos
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("EntradaProducto")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("EntradaProducto", "Folio");
                    fechaValor.Add("EntradaProducto", "Fecha");
                    break;
                case TipoPoliza.ProduccionAlimento:
                    polizasPasesProcesoModel = Contexto.ProduccionesFormula;
                    propiedades = (from prp in Contexto.ProduccionesFormula
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("FolioFormula")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("FolioFormula", "FolioFormula");
                    fechaValor.Add("FechaProduccion", "FechaProduccion");
                    break;
                case TipoPoliza.SalidaConsumo:
                    polizasPasesProcesoModel = Contexto.SolicitudProductos;
                    propiedades = (from prp in Contexto.SolicitudProductos
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("FolioSolicitud")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("FolioSolicitud", "FolioSolicitud");
                    fechaValor.Add("FechaEntrega", "FechaEntrega");
                    break;
                case TipoPoliza.SalidaVentaProducto:
                    polizasPasesProcesoModel = Contexto.SalidasVentaProductos;
                    propiedades = (from prp in Contexto.SalidasVentaProductos
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("FolioSalida")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("FolioSalida", "FolioSalida");
                    fechaValor.Add("FechaSalida", "FechaSalida");
                    break;
                case TipoPoliza.GastosMateriaPrima:
                    polizasPasesProcesoModel = Contexto.GastosMateriaPrima;
                    propiedades = (from prp in Contexto.GastosMateriaPrima
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("FolioMovimiento")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("FolioMovimiento", "FolioMovimiento");
                    fechaValor.Add("Fecha", "Fecha");
                    break;
                case TipoPoliza.PolizaContratoTerceros:
                    polizasPasesProcesoModel =
                        Contexto.PolizasContrato.Where(
                            tipo =>
                            tipo.Contrato.Parcial == CompraParcialEnum.Total &&
                            tipo.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaTercero.GetHashCode()).
                            ToList();
                    propiedades =
                        (from prp in
                             Contexto.PolizasContrato.Where(
                                 tipo =>
                                 tipo.Contrato.Parcial == CompraParcialEnum.Total &&
                                 tipo.Contrato.TipoContrato.TipoContratoId ==
                                 TipoContratoEnum.BodegaTercero.GetHashCode()).ToList()
                         let props = prp.GetType().GetProperties()
                         from p in props
                         where p.Name.Equals("Contrato")
                         select p).Distinct().ToList();
                    propiedadesValor.Add("Contrato", "Folio");
                    fechaValor.Add("Contrato", "Fecha");
                    break;
                case TipoPoliza.PolizaContratoTransito:
                    polizasPasesProcesoModel = Contexto.PolizasContrato.Where(
                        tipo => tipo.Contrato.Parcial == CompraParcialEnum.Total
                                &&
                                tipo.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.EnTransito.GetHashCode())
                        .ToList();
                    propiedades =
                        (from prp in
                             Contexto.PolizasContrato.Where(
                                 tipo =>
                                 tipo.Contrato.Parcial == CompraParcialEnum.Total &&
                                 tipo.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.EnTransito.GetHashCode())
                             .ToList()
                         let props = prp.GetType().GetProperties()
                         from p in props
                         where p.Name.Equals("Contrato")
                         select p).Distinct().ToList();
                    propiedadesValor.Add("Contrato", "Folio");
                    fechaValor.Add("Contrato", "Fecha");
                    break;
                case TipoPoliza.PolizaContratoParcialidades:
                    var parcialidades = new List<ContratoParcialInfo>();
                    List<ContratoInfo> contratos =
                        Contexto.PolizasContrato.Where(tipo => tipo.Contrato.Parcial == CompraParcialEnum.Parcial).
                            Select(cont => cont.Contrato).ToList();
                    for (var indexContrato = 0; indexContrato < contratos.Count; indexContrato++)
                    {
                        parcialidades.AddRange(contratos[indexContrato].ListaContratoParcial);
                    }
                    polizasPasesProcesoModel = parcialidades;
                    propiedades =
                        (from prp in parcialidades
                         let props = prp.GetType().GetProperties()
                         from p in props
                         where p.Name.Equals("Contrato")
                         select p).Distinct().ToList();
                    propiedadesValor.Add("Contrato", "Folio");
                    fechaValor.Add("Contrato", "Fecha");
                    break;
                case TipoPoliza.SalidaVenta:
                    polizasPasesProcesoModel = Contexto.VentasGanado;
                    propiedades = (from prp in Contexto.VentasGanado
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("VentaGanado")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("VentaGanado", "FolioTicket");
                    fechaValor.Add("VentaGanado", "FechaVenta");
                    break;
                case TipoPoliza.SalidaMuerte:
                    polizasPasesProcesoModel = Contexto.AnimalesCosto;
                    propiedades = (from prp in Contexto.AnimalesCosto
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("Arete")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("Arete", "Arete");
                    fechaValor.Add("FechaCosto", "FechaCosto");
                    break;
                case TipoPoliza.PolizaSacrificio:
                    polizasPasesProcesoModel = Contexto.LotesSacrificio;
                    propiedades = (from prp in Contexto.LotesSacrificio
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("Folio") || p.Name.Equals("Serie")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("Serie", "Serie");
                    propiedadesValor.Add("Folio", "Folio");
                    fechaValor.Add("Fecha", "Fecha");
                    break;
                case TipoPoliza.EntradaGanado:
                    polizasPasesProcesoModel = Contexto.EntradasGanado;
                    propiedades = (from prp in Contexto.EntradasGanado
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("EntradaGanado")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("EntradaGanado", "FolioEntrada");
                    fechaValor.Add("EntradaGanado", "FechaEntrada");
                    break;
                case TipoPoliza.GastosInventario:
                    polizasPasesProcesoModel = Contexto.GastosInventario;
                    propiedades = (from prp in Contexto.GastosInventario
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("FolioGasto")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("FolioGasto", "FolioGasto");
                    fechaValor.Add("FechaGasto", "FechaGasto");
                    break;
                case TipoPoliza.SalidaGanado:
                case TipoPoliza.PolizaSacrificioTraspasoGanado:
                    polizasPasesProcesoModel = Contexto.InterfaceSalidasTraspasos;
                    propiedades = (from prp in Contexto.InterfaceSalidasTraspasos
                                   let props = prp.GetType().GetProperties()
                                   from p in props
                                   where p.Name.Equals("FolioTraspaso")
                                   select p).Distinct().ToList();
                    propiedadesValor.Add("FolioTraspaso", "FolioTraspaso");
                    fechaValor.Add("FechaEnvio", "FechaEnvio");
                    break;
            }
            return polizasPasesProcesoModel;
        }

        private List<PolizaContratoModel> ObtenerPolizasContratoCostos()
        {
            List<PolizaContratoModel> polizasCostos =
                Contexto.PolizasContrato.Where(costo => costo.OtrosCostos.Count > 0).ToList();
            var polizasContratoCostos = new List<PolizaContratoModel>();
            PolizaContratoModel polizaContratoModel;
            for (var indexCostos = 0; indexCostos < polizasCostos.Count; indexCostos++)
            {
                for (var indexOtrosCostos = 0;
                     indexOtrosCostos < polizasCostos[indexCostos].OtrosCostos.Count;
                     indexOtrosCostos++)
                {
                    polizasCostos[indexCostos].Contrato.Fecha =
                        polizasCostos[indexCostos].OtrosCostos[indexOtrosCostos].FechaCosto;
                    polizaContratoModel = new PolizaContratoModel
                                              {
                                                  AlmacenMovimiento =
                                                      polizasCostos[indexCostos].AlmacenMovimiento,
                                                  OtrosCostos = new List<CostoInfo>
                                                                    {
                                                                        polizasCostos[indexCostos].OtrosCostos[
                                                                            indexOtrosCostos]
                                                                    },
                                                  Contrato = polizasCostos[indexCostos].Contrato,
                                              };
                    polizasContratoCostos.Add(polizaContratoModel);
                }
            }
            return polizasContratoCostos;
        }

        private string ObtenerLlavePoliza(string prefijoPoliza, List<PropertyInfo> propiedades
                                        , dynamic polizaPaseProcesoModel, Dictionary<string, string> fechaValor)
        {
            var sb = new StringBuilder(prefijoPoliza).Append("-");
            PropertyInfo propiedadDatos;
            for (var indexPropiedades = 0; indexPropiedades < propiedades.Count; indexPropiedades++)
            {
                try
                {
                    propiedadDatos = polizaPaseProcesoModel.GetType().GetProperty(propiedades[indexPropiedades].Name);
                    if (propiedadDatos != null)
                    {
                        object valorPropiedad = propiedadDatos.GetValue(polizaPaseProcesoModel, null);
                        if (valorPropiedad != null)
                        {
                            propiedadDatos =
                                valorPropiedad.GetType().GetProperty(
                                    fechaValor[propiedades[indexPropiedades].Name]);
                            if (propiedadDatos == null)
                            {
                                sb.Append(valorPropiedad).Append("-");
                            }
                            else
                            {
                                valorPropiedad = propiedadDatos.GetValue(valorPropiedad, null);
                                if (valorPropiedad != null)
                                {
                                    sb.Append(valorPropiedad).Append("-");
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            return sb.ToString().TrimStart('-').TrimEnd('-');
        }

        private string ObtenerFechaPoliza(dynamic polizaPaseProcesoModel,
                                          Dictionary<string, string> propiedadesValor)
        {
            PropertyInfo propiedadDatos;
            var sb = new StringBuilder();
            foreach (var llave in propiedadesValor)
            {
                try
                {
                    propiedadDatos = polizaPaseProcesoModel.GetType().GetProperty(llave.Key);
                    if (propiedadDatos != null)
                    {
                        object valorPropiedad = propiedadDatos.GetValue(polizaPaseProcesoModel, null);
                        if (valorPropiedad != null)
                        {
                            propiedadDatos =
                                valorPropiedad.GetType().GetProperty(llave.Value);
                            if (propiedadDatos == null)
                            {
                                sb = new StringBuilder();
                                sb.Append(string.Format("{0:yyyyMMdd}", valorPropiedad));
                                break;
                            }
                            else
                            {
                                valorPropiedad = propiedadDatos.GetValue(valorPropiedad, null);
                                if (valorPropiedad != null)
                                {
                                    sb = new StringBuilder();
                                    sb.Append(string.Format("{0:yyyyMMdd}", valorPropiedad));
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            return sb.ToString();
        }

        #endregion GENERAR CONCILIACION

        #region GUARDAR

        /// <summary>
        /// Metodo para Guardar
        /// </summary>
        private void Guardar(List<PolizaInfo> polizasReenviar)
        {
            try
            {
                var polizaPL = new PolizaPL();
                ObservableCollection<ConciliacionPolizasMovimientosModel> polizasConciliacion = Contexto.PolizasMovimientos;
                var polizasMovimientos = new List<PolizaInfo>();
                polizasConciliacion.ToList().ForEach(datos => polizasMovimientos.AddRange(datos.PolizasMovimientosDiferentes));
                polizasConciliacion.ToList().ForEach(datos => polizasMovimientos.AddRange(datos.PolizasFaltantes));
                polizasReenviar =
                    polizasMovimientos.Join(polizasReenviar, comp => comp.Referencia3, ree => ree.Referencia3,
                                            (comp, ree) => comp).Distinct().ToList();
                List<PolizaInfo> polizasCancelar = (from comp in Contexto.PolizasCompletas
                                                    from ree in polizasReenviar
                                                    where comp.Concepto.Equals(ree.Concepto)
                                                        && comp.Cuenta.Equals(ree.Cuenta)
                                                        && comp.TipoPolizaID == ree.TipoPolizaID
                                                    select comp).ToList();
                polizasReenviar.ForEach(datos =>
                {
                    datos.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    datos.OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                });
                polizaPL.GuardarConciliacion(polizasReenviar, polizasCancelar);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                  MessageImage.Correct);
                InicializaContexto();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ErrorGuardar_ConciliacionSAP, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        #endregion GUARDAR

        #endregion METODOS
    }
}
 