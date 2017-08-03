using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
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
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para BasculaMateriaPrima.xaml
    /// </summary>
    public partial class BasculaMateriaPrima
    {

        #region Propiedades
        private int organizacionID;
        private int usuarioID;
        private SerialPortManager spManager;
        private bool basculaConectada;
        private bool pesajeTara;
        private PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo;
        private List<ParametrosDetallePedidoInfo> listaMateriaPrima = new List<ParametrosDetallePedidoInfo>();
        //private bool cantidadActualizada;
        private string nombreImpresora;
        private PedidoInfo pedidoSeleccionado;
        private ProductoInfo productoDetalle = new ProductoInfo();
        private ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfoGlobal = new ProgramacionMateriaPrimaInfo();
        private SKAyuda<PedidoInfo> skAyudaFolio;
        private bool segundoPesaje;
        private AlmacenInventarioLoteInfo almacenDelTicket;
        private decimal cantidadEntregadaLote;
        #endregion

        #region Constructor
        public BasculaMateriaPrima()
        {
            InitializeComponent();
            basculaConectada = false;
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento loaded de Forma.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasculaMateriaPrima_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (basculaConectada)
            {
                TxtDisplayPesoOculto.Text = "0.00";
            }
            organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            nombreImpresora = AuxConfiguracion.ObtenerConfiguracion().ImpresoraRecepcionGanado;
            AgregarAyudaFolio();
            InicializarBascula();
            skAyudaFolio.AsignarFoco();
            BtnImpresion.IsEnabled = false;
            BtnGuardar.IsEnabled = false;
            BtnCapturarPesoBruto.IsEnabled = false;
            BtnCapturarPesoTara.IsEnabled = false;
            TxtTicket.IsEnabled = false;
            almacenDelTicket = null;
            cantidadEntregadaLote = 0;
        }

        /// <summary>
        /// Evento click de editar en grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var parametrosDetallePedidoInfo = (ParametrosDetallePedidoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);

                if (parametrosDetallePedidoInfo == null) return;
                var datosProgramacionBasculaMateriaPrima = new DatosProgramacionBasculaMateriaPrima(parametrosDetallePedidoInfo);
                MostrarCentrado(datosProgramacionBasculaMateriaPrima);
                pesajeTara = datosProgramacionBasculaMateriaPrima.BanderaPesaje;
                pesajeMateriaPrimaInfo = datosProgramacionBasculaMateriaPrima.PesajeMateriaPrimaInfoP;
                if (pesajeTara)
                {
                    TxtPesoTara.Text = TxtDisplayPeso.Text;
                    if (!basculaConectada)
                    {
                        TxtPesoTara.IsEnabled = true;
                        TxtPesoTara.Focus();
                        BtnImpresion.IsEnabled = true;
                    }
                    else
                    {
                        TxtPesoTara.Text = TxtDisplayPeso.Text;
                        BtnImpresion.IsEnabled = true;
                    }
                    //Desactivamos el boton editar en el grid principal
                    foreach (var detallePedidoInfo in listaMateriaPrima)
                    {
                        detallePedidoInfo.Editable = false;
                    }
                    GridMateriaPrima.ItemsSource = null;
                    GridMateriaPrima.ItemsSource = listaMateriaPrima;
                    BtnCapturarPesoTara.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento click de boton guardar
        /// </summary>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Validar pesos antes de guardar
                var resultadoValidacion = ValidarPesos();
                if (resultadoValidacion.Resultado)
                {
                    pesajeMateriaPrimaInfo.EstatusID = Estatus.PedidoCompletado.GetHashCode();
                    pesajeMateriaPrimaInfo.UsuarioModificacionID = usuarioID;

                    var pesajeMateriaPrimaPl = new PesajeMateriaPrimaPL();
                    pesajeMateriaPrimaPl.ActualizarPesajePorId(pesajeMateriaPrimaInfo);
                    var guion = Properties.Resources.BasculaDeMateriaPrima_EtiquetaGuion;
                    var punto = Properties.Resources.BasculaDeMateriaPrima_EtiquetaPunto;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      string.Format("{0} {1}{2}{3}{4} {5}",
                                                    Properties.Resources.BasculaDeMateriaPrima_EtiquetaFolio,
                                                    skAyudaFolio.Clave.ToString(CultureInfo.InvariantCulture),
                                                    guion.Trim(),
                                                    pesajeMateriaPrimaInfo.Ticket.ToString(CultureInfo.InvariantCulture),
                                                    punto.Trim(),
                                                    Properties.Resources.CrearContrato_DatosGuardadosExito),
                                      MessageBoxButton.OK, MessageImage.Correct);
                    LimpiarCampos();
                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje) ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
                BtnGuardar.Focus();
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                BtnGuardar.Focus();
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                BtnGuardar.Focus();
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cerrar la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasculaMateriaPrima_OnUnloaded(object sender, RoutedEventArgs e)
        {
            try{
                if (spManager != null)
                {
                    spManager.StopListening();
                    spManager.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento preview text input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoTara_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// Evento preview text input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTicket_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// Evento click de btncancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.BasculaDeMateriaPrima_MensajeBtnCancelar,
               MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                LimpiarCampos();
            }
        }

        /// <summary>
        /// Evento keydown de txtticket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTicket_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter && e.Key != Key.Tab) return;
                if (TxtTicket.Text != string.Empty)
                {
                    //Se obtiene registro de ticket y se llenan los campos
                    decimal cantidadEntregada = 0;
                    var pesajeMateriaPrimaPl = new PesajeMateriaPrimaPL();
                    var pesajeMateriaPrimaInfoP = new PesajeMateriaPrimaInfo()
                    {
                        Ticket = Convert.ToInt32(TxtTicket.Text),
                        PedidoID = pedidoSeleccionado.PedidoID,
                        Activo = EstatusEnum.Activo.ValorBooleanoDesdeEnum()
                    };
                    pesajeMateriaPrimaInfo = pesajeMateriaPrimaPl.ObtenerPorTicketPedido(pesajeMateriaPrimaInfoP);
                    if (pesajeMateriaPrimaInfo != null)
                    {
                        if (pesajeMateriaPrimaInfo.EstatusID == Estatus.PedidoPendiente.GetHashCode())
                        {
                            if (pedidoSeleccionado != null)
                            {
                                listaMateriaPrima.Clear();
                                GridMateriaPrima.Items.Refresh();

                                foreach (var pedidoInfo in pedidoSeleccionado.DetallePedido)
                                {
                                    var materiaPrimaInfo = new ParametrosDetallePedidoInfo()
                                    {
                                        Producto = pedidoInfo.Producto,
                                        PedidoDetalleId = pedidoInfo.PedidoDetalleId,
                                        LoteProceso = pedidoInfo.InventarioLoteDestino,
                                        CantidadSolicitada = pedidoInfo.CantidadSolicitada,
                                    };

                                    if (pedidoInfo.ProgramacionMateriaPrima != null)
                                    {
                                        cantidadEntregada +=
                                            pedidoInfo.ProgramacionMateriaPrima.Sum(
                                                programacionMateriaPrimaInfo =>
                                                    programacionMateriaPrimaInfo.CantidadEntregada);

                                        if (TxtPesoNeto.Text != String.Empty)
                                        {
                                            materiaPrimaInfo.CantidadEntregada = cantidadEntregada +
                                                                                 Convert.ToInt32(TxtPesoNeto.Text);
                                        }
                                        else
                                        {
                                            materiaPrimaInfo.CantidadEntregada = cantidadEntregada;
                                        }
                                        materiaPrimaInfo.CantidadPendiente = materiaPrimaInfo.CantidadSolicitada -
                                                                             materiaPrimaInfo.CantidadEntregada;
                                        materiaPrimaInfo.CantidadProgramada = pedidoInfo.TotalCantidadProgramada;

                                        materiaPrimaInfo.ProgramacionMateriaPrima = pedidoInfo.ProgramacionMateriaPrima;

                                        materiaPrimaInfo.Editable = false;

                                        //Obtiene el pedido detalle para la programacionmateriaprima del pesaje
                                        if (
                                            pedidoInfo.ProgramacionMateriaPrima.Any(
                                                programacionMateriaPrimaInfo =>
                                                    programacionMateriaPrimaInfo.ProgramacionMateriaPrimaId ==
                                                    pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID))
                                        {
                                            productoDetalle = pedidoInfo.Producto;
                                        }

                                        foreach (var item in pedidoInfo.ProgramacionMateriaPrima)
                                        {
                                            if (materiaPrimaInfo.CantidadPendiente > 0 &&
                                                item.ProgramacionMateriaPrimaId ==
                                                pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID)
                                            {
                                                almacenDelTicket = item.InventarioLoteOrigen;
                                                cantidadEntregadaLote += item.CantidadEntregada;
                                                listaMateriaPrima.Add(materiaPrimaInfo);
                                            }
                                        }

                                        cantidadEntregada = 0;
                                    }

                                }
                                GridMateriaPrima.ItemsSource = null;
                                GridMateriaPrima.ItemsSource = listaMateriaPrima;

                                TxtTicket.IsEnabled = false;

                                TxtPesoTara.Text = pesajeMateriaPrimaInfo.PesoTara.ToString(CultureInfo.InvariantCulture);
                                if (!basculaConectada)
                                {
                                    TxtPesoBruto.IsEnabled = true;
                                    GridMateriaPrima.Focusable = false;
                                }
                                else
                                {
                                    TxtPesoBruto.Text = TxtDisplayPeso.Text;
                                }
                                skAyudaFolio.Clave =
                                    pedidoSeleccionado.FolioPedido.ToString(CultureInfo.InvariantCulture);
                            }

                            skAyudaFolio.IsEnabled = false;
                            TxtPiezas.Text = pesajeMateriaPrimaInfo.Piezas.ToString(CultureInfo.InvariantCulture);

                            BtnImpresion.IsEnabled = true;
                            if (pesajeMateriaPrimaInfo.PesoBruto > 0)
                            {
                                BtnGuardar.IsEnabled = true;
                                TxtPesoBruto.Text =
                                    pesajeMateriaPrimaInfo.PesoBruto.ToString(CultureInfo.InvariantCulture);
                                var pesoBruto = TxtPesoBruto.Text.Replace(",", "").Trim();
                                var pesoTara = TxtPesoTara.Text.Replace(",", "").Trim();
                                TxtPesoNeto.Text =
                                    (Convert.ToInt32(pesoBruto) - Convert.ToInt32(pesoTara)).ToString(
                                        CultureInfo.InvariantCulture);
                                TxtPesoBruto.IsEnabled = false;
                            }
                            else
                            {
                                BtnCapturarPesoBruto.IsEnabled = true;
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.BasculaDeMateriaPrima_MensajeTicketCompleto,
                                MessageBoxButton.OK, MessageImage.Warning);
                            TxtTicket.Clear();
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.BasculaDeMateriaPrima_MensajeTicketNoRegistrado,
                            MessageBoxButton.OK, MessageImage.Warning);
                        TxtTicket.Clear();
                    }
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorMostrarTicket, MessageBoxButton.OK, MessageImage.Error);
                TxtTicket.Focus();
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorMostrarTicket, MessageBoxButton.OK, MessageImage.Error);
                TxtTicket.Focus();
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorMostrarTicket, MessageBoxButton.OK, MessageImage.Error);
                TxtTicket.Focus();
            }
        }

        /// <summary>
        /// Al cambiar el peso bruto se obtiene el peso neto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoBruto_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TxtPesoNeto.Value = 0;
        }

        /// <summary>
        /// Evento previewtextinput de txtpesobruto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoBruto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// Evento previewtextinput de txtpiezas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPiezas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// Evento keydown para la forma en general
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasculaMateriaPrima_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Evento keydown de txtpesobruto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoBruto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab) return;
            if (TxtPesoBruto.Value > 0)
            {
                if (TxtPesoBruto.Value > TxtPesoTara.Value)
                {
                    TxtPesoNeto.Value = TxtPesoBruto.Value - TxtPesoTara.Value;
                }
                else
                {
                    e.Handled = true;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBruto,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
        }

        /// <summary>
        /// Evento lostfocus de txtpesobruto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoBruto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtPesoBruto.Value > 0)
            {
                if (TxtPesoBruto.Value > TxtPesoTara.Value)
                {
                    TxtPesoNeto.Value = TxtPesoBruto.Value - TxtPesoTara.Value;
                }
                else
                {
                    e.Handled = true;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBruto,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
        }

        /// <summary>
        /// Permite capturar un dato en txtpesobruto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCapturarPesoBruto_OnClick(object sender, RoutedEventArgs e)
        {
            TxtPesoBruto.Text = TxtDisplayPeso.Text;
            TxtPesoBruto.IsEnabled = true;
            TxtPesoBruto.Focus();
        }

        /// <summary>
        /// Permite capturar un dato en txtpesotara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCapturarPesoTara_OnClick(object sender, RoutedEventArgs e)
        {
            TxtPesoTara.Text = TxtDisplayPeso.Text;
            if (!basculaConectada)
            {
                TxtPesoTara.IsEnabled = true;
                TxtPesoTara.Focus();
            }
        }

        /// <summary>
        /// Imprimir ticket y guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImpresion_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pesajeTara)
                {
                    //Validar pesos antes de guardar
                    var resultadoValidacion = ValidarPesajeTara();
                    if (resultadoValidacion.Resultado)
                    {
                        Guardar();
                        if (TxtPesoTara.Text != String.Empty)
                        {
                            ImprimeTicketPesajeTara();
                        }
                        LimpiarCampos();
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
                else
                {
                    var resultadoValidacion = ValidarSegundoPesaje();
                    if (!resultadoValidacion.Resultado)
                    {
                        var mensaje = String.IsNullOrEmpty(resultadoValidacion.Mensaje)
                            ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco
                            : resultadoValidacion.Mensaje;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            mensaje, MessageBoxButton.OK, MessageImage.Stop);
                        return;
                    }

                    if (string.IsNullOrEmpty(TxtPesoBruto.Text))
                    {
                        if (!basculaConectada)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBrutoBasculaDesconectada,
                                MessageBoxButton.OK, MessageImage.Warning);
                            TxtPesoBruto.Focus();
                            return;
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBrutoBasculaDesconectada,
                                MessageBoxButton.OK, MessageImage.Warning);
                            BtnImpresion.Focus();
                            return;
                        }
                    }

                    if (TxtPesoTara.Value > 0 && TxtPesoBruto.Value == 0)
                    {
                        ImprimeTicketPesajeTara();
                        LimpiarCampos();
                        return;
                    }

                    if (pesajeMateriaPrimaInfo.PesoTara > 0 && pesajeMateriaPrimaInfo.PesoBruto > 0)
                    {
                        ImprimeTicketSegundoPesaje();
                        return;
                    }

                    //Validar pesos antes de guardar
                    resultadoValidacion = ValidarSegundoPesaje();
                    if (resultadoValidacion.Resultado)
                    {
                        Guardar();
                        BtnGuardar.IsEnabled = true;
                        BtnCapturarPesoBruto.IsEnabled = true;
                        if (TxtPesoTara.Text != String.Empty)
                        {
                            ImprimeTicketSegundoPesaje();
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
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                BtnGuardar.Focus();
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                BtnGuardar.Focus();
            }
            catch (Exception exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaDeMateriaPrima_OcurrioErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                BtnGuardar.Focus();
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Valida 
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarPesos()
        {
            var resultado = new ResultadoValidacion();

            if (TxtPesoTara.Text.Trim() == String.Empty)
            {
                if (!basculaConectada)
                {
                    resultado.Mensaje =
                        Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoTaraBasculaDesconectada;
                    TxtPesoTara.Focus();
                }
                else
                {
                    resultado.Mensaje =
                        Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoTaraBasculaConectada;
                    BtnGuardar.Focus();
                }
                resultado.Resultado = false;
                return resultado;
            }
            var pesoTara = TxtPesoTara.Text.Replace(",", "").Trim();
            if (Convert.ToInt32(pesoTara) == 0)
            {
                resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_PesoTaraMayorCero;
                TxtPesoTara.Focus();
                return resultado;
            }

            if (segundoPesaje)
            {
                if (TxtPesoBruto.Text.Trim() == String.Empty)
                {
                    if (!basculaConectada)
                    {
                        resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBrutoBasculaDesconectada;
                        TxtPesoBruto.Focus();
                    }
                    else
                    {
                        resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBrutoBasculaConectada;
                        BtnGuardar.Focus();
                    }
                    resultado.Resultado = false;
                    return resultado;
                }

                var pesoBruto = TxtPesoBruto.Text.Replace(",", "").Trim();
                if (Convert.ToInt32(pesoBruto) < Convert.ToInt32(pesoTara))
                {
                    if (!basculaConectada)
                    {
                        resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBruto;
                        TxtPesoTara.Focus();
                    }
                    else
                    {
                        resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBruto;
                        BtnGuardar.Focus();
                    }
                    resultado.Resultado = false;
                    return resultado;
                }
            }

            resultado.Resultado = true;
            return resultado;
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
        /// Limpiar pantalla
        /// </summary>
        private void LimpiarCampos()
        {
            TxtTicket.Clear();
            TxtPesoBruto.Text = String.Empty;
            TxtPesoNeto.Text = String.Empty;
            TxtPesoTara.Text = String.Empty;
            TxtPiezas.Text = String.Empty;
            listaMateriaPrima.Clear();
            GridMateriaPrima.ItemsSource = new List<ParametrosDetallePedidoInfo>();
            TxtTicket.IsEnabled = false;
            GridMateriaPrima.Focusable = true;
            TxtPesoBruto.IsEnabled = false;
            TxtPesoTara.IsEnabled = false;
            TxtPiezas.IsEnabled = false;
            pesajeTara = false;
            segundoPesaje = false;
            skAyudaFolio.IsEnabled = true;
            skAyudaFolio.LimpiarCampos();
            skAyudaFolio.AsignarFoco();
            BtnGuardar.IsEnabled = false;
            BtnImpresion.IsEnabled = false;
            BtnCapturarPesoBruto.IsEnabled = false;
            BtnCapturarPesoTara.IsEnabled = false;
            almacenDelTicket = null;
        }

        /// <summary>
        /// Evento guardar
        /// </summary>
        private void Guardar()
        {
            try
            {
                var basculaMateriaPrimaPl = new BasculaMateriaPrimaPL();
                //Se guarda registro para peso tara
                if (pesajeTara)
                {
                    var pesoTara = TxtPesoTara.Text.Replace(",", "").Trim();
                    var guion = Properties.Resources.BasculaDeMateriaPrima_EtiquetaGuion;
                    var punto = Properties.Resources.BasculaDeMateriaPrima_EtiquetaPunto;
                    pesajeMateriaPrimaInfo.PesoTara = Convert.ToInt32(pesoTara);
                    pesajeMateriaPrimaInfo.PesoBruto = 0;
                    pesajeMateriaPrimaInfo.Piezas = 0;
                    pesajeMateriaPrimaInfo.EstatusID = Estatus.PedidoPendiente.GetHashCode();
                    pesajeMateriaPrimaInfo.TipoPesajeID = TipoPesajeEnum.Bascula.GetHashCode();
                    pesajeMateriaPrimaInfo.Activo = EstatusEnum.Activo.ValorBooleanoDesdeEnum();
                    pesajeMateriaPrimaInfo.UsuarioCreacionID = usuarioID;
                    pesajeMateriaPrimaInfo.FechaSurtido = DateTime.Today;
                    pesajeMateriaPrimaInfo.FechaRecibe = DateTime.Today;
                    pesajeMateriaPrimaInfo.UsuarioIDSurtido = usuarioID;
                    pesajeMateriaPrimaInfo.UsuarioIDRecibe = usuarioID;
                    pesajeMateriaPrimaInfo = basculaMateriaPrimaPl.GuardarPrimerPesaje(pesajeMateriaPrimaInfo);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format("{0} {1}{2}{3}{4} {5}",
                            Properties.Resources.BasculaDeMateriaPrima_EtiquetaFolio,
                        skAyudaFolio.Clave.ToString(CultureInfo.InvariantCulture),
                        guion.Trim(),
                        pesajeMateriaPrimaInfo.Ticket.ToString(CultureInfo.InvariantCulture),
                        punto.Trim(),
                        Properties.Resources.CrearContrato_DatosGuardadosExito),
                    MessageBoxButton.OK, MessageImage.Correct);
                }
                else
                {
                    //Guardar pesaje
                    var pesoBruto = TxtPesoBruto.Text.Replace(",", "").Trim();
                    var pesoNeto = TxtPesoNeto.Text.Replace(",", "").Trim();
                    var guion = Properties.Resources.BasculaDeMateriaPrima_EtiquetaGuion;
                    var punto = Properties.Resources.BasculaDeMateriaPrima_EtiquetaPunto;
                    pesajeMateriaPrimaInfo.PesoBruto = Convert.ToInt32(pesoBruto);
                    pesajeMateriaPrimaInfo.UsuarioModificacionID = usuarioID;
                    pesajeMateriaPrimaInfo.Ticket = Convert.ToInt32(TxtTicket.Text);
                    pesajeMateriaPrimaInfo.EstatusID = Estatus.PedidoCompletado.GetHashCode();
                    basculaMateriaPrimaPl.GuardarSegundoPesaje(pesajeMateriaPrimaInfo, Convert.ToDecimal(pesoNeto), pedidoSeleccionado);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format("{0} {1}{2}{3}{4} {5}",
                        Properties.Resources.BasculaDeMateriaPrima_EtiquetaFolio,
                        skAyudaFolio.Clave.ToString(CultureInfo.InvariantCulture),
                        guion.Trim(),
                        TxtTicket.Text.ToString(CultureInfo.InvariantCulture),
                        punto.Trim(),
                        Properties.Resources.CrearContrato_DatosGuardadosExito),
                    MessageBoxButton.OK, MessageImage.Correct);
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                pesajeMateriaPrimaInfo.PesoBruto = 0;
                Logger.Error(ex);
                throw;
            }
            catch (ExcepcionGenerica exg)
            {
                pesajeMateriaPrimaInfo.PesoBruto = 0;
                Logger.Error(exg);
                throw;
            }
            catch (Exception exg)
            {
                pesajeMateriaPrimaInfo.PesoBruto = 0;
                Logger.Error(exg);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), exg);
            }
        }

        /// <summary>
        /// Metodo que carga la ayuda
        /// </summary>
        private void AgregarAyudaFolio()
        {
            try
            {
                skAyudaFolio = new SKAyuda<PedidoInfo>(0,
                    false,
                    new PedidoInfo
                    {
                        FolioPedido = 0,
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                        ListaEstatusPedido = new List<EstatusInfo>
                        {
                            new EstatusInfo { EstatusId = (int)Estatus.PedidoProgramado },
                            new EstatusInfo { EstatusId = (int)Estatus.PedidoParcial },
                        },
                        Activo = EstatusEnum.Activo
                    },
                    "FolioPedidoBusquedaPorFolio",
                    "BasculaMateriaPrimaDescripcionOrganizacion",
                    true,
                    150,
                    true)
                {
                    AyudaPL = new PedidosPL(),
                    MensajeClaveInexistente = Properties.Resources.BasculaDeMateriaPrima_AyudaPedidoInvalidado,
                    MensajeBusquedaCerrar = Properties.Resources.BasculaDeMateriaPrima_AyudaPedidoSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.BasculaDeMateriaPrima_AyudaPedidoBusqueda,
                    MensajeAgregar = Properties.Resources.ProgramacionMateriaPrima_Seleccionar,
                    TituloEtiqueta = Properties.Resources.ProgramacionMateriaPrima_lblFolio,
                    TituloPantalla = Properties.Resources.BasculaDeMateriaPrima_AyudaPedidosTitulo,

                };
                skAyudaFolio.ObtenerDatos += ObtenerDatosFolio;
                skAyudaFolio.AsignaTabIndex(0);
                SplAyudaFolio.Children.Clear();
                SplAyudaFolio.Children.Add(skAyudaFolio);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         ex.Message,
                         MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga los datos del resto de la pantalla
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosFolio(string clave)
        {
            try
            {
                if (skAyudaFolio.Info != null)
                {
                    decimal cantidadEntregada = 0;

                    pedidoSeleccionado = skAyudaFolio.Info;

                    if (pedidoSeleccionado != null)
                    {
                        if (pedidoSeleccionado.DetallePedido != null && pedidoSeleccionado.DetallePedido.Count > 0 &&
                            (pedidoSeleccionado.EstatusPedido.EstatusId == (int)Estatus.PedidoParcial 
                             || pedidoSeleccionado.EstatusPedido.EstatusId == (int)Estatus.PedidoProgramado))
                        {
                            TxtTicket.IsEnabled = true;
                            foreach (var pedidoInfo in pedidoSeleccionado.DetallePedido)
                            {
                                var materiaPrimaInfo = new ParametrosDetallePedidoInfo()
                                {
                                    Producto = pedidoInfo.Producto,
                                    PedidoDetalleId = pedidoInfo.PedidoDetalleId,
                                    LoteProceso = pedidoInfo.InventarioLoteDestino,
                                    CantidadSolicitada = pedidoInfo.CantidadSolicitada,
                                };

                                if (pedidoInfo.ProgramacionMateriaPrima != null)
                                {
                                    cantidadEntregada +=
                                        pedidoInfo.ProgramacionMateriaPrima.Sum(
                                            programacionMateriaPrimaInfo =>
                                                programacionMateriaPrimaInfo.CantidadEntregada);

                                    materiaPrimaInfo.CantidadEntregada = cantidadEntregada;
                                    materiaPrimaInfo.CantidadPendiente = materiaPrimaInfo.CantidadSolicitada -
                                                                            materiaPrimaInfo.CantidadEntregada;
                                    materiaPrimaInfo.CantidadProgramada = pedidoInfo.TotalCantidadProgramada;

                                    materiaPrimaInfo.ProgramacionMateriaPrima = pedidoInfo.ProgramacionMateriaPrima;

                                    materiaPrimaInfo.Editable = true;

                                    if (materiaPrimaInfo.CantidadPendiente > 0)
                                    {
                                        listaMateriaPrima.Add(materiaPrimaInfo);
                                    }

                                    cantidadEntregada = 0;
                                }
                            }
                            if (listaMateriaPrima != null && (listaMateriaPrima != null && listaMateriaPrima.Count > 0))
                            {
                                GridMateriaPrima.ItemsSource = listaMateriaPrima;
                                skAyudaFolio.IsEnabled = false;

                                skAyudaFolio.Info = new PedidoInfo
                                {
                                    FolioPedido = 0,
                                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                                    ListaEstatusPedido = new List<EstatusInfo>
                                    {
                                        new EstatusInfo {EstatusId = (int) Estatus.PedidoProgramado},
                                        new EstatusInfo {EstatusId = (int) Estatus.PedidoParcial},
                                    },
                                    Activo = EstatusEnum.Activo
                                };
                            }
                            else
                            {
                                skAyudaFolio.Info = new PedidoInfo
                                {
                                    FolioPedido = 0,
                                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                                    ListaEstatusPedido = new List<EstatusInfo>
                                    {
                                        new EstatusInfo {EstatusId = (int) Estatus.PedidoProgramado},
                                        new EstatusInfo {EstatusId = (int) Estatus.PedidoParcial},
                                    },
                                    Activo = EstatusEnum.Activo
                                };
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BasculaDeMateriaPrima_MensajeAyudaProgramaciones,
                                    MessageBoxButton.OK, MessageImage.Warning);
                                skAyudaFolio.LimpiarCampos();
                            }
                        }
                        else
                        {
                            skAyudaFolio.Info = new PedidoInfo
                            {
                                FolioPedido = 0,
                                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                                ListaEstatusPedido = new List<EstatusInfo>
                                    {
                                        new EstatusInfo {EstatusId = (int) Estatus.PedidoProgramado},
                                        new EstatusInfo {EstatusId = (int) Estatus.PedidoParcial},
                                    },
                                Activo = EstatusEnum.Activo
                            };
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.BasculaDeMateriaPrima_MensajeAyudaDetallePedido,
                            MessageBoxButton.OK, MessageImage.Warning);
                            skAyudaFolio.LimpiarCampos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         ex.Message,
                         MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Imprime el ticket del primer pesaje
        /// </summary>
        private void ImprimeTicketPesajeTara()
        {
            try
            {
                var print = new PrintDocument();
                print.PrintPage += print_PagePrimerPesaje;
                print.PrinterSettings.PrinterName = nombreImpresora;
                print.PrintController = new StandardPrintController();
                print.Print();
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgErrorImprimir, MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        /// <summary>
        /// Imprime ticket primer pesaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void print_PagePrimerPesaje(object sender, PrintPageEventArgs e)
        {
            const int posicionX = 15, posicionXConceptos = 120, tamanoFuenteConceptos = 8;
            string letra = "Lucida Console";
            var organizacion = new OrganizacionPL();
            var organizacionInfo = organizacion.ObtenerPorID(organizacionID);

            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblHora.ToUpper(), new Font(letra, 8), Brushes.Black, 140, 10);
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), new Font(letra, 8), Brushes.Black, 60, 10);
            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblFecha.ToUpper(), new Font(letra, 8), Brushes.Black, posicionX, 10);
            e.Graphics.DrawString(DateTime.Now.ToShortTimeString().ToUpper(), new Font(letra, 8), Brushes.Black, 190, 10);

            // Organizacion
            e.Graphics.DrawString(organizacionInfo.Descripcion.ToUpper(), new Font(letra, 10), Brushes.Black, posicionX, 25);
            //Direccion de la organizacion
            ObtenerDireccionOrganizacion(e, organizacionInfo.Direccion.ToUpper());

            //Ticket
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_Ticket.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 70);
            e.Graphics.DrawString(skAyudaFolio.Clave + "-" + pesajeMateriaPrimaInfo.Ticket.ToString(CultureInfo.InvariantCulture).ToUpper().Trim(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 100, 70);

            //Obtener producto y programacionmateriaprima
            foreach (var pedidoInfo in pedidoSeleccionado.DetallePedido)
            {
                if (pedidoInfo.ProgramacionMateriaPrima == null) continue;
                foreach (
                    var programacionMateriaPrimaInfo in
                        pedidoInfo.ProgramacionMateriaPrima.Where(
                            programacionMateriaPrimaInfo =>
                            programacionMateriaPrimaInfo.ProgramacionMateriaPrimaId ==
                            pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID))
                {
                    productoDetalle = pedidoInfo.Producto;
                    programacionMateriaPrimaInfoGlobal = programacionMateriaPrimaInfo;
                    break;
                }
            }

            //Lote
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_Lote.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 100);
            e.Graphics.DrawString(programacionMateriaPrimaInfoGlobal.InventarioLoteOrigen.Lote.ToString(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 100, 100);

            //Concepto
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketConcepto.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 130);
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketPaseAProceso.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 130);

            ////Producto
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketProducto.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 145);
            e.Graphics.DrawString(productoDetalle.Descripcion.Trim().ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 145);

            //Proveedor
            var proveedorChofer = new ProveedorChoferPL();
            var proveedorChoferInfo = proveedorChofer.ObtenerProveedorChoferPorProveedorChoferId(pesajeMateriaPrimaInfo.ProveedorChoferID);
            var proveedor = new ProveedorPL();
            var proveedorInfo = proveedor.ObtenerPorID(proveedorChoferInfo.Proveedor);
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketProveedor.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 160);
            e.Graphics.DrawString(proveedorInfo.Descripcion.Trim().ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 160);

            //Chofer
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketChofer.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 175);
            e.Graphics.DrawString(proveedorChoferInfo.Chofer.NombreCompleto.ToString(CultureInfo.InvariantCulture).Trim().ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 175);

            //Placas
            var camionPl = new CamionPL();
            var camionInfo = camionPl.ObtenerPorID(pesajeMateriaPrimaInfo.CamionID);
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketPlacas.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 190);
            e.Graphics.DrawString(camionInfo.PlacaCamion.ToString(CultureInfo.InvariantCulture).Trim().ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 190);

            //Peso Tara
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketPesoTara.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 220);
            e.Graphics.DrawString(TxtPesoTara.Text.Trim(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 130, 220);

            //Registro
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Responsable.ToUpper(), new Font(letra, 9), Brushes.Black, posicionX, 260);
            e.Graphics.DrawString(Application.Current.Properties["Nombre"].ToString().ToUpper(), new Font(letra, 9), Brushes.Black, 130, 260);

            //Bascula
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketBascula.ToUpper(), new Font(letra, 9), Brushes.Black, 100, 290);
        }

        /// <summary>
        /// Imprime el ticket del segundo
        /// </summary>
        private void ImprimeTicketSegundoPesaje()
        {
            try
            {
                var print = new PrintDocument();
                print.PrintPage += print_PageSegundoPesaje;
                print.PrinterSettings.PrinterName = nombreImpresora;
                print.PrintController = new StandardPrintController();
                print.Print();
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgErrorImprimir, MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        /// <summary>
        /// Imprime ticket segundo pesaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void print_PageSegundoPesaje(object sender, PrintPageEventArgs e)
        {
            const int posicionX = 15, posicionXConceptos = 120, tamanoFuenteConceptos = 8;
            string letra = "Lucida Console";
            var organizacion = new OrganizacionPL();
            var organizacionInfo = organizacion.ObtenerPorID(organizacionID);

            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblHora.ToUpper(), new Font(letra, 8), Brushes.Black, 140, 10);
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), new Font(letra, 8), Brushes.Black, 60, 10);
            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblFecha.ToUpper(), new Font(letra, 8), Brushes.Black, posicionX, 10);
            e.Graphics.DrawString(DateTime.Now.ToShortTimeString().ToUpper(), new Font(letra, 8), Brushes.Black, 190, 10);

            // Organizacion
            e.Graphics.DrawString(organizacionInfo.Descripcion.ToUpper(), new Font(letra, 10), Brushes.Black, posicionX, 25);
            //Direccion de la organizacion
            ObtenerDireccionOrganizacion(e, organizacionInfo.Direccion.ToUpper());

            //Ticket
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Ticket.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 70);
            e.Graphics.DrawString(pedidoSeleccionado.FolioPedido + "-" + TxtTicket.Text.Trim(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 100, 70);

            //Obtener producto y programacionmateriaprima
            foreach (var pedidoInfo in pedidoSeleccionado.DetallePedido)
            {
                if (pedidoInfo.ProgramacionMateriaPrima == null) continue;
                foreach (
                    var programacionMateriaPrimaInfo in
                        pedidoInfo.ProgramacionMateriaPrima.Where(
                            programacionMateriaPrimaInfo =>
                            programacionMateriaPrimaInfo.ProgramacionMateriaPrimaId ==
                            pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID))
                {
                    productoDetalle = pedidoInfo.Producto;
                    programacionMateriaPrimaInfoGlobal = programacionMateriaPrimaInfo;
                    break;
                }
            }

            //Lote
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_Lote.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 100);
            e.Graphics.DrawString(programacionMateriaPrimaInfoGlobal.InventarioLoteOrigen.Lote.ToString(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 100, 100);

            //Concepto
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketConcepto.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 130);
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketPaseAProceso.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 130);

            ////Producto
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketProducto.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 145);
            e.Graphics.DrawString(productoDetalle.Descripcion.Trim().ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 145);

            //Proveedor
            var proveedorChofer = new ProveedorChoferPL();
            var proveedorChoferInfo = proveedorChofer.ObtenerProveedorChoferPorProveedorChoferId(pesajeMateriaPrimaInfo.ProveedorChoferID);
            var proveedor = new ProveedorPL();
            var proveedorInfo = proveedor.ObtenerPorID(proveedorChoferInfo.Proveedor);
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketProveedor.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 160);
            e.Graphics.DrawString(proveedorInfo.Descripcion.Trim().ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 160);

            //Chofer
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketChofer.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 175);
            e.Graphics.DrawString(proveedorChoferInfo.Chofer.NombreCompleto.ToString(CultureInfo.InvariantCulture).Trim().ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 175);

            //Placas
            var camionPl = new CamionPL();
            var camionInfo = camionPl.ObtenerPorID(pesajeMateriaPrimaInfo.CamionID);
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketPlacas.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 190);
            e.Graphics.DrawString(camionInfo.PlacaCamion.ToString(CultureInfo.InvariantCulture).Trim().ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 190);
            
            //PesoBruto
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketPesoBruto.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 220);
            e.Graphics.DrawString(TxtPesoBruto.Text.Trim().PadLeft(15, ' '), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 140, 220);
            //Peso Tara
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketPesoTara.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 235);
            e.Graphics.DrawString(TxtPesoTara.Text.Trim().PadLeft(15, ' '), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 140, 235);
            //Peso Neto
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketPesoNeto.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 250);
            e.Graphics.DrawString(TxtPesoNeto.Text.Trim().PadLeft(15, ' '), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 140, 250);
            //Piezas
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketPiezas.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 280);
            e.Graphics.DrawString(TxtPiezas.Text.Trim().PadLeft(15, ' '), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 140, 280);

            //Registro
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Responsable.ToUpper(), new Font(letra, 9), Brushes.Black, posicionX, 310);
            e.Graphics.DrawString(Application.Current.Properties["Nombre"].ToString().ToUpper(), new Font(letra, 9), Brushes.Black, 130, 310);

            //Bascula
            e.Graphics.DrawString(Properties.Resources.BasculaDeMateriaPrima_TicketBascula.ToUpper(), new Font(letra, 9), Brushes.Black, 100, 340);
        }

        /// <summary>
        /// Obtiene la direccion de la organizacion
        /// </summary>
        /// <param name="e"></param>
        /// <param name="direccion"></param>
        private void ObtenerDireccionOrganizacion(PrintPageEventArgs e, string direccion)
        {
            string letra = "Lucida Console";
            const int limiteRenglon = 120;
            if (direccion.Length > limiteRenglon)
            {
                e.Graphics.DrawString(direccion.Substring(0, limiteRenglon), new Font(letra, 10), Brushes.Black, 15, 40);
                e.Graphics.DrawString(direccion.Substring(limiteRenglon, direccion.Length - limiteRenglon), new Font(letra, 10), Brushes.Black, 15, 48);
            }
            else
            {
                e.Graphics.DrawString(direccion, new Font(letra, 10), Brushes.Black, 15, 40);
            }
        }

        /// <summary>
        /// Valida el segundo pesaje
        /// </summary>
        /// <returns></returns>
        public ResultadoValidacion ValidarSegundoPesaje()
        {
            var resultado = new ResultadoValidacion();

            if (TxtPesoTara.Value == 0)
            {
                resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_PesoTaraMayorCero;
                TxtPesoTara.Focus();
                return resultado;
            }

            if (string.IsNullOrEmpty(TxtPesoBruto.Text))
            {
                if (!basculaConectada)
                {
                    resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBrutoBasculaDesconectada;
                    TxtPesoBruto.Focus();
                }
                else
                {
                    resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBrutoBasculaConectada;
                }
                resultado.Resultado = false;
                return resultado;
            }

            if (TxtPesoBruto.Value <= TxtPesoTara.Value)
            {
                if (!basculaConectada)
                {
                    resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBruto;
                    TxtPesoBruto.Focus();
                }
                else
                {
                    resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoBruto;
                    BtnGuardar.Focus();
                }
                resultado.Resultado = false;
                return resultado;
            }

            var pedidoPL = new PedidosPL();
            List<PedidoPendienteLoteModel> listaPedidosPendientes =
                pedidoPL.ObtenerPedidosEntregadosPorLote(almacenDelTicket.AlmacenInventarioLoteId);

            int cantidadEntregada = 0;

            if(listaPedidosPendientes != null)
            {
                listaPedidosPendientes.ForEach(pedido =>
                    {
                        if (pedido.CantidadEntregada > 0)
                        {
                            cantidadEntregada = cantidadEntregada + pedido.CantidadEntregada;
                        }
                    });
            }

            if (TxtPesoNeto.Value > (almacenDelTicket.Cantidad - cantidadEntregada))
            {
                resultado.Mensaje = string.Format(Properties.Resources.BasculaDeMateriaPrima_LoteSinExistenciaPesada,(almacenDelTicket.Cantidad - cantidadEntregada));
                if (!basculaConectada)
                {
                    TxtPesoBruto.Focus();
                }
                else
                {
                    BtnGuardar.Focus();
                }
                resultado.Resultado = false;
                return resultado;
            }

            resultado.Resultado = true;
            
            return resultado;
        }

        /// <summary>
        /// Valida el pesaje tara
        /// </summary>
        /// <returns></returns>
        public ResultadoValidacion ValidarPesajeTara()
        {
            var resultado = new ResultadoValidacion();

            if (string.IsNullOrEmpty(TxtPesoTara.Text))
            {
                if (!basculaConectada)
                {
                    resultado.Mensaje =
                        Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoTaraBasculaDesconectada;
                    TxtPesoTara.Focus();
                }
                else
                {
                    resultado.Mensaje =
                        Properties.Resources.BasculaDeMateriaPrima_MensajeValidacionPesoTaraBasculaConectada;
                    BtnGuardar.Focus();
                }
                resultado.Resultado = false;
                return resultado;
            }
            var pesoTara = TxtPesoTara.Text.Replace(",", "").Trim();
            if (Convert.ToInt32(pesoTara) == 0)
            {
                resultado.Mensaje = Properties.Resources.BasculaDeMateriaPrima_PesoTaraMayorCero;
                TxtPesoTara.Focus();
                return resultado;
            }

            resultado.Resultado = true;
            return resultado;
        }
        #endregion

        #region Bascula
        /// <summary>
        /// Método que inicializa los datos de la bascula
        /// </summary>
        private void InicializarBascula()
        {
            try
            {
                spManager = new SerialPortManager();
                spManager.NewSerialDataRecieved += (spManager_NewSerialDataRecieved);

                if (spManager != null)
                {

                    spManager.StartListening(AuxConfiguracion.ObtenerConfiguracion().PuertoBascula,
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().BasculaBaudrate),
                        ObtenerParidad(AuxConfiguracion.ObtenerConfiguracion().BasculaParidad),
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().BasculaDataBits),
                        ObtenerStopBits(AuxConfiguracion.ObtenerConfiguracion().BasculaBitStop));

                    basculaConectada = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaMateriaPrima_MsgErrorBascula,
                    MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        /// <summary>
        /// Cambia la variable string en una entidad Parity
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private Parity ObtenerParidad(string parametro)
        {
            Parity paridad;

            switch (parametro)
            {
                case "Even":
                    paridad = Parity.Even;
                    break;
                case "Mark":
                    paridad = Parity.Mark;
                    break;
                case "None":
                    paridad = Parity.None;
                    break;
                case "Odd":
                    paridad = Parity.Odd;
                    break;
                case "Space":
                    paridad = Parity.Space;
                    break;
                default:
                    paridad = Parity.None;
                    break;
            }
            return paridad;
        }

        /// <summary>
        /// Cambia la variable string en una entidad StopBit
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private StopBits ObtenerStopBits(string parametro)
        {
            StopBits stopBit;

            switch (parametro)
            {
                case "None":
                    stopBit = StopBits.None;
                    break;
                case "One":
                    stopBit = StopBits.One;
                    break;
                case "OnePointFive":
                    stopBit = StopBits.OnePointFive;
                    break;
                case "Two":
                    stopBit = StopBits.Two;
                    break;
                default:
                    stopBit = StopBits.One;
                    break;
            }
            return stopBit;
        }
        /// <summary>
        /// Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            string strEnd = "", peso = "";
            double val;
            try
            {
                strEnd = spManager.ObtenerLetura(e.Data);
                if (strEnd.Trim() != "")
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    // damos formato al valor peso para presentarlo
                    peso = String.Format(CultureInfo.CurrentCulture, "{0:0}", val).Replace(",", ".");

                    //Aquie es para que se este reflejando la bascula en el display
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        TxtDisplayPeso.Text = peso;
                    }), null);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion
    }
}
