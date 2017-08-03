using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Costo = SIE.Services.Info.Enums.Costo;
using TipoCuenta = SIE.Services.Info.Enums.TipoCuenta;
using SIE.Services.Servicios.BL;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para EntradaMateriaPrima.xaml
    /// </summary>
    public partial class EntradaMateriaPrima
    {
        #region Atributos

        private SKAyuda<EntradaProductoInfo> skAyudaFolioEntrada;
        private SKAyuda<CostoInfo> skAyudaCosto;
        private SKAyuda<CuentaSAPInfo> skAyudaCuenta;
        private SKAyuda<ProveedorInfo> skAyudaProveedor;

        private int organizacionId;
        private int usuarioId;
        private int renglonGrid;
        private int renglonGridCuentaProveedor;
        private bool cargarInicial;
        private bool nuevaLinea;
        private int renglon;
        private int columna;
        private int numeroRenglon;

        private string productoConRestriccion;
        private string porcentajeDescuento;
        private bool aplicarValidacionRestriccionDesc;
        private ContenedorEntradaMateriaPrimaInfo ContenedorEntradaMateriaPrima
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ContenedorEntradaMateriaPrimaInfo)DataContext;
            }
            set
            {
                DataContext = value;

            }
        }

        private void InicializaContexto()
        {
            ContenedorEntradaMateriaPrima = new ContenedorEntradaMateriaPrimaInfo();
        }


        #endregion

        #region Constructor
        /// <summary>
        /// Costructor de la clase
        /// </summary>
        public EntradaMateriaPrima()
        {
            try
            {
                organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
                usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                InitializeComponent();
                renglonGrid = -1;
                renglonGridCuentaProveedor = -1;
                renglon = 0;
                columna = 0;
                numeroRenglon = -1;
                aplicarValidacionRestriccionDesc = false;
                productoConRestriccion = string.Empty;
                porcentajeDescuento = string.Empty;
                if (obtenerParametrosRestriccionDescuentoProductoSK())
                {
                    InicializaContexto();
                    CargarAyudas();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }

        }
        

        #endregion

        #region Eventos
        /// <summary>
        /// Evento de carga del formulario
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void EntradaMateriaPrima_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
				InicializaContexto();
				CargarAyudas();
                if (skAyudaFolioEntrada != null)
                {
                    numeroRenglon = -1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento click del boton limpiar
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LimpiarTodo();
                skAyudaFolioEntrada.AsignarFoco();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento check que indica que el costo tiene una cuenta
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void chkCuenta_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var checkBox = (CheckBox)sender;

                if (!checkBox.IsChecked.HasValue)
                {
                    return;
                }
                var costo = (CostoEntradaMateriaPrimaInfo)checkBox.CommandParameter;
                var rowIndex = gridCostos.Items.IndexOf(costo);
                var cell = GetCell(rowIndex, 2);
                var stackPanel = GetVisualChild<StackPanel>(cell);
                if (costo.TieneCuenta)
                {
                    AgregarAyudaCuenta(stackPanel);
                }
                else
                {
                    AgregarAyudaProveedor(stackPanel);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento checked del checbox de cuenta
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void chkCuenta_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var checkBox = (CheckBox)sender;

                if (!checkBox.IsChecked.HasValue)
                {
                    return;
                }
                var costo = (CostoEntradaMateriaPrimaInfo)checkBox.CommandParameter;
                var rowIndex = gridCostos.Items.IndexOf(costo);
                var cell = GetCell(rowIndex, 2);
                var stackPanel = GetVisualChild<StackPanel>(cell);
                if (costo.TieneCuenta)
                {
                    AgregarAyudaCuenta(stackPanel);
                }
                else
                {
                    AgregarAyudaProveedor(stackPanel);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento click del boton eliminar
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridCostos.SelectedIndex > -1)
                {
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.RemoveAt(gridCostos.SelectedIndex);
                    numeroRenglon--;
                    renglonGrid--;
                    renglonGridCuentaProveedor--;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Evento click del boton guardar
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ContenedorEntradaMateriaPrima.EntradaProducto == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.EntradaMateriaPrima_SeleccionarFolio,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    return;
                }

                //Validar si el producto es una premezcla
                if (ContenedorEntradaMateriaPrima.Producto.SubFamilia.SubFamiliaID ==
                    (int)SubFamiliasEnum.MicroIngredientes)
                {
                    //Existe la configuracion de la premezcla
                    if (ContenedorEntradaMateriaPrima.EntradaProducto.PremezclaInfo == null ||
                        ContenedorEntradaMateriaPrima.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrima_CrearConfiguracionPremezcla,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        return;
                    }
                    //Se asigna el precio capturado
                    ContenedorEntradaMateriaPrima.Contrato.Precio = !String.IsNullOrEmpty(txtPrecio.Text)
                        ? Convert.ToDecimal(txtPrecio.Text)
                        : 0;
                    if (ContenedorEntradaMateriaPrima.Contrato.Precio == 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrima_ProporcionarPrecio,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        txtPrecio.Focus();
                        return;
                    }

                    //Validar la capacidad del lote seleccionado
                    if (ValidarCapacidadLoteSubProductoPremezcla(
                        ContenedorEntradaMateriaPrima.EntradaProducto.PremezclaInfo))
                    {
                        return;
                    }
                }
                else
                {
                    var tipoContrato = (TipoContratoEnum)ContenedorEntradaMateriaPrima.Contrato.TipoContrato.TipoContratoId;
                    var tipoEntrada = (TipoEntradaEnum)CboTipoEntrada.SelectedItem;
                    switch (tipoContrato)
                    {
                        case TipoContratoEnum.BodegaNormal:
                            if (tipoEntrada == TipoEntradaEnum.Traspaso)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                  Properties.Resources.EntradaMateriaPrima_TraspasoBodegaNormal,
                                                  MessageBoxButton.OK,
                                                  MessageImage.Stop);
                                return;
                            }
                            break;
                        case TipoContratoEnum.BodegaTercero:
                            if (tipoEntrada == TipoEntradaEnum.Compra)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                  Properties.Resources.EntradaMateriaPrima_CompraBodegaTerceros,
                                                  MessageBoxButton.OK,
                                                  MessageImage.Stop);
                                return;
                            }
                            break;
                    }
                }
                
                ContenedorEntradaMateriaPrima.Observaciones = txtObservaciones.Text;
                ContenedorEntradaMateriaPrima.TipoEntrada = CboTipoEntrada.Text;
                ContenedorEntradaMateriaPrima.UsuarioId = usuarioId;
                ContenedorEntradaMateriaPrima.aplicaRestriccionDescuento = aplicarValidacionRestriccionDesc;
                ContenedorEntradaMateriaPrima.PorcentajeRestriccionDescuento = decimal.Parse(porcentajeDescuento);
                if (ContenedorEntradaMateriaPrima.Contrato != null)
                {
                    ContenedorEntradaMateriaPrima.Contrato.Precio = txtPrecio.Text != null
                        ? Convert.ToDecimal(txtPrecio.Text)
                        : 0;
                    if (ContenedorEntradaMateriaPrima.Contrato.Precio == 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrima_ProporcionarPrecioEntrada,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        txtPrecio.Focus();
                        return;
                    }

                    if (ContenedorEntradaMateriaPrima.Contrato.TipoContrato != null &&
                        ContenedorEntradaMateriaPrima.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaNormal.GetHashCode())
                    {
                        if (ContenedorEntradaMateriaPrima.Contrato.Parcial == CompraParcialEnum.Parcial)
                        {
                            if (ContenedorEntradaMateriaPrima.Contrato.ListaContratoParcial != null)
                            {
                                //Si no tiene parcialidades seleccionadas o tiene mas de 2 seleccionadas
                                if (!(ContenedorEntradaMateriaPrima.Contrato.ListaContratoParcial.Where(
                                    registro => registro.Seleccionado).ToList().Count > 0 &&
                                      ContenedorEntradaMateriaPrima.Contrato.ListaContratoParcial.Where(
                                          registro => registro.Seleccionado).ToList().Count <= 2))
                                {
                                    //Cambiar mensaje
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.EntradaMateriaPrima_ProporcionarPrecio,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                    return;
                                }
                            }
                            else
                            {
                                //Cambiar mensaje
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.EntradaMateriaPrima_ProporcionarPrecio,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                return;
                            }
                        }
                    }

                    if (ContenedorEntradaMateriaPrima.Contrato.PesoNegociar != null &&
                        ContenedorEntradaMateriaPrima.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString())
                    {
                        var entradaProductoPl = new EntradaProductoPL();
                        List<EntradaProductoInfo> listaEntradaProductos =
                            entradaProductoPl.ObtenerEntradaProductoPorContrato(ContenedorEntradaMateriaPrima.Contrato);

                        if (listaEntradaProductos != null)
                        {
                            var pesoBonificacionTotal = listaEntradaProductos.Sum(registro => registro.PesoBonificacion);
                            decimal toleranciaCalculada = (ContenedorEntradaMateriaPrima.Contrato.Tolerancia *
                                                           ContenedorEntradaMateriaPrima.Contrato.Cantidad) / 100;


                        }
                    }
                    else
                    {
                        var entradaProductoPl = new EntradaProductoPL();
                        List<EntradaProductoInfo> listaEntradaProductos =
                            entradaProductoPl.ObtenerEntradaProductoPorContrato(ContenedorEntradaMateriaPrima.Contrato);

                        if (listaEntradaProductos != null)
                        {
                            var lista = listaEntradaProductos;
                            if (lista.Count > 0)
                            {
                                lista = lista.Where(registro => registro.AlmacenMovimiento.AlmacenMovimientoID > 0).ToList();
                                if (lista.Count > 0)
                                {
                                    decimal descuentos = 0;


                                    foreach (var productoActual in lista)
                                    {
                                        if (productoActual.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Granos)
                                        {

                                            foreach (var detalle in productoActual.ProductoDetalle
                                                .Where(t => t.Indicador.IndicadorId == (int)IndicadoresEnum.Humedad ||
                                                            t.Indicador.IndicadorId == (int)IndicadoresEnum.Impurezas ||
                                                            t.Indicador.IndicadorId ==
                                                            (int)IndicadoresEnum.Danostotales))
                                            {
                                                if (detalle.ProductoMuestras != null)
                                                {
                                                    descuentos = detalle.ProductoMuestras.Aggregate(descuentos, (current, muestra) => current + muestra.Descuento);
                                                }
                                            }
                                        }
                                        else if (productoActual.Producto.SubFamilia.SubFamiliaID ==
                                 (int)SubFamiliasEnum.Forrajes)
                                        {
                                            foreach (var entradaProductoDetalle in productoActual.ProductoDetalle)
                                            {
                                                foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras)
                                                {
                                                    descuentos += productoMuestra.Descuento;
                                                    break;
                                                }
                                                break;
                                            }
                                        }
                                        else
                                        // Si es otra subfamilia se hace la validacion que se hacia antes del cambio de la calidad origen.
                                        {
                                            foreach (var entradaProductoDetalle in productoActual.ProductoDetalle)
                                            {
                                                foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras)
                                                {
                                                    descuentos += productoMuestra.Descuento;
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }

                    if (ContenedorEntradaMateriaPrima.Contrato != null)
                    {
                        if (ContenedorEntradaMateriaPrima.Contrato.Parcial == CompraParcialEnum.Parcial)
                        {
                            if (ContenedorEntradaMateriaPrima.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaNormal.GetHashCode())
                                if (ContenedorEntradaMateriaPrima.Contrato.ListaContratoParcial == null)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.EntradaMateriaPrima_ContratoParcialSinParcialidades,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                    return;
                                }
                        }
                    }
                }

                if (ValidarDatos())
                {
                    var entradaMateriaPrimaPl = new EntradaMateriaPrimaPL();
                    MemoryStream resultado = entradaMateriaPrimaPl.GuardarEntradaMateriaPrima(ContenedorEntradaMateriaPrima);

                    if (resultado != null)
                    {
                        enviarCorreo();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          string.Format(Properties.Resources.EntradaMateriaPrima_GuardadoOk,
                                                        skAyudaFolioEntrada.Clave),
                                          MessageBoxButton.OK,
                                          MessageImage.Correct);

                        var exportarPoliza = new ExportarPoliza();
                        exportarPoliza.ImprimirPoliza(resultado,
                                                      string.Format("{0} {1}",
                                                                    "Poliza de Entrada de Materia Prima Folio No",
                                                                    ContenedorEntradaMateriaPrima.EntradaProducto.Folio));
                        LimpiarTodo();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrima_DatosEnBlanco,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                    if (ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima == null || ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Count == 0) return;
                    if (ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Count <= 0) return;
                    gridCostos.Focus();
                    gridCostos.CurrentCell = new DataGridCellInfo(
                    gridCostos.Items[renglon], gridCostos.Columns[columna]);
                    gridCostos.BeginEdit();
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Validar si la cantidad disponible de los lotes cubren lo entregado
        /// </summary>
        /// <param name="premezclaInfo"></param>
        /// <returns></returns>
        private bool ValidarCapacidadLoteSubProductoPremezcla(PremezclaInfo premezclaInfo)
        {
            try
            {
                foreach (var ingredientePremezcla in premezclaInfo.ListaPremezclaDetalleInfos)
                {
                    if (ingredientePremezcla.Lote != null)
                    {
                        if (ingredientePremezcla.Lote.AlmacenInventarioLoteId > 0)
                        {
                            if (ingredientePremezcla.Lote.Cantidad < ingredientePremezcla.Kilogramos)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                String.Format("{0}{1}{2}", Properties.Resources.EntradaMateriaPrima_LoteSeleccionado,
                                    ingredientePremezcla.Producto.Descripcion,
                                    Properties.Resources.EntradaMateriaPrima_LoteSeleccionadoNotieneCapacidad),
                                    MessageBoxButton.OK,
                                    MessageImage.Warning);
                                return true;
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                String.Format("{0}{1}{2}", Properties.Resources.EntradaMateriaPrima_IngredienteSeleccionado,
                                    ingredientePremezcla.Producto.Descripcion,
                                    Properties.Resources.EntradaMateriaPrima_IngredienteSeleccionadoNotieneCapacidad),
                                    MessageBoxButton.OK,
                                    MessageImage.Warning);
                            return true;
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                String.Format("{0}{1}{2}", Properties.Resources.EntradaMateriaPrima_IngredienteSeleccionado,
                                    ingredientePremezcla.Producto.Descripcion,
                                    Properties.Resources.EntradaMateriaPrima_IngredienteSeleccionadoNotieneCapacidad),
                                    MessageBoxButton.OK,
                                    MessageImage.Warning);
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// Evento load del panel que contiene la ayuda de cuanta o proveedor
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void stpAyudaCuentaProveedor_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridCell cell = new DataGridCell();
                StackPanel stackPanel = (StackPanel)sender;
                CostoEntradaMateriaPrimaInfo tmpCostoEntrada = null;
				bool esCostoEditado = false;
                bool editable = true;

                if (ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima != null && ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Count > numeroRenglon)
                {
                    tmpCostoEntrada = ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[numeroRenglon];
                    esCostoEditado = tmpCostoEntrada.EsEditado;
                    editable = (tmpCostoEntrada.Costos != null && tmpCostoEntrada.Costos.Editable);

                }

                if (cargarInicial)
                {
                    if (ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima != null)
                    {
                        if (ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Count == (numeroRenglon))
                        {
                            cargarInicial = false;
                        }
                    }

                    if (stackPanel.Children.Count >= 0)
                    {
                        if (tmpCostoEntrada != null)
                        {
                            if (tmpCostoEntrada.TieneCuenta)
                            {
                                if (!esCostoEditado)
                                {
                                    if (tmpCostoEntrada.Costos == null)
                                    {
                                        AgregarAyudaCuenta(stackPanel);
                                    }
                                    else
                                    {
                                        AgregarDatosCuentaProveedorSinAyuda(stackPanel, tmpCostoEntrada); 
                                    }
                                }

                            }
                            else
                            {
                                if (tmpCostoEntrada.EsFlete)
                                {
                                    AgregarDatosProveedorSinAyuda(stackPanel, tmpCostoEntrada);
                                }
                                else
                                {
                                    AgregarAyudaProveedor(stackPanel);
                                }
                            }
                        }
                        else
                        {
                            AgregarAyudaProveedor(stackPanel);
                        }
                    }
                }
                else
                {
                    if (nuevaLinea)
                    {
                        if (tmpCostoEntrada != null && tmpCostoEntrada.TieneCuenta)
                        {
                            AgregarAyudaCuenta(stackPanel);
                        }
                        else
                        {
                            AgregarAyudaProveedor(stackPanel);
                        }
                        nuevaLinea = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

 		/// <summary>
        /// Evento load del panel que contine la ayuda de costos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stpAyudaCostos_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var stackPanel = (StackPanel)sender;
                if (cargarInicial)
                {
                    renglonGrid++;
                    renglonGridCuentaProveedor++;
                    numeroRenglon ++;
                    CostoEntradaMateriaPrimaInfo tmpCostoEntrada = null;
                    if (ContenedorEntradaMateriaPrima != null)
                    {
                        if (ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Count > numeroRenglon)
                        {
                            tmpCostoEntrada = ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[numeroRenglon];
                        }
                    }
                    if (stackPanel.Children.Count >= 0)
                    {
                        if (tmpCostoEntrada == null || !tmpCostoEntrada.EsFlete)
                        {
                            AgregarAyudaCosto(stackPanel);
                        }
                        else
                        {
                            AgregarDatosCuentaSinAyuda(stackPanel, tmpCostoEntrada);
                        }
                    }
                }
                else
                {
                    if (nuevaLinea){
                        renglonGrid++;
                        renglonGridCuentaProveedor++;
                        numeroRenglon ++;
                        AgregarAyudaCosto(stackPanel);
                    }
                }

            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        /// <summary>
        /// Evento click del boton agregar nueva linea
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void btnAgregarLinea_Click(object sender, RoutedEventArgs e)
        {
            AgregarNuevaLinea(false);
        }


        /// <summary>
        /// Evento que indica cuando se cambiaron los datos del contexto del grid de costos
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void GridCostos_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            renglonGrid = -1;
            renglonGridCuentaProveedor = -1;
            numeroRenglon = -1;
        }

        /// <summary>
        /// Evento load del grid de costos
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void GridCostos_OnLoaded(object sender, RoutedEventArgs e)
        {
            renglonGrid = -1;
            renglonGridCuentaProveedor = -1;
            numeroRenglon = -1;
        }

        /// <summary>
        /// Envia el tab para al contro dentro de la celda
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void GridCostos_OnCurrentCellChanged(object sender, EventArgs e)
        {
            Send(Key.Tab);
        }

        /// <summary>
        /// Valida la entrada de caracteres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtObservaciones_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarCaracterValidoTexo(e.Text);
        }

        /// <summary>
        /// Boton para cancelar el proceso
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(
                               Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.EntradaMateriaPrima_limpiar,
                               MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {

                LimpiarTodo();
                skAyudaFolioEntrada.AsignarFoco();
            }
        }

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Existe la configuracion de la premezcla
                if (ContenedorEntradaMateriaPrima.EntradaProducto.PremezclaInfo == null ||
                    ContenedorEntradaMateriaPrima.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaMateriaPrima_CrearConfiguracionPremezcla,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                    return;
                }

                //decimal pesoNeto = ContenedorEntradaMateriaPrima.EntradaProducto.PesoBruto - ContenedorEntradaMateriaPrima.EntradaProducto.PesoTara;
                decimal pesoNeto = ContenedorEntradaMateriaPrima.EntradaProducto.PesoOrigen;

                var premezclaDetalle = new PremezclaDetalle(ContenedorEntradaMateriaPrima.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos,
                                                         pesoNeto);
                MostrarCentrado(premezclaDetalle);
                ContenedorEntradaMateriaPrima.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos =
                        premezclaDetalle.ListaDetallePremezcla;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void BtnVerEntradas_OnClick(object sender, RoutedEventArgs e)
        {
            var dialogoComprasParciales = new EntradaMateriaPrimaComprasParciales(ContenedorEntradaMateriaPrima);
            MostrarCentrado(dialogoComprasParciales);
            if (dialogoComprasParciales.contenedorRetorno.Contrato.ListaContratoParcial != null && dialogoComprasParciales.Grabo)
            {
                ContenedorEntradaMateriaPrima.Contrato.ListaContratoParcial =
                    dialogoComprasParciales.contenedorRetorno.Contrato.ListaContratoParcial;

                var listaContratoSeleccionado =
                        ContenedorEntradaMateriaPrima.Contrato.ListaContratoParcial.Where(
                            registro => registro.Seleccionado).ToList();
                if (listaContratoSeleccionado.Any())
                {
                    txtPrecio.Text =
                        (
                            listaContratoSeleccionado.Sum(registro => registro.Importe * registro.CantidadEntrante) /
                            listaContratoSeleccionado.Sum(registro => registro.CantidadEntrante)).ToString("N2");
                }
            }
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaFolio();
            skAyudaFolioEntrada.AsignarFoco();
        }

        /// <summary>
        /// Agrega la ayuda para la busqueda de folio
        /// </summary>
        private void AgregarAyudaFolio()
        {
            try
            {
                skAyudaFolioEntrada = new SKAyuda<EntradaProductoInfo>(0,
                    false,
                    new EntradaProductoInfo
                    {
                        Folio = 0,
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                        Estatus = new EstatusInfo
                        {
                            EstatusId = (int)Estatus.Aprobado
                        }
                    },
                    "PropiedadFolio",
                    "PropiedadDescripcionProducto",
                    true,
                    220,
                    true)
                {
                    AyudaPL = new EntradaProductoPL(),
                    MensajeClaveInexistente = Properties.Resources.EntradaMateriaPrima_AyudaFolioInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.EntradaMateriaPrima_Busqueda,
                    MensajeAgregar = Properties.Resources.EntradaMateriaPrima_Seleccionar,
                    TituloEtiqueta = Properties.Resources.EntradaMateriaPrima_LeyendaBusqueda,
                    TituloPantalla = Properties.Resources.EntradaMateriaPrima_Busqueda_Titulo,
                    MetodoPorDescripcion = "ObtenerFoliosPorPaginaParaEntradaMateriaPrima"

                };

                skAyudaFolioEntrada.ObtenerDatos += ObtenerDatosFolio;
                skAyudaFolioEntrada.LlamadaMetodosNoExistenDatos += LimpiarTodo;

                skAyudaFolioEntrada.AsignaTabIndex(0);
                SplAyudaFolioMateriaPrima.Children.Clear();
                SplAyudaFolioMateriaPrima.Children.Add(skAyudaFolioEntrada);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        /// <summary>
        /// Inicializa la ayuda para obtener la cuenta del proveedor
        /// </summary>
        private void AgregarAyudaCuenta(StackPanel stackPanel)
        {
            skAyudaCuenta = new SKAyuda<CuentaSAPInfo>("CuentaSAP", "Descripcion", 250, false
                                                      , "CuentaSap", "DescripcionCuenta", 80, 10, true);
            var camposInfo = new List<String> { "CuentaSAP", "Descripcion" };
            skAyudaCuenta.AyudaPL = new CuentaSAPPL();
            skAyudaCuenta.Info = new CuentaSAPInfo
            {
                ListaTiposCuenta = new List<TipoCuentaInfo>
                {
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode() },
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Inventario.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Iva.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Provision.GetHashCode()},
                },

                Activo = EstatusEnum.Activo
            };
            skAyudaCuenta.CamposInfo = camposInfo;

            skAyudaCuenta.MetodoPorId = "ObtenerPorFiltro";
            skAyudaCuenta.MetodoPorDescripcion = "ObtenerPorPagina";
            skAyudaCuenta.MetodoPaginadoBusqueda = "ObtenerPorPagina";

            skAyudaCuenta.MensajeClaveInexistente = Properties.Resources.Cuenta_CodigoInvalido;
            skAyudaCuenta.MensajeAgregar = Properties.Resources.Cuenta_Seleccionar;
            skAyudaCuenta.MensajeBusqueda = Properties.Resources.Cuenta_Busqueda;
            skAyudaCuenta.MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_CuentaSalirSinSeleccionar;
            skAyudaCuenta.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaCuenta;
            skAyudaCuenta.TituloPantalla = Properties.Resources.BusquedaCuenta_Titulo;
            skAyudaCuenta.ObtenerDatos += ObtenerDatosCuenta;
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCuenta);
        }

        /// <summary>
        /// Inicializa la busqueda
        /// </summary>
        /// <param name="calve"></param>
        private void ObtenerDatosCuenta(string calve)
        {
            skAyudaCuenta.Info = new CuentaSAPInfo
            {
                ListaTiposCuenta = new List<TipoCuentaInfo>
                {
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode() },
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Inventario.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Iva.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Provision.GetHashCode()},
                },

                Activo = EstatusEnum.Activo
            };
        }

        /// <summary>
        /// Agrega los datos de la cuenta sin ayuda
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <param name="tmpCostoEntrada"></param>
        private void AgregarDatosCuentaProveedorSinAyuda(StackPanel stackPanel, CostoEntradaMateriaPrimaInfo tmpCostoEntrada)
        {
            var txtId = new TextBox
            {
                Text = tmpCostoEntrada.Costos.ClaveContable,
                Width = 79,
                IsEnabled = false
            };
            var txtClave = new TextBox
            {
                Text = tmpCostoEntrada.Costos.Descripcion,
                Width = 160,
                IsEnabled = false,
                Margin = new Thickness(5, 0, 0, 0)
            };

            stackPanel.Children.Clear();
            stackPanel.Children.Add(txtId);
            stackPanel.Children.Add(txtClave);

        }

        /// <summary>
        /// Agrega los datos del proveedor sin ayuda
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <param name="tmpCostoEntrada"></param>
        private void AgregarDatosProveedorSinAyuda(StackPanel stackPanel, CostoEntradaMateriaPrimaInfo tmpCostoEntrada)
        {
            var txtId = new TextBox
            {
                //Text = tmpCostoEntrada.Provedor.ProveedorID.ToString(CultureInfo.InvariantCulture),
                Text = tmpCostoEntrada.Provedor.CodigoSAP.ToString(CultureInfo.InvariantCulture),
                Width = 79,
                IsEnabled = false
            };
            var txtClave = new TextBox
            {
                Text = tmpCostoEntrada.Provedor.Descripcion,
                Width = 250,
                IsEnabled = false,
                Margin = new Thickness(5, 0, 0, 0)
            };

            stackPanel.Children.Clear();
            stackPanel.Children.Add(txtId);
            stackPanel.Children.Add(txtClave);

        }

        /// <summary>
        /// Agrega la ayuda para buscae proveedores
        /// </summary>
        /// <param name="stackPanel"></param>
        private void AgregarAyudaProveedor(StackPanel stackPanel)
        {

            var proveedorInfo = new ProveedorInfo
            {
                ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()}
                    },

                Activo = EstatusEnum.Activo
            };

            skAyudaProveedor = new SKAyuda<ProveedorInfo>(250, false, proveedorInfo
                , "PropiedadCodigoSapEntradaMateriaPrima"
                , "PropiedadDescripcionEntradaMateriaPrima"
                , false, 80, 10, false);

            skAyudaProveedor.AyudaPL = new ProveedorPL();
            skAyudaProveedor.MensajeClaveInexistente = Properties.Resources.Proveedor_CodigoInvalido;
            skAyudaProveedor.MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_ProveedorSalirSinSeleccionar;
            skAyudaProveedor.MensajeBusqueda = Properties.Resources.Proveedor_Busqueda;
            skAyudaProveedor.MensajeAgregar = Properties.Resources.Proveedor_Seleccionar;
            skAyudaProveedor.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor;
            skAyudaProveedor.TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo;
            skAyudaProveedor.AyudaLimpia += skAyudaProveedor_AyudaLimpia;

            skAyudaProveedor.ObtenerDatos += ObtenerDatosProveedor;
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaProveedor);
        }

        /// <summary>
        /// Agrega la ayuda para la busqueda de costos
        /// </summary>
        /// <param name="stackPanel"></param>
        private void AgregarAyudaCosto(StackPanel stackPanel)
        {
            skAyudaCosto = new SKAyuda<CostoInfo>(250, false, new CostoInfo
            {
                TipoCosto = new TipoCostoInfo { TipoCostoID = (int)TipoCostoEnum.Flete },
                ListaTipoCostos = new List<TipoCostoInfo>
                {
                    new TipoCostoInfo
                    {
                        TipoCostoID = (int)TipoCostoEnum.Flete,
                    },
                    new TipoCostoInfo
                    {
                        TipoCostoID = (int)TipoCostoEnum.MateriaPrima,
                    }

                }
            }
            , "PropiedadClaveOtrosCostos"
            , "PropiedadDescripcionOtrosCostos", "PropiedadOcultaCosteoEntrada", true, true)
            {

                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.CostoOrganizacion_CodigoInvalidoCosteo,
                MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_CostoSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
                BindearCampos = true,
                BindingID = "ClaveCosto",
                BindingDescripcion = "DescripcionCosto"
            };
            
            skAyudaCosto.AyudaLimpia += skAyudaCosto_AyudaLimpia;


            skAyudaCosto.ObtenerDatos += ObtenerDatosCostos;
            skAyudaCosto.AsignaTabIndex(0);
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCosto);
        }

        private void skAyudaCosto_AyudaLimpia(object sender, EventArgs e)
        {
            try
            {
                if (ContenedorEntradaMateriaPrima != null && gridCostos.SelectedIndex > -1)
                {
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].Costos = new CostoInfo
                    {
                        TipoCosto = new TipoCostoInfo { TipoCostoID = (int)TipoCostoEnum.Flete },
                        ListaTipoCostos = new List<TipoCostoInfo>
                            {
                                new TipoCostoInfo
                                {
                                    TipoCostoID = (int)TipoCostoEnum.Flete,
                                },
                                new TipoCostoInfo
                                {
                                    TipoCostoID = (int)TipoCostoEnum.MateriaPrima,
                                }
                            }
                    };
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Onbiene los datos que se muestran con el folio obtenido
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCostos(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                var costoPl = new CostoPL();
                var costo = new CostoInfo { CostoID = int.Parse(clave) };
                costo = costoPl.ObtenerCostoPorID(costo);
                if (costo == null)
                {
                    return;
                }

                var cell = GetCell(gridCostos.SelectedIndex, 1);
                var check = GetVisualChild<CheckBox>(cell);

                switch (costo.AbonoA)
                {
                    case AbonoA.AMBOS:
                        check.IsChecked = false;
                        check.IsEnabled = true;
                        break;
                    case AbonoA.CUENTA:
                        check.IsChecked = true;
                        check.IsEnabled = false;
                        break;
                    case AbonoA.PROVEEDOR:
                        check.IsChecked = false;
                        check.IsEnabled = false;
                        break;
                }

                if (costo.Descripcion == Costo.Fletes.ToString())
                {
                    if (ContenedorEntradaMateriaPrima.Contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrima_contratolibrebordo,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        limpiarTipoCosto();

                        return;
                    }

                    if (ContenedorEntradaMateriaPrima.Contrato.TipoContrato.TipoContratoId !=
                        (int)TipoContratoEnum.BodegaNormal)
                    {

                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrima_contratoNormal,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        limpiarTipoCosto();

                        return;
                    }
                }
                if (costo.TipoCosto.TipoCostoID != (int)TipoCostoEnum.Flete && costo.TipoCosto.TipoCostoID != (int)TipoCostoEnum.MateriaPrima)
                {
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].Costos = null;

                    limpiarTipoCosto();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaMateriaPrima_costoInvalido,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                    return;
                }
                ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].Costos = costo;
                ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].ClaveCosto = costo.ClaveContable;
                ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].DescripcionCosto = costo.Descripcion;

                var costoInfo = new CostoInfo
                {
                    TipoCosto = new TipoCostoInfo { TipoCostoID = (int)TipoCostoEnum.Flete },
                    ListaTipoCostos = new List<TipoCostoInfo>
                        {
                            new TipoCostoInfo
                            {
                                TipoCostoID = (int) TipoCostoEnum.Flete
                            },
                            new TipoCostoInfo
                            {
                                TipoCostoID = (int) TipoCostoEnum.MateriaPrima
                            }
                        }
                };
                skAyudaCosto.Info = costoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Limpia el tipo costo actual
        /// </summary>
        private void limpiarTipoCosto()
        {
            try
            {
                if (gridCostos.ItemsSource != null)
                {
                    if (gridCostos.SelectedIndex >= 0)
                    {
                        var cell = GetCell(gridCostos.SelectedIndex, 0);
                        var stackPanel = GetVisualChild<StackPanel>(cell);
                        AgregarAyudaCosto(stackPanel);
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
                    return;
                }
                ProveedorInfo proveedor = null;


                var proveedorPl = new ProveedorPL();
                proveedor = proveedorPl.ObtenerPorCodigoSAP(new ProveedorInfo { CodigoSAP = clave });

                ProveedorInfo nuevoProveedor = skAyudaProveedor.Info = new ProveedorInfo
                {
                    ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()}
                    },
                    Activo = EstatusEnum.Activo
                };
                if (proveedor == null)
                {
                    return;
                }
                if (proveedor.TipoProveedor.TipoProveedorID == TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() ||
                    proveedor.TipoProveedor.TipoProveedorID == TipoProveedorEnum.ProveedoresFletes.GetHashCode())
                {
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].Provedor = proveedor;
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].NombrePreveedor = proveedor.Descripcion;
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].ProveedoriD = proveedor.ProveedorID.ToString(CultureInfo.InvariantCulture);
                    proveedor.ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo
                        {
                            TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode()
                        },
                        new TipoProveedorInfo {TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()}
                    };
                    skAyudaProveedor.Info = proveedor;
                }
                else
                {
                    DataGridCell cell = GetCell(gridCostos.SelectedIndex, 2);
                    StackPanel panel = GetVisualChild<StackPanel>(cell);

                    skAyudaProveedor.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaMateriaPrima_proveedorInvalido,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].Provedor = nuevoProveedor;
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].NombrePreveedor = string.Empty;
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].ProveedoriD = string.Empty;
                    AgregarAyudaProveedorConDatos(panel, nuevoProveedor);
                }

                skAyudaProveedor.Info = new ProveedorInfo
                {
                    ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()}
                    },
                    Activo = EstatusEnum.Activo
                };

                skAyudaProveedor.Info = nuevoProveedor;

            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        private void AgregarAyudaProveedorConDatos(StackPanel stackPanel, ProveedorInfo proveedor)
        {
            proveedor.Activo = EstatusEnum.Activo;
            proveedor.ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()}
                    };

            skAyudaProveedor = new SKAyuda<ProveedorInfo>(250, false, proveedor
                , "PropiedadCodigoSapEntradaMateriaPrima"
                , "PropiedadDescripcionEntradaMateriaPrima"
                , false, 80, 10, false);

            skAyudaProveedor.AyudaPL = new ProveedorPL();
            skAyudaProveedor.MensajeClaveInexistente = Properties.Resources.Proveedor_CodigoInvalido;
            skAyudaProveedor.MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_ProveedorSalirSinSeleccionar;
            skAyudaProveedor.MensajeBusqueda = Properties.Resources.Proveedor_Busqueda;
            skAyudaProveedor.MensajeAgregar = Properties.Resources.Proveedor_Seleccionar;
            skAyudaProveedor.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor;
            skAyudaProveedor.TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo;
            skAyudaProveedor.AyudaLimpia += skAyudaProveedor_AyudaLimpia;
            skAyudaProveedor.ObtenerDatos += ObtenerDatosProveedor;
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaProveedor);
        }

        private void skAyudaProveedor_AyudaLimpia(object sender, EventArgs e)
        {
            if (ContenedorEntradaMateriaPrima != null && gridCostos.SelectedIndex > -1)
            {
                ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima[gridCostos.SelectedIndex].Provedor = new ProveedorInfo
                {
                    ListaTiposProveedor = new List<TipoProveedorInfo>
                        {
                            new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                            new TipoProveedorInfo{TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()}
                        },

                    Activo = EstatusEnum.Activo
                };
            }
        }



        public bool obtenerParametrosRestriccionDescuentoProductoSK()
        {
            ParametroGeneralBL parametroGenBL = new ParametroGeneralBL();
            ParametroGeneralInfo parametroGeneral;
            parametroGeneral = parametroGenBL.ObtenerPorClaveParametroActivo(ParametrosEnum.ProductoConRestriccionDescSK.ToString());
            if (parametroGeneral != null && parametroGeneral.Activo == EstatusEnum.Activo)
            {
                productoConRestriccion = parametroGeneral.Valor;
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgErrorParametroProductoRestriccion, MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
            parametroGeneral = parametroGenBL.ObtenerPorClaveParametroActivo(ParametrosEnum.PorcentajeMaxDesctoProductoSK.ToString());
            if (parametroGeneral != null && parametroGeneral.Activo == EstatusEnum.Activo)
            {
                try
                {
                    porcentajeDescuento = decimal.Parse(parametroGeneral.Valor).ToString();
                }
                catch(Exception)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgErrorParametroPorcentajeDesc, MessageBoxButton.OK, MessageImage.Error);
                    return false;
                }
                //porcentajeDescuento = parametroGeneral.Valor;
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgErrorParametroPorcentajeDesc, MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
            return true;
        }

        public bool validarRestriccionDescuento(string idProducto, string proveedorMateriaPrima, string PesoNegociado)
        {
            bool resultado = false;
            string[] productos = null;


            if (proveedorMateriaPrima.ToUpper().Trim() == "SUKARNE SA DE CV" && PesoNegociado.ToUpper().Trim() == PesoNegociarEnum.Origen.ToString().ToUpper().Trim())
            {
                if (productoConRestriccion.Trim().Length > 0)
                {
                    productos = productoConRestriccion.Split('|');
                    if (productos.Contains(idProducto))
                    {
                        resultado = true;
                    }
                }
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene los datos del folio obtenido en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosFolio(string clave)
        {
            try
            {
                LimpiarDatos();
                EntradaProductoInfo productoActual = skAyudaFolioEntrada.Info;
                skAyudaFolioEntrada.Clave = productoActual.Folio.ToString();
                var entradaProductoPl = new EntradaProductoPL();
                productoActual = entradaProductoPl.ObtenerEntradaProductoPorFolio(productoActual.Folio,
                    productoActual.Organizacion.OrganizacionID);

                RecepcionDeMateriaPrima entMP = new RecepcionDeMateriaPrima();
                if (productoActual.Producto.SubFamilia.SubFamiliaID !=
                    (int)SubFamiliasEnum.MicroIngredientes)
                {
                    aplicarValidacionRestriccionDesc = validarRestriccionDescuento(productoActual.Contrato.Producto.ProductoId.ToString(), productoActual.Contrato.Proveedor.Descripcion, productoActual.Contrato.PesoNegociar);
                }
                //aplicarValidacionRestriccionDesc = validarRestriccionDescuento(productoActual.Contrato.Producto.ProductoId.ToString(), productoActual.Contrato.Proveedor.Descripcion, productoActual.Contrato.PesoNegociar);

                if (productoConRestriccion.Length == 0)
                {
                    SkMessageBox.Show(
                                                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.EntradaMateriaPrima_MsgSinConfiguracionProductoRestriccion,
                                                MessageBoxButton.OK,
                                                MessageImage.Stop);
                    //EntradaMateriaPrima_MsgSinConfiguracionProductoRestriccion
                    return;
                }
                if (porcentajeDescuento.Length == 0)
                {
                    SkMessageBox.Show(
                                                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.EntradaMateriaPrima_MsgSinConfiguracionPorcentajeDescuento,
                                                MessageBoxButton.OK,
                                                MessageImage.Stop);
                    return;
                }
                //aplicarValidacionRestriccionDesc = false;

                //aplicarValidacionRestriccionDesc = aplicarValidacionRestriccionDesc;
                if (productoActual.AlmacenMovimiento.AlmacenMovimientoID == 0)
                {
                    if (productoActual.Estatus.EstatusId == (int)Estatus.Aprobado ||
                        productoActual.Estatus.EstatusId == (int)Estatus.Autorizado)
                    {
                        if (((productoActual.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes) ||
                              ((productoActual.Contrato.PesoNegociar == PesoNegociarEnum.Destino.ToString() &&
                                productoActual.PesoOrigen >= 0) ||
                               (productoActual.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString() &&
                                productoActual.PesoOrigen > 0)
                               )) && productoActual.PesoBruto > 0 && productoActual.PesoTara > 0)
                        {

                            txtFechaEntrada.Text = DateTime.Now.ToShortDateString();

                            ContenedorEntradaMateriaPrima.EntradaProducto = productoActual;

                            if (productoActual.Contrato != null)
                            {
                                if (productoActual.Contrato.TipoCambio != null &&
                                    productoActual.Contrato.TipoCambio.Descripcion == TipoCambioEnum.Dolar.ToString().ToUpper())
                                {
                                    TipoCambioPL tipoCambioPl = new TipoCambioPL();
                                    var listaTipoCambio = tipoCambioPl.ObtenerPorFechaActual();
                                    if (listaTipoCambio != null)
                                    {
                                        var tipoCambio =
                                            listaTipoCambio.First(
                                                registro =>
                                                    registro.Descripcion.ToUpper() ==
                                                    TipoCambioEnum.Dolar.ToString().ToUpper());
                                        if (tipoCambio != null)
                                        {
                                            productoActual.Contrato.Precio = productoActual.Contrato.PrecioConvertido *
                                                                             tipoCambio.Cambio;
                                        }
                                        else
                                        {
                                            SkMessageBox.Show(
                                                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.EntradaMateriaPrima_MsgSinConfiguracionTipoCambio,
                                                MessageBoxButton.OK,
                                                MessageImage.Stop);
                                            LimpiarTodo();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        SkMessageBox.Show(
                                                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.EntradaMateriaPrima_MsgSinConfiguracionTipoCambio,
                                                MessageBoxButton.OK,
                                                MessageImage.Stop);
                                        LimpiarTodo();
                                        return;
                                    }
                                }

                                var programacionFletesPl = new ProgramaciondeFletesPL();
                                var fleteContrato =
                                    programacionFletesPl.ObtenerFletes(
                                        productoActual.Contrato);

                                if (fleteContrato != null)
                                {
                                    if (productoActual.RegistroVigilancia.ProveedorChofer != null)
                                    {
                                        txtTipoTarifa.Text =
                                            fleteContrato.First(registro => registro.Flete.Proveedor.ProveedorID ==
                                                                            productoActual.RegistroVigilancia.ProveedorChofer.Proveedor.ProveedorID)
                                                .Flete.TipoTarifa.Descripcion;
                                    }
                                }

                                txtCompra.Text =
                                    productoActual.Contrato.Parcial.ToString();

                                if (productoActual.Contrato.Parcial ==
                                    CompraParcialEnum.Parcial)
                                {
                                    btnVerEntradas.IsEnabled = true;
                                }
                                else
                                {
                                    btnVerEntradas.IsEnabled = false;
                                }
                            }

                            txtKilosOrigenBonificacion.Value = ContenedorEntradaMateriaPrima.EntradaProducto.PesoBonificacion;

                            ContratoInfo contratoActual = productoActual.Contrato;
                            if (contratoActual != null)
                            {
                                txtNumeroProveedor.Text =
                                    contratoActual.Proveedor.ProveedorID.ToString(CultureInfo.InvariantCulture);
                                txtNombreProveedor.Text = contratoActual.Proveedor.Descripcion;

                                ContenedorEntradaMateriaPrima.Contrato = contratoActual;
                            }

                            CboTipoEntrada.ItemsSource = Enum.GetValues(typeof(TipoEntradaEnum));
                            CboTipoEntrada.SelectedIndex = 0;

                            
                                CalcularMerma(contratoActual, productoActual);
                            if (aplicarValidacionRestriccionDesc)
                            {
                                txtMerma.Text = productoActual.PesoDescuento.ToString();
                                txtMermaDecimales.Text = porcentajeDescuento;
                                txtKilosEntrada.Text = (productoActual.PesoOrigen - productoActual.PesoDescuento).ToString();
                                //txtPrecio.Text = productoActual.
                            }

                            decimal descuentos = 0;
                            if (productoActual.ProductoDetalle != null)
                            {
                                if (ContenedorEntradaMateriaPrima.Contrato.AplicaDescuento == 1)
                                {
                                    if (productoActual.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Granos)
                                    {
                                        foreach (var detalle in productoActual.ProductoDetalle
                                            .Where(t => t.Indicador.IndicadorId == (int)IndicadoresEnum.Humedad ||
                                                        t.Indicador.IndicadorId == (int)IndicadoresEnum.Impurezas ||
                                                        t.Indicador.IndicadorId == (int)IndicadoresEnum.Danostotales))
                                        {
                                            if (detalle.ProductoMuestras != null)
                                            {
                                                descuentos = detalle.ProductoMuestras.Aggregate(descuentos,
                                                                                                (current, muestra) =>
                                                                                                current +
                                                                                                muestra.Descuento);
                                            }
                                        }
                                    }
                                    else if (productoActual.Producto.SubFamilia.SubFamiliaID ==
                                             (int)SubFamiliasEnum.Forrajes)
                                    {
                                        foreach (var entradaProductoDetalle in productoActual.ProductoDetalle)
                                        {
                                            foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras)
                                            {
                                                descuentos += productoMuestra.Descuento;
                                                break;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    // Si es otra subfamilia se hace la validacion que se hacia antes del cambio de la calidad origen.
                                    {
                                        foreach (var entradaProductoDetalle in productoActual.ProductoDetalle)
                                        {
                                            foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras)
                                            {
                                                descuentos += productoMuestra.Descuento;
                                            }
                                        }
                                    }
                                }
                            }

                            ProductoInfo producto = productoActual.Producto;
                            if (producto != null)
                            {
                                ContenedorEntradaMateriaPrima.Producto = producto;
                                txtProducto.Text = producto.ProductoId.ToString(CultureInfo.InvariantCulture);
                                txtDescripcionProducto.Text = producto.ProductoDescripcion;
                            }

                            txtKilosOrigen.Value = productoActual.PesoOrigen;

                            var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                            AlmacenInventarioLoteInfo almacenIncentarioLote =
                                almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorId(
                                    productoActual.AlmacenInventarioLote.AlmacenInventarioLoteId);
                            if (almacenIncentarioLote != null)
                            {
                                txtLote.Text = almacenIncentarioLote.Lote.ToString(CultureInfo.InvariantCulture);
                            }

                            ObtenerCostosFletes();

                            //Validar si es una premezcla
                            if (productoActual.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes)
                            {
                                btnVer.Visibility = Visibility.Visible;
                                CboTipoEntrada.SelectedItem = TipoEntradaEnum.Compra;
                                CboTipoEntrada.IsEnabled = false;
                                txtPrecio.IsEnabled = true;
                                txtPrecio.Focus();
                            }

                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.EntradaMateriaPrima_MsgSinPeso,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            LimpiarTodo();
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrima_MsgEstatusIncorrecto,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        LimpiarTodo();
                    }

                }
                else
                {
                    skAyudaFolioEntrada.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaMateriaPrima_AyudaFolioInvalido,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para calcular la merma de un producto
        /// </summary>
        /// <param name="contrato"></param>
        /// <param name="producto"></param>
        private void CalcularMerma(ContratoInfo contrato, EntradaProductoInfo producto)
        {
            try
            {
  				decimal descuentos = 0;
                if (producto.Contrato.AplicaDescuento == 1)
                {
                    if (producto.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Granos)
                    {
                        if (producto.ProductoDetalle != null)
                        {
                            foreach (var detalle in producto.ProductoDetalle
                                .Where(t => t.Indicador.IndicadorId == (int)IndicadoresEnum.Humedad ||
                                            t.Indicador.IndicadorId == (int)IndicadoresEnum.Impurezas ||
                                            t.Indicador.IndicadorId == (int)IndicadoresEnum.Danostotales))
                            {
                           if (detalle.ProductoMuestras != null)
                                {
                                    descuentos = detalle.ProductoMuestras.Aggregate(descuentos,
                                                                                    (current, muestra) =>
                                                                                    current + muestra.Descuento);
                                }
                            }
                        }
                    }
                    else if (producto.Producto.SubFamilia.SubFamiliaID ==
                                (int)SubFamiliasEnum.Forrajes)
                    {
                        foreach (var entradaProductoDetalle in producto.ProductoDetalle)
                        {
                            foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras)
                            {
                                descuentos += productoMuestra.Descuento;
                                break;
                            }
                            break;
                        }
                    }
                    else
                    // Si es otra subfamilia se hace la validacion que se hacia antes del cambio de la calidad origen.
                    {
                        foreach (var entradaProductoDetalle in producto.ProductoDetalle)
                        {
                            foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras)
                            {
                                descuentos += productoMuestra.Descuento;
                            }
                        }
                    }
                }
                decimal pesoNeto = ((producto.PesoBruto - producto.PesoTara) - ((producto.PesoBruto - producto.PesoTara) * descuentos / 100));

                if (contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString())
                {
                    txtMerma.Value = producto.PesoOrigen - (pesoNeto + ((producto.PesoBruto - producto.PesoTara) * descuentos / 100));
                    txtMermaDecimales.Value = decimal.Round(
                        (((producto.PesoOrigen - (pesoNeto + ((producto.PesoBruto - producto.PesoTara) * descuentos / 100))) / producto.PesoOrigen) * 100), 2);
                }
                else
                {
                    txtMerma.Value = 0;
                    txtMermaDecimales.Value = (decimal)0.00;
                }
                txtKilosEntrada.Value = pesoNeto;

                if (contrato.Parcial == CompraParcialEnum.Total || contrato.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaTercero.GetHashCode())
                {
                    if (contrato.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaNormal.GetHashCode())
                        txtPrecio.Text = contrato.Precio.ToString("N4");
                    else
                    {
                        var entradaMateriaPrimaPl = new EntradaMateriaPrimaPL();
                        txtPrecio.Text = entradaMateriaPrimaPl.ObtenerPrecioOrigen(contrato).ToString("N4");
                    }
                }
                else
                {
                    txtPrecio.Text = "0";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Limpia los datos actuales
        /// </summary>
        private void LimpiarDatos()
        {
            txtDescripcionProducto.Text = string.Empty;
            txtFechaEntrada.Text = string.Empty;
            txtKilosEntrada.Text = string.Empty;
            txtKilosOrigen.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtMerma.Text = string.Empty;
            txtMermaDecimales.Text = string.Empty;
            txtNombreProveedor.Text = string.Empty;
            txtNumeroProveedor.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtKilosOrigenBonificacion.Text = string.Empty;
            txtCompra.Text = string.Empty;
            txtTipoTarifa.Text = string.Empty;
            txtProducto.Text = string.Empty;
            renglonGrid = -1;
            renglonGridCuentaProveedor = -1;
           	cargarInicial = true; 
			numeroRenglon = -1;
            txtPrecio.IsEnabled = false;
            CboTipoEntrada.ItemsSource = null;
            btnVerEntradas.IsEnabled = false;
            CboTipoEntrada.IsEnabled = true;
            btnVer.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Metodo que se ejecuta cuando no se encuentran datos del folio proporcionado
        /// </summary>
        private void LimpiarTodo()
        {
            try
            {
                LimpiarDatos();
                if (ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima != null)
                {
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Clear();
                }
                InicializaContexto();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene los costos de los fletes
        /// </summary>
        private void ObtenerCostosFletes()
        {
            try
            {
                string entrada = String.IsNullOrEmpty(txtKilosEntrada.Text) ? "0" : txtKilosEntrada.Value.ToString();
                decimal kilosEntrada = decimal.Parse(entrada);
                if (txtKilosOrigen.Value != null)
                {
                    decimal kilosOrigen = (decimal)txtKilosOrigen.Value;
                    var costoEntradaMateriaPrimaPesos = new CostoEntradaMateriaPrimaInfo
                    {
                        KilosEntrada = kilosEntrada,
                        KilosOrigen = kilosOrigen
                    };
                    if (ContenedorEntradaMateriaPrima.Contrato.PesoNegociar == PesoNegociarEnum.Destino.ToString())
                    {
                        costoEntradaMateriaPrimaPesos.KilosOrigen = costoEntradaMateriaPrimaPesos.KilosEntrada;
                    }
                    else if (ContenedorEntradaMateriaPrima.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString())
                    {
                        costoEntradaMateriaPrimaPesos.KilosOrigen =
                            ContenedorEntradaMateriaPrima.EntradaProducto.PesoOrigen;
                    }
                    renglonGrid = -1;
                    renglonGridCuentaProveedor = -1;
                    numeroRenglon = -1;
                    var entradaMateriaPrimaPl = new EntradaMateriaPrimaPL();
                    List<CostoEntradaMateriaPrimaInfo> lista =
                        entradaMateriaPrimaPl.ObtenerCostosFletes(ContenedorEntradaMateriaPrima,
                            costoEntradaMateriaPrimaPesos);

                    if (lista != null)
                    {
                        foreach (var tmpResultado in lista)
                        {
                            if (tmpResultado.Costos.CostoID == Costo.Fletes.GetHashCode())
                            {
                                var fletePl = new ProgramaciondeFletesPL();
                                var fletes = fletePl.ObtenerFletes(ContenedorEntradaMateriaPrima.Contrato);
                                var flete = fletes.First(registro => registro.Flete.Proveedor.ProveedorID ==
                                                                     ContenedorEntradaMateriaPrima.EntradaProducto
                                                                         .RegistroVigilancia.ProveedorChofer.Proveedor
                                                                         .ProveedorID);
                                if (flete != null)
                                {
                                    //Si el tipo tarifa es por viaje, actualizamos los kilos origen para que tome el importe del flete y lo deje como fue capturado, si es por 
                                    //tonelada, la tarifa se divide entre mil (kilos por tonelada) y multiplica la tarifa por kilos recibidos
                                    if (flete.Flete.TipoTarifa.TipoTarifaId == TipoTarifaEnum.Viaje.GetHashCode())
                                    {
                                        tmpResultado.KilosOrigen = 1;
                                        tmpResultado.Importe = tmpResultado.FleteDetalle.Tarifa * 1;
                                    }
                                    else if (flete.Flete.TipoTarifa.TipoTarifaId == TipoTarifaEnum.Tonelada.GetHashCode())
                                    {

                                        tmpResultado.Importe = tmpResultado.FleteDetalle.Tarifa *
                                                               (costoEntradaMateriaPrimaPesos.KilosOrigen / 1000);
                                    }

                                    decimal mermaActual = (decimal)txtMermaDecimales.Value;
                                    decimal mermaPermitida = flete.Flete.MermaPermitida;
                                    //Nuevos calculos solicitados
                                    if (mermaActual > mermaPermitida)
                                    {
                                        decimal diferenciaMerma = ((mermaActual - mermaPermitida) * kilosOrigen) / 100;
                                        diferenciaMerma = diferenciaMerma * Convert.ToDecimal(txtPrecio.Text);
                                        tmpResultado.Importe = tmpResultado.Importe - diferenciaMerma;
                                    }
                                }
                            }
                            else
                            {
                                //Todos los costos que no sean fletes se divide entre mil la tarifa
                                tmpResultado.Importe = tmpResultado.FleteDetalle.Tarifa *
                                                       (costoEntradaMateriaPrimaPesos.KilosOrigen / 1000);
                            }
                        }
                    }
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima = lista.ConvertirAObservable();
                }
                gridCostos.DataContext = ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima;
                AgregarNuevaLinea(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene el porcentaje de la merma permitida
        /// </summary>
        private decimal ObtenerPorcentajeMermaPermitida(EntradaProductoInfo entradaActual)
        {
            decimal mermaPermitida = 0;
            try
            {
                EntradaMateriaPrimaPL entradaMateriaPrimaPl = new EntradaMateriaPrimaPL();

                CostoEntradaMateriaPrimaInfo resultado = entradaMateriaPrimaPl.ObtenerMermaPermitida(entradaActual);

                if (resultado != null)
                {
                    mermaPermitida = resultado.Flete.MermaPermitida;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return mermaPermitida;
        }

        /// <summary>
        /// Agrega los datos de la cuenta sin ayuda
        /// </summary>
        /// <param name="stackPanel"></param>
        /// <param name="tmpCostoEntrada"></param>
        private void AgregarDatosCuentaSinAyuda(StackPanel stackPanel, CostoEntradaMateriaPrimaInfo tmpCostoEntrada)
        {
            var txtId = new TextBox
            {
                Text = tmpCostoEntrada.Costos.ClaveContable,
                Width = 50,
                IsEnabled = false
            };
            var txtClave = new TextBox
            {
                Text = tmpCostoEntrada.Costos.Descripcion,
                Width = 250,
                IsEnabled = false,
                Margin = new Thickness(10, 0, 0, 0)
            };
            stackPanel.Children.Clear();
            stackPanel.Children.Add(txtId);
            stackPanel.Children.Add(txtClave);

        }

        /// <summary>
        /// Agrega nueva linea al grid de costos
        /// </summary>
        private void AgregarNuevaLinea(bool primerLinea)
        {
            if (ContenedorEntradaMateriaPrima.EntradaProducto == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaMateriaPrima_SeleccionarFolio,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
            }
            else
            {
 				nuevaLinea = true;
                if (ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima == null || ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Count == 0)
                {
                    var lista = new ObservableCollection<CostoEntradaMateriaPrimaInfo>();
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima = new ObservableCollection<CostoEntradaMateriaPrimaInfo>();
                    ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima = lista.ConvertirAObservable();
                    gridCostos.DataContext = ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima;
                }
                ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Add(new CostoEntradaMateriaPrimaInfo());
            }
        }

        /// <summary>
        /// Valida si existen datos requeridos
        /// </summary>
        /// <returns></returns>
        private bool ValidarDatos()
        {
            renglon = 0;
            if (ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima == null || ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Count == 0)
            {
                return true;
            }
            foreach (CostoEntradaMateriaPrimaInfo costoEntradaMateriaPrimaInfo in
                       ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima)
            {
                if (costoEntradaMateriaPrimaInfo.Costos == null)
                {
                    columna = 0;
                    return false;
                }
                if (costoEntradaMateriaPrimaInfo.TieneCuenta)
                {
                    if (string.IsNullOrEmpty(costoEntradaMateriaPrimaInfo.CuentaSap))
                    {
                        columna = 2;
                        return false;
                    }

                }
                else
                {
                    if (costoEntradaMateriaPrimaInfo.Provedor == null)
                    {
                        columna = 2;
                        return false;
                    }
                }
                if (costoEntradaMateriaPrimaInfo.Importe == 0)
                {
                    columna = 3;
                    return false;
                }

                renglon++;
            }
            return true;
        }


        /// <summary>
        /// Obtiene el Control Hijo del tipo que se especifica
        /// </summary>
        /// <param name="parent">Control donde se buscará el Tipo de Control especificado</param>
        /// <returns>T</returns>
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            var child = default(T);
            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T ?? GetVisualChild<T>(v);
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        /// <summary>
        /// Obtiene un Renglon en base al Indice del mismo
        /// </summary>
        /// <param name="row">Indice del Renglon a Buscar</param>
        /// <param name="column">Indice de la Columna a Buscar</param>
        /// <returns>DataGridCell</returns>
        public DataGridCell GetCell(int row, int column)
        {
            var rowContainer = GetRow(row);
            if (rowContainer != null)
            {
                var presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    gridCostos.ScrollIntoView(rowContainer, gridCostos.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        /// <summary>
        /// Obtiene un Renglon en base al Indice del mismo
        /// </summary>
        /// <param name="index">Indice del Renglon a Buscar</param>
        /// <returns>DataGridRow</returns>
        public DataGridRow GetRow(int index)
        {
            var row = (DataGridRow)gridCostos.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                gridCostos.UpdateLayout();
                gridCostos.ScrollIntoView(gridCostos.Items[index]);
                row = (DataGridRow)gridCostos.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        /// <summary>
        /// Evento preview key down del grid de datos
        /// </summary>
        /// <param name="sender">Objeto que invoco el evento</param>
        /// <param name="e"></param>
        private void gridCostos_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Send(Key.Tab);
            }

        }

        /// <summary>
        /// Envia tecla
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

        /// <summary>
        /// Envia notificacion al proveedor sobre la recepcion de materia prima en caso de tener flete
        /// </summary>
        /// <returns></returns>
        private Boolean enviarCorreo()
        {
            String sCorreo = String.Empty;
            String sMensaje = String.Empty;
            String sProveedor = String.Empty;
            OrganizacionInfo organizacion = new OrganizacionInfo();
            Decimal importeFlete = 0;
            ProveedorPL proveedorPl = new ProveedorPL();
            CorreoInfo correoenviar = new CorreoInfo();
            CorreoPL correo = new CorreoPL();
            ProveedorInfo proveedor = new ProveedorInfo();

            List<CostoEntradaMateriaPrimaInfo> ListaCostoEntradaMateriaPrima = ContenedorEntradaMateriaPrima.ListaCostoEntradaMateriaPrima.Where(item => item.Costos.CostoID == (int)Costo.Fletes).ToList();

            if (ListaCostoEntradaMateriaPrima != null && ListaCostoEntradaMateriaPrima.Count > 0)
            {
                organizacion.OrganizacionID = organizacionId;
                List<ProveedorInfo> listaProveedores = (from costo in ListaCostoEntradaMateriaPrima where costo.Provedor != null select costo.Provedor).ToList();
                var lsProveedores = new List<int>();

                if (listaProveedores.Count > 0)
                {
                    lsProveedores = listaProveedores.Select(prov => prov.ProveedorID).Distinct().ToList();
                }

                foreach(int lsProveedor in lsProveedores)
                {
                    importeFlete = 0;
                    importeFlete = Math.Round(Convert.ToDecimal(ListaCostoEntradaMateriaPrima.Where(entrada => entrada.Provedor.ProveedorID == lsProveedor).Sum(imp => imp.Importe)), 2);
                    sProveedor = ListaCostoEntradaMateriaPrima.Where(entrada => entrada.Provedor.ProveedorID == lsProveedor).Select(prov => prov.Provedor.Descripcion).FirstOrDefault();
                    sMensaje = String.Format(
                        "FOLIO ENTRADA: {0}<BR><BR>PROVEEDOR FLETE: {1}<BR><BR>PESO ORIGEN: {2}<BR><BR>PESO LLEGADA: {3}<BR><BR>MERMA: {4}<BR><BR>PORCENTAJE MERMA: {5}%<BR><BR>IMPORTE FLETE: ${6} <BR><BR>",
                        skAyudaFolioEntrada.Clave,
                        sProveedor,
                        ContenedorEntradaMateriaPrima.EntradaProducto.PesoOrigen.ToString("#,##0", new CultureInfo("en-US")),
                        txtKilosEntrada.Text,
                        txtMerma.Text,
                        txtMermaDecimales.Text,
                        importeFlete.ToString("#,##0.00", new CultureInfo("en-US"))
                        );

                    proveedor = proveedorPl.ObtenerPorIDConCorreo(lsProveedor);
                    sCorreo = proveedor.Correo == null ? "" : proveedor.Correo;

                    if (sCorreo.Trim().Length > 0)
                    {
                        correoenviar = new CorreoInfo();

                        correoenviar.Asunto = "Entrada de Materia Prima";
                        correoenviar.Correos = new List<string>();
                        correoenviar.Mensaje = sMensaje;
                        correoenviar.Correos.Add(sCorreo);

                        correo.EnviarCorreo(organizacion, correoenviar);
                    }
                }
            }

            return true;
        }

        #endregion

    }
}