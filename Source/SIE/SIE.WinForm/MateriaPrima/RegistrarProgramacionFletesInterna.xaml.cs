using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Catalogos;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;
using TipoMovimiento = SIE.Services.Info.Enums.TipoMovimiento;
using TipoOrganizacion = SIE.Services.Info.Enums.TipoOrganizacion;
using Window = System.Windows.Window;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para RegistrarProgramacionFletesInterna.xaml
    /// </summary>
    public partial class RegistrarProgramacionFletesInterna
    {

        #region Propiedades
        private SKAyuda<ProductoInfo> skAyudaProductos;
        private SKAyuda<AlmacenInfo> skAyudaAlmacenSalida;
        private SKAyuda<OrganizacionInfo> skAyudaDestino;
        private SKAyuda<ProveedorInfo> skAyudaProveedores;
        private int usuarioID;
        private int organizacionID;
        private AlmacenInfo almacenSalida  = new AlmacenInfo();
        private ProductoInfo productoInfo = new ProductoInfo();
        private FleteInternoInfo fleteInternoInfo;
        private bool nuevoGlobal;
        public List<FleteInternoDetalleInfo> ListaFleteInternoDetalle = new List<FleteInternoDetalleInfo>();
        public List<FleteInternoDetalleInfo> ListaFleteInternoDetalleFiltrados = new List<FleteInternoDetalleInfo>();
        public List<FleteInternoCostoInfo> ListaFleteInternoCosto = new List<FleteInternoCostoInfo>();
        private FleteMermaPermitidaInfo fleteMermaPermitidaInfo;
        private FleteInternoDetalleInfo fleteInternoDetalleInfo;
        private string almacenSalidaAnterior = "";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de la clase para flete interno nuevo
        /// </summary>
        /// <param name="nuevo"></param>
        public RegistrarProgramacionFletesInterna(bool nuevo)
        {
            InitializeComponent();
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            CargarAyudas();
            nuevoGlobal = nuevo;
            DefinirPantallaFleteInternoNuevo();
            ValidarProveedoresFletes();
            ValidarCostosTipoFlete();
        }

        /// <summary>
        /// Constructor de la clase para flete interno previamente creado
        /// </summary>
        public RegistrarProgramacionFletesInterna(FleteInternoInfo fleteInternoInfoP)
        {
            InitializeComponent();
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            CargarAyudas();
            fleteInternoInfo = fleteInternoInfoP;
            DefinirPantallaFleteInternoCreado();
            ValidarProveedoresFletes();
            ValidarCostosTipoFlete();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Txt no permite caracters especiales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtObservaciones_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConPunto(e.Text);
        }

        /// <summary>
        /// Agregar detalle al grid
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
                        foreach (var fleteInternoDetalleInfoP in ListaFleteInternoDetalle.Where(fleteInternoDetalleInfoP => fleteInternoDetalleInfoP.Proveedor.CodigoSAP ==
                                                                                                                                             (skAyudaProveedores.Clave)))
                        {
                            fleteInternoDetalleInfoP.MermaPermitida = Convert.ToDecimal(TxtMermaPermitida.Value);
                            fleteInternoDetalleInfoP.MermaPermitidaDescripcion = TxtMermaPermitida.Text;
                            if (fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Granos.GetHashCode() ||
                                fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Harinas.GetHashCode())
                            {
                                fleteInternoDetalleInfo.MermaPermitidaDescripcion =
                                    fleteInternoDetalleInfo.MermaPermitida.ToString("N3");
                            }
                            else
                            {
                                fleteInternoDetalleInfo.MermaPermitidaDescripcion = String.Empty;
                            }

                            fleteInternoDetalleInfoP.Observaciones = TxtObservaciones.Text;
                            fleteInternoDetalleInfoP.ListadoFleteInternoCosto.Clear();
                            fleteInternoDetalleInfoP.ListadoFleteInternoCosto.AddRange(ListaFleteInternoCosto);
                            if (fleteInternoDetalleInfoP.Guardado)
                            {
                                fleteInternoDetalleInfoP.Modificado = true;
                                fleteInternoDetalleInfoP.UsuarioModificacionId = usuarioID;
                                fleteInternoDetalleInfoP.TipoTarifaID = int.Parse(CboTipoFlete.SelectedValue.ToString());
                                fleteInternoDetalleInfoP.TipoTarifa = CboTipoFlete.Text.ToString();

                            }
                        }
                        GridFleteInternoDetalle.ItemsSource = null;
                        GridFleteInternoDetalle.ItemsSource = ListaFleteInternoDetalle.Where(registro => !registro.Eliminado).ToList();
                        BtnAgregar.Content = Properties.Resources.DiferenciasDeInventario_BtnAgregar;
                        LimpiarSeccionProveedores();
                        skAyudaProveedores.IsEnabled = true;
                        skAyudaProveedores.AsignarFoco();
                    }
                    else
                    {
                        if (fleteInternoInfo == null)
                        {
                            int tipoMovimiento = CboTipoMovimiento.SelectedIndex == 1 ? TipoMovimiento.PaseProceso.GetHashCode() : TipoMovimiento.SalidaPorTraspaso.GetHashCode();
                            fleteInternoInfo = new FleteInternoInfo()
                            {
                                Organizacion = new OrganizacionInfo(){OrganizacionID = organizacionID},
                                TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = tipoMovimiento },
                                AlmacenOrigen = new AlmacenInfo() { AlmacenID = Convert.ToInt32(skAyudaAlmacenSalida.Clave) },
                                Producto = new ProductoInfo() { ProductoId = Convert.ToInt32(skAyudaProductos.Clave), SubfamiliaId = skAyudaProductos.Info.SubFamilia.SubFamiliaID},
                                Activo = EstatusEnum.Activo,
                                UsuarioCreacionId = usuarioID
                            };
                        }
                        AgregarFleteInternoDetalle();
                        DeshabilitarControlesFleteInterno();
                    }
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
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorAgregarFleteInterno, MessageBoxButton.OK,
                    MessageImage.Error);
                BtnGuardar.Focus();
            }
        }

        /// <summary>
        /// Se ejecuta al cargar la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrarProgramacionFletesInterna_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (nuevoGlobal)
                {
                    CboTipoMovimiento.Focus();
                }
                else
                {
                    skAyudaProveedores.AsignarFoco();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

            }
        }

        /// <summary>
        /// Asignar movimiento interno cuando el tipo movimiento sea pase a proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTipoMovimiento_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!nuevoGlobal) return;
                LimpiarSeccionProveedores();
                var existeAlmacenMateriasPrimas = false;
                skAyudaAlmacenSalida.LimpiarCampos();
                skAyudaDestino.LimpiarCampos();
                skAyudaProductos.LimpiarCampos();
                TxtMermaPermitida.Text = "";

                //Obtener almacen materias prima default
                if (CboTipoMovimiento.SelectedIndex != 0)
                {
                    skAyudaProductos.Info.AlmacenID = 0;
                    skAyudaProductos.Info.ProductoId = 0;
                    //NAKADA
                    var almacenPl = new AlmacenPL();
                    var listadoAlmacenes = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionID);
                    if (listadoAlmacenes != null)
                    {
                        foreach (
                            var almacenInfo in
                                listadoAlmacenes.Where(
                                    almacenInfo =>
                                        almacenInfo.TipoAlmacenID == TipoAlmacenEnum.MateriasPrimas.GetHashCode())
                            )
                        {
                            skAyudaAlmacenSalida.Clave = almacenInfo.AlmacenID.ToString(CultureInfo.InvariantCulture);
                            skAyudaAlmacenSalida.Descripcion = almacenInfo.Descripcion;
                            productoInfo.AlmacenID = almacenInfo.AlmacenID;
                            existeAlmacenMateriasPrimas = true;
                            break;
                        }
                        if (!existeAlmacenMateriasPrimas)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources
                                    .RegistrarProgramacionFletesInterna_MensajeValidacionAlmacenMateriasPrimas,
                                MessageBoxButton.OK, MessageImage.Stop);
                        }
                        else
                        {
                            //Verificar si hay productos dados de alta
                            var productos = almacenPl.ObtenerProductosAlamcen(Convert.ToInt32(skAyudaAlmacenSalida.Clave), organizacionID);
                            if (productos == null)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources
                                        .RegistrarProgramacionFletesInterna_MensajeValidacionAlmacenNoTieneProductosParte1 +
                                    skAyudaAlmacenSalida.Descripcion +
                                    Properties.Resources
                                        .RegistrarProgramacionFletesInterna_MensajeValidacionAlmacenNoTieneProductosParte2
                                    , MessageBoxButton.OK,
                                    MessageImage.Stop);
                                skAyudaProductos.LimpiarCampos();
                                skAyudaAlmacenSalida.LimpiarCampos();
                            }
                            else
                            {
                                almacenSalida = almacenPl.ObtenerPorID(Convert.ToInt32(skAyudaAlmacenSalida.Clave));
                                productoInfo.AlmacenID = almacenSalida.AlmacenID;
                                skAyudaProductos.AsignarFoco();
                            }
                        }
                    }
                }
                else
                {
                    skAyudaProductos.LimpiarCampos();
                    skAyudaProductos.Info.AlmacenID = 0;
                    skAyudaProductos.Info.ProductoId = 0;
                    skAyudaAlmacenSalida.LimpiarCampos();
                }

                if (CboTipoMovimiento.SelectedIndex == 1)
                {
                    skAyudaDestino.LimpiarCampos();
                    skAyudaDestino.Descripcion =
                        Properties.Resources.RegistrarProgramacionFletesInterna_LblMovimientoInterno;
                    skAyudaDestino.IsEnabled = false;
                }
                else
                {
                    skAyudaDestino.LimpiarCampos();
                    skAyudaDestino.IsEnabled = true;
                }

                //Verifica configuracion para flete interno
                VerificarConfiguracionFleteInterno();
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorSeleccionarTipoMovimiento, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.RegistrarProgramacionFletesInterna_MensajeConfirmacionCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        /// <summary>
        /// Cancelar flete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelarFlete_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeConfirmacionCancelarFlete,
                    MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    var registrarProgramacionFletesInternaPl = new RegistrarProgramacionFletesInternaPL();
                    registrarProgramacionFletesInternaPl.CancelarFlete(fleteInternoInfo, usuarioID);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeFleteCancelado,
                    MessageBoxButton.OK, MessageImage.Correct);
                    Close();
                }
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorCancelarFlete, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Guarda el flete y los detalles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultadoValidacion = ValidarGuardar();
                if (resultadoValidacion.Resultado)
                {
                    var registrarProgramacionFletesInternaPl = new RegistrarProgramacionFletesInternaPL();
                    if (fleteInternoInfo != null)
                    {
                        fleteInternoInfo.ListadoFleteInternoDetalle = new List<FleteInternoDetalleInfo>();
                        fleteInternoInfo.ListadoFleteInternoDetalle.AddRange(ListaFleteInternoDetalle);
                    }
                    registrarProgramacionFletesInternaPl.Guardar(fleteInternoInfo, usuarioID);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        nuevoGlobal
                            ? Properties.Resources.CrearContrato_DatosGuardadosExito
                            : Properties.Resources.RegistrarProgramacionFletesInterna_MensajeActualizadoCorrectamente,
                        MessageBoxButton.OK, MessageImage.Correct);
                    Close();
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
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorGuardar, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Cambia al siguiente elemento al dar enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrarProgramacionFletesInterna_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Abre la opcion costos de fletes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCostos_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CboTipoFlete.Text.ToString().Equals(Properties.Resources.RegistrarProgramacionFletesInterna_Seleccione))
                {
                    var registrarProgramacionFleteInternaCostoFletes = new RegistrarProgramacionFletesInternaCostoDeFletes(ListaFleteInternoCosto);
                    registrarProgramacionFleteInternaCostoFletes.Cadena = CboTipoFlete.Text.ToString();
                    MostrarCentrado(registrarProgramacionFleteInternaCostoFletes);
                    ListaFleteInternoCosto.Clear();
                    ListaFleteInternoCosto.AddRange(registrarProgramacionFleteInternaCostoFletes.ListadoFleteInternoCostoPrincipal);
                }
                else
                {
                    var mensaje = "";
                    mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_SeleccioneFleteCostos;
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
        /// Editar un flete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                fleteInternoDetalleInfo = (FleteInternoDetalleInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                BtnAgregar.Content = Properties.Resources.OtrosCostos_MensajeCosto;

                if (fleteInternoDetalleInfo == null) return;
                skAyudaProveedores.IsEnabled = false;
                skAyudaProveedores.Clave = fleteInternoDetalleInfo.Proveedor.CodigoSAP;
                skAyudaProveedores.Descripcion = fleteInternoDetalleInfo.Proveedor.Descripcion;
                TxtMermaPermitida.Text = fleteInternoDetalleInfo.MermaPermitida.ToString(CultureInfo.InvariantCulture);
                TxtObservaciones.Text = fleteInternoDetalleInfo.Observaciones;
                ListaFleteInternoCosto.Clear();
                ListaFleteInternoCosto.AddRange(fleteInternoDetalleInfo.ListadoFleteInternoCosto);

                //Validar que la merma permitida este configurada
                if (fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Granos.GetHashCode() ||
                    fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Harinas.GetHashCode())
                {
                    var organizacion = CboTipoMovimiento.SelectedIndex == 1
                        ? organizacionID
                        : Convert.ToInt32(skAyudaDestino.Clave);
                    fleteMermaPermitidaInfo = new FleteMermaPermitidaInfo()
                    {
                        Organizacion = new OrganizacionInfo() {OrganizacionID = organizacion},
                        SubFamilia = new SubFamiliaInfo() { SubFamiliaID = fleteInternoInfo.Producto.SubfamiliaId },
                        Activo = EstatusEnum.Activo
                    };
                    var fleteMermaPermitidaPl = new FleteMermaPermitidaPL();
                    fleteMermaPermitidaInfo = fleteMermaPermitidaPl.ObtenerConfiguracion(fleteMermaPermitidaInfo);
                    if (fleteMermaPermitidaInfo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeMermaPermitidaNoConfigurada
                            , MessageBoxButton.OK,
                            MessageImage.Stop);
                        skAyudaProveedores.LimpiarCampos();
                    }
                    else
                    {
                        TxtObservaciones.IsEnabled = true;
                        TxtMermaPermitida.IsEnabled = true;
                        BtnCostos.IsEnabled = true;
                        TxtMermaPermitida.Focus();
                    }
                }
                else
                {
                    TxtMermaPermitida.Text = String.Empty;
                    TxtObservaciones.Text = String.Empty;
                    TxtMermaPermitida.IsEnabled = false;
                    TxtObservaciones.IsEnabled = false;
                    BtnCostos.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorEditar
                    , MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Elimina un detalle del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEliminar = (Button)e.Source;
            try
            {
                fleteInternoDetalleInfo = (FleteInternoDetalleInfo)Extensor.ClonarInfo(botonEliminar.CommandParameter);
                if (fleteInternoDetalleInfo == null) return;

                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.RegistrarProgramacionFletesInterna_MensajeConfirmacionEliminarProveedor1 +
                   fleteInternoDetalleInfo.Proveedor.Descripcion + Properties.Resources.RegistrarProgramacionFletesInterna_MensajeConfirmacionEliminarProveedor2,
                   MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    foreach (var fleteInternoDetalleP in ListaFleteInternoDetalle.Where(fleteInternoDetalleP => fleteInternoDetalleP.Proveedor.CodigoSAP ==
                                                                                                                                         fleteInternoDetalleInfo.Proveedor.CodigoSAP))
                    {
                        fleteInternoDetalleP.Eliminado = true;
                        fleteInternoDetalleP.UsuarioModificacionId = usuarioID;
                        fleteInternoDetalleP.Activo = EstatusEnum.Inactivo;
                    }

                    //Filtrar lista para quitar eliminados que no han sido guardados
                    var listaTemp = ListaFleteInternoDetalle.Where(fleteInternoDetalleP => (fleteInternoDetalleP.Guardado) || (!fleteInternoDetalleP.Guardado && !fleteInternoDetalleP.Eliminado)).ToList();
                    ListaFleteInternoDetalle.Clear();
                    ListaFleteInternoDetalle.AddRange(listaTemp);
                    ListaFleteInternoDetalleFiltrados.Clear();
                    ListaFleteInternoDetalleFiltrados = ListaFleteInternoDetalle.Where(fleteInternoDetalleP => !fleteInternoDetalleP.Eliminado).ToList();
                    GridFleteInternoDetalle.ItemsSource = null;
                    GridFleteInternoDetalle.ItemsSource = ListaFleteInternoDetalleFiltrados;
                    LimpiarSeccionProveedores();
                    skAyudaProveedores.IsEnabled = true;
                    BtnAgregar.Content = Properties.Resources.RegistrarProgramacionFletesInterna_BtnAgregar;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarProgramacionFletesInterna_MensajeProveedorEliminadoCorrectamente,
                        MessageBoxButton.OK, MessageImage.Correct);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorEliminar
                    , MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Validacion antes de agregar el ajuste al grid
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarGuardar()
        {
                var resultado = new ResultadoValidacion();

                //Verificar si tiene detalles agregados
                if (GridFleteInternoDetalle.Items.Count == 0)
                {
                    BtnGuardar.Focus();
                    resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeConfirmacionGuardado;
                    resultado.Resultado = false;
                    return resultado;
                }

                if (nuevoGlobal)
                {
                    var organizacionIdTemp = CboTipoMovimiento.SelectedIndex == 1 ? organizacionID : Convert.ToInt32(skAyudaDestino.Clave);
                    //Validar que existan almacenes configurados para el destino
                    bool almacenEncontrado = false;
                    var almacenPl = new AlmacenPL();
                    var organizacionPl = new OrganizacionPL();
                    var almacenesLocales = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionIdTemp);
                    //Obtener almacen para flete interno info
                    if (fleteInternoInfo.TipoMovimiento.TipoMovimientoID == TipoMovimiento.PaseProceso.GetHashCode())
                    {
                        //NAKADA
                        foreach (
                            var almacenInfoP in
                                almacenesLocales.Where(
                                    almacenInfoP => almacenInfoP.TipoAlmacenID == TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode()))
                        {
                            fleteInternoInfo.AlmacenDestino = new AlmacenInfo() {AlmacenID = almacenInfoP.AlmacenID};
                            almacenEncontrado = true;
                            break;
                        }
                        if (!almacenEncontrado)
                        {
                            //Almacen planta de alimentos no encontrado
                            BtnGuardar.Focus();
                            resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeAlmacenPlantaAlimentosNoConfigurado;
                            resultado.Resultado = false;
                            return resultado;
                        }
                    }
                    else
                    {
                        var almacenesDestino = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionIdTemp);
                        if (almacenesDestino != null)
                        {
                            var organizacionInfo =
                                organizacionPl.ObtenerPorID(Convert.ToInt32(skAyudaDestino.Clave));
                            if (organizacionInfo.TipoOrganizacion.TipoOrganizacionID ==
                                TipoOrganizacion.Ganadera.GetHashCode())
                            {
                                //NAKADA
                                foreach (
                                    var almacenInfoP in
                                        almacenesDestino.Where(
                                            almacenInfoP =>
                                                almacenInfoP.TipoAlmacenID ==
                                                TipoAlmacenEnum.MateriasPrimas.GetHashCode()))
                                {
                                    fleteInternoInfo.AlmacenDestino = new AlmacenInfo()
                                    {
                                        AlmacenID = almacenInfoP.AlmacenID
                                    };
                                    almacenEncontrado = true;
                                    break;
                                }
                                if (!almacenEncontrado)
                                {
                                    //Almacen materias primas no encontrado
                                    BtnGuardar.Focus();
                                    resultado.Mensaje =
                                        Properties.Resources.RegistrarProgramacionFletesInterna_MensajeAlmacenMateriasPrimasNoConfigurado;
                                    resultado.Resultado = false;
                                    return resultado;
                                }
                            }
                            else
                            {
                                //NAKADA
                                foreach (
                                    var almacenInfoP in
                                        almacenesDestino.Where(
                                            almacenInfoP =>
                                                almacenInfoP.TipoAlmacenID == TipoAlmacenEnum.CentroAcopio.GetHashCode())
                                    )
                                {
                                    fleteInternoInfo.AlmacenDestino = new AlmacenInfo()
                                    {
                                        AlmacenID = almacenInfoP.AlmacenID
                                    };
                                    almacenEncontrado = true;
                                    break;
                                }
                                if (!almacenEncontrado)
                                {
                                    //Almacen centro de acopio no encontrado
                                    BtnGuardar.Focus();
                                    resultado.Mensaje =
                                        Properties.Resources.RegistrarProgramacionFletesInterna_MensajeAlmacenCentroAcopioNoConfigurado;
                                    resultado.Resultado = false;
                                    return resultado;
                                }
                            }
                        }
                        else
                        {
                            BtnGuardar.Focus();
                            resultado.Mensaje =
                                Properties.Resources.RegistrarProgramacionFletesInterna_MensajeOrganizacionSinConfiguracionDeAlmacenes;
                            resultado.Resultado = false;
                            return resultado;
                        }
                    }
                }

                //Verificar si tiene detalles agregados
                if ((string)BtnAgregar.Content == Properties.Resources.OtrosCostos_MensajeCosto)
                {
                    BtnAgregar.Focus();
                    resultado.Mensaje =
                        Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDatosPorActualizarParte1 + skAyudaProveedores.Descripcion
                        + Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDatosPorActualizarParte2;
                    resultado.Resultado = false;
                    return resultado;
                }

                resultado.Resultado = true;
                return resultado;
        }

        /// <summary>
        /// Valida que existan proveedores fleteros
        /// </summary>
        private void ValidarProveedoresFletes()
        {
            try
            {
            var proveedorPl = new ProveedorPL();
            var listaProveedores = proveedorPl.ObtenerPorTipoProveedorID(EstatusEnum.Activo.GetHashCode(),
                TipoProveedorEnum.ProveedoresFletes.GetHashCode());
            if (listaProveedores == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionProveedorFletes
                , MessageBoxButton.OK,
                MessageImage.Stop);
                DeshabilitarControlesGlobal();
            }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorValidarProveedoresFlete
                    , MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida que existan costos tipo flete
        /// </summary>
        private void ValidarCostosTipoFlete()
        {
            try{
            //REVISAR
            var costoPl = new CostoPL();
            var listadoCostos = costoPl.ObtenerPorPaginaTipoCosto(new PaginacionInfo(){Inicio = 1, Limite = 5}, new CostoInfo(){ListaTipoCostos = new List<TipoCostoInfo>(){new TipoCostoInfo(){TipoCostoID = TipoCostoEnum.Flete.GetHashCode()}}});
            if (listadoCostos == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionCostosFletes
                , MessageBoxButton.OK,
                MessageImage.Stop);
                DeshabilitarControlesGlobal();
            }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorValidarCostosFletes
                    , MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Deshabilitar toda la pantalla
        /// </summary>
        private void DeshabilitarControlesGlobal()
        {
            CboTipoMovimiento.IsEnabled = false;
            skAyudaAlmacenSalida.IsEnabled = false;
            skAyudaProductos.IsEnabled = false;
            skAyudaDestino.IsEnabled = false;
            skAyudaProveedores.IsEnabled = false;
            TxtMermaPermitida.IsEnabled = false;
            TxtObservaciones.IsEnabled = false;
            BtnAgregar.IsEnabled = false;
            BtnCostos.IsEnabled = false;
            GridFleteInternoDetalle.IsEnabled = false;
            BtnGuardar.IsEnabled = false;
            BtnCancelarFlete.IsEnabled = false;
        }

        /// <summary>
        /// Define la pantalla cuando el flete interno es nuevo
        /// </summary>
        private void DefinirPantallaFleteInternoNuevo()
        {
            BtnCancelarFlete.IsEnabled = !nuevoGlobal;
            BtnCostos.IsEnabled = false;
        }

        /// <summary>
        /// Define la pantalla cuando el flete interno esta creado
        /// </summary>
        private void DefinirPantallaFleteInternoCreado()
        {
            try
            {
                UcTitulo.TextoTitulo = Properties.Resources.RegistrarProgramacionFletesInterna_TituloActualizar;
                BtnCostos.IsEnabled = false;
                CboTipoMovimiento.IsEnabled = false;
                skAyudaProductos.IsEnabled = false;
                skAyudaAlmacenSalida.IsEnabled = false;
                skAyudaDestino.IsEnabled = false;
                if (fleteInternoInfo.TipoMovimiento.TipoMovimientoID == TipoMovimiento.PaseProceso.GetHashCode())
                {
                    CboTipoMovimiento.SelectedIndex = 1;
                    skAyudaDestino.Descripcion =
                        Properties.Resources.RegistrarProgramacionFletesInterna_LblMovimientoInterno;
                }
                else
                {
                    CboTipoMovimiento.SelectedIndex = 2;
                    skAyudaDestino.Clave =
                        fleteInternoInfo.OrganizacionDestino.OrganizacionID.ToString(CultureInfo.InvariantCulture);
                    skAyudaDestino.Descripcion = fleteInternoInfo.OrganizacionDestino.Descripcion;
                }
                skAyudaProductos.Clave = fleteInternoInfo.Producto.ProductoId.ToString(CultureInfo.InvariantCulture);
                skAyudaProductos.Descripcion = fleteInternoInfo.Producto.Descripcion;

                skAyudaAlmacenSalida.Clave =
                    fleteInternoInfo.AlmacenOrigen.AlmacenID.ToString(CultureInfo.InvariantCulture);
                skAyudaAlmacenSalida.Descripcion = fleteInternoInfo.AlmacenOrigen.Descripcion;

                //Obtener detalles y llenar grid
                var fleteInternoDetallePl = new FleteInternoDetallePL();
                var listaFleteInternoDetalle = fleteInternoDetallePl.ObtenerPorFleteInternoId(fleteInternoInfo);
                if (listaFleteInternoDetalle != null)
                {
                    foreach (var listaFleteInternoDetInfo in listaFleteInternoDetalle)
                    {
                        foreach (var fleteInternoCosto in listaFleteInternoDetInfo.ListadoFleteInternoCosto)
                        {
                            fleteInternoCosto.Tarifa = fleteInternoCosto.Tarifa;
                        }
                        if (fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Granos.GetHashCode() ||
                            fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Harinas.GetHashCode())
                        {
                            listaFleteInternoDetInfo.MermaPermitidaDescripcion = listaFleteInternoDetInfo.MermaPermitida.ToString("N3");
                        }
                        listaFleteInternoDetInfo.TipoTarifa = listaFleteInternoDetInfo.TipoTarifa;
                    }
                    GridFleteInternoDetalle.ItemsSource = listaFleteInternoDetalle;
                    ListaFleteInternoDetalle = listaFleteInternoDetalle;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeError
                    , MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga las ayudas y el combo tipo movimiento
        /// </summary>
        private void CargarAyudas()
        {
            CargarComboTipoFlete();
            CargarComboTipoMovimiento();
            CargarAyudaAlmacenes();
            CargarAyudaProductos();
            CargarAyudaDestino();
            CargarAyudaProveedor();
        }

        /// <summary>
        /// Metodo que carga los datos en combo ajuste
        /// </summary>
        private void CargarComboTipoFlete()
        {
            try
            {
                List<TipoTarifaInfo> lista = new List<TipoTarifaInfo>();
                var TipoTarifa = new TipoTarifaPL();
                lista = TipoTarifa.ObtenerTodos();
                CboTipoFlete.ItemsSource = lista;
                CboTipoFlete.DisplayMemberPath = "Descripcion";
                CboTipoFlete.SelectedValuePath = "TipoTarifaId";
                CboTipoFlete.SelectedIndex = 0;
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo que carga los datos en combo ajuste
        /// </summary>
        private void CargarComboTipoMovimiento()
        {
            try
            {
                CboTipoMovimiento.Items.Clear();
                CboTipoMovimiento.Items.Insert(0, Properties.Resources.RegistrarProgramacionFletesInterna_Seleccione);
                CboTipoMovimiento.Items.Insert(1, Properties.Resources.RegistrarProgramacionFletesInterna_CboTipoMovimientoPaseProceso);
                CboTipoMovimiento.Items.Insert(2, Properties.Resources.RegistrarProgramacionFletesInterna_CboTipoMovimientoSalidaPorTraspaso);
                CboTipoMovimiento.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Carga ayudas de almacenes
        /// </summary>
        private void CargarAyudaAlmacenes()
        {
            var almacenInfo = new AlmacenInfo
            {
                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                ListaTipoAlmacen = new List<TipoAlmacenInfo>
                        {
                            //Agregar el resto de tipo almacen
                            new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.CentroAcopio },
                            new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.MateriasPrimas },
                            new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.BodegaDeTerceros },
                            new TipoAlmacenInfo { TipoAlmacenID = (int)TipoAlmacenEnum.PlantaDeAlimentos }
                        },
                Activo = EstatusEnum.Activo
            };
            skAyudaAlmacenSalida = new SKAyuda<AlmacenInfo>(200, false, almacenInfo
                                                   , "PropiedadClaveRegistrarProgracionFletesInterna"
                                                   , "PropiedadDescripcionRegistrarProgramacionFletesInterna",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new AlmacenPL(),
                MensajeClaveInexistente = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaAlmacenInvalido,
                MensajeBusquedaCerrar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaAlmacenSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaAlmacenSeleccionar,
                TituloEtiqueta = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaLeyendaAlmacen,
                TituloPantalla = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaAlmacenTitulo,
            };

            skAyudaAlmacenSalida.LlamadaMetodosNoExistenDatos += LimpiarAyudaProductos;
            skAyudaAlmacenSalida.ObtenerDatos += ObtenerDatosAlmacen;
            //skAyudaAlmacenSalida.AsignaTabIndex(0);
            SplAyudaAlmacenSalida.Children.Clear();
            SplAyudaAlmacenSalida.Children.Add(skAyudaAlmacenSalida);
        }

        /// <summary>
        /// Se limpian los productos
        /// </summary>
        private void LimpiarAyudaProductos()
        {
            skAyudaProductos.LimpiarCampos();
            TxtMermaPermitida.Text = "";
            skAyudaProveedores.LimpiarCampos();
            TxtObservaciones.Clear();
            BtnCostos.IsEnabled = false;
            skAyudaProductos.Info.AlmacenID = 0;
            skAyudaProductos.Info.ProductoId = 0;
        }

        /// <summary>
        /// Se ejecuta al seleccionar un elemento en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosAlmacen(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    skAyudaProductos.LimpiarCampos();
                    TxtMermaPermitida.Text = "";
                    return;
                }
                if (skAyudaAlmacenSalida.Info == null)
                {
                    skAyudaProductos.LimpiarCampos();
                    TxtMermaPermitida.Text = "";
                    return;
                }
                if (almacenSalidaAnterior != clave)
                {
                    skAyudaProductos.LimpiarCampos();
                    TxtMermaPermitida.Text = "";
                    if (skAyudaAlmacenSalida.Info != null)
                    {
                        skAyudaAlmacenSalida.Info = new AlmacenInfo
                        {
                            Organizacion = new OrganizacionInfo {OrganizacionID = organizacionID},
                            ListaTipoAlmacen = new List<TipoAlmacenInfo>
                            {
                                //Agregar el resto de tipo almacen
                                new TipoAlmacenInfo {TipoAlmacenID = (int) TipoAlmacenEnum.CentroAcopio},
                                new TipoAlmacenInfo {TipoAlmacenID = (int) TipoAlmacenEnum.MateriasPrimas},
                                new TipoAlmacenInfo {TipoAlmacenID = (int) TipoAlmacenEnum.BodegaDeTerceros},
                                new TipoAlmacenInfo {TipoAlmacenID = (int) TipoAlmacenEnum.PlantaDeAlimentos}
                            },
                            Activo = EstatusEnum.Activo
                        };
                        var almacenPl = new AlmacenPL();
                        //Verificar si hay productos dados de alta
                        var productos = almacenPl.ObtenerProductosAlamcen(Convert.ToInt32(skAyudaAlmacenSalida.Clave),
                            organizacionID);
                        if (productos == null)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources
                                    .RegistrarProgramacionFletesInterna_MensajeValidacionAlmacenNoTieneProductosParte1 +
                                skAyudaAlmacenSalida.Descripcion +
                                Properties.Resources
                                    .RegistrarProgramacionFletesInterna_MensajeValidacionAlmacenNoTieneProductosParte2
                                , MessageBoxButton.OK,
                                MessageImage.Stop);
                            skAyudaProductos.LimpiarCampos();
                            skAyudaProductos.Info.AlmacenID = 0;
                            skAyudaProductos.Info.ProductoId = 0;
                            skAyudaAlmacenSalida.LimpiarCampos();
                            skAyudaAlmacenSalida.AsignarFoco();
                        }
                        else
                        {
                            almacenSalida = almacenPl.ObtenerPorID(Convert.ToInt32(skAyudaAlmacenSalida.Clave));
                            productoInfo.AlmacenID = almacenSalida.AlmacenID;
                            almacenSalidaAnterior = clave;
                        }
                        LimpiarSeccionProveedores();
                    }
                    else
                    {
                        skAyudaAlmacenSalida.Info = new AlmacenInfo
                        {
                            Organizacion = new OrganizacionInfo {OrganizacionID = organizacionID},
                            ListaTipoAlmacen = new List<TipoAlmacenInfo>
                            {
                                //Agregar el resto de tipo almacen
                                new TipoAlmacenInfo {TipoAlmacenID = (int) TipoAlmacenEnum.CentroAcopio},
                                new TipoAlmacenInfo {TipoAlmacenID = (int) TipoAlmacenEnum.MateriasPrimas},
                                new TipoAlmacenInfo {TipoAlmacenID = (int) TipoAlmacenEnum.BodegaDeTerceros},
                                new TipoAlmacenInfo {TipoAlmacenID = (int) TipoAlmacenEnum.PlantaDeAlimentos}
                            },
                            Activo = EstatusEnum.Activo
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorAyudaAlmacen
                    , MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga ayudas de productos
        /// </summary>
        private void CargarAyudaProductos()
        {
            productoInfo = new ProductoInfo
            {
                ProductoId = 0,
                AlmacenID = almacenSalida.AlmacenID,
                Activo = EstatusEnum.Activo
            };
            skAyudaProductos = new SKAyuda<ProductoInfo>(200, false, productoInfo
                                                   , "PropiedadClaveRegistrarProgramacionMateriaPrima"
                                                   , "PropiedadDescripcionRegistrarProgramacionMateriaPrima",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new ProductoPL(),
                MensajeClaveInexistente = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoSeleccionar,
                TituloEtiqueta = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaLeyendaProducto,
                TituloPantalla = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoTitulo,
            };
            skAyudaProductos.ObtenerDatos += ObtenerDatosProducto;
            //skAyudaProductos.AsignaTabIndex(1);
            SplAyudaProducto.Children.Clear();
            SplAyudaProducto.Children.Add(skAyudaProductos);
        }

        /// <summary>
        /// Obtiene los datos del proveedor con la clave obtenida en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProducto(string clave)
        {
            try
            {
                if (skAyudaAlmacenSalida.Clave != String.Empty && skAyudaAlmacenSalida.Descripcion != String.Empty)
                {
                    skAyudaProductos.Info.AlmacenID = Convert.ToInt32(skAyudaAlmacenSalida.Clave);
                    productoInfo.AlmacenID = Convert.ToInt32(skAyudaAlmacenSalida.Clave);
                    var productoPl = new ProductoPL();
                    var productoInf = new ProductoInfo()
                    {
                        ProductoId = skAyudaProductos.Info.ProductoId,
                        AlmacenID = Convert.ToInt32(skAyudaAlmacenSalida.Clave)
                    };
                    productoInf = productoPl.ObtenerPorProductoIdAlmacenId(productoInf);
                    if (productoInf != null)
                    {
                        if (skAyudaProductos.Info.SubfamiliaId == SubFamiliasEnum.Granos.GetHashCode() ||
                            skAyudaProductos.Info.SubfamiliaId == SubFamiliasEnum.Harinas.GetHashCode())
                        {
                            TxtMermaPermitida.Text = String.Empty;
                            TxtObservaciones.Clear();
                            TxtMermaPermitida.IsEnabled = true;
                            TxtObservaciones.IsEnabled = true;
                        }
                        else
                        {
                            TxtMermaPermitida.Text = String.Empty;
                            TxtObservaciones.Clear();
                            TxtObservaciones.IsEnabled = false;
                            TxtMermaPermitida.IsEnabled = false;
                        }
                    }
                    else
                    {
                        skAyudaProductos.LimpiarCampos();
                        productoInfo.AlmacenID = almacenSalida.AlmacenID;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeProductoNoCorrespondeAlmacen,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        LimpiarSeccionProveedores();
                        return;
                    }
                    LimpiarSeccionProveedores();
                    VerificarConfiguracionFleteInterno();
                }
                else
                {
                    skAyudaProductos.LimpiarCampos();
                    skAyudaAlmacenSalida.AsignarFoco();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources
                        .RegistrarProgramacionFletesInterna_AyudaAlmacenSalidaSinSeleccionar
                    , MessageBoxButton.OK,
                    MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosProducto, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga ayudas de almacenes
        /// </summary>
        private void CargarAyudaDestino()
        {
            var organizacionInfo = new OrganizacionInfo
            {
                ListaTiposOrganizacion = new List<TipoOrganizacionInfo>
                        {
                            //Agregar el resto de tipo almacen
                            new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Ganadera },
                            new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Centro },
                        },
                Activo = EstatusEnum.Activo
            };
            skAyudaDestino = new SKAyuda<OrganizacionInfo>(200, false, organizacionInfo
                                                   , "PropiedadClaveRegistrarProgramacionFleteInterna"
                                                   , "PropiedadDescripcionRegistrarProgramacionFleteInterna",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaOrganizacionInvalido,
                MensajeBusquedaCerrar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaOrganizacionSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaOrganizacionSeleccionar,
                TituloEtiqueta = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaOrganizacionProducto,
                TituloPantalla = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaOrganizacionTitulo,
            };
            skAyudaDestino.ObtenerDatos += ObtenerDatosOrganizacion;
            //skAyudaDestino.AsignaTabIndex(1);
            SplAyudaDestino.Children.Clear();
            SplAyudaDestino.Children.Add(skAyudaDestino);
        }

        /// <summary>
        /// Obtiene la organizacion seleccionada en el filtro (destino)
        /// </summary>
        private void ObtenerDatosOrganizacion(string clave)
        {
            try
            {
                if (VerificarConfiguracionFleteInterno())
                {
                    return;
                }
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaDestino.Info == null)
                {
                    return;
                }
                if (skAyudaDestino.Info != null && skAyudaDestino.Clave != string.Empty && skAyudaDestino.Descripcion != string.Empty)
                {
                    var almacenPl = new AlmacenPL();
                    var listadoAlmacenes = almacenPl.ObtenerAlmacenPorOrganizacion(Convert.ToInt32(skAyudaDestino.Clave));
                    if (listadoAlmacenes == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDestinoNoHayAlmacenParte1 + skAyudaDestino.Descripcion +
                        Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDestinoNoHayAlmacenParte2
                        , MessageBoxButton.OK,
                        MessageImage.Stop);
                        skAyudaDestino.LimpiarCampos();
                        skAyudaDestino.AsignarFoco();
                    }
                    skAyudaDestino.Info = new OrganizacionInfo
                    {
                        ListaTiposOrganizacion = new List<TipoOrganizacionInfo>
                        {
                            //Agregar el resto de tipo almacen
                            new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Ganadera },
                            new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Centro },
                        },
                        Activo = EstatusEnum.Activo
                    };
                    LimpiarSeccionProveedores();
                }
                else
                {
                    skAyudaDestino.Info = new OrganizacionInfo
                    {
                        ListaTiposOrganizacion = new List<TipoOrganizacionInfo>
                        {
                            //Agregar el resto de tipo almacen
                            new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Ganadera },
                            new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Centro },
                        },
                        Activo = EstatusEnum.Activo
                    };
                }
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
        /// Metodo que carga los datos de la ayuda
        /// </summary>
        private void CargarAyudaProveedor()
        {
            var proveedorInfo = new ProveedorInfo
            {
                ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode() },
                    },
                Activo = EstatusEnum.Activo
            };
            skAyudaProveedores = new SKAyuda<ProveedorInfo>(200, false, proveedorInfo
                                                   , "PropiedadClaveCrearContrato"
                                                   , "PropiedadDescripcionCrearContrato",
                                                   "", false, 80, 10, true)
            {
                AyudaPL = new ProveedorPL(),
                MensajeClaveInexistente = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProveedorInvalido,
                MensajeBusquedaCerrar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProveedorSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProveedorSeleccionar,
                TituloEtiqueta = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaLeyendaProveedor,
                TituloPantalla = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProveedorTitulo,
            };
            skAyudaProveedores.ObtenerDatos += ObtenerDatosProveedor;
            skAyudaProveedores.LlamadaMetodosNoExistenDatos += LimpiarProveedor;
            //skAyudaProveedores.AsignaTabIndex(3);
            SplAyudaProveedor.Children.Clear();
            SplAyudaProveedor.Children.Add(skAyudaProveedores);
        }

        private void LimpiarProveedor()
        {
            skAyudaProveedores.LimpiarCampos();
            BtnCostos.IsEnabled = false;
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
                    return;
                }
                if (skAyudaProveedores.Info == null)
                {
                    return;
                }
                    var resultadoValidacion = ValidarSeleccionProveedor();
                    if (resultadoValidacion.Resultado)
                    {
                        if (fleteInternoInfo != null)
                        {
                            if (fleteInternoInfo.Producto.SubfamiliaId== SubFamiliasEnum.Granos.GetHashCode() ||
                                fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Harinas.GetHashCode())
                            {
                                var organizacion = CboTipoMovimiento.SelectedIndex == 1
                                    ? organizacionID
                                    : Convert.ToInt32(skAyudaDestino.Clave);
                                fleteMermaPermitidaInfo = new FleteMermaPermitidaInfo()
                                {
                                    Organizacion = new OrganizacionInfo() { OrganizacionID = organizacion },
                                    SubFamilia = new SubFamiliaInfo() { SubFamiliaID = fleteInternoInfo.Producto.SubfamiliaId },
                                    Activo = EstatusEnum.Activo
                                };
                                var fleteMermaPermitidaPl = new FleteMermaPermitidaPL();
                                fleteMermaPermitidaInfo = fleteMermaPermitidaPl.ObtenerConfiguracion(fleteMermaPermitidaInfo);
                                if (fleteMermaPermitidaInfo == null)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources
                                            .RegistrarProgramacionFletesInterna_MensajeMermaPermitidaNoConfigurada
                                        , MessageBoxButton.OK,
                                        MessageImage.Stop);
                                    LimpiarSeccionProveedores();
                                    skAyudaProveedores.AsignarFoco();
                                }
                                else
                                {
                                    TxtMermaPermitida.Value = fleteMermaPermitidaInfo.MermaPermitida;
                                    TxtObservaciones.IsEnabled = true;
                                    TxtMermaPermitida.IsEnabled = true;
                                    BtnCostos.IsEnabled = true;
                                }
                            }
                            else
                            {
                                TxtMermaPermitida.IsEnabled = false;
                                TxtObservaciones.IsEnabled = false;
                                BtnCostos.IsEnabled = true;
                            }
                        }
                        else
                        {
                            if (skAyudaProductos.Info.SubFamilia.SubFamiliaID == SubFamiliasEnum.Granos.GetHashCode() ||
                                skAyudaProductos.Info.SubFamilia.SubFamiliaID == SubFamiliasEnum.Harinas.GetHashCode())
                            {
                                var organizacion = CboTipoMovimiento.SelectedIndex == 1
                                    ? organizacionID
                                    : Convert.ToInt32(skAyudaDestino.Clave);
                                fleteMermaPermitidaInfo = new FleteMermaPermitidaInfo()
                                {
                                    Organizacion = new OrganizacionInfo() { OrganizacionID = organizacion },
                                    SubFamilia = new SubFamiliaInfo() { SubFamiliaID = skAyudaProductos.Info.SubFamilia.SubFamiliaID },
                                    Activo = EstatusEnum.Activo
                                };
                                var fleteMermaPermitidaPl = new FleteMermaPermitidaPL();
                                fleteMermaPermitidaInfo = fleteMermaPermitidaPl.ObtenerConfiguracion(fleteMermaPermitidaInfo);
                                if (fleteMermaPermitidaInfo == null)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources
                                            .RegistrarProgramacionFletesInterna_MensajeMermaPermitidaNoConfigurada
                                        , MessageBoxButton.OK,
                                        MessageImage.Stop);
                                    LimpiarSeccionProveedores();
                                    skAyudaProveedores.AsignarFoco();
                                }
                                else
                                {
                                    TxtMermaPermitida.Value = fleteMermaPermitidaInfo.MermaPermitida;
                                    TxtObservaciones.IsEnabled = true;
                                    TxtMermaPermitida.IsEnabled = true;
                                    BtnCostos.IsEnabled = true;
                                }
                            }
                            else
                            {
                                TxtMermaPermitida.IsEnabled = false;
                                TxtObservaciones.IsEnabled = false;
                                BtnCostos.IsEnabled = true;
                            }
                        }
                    }
                    else
                    {
                        LimpiarSeccionProveedores();
                        var mensaje = "";
                        mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                            ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco
                            : resultadoValidacion.Mensaje;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            mensaje, MessageBoxButton.OK, MessageImage.Stop);
                    }

                    //Merma permitida minima y maxima pendiente
                    skAyudaProveedores.Info = new ProveedorInfo
                    {
                        ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo
                            {
                                TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()
                            }
                        },
                        Activo = EstatusEnum.Activo
                    };
                    ListaFleteInternoCosto.Clear();
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorAyudaProveedor,
                        MessageBoxButton.OK,
                        MessageImage.Error);
            }
        }

        /// <summary>
        /// Validar que esten ingresados los datos del flete
        /// </summary>
        private ResultadoValidacion ValidarSeleccionProveedor()
        {
            var resultado = new ResultadoValidacion();

            if (CboTipoMovimiento.SelectedIndex == 0)
            {
                CboTipoMovimiento.Focus();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionTipoMovimiento;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaAlmacenSalida.Clave.Trim()) && String.IsNullOrEmpty(skAyudaAlmacenSalida.Descripcion.Trim()))
            {
                skAyudaAlmacenSalida.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionAlmacenSalida;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaProductos.Clave.Trim()) && String.IsNullOrEmpty(skAyudaProductos.Descripcion.Trim()))
            {
                skAyudaProductos.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionProducto;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaDestino.Clave.Trim()) && String.IsNullOrEmpty(skAyudaDestino.Descripcion.Trim()))
            {
                skAyudaDestino.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDestino;
                resultado.Resultado = false;
                return resultado;
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Verificar si hay un flete interno creado para la configuracion seleccionada
        /// </summary>
        private bool VerificarConfiguracionFleteInterno()
        {
            try
            {
                AlmacenInfo almacenInfo = null;
                if (CboTipoMovimiento.SelectedIndex == 0) return false;
                if (String.IsNullOrEmpty(skAyudaAlmacenSalida.Clave.Trim()) ||
                    String.IsNullOrEmpty(skAyudaAlmacenSalida.Descripcion.Trim())) return false;
                if (String.IsNullOrEmpty(skAyudaProductos.Clave.Trim()) ||
                    String.IsNullOrEmpty(skAyudaProductos.Descripcion.Trim())) return false;
                if (CboTipoMovimiento.SelectedIndex == 2)
                {
                    if (String.IsNullOrEmpty(skAyudaDestino.Clave.Trim()) ||
                        String.IsNullOrEmpty(skAyudaDestino.Descripcion.Trim())) return false;

                    var almacenPl = new AlmacenPL();
                        var almacenesDestino =
                            almacenPl.ObtenerAlmacenPorOrganizacion(Convert.ToInt32(skAyudaDestino.Clave));
                    if (almacenesDestino != null && almacenesDestino.Count > 0)
                    {
                        var organizacionPl = new OrganizacionPL();
                        var organizacionInfo = organizacionPl.ObtenerPorID(Convert.ToInt32(skAyudaDestino.Clave));
                        if (organizacionInfo.TipoOrganizacion.TipoOrganizacionID ==
                            TipoOrganizacion.Ganadera.GetHashCode())
                        {
                            almacenInfo =
                                almacenesDestino.FirstOrDefault(
                                    a => a.TipoAlmacenID == (int) TipoAlmacenEnum.MateriasPrimas
                                         && a.Activo == EstatusEnum.Activo);
                        }
                        else
                        {
                            if (organizacionInfo.TipoOrganizacion.TipoOrganizacionID ==
                                TipoOrganizacion.Centro.GetHashCode())
                            {
                                almacenInfo =
                                    almacenesDestino.FirstOrDefault(
                                        a => a.TipoAlmacenID == (int) TipoAlmacenEnum.CentroAcopio
                                             && a.Activo == EstatusEnum.Activo);
                            }
                        }

                        if (almacenInfo != null)
                        {
                            var fleteInterno = new FleteInternoInfo()
                            {
                                Organizacion = new OrganizacionInfo() { OrganizacionID = organizacionID },
                                TipoMovimiento =
                                    new TipoMovimientoInfo()
                                    {
                                        TipoMovimientoID = TipoMovimiento.SalidaPorTraspaso.GetHashCode()
                                    },
                                AlmacenOrigen = new AlmacenInfo() { AlmacenID = Convert.ToInt32(skAyudaAlmacenSalida.Clave) },
                                AlmacenDestino = almacenInfo,
                                Producto = new ProductoInfo() { ProductoId = Convert.ToInt32(skAyudaProductos.Clave) },
                                Activo = EstatusEnum.Activo
                            };
                            var fleteInternoPl = new FleteInternoPL();
                            fleteInternoInfo = fleteInternoPl.ObtenerPorConfiguracion(fleteInterno);
                            if (fleteInternoInfo == null) return false;
                            nuevoGlobal = false;
                            DefinirPantallaFleteInternoCreado();
                            BtnCancelarFlete.IsEnabled = true;
                            LimpiarSeccionProveedores();
                        }
                        else
                        {
                            organizacionInfo = organizacionPl.ObtenerPorID(Convert.ToInt32(skAyudaDestino.Clave));
                            if (organizacionInfo.TipoOrganizacion.TipoOrganizacionID ==
                                TipoOrganizacion.Ganadera.GetHashCode())
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeAlmacenMateriasPrimasNoConfigurado,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                skAyudaDestino.LimpiarCampos();
                                skAyudaDestino.AsignarFoco();
                                return false;
                            }
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.RegistrarProgramacionFletesInterna_MensajeAlmacenCentroAcopioNoConfigurado,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            skAyudaDestino.LimpiarCampos();
                            skAyudaDestino.AsignarFoco();
                            return false;
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDestinoNoHayAlmacenParte1 + skAyudaDestino.Descripcion +
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDestinoNoHayAlmacenParte2
                            , MessageBoxButton.OK,
                            MessageImage.Stop);
                        skAyudaDestino.LimpiarCampos();
                        skAyudaDestino.AsignarFoco();
                        return false;
                    }
                }
                else
                {
                    var almacenPl = new AlmacenPL();
                    var almacenesDestino =
                        almacenPl.ObtenerAlmacenPorOrganizacion(organizacionID);
                    if (almacenesDestino != null && almacenesDestino.Count > 0)
                    {
                        almacenInfo =
                            almacenesDestino.FirstOrDefault(
                                a => a.TipoAlmacenID == (int) TipoAlmacenEnum.PlantaDeAlimentos
                                     && a.Activo == EstatusEnum.Activo);

                        if (almacenInfo != null)
                        {
                            var fleteInterno = new FleteInternoInfo()
                            {
                                Organizacion = new OrganizacionInfo() { OrganizacionID = organizacionID },
                                TipoMovimiento =
                                    new TipoMovimientoInfo()
                                    {
                                        TipoMovimientoID = TipoMovimiento.PaseProceso.GetHashCode()
                                    },
                                AlmacenOrigen = new AlmacenInfo() { AlmacenID = Convert.ToInt32(skAyudaAlmacenSalida.Clave) },
                                AlmacenDestino = almacenInfo,
                                Producto = new ProductoInfo() { ProductoId = Convert.ToInt32(skAyudaProductos.Clave) },
                                Activo = EstatusEnum.Activo
                            };
                            var fleteInternoPl = new FleteInternoPL();
                            fleteInternoInfo = fleteInternoPl.ObtenerPorConfiguracion(fleteInterno);
                            if (fleteInternoInfo == null) return false;
                            nuevoGlobal = false;
                            DefinirPantallaFleteInternoCreado();
                            BtnCancelarFlete.IsEnabled = true;
                            LimpiarSeccionProveedores();
                        }
                        else
                        {
                            var organizacionPl = new OrganizacionPL();
                            var organizacionInfo = organizacionPl.ObtenerPorID(Convert.ToInt32(skAyudaDestino.Clave));
                            if (organizacionInfo.TipoOrganizacion.TipoOrganizacionID ==
                                TipoOrganizacion.Ganadera.GetHashCode())
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeAlmacenMateriasPrimasNoConfigurado,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                skAyudaDestino.LimpiarCampos();
                                skAyudaDestino.AsignarFoco();
                                return false;
                            }
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.RegistrarProgramacionFletesInterna_MensajeAlmacenCentroAcopioNoConfigurado,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            skAyudaDestino.LimpiarCampos();
                            skAyudaDestino.AsignarFoco();
                            return false;
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDestinoNoHayAlmacenParte1 + skAyudaDestino.Descripcion +
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDestinoNoHayAlmacenParte2
                            , MessageBoxButton.OK,
                            MessageImage.Stop);
                        skAyudaDestino.LimpiarCampos();
                        skAyudaDestino.AsignarFoco();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorVerificarConfiguracion,
                        MessageBoxButton.OK,
                        MessageImage.Error);
            }
            return true;
        }

        /// <summary>
        /// Validacion antes de agregar el ajuste al grid
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarAgregar(bool actualizar)
        {
            var resultado = new ResultadoValidacion();

            if (CboTipoMovimiento.SelectedIndex == 0)
            {
                CboTipoMovimiento.Focus();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionTipoMovimiento;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaAlmacenSalida.Clave.Trim()) && String.IsNullOrEmpty(skAyudaAlmacenSalida.Descripcion.Trim()))
            {
                skAyudaAlmacenSalida.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionAlmacenSalida;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaProductos.Clave.Trim()) && String.IsNullOrEmpty(skAyudaProductos.Descripcion.Trim()))
            {
                skAyudaProductos.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionProducto;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaDestino.Clave.Trim()) && String.IsNullOrEmpty(skAyudaDestino.Descripcion.Trim()))
            {
                skAyudaDestino.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDestino;
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(skAyudaProveedores.Clave.Trim()) && String.IsNullOrEmpty(skAyudaProveedores.Descripcion.Trim()))
            {
                skAyudaProveedores.AsignarFoco();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionProveedor;
                resultado.Resultado = false;
                return resultado;
            }

            //Se hara la validacion cuando el movimiento sea salida por traspaso
            if (CboTipoMovimiento.SelectedIndex == 2)
            {
                if (organizacionID == Convert.ToInt32(skAyudaDestino.Clave))
                {
                    skAyudaDestino.AsignarFoco();
                    resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionDestinoInvalido;
                    resultado.Resultado = false;
                    return resultado;
                }
            }

            //Verificar si el producto ya esta agregado
            if (!actualizar)
            {
                ListaFleteInternoDetalleFiltrados.Clear();
                ListaFleteInternoDetalleFiltrados = ListaFleteInternoDetalle.Where(fleteInternoDetalleP => !fleteInternoDetalleP.Eliminado).ToList();
                if (ListaFleteInternoDetalleFiltrados != null)
                {
                    if ((from fleteInternoDetalle in ListaFleteInternoDetalleFiltrados
                         where fleteInternoDetalle.Proveedor.CodigoSAP == skAyudaProveedores.Clave
                         select fleteInternoDetalle).Any())
                    {
                        skAyudaProveedores.AsignarFoco();
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionProveedorAgregado;
                        return resultado;
                    }
                }
            }

            if (fleteInternoInfo != null)
            {
                if (fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Granos.GetHashCode() ||
                    fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Harinas.GetHashCode())
                {
                    //Validar merma
                    if (TxtMermaPermitida.Value < 0)
                    {
                        TxtMermaPermitida.Focus();
                        resultado.Mensaje =
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionMermaPermitidaCero;
                        resultado.Resultado = false;
                        return resultado;
                    }

                    if (TxtMermaPermitida.Text == String.Empty || TxtMermaPermitida.Value == 0)
                    {
                        TxtMermaPermitida.Focus();
                        resultado.Mensaje =
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionMermaPermitida;
                        resultado.Resultado = false;
                        return resultado;
                    }

                    //Validacion pendiente
                    if (fleteMermaPermitidaInfo != null)
                    {
                        //Validar observaciones
                        if (TxtMermaPermitida.Value > fleteMermaPermitidaInfo.MermaPermitida)
                        {
                            if (TxtObservaciones.Text == String.Empty)
                            {
                                TxtObservaciones.Focus();
                                resultado.Mensaje =
                                    Properties.Resources
                                        .RegistrarProgramacionFletesInterna_MensajeValidacionObservaciones;
                                resultado.Resultado = false;
                                return resultado;
                            }
                        }
                    }
                    else
                    {
                        BtnAgregar.Focus();
                        resultado.Mensaje =
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeMermaPermitidaNoConfigurada;
                        resultado.Resultado = false;
                        return resultado;
                    }
                }
            }
            else
            {
                if (skAyudaProductos.Info.SubFamilia.SubFamiliaID == SubFamiliasEnum.Granos.GetHashCode() ||
                    skAyudaProductos.Info.SubFamilia.SubFamiliaID == SubFamiliasEnum.Harinas.GetHashCode())
                {
                    //Validar merma
                    if (TxtMermaPermitida.Value < 0)
                    {
                        TxtMermaPermitida.Focus();
                        resultado.Mensaje =
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionMermaPermitidaCero;
                        resultado.Resultado = false;
                        return resultado;
                    }

                    if (TxtMermaPermitida.Text == String.Empty || TxtMermaPermitida.Value == 0)
                    {
                        TxtMermaPermitida.Focus();
                        resultado.Mensaje =
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionMermaPermitida;
                        resultado.Resultado = false;
                        return resultado;
                    }

                    //Validacion pendiente
                    if (fleteMermaPermitidaInfo != null)
                    {
                        //Validar observaciones
                        if (TxtMermaPermitida.Value > fleteMermaPermitidaInfo.MermaPermitida)
                        {
                            if (TxtObservaciones.Text == String.Empty)
                            {
                                TxtObservaciones.Focus();
                                resultado.Mensaje =
                                    Properties.Resources
                                        .RegistrarProgramacionFletesInterna_MensajeValidacionObservaciones;
                                resultado.Resultado = false;
                                return resultado;
                            }
                        }
                    }
                    else
                    {
                        BtnAgregar.Focus();
                        resultado.Mensaje =
                            Properties.Resources.RegistrarProgramacionFletesInterna_MensajeMermaPermitidaNoConfigurada;
                        resultado.Resultado = false;
                        return resultado;
                    }
                }
            }

            var costos = ListaFleteInternoCosto.Where(fleteInternoCostoP => !fleteInternoCostoP.Eliminado).ToList();
            if (costos.Count == 0)
            {
                BtnCostos.Focus();
                resultado.Mensaje = Properties.Resources.RegistrarProgramacionFletesInterna_MensajeValidacionIngresarCosto;
                resultado.Resultado = false;
                return resultado;
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Agrega un producto al grid principal
        /// </summary>
        private void AgregarFleteInternoDetalle()
        {
            try
            {
                //Revisar
                var proveedorPl = new ProveedorPL();
                var proveedorInfo = new ProveedorInfo() { CodigoSAP = skAyudaProveedores.Clave };
                proveedorInfo = proveedorPl.ObtenerPorCodigoSAP(proveedorInfo);

                TipoTarifaEnum.Tonelada.GetHashCode();

                fleteInternoDetalleInfo = new FleteInternoDetalleInfo
                {
                    Proveedor = proveedorInfo,
                    MermaPermitida = Convert.ToDecimal(TxtMermaPermitida.Value),
                    MermaPermitidaDescripcion = TxtMermaPermitida.Text,
                    Observaciones = TxtObservaciones.Text,
                    UsuarioCreacionId = usuarioID,
                    Activo = EstatusEnum.Activo,
                    ListadoFleteInternoCosto = new List<FleteInternoCostoInfo>(),
                    TipoTarifaID = int.Parse(CboTipoFlete.SelectedValue.ToString()), //agregado Jose Angel Rodriguez Rodriguez
                    TipoTarifa = CboTipoFlete.Text.ToString()//agregado Jose Angel Rodriguez Rodriguez

                };
                if (fleteInternoInfo != null)
                {
                    if (fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Granos.GetHashCode() ||
                        fleteInternoInfo.Producto.SubfamiliaId == SubFamiliasEnum.Harinas.GetHashCode())
                    {
                        fleteInternoDetalleInfo.MermaPermitidaDescripcion =
                            fleteInternoDetalleInfo.MermaPermitida.ToString("N3");
                    }
                    else
                    {
                        fleteInternoDetalleInfo.MermaPermitidaDescripcion = String.Empty;
                    }
                }
                else
                {
                    if (skAyudaProductos.Info.SubFamilia.SubFamiliaID == SubFamiliasEnum.Granos.GetHashCode() ||
                        skAyudaProductos.Info.SubFamilia.SubFamiliaID == SubFamiliasEnum.Harinas.GetHashCode())
                    {
                        fleteInternoDetalleInfo.MermaPermitidaDescripcion =
                            fleteInternoDetalleInfo.MermaPermitida.ToString("N3");
                    }
                    else
                    {
                        fleteInternoDetalleInfo.MermaPermitidaDescripcion = String.Empty;
                    }
                }
                fleteInternoDetalleInfo.ListadoFleteInternoCosto.AddRange(ListaFleteInternoCosto);
                ListaFleteInternoDetalle.Add(fleteInternoDetalleInfo);
                GridFleteInternoDetalle.ItemsSource = null;
                GridFleteInternoDetalle.ItemsSource = ListaFleteInternoDetalle.Where(registro => !registro.Eliminado).ToList();
                LimpiarSeccionProveedores();
                skAyudaProveedores.AsignarFoco();
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarProgramacionFletesInterna_MensajeErrorAgregarFleteDetalle,
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Limpiar seccion ayuda proveedores
        /// </summary>
        private void LimpiarSeccionProveedores()
        {
            skAyudaProveedores.LimpiarCampos();
            TxtMermaPermitida.Text = string.Empty;
            TxtObservaciones.Clear();
            ListaFleteInternoCosto.Clear();
            BtnCostos.IsEnabled = false;
        }

        /// <summary>
        /// Deshabilita los controles tipo movimiento, almacen salida, producto y destino
        /// </summary>
        private void DeshabilitarControlesFleteInterno()
        {
            CboTipoMovimiento.IsEnabled = false;
            skAyudaAlmacenSalida.IsEnabled = false;
            skAyudaProductos.IsEnabled = false;
            skAyudaDestino.IsEnabled = false;
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
        /// Metodo que crea la ventana
        /// </summary>
        /// <param name="ventana"></param>
        protected void MostrarCentrado(Window ventana)
        {
            ventana.Left = (ActualWidth - ventana.Width) / 2;
            ventana.Top = ((ActualHeight - ventana.Height) / 2) + 132;
            ventana.Owner = Application.Current.Windows[1];
            ventana.ShowDialog();
        }
        #endregion
    }
}
