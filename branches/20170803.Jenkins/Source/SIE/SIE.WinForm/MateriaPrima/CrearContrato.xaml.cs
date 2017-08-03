using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;
using TipoCuenta = SIE.Services.Info.Enums.TipoCuenta;
using TipoOrganizacion = SIE.Services.Info.Enums.TipoOrganizacion;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para CrearContrato.xaml
    /// </summary>
    public partial class CrearContrato
    {
        #region Atributos
        private ContratoInfo contratoGlobal;
        private bool nuevoContrato;
        private int usuario;
        private List<CostoInfo> listaOtrosCostos = new List<CostoInfo>();
        public List<ContratoParcialInfo> listaContratoParcial = new List<ContratoParcialInfo>();
        //private bool contratoGuardado;
        public List<ContratoDetalleInfo> listaContratosDetalle = new List<ContratoDetalleInfo>();
        public List<ContratoHumedadInfo> listaContratoHumedad = new List<ContratoHumedadInfo>();
        private SKAyuda<ProveedorInfo> skAyudaProveedores;
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        private SKAyuda<ProductoInfo> skAyudaProducto;
        private SKAyuda<CuentaSAPInfo> skAyudaCuentaSAP;
        private int tipoCuentaID;
        private bool impresion;
        private int calidad;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de CrearContrato que recibe parametros
        /// </summary>
        /// <param name="nuevo"></param>
        public CrearContrato(bool nuevo)
        {
            InitializeComponent();
            nuevoContrato = nuevo;
            usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            calidad = 1;
            RbCalidadOrigen.IsChecked = true;
        }

        /// <summary>
        /// Constructor de CrearContrato
        /// </summary>
        public CrearContrato(ContratoInfo contratoInfo)
        {
            InitializeComponent();
            contratoGlobal = contratoInfo;
            usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            calidad = contratoGlobal.CalidadOrigen;
            if (calidad == EsOrigenEnum.Origen.GetHashCode())
            {
                RbCalidadOrigen.IsChecked = true;
            }
            else
            {
                RbCalidadDestino.IsChecked = true;
            }
            if (contratoGlobal.AplicaDescuento == 1)
            {
                chkDescuento.IsChecked = true;
            }
            else
            {
                chkDescuento.IsChecked = false;
            }
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento loaded de la pantalla CrearContrato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrearContrato_OnLoaded(object sender, RoutedEventArgs e)
        {
            CargarCombos();
            DefinirPantalla();
            if (contratoGlobal == null) return;
            if (contratoGlobal.Parcial == CompraParcialEnum.Parcial)
            {
                CargarContratoParcial();
            }
            if (contratoGlobal.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaTercero.GetHashCode() || contratoGlobal.TipoContrato.TipoContratoId == TipoContratoEnum.EnTransito.GetHashCode())
            {
                CargarListaCostos();
                CargarContratoHumedad();
            }
        }

        /// <summary>
        /// Evento cancelar de BtnCancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.CrearContrato_MensajeBtnCancelar,
                   MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Evento de btnGuardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (impresion)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_DatosGuardadosExito,
                    MessageBoxButton.OK, MessageImage.Correct);
                    LimpiarCamposPantalla();
                    impresion = false;
                }
                else
                {
                    var resultadoValidacion = ValidarCampos();
                    if (resultadoValidacion.Resultado)
                    {
                        if (!Guardar()) return;
                        if (nuevoContrato)
                        {
                            var punto = Properties.Resources.CrearContrato_Punto;

                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format("{0} {1}{2} {3}",
                                    Properties.Resources.CrearContrato_CancelarContratoMensaje1.Trim(),
                                    contratoGlobal.Folio.ToString(CultureInfo.InvariantCulture).Trim(),
                                    punto.Trim(),
                                    Properties.Resources.CrearContrato_DatosGuardadosExito),
                                MessageBoxButton.OK, MessageImage.Correct);
                            LimpiarCamposPantalla();
                            skAyudaOrganizacion.AsignarFoco();
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CrearContrato_DatosGuardadosExito,
                                MessageBoxButton.OK, MessageImage.Correct);
                            Close();
                        }
                    }
                    else
                    {
                        var mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje) ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco : resultadoValidacion.Mensaje;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            mensaje, MessageBoxButton.OK, MessageImage.Stop);
                    }
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                //Favor de seleccionar una organizacion
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                    MessageBoxButton.OK,
                    MessageImage.Stop);
                BtnGuardar.Focus();
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Guardar contrato e imprimir el ticket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImprimir_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (contratoGlobal != null)
                {
                    if (contratoGlobal.Guardado)
                    {
                        var crearContratoPl = new CrearContratoPL();
                        crearContratoPl.ImprimirContrato(contratoGlobal);
                    }
                }
                else
                {
                    var resultadoValidacion = ValidarCampos();
                    if (resultadoValidacion.Resultado)
                    {
                        if (!Guardar()) return;
                        var crearContratoPl = new CrearContratoPL();
                        InhabilitarCampos();
                        if (contratoGlobal != null)
                        {
                            contratoGlobal.Guardado = true;
                        }
                        impresion = true;
                        crearContratoPl.ImprimirContrato(contratoGlobal);
                    }
                    else
                    {
                        var mensaje = "";
                        mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                            ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco
                            : resultadoValidacion.Mensaje;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            mensaje, MessageBoxButton.OK, MessageImage.Stop);
                    }
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                //Favor de seleccionar una organizacion
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                    MessageBoxButton.OK,
                    MessageImage.Stop);
                BtnGuardar.Focus();
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo que inhabilita campos despues de imprimir
        /// </summary>
        private void InhabilitarCampos()
        {
            CboTipoCompra.IsEnabled = false;
            skAyudaOrganizacion.IsEnabled = false;
            skAyudaCuentaSAP.IsEnabled = false;
            skAyudaProveedores.IsEnabled = false;
            skAyudaProducto.IsEnabled = false;
            TxtToneladas.IsEnabled = false;
            TxtTolerancia.IsEnabled = false;
            CboMoneda.IsEnabled = false;
            TxtPrecio.IsEnabled = false;
            TxtMermaPermitida.IsEnabled = false;
            CboFlete.IsEnabled = false;
            CboPesoNegociado.IsEnabled = false;
            GridIndicadores.IsEnabled = false;
            BtnOtrosCostos.IsEnabled = false;
            BtnCapturarCompra.IsEnabled = false;
        }

        /// <summary>
        /// Evento SelectionChanged de CboTipoCompra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTipoCompra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CboTipoCompra.SelectedItem == null || CboTipoCompra.SelectedIndex == 0)
                {
                    skAyudaCuentaSAP.LimpiarCampos();
                    skAyudaCuentaSAP.IsEnabled = false;
                    BtnOtrosCostos.IsEnabled = false;
                }
                else
                {
                    switch ((int)CboTipoCompra.SelectedValue)
                    {
                        case (int)TipoContratoEnum.BodegaNormal:
                            BtnOtrosCostos.IsEnabled = false;
                            AgregarAyudaProveedor();
                            skAyudaCuentaSAP.LimpiarCampos();
                            skAyudaCuentaSAP.IsEnabled = false;
                            RbCompraTotal.IsChecked = false;
                            RbCompraParcial.IsChecked = false;
                            listaContratoParcial.Clear();
                            BtnCapturarCompra.IsEnabled = false;
                            break;
                        case (int)TipoContratoEnum.BodegaTercero:
                            BtnOtrosCostos.IsEnabled = true;
                            AgregarAyudaProveedorAlmacen();
                            skAyudaCuentaSAP.LimpiarCampos();
                            skAyudaCuentaSAP.IsEnabled = true;
                            skAyudaCuentaSAP.Info = new CuentaSAPInfo()
                            {
                                ListaTiposCuenta = new List<TipoCuentaInfo>() { new TipoCuentaInfo() { TipoCuentaID = TipoCuenta.Producto.GetHashCode() } },
                                Activo = EstatusEnum.Activo
                            };
                            tipoCuentaID = TipoCuenta.Producto.GetHashCode();
                            RbCompraTotal.IsChecked = false;
                            RbCompraParcial.IsChecked = false;
                            listaContratoParcial.Clear();
                            BtnCapturarCompra.IsEnabled = false;
                            break;
                        case (int)TipoContratoEnum.EnTransito:
                            BtnOtrosCostos.IsEnabled = true;
                            AgregarAyudaProveedorAlmacen();
                            skAyudaCuentaSAP.LimpiarCampos();
                            skAyudaCuentaSAP.IsEnabled = true;
                            skAyudaCuentaSAP.Info = new CuentaSAPInfo()
                            {
                                ListaTiposCuenta = new List<TipoCuentaInfo>() { new TipoCuentaInfo() { TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode() } },
                                Activo = EstatusEnum.Activo
                            };
                            tipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode();
                            RbCompraTotal.IsChecked = false;
                            RbCompraParcial.IsChecked = false;
                            listaContratoParcial.Clear();
                            BtnCapturarCompra.IsEnabled = false;
                            break;
                    }
                }
                listaOtrosCostos = new List<CostoInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que cancela un contrato (cambiar el estado a inactivo)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelarContrato_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_CancelarContratoMensaje,
                    MessageBoxButton.YesNo, MessageImage.Warning) != MessageBoxResult.Yes) return;

                var entradaProductoPl = new EntradaProductoPL();
                var listaEntradasProducto = entradaProductoPl.ObtenerEntradaProductoPorContratoId(
                    new EntradaProductoInfo()
                    {
                        Contrato = contratoGlobal,
                        Organizacion = contratoGlobal.Organizacion,
                        Producto = contratoGlobal.Producto,
                        Activo = EstatusEnum.Activo
                    }
                    );
                if (listaEntradasProducto != null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CrearContrato_MensajeContratoTieneEntradas,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    var crearContratoPl = new CrearContratoPL();
                    contratoGlobal.Estatus = new EstatusInfo() { EstatusId = EstatusContratoEnum.Cancelado.GetHashCode() };
                    contratoGlobal.UsuarioModificacionId = usuario;

                    var listaContratosParcial = (from contratoParcialInfo in listaContratoParcial
                                                 where contratoParcialInfo.Guardado
                                                 select new ContratoParcialInfo()
                                                 {
                                                     ContratoParcialId = contratoParcialInfo.ContratoParcialId
                                                 }).ToList();

                    contratoGlobal.ListaContratoParcial = listaContratosParcial;
                    crearContratoPl.ActualizarEstado(contratoGlobal);

                    var punto = Properties.Resources.CrearContrato_Punto;

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format("{0} {1}{2} {3}",
                            Properties.Resources.CrearContrato_CancelarContratoMensaje1.Trim(),
                            contratoGlobal.Folio.ToString(CultureInfo.InvariantCulture).Trim(),
                            punto.Trim(),
                            Properties.Resources.CrearContrato_CancelarContratoMensaje2.Trim()),
                        MessageBoxButton.OK, MessageImage.Correct);
                }
                Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Evento previewkeydown de gridindicadores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridIndicadores_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            e.Handled = true;
            Send(Key.Tab);
        }

        /// <summary>
        /// Evento keydown para la forma en general
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrearContrato_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Evento keydown de cbopesonegociado (envia foco a primer celda del grid)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboPesoNegociado_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (listaContratosDetalle == null) return;
                    if (listaContratosDetalle.Count <= 0) return;
                    //GridIndicadores.Focus();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                }
            }
            else if (e.Key == Key.Tab)
            {
                e.Handled = true;
                Send(Key.Enter);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al entrar a alguna celda del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridIndicadores_OnCurrentCellChanged(object sender, EventArgs e)
        {
            Send(Key.Tab);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Limpia los campos de la pantalla e inicializa las variables iniciales
        /// </summary>
        public void LimpiarCamposPantalla()
        {
            try
            {
                skAyudaOrganizacion.LimpiarCampos();
                skAyudaProducto.LimpiarCampos();
                CboTipoCompra.SelectedIndex = 0;
                skAyudaCuentaSAP.LimpiarCampos();
                skAyudaProveedores.LimpiarCampos();
                TxtToneladas.ClearValue(DecimalUpDown.ValueProperty);
                TxtTolerancia.ClearValue(DecimalUpDown.ValueProperty);
                CboMoneda.SelectedIndex = 0;
                TxtPrecio.ClearValue(DecimalUpDown.ValueProperty);
                RbCompraParcial.IsChecked = false;
                RbCompraTotal.IsChecked = false;
                TxtMermaPermitida.ClearValue(DecimalUpDown.ValueProperty);
                txtFecha.Clear();
                CboFlete.SelectedIndex = 0;
                CboPesoNegociado.SelectedIndex = 0;
                lblFolio.Content = string.Empty;
                lblNumeroFolio.Content = string.Empty;
                if (listaOtrosCostos != null)
                {
                    listaOtrosCostos.Clear();
                }
                if (listaContratosDetalle != null)
                {
                    listaContratosDetalle.Clear();
                }
                if (listaContratoParcial != null)
                {
                    listaContratoParcial.Clear();
                }
                if (listaContratoHumedad != null)
                {
                    listaContratoHumedad.Clear();
                }
                CboTipoCompra.IsEnabled = true;
                skAyudaOrganizacion.IsEnabled = true;
                skAyudaProducto.IsEnabled = true;
                skAyudaCuentaSAP.IsEnabled = true;
                skAyudaProveedores.IsEnabled = true;
                TxtToneladas.IsEnabled = true;
                TxtTolerancia.IsEnabled = true;
                CboMoneda.IsEnabled = true;
                TxtPrecio.IsEnabled = true;
                TxtMermaPermitida.IsEnabled = true;
                CboFlete.IsEnabled = true;
                CboPesoNegociado.IsEnabled = true;
                BtnCancelar.IsEnabled = true;
                BtnOtrosCostos.IsEnabled = false;
                BtnCancelarContrato.IsEnabled = false;
                BtnCapturarCompra.IsEnabled = false;
                BtnHumedad.IsEnabled = false;
                var fechaPl = new FechaPL();
                var fechaActual = fechaPl.ObtenerFechaActual();
                txtFecha.Text = fechaActual.FechaActual.ToString("dd'/'MM'/'yyyy");
                nuevoContrato = true;
                contratoGlobal = null;
                //contratoGuardado = false;
                GridIndicadores.ItemsSource = null;
                GridIndicadores.IsEnabled = true;

                txtContratoAcerca.Text = string.Empty;
                txtContratoAcerca.IsEnabled = false;
                txtFolioCobertura.Text = string.Empty;
                txtFolioCobertura.IsEnabled = false;
                RbCalidadOrigen.IsChecked = true;
                RbCalidadDestino.IsChecked = false;
                chkCostoSecado.IsChecked = false;
                chkCostoSecado.IsEnabled = false;
                chkDescuento.IsChecked = false;
                calidad = 1;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Se valida que el proveedor tenga un almacen
        /// </summary>
        /// <returns></returns>
        public bool ValidarProveedorAlmacen()
        {
            try
            {
                var resultado = false;
                if ((int)CboTipoCompra.SelectedValue != TipoContratoEnum.BodegaTercero.GetHashCode())
                {
                    return true;
                }

                //Se obtienen los datos del proveedor
                var proveedorPl = new ProveedorPL();
                var proveedorInfo = new ProveedorInfo()
                {
                    CodigoSAP = skAyudaProveedores.Clave
                };
                proveedorInfo = proveedorPl.ObtenerPorCodigoSAP(proveedorInfo);
                var proveedorAlmacenBl = new ProveedorAlmacenPL();
                var almacenProveedor = proveedorAlmacenBl.ObtenerPorProveedorId(proveedorInfo);
                if (almacenProveedor != null)
                {
                    resultado = true;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Metodo que carga los datos de la ayuda
        /// </summary>
        private void AgregarAyudaProveedor()
        {
            var proveedorInfo = new ProveedorInfo
            {
                ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode() }
                    },
                Activo = EstatusEnum.Activo
            };
            skAyudaProveedores = new SKAyuda<ProveedorInfo>(200, false, proveedorInfo
                                                   , "PropiedadClaveCrearContrato"
                                                   , "PropiedadDescripcionCrearContrato",
                                                   "", false, 80, 10, true)
            {
                AyudaPL = new ProveedorPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaProveedorAdministrarContrato_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaProveedorAdministrarContrato_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaProveedorAdministrarContrato_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo,
            };
            skAyudaProveedores.ObtenerDatos += ObtenerDatosProveedor;
            skAyudaProveedores.AsignaTabIndex(4);
            SplAyudaProveedor.Children.Clear();
            SplAyudaProveedor.Children.Add(skAyudaProveedores);
        }

        /// <summary>
        /// Metodo que carga los datos de la ayuda de los proveedores que tienen almacen
        /// </summary>
        private void AgregarAyudaProveedorAlmacen()
        {
            var proveedorInfo = new ProveedorInfo
            {
                ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode() }
                    },
                Activo = EstatusEnum.Activo
            };
            skAyudaProveedores = new SKAyuda<ProveedorInfo>(200, false, proveedorInfo
                                                   , "PropiedadClaveCrearContrato"
                                                   , "PropiedadDescripcionCrearContratoAlmacen",
                                                   "", false, 80, 10, true)
            {
                AyudaPL = new ProveedorPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaProveedorAdministrarContrato_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaProveedorAdministrarContrato_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaProveedorAdministrarContrato_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo,
            };
            skAyudaProveedores.ObtenerDatos += ObtenerDatosProveedorAlmacen;
            skAyudaProveedores.AsignaTabIndex(4);
            SplAyudaProveedor.Children.Clear();
            SplAyudaProveedor.Children.Add(skAyudaProveedores);
        }

        /// <summary>
        /// Obtiene los datos del proveedor por almacen seleccionado en la ayuda
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatosProveedorAlmacen(string filtro)
        {
            try
            {
                if (string.IsNullOrEmpty(filtro))
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    return;
                }
                if (skAyudaProveedores.Info == null)
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    return;
                }
                if (skAyudaProveedores.Info.TipoProveedor.TipoProveedorID !=
                    TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() && skAyudaProveedores.Info.TipoProveedor.TipoProveedorID !=
                    TipoProveedorEnum.ProveedoresFletes.GetHashCode())
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };

                    skAyudaProveedores.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CrearContrato_MensajeProveedorInvalido,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                else
                {
                    var proveedorAlmacen = new ProveedorAlmacenInfo();
                    var proveedorAlmacenPl = new ProveedorAlmacenPL();

                    var proveedor = new ProveedorInfo();
                    var proveedorPl = new ProveedorPL();
                    proveedor = proveedorPl.ObtenerPorCodigoSAP(new ProveedorInfo() { CodigoSAP = filtro });

                    if (proveedor != null)
                    {
                        proveedorAlmacen = proveedorAlmacenPl.ObtenerPorProveedorId(proveedor);
                        if (proveedorAlmacen != null)
                        {
                            skAyudaProveedores.Info = new ProveedorInfo
                            {
                                ListaTiposProveedor = new List<TipoProveedorInfo>
                                {
                                    new TipoProveedorInfo
                                    {
                                        TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                                    },
                                    new TipoProveedorInfo
                                    {
                                        TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                                    }
                                },
                                Activo = EstatusEnum.Activo
                            };
                        }
                        else
                        {
                            skAyudaProveedores.LimpiarCampos();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CrearContrato_MensajeProveedorSinAlmacen,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CrearContrato_MensajeProveedorInvalido,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene los datos del proveedor con la clave obtenida en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProveedor(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                 TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    return;
                }
                if (skAyudaProveedores.Info == null)
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                 TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    return;
                }
                if (skAyudaProveedores.Info.TipoProveedor.TipoProveedorID !=
                    TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() && skAyudaProveedores.Info.TipoProveedor.TipoProveedorID != 
                    TipoProveedorEnum.ProveedoresFletes.GetHashCode())
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                 TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()///Descomentar
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };

                    skAyudaProveedores.LimpiarCampos();
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                 TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CrearContrato_MensajeProveedorInvalido,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                else
                {
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                            },
                            new TipoProveedorInfo
                            {
                                 TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Carga ayudas de almacenes
        /// </summary>
        private void AgregarAyudaDivision()
        {
            var organizacionInfo = new OrganizacionInfo()
            {
                TipoOrganizacion = new TipoOrganizacionInfo() { TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode() },
                Activo = EstatusEnum.Activo
            };
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, organizacionInfo
                                                   , "PropiedadClaveCrearContrato"
                                                   , "PropiedadDescripcionCrearContrato",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.CrearContrato_AyudaDivisionInvalida,
                MensajeBusquedaCerrar = Properties.Resources.CrearContrato_AyudaDivisionSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.CrearContrato_AyudaDivisionSeleccionar,
                TituloEtiqueta = Properties.Resources.CrearContrato_AyudaDivisionEtiquetaDivision,
                TituloPantalla = Properties.Resources.CrearContrato_AyudaDivisionTitulo,
            };
            skAyudaOrganizacion.ObtenerDatos += ObtenerDatosOrganizacion;
            skAyudaOrganizacion.AsignaTabIndex(0);
            SplAyudaDivision.Children.Clear();
            SplAyudaDivision.Children.Add(skAyudaOrganizacion);
        }

        /// <summary>
        /// Obtiene la organizacion seleccionada en el filtro (destino)
        /// </summary>
        private void ObtenerDatosOrganizacion(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaOrganizacion.Info == null)
                {
                    return;
                }
                skAyudaOrganizacion.Info = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo() { TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode() },
                    Activo = EstatusEnum.Activo
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosOrganizacion, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Agregar la ayuda productos a la pantalla
        /// </summary>
        private void AgregarAyudaProductos()
        {
            var productoInfo = new ProductoInfo
            {
                FamiliaId = FamiliasEnum.MateriaPrimas.GetHashCode(),
                SubfamiliaId = SubFamiliasEnum.Pacas.GetHashCode(),
                Familia = new FamiliaInfo() { FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode() },
                SubFamilia = new SubFamiliaInfo() { SubFamiliaID = SubFamiliasEnum.Pacas.GetHashCode() },
                Activo = EstatusEnum.Activo
            };
            skAyudaProducto = new SKAyuda<ProductoInfo>(200, false, productoInfo
                                                   , "PropiedadClaveCrearContrato"
                                                   , "PropiedadDescripcionCrearContrato",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new ProductoPL(),
                MensajeClaveInexistente = Properties.Resources.CrearContrato_AyudaProductoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.CrearContrato_AyudaProductoSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.CrearContrato_AyudaProductoSeleccionar,
                TituloEtiqueta = Properties.Resources.CrearContrato_AyudaProductoEtiquetaProducto,
                TituloPantalla = Properties.Resources.CrearContrato_AyudaProductoTitulo,
            };
            skAyudaProducto.ObtenerDatos += ObtenerDatosProducto;
            skAyudaProducto.AsignaTabIndex(1);
            SplAyudaProducto.Children.Clear();
            SplAyudaProducto.Children.Add(skAyudaProducto);
        }

        /// <summary>
        /// Obtiene datos del producto seleccionado
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProducto(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaProducto.Info == null)
                {
                    return;
                }
                //Obtener indicadores
                LlenarGridIndicadores(nuevoContrato);
                ActivarAserca(skAyudaProducto.Info);
                skAyudaProducto.Info = new ProductoInfo()
                {
                    FamiliaId = FamiliasEnum.MateriaPrimas.GetHashCode(),
                    SubfamiliaId = SubFamiliasEnum.Pacas.GetHashCode(),
                    Familia = new FamiliaInfo() { FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode() },
                    SubFamilia = new SubFamiliaInfo() { SubFamiliaID = SubFamiliasEnum.Pacas.GetHashCode() },
                    Activo = EstatusEnum.Activo
                };


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosOrganizacion, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Verificar si el producto pertenece a la familia de grano para activar el folio de contrato ASERCA
        /// </summary>
        /// <param name="producto">Producto seleccionado</param>
        private void ActivarAserca(ProductoInfo producto)
        {
            try
            {
                var subFamiliaPl = new SubFamiliaPL();

                SubFamiliaInfo subFamilia = subFamiliaPl.ObtenerPorID(producto.SubfamiliaId);
                txtContratoAcerca.IsEnabled = false;
                txtFolioCobertura.IsEnabled = false;
                chkCostoSecado.IsEnabled = false;
                chkCostoSecado.IsChecked = false;
                txtFolioCobertura.Text = string.Empty;
                txtContratoAcerca.Text = string.Empty;
                if (subFamilia != null)
                {
                    if (subFamilia.SubFamiliaID == (int)SubFamiliasEnum.Granos)
                    {
                        txtContratoAcerca.IsEnabled = true;
                        txtFolioCobertura.IsEnabled = true;
                        chkCostoSecado.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearContrato_ErrSubfamilia, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Agregar la ayuda cuenta a la pantalla
        /// </summary>
        private void AgregarAyudaCuentas()
        {
            var cuentaSapInfo = new CuentaSAPInfo
            {
                ListaTiposCuenta = new List<TipoCuentaInfo>() { new TipoCuentaInfo() { TipoCuentaID = tipoCuentaID } },
                Activo = EstatusEnum.Activo
            };
            skAyudaCuentaSAP = new SKAyuda<CuentaSAPInfo>(200, false, cuentaSapInfo
                                                   , "PropiedadClaveCrearContrato"
                                                   , "PropiedadDescripcionCrearContrato",
                                                   "", false, 80, 10, true)
            {
                AyudaPL = new CuentaSAPPL(),
                MensajeClaveInexistente = Properties.Resources.CrearContrato_AyudaCuentaInvalido,
                MensajeBusquedaCerrar = Properties.Resources.CrearContrato_AyudaCuentaSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.CrearContrato_AyudaCuentaSeleccionar,
                TituloEtiqueta = Properties.Resources.CrearContrato_AyudaCuentaEtiquetaCuenta,
                TituloPantalla = Properties.Resources.CrearContrato_AyudaCuentaTitulo,
            };
            skAyudaCuentaSAP.ObtenerDatos += ObtenerDatosCuenta;
            skAyudaCuentaSAP.AsignaTabIndex(3);
            SplAyudaCuenta.Children.Clear();
            SplAyudaCuenta.Children.Add(skAyudaCuentaSAP);
        }

        /// <summary>
        /// Obtiene los datos de la cuenta
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCuenta(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaCuentaSAP.Info == null)
                {
                    return;
                }
                skAyudaCuentaSAP.Info = new CuentaSAPInfo()
                {
                    ListaTiposCuenta = new List<TipoCuentaInfo>() { new TipoCuentaInfo() { TipoCuentaID = tipoCuentaID } },
                    Activo = EstatusEnum.Activo
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosOrganizacion, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Cargar lista de costos
        /// </summary>
        private void CargarListaCostos()
        {
            try
            {
                var almacenInventarioDetallePl = new AlmacenMovimientoDetallePL();
                var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle()
                {
                    ContratoId = contratoGlobal.ContratoId
                };
                almacenMovimientoDetalleInfo =
                    almacenInventarioDetallePl.ObtenerPorContratoId(almacenMovimientoDetalleInfo);

                if (almacenMovimientoDetalleInfo == null) return;
                var almacenMovimientoCostoPl = new AlmacenMovimientoCostoPL();
                //var almacenMovimientoInfo = new AlmacenMovimientoInfo()
                //{
                //    AlmacenMovimientoID = almacenMovimientoDetalleInfo.AlmacenMovimientoID
                //};
                var listaAlmacenMovimientoCosto =
                    almacenMovimientoCostoPl.ObtenerPorContratoID(almacenMovimientoDetalleInfo.ContratoId);
                if (listaAlmacenMovimientoCosto == null) return;

                var costos = listaAlmacenMovimientoCosto.Select(almacenMovimientoCostoInfo => new CostoInfo()
                {
                    CostoID = almacenMovimientoCostoInfo.Costo.CostoID,
                    Descripcion = almacenMovimientoCostoInfo.Costo.Descripcion,
                    ToneladasCosto = almacenMovimientoCostoInfo.Cantidad / 1000,
                    ImporteCosto = almacenMovimientoCostoInfo.Importe,
                    Proveedor = almacenMovimientoCostoInfo.Proveedor,
                    CuentaSap = almacenMovimientoCostoInfo.CuentaSap,
                    TieneCuenta = almacenMovimientoCostoInfo.TieneCuenta,
                    AplicaIva = almacenMovimientoCostoInfo.Iva,
                    AplicaRetencion = almacenMovimientoCostoInfo.Retencion,
                    Editable = false
                }).ToList();
                listaOtrosCostos.AddRange(costos);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearContrato_MensajeErrorCargarListaCostos,
                MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene las parcialidades
        /// </summary>
        public void CargarContratoParcial()
        {
            try
            {
                var contratoParcialPl = new ContratoParcialPL();
                listaContratoParcial = contratoParcialPl.ObtenerPorContratoId(contratoGlobal);
                //Convertir importe y cantidad
                if (listaContratoParcial != null)
                {
                    foreach (var contratoParcialInfo in listaContratoParcial)
                    {
                        contratoParcialInfo.Cantidad = contratoParcialInfo.Cantidad / 1000;
                        if (contratoGlobal.TipoCambio == null) continue;
                        if (contratoGlobal.TipoCambio.Descripcion.ToUpper() ==
                            Properties.Resources.CrearContrato_DescripcionMonedaDolarMayuscula)
                        {
                            contratoParcialInfo.Importe = (contratoParcialInfo.Importe * 1000) /
                                                          contratoParcialInfo.TipoCambio.Cambio;
                        }
                        else
                        {
                            contratoParcialInfo.Importe = contratoParcialInfo.Importe * 1000;
                        }
                        contratoParcialInfo.Guardado = true;
                    }
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearContrato_MensajeErrorCargarContratoParcial,
                MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga listado de humedades
        /// </summary>
        public void CargarContratoHumedad()
        {
            try
            {
                var contratoHumedadPl = new ContratoHumedadPL();
                var listaHumedades = contratoHumedadPl.ObtenerPorContratoId(contratoGlobal);
                if (listaHumedades != null)
                {
                    listaContratoHumedad = listaHumedades;
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearContrato_MensajeErrorCargarHumedades,
                MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Se establece que controles estaran activos o inactivos para cada tipo de compra y que datos se mostraran
        /// </summary>
        private void DefinirPantalla()
        {
            try
            {
                btnActualizar.IsEnabled = false;
                BtnOtrosCostos.IsEnabled = false;
                BtnGuardar.IsEnabled = false;
                BtnCancelar.IsEnabled = false;
                BtnCancelarContrato.IsEnabled = false;
                lblFolio.Content = String.Empty;
                lblNumeroFolio.Content = String.Empty;

                if (nuevoContrato)
                {
                    var fechaPl = new FechaPL();
                    var fechaActual = fechaPl.ObtenerFechaActual();
                    txtFecha.Text = fechaActual.FechaActual.ToString("dd'/'MM'/'yyyy");
                    BtnGuardar.IsEnabled = true;
                    BtnCancelar.IsEnabled = true;
                    skAyudaCuentaSAP.IsEnabled = false;
                    BtnCapturarCompra.IsEnabled = false;
                    BtnHumedad.IsEnabled = false;
                    BtnCerrarContrato.IsEnabled = false;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else
                {
                    BtnHumedad.IsEnabled = true;
                    txtFecha.Text = contratoGlobal.Fecha.ToString("dd'/'MM'/'yyyy");
                    lblFolio.Content = Properties.Resources.CrearContrato_LblFolio;
                    lblNumeroFolio.Content = contratoGlobal.Folio;
                    btnActualizar.IsEnabled = true;
                    BtnCancelar.IsEnabled = true;
                    if (contratoGlobal.Activo == EstatusEnum.Activo)
                    {
                        BtnCancelarContrato.IsEnabled = true;

                        if (contratoGlobal.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaTercero ||
                            contratoGlobal.TipoContrato.TipoContratoId == (int)TipoContratoEnum.EnTransito)
                        {
                            BtnOtrosCostos.IsEnabled = true;
                            BtnGuardar.IsEnabled = true;
                        }
                        else
                        {
                            if (contratoGlobal.Parcial == CompraParcialEnum.Parcial)
                            {
                                BtnGuardar.IsEnabled = true;
                            }
                        }
                    }
                    else
                    {
                        if (contratoGlobal.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaTercero || contratoGlobal.TipoContrato.TipoContratoId == (int)TipoContratoEnum.EnTransito)
                        {
                            BtnOtrosCostos.IsEnabled = true;
                        }
                    }
                    CargarDatosContrato();
                    DeshabilitarControles();
                    LlenarGridIndicadores(false);
                    TxtToneladas.IsEnabled = true;
                    TxtTolerancia.IsEnabled = true;
                    CboPesoNegociado.IsEnabled = true;
                    txtFolioCobertura.IsEnabled = true;
                    txtContratoAcerca.IsEnabled = true;
                    CboFlete.IsEnabled = true;
                    //skAyudaOrganizacion.IsEnabled = false;
                    //skAyudaProducto.IsEnabled = false;
                    //CboTipoCompra.IsEnabled = false;
                    //skAyudaCuentaSAP.IsEnabled = false;
                    //skAyudaProveedores.IsEnabled = false;
                    //TxtToneladas.IsEnabled = false;
                    //TxtTolerancia.IsEnabled = false;
                    //CboMoneda.IsEnabled = false;
                    //TxtPrecio.IsEnabled = false;
                    //RbCompraParcial.IsEnabled = false;
                    //RbCompraTotal.IsEnabled = false;
                    //TxtMermaPermitida.IsEnabled = false;
                    //CboFlete.IsEnabled = false;
                    //CboPesoNegociado.IsEnabled = false;
                    //if (contratoGlobal.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaNormal.GetHashCode())
                    //{
                    //    BtnHumedad.IsEnabled = false;
                    //}
                }
            }
            catch (ExcepcionGenerica ex)
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
        /// Metodo que carga los valores en el grid indicadores para contratos nuevos o ya creados
        /// </summary>
        /// <param name="nuevo"></param>
        private void LlenarGridIndicadores(bool nuevo)
        {
            try
            {
                if (nuevo)
                {
                    var productoInfo = new ProductoInfo()
                    {
                        ProductoId = Convert.ToInt32(skAyudaProducto.Clave)
                    };
                    var indicadorProductoPl = new IndicadorProductoPL();
                    var indicadoresInfo = indicadorProductoPl.ObtenerPorProductoId(productoInfo, EstatusEnum.Activo);

                    if (indicadoresInfo != null)
                    {
                        listaContratosDetalle = indicadoresInfo.Select(contratoInfo => new ContratoDetalleInfo()
                        {
                            Indicador = contratoInfo.IndicadorInfo
                        }).ToList();
                        GridIndicadores.ItemsSource = listaContratosDetalle;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.CrearContrato_MsgProductoSinIndicador,
                           MessageBoxButton.OK, MessageImage.Warning);
                        //CboProducto.SelectedIndex = 0;
                        //CboProducto.Focus();
                        GridIndicadores.ItemsSource = null;
                        listaContratosDetalle.Clear();
                    }
                }
                else
                {
                    GridIndicadores.IsEnabled = false;
                    GridIndicadores.ItemsSource = contratoGlobal.ListaContratoDetalleInfo;
                    listaContratosDetalle = contratoGlobal.ListaContratoDetalleInfo;
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearContrato_MensajeErrorCargarIndicadores,
                MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga los datos del contrato en el pantalla
        /// </summary>
        private void CargarDatosContrato()
        {
            skAyudaOrganizacion.Clave = Convert.ToString(contratoGlobal.Organizacion.OrganizacionID);
            skAyudaOrganizacion.Descripcion = Convert.ToString(contratoGlobal.Organizacion.Descripcion);
            skAyudaProducto.Clave = Convert.ToString(contratoGlobal.Producto.ProductoId);
            skAyudaProducto.Descripcion = Convert.ToString(contratoGlobal.Producto.Descripcion);
            CboTipoCompra.SelectedValue = contratoGlobal.TipoContrato.TipoContratoId;

            if (contratoGlobal.TipoContrato.TipoContratoId != TipoContratoEnum.BodegaNormal.GetHashCode())
            {
                skAyudaCuentaSAP.Clave = contratoGlobal.Cuenta.CuentaSAP;
                skAyudaCuentaSAP.Descripcion = contratoGlobal.Cuenta.Descripcion;
            }

            skAyudaProveedores.Clave = Convert.ToString(contratoGlobal.Proveedor.CodigoSAP);
            skAyudaProveedores.Descripcion = Convert.ToString(contratoGlobal.Proveedor.Descripcion);
            TxtToneladas.Value = contratoGlobal.CantidadToneladas;
            if (contratoGlobal.Tolerancia > 0)
            {
                TxtTolerancia.Value = contratoGlobal.Tolerancia;
            }
            else
            {
                TxtTolerancia.Text = Convert.ToString(contratoGlobal.Tolerancia);
            }

            //Verificar moneda por desc
            if (contratoGlobal.TipoCambio.Descripcion.ToUpper().Trim() ==
                Properties.Resources.CrearContrato_DescripcionDolarMayuscula)
            {
                CboMoneda.SelectedIndex = 2;
            }
            else
            {
                CboMoneda.SelectedIndex = 1;
            }
            //CboMoneda.SelectedValue = contratoGlobal.TipoCambio.TipoCambioId;
            //

            TxtPrecio.Value = contratoGlobal.PrecioToneladas;

            if (contratoGlobal.Parcial == CompraParcialEnum.Parcial)
            {
                RbCompraParcial.IsChecked = true;
            }
            else
            {
                RbCompraTotal.IsChecked = true;
            }

            BtnCapturarCompra.IsEnabled = RbCompraParcial.IsChecked == true;

            if (contratoGlobal.Merma > 0)
            {
                TxtMermaPermitida.Value = contratoGlobal.Merma;
            }
            else
            {
                TxtMermaPermitida.Text = Convert.ToString(contratoGlobal.Merma);
            }

            CboFlete.SelectedValue = contratoGlobal.TipoFlete.TipoFleteId;

            if (Properties.Resources.CrearContrato_CboPesoNegociadoOrigen == contratoGlobal.PesoNegociar)
            {
                CboPesoNegociado.SelectedIndex = 1;
            }
            else
            {
                if (Properties.Resources.CrearContrato_CboPesoNegociadoDestino == contratoGlobal.PesoNegociar)
                {
                    CboPesoNegociado.SelectedIndex = 2;
                }
            }
            txtFolioCobertura.Value = contratoGlobal.FolioCobertura;
            txtContratoAcerca.Text = contratoGlobal.FolioAserca;
        }

        /// <summary>
        /// Metodo para deshabilitar controles (editar contrato)
        /// </summary>
        private void DeshabilitarControles()
        {
            skAyudaOrganizacion.IsEnabled = false;
            skAyudaProducto.IsEnabled = false;
            CboTipoCompra.IsEnabled = false;
            skAyudaCuentaSAP.IsEnabled = false;
            skAyudaProveedores.IsEnabled = false;
            TxtToneladas.IsEnabled = false;
            TxtTolerancia.IsEnabled = false;
            CboMoneda.IsEnabled = false;
            TxtPrecio.IsEnabled = false;
            RbCompraParcial.IsEnabled = false;
            RbCompraTotal.IsEnabled = false;
            TxtMermaPermitida.IsEnabled = false;
            CboFlete.IsEnabled = false;
            CboPesoNegociado.IsEnabled = false;
            if (contratoGlobal.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaNormal.GetHashCode())
            {
                BtnHumedad.IsEnabled = false;
            }
        }

        /// <summary>
        /// Cargar combos 
        /// </summary>
        private void CargarCombos()
        {
            AgregarAyudaDivision();
            AgregarAyudaProductos();
            AgregarAyudaProveedor();
            AgregarAyudaCuentas();
            CargarComboFlete();
            CargarComboTipoCompra();
            CargarComboTipoCambio();
            CargarComboPesoNegociado();
        }

        /// <summary>
        /// Cargar combo fletes
        /// </summary>
        private void CargarComboFlete()
        {
            try
            {
                var fletePl = new TipoFletePL();
                var listaTiposFleteInfo = fletePl.ObtenerTiposFleteTodos(EstatusEnum.Activo);
                if (listaTiposFleteInfo == null) return;
                //Se filtran productos para los tipos de contrato especificados en el requerimiento Administrar Contrato
                var listaFletesFiltradosInfo = listaTiposFleteInfo.Where(tipoFleteInfoT => tipoFleteInfoT.TipoFleteId == (int)TipoFleteEnum.LibreAbordo ||
                    tipoFleteInfoT.TipoFleteId == (int)TipoFleteEnum.PagoenGanadera).ToList();
                //Termina filtrado
                var tipoFleteInfo = new TipoFleteInfo { Descripcion = Properties.Resources.AdministrarContrato_Seleccione, TipoFleteId = 0 };
                listaFletesFiltradosInfo.Insert(0, tipoFleteInfo);
                CboFlete.ItemsSource = listaFletesFiltradosInfo;
                CboFlete.SelectedValue = 0;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearContrato_MensajeErrorCargarComboTipoFlete,
                MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Cargar combo fletes
        /// </summary>
        private void CargarComboTipoCompra()
        {
            try
            {
                var contratoPl = new TipoContratoPL();
                //Verificar si se obtendra por activos
                var listaTiposContratoInfo = contratoPl.ObtenerTodos();
                if (listaTiposContratoInfo == null) return;
                //Se filtran productos para los tipos de contrato especificados en el requerimiento Administrar Contrato
                var listaTiposContratoFiltradosInfo = listaTiposContratoInfo.Where(tipoContratoInfoT => tipoContratoInfoT.TipoContratoId == (int)TipoContratoEnum.BodegaNormal ||
                    tipoContratoInfoT.TipoContratoId == (int)TipoContratoEnum.BodegaTercero || tipoContratoInfoT.TipoContratoId == (int)TipoContratoEnum.EnTransito).ToList();
                //Termina filtrado
                var tipoContratoInfo = new TipoContratoInfo { Descripcion = Properties.Resources.AdministrarContrato_Seleccione, TipoContratoId = 0 };
                listaTiposContratoFiltradosInfo.Insert(0, tipoContratoInfo);
                CboTipoCompra.ItemsSource = listaTiposContratoFiltradosInfo;
                CboTipoCompra.SelectedValue = 0;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearContrato_MensajeErrorCargarComboTipoCompra,
                MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Cargar combo tipo cambio
        /// </summary>
        private void CargarComboTipoCambio()
        {
            try
            {
                var listaTiposCambioFiltradosInfo = new List<TipoCambioInfo>();
                //Termina filtrado
                var tipoCambioInfo = new TipoCambioInfo { Descripcion = Properties.Resources.AdministrarContrato_Seleccione, TipoCambioId = 0 };
                listaTiposCambioFiltradosInfo.Insert(0, tipoCambioInfo);
                listaTiposCambioFiltradosInfo.Insert(1, new TipoCambioInfo() { Descripcion = Properties.Resources.CrearContrato_DescripcionPesos });
                listaTiposCambioFiltradosInfo.Insert(2, new TipoCambioInfo() { Descripcion = Properties.Resources.CrearContrato_DescripcionDolar });
                CboMoneda.ItemsSource = listaTiposCambioFiltradosInfo;
                CboMoneda.SelectedValue = 0;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearContrato_MensajeErrorCargarComboTipoCambio,
                MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Cargar como peso negociado
        /// </summary>
        private void CargarComboPesoNegociado()
        {
            try
            {
                CboPesoNegociado.Items.Insert(0, Properties.Resources.AdministrarContrato_Seleccione);
                CboPesoNegociado.Items.Insert((int)PesoNegociarEnum.Origen, PesoNegociarEnum.Origen);
                CboPesoNegociado.Items.Insert((int)PesoNegociarEnum.Destino, PesoNegociarEnum.Destino);
                CboPesoNegociado.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que valida los elementos obligatorios
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCampos()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(skAyudaOrganizacion.Clave.Trim()) && String.IsNullOrEmpty(skAyudaOrganizacion.Descripcion.Trim()))
            {
                skAyudaOrganizacion.AsignarFoco();
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionDivision;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaProducto.Clave.Trim()) && String.IsNullOrEmpty(skAyudaProducto.Descripcion.Trim()))
            {
                skAyudaProducto.AsignarFoco();
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionProducto;
                resultado.Resultado = false;
                return resultado;
            }

            if ((String.IsNullOrEmpty(CboTipoCompra.Text) || (int)CboTipoCompra.SelectedValue == 0))
            {
                CboTipoCompra.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionTipoCompra;
                return resultado;
            }

            if ((int)CboTipoCompra.SelectedValue != TipoContratoEnum.BodegaNormal.GetHashCode())
            {
                if (String.IsNullOrEmpty(skAyudaCuentaSAP.Clave.Trim()) && String.IsNullOrEmpty(skAyudaCuentaSAP.Descripcion.Trim()))
                {
                    skAyudaCuentaSAP.AsignarFoco();
                    resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionCuenta;
                    resultado.Resultado = false;
                    return resultado;
                }
            }

            if (String.IsNullOrEmpty(skAyudaProveedores.Clave.Trim()) && String.IsNullOrEmpty(skAyudaProveedores.Descripcion.Trim()))
            {
                skAyudaProveedores.AsignarFoco();
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionProveedor;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtToneladas.Text))
            {
                TxtToneladas.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionToneladas;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtTolerancia.Text))
            {
                TxtTolerancia.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_ValidacionTolerancia;
                return resultado;
            }

            if ((String.IsNullOrEmpty(CboMoneda.Text) || CboMoneda.SelectedIndex == 0))
            {
                CboMoneda.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionMoneda;
                return resultado;
            }

            //Validar que haya
            bool tipoCambioEncontrado = false;
            var tipoCambioPl = new TipoCambioPL();
            var listaTipoCambio = tipoCambioPl.ObtenerPorFechaActual();
            if (listaTipoCambio != null)
            {
                foreach (var tipoCambioInfo in listaTipoCambio)
                {
                    if (CboMoneda.Text == Properties.Resources.CrearContrato_DescripcionPesos)
                    {
                        if (tipoCambioInfo.Descripcion == Properties.Resources.CrearContrato_DescripcionPesosMayuscula)
                        {
                            tipoCambioEncontrado = true;
                        }
                    }

                    if (CboMoneda.Text == Properties.Resources.CrearContrato_DescripcionDolar)
                    {
                        if (tipoCambioInfo.Descripcion == Properties.Resources.CrearContrato_DescripcionDolarMayuscula)
                        {
                            tipoCambioEncontrado = true;
                        }
                    }
                }

                if (!tipoCambioEncontrado)
                {
                    if (CboMoneda.Text == Properties.Resources.CrearContrato_DescripcionPesos)
                    {
                        BtnGuardar.Focus();
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.Resources.CrearContrato_ValidacionTipoCambioPesos;
                        return resultado;
                    }

                    if (CboMoneda.Text == Properties.Resources.CrearContrato_DescripcionDolar)
                    {
                        BtnGuardar.Focus();
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.Resources.CrearContrato_ValidacionTipoCambioDolar;
                        return resultado;
                    }
                }
            }
            else
            {
                BtnGuardar.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_ValidacionTipoCambioFechaActual;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtPrecio.Text))
            {
                TxtPrecio.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionPrecio;
                return resultado;
            }

            if (RbCompraTotal.IsChecked == false && RbCompraParcial.IsChecked == false)
            {
                RbCompraTotal.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_ValidacionCompra;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtMermaPermitida.Text))
            {
                TxtMermaPermitida.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionMermaPermitida;
                return resultado;
            }

            if ((String.IsNullOrEmpty(CboFlete.Text) || (int)CboFlete.SelectedValue == 0))
            {
                CboFlete.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionTipoFlete;
                return resultado;
            }

            if ((String.IsNullOrEmpty(CboPesoNegociado.Text) || CboPesoNegociado.Text == Properties.Resources.AdministrarContrato_Seleccione))
            {
                CboPesoNegociado.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionPesoNegociado;
                return resultado;
            }

            if (listaContratosDetalle != null && listaContratosDetalle.Count > 0)
            {
                if (listaContratosDetalle.Any(contratoDetalleInfo => String.IsNullOrEmpty(Convert.ToString(contratoDetalleInfo.PorcentajePermitido)) ||
                                                                     contratoDetalleInfo.PorcentajePermitido == 0))
                {
                    GridIndicadores.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CrearContrato_LblRangosRequeridos;
                    return resultado;
                }
            }
            else
            {
                GridIndicadores.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_CapturarValidacionIndicadoresCalidad;
                return resultado;
            }

            //Validar almacen bodega terceros
            var almacenPl = new AlmacenPL();
            if ((int)CboTipoCompra.SelectedValue == TipoContratoEnum.BodegaTercero.GetHashCode())
            {
                var listaTipoAlmacen = new List<TipoAlmacenEnum> { TipoAlmacenEnum.BodegaDeTerceros };
                var listaAlmacenes = almacenPl.ObtenerAlmacenPorTiposAlmacen(listaTipoAlmacen,
                    new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave) });
                if (listaAlmacenes == null || listaAlmacenes.Count == 0)
                {
                    skAyudaOrganizacion.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CrearContrato_ValidacionAlmacenBodegaTerceros;
                    return resultado;
                }
            }

            //Validar almacen en transito
            if ((int)CboTipoCompra.SelectedValue == TipoContratoEnum.EnTransito.GetHashCode())
            {
                var listaTipoAlmacen = new List<TipoAlmacenEnum> { TipoAlmacenEnum.EnTránsito };
                var listaAlmacenes = almacenPl.ObtenerAlmacenPorTiposAlmacen(listaTipoAlmacen,
                    new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave) });
                if (listaAlmacenes == null)
                {
                    skAyudaOrganizacion.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CrearContrato_ValidacionAlmacenEnTransito;
                    return resultado;
                }
            }
            //

            //Validar proveedor almacen
            if ((int)CboTipoCompra.SelectedValue != TipoContratoEnum.BodegaNormal.GetHashCode())
            {
                var tipoContrato = (int)CboTipoCompra.SelectedValue == (int)TipoContratoEnum.BodegaTercero
                                ? TipoContratoEnum.BodegaTercero.GetHashCode()
                                : TipoContratoEnum.EnTransito.GetHashCode();
                var proveedorPl = new ProveedorPL();
                var proveedorInfo = new ProveedorInfo()
                {
                    CodigoSAP = skAyudaProveedores.Clave
                };
                proveedorInfo = proveedorPl.ObtenerPorCodigoSAP(proveedorInfo);
                var proveedorAlmacenBl = new ProveedorAlmacenPL();
                var almacenProveedor = proveedorAlmacenBl.ObtenerPorProveedorId(proveedorInfo);
                if (almacenProveedor == null)
                {
                    skAyudaProveedores.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CrearContrato_MensajeProveedorAlmacen;
                    return resultado;
                }
                var almacenInfo = almacenPl.ObtenerPorID(new AlmacenInfo() { AlmacenID = almacenProveedor.AlmacenId });
                if (almacenInfo != null)
                {
                    if (tipoContrato == TipoContratoEnum.BodegaTercero.GetHashCode())
                    {
                        if (almacenInfo.TipoAlmacen.TipoAlmacenID != TipoAlmacenEnum.BodegaDeTerceros.GetHashCode())
                        {
                            skAyudaProveedores.Focus();
                            resultado.Resultado = false;
                            resultado.Mensaje =
                                Properties.Resources.CrearContrato_ValidacionProveedorAlmacenBodegaTerceros;
                            return resultado;
                        }
                    }

                    if (tipoContrato == TipoContratoEnum.EnTransito.GetHashCode())
                    {
                        if (almacenInfo.TipoAlmacen.TipoAlmacenID != TipoAlmacenEnum.EnTránsito.GetHashCode())
                        {
                            skAyudaProveedores.Focus();
                            resultado.Resultado = false;
                            resultado.Mensaje =
                                Properties.Resources.CrearContrato_ValidacionProveedorAlmacenEnTransito;
                            return resultado;
                        }
                    }
                }
            }
            //

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Metodo que crea la ventana Otros Costos
        /// </summary>
        /// <param name="ventana"></param>
        protected void MostrarCentrado(Window ventana)
        {
            ventana.Left = (ActualWidth - ventana.Width) / 2;
            ventana.Top = ((ActualHeight - ventana.Height) / 2) + 132;
            ventana.Owner = Application.Current.Windows[1];
            ventana.ShowDialog();
        }

        /// <summary>
        /// Guardar el contrato
        /// </summary>
        private bool Guardar()
        {
            try
            {
                var tipoCambio = new TipoCambioInfo();
                var tipoCambioPl = new TipoCambioPL();
                var listaTipoCambio = tipoCambioPl.ObtenerPorFechaActual();
                foreach (var tipoCambioInfo in listaTipoCambio)
                {
                    if (CboMoneda.Text == Properties.Resources.CrearContrato_DescripcionPesos)
                    {
                        if (tipoCambioInfo.Descripcion == Properties.Resources.CrearContrato_DescripcionPesosMayuscula)
                        {
                            tipoCambio = tipoCambioInfo;
                        }
                    }

                    if (CboMoneda.Text == Properties.Resources.CrearContrato_DescripcionDolar)
                    {
                        if (tipoCambioInfo.Descripcion == Properties.Resources.CrearContrato_DescripcionDolarMayuscula)
                        {
                            tipoCambio = tipoCambioInfo;
                        }
                    }
                }

                if (nuevoContrato)
                {
                    decimal precio;
                    //Convertir a kilos
                    var cantidad = TxtToneladas.Value.HasValue ? TxtToneladas.Value.Value * 1000 : 0;
                    //Convertir a precio por kilo
                    if (CboMoneda.Text == Properties.Resources.CrearContrato_DescripcionDolar)
                    {
                        precio = tipoCambio.Cambio * (TxtPrecio.Value.HasValue ? TxtPrecio.Value.Value : 0);
                        precio = precio / 1000;
                    }
                    else
                    {
                        precio = ((TxtPrecio.Value.HasValue ? TxtPrecio.Value.Value : 0) / 1000);
                    }

                    //Se obtienen los datos del proveedor
                    var proveedorPl = new ProveedorPL();
                    var proveedorInfo = new ProveedorInfo()
                    {
                        CodigoSAP = skAyudaProveedores.Clave
                    };
                    proveedorInfo = proveedorPl.ObtenerPorCodigoSAP(proveedorInfo);

                    var cuentaSappl = new CuentaSAPPL();
                    var cuentaInfo = new CuentaSAPInfo()
                    {
                        CuentaSAP = skAyudaCuentaSAP.Clave
                    };
                    if ((int)CboTipoCompra.SelectedValue != TipoContratoEnum.BodegaNormal.GetHashCode())
                    {
                        cuentaInfo = cuentaSappl.ObtenerPorCuentaSAP(cuentaInfo);
                    }
                    //Generar Folio
                    contratoGlobal = new ContratoInfo
                    {
                        Organizacion = new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave), Descripcion = skAyudaOrganizacion.Descripcion },
                        Producto = new ProductoInfo() { ProductoId = Convert.ToInt32(skAyudaProducto.Clave), Descripcion = skAyudaProducto.Descripcion, ProductoDescripcion = skAyudaProducto.Descripcion },
                        TipoContrato = (TipoContratoInfo)CboTipoCompra.SelectedItem,
                        TipoFlete = (TipoFleteInfo)CboFlete.SelectedItem,
                        Cuenta = cuentaInfo,
                        Proveedor = proveedorInfo,
                        TipoCambio = tipoCambio,
                        Cantidad = cantidad,
                        Precio = precio,
                        Merma = TxtMermaPermitida.Value.HasValue ? TxtMermaPermitida.Value.Value : 0,
                        PesoNegociar = CboPesoNegociado.Text,
                        Tolerancia = TxtTolerancia.Value.HasValue ? TxtTolerancia.Value.Value : 0,
                        Activo = EstatusEnum.Activo,
                        UsuarioCreacionId = usuario,
                        //Guardado = contratoGuardado,
                        ListaContratoDetalleInfo = listaContratosDetalle,
                        UsuarioModificacionId = usuario,
                        FechaVigencia = DateTime.Today.AddMonths(3)
                    };

                    //Compra
                    if (RbCompraParcial.IsChecked == true)
                    {
                        contratoGlobal.Parcial = CompraParcialEnum.Parcial;
                    }
                    if (RbCompraTotal.IsChecked == true)
                    {
                        contratoGlobal.Parcial = CompraParcialEnum.Total;
                    }
                }

                //Convertir parcialidades a la moneda y cantidad en kilos
                if (RbCompraParcial.IsChecked == true)
                {
                    foreach (var contratoParcialInfo in listaContratoParcial.Where(contratoParcialInfo => !contratoParcialInfo.Guardado))
                    {
                        if (CboMoneda.Text == Properties.Resources.CrearContrato_DescripcionDolar)
                        {
                            contratoParcialInfo.Importe = contratoParcialInfo.Importe / 1000;
                            contratoParcialInfo.Importe = contratoParcialInfo.Importe * tipoCambio.Cambio;
                        }
                        else
                        {
                            contratoParcialInfo.Importe = contratoParcialInfo.Importe / 1000;
                        }
                        contratoParcialInfo.Cantidad = contratoParcialInfo.Cantidad * 1000;
                        contratoParcialInfo.TipoCambio = tipoCambio;
                    }
                }

                contratoGlobal.Fecha = DateTime.Today;
                contratoGlobal.CantidadToneladas = TxtToneladas.Value.HasValue ? TxtToneladas.Value.Value : 0;
                contratoGlobal.PrecioToneladas = TxtPrecio.Value.HasValue ? TxtPrecio.Value.Value : 0;

                contratoGlobal.UsuarioCreacionId = usuario;
                contratoGlobal.UsuarioModificacionId = usuario;

                contratoGlobal.ListaContratoParcial = listaContratoParcial;

                contratoGlobal.ListaContratoHumedad = listaContratoHumedad;

                contratoGlobal.Estatus = new EstatusInfo() { EstatusId = EstatusContratoEnum.Activo.GetHashCode() };

                try
                {
                    contratoGlobal.FolioAserca = txtContratoAcerca.Text;
                    contratoGlobal.FolioCobertura = string.IsNullOrEmpty(txtFolioCobertura.Text) ? -1 : Convert.ToInt32(txtFolioCobertura.Text);
                }
                catch (Exception)
                {
                    contratoGlobal.FolioAserca = null;
                    contratoGlobal.FolioCobertura = -1;
                }

                contratoGlobal.CalidadOrigen = calidad;
                contratoGlobal.CostoSecado = chkCostoSecado.IsChecked != null && (bool)chkCostoSecado.IsChecked ? 1 : 0;
                contratoGlobal.AplicaDescuento = chkDescuento.IsChecked != null && (bool)chkDescuento.IsChecked ? 1 : 0;


                var crearContratoPl = new CrearContratoPL();
                contratoGlobal = crearContratoPl.Guardar(contratoGlobal, listaOtrosCostos);
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
            return true;
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
        /// Envia un evento tab
        /// </summary>
        /// <param name="key"></param>
        public static void Send(Key key)
        {
            if (Keyboard.PrimaryDevice != null)
            {
                if (Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                    {
                        RoutedEvent = Keyboard.KeyDownEvent
                    };
                    InputManager.Current.ProcessInput(e);
                }
            }
        }
        #endregion

        /// <summary>
        /// Al checar compra parcial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbCompraParcial_OnChecked(object sender, RoutedEventArgs e)
        {
            //
            RbCompraTotal.IsChecked = false;
            BtnCapturarCompra.IsEnabled = true;
        }

        /// <summary>
        /// Al checar compra total
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbCompraTotal_OnChecked(object sender, RoutedEventArgs e)
        {
            //
            RbCompraParcial.IsChecked = false;
            BtnCapturarCompra.IsEnabled = false;
        }

        /// <summary>
        /// Se abre pantalla registrar compra parcial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCapturarCompra_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultadoValidacion = ValidarCamposCapturarCompra();
                if (resultadoValidacion.Resultado)
                {
                    var toneladas = TxtToneladas.Value.HasValue ? TxtToneladas.Value.Value : 0;
                    var precio = TxtPrecio.Value.HasValue ? TxtPrecio.Value.Value : 0;
                    var tolerancia = TxtTolerancia.Value.HasValue ? TxtTolerancia.Value.Value : 0;
                    var registrarCompraParcial = new RegistrarCompraParcial(listaContratoParcial, contratoGlobal, nuevoContrato, toneladas, precio, tolerancia, (int)CboTipoCompra.SelectedValue);
                    MostrarCentrado(registrarCompraParcial);
                    listaContratoParcial = registrarCompraParcial.listaContratoParcial;
                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje) ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Se validan los campos toneladas, precio y tolerancia
        /// </summary>
        /// <returns></returns>
        public ResultadoValidacion ValidarCamposCapturarCompra()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(TxtToneladas.Text))
            {
                TxtToneladas.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_CapturarValidacionToneladas;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtPrecio.Text))
            {
                TxtPrecio.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_CapturarValidacionPrecio;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtTolerancia.Text))
            {
                TxtTolerancia.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_CapturarValidacionTolerancia;
                return resultado;
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Metodo que abre la ventana otros costos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOtrosCostos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultadoValidacion = ValidarCamposCapturarOtrosCostos();
                if (resultadoValidacion.Resultado)
                {
                    var toneladas = TxtToneladas.Value.HasValue ? TxtToneladas.Value.Value : 0;
                    var tolerancia = TxtTolerancia.Value.HasValue ? TxtTolerancia.Value.Value : 0;
                    var crearContrato = new OtrosCostos(toneladas, listaOtrosCostos, contratoGlobal, nuevoContrato, tolerancia);
                    MostrarCentrado(crearContrato);
                    listaOtrosCostos = crearContrato.listaCostos;
                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje) ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Se validan los campos toneladas y tolerancia
        /// </summary>
        /// <returns></returns>
        public ResultadoValidacion ValidarCamposCapturarOtrosCostos()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(TxtToneladas.Text))
            {
                TxtToneladas.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_CapturarValidacionToneladas;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtTolerancia.Text))
            {
                TxtTolerancia.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_CapturarValidacionTolerancia;
                return resultado;
            }

            if (RbCompraParcial.IsChecked == true)
            {
                if (listaContratoParcial == null || listaContratoParcial.Count == 0)
                {
                    BtnCapturarCompra.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionOtrosCostos;
                    return resultado;
                }
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Evento click de cerrar contrato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCerrarContrato_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_ConfirmacionCerrarContrato,
                    MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    var contratoPl = new ContratoPL();

                    //Quitar cuando se modifique el obtener por pag
                    contratoGlobal.Estatus = new EstatusInfo() { EstatusId = EstatusContratoEnum.Cerrado.GetHashCode() };
                    contratoGlobal.UsuarioModificacionId = usuario;
                    contratoPl.ActualizarEstado(contratoGlobal, EstatusEnum.Inactivo);

                    decimal toneladasRecibidas = 0;
                    decimal toneladasPendientes = 0;

                    if (contratoGlobal.TipoContrato.TipoContratoId != TipoContratoEnum.BodegaNormal.GetHashCode())
                    {
                        var almacenMovimientoDetallePl = new AlmacenMovimientoDetallePL();
                        var listaAlmacenMovimiento =
                            almacenMovimientoDetallePl.ObtenerAlmacenMovimientoDetallePorContratoId(
                                new AlmacenMovimientoDetalle() { ContratoId = contratoGlobal.ContratoId });

                        if (listaAlmacenMovimiento != null)
                        {
                            toneladasRecibidas =
                                    listaAlmacenMovimiento.Sum(almacenMovimientoInfo => almacenMovimientoInfo.Cantidad / 1000);
                            toneladasPendientes = ((contratoGlobal.Cantidad / 1000)) - toneladasRecibidas;
                        }
                    }
                    else
                    {
                        toneladasRecibidas = contratoGlobal.Cantidad / 1000;
                    }

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format("{0} {1} {2} {3} {4} {5}",
                            Properties.Resources.CrearContrato_LblFolioCerrar.Trim(),
                            contratoGlobal.Folio.ToString(CultureInfo.InvariantCulture).Trim(),
                            Properties.Resources.CrearContrato_LblToneladasRecibidas.Trim(),
                            toneladasRecibidas.ToString("N3"),
                            Properties.Resources.CrearContrato_LblToneladasPendientes.Trim(),
                            toneladasPendientes.ToString("N3")),
                            MessageBoxButton.OK, MessageImage.Warning);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_ContratoCerradoExito, MessageBoxButton.OK,
                    MessageImage.Correct);
                    Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearContrato_MensajeErrorCerrar,
                MessageBoxButton.OK,
                MessageImage.Error);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHumedad_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var crearContrato = new RegistrarHumedad(listaContratoHumedad);
                MostrarCentrado(crearContrato);
                listaContratoHumedad = crearContrato.ListaContratoHumedad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #region eventos
        /// <summary>
        /// Evento check del calidad origen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbCalidadOrigen_OnChecked(object sender, RoutedEventArgs e)
        {
            calidad = 1;
        }

        /// <summary>
        /// Evento check de a calidad destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbCalidadDestino_OnChecked(object sender, RoutedEventArgs e)
        {
            calidad = 0;
        }

        #endregion

        private void BtnActualizar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultadoValidacion = ValidarCamposActualizar();
                if (resultadoValidacion.Resultado)
                {
                    var contratoPL = new ContratoPL();
                    contratoGlobal.Tolerancia = TxtTolerancia.Value.HasValue ? TxtTolerancia.Value.Value : 0;
                    decimal cantidad = TxtToneladas.Value.HasValue ? TxtToneladas.Value.Value : 0;
                    contratoGlobal.Cantidad = cantidad*1000;
                    contratoGlobal.PesoNegociar = CboPesoNegociado.Text;
                    contratoGlobal.FolioAserca = txtContratoAcerca.Text;
                    contratoGlobal.FolioCobertura = Convert.ToInt32(txtFolioCobertura.Value.HasValue ? txtFolioCobertura.Value.Value : 0);
                    contratoGlobal.CalidadOrigen = calidad;
                    contratoGlobal.AplicaDescuento = chkDescuento.IsChecked != null && (bool)chkDescuento.IsChecked ? 1 : 0;
                    contratoGlobal.TipoFlete = (TipoFleteInfo)CboFlete.SelectedItem;
                    contratoGlobal.Estatus.EstatusId = EstatusContratoEnum.Activo.GetHashCode();
                    contratoGlobal.UsuarioModificacionId = Auxiliar.AuxConfiguracion.ObtenerUsuarioLogueado();
                    

                    contratoPL.ActualizarContrato(contratoGlobal, EstatusEnum.Activo);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CrearContrato_DatosGuardadosExito,
                                      MessageBoxButton.OK, MessageImage.Correct);
                    Close();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     resultadoValidacion.Mensaje,
                                     MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CrearContrato_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Metodo que valida los elementos obligatorios
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCamposActualizar()
        {
            var resultado = new ResultadoValidacion();
            resultado.Resultado = true;

            if (String.IsNullOrEmpty(TxtToneladas.Text))
            {
                TxtToneladas.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionToneladas;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtTolerancia.Text))
            {
                TxtTolerancia.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_ValidacionTolerancia;
                return resultado;
            }
            if ((String.IsNullOrEmpty(CboPesoNegociado.Text) || CboPesoNegociado.Text == Properties.Resources.AdministrarContrato_Seleccione))
            {
                CboPesoNegociado.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionPesoNegociado;
                return resultado;
            }
            if ((String.IsNullOrEmpty(CboFlete.Text) || (int)CboFlete.SelectedValue == 0))
            {
                CboFlete.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_MensajeValidacionTipoFlete;
                return resultado;
            }
            return resultado;
        }
    }
}
