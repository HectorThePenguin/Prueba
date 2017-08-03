using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
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

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para FacturacionSacrificio.xaml
    /// </summary>
    public partial class FacturacionSacrificio
    {
        private SKAyuda<ClienteInfo> skAyudaCliente;
        private int ordenSacrificioId;
        private int organizacionId;
        private int usuarioId;

        public FacturacionSacrificio()
        {
            InitializeComponent();
        }

        #region Metodos
        /// <summary>
        /// Agrega la ayuda de clientes
        /// </summary>
        private void AgregarAyudaCliente()
        {
            try
            {
                skAyudaCliente = new SKAyuda<ClienteInfo>(200,
                    false,
                    new ClienteInfo(),
                    "PropiedadCodigoCliente",
                    "PropiedadDescripcionCliente",
                    true,
                    80,
                    10,
                    false)
                {
                    AyudaPL = new ClientePL(),
                    MensajeClaveInexistente = Properties.Resources.FacturacionSacrificio_AyudaClienteInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.FacturacionSacrificio_AyudaClienteSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.SalidaVentaTraspaso_AyudaClienteBusqueda,
                    MensajeAgregar = Properties.Resources.SalidaVentaTraspaso_AyudaClienteSeleccionar,
                    TituloEtiqueta = Properties.Resources.FacturacionSacrificio_AyudaClienteLeyendaBusqueda,
                    TituloPantalla = Properties.Resources.SalidaVentaTraspaso_AyudaClienteBusqueda_Titulo,

                };

                splAyudaCliente.Children.Clear();
                splAyudaCliente.Children.Add(skAyudaCliente);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void Limpiar()
        {
            var fechaPl = new FechaPL();
            var fechaServidor = fechaPl.ObtenerFechaActual();
            ordenSacrificioId = 0;
            dtpFechaSacrificio.DisplayDate = fechaServidor.FechaActual;
            dtpFechaSacrificio.SelectedDate = fechaServidor.FechaActual;
            txtObservaciones.Text = string.Empty;
            txtNombreCliente.Text = string.Empty;
            txtFolioOrdenSacrificio.Text = string.Empty;
            txtFechaFactura.Text = string.Empty;
            txtCorrales.Text = string.Empty;
            txtCodigoSapCliente.Text = string.Empty;
            txtCabezas.Text = string.Empty;
            skAyudaCliente.LimpiarCampos();
            skAyudaCliente.Info = new ClienteInfo();
            listaFacturas.ItemsSource = null;
        }

        private bool ValidarDatos()
        {
            if (rdGenerarFactura.IsChecked != null && (bool) rdGenerarFactura.IsChecked)
            {
                if (dtpFechaSacrificio.SelectedDate == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.FacturacionSacrificio_MensajeSeleccionarFecha,
                        MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }

                if (dtpFechaSacrificio.SelectedDate != null)
                {
                    var fechaServidorPl = new FechaPL();
                    var fechaServidor = fechaServidorPl.ObtenerFechaActual();

                    DateTime fechaSeleccionada = dtpFechaSacrificio.SelectedDate.Value;
                    if (fechaSeleccionada.Date == fechaServidor.FechaActual.Date)
                    {
                        var loteSacrificioPl = new LoteSacrificioPL();
                        var datos = loteSacrificioPl.ObtenerLoteSacrificio(fechaSeleccionada.AddDays(-1),
                                                                           AuxConfiguracion.ObtenerOrganizacionUsuario());
                        if (datos != null)
                        {
                            if (datos.OrdenSacrificioId != 0)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.FacturacionSacrificio_MensajeNoSePuedeGenerarLaFactura,
                                    MessageBoxButton.OK, MessageImage.Warning);
                                return false;
                            }
                        }
                    }
                }

                if (ordenSacrificioId == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.FacturacionSacrificio_MensajeNoSeHaSeleccionadoOrdenSacrificio, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }

                if (skAyudaCliente.Clave.Trim() == "")
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.FacturacionSacrificio_MensajeDebeCapturarCliente, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
            }

            if (rdCancelarFactura.IsChecked != null && (bool)rdCancelarFactura.IsChecked)
            {
                if (ordenSacrificioId == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.FacturacionSacrificio_MensajeNoSeHaSeleccionadoOrdenSacrificio, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }

                if (txtObservaciones.Text.Trim() == "")
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.FacturacionSacrificio_MensajeDebeCapturarObservaciones, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }

                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    String.Format(Properties.Resources.FacturacionSacrificio_MensajeEstaSeguroCancelarFactura, txtFechaFactura.Text),
                    MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        private void BuscarCancelacion()
        {
            try
            {
                var loteSacrificioPl = new LoteSacrificioPL();
                int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                
                TipoSacrificioEnum tipoSacrificio = (TipoSacrificioEnum)cboTipoSacrificio.SelectedItem;
                bool traspasoGanado = tipoSacrificio == TipoSacrificioEnum.Traspaso;
                LoteSacrificioInfo loteCancelar;
                if (traspasoGanado)
                {
                    loteCancelar =
                        loteSacrificioPl.ObtenerLoteSacrificioACancelarLucero(organizacionID);
                }
                else
                {
                    loteCancelar =
                        loteSacrificioPl.ObtenerLoteSacrificioACancelar(organizacionID);
                }
                if (loteCancelar != null)
                {
                    ordenSacrificioId = loteCancelar.OrdenSacrificioId;
                    txtFechaFactura.Text = loteCancelar.Fecha.ToShortDateString().ToString(CultureInfo.InvariantCulture);
                    txtCodigoSapCliente.Text = loteCancelar.Cliente.CodigoSAP;
                    txtNombreCliente.Text = loteCancelar.Cliente.Descripcion;
                    List<LoteSacrificioInfo> loteDetalleCancelar;
                    if (traspasoGanado)
                    {
                        ordenSacrificioId = 1;
                        loteDetalleCancelar =
                            loteSacrificioPl.ObtenerFacturasPorOrdenSacrificioACancelarLucero(organizacionID);
                    }
                    else
                    {
                        loteDetalleCancelar =
                            loteSacrificioPl.ObtenerFacturasPorOrdenSacrificioACancelar(loteCancelar.OrdenSacrificioId);
                    }
                    if (loteDetalleCancelar != null)
                    {
                        listaFacturas.DisplayMemberPath = "FolioFactura";
                        listaFacturas.ItemsSource = loteDetalleCancelar;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.FacturacionSacrificio_MensajeNoExistenFacturasParaCancelar,
                                       MessageBoxButton.OK, MessageImage.Warning);
                    Limpiar();
                    rdGenerarFactura.IsChecked = true;
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
                Limpiar();
                rdGenerarFactura.IsChecked = true;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.FacturacionSacrificio_MensajeErrorBuscarCanceladas,
                                       MessageBoxButton.OK, MessageImage.Warning);
                Limpiar();
                rdGenerarFactura.IsChecked = true;
            }
        }
        #endregion

        #region Eventos
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var loteSacrificioPl = new LoteSacrificioPL();

                txtFolioOrdenSacrificio.Text = string.Empty;
                txtCabezas.Text = string.Empty;
                txtCorrales.Text = string.Empty;
                skAyudaCliente.LimpiarCampos();
                skAyudaCliente.Info = new ClienteInfo();
                ordenSacrificioId = 0;

                if (dtpFechaSacrificio.SelectedDate != null)
                {
                    DateTime fechaSeleccionada = dtpFechaSacrificio.SelectedDate.Value;

                    int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                   
                    TipoSacrificioEnum tipoSacrificio = (TipoSacrificioEnum) cboTipoSacrificio.SelectedItem;
                    bool traspasoGanado = tipoSacrificio == TipoSacrificioEnum.Traspaso;
                    LoteSacrificioInfo datos;
                    if (traspasoGanado)
                    {
                        datos = loteSacrificioPl.ObtenerLoteSacrificioLucero(fechaSeleccionada, organizacionID);
                    }
                    else
                    {
                        datos = loteSacrificioPl.ObtenerLoteSacrificio(fechaSeleccionada, organizacionID);
                    }
                    if (datos != null)
                    {
                        if (datos.OrdenSacrificioId > 0)
                        {
                            ordenSacrificioId = datos.OrdenSacrificioId;
                            txtFolioOrdenSacrificio.Text = datos.FolioOrdenSacrificio.ToString("N0",
                                CultureInfo.InvariantCulture);
                            txtCabezas.Text = datos.Cabezas.ToString("N0", CultureInfo.InvariantCulture);
                            txtCorrales.Text = datos.Corrales.ToString("N0", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.FacturacionSacrificio_MensajeNoExistenOrdenes,
                                MessageBoxButton.OK, MessageImage.Warning);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.FacturacionSacrificio_MensajeNoExistenOrdenes,
                            MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.FacturacionSacrificio_MensajeSeleccionarFecha,
                            MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.FacturacionSacrificio_MensajeErrorAlBuscar,
                                       MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        private void FacturacionSacrificio_OnLoaded(object sender, RoutedEventArgs e)
        {
            AgregarAyudaCliente();
            Limpiar();
            rdGenerarFactura.IsChecked = true;
            organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var parametroGeneralPL = new ParametroGeneralPL();
            ParametroGeneralInfo parametroGanaderaTraspasa =
                parametroGeneralPL.ObtenerPorClaveParametro(ParametrosEnum.GANADERATRASPASAGANADO.ToString());
            var traspasoGanado = false;
            if (parametroGanaderaTraspasa != null)
            {
                traspasoGanado =
                    parametroGanaderaTraspasa.Valor.Split('|').ToList().Any(
                        dato => Convert.ToInt32(dato) == organizacionId);
            }
            if (traspasoGanado)
            {
                splTipoSacrificion.Visibility = Visibility.Visible;
            }
        }

        private void RdGenerarFactura_OnChecked(object sender, RoutedEventArgs e)
        {
            grupoGenerarFactura.Visibility = Visibility.Visible;
            grupoBusqueda.Visibility = Visibility.Visible;
            grupoCancelarFactura.Visibility = Visibility.Hidden;
            Limpiar();
        }

        private void RdCancelarFactura_OnChecked(object sender, RoutedEventArgs e)
        {
            grupoCancelarFactura.Visibility = Visibility.Visible;
            grupoBusqueda.Visibility = Visibility.Hidden;
            grupoGenerarFactura.Visibility = Visibility.Hidden;
            Limpiar();
            BuscarCancelacion();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
                usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);

                if (ValidarDatos())
                {
                    var cancelado = rdCancelarFactura.IsChecked.HasValue && rdCancelarFactura.IsChecked.Value;
                    var loteSacrificioPl = new LoteSacrificioPL();
                    TipoSacrificioEnum tipoSacrificio = (TipoSacrificioEnum)cboTipoSacrificio.SelectedItem;
                    bool ganadoPropio = tipoSacrificio == TipoSacrificioEnum.Propio;
                    loteSacrificioPl.ActualizarLoteSacrificio(new LoteSacrificioInfo
                                                                  {
                                                                      OrdenSacrificioId = ordenSacrificioId,
                                                                      Cliente = skAyudaCliente.Info,
                                                                      OrganizacionId = organizacionId,
                                                                      Observaciones = txtObservaciones.Text.Trim(),
                                                                      UsuarioModificacionId = usuarioId,
                                                                      Cancelacion = cancelado,
                                                                      Fecha =
                                                                          cancelado
                                                                              ? Convert.ToDateTime(txtFechaFactura.Text)
                                                                              : dtpFechaSacrificio.SelectedDate.Value
                                                                  },ganadoPropio);
                    
                    if (rdGenerarFactura.IsChecked != null && (bool) rdGenerarFactura.IsChecked)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.FacturacionSacrificio_MensajeGuardadoCorrectamente,
                                       MessageBoxButton.OK, MessageImage.Correct);
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.FacturacionSacrificio_MensajeCancelacionExitosa,
                                       MessageBoxButton.OK, MessageImage.Correct);
                    }
                    Limpiar();
                    rdGenerarFactura.IsChecked = true;
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
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.FacturacionSacrificio_MensajeErrorAlGuardar,
                                       MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.FacturacionSacrificio_MensajeCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                Limpiar();
                rdGenerarFactura.IsChecked = true;
            }
        }

        private void dtpFechaSacrificio_LostFocus(object sender, RoutedEventArgs e)
        {
            var fechaPl = new FechaPL();
            var fechaServidor = fechaPl.ObtenerFechaActual();
            int diasFacturacion = 1;
            var parametroOrganizacionPL = new ParametroOrganizacionPL();
            ParametroOrganizacionInfo parametroOrganizacion =
                parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                    ParametrosEnum.DIASFACTURACION.ToString());

            if (parametroOrganizacion != null)
            {
                int.TryParse(parametroOrganizacion.Valor, out diasFacturacion);
                if (diasFacturacion == 0)
                {
                    diasFacturacion = 1;
                }
            }

            if (dtpFechaSacrificio.SelectedDate != null)
            {
                DateTime fechaMinima = fechaServidor.FechaActual.AddDays(-diasFacturacion);
                if (dtpFechaSacrificio.SelectedDate.Value.Date < fechaMinima.Date)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format(Properties.Resources.FacturacionSacrificio_MensajeFechaMenorAyer,diasFacturacion),
                        MessageBoxButton.OK, MessageImage.Warning);
                    dtpFechaSacrificio.SelectedDate = DateTime.Now;
                }
                if (dtpFechaSacrificio.SelectedDate.Value.Date > fechaServidor.FechaActual.Date)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.FacturacionSacrificio_MensajeFechaMayorAHoy,
                        MessageBoxButton.OK, MessageImage.Warning);
                    dtpFechaSacrificio.SelectedDate = DateTime.Now;
                }
            }
        }

        #endregion
    }
}
