using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para DiferenciasDeInventario.xaml
    /// </summary>
    public partial class DiferenciasDeInventario
    {
        #region Propiedades
        private readonly int organizacionID;
        private readonly int usuarioID;
        private readonly List<DiferenciasDeInventariosInfo> listaDiferenciasInventarios =
            new List<DiferenciasDeInventariosInfo>();
        private bool edicionAjuste;
        private DiferenciasDeInventariosInfo diferenciasDeInventarioInfo = new DiferenciasDeInventariosInfo();
        private List<DiferenciasDeInventariosInfo> listaAjustesPendientesPrincipal =
            new List<DiferenciasDeInventariosInfo>();
        private List<AlmacenInfo> listaAlmacenFiltrada = new List<AlmacenInfo>();
        private readonly List<AlmacenInfo> listaAlmacenFiltradaGlobal = new List<AlmacenInfo>();
        private bool esCambioCodigo = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public DiferenciasDeInventario()
        {
            organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            InitializeComponent();
            CargarCombos();
            DeshabilitarControlesInicio();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento loaded de formulario principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiferenciasDeInventario_OnLoaded(object sender, RoutedEventArgs e)
        {
            //CargarCombos();
            //DeshabilitarControlesInicio();
            //Verificar pendientes
            CargarAjustesPendientes();
            //
            CboAjuste.Focus();
        }

        /// <summary>
        /// Activa el combo de almacen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAjuste_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!esCambioCodigo)
                {
                    if (!edicionAjuste)
                    {
                        if (CboAjuste.SelectedItem == null || CboAjuste.SelectedIndex == 0)
                        {
                            LimpiarControlesDatosAjuste(false);
                        }
                        else
                        {
                            CboAlmacen.IsEnabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Activar el combo producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAlmacen_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!edicionAjuste)
                {
                    if (CboAlmacen.SelectedItem == null || CboAlmacen.SelectedIndex == 0)
                    {
                        //Verificar funcionalidad
                        var listaProductos = new List<ProductoInfo>();
                        var productoInfo = new ProductoInfo
                        {
                            ProductoId = 0,
                            ProductoDescripcion = Properties.Resources.DiferenciasDeInventario_CboSeleccione
                        };
                        listaProductos.Insert(0, productoInfo);
                        CboProducto.ItemsSource = null;
                        CboProducto.ItemsSource = listaProductos;
                        CboProducto.SelectedIndex = 0;
                        CboProducto.IsEnabled = false;
                        CboLote.IsEnabled = false;
                        TxtJustificacion.Clear();
                        TxtKilogramosAjuste.IsEnabled = false;
                        TxtJustificacion.IsEnabled = false;
                    }
                    else
                    {
                        CboProducto.ItemsSource = null;
                        var almacenInventarioPl = new AlmacenInventarioPL();
                        var listaProductos =
                            almacenInventarioPl.ObtienePorAlmacenIdLlenaProductoInfo(
                                (AlmacenInfo)CboAlmacen.SelectedItem);
                        if (listaProductos != null)
                        {
                            //Filtrar lista productos
                            var listaProductosFiltrada = (from almacenInventarioInfo in listaProductos
                                                          where
                                                          almacenInventarioInfo.Producto != null &&
                                                              (almacenInventarioInfo.Producto.Familia.FamiliaID ==
                                                              FamiliasEnum.MateriaPrimas.GetHashCode() ||
                                                              almacenInventarioInfo.Producto.Familia.FamiliaID ==
                                                              FamiliasEnum.Premezclas.GetHashCode())
                                                          select almacenInventarioInfo.Producto).ToList();
                            //Agregamos lista filtrada al cbo
                            var productoInfo = new ProductoInfo
                            {
                                ProductoDescripcion = Properties.Resources.DiferenciasDeInventario_CboSeleccione,
                                ProductoId = 0
                            };
                            listaProductosFiltrada.Insert(0, productoInfo);
                            CboProducto.ItemsSource = listaProductosFiltrada;
                            CboProducto.SelectedIndex = 0;
                            CboProducto.IsEnabled = true;
                        }
                        else
                        {
                            var listaProductosDefault = new List<ProductoInfo>();
                            var productoInfoDefault = new ProductoInfo
                            {
                                ProductoDescripcion = Properties.Resources.DiferenciasDeInventario_CboSeleccione,
                                ProductoId = 0
                            };
                            listaProductosDefault.Insert(0, productoInfoDefault);
                            CboProducto.ItemsSource = listaProductosDefault;
                            CboProducto.SelectedIndex = 0;
                            CboProducto.IsEnabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeErrorObtenerProductos, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Activar el combo lote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboProducto_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!edicionAjuste)
                {
                    if (CboProducto.SelectedItem == null || CboProducto.SelectedIndex == 0)
                    {
                        var listaAlmacenInventarioLoteInfo = new List<AlmacenInventarioLoteInfo>();
                        var almacenInventarioLoteInfo = new AlmacenInventarioLoteInfo
                        {
                            AlmacenInventarioLoteId = 0,
                            DescripcionLote = Properties.Resources.DiferenciasDeInventario_CboSeleccione
                        };
                        listaAlmacenInventarioLoteInfo.Insert(0, almacenInventarioLoteInfo);
                        CboLote.ItemsSource = null;
                        CboLote.ItemsSource = listaAlmacenInventarioLoteInfo;
                        CboLote.SelectedIndex = 0;
                        CboLote.IsEnabled = false;
                        TxtJustificacion.Clear();
                        TxtKilogramosAjuste.IsEnabled = false;
                        TxtJustificacion.IsEnabled = false;
                    }
                    else
                    {
                        var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                        List<AlmacenInventarioLoteInfo> listaLotes;
                        if (CboAjuste.SelectedIndex == TipoAjusteEnum.CerrarLote.GetHashCode())
                        {
                            listaLotes = almacenInventarioLotePl.ObtenerPorAlmacenProductoEnCeros((AlmacenInfo)CboAlmacen.SelectedItem,
                                (ProductoInfo)CboProducto.SelectedItem);
                        }
                        else
                        {
                            listaLotes =
                                almacenInventarioLotePl.ObtenerPorAlmacenProductoConMovimientos(
                                    (AlmacenInfo)CboAlmacen.SelectedItem,
                                    (ProductoInfo)CboProducto.SelectedItem);
                        }

                        if (listaLotes != null)
                        {
                            var almacenInventarioLoteInfo = new AlmacenInventarioLoteInfo
                            {
                                AlmacenInventarioLoteId = 0,
                                DescripcionLote = Properties.Resources.DiferenciasDeInventario_CboSeleccione
                            };
                            foreach (var inventarioLoteInfo in listaLotes)
                            {
                                inventarioLoteInfo.DescripcionLote =
                                    inventarioLoteInfo.Lote.ToString(CultureInfo.InvariantCulture);
                            }
                            listaLotes.Insert(0, almacenInventarioLoteInfo);
                            CboLote.ItemsSource = listaLotes;
                            CboLote.SelectedIndex = 0;
                            CboLote.IsEnabled = true;
                        }
                        else
                        {
                            listaLotes = new List<AlmacenInventarioLoteInfo>();
                            var almacenInventarioLoteInfo = new AlmacenInventarioLoteInfo
                            {
                                AlmacenInventarioLoteId = 0,
                                DescripcionLote = Properties.Resources.DiferenciasDeInventario_CboSeleccione
                            };
                            listaLotes.Insert(0, almacenInventarioLoteInfo);
                            CboLote.ItemsSource = null;
                            CboLote.ItemsSource = listaLotes;
                            CboLote.SelectedIndex = 0;
                            CboLote.IsEnabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeErrorObtenerLotes, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtener kilogramos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboLote_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!edicionAjuste)
                {
                    if (CboLote.SelectedItem == null || CboLote.SelectedIndex == 0)
                    {
                        TxtKilogramosTotales.Text = "";
                        TxtKilogramosAjuste.Text = "";
                        TxtKilogramosActuales.Text = "";
                        TxtPorcentajeAjuste.Text = "";
                        TxtJustificacion.Clear();
                        TxtKilogramosAjuste.IsEnabled = false;
                        TxtJustificacion.IsEnabled = false;
                    }
                    else
                    {

                        TxtKilogramosTotales.Text = "";
                        TxtKilogramosAjuste.Text = "";
                        TxtKilogramosActuales.Text = "";
                        TxtPorcentajeAjuste.Text = "";
                        TxtKilosDiferencia.Text = string.Empty;
                        //TxtKilosDiferencia.ClearValue(IntegerUpDown.ValueProperty);
                        //Obtener tamanio del lote y mostrar en txt
                        var almacenInventarioLoteInfo = (AlmacenInventarioLoteInfo)CboLote.SelectedItem;
                        var almacenMovimientoDetalle = new AlmacenMovimientoDetalle
                        {
                            AlmacenInventarioLoteId = almacenInventarioLoteInfo.AlmacenInventarioLoteId
                        };
                        if (almacenInventarioLoteInfo != null)
                        {
                            TxtKilogramosActuales.Value = almacenInventarioLoteInfo.Cantidad;
                        }
                        var listaMovimientos = new List<TipoMovimientoInfo>
                        {
                            new TipoMovimientoInfo
                                {
                                    TipoMovimientoID = TipoMovimiento.EntradaPorCompra.GetHashCode()
                                },
                                new TipoMovimientoInfo
                                    {
                                        TipoMovimientoID = TipoMovimiento.EntradaPorAjuste.GetHashCode()
                                    },
                            new TipoMovimientoInfo
                                {
                                    TipoMovimientoID = TipoMovimiento.EntradaBodegaTerceros.GetHashCode()
                                },
                            new TipoMovimientoInfo
                                {
                                    TipoMovimientoID = TipoMovimiento.EntradaAlmacen.GetHashCode()
                                },
                                new TipoMovimientoInfo
                                    {
                                        TipoMovimientoID = TipoMovimiento.RecepcionAProceso.GetHashCode()
                                    }

                        };

                        var almacenMovimientoDetallePl = new AlmacenMovimientoDetallePL();
                        var listaAlmacenMovimientoDetalle =
                            almacenMovimientoDetallePl.ObtenerAlmacenMovimientoDetallePorLoteId(
                                almacenMovimientoDetalle,
                                listaMovimientos);

                        var almacenInfo = (AlmacenInfo)CboAlmacen.SelectedItem;
                        if (almacenInfo.TipoAlmacenID == TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode())
                        {
                            var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                            var almacenInventarioLoteInfoP =
                                almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorId(
                                    almacenInventarioLoteInfo.AlmacenInventarioLoteId);
                            if (almacenInventarioLoteInfoP != null)
                            {
                                var kilogramosTotales = listaAlmacenMovimientoDetalle.Sum(alm => alm.Cantidad);
                                //var kilogramosTotales = almacenInventarioLoteInfoP.Cantidad;
                                TxtKilogramosTotales.Text = kilogramosTotales.ToString(CultureInfo.InvariantCulture);
                                TxtJustificacion.IsEnabled = true;
                                TxtKilogramosAjuste.IsEnabled = true;
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.DiferenciasDeInventario_MensajeKilogramosTotalesNoEncontrados,
                                    MessageBoxButton.OK, MessageImage.Warning);
                                CboLote.Focus();
                            }
                        }
                        else
                        {
                            if (CboAjuste.SelectedIndex != TipoAjusteEnum.CerrarLote.GetHashCode())
                            {
                                //if (almacenInfo.TipoAlmacenID == TipoAlmacenEnum.MateriasPrimas.GetHashCode())
                                //{
                                //var almacenMovimientoDetallePl = new AlmacenMovimientoDetallePL();
                                //var 
                                    listaAlmacenMovimientoDetalle =
                                    almacenMovimientoDetallePl.ObtenerAlmacenMovimientoDetallePorLoteId(
                                        almacenMovimientoDetalle,
                                        listaMovimientos);
                                if (listaAlmacenMovimientoDetalle != null)
                                {
                                    var kilogramosTotales = listaAlmacenMovimientoDetalle.Sum(alm => alm.Cantidad);
                                    //var kilogramosTotales = (from almacenMovimientoDetalleInfo in listaAlmacenMovimientoDetalle
                                    //                         select almacenMovimientoDetalleInfo.Cantidad).Sum();
                                    TxtKilogramosTotales.Text =
                                        kilogramosTotales.ToString(CultureInfo.InvariantCulture);
                                    TxtJustificacion.IsEnabled = true;
                                    TxtKilogramosAjuste.IsEnabled = true;
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                      Properties.Resources.
                                                          DiferenciasDeInventario_MensajeKilogramosTotalesNoEncontrados,
                                                      MessageBoxButton.OK, MessageImage.Warning);
                                    CboLote.Focus();
                                }
                                //}
                            }
                        }

                        if (CboAjuste.SelectedIndex == TipoAjusteEnum.CerrarLote.GetHashCode())
                        {
                            if (almacenInventarioLoteInfo.Cantidad > 0)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                   Properties.Resources.DiferenciasDeInventario_MensajeLoteConKilos,
                                   MessageBoxButton.OK, MessageImage.Warning);
                                CboAjuste.SelectedIndex = 0;
                                return;
                            }
                            var pedidosPL = new PedidosPL();
                            List<PedidoPendienteLoteModel> listaPedidos =
                                pedidosPL.ObtenerPedidosPendientesPorLote(
                                    almacenInventarioLoteInfo.AlmacenInventarioLoteId);

                            if (listaPedidos != null && listaPedidos.Any())
                            {
                                var pesajesPendientes =
                                    listaPedidos.Where(pedi => pedi.EstatusID != Estatus.PedidoCompletado.GetHashCode())
                                        .ToList();

                                if (pesajesPendientes.Any())
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.DiferenciasDeInventario_MensajeLoteConPedidos,
                                  MessageBoxButton.OK, MessageImage.Warning);
                                    CboAjuste.SelectedIndex = 0;
                                    return;
                                }
                            }

                            TxtKilogramosTotales.Value = 0;
                            TxtKilogramosAjuste.Value = 0;
                            TxtKilogramosAjuste.IsEnabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento keydown para la forma en general
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiferenciasDeInventario_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        ///// <summary>
        ///// Enter o tab en TxtKilogramosAjuste
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void TxtKilogramosAjuste_OnKeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {

        //        if (e.Key == Key.Enter || e.Key == Key.Tab)
        //        {
        //            CalcularAjuste();
        //        }
        //    }
        //    catch (Exception exg)
        //    {
        //        Logger.Error(exg);
        //        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
        //            Properties.Resources.DiferenciasDeInventario_MensajeErrorObtenerKilogramos, MessageBoxButton.OK,
        //            MessageImage.Error);
        //        TxtKilogramosAjuste.Focus();
        //    }
        //}

        /// <summary>
        /// Key up de retroceso y suprimir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtKilogramosAjuste_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                TxtKilosDiferencia.Text = "";
                TxtPorcentajeAjuste.Text = "";
            }
        }

        //Txt no permite caracteres especiales
        private void TxtJustificacion_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConPunto(e.Text);
        }

        /// <summary>
        /// Agrega un ajuste al grid principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bool actualizar = (string)BtnAgregar.Content == Properties.Resources.DiferenciasDeInventario_LblActualizar;
                //Validar pesos antes de guardar
                var resultadoValidacion = ValidarAgregar(actualizar);
                if (resultadoValidacion.Resultado)
                {
                    if ((string)BtnAgregar.Content == Properties.Resources.DiferenciasDeInventario_LblActualizar)
                    {

                        foreach (var diferenciasDeInventariosInfoPar in listaDiferenciasInventarios.Where(diferenciasDeInventariosInfoPar => diferenciasDeInventariosInfoPar.AlmacenMovimiento.AlmacenMovimientoID ==
                                                                                                                                             diferenciasDeInventarioInfo.AlmacenMovimiento.AlmacenMovimientoID))
                        {
                            diferenciasDeInventariosInfoPar.KilogramosFisicos =
                                Convert.ToDecimal(TxtKilogramosAjuste.Value);
                            diferenciasDeInventariosInfoPar.KilogramosTeoricos =
                                Convert.ToDecimal(TxtKilogramosActuales.Value);
                            diferenciasDeInventariosInfoPar.PorcentajeAjuste =
                                Convert.ToDecimal(TxtPorcentajeAjuste.Value);
                            diferenciasDeInventariosInfoPar.AlmacenMovimiento.Observaciones =
                                TxtJustificacion.Text.Trim();
                        }
                        GridDiferenciasDeInventarios.ItemsSource = null;
                        GridDiferenciasDeInventarios.ItemsSource = listaDiferenciasInventarios;
                        BtnAgregar.Content = Properties.Resources.DiferenciasDeInventario_BtnAgregar;
                        edicionAjuste = false;
                        RecargarComboAlmacen();
                        LimpiarControlesDatosAjuste(true);
                    }
                    else
                    {
                        AgregarAjuste();
                    }
                }
                else
                {
                    string mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                        ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco
                        : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeErrorAgregarAjuste, MessageBoxButton.OK,
                    MessageImage.Error);
                BtnGuardar.Focus();
            }
        }

        /// <summary>
        /// Limpiar controles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            RecargarComboAlmacen();
            LimpiarControlesDatosAjuste(true);
        }

        /// <summary>
        /// Evento 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtKilogramosAjuste_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                CalcularAjuste();
                //if (TxtKilogramosAjuste.Text.Trim() != String.Empty &&
                //    (Convert.ToDecimal(TxtKilogramosAjuste.Text.Replace(",","").Trim()) > 0))
                //{
                //    decimal kilogramosActuales;
                //    decimal kilogramosTotales = Convert.ToDecimal(TxtKilogramosTotales.Text.Replace(",","").Trim());
                //    decimal kilogramosAjuste = Convert.ToDecimal(TxtKilogramosAjuste.Text.Replace(",","").Trim());

                //    kilogramosActuales = kilogramosTotales - kilogramosAjuste;
                //    TxtKilogramosActuales.Text = kilogramosActuales.ToString(CultureInfo.InvariantCulture);

                //    var porcentajeAjuste = decimal.Round(((kilogramosActuales / kilogramosTotales) * 100), 2);
                //    TxtPorcentajeAjuste.Value = porcentajeAjuste;
                //}
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeErrorObtenerKilogramos, MessageBoxButton.OK,
                    MessageImage.Error);
                CboLote.Focus();
            }
        }

        /// <summary>
        /// Editar costo info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                diferenciasDeInventarioInfo =
                    (DiferenciasDeInventariosInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                BtnAgregar.Content = Properties.Resources.OtrosCostos_MensajeCosto;

                if (diferenciasDeInventarioInfo == null) return;
                edicionAjuste = true;

                AsignarAjusteEdicion();
                AsignarAlmacenEdicion();
                AsignarLoteEdicion();
                AsignarProductoEdicion();

                //TxtKilogramosTotales.Text = String.Format("{0:0}", Convert.ToDecimal(diferenciasDeInventarioInfo.KilogramosTotales));
                TxtKilogramosTotales.Value = diferenciasDeInventarioInfo.KilogramosTotales;

                //TxtKilogramosAjuste.Text = String.Format("{0:0}", Convert.ToDecimal(diferenciasDeInventarioInfo.KilogramosFisicos));
                TxtKilogramosAjuste.Value = diferenciasDeInventarioInfo.KilogramosFisicos;

                //TxtKilogramosActuales.Text = String.Format("{0:0}", Convert.ToDecimal(diferenciasDeInventarioInfo.KilogramosTeoricos));
                TxtKilogramosActuales.Value = diferenciasDeInventarioInfo.KilogramosTeoricos;

                TxtPorcentajeAjuste.Value = diferenciasDeInventarioInfo.PorcentajeAjuste;

                TxtJustificacion.Text = diferenciasDeInventarioInfo.AlmacenMovimiento.Observaciones;

                BtnAgregar.Content = Properties.Resources.DiferenciasDeInventario_LblActualizar;
                TxtKilogramosAjuste.IsEnabled = true;
                TxtJustificacion.IsEnabled = true;
                TxtKilogramosAjuste.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeErrorObtenerDatosEditar, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Guarda las diferencias de inventario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            var listaFiltradaConfigurados = new List<DiferenciasDeInventariosInfo>();
            var listaFiltradaNoConfigurados = new List<DiferenciasDeInventariosInfo>();
            var listaFiltradaAutorizacion = new List<DiferenciasDeInventariosInfo>();
            //var punto = ".";
            try
            {
                //var almacen = (AlmacenInfo)CboAlmacen.SelectedItem;
                //var mermaSuperavitPl = new MermaSuperavitPL();
                //var listadoMermaSuperavit = mermaSuperavitPl.ObtenerPorAlmacenID(almacen.AlmacenID);
                //if (listadoMermaSuperavit != null)
                //{
                ValidarPorcentajesPermitidos();

                var diferenciasDeInventarioPl = new DiferenciasDeInventarioPL();

                //Validar tienen configuracion en false
                listaFiltradaConfigurados.AddRange(
                    listaDiferenciasInventarios.Where(
                        diferenciasDeInventarios => diferenciasDeInventarios.TieneConfiguracion));
                listaFiltradaNoConfigurados.AddRange(
                    listaDiferenciasInventarios.Where(
                        diferenciasDeInventarios => !diferenciasDeInventarios.TieneConfiguracion));

                if (listaFiltradaNoConfigurados.Count > 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.DiferenciasDeInventario_MensajeProductoNoCuentaConfiguracion,
                        MessageBoxButton.OK, MessageImage.Warning);
                    var diferenciasDeInventarioProductosSinConfiguracion =
                        new DiferenciasDeInventarioProductosSinConfiguracion(listaFiltradaNoConfigurados);
                    MostrarCentrado(diferenciasDeInventarioProductosSinConfiguracion);
                }

                listaFiltradaAutorizacion.AddRange(
                    listaDiferenciasInventarios.Where(
                        diferenciasDeInventariosInfoP => diferenciasDeInventariosInfoP.RequiereAutorizacion));

                if (listaFiltradaAutorizacion.Count > 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.DiferenciasDeInventario_MensajeRequiereAutorizacion,
                        MessageBoxButton.OK, MessageImage.Warning);
                    var diferenciasDeInventariosProductosRequierenAutorizacion =
                        new DiferenciasDeInventarioProductosRequierenAutorizacion(listaFiltradaAutorizacion);
                    MostrarCentrado(diferenciasDeInventariosProductosRequierenAutorizacion);
                }

                //Se guardan los que tengan configuracion
                if (listaFiltradaConfigurados.Count > 0)
                {
                    UsuarioInfo usuarioInfo = new UsuarioInfo
                    {
                        UsuarioID = usuarioID,
                        OrganizacionID = organizacionID
                    };

                    IList<MemoryStream> pdf = diferenciasDeInventarioPl.Guardar(listaFiltradaConfigurados, usuarioInfo);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CrearContrato_DatosGuardadosExito,
                        MessageBoxButton.OK, MessageImage.Correct);
                    if (pdf != null)
                    {
                        var exportarPoliza = new ExportarPoliza();
                        for (var indexPoliza = 0; indexPoliza < pdf.Count; indexPoliza++)
                        {
                            exportarPoliza.ImprimirPoliza(pdf[indexPoliza],
                                                          string.Format("{0}", "Poliza por ajuste"));
                        }
                    }
                    RecargarComboAlmacen();
                    LimpiarGrid();
                    LimpiarControlesDatosAjuste(true);
                    BtnGuardar.IsEnabled = false;
                    CargarAjustesPendientes();
                }
                //}
                //else
                //{
                //    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                //        Properties.Resources.DiferenciasDeInventario_MensajeAlmacenNoCuentaConfiguracion,
                //        MessageBoxButton.OK, MessageImage.Warning);
                //}
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                //Favor de seleccionar una organizacion
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    exg.Message, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Limpia los controles de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.DiferenciasDeInventario_MensajeBtnCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                RecargarComboAlmacen();
                LimpiarGrid();
                LimpiarControlesDatosAjuste(true);
                BtnGuardar.IsEnabled = false;
                CargarAjustesPendientes();
            }
        }

        /// <summary>
        /// Muestra ventana ajustes pendientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgAjustesPendientes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ObtenerAjustesPendientes();
        }

        /// <summary>
        /// Muestra ventana ajustes pendientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblAjustesPendientes_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ObtenerAjustesPendientes();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Carga combos ajuste y almacen
        /// </summary>
        private void CargarCombos()
        {
            //Combo ajuste
            CargarComboAjuste();
            CargarComboAlmacen();
        }

        /// <summary>
        /// Metodo que carga los datos en combo ajuste
        /// </summary>
        private void CargarComboAjuste()
        {
            try
            {
                CboAjuste.Items.Clear();
                CboAjuste.Items.Insert(0, Properties.Resources.DiferenciasDeInventario_CboSeleccione);
                CboAjuste.Items.Insert(TipoAjusteEnum.Merma.GetHashCode(), TipoAjusteEnum.Merma);
                CboAjuste.Items.Insert(TipoAjusteEnum.Superávit.GetHashCode(), TipoAjusteEnum.Superávit);
                CboAjuste.Items.Insert(TipoAjusteEnum.CerrarLote.GetHashCode(), Properties.Resources.DiferenciasDeInventario_CerrarLote);
                CboAjuste.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo que carga los datos en combo almacen
        /// </summary>
        private void CargarComboAlmacen()
        {
            try
            {
                CboAlmacen.ItemsSource = null;
                var almacenPl = new AlmacenPL();
                var listaAlmacen = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionID);
                if (listaAlmacen != null)
                {
                    //Filtrar lista productos
                    listaAlmacenFiltrada = (from almacenInfoL in listaAlmacen
                                            where
                                                almacenInfoL.TipoAlmacenID ==
                                                TipoAlmacenEnum.BodegaDeTerceros.GetHashCode() ||
                                                almacenInfoL.TipoAlmacenID ==
                                                TipoAlmacenEnum.MateriasPrimas.GetHashCode() ||
                                                almacenInfoL.TipoAlmacenID ==
                                               TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode() ||
                                                almacenInfoL.TipoAlmacenID ==
                                               TipoAlmacenEnum.EnTránsito.GetHashCode()
                                            select almacenInfoL).ToList();
                    //Agregamos lista filtrada al cbo
                    listaAlmacenFiltradaGlobal.AddRange(listaAlmacenFiltrada);
                    var almacenInfo = new AlmacenInfo
                    {
                        Descripcion = Properties.Resources.DiferenciasDeInventario_CboSeleccione,
                        AlmacenID = 0
                    };
                    listaAlmacenFiltrada.ForEach(alm => alm.Descripcion = string.Format("{0} ({1})", alm.Descripcion, alm.CodigoAlmacen));
                    listaAlmacenFiltrada.Insert(0, almacenInfo);
                    CboAlmacen.ItemsSource = listaAlmacenFiltrada;
                    CboAlmacen.SelectedValue = 0;
                }
                else
                {
                    listaAlmacen = new List<AlmacenInfo>();
                    var almacenInfo = new AlmacenInfo
                    {
                        Descripcion = Properties.Resources.DiferenciasDeInventario_CboSeleccione,
                        AlmacenID = 0
                    };
                    listaAlmacen.Insert(0, almacenInfo);
                    CboAlmacen.ItemsSource = listaAlmacen;
                    CboAlmacen.SelectedValue = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeErrorCargaComboAlmacen, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Deshabilita los controles al cargar la pantalla
        /// </summary>
        private void DeshabilitarControlesInicio()
        {
            CboAlmacen.IsEnabled = false;
            CboProducto.IsEnabled = false;
            CboLote.IsEnabled = false;
            TxtKilogramosTotales.IsEnabled = false;
            TxtPorcentajeAjuste.IsEnabled = false;
            TxtKilogramosActuales.IsEnabled = false;
            TxtKilogramosAjuste.IsEnabled = false;
            TxtJustificacion.IsEnabled = false;
            BtnGuardar.IsEnabled = false;
        }

        /// <summary>
        /// Avanza al siguiente control en la pantalla
        /// </summary>
        /// <param name="e"></param>
        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }


        /// <summary>
        /// Validacion antes de agregar el ajuste al grid
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarAgregar(bool actualizar)
        {
            var resultado = new ResultadoValidacion();
            if (CboAjuste.SelectedIndex != TipoAjusteEnum.CerrarLote.GetHashCode())
            {
                if (TxtKilosDiferencia.Value.HasValue && TxtKilosDiferencia.Value.Value == 0)
                {
                    TxtKilogramosAjuste.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionKilosCero;
                    return resultado;
                }
                if (TxtPorcentajeAjuste.Text.Trim() == String.Empty)
                {
                    TxtKilogramosAjuste.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionKilogramosFisicos;
                    return resultado;
                }
            }

            if (CboAjuste.SelectedIndex == 0)
            {
                CboAjuste.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionAjuste;
                return resultado;
            }

            if (CboAlmacen.SelectedIndex == 0)
            {
                CboAlmacen.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionAlmacen;
                return resultado;
            }

            if (CboProducto.SelectedIndex == 0)
            {
                CboProducto.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionProducto;
                return resultado;
            }

            if (CboLote.SelectedIndex == 0)
            {
                CboLote.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionLote;
                return resultado;
            }

            if (!TxtKilogramosAjuste.Value.HasValue)
            {
                TxtKilogramosAjuste.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionKilogramosFisicos;
                return resultado;
            }

            //if (TxtKilogramosAjuste.Text.Trim() == String.Empty)
            //{

            //}

            //if (Convert.ToDecimal(TxtKilogramosAjuste.Text.Replace(",","").Trim()) == 0)
            //{
            //    TxtKilogramosAjuste.Focus();
            //    resultado.Resultado = false;
            //    resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionKilogramosFisicos;
            //    return resultado;
            //}

            if (TxtKilogramosTotales.Text.Trim() == String.Empty)
            {
                TxtKilogramosAjuste.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionKilogramosFisicos;
                return resultado;
            }

            if (TxtKilogramosActuales.Text.Trim() == String.Empty)
            {
                TxtKilogramosAjuste.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionKilogramosFisicos;
                return resultado;
            }

            if (TxtJustificacion.Text.Trim() == String.Empty)
            {
                if (CboAjuste.SelectedIndex == TipoAjusteEnum.Merma.GetHashCode())
                {
                    TxtJustificacion.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeValidacionJustificacion;
                    return resultado;
                }
            }

            //Si no es actualizacion no se toma en cuenta
            if (!actualizar)
            {
                //Validar grid principal
                if (listaDiferenciasInventarios != null)
                {
                    if ((from diferenciaInventariosInfo in listaDiferenciasInventarios
                         let almacenInfo = (AlmacenInfo)CboAlmacen.SelectedItem
                         let productoInfo = (ProductoInfo)CboProducto.SelectedItem
                         let loteInfo = (AlmacenInventarioLoteInfo)CboLote.SelectedItem
                         where diferenciaInventariosInfo.Almacen.AlmacenID == almacenInfo.AlmacenID &&
                               diferenciaInventariosInfo.Producto.ProductoId == productoInfo.ProductoId &&
                               diferenciaInventariosInfo.AlmacenInventarioLote.AlmacenInventarioLoteId ==
                               loteInfo.AlmacenInventarioLoteId
                         select diferenciaInventariosInfo).Any())
                    {
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeAjusteEnGrid;
                        return resultado;
                    }
                }

                //Validar lista ajustes pendientes
                //CargarAjustesPendientes();
                if (listaAjustesPendientesPrincipal != null)
                {
                    if ((from diferenciasDeInventarios in listaAjustesPendientesPrincipal
                         let almacenInfo = (AlmacenInfo)CboAlmacen.SelectedItem
                         let productoInfo = (ProductoInfo)CboProducto.SelectedItem
                         let loteInfo = (AlmacenInventarioLoteInfo)CboLote.SelectedItem
                         where diferenciasDeInventarios.Almacen.AlmacenID == almacenInfo.AlmacenID &&
                               diferenciasDeInventarios.Producto.ProductoId == productoInfo.ProductoId &&
                               diferenciasDeInventarios.AlmacenInventarioLote.AlmacenInventarioLoteId ==
                               loteInfo.AlmacenInventarioLoteId
                         select diferenciasDeInventarios).Any())
                    {
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.Resources.DiferenciasDeInventario_MensajeAjusteEnGridPendientes;
                        return resultado;
                    }
                }
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Se agrega ajuste al grid
        /// </summary>
        private void AgregarAjuste()
        {
            try
            {
                var random = new Random();
                int ajuste;
                string descripcionAjuste;
                decimal diferenciaKilos = TxtKilosDiferencia.Value.HasValue ? TxtKilosDiferencia.Value.Value : 0;
                if (CboAjuste.SelectedIndex != TipoAjusteEnum.CerrarLote.GetHashCode())
                {
                    if (diferenciaKilos > 0)
                    {
                        ajuste = TipoMovimiento.SalidaPorAjuste.GetHashCode();
                        descripcionAjuste = TipoAjusteEnum.Merma.ToString();
                        esCambioCodigo = true;
                        CboAjuste.SelectedIndex = TipoAjusteEnum.Merma.GetHashCode();
                    }
                    else
                    {
                        ajuste = TipoMovimiento.EntradaPorAjuste.GetHashCode();
                        descripcionAjuste = TipoAjusteEnum.Superávit.ToString();
                        esCambioCodigo = true;
                        CboAjuste.SelectedIndex = TipoAjusteEnum.Superávit.GetHashCode();
                    }
                }
                else
                {
                    ajuste = 0;
                    descripcionAjuste = TipoAjusteEnum.CerrarLote.ToString();
                }
                var diferenciaInventariosInfo = new DiferenciasDeInventariosInfo
                {
                    AlmacenMovimiento = new AlmacenMovimientoInfo
                    {
                        TipoMovimientoID = ajuste,
                        AlmacenMovimientoID = random.Next(999999999),
                        Status = Estatus.DifInvAplicado.GetHashCode(),
                        UsuarioCreacionID = usuarioID,
                        Observaciones = TxtJustificacion.Text
                    },
                    DescripcionAjuste = descripcionAjuste,
                    Almacen = (AlmacenInfo)CboAlmacen.SelectedItem,
                    Producto = (ProductoInfo)CboProducto.SelectedItem,
                    AlmacenInventarioLote = (AlmacenInventarioLoteInfo)CboLote.SelectedItem,
                    KilogramosTotales = Convert.ToDecimal(TxtKilogramosTotales.Value),
                    KilogramosFisicos = Convert.ToDecimal(TxtKilogramosAjuste.Value),
                    KilogramosTeoricos = Convert.ToDecimal(TxtKilogramosActuales.Value),
                    PorcentajeAjuste = Convert.ToDecimal(TxtPorcentajeAjuste.Value),
                    Guardado = false,
                    Editable = true,
                    UsuarioCreacionId = usuarioID
                };
                listaDiferenciasInventarios.Add(diferenciaInventariosInfo);
                GridDiferenciasDeInventarios.ItemsSource = null;
                GridDiferenciasDeInventarios.ItemsSource = listaDiferenciasInventarios;
                LimpiarControlesDatosAjuste(true);
                BtnGuardar.IsEnabled = true;
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeErrorAgregarAjuste, MessageBoxButton.OK,
                    MessageImage.Error);
                BtnGuardar.Focus();
            }
        }

        /// <summary>
        /// Limpia controles en la pantalla
        /// </summary>
        private void LimpiarControlesDatosAjuste(bool limpiaTotal)
        {

            TxtKilogramosTotales.Text = string.Empty;
            TxtKilogramosAjuste.Text = string.Empty;
            TxtKilogramosActuales.Text = string.Empty;
            TxtPorcentajeAjuste.Text = string.Empty;
            TxtKilosDiferencia.Text = string.Empty;
            TxtJustificacion.Clear();

            if (limpiaTotal)
            {
                CboAjuste.SelectedIndex = 0;
            }

            CboAlmacen.SelectedIndex = 0;
            CboProducto.SelectedIndex = 0;
            CboLote.SelectedIndex = 0;
            CboAlmacen.IsEnabled = false;
            CboProducto.IsEnabled = false;
            CboLote.IsEnabled = false;
            TxtKilogramosAjuste.IsEnabled = false;
            TxtJustificacion.IsEnabled = false;
            CboAjuste.IsEnabled = true;
            CboAjuste.Focus();
            edicionAjuste = false;
            BtnAgregar.Content = Properties.Resources.DiferenciasDeInventario_BtnAgregar;
            esCambioCodigo = false;
        }

        /// <summary>
        /// Limpiar grid
        /// </summary>
        private void LimpiarGrid()
        {
            listaDiferenciasInventarios.Clear();
            GridDiferenciasDeInventarios.ItemsSource = null;
            GridDiferenciasDeInventarios.ItemsSource = listaDiferenciasInventarios;
        }

        /// <summary>
        /// Valida si hay ajustes que requieran autorizacion
        /// </summary>
        private void ValidarPorcentajesPermitidos()
        {
            var mermaSuperavitPl = new MermaSuperavitPL();
            foreach (var diferenciasDeInventariosInfoP in listaDiferenciasInventarios)
            {
                var mermaSuperavitInfo =
                    mermaSuperavitPl.ObtenerPorAlmacenIdProductoId(diferenciasDeInventariosInfoP.Almacen,
                        diferenciasDeInventariosInfoP.Producto);
                if (mermaSuperavitInfo != null)
                {
                    if (diferenciasDeInventariosInfoP.AlmacenMovimiento.TipoMovimientoID ==
                        TipoMovimiento.SalidaPorAjuste.GetHashCode())
                    {
                        //Si tiene estatus autorizado se cambia a aplicado para guardarlo
                        if (diferenciasDeInventariosInfoP.AlmacenMovimiento.Status !=
                            Estatus.DifInvAutorizado.GetHashCode())
                        {
                            if (diferenciasDeInventariosInfoP.PorcentajeAjuste > mermaSuperavitInfo.Merma)
                            {
                                //TODO cambiar a pendiente cuando se encuentre la funcionalidad de autorizacion
                                diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                                    Estatus.DifInvPendiente.GetHashCode();

                                //diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                                //    Estatus.DifInvAplicado.GetHashCode();

                                diferenciasDeInventariosInfoP.TieneConfiguracion = true;
                                diferenciasDeInventariosInfoP.PorcentajeAjustePermitidoMerma = mermaSuperavitInfo.Merma;
                                diferenciasDeInventariosInfoP.RequiereAutorizacion = true;
                            }
                            else
                            {
                                diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                                    Estatus.DifInvAplicado.GetHashCode();
                                diferenciasDeInventariosInfoP.TieneConfiguracion = true;
                            }
                        }
                        else
                        {
                            diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                                Estatus.DifInvAplicado.GetHashCode();
                            diferenciasDeInventariosInfoP.TieneConfiguracion = true;
                        }
                    }
                    else
                    {
                        //Si tiene estatus autorizado se cambia a aplicado para guardarlo
                        if (diferenciasDeInventariosInfoP.AlmacenMovimiento.Status !=
                            Estatus.DifInvAutorizado.GetHashCode())
                        {
                            if (diferenciasDeInventariosInfoP.PorcentajeAjuste > mermaSuperavitInfo.Superavit)
                            {
                                //TODO cambiar a pendiente cuando se encuentre la funcionalidad de autorizacion
                                diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                                    Estatus.DifInvPendiente.GetHashCode();

                                //diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                                //    Estatus.DifInvAplicado.GetHashCode();

                                diferenciasDeInventariosInfoP.TieneConfiguracion = true;
                                diferenciasDeInventariosInfoP.PorcentajeAjustePermitidoSuperavit =
                                    mermaSuperavitInfo.Superavit;
                                diferenciasDeInventariosInfoP.RequiereAutorizacion = true;
                            }
                            else
                            {
                                diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                                    Estatus.DifInvAplicado.GetHashCode();
                                diferenciasDeInventariosInfoP.TieneConfiguracion = true;
                            }
                        }
                        else
                        {
                            diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                                Estatus.DifInvAplicado.GetHashCode();
                            diferenciasDeInventariosInfoP.TieneConfiguracion = true;
                        }
                    }
                }
                else
                {
                    if (diferenciasDeInventariosInfoP.AlmacenMovimiento.Status ==
                        Estatus.DifInvAutorizado.GetHashCode())
                    {
                        diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                            Estatus.DifInvAplicado.GetHashCode();
                        diferenciasDeInventariosInfoP.TieneConfiguracion = true;
                    }
                    else
                    {
                        //TODO cambiar a pendiente cuando se encuentre la funcionalidad de autorizacion
                        diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                            Estatus.DifInvPendiente.GetHashCode();
                        diferenciasDeInventariosInfoP.TieneConfiguracion = false;

                        //diferenciasDeInventariosInfoP.AlmacenMovimiento.Status =
                        //    Estatus.DifInvAplicado.GetHashCode();
                        //diferenciasDeInventariosInfoP.TieneConfiguracion = true;
                    }
                }
            }
        }

        //Carga el combo almacen con almacenes previamente filtrados
        private void RecargarComboAlmacen()
        {
            CboAlmacen.ItemsSource = null;
            var almacenInfo = new AlmacenInfo
            {
                Descripcion = Properties.Resources.DiferenciasDeInventario_CboSeleccione,
                AlmacenID = 0
            };
            var listaAlmacenes = new List<AlmacenInfo>();
            listaAlmacenes.AddRange(listaAlmacenFiltradaGlobal);
            listaAlmacenes.Insert(0, almacenInfo);
            CboAlmacen.ItemsSource = listaAlmacenes;
        }

        /// <summary>
        /// Obtiene los ajustes pendientes
        /// </summary>
        private void CargarAjustesPendientes()
        {
            try
            {
                var diferenciasDeInventariosPl = new DiferenciasDeInventarioPL();
                var listaEstatus = new List<EstatusInfo>
                {
                    new EstatusInfo {EstatusId = Estatus.DifInvAutorizado.GetHashCode()},
                    new EstatusInfo {EstatusId = Estatus.DifInvPendiente.GetHashCode()},
                    new EstatusInfo {EstatusId = Estatus.DifInvRechazado.GetHashCode()}
                };
                var listaMovimientos = new List<TipoMovimientoInfo>
                {
                    new TipoMovimientoInfo {TipoMovimientoID = TipoMovimiento.EntradaPorAjuste.GetHashCode()},
                    new TipoMovimientoInfo {TipoMovimientoID = TipoMovimiento.SalidaPorAjuste.GetHashCode()}
                };
                listaAjustesPendientesPrincipal =
                    diferenciasDeInventariosPl.ObtenerAjustesPendientesPorUsuario(listaEstatus, listaMovimientos,
                        new UsuarioInfo { UsuarioCreacionID = usuarioID });
                if (listaAjustesPendientesPrincipal != null)
                {
                    if (listaAjustesPendientesPrincipal.Count > 0)
                    {
                        LblAjustesPendientes.Content =
                            Properties.Resources.DiferenciasDeInventario_LblPendientesAutorizacion +
                            Properties.Resources.DiferenciasDeInventario_AbrirPorcentaje +
                            listaAjustesPendientesPrincipal.Count +
                            Properties.Resources.DiferenciasDeInventario_CerrarPorcentaje;
                        ImgAjustesPendientes.Visibility = Visibility.Visible;
                        LblAjustesPendientes.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ImgAjustesPendientes.Visibility = Visibility.Hidden;
                        LblAjustesPendientes.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    ImgAjustesPendientes.Visibility = Visibility.Hidden;
                    LblAjustesPendientes.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
            }
        }

        /// <summary>
        /// Muestra ventana ajustes pendientes
        /// </summary>
        private void ObtenerAjustesPendientes()
        {
            //Quitar guardados
            var listaAjustesPendientesFiltrada = listaDiferenciasInventarios.Where(diferenciasDeInventariosInfoPar => diferenciasDeInventariosInfoPar.Guardado).ToList();
            //

            var ajustesPendientesDiferenciasDeInventario = new AjustesPendientesDiferenciasDeInventario(listaAjustesPendientesFiltrada);
            MostrarCentrado(ajustesPendientesDiferenciasDeInventario);
            var listaAjustesPendientesSeleccionados =
                ajustesPendientesDiferenciasDeInventario.ListaAjustesPendientesRegresoSeleccionados;
            listaDiferenciasInventarios.AddRange(listaAjustesPendientesSeleccionados);
            GridDiferenciasDeInventarios.ItemsSource = null;
            GridDiferenciasDeInventarios.ItemsSource = listaDiferenciasInventarios;

            CargarAjustesPendientes();
            if (listaAjustesPendientesPrincipal != null)
            {
                //Obtener guardados
                listaAjustesPendientesFiltrada = listaDiferenciasInventarios.Where(diferenciasDeInventariosInfoPar => diferenciasDeInventariosInfoPar.Guardado).ToList();
                //Obtener guardados

                var ajustesPendientes = listaAjustesPendientesPrincipal.Count - listaAjustesPendientesFiltrada.Count;
                //Colocar en etiqueta ajustes pendientes
                LblAjustesPendientes.Content = Properties.Resources.DiferenciasDeInventario_LblPendientesAutorizacion +
                                               Properties.Resources.DiferenciasDeInventario_AbrirPorcentaje +
                                               ajustesPendientes +
                                               Properties.Resources.DiferenciasDeInventario_CerrarPorcentaje;
                if (listaAjustesPendientesSeleccionados.Count > 0)
                {
                    BtnGuardar.IsEnabled = true;
                }
            }
        }

        //Asigna el ajuste del info al cmb
        private void AsignarAjusteEdicion()
        {
            CboAjuste.IsEnabled = false;
            CboAjuste.SelectedIndex = diferenciasDeInventarioInfo.AlmacenMovimiento.TipoMovimientoID ==
                                      TipoMovimiento.SalidaPorAjuste.GetHashCode()
                ? TipoAjusteEnum.Merma.GetHashCode()
                : TipoAjusteEnum.Superávit.GetHashCode();
        }

        //Asigna el almacen del ajuste al editar
        private void AsignarAlmacenEdicion()
        {
            var listaAlmacen = new List<AlmacenInfo>();
            var almacenDefault = new AlmacenInfo
            {
                Descripcion = Properties.Resources.DiferenciasDeInventario_CboSeleccione,
                AlmacenID = 0
            };
            listaAlmacen.Insert(0, almacenDefault);
            almacenDefault = new AlmacenInfo
            {
                Descripcion = diferenciasDeInventarioInfo.Almacen.Descripcion,
                AlmacenID = diferenciasDeInventarioInfo.Almacen.AlmacenID
            };
            listaAlmacen.Insert(1, almacenDefault);
            CboAlmacen.ItemsSource = null;
            CboAlmacen.ItemsSource = listaAlmacen;
            CboAlmacen.SelectedIndex = 1;
            CboAlmacen.IsEnabled = false;
        }

        //Asigna el producto del ajuste al editar
        private void AsignarProductoEdicion()
        {
            var listaProductos = new List<ProductoInfo>();
            var productoInfoDefault = new ProductoInfo
            {
                ProductoDescripcion = Properties.Resources.DiferenciasDeInventario_CboSeleccione,
                ProductoId = 0
            };
            listaProductos.Insert(0, productoInfoDefault);
            productoInfoDefault = new ProductoInfo
            {
                ProductoDescripcion = diferenciasDeInventarioInfo.Producto.Descripcion,
                ProductoId = diferenciasDeInventarioInfo.Producto.ProductoId
            };
            listaProductos.Insert(1, productoInfoDefault);
            CboProducto.ItemsSource = listaProductos;
            CboProducto.SelectedIndex = 1;
            CboProducto.IsEnabled = false;
        }

        //Asigna el lote del ajuste al editar
        private void AsignarLoteEdicion()
        {
            var listaLotes = new List<AlmacenInventarioLoteInfo>();
            var loteInfo = new AlmacenInventarioLoteInfo
            {
                DescripcionLote = Properties.Resources.DiferenciasDeInventario_CboSeleccione,
                AlmacenInventarioLoteId = diferenciasDeInventarioInfo.AlmacenInventarioLote.AlmacenInventarioLoteId
            };
            listaLotes.Insert(0, loteInfo);
            loteInfo = new AlmacenInventarioLoteInfo
            {
                DescripcionLote = diferenciasDeInventarioInfo.AlmacenInventarioLote.Lote.ToString(CultureInfo.InvariantCulture),
                AlmacenInventarioLoteId = diferenciasDeInventarioInfo.AlmacenInventarioLote.AlmacenInventarioLoteId
            };
            listaLotes.Insert(1, loteInfo);
            CboLote.ItemsSource = listaLotes;
            CboLote.SelectedIndex = 1;
            CboLote.IsEnabled = false;
        }

        private void CalcularAjuste()
        {
            if (CboLote.SelectedIndex != 0)
            {

                if (CboAjuste.SelectedIndex == TipoAjusteEnum.CerrarLote.GetHashCode())
                {
                    return;
                }
                if (TxtKilogramosAjuste.Value.HasValue)
                {
                    decimal tamanioLote = Convert.ToDecimal(TxtKilogramosTotales.Value);
                    decimal kilogramosTeoricos = Convert.ToDecimal(TxtKilogramosActuales.Value);
                    decimal kilogramosFisicos = Convert.ToDecimal(TxtKilogramosAjuste.Value);

                    decimal kilogramosDiferencia = kilogramosTeoricos - kilogramosFisicos;
                    TxtKilosDiferencia.Value = Convert.ToInt32(kilogramosDiferencia);

                    if (tamanioLote != 0)
                    {
                        var porcentajeAjuste = decimal.Round(((kilogramosDiferencia / tamanioLote) * 100), 2);
                        TxtPorcentajeAjuste.Value = porcentajeAjuste;
                    }
                    if (TxtKilosDiferencia.Value.Value > 0)
                    {
                        esCambioCodigo = true;
                        CboAjuste.SelectedIndex = TipoAjusteEnum.Merma.GetHashCode();
                    }
                    else
                    {
                        esCambioCodigo = true;
                        CboAjuste.SelectedIndex = TipoAjusteEnum.Superávit.GetHashCode();
                    }
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.DiferenciasDeInventario_MensajeValidacionSeleccionarLote,
                    MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}
