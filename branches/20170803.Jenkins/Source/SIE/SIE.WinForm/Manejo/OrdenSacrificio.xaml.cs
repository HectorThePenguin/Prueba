using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para OrdenSacrificio.xaml
    /// </summary>
    public partial class OrdenSacrificio
    {
        #region Variables
        private int organizacionId;
        private int usuario;
        private IList<OrdenSacrificioDetalleInfo> listaSacrificios;
        private IList<OrdenSacrificioDetalleInfo> listaSacrificiosBorrados;
        private IList<OrdenSacrificioDetalleInfo> listaSacrificiosCorraleta;
        private CorralInfo corral;
        private CorralInfo corraleta;
        private LoteInfo lote;
        private IList<LoteInfo> lotesCorraleta;
        private ProveedorInfo proveedor;
        private List<TurnoEnum> ListaTurnos { get; set; }
        private OrdenSacrificioDetalleInfo ordenActualizada;
        private bool nuevo;
        private int indiceActualizado;
        private bool banderaFocus;
        private int diasZigma;
        private int diferenciasZigma;
        private OrdenSacrificioInfo ordenActual;
        private string textoAnterior;
        private bool ctrlPegar;
        private bool banderaCambioTurno;
        private bool banderaAplicaMarel;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrdenSacrificio()
        {
            try
            {
                listaSacrificios = new List<OrdenSacrificioDetalleInfo>();
                listaSacrificiosBorrados = new List<OrdenSacrificioDetalleInfo>();
                listaSacrificiosCorraleta = new List<OrdenSacrificioDetalleInfo>();
                banderaFocus = true;
                banderaCambioTurno = true;
                organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
                usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                InitializeComponent();
                LimpiarControles();
                ListaTurnos = Enum.GetValues(typeof(TurnoEnum)).Cast<TurnoEnum>().ToList();
                LeerConfiguracion();
                ConsultarCorraletaSacrificio();
                if(AplicaEnvioMarel())
                {
                    txtCabezasSacrificar.IsReadOnly = true;
                    banderaAplicaMarel = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.OrdenSacrificio_ErrorConfiguracion, MessageBoxButton.OK, MessageImage.Error);
                InavilitarControles();
            }

        }
        #endregion

        #region Eventos

        /// <summary>
        /// Evento de carga de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var fechaActual = DateTime.Now.AddDays(1);
            dtpFechaOrden.SelectedDate = fechaActual;

            txtCorral.Focus();
        }

        /// <summary>
        /// Clic del boton limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarControles();
        }

        /// <summary>
        /// Click del boton imprimir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listaSacrificios.Count > 0)
                {
                    var ordenSacrificioPl = new OrdenSacrificioPL();
                    ordenSacrificioPl.ImprimirOrdenSacrificio(ObtenerEtiquetasImpresion(), listaSacrificios);
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.OrdenSacrificio_ErrorImprimir, MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Se obtiene las etiquetas para la impresion del reporte
        /// </summary>
        /// <returns></returns>
        private ImpresionOrdenSacrificioInfo ObtenerEtiquetasImpresion()
        {
            var etiquetasImpresion = new ImpresionOrdenSacrificioInfo();
            try
            {
                etiquetasImpresion.EtiquetaFecha = Properties.Resources.OrdenSacrificio_ReporteFecha;
                etiquetasImpresion.EtiquetaCabezaEmpresa = Properties.Resources.OrdenSacrificio_ReporteCabezaEmpresa;
                etiquetasImpresion.EtiquetaTitulo = Properties.Resources.OrdenSacrificio_ReporteTituloReporte;
                etiquetasImpresion.EtiquetaFolioIso = Properties.Resources.OrdenSacrificio_ReporteSerieIso;
                etiquetasImpresion.EtiquetaPlanta = Properties.Resources.OrdenSacrificio_ReportePlanta;
                etiquetasImpresion.EtiquetaNoSalida = Properties.Resources.OrdenSacrificio_ReporteNoSalida;
                etiquetasImpresion.EtiquetaCorraletas = Properties.Resources.OrdenSacrificio_ReporteCorraletas;
                etiquetasImpresion.EtiquetaCorral = Properties.Resources.OrdenSacrificio_ReporteCorral;
                etiquetasImpresion.EtiquetaLote = Properties.Resources.OrdenSacrificio_ReporteLote;
                etiquetasImpresion.EtiquetaCabezas = Properties.Resources.OrdenSacrificio_ReporteCabezas;
                etiquetasImpresion.EtiquetaDiasEngorda = Properties.Resources.OrdenSacrificio_ReporteDiasEngorda;
                etiquetasImpresion.EtiquetaTipo = Properties.Resources.OrdenSacrificio_ReporteTipo;
                etiquetasImpresion.EtiquetaProveedor = Properties.Resources.OrdenSacrificio_ReporteProveedor;
                etiquetasImpresion.EtiquetaEstatus = Properties.Resources.OrdenSacrificio_ReporteStatus;
                etiquetasImpresion.EtiquetaDiasRetiro = Properties.Resources.OrdenSacrificio_ReporteDiasRetiro;
                etiquetasImpresion.Etiquetaczas = Properties.Resources.OrdenSacrificio_Reporteczas;
                etiquetasImpresion.EtiquetaTotal = Properties.Resources.OrdenSacrificio_ReporteTotal;
                etiquetasImpresion.EtiquetaObservaciones = Properties.Resources.OrdenSacrificio_ReporteObservaciones;
                etiquetasImpresion.EtiquetaFechaInicio = Properties.Resources.OrdenSacrificio_ReporteFechaInicio;
                etiquetasImpresion.EtiquetaFechaTerminacion = Properties.Resources.OrdenSacrificio_ReporteFechaTermino;
                etiquetasImpresion.Observaciones = txtObservacion.Text;
                if (dtpFechaOrden.SelectedDate != null)
                {
                    etiquetasImpresion.FechaReporte = (DateTime)dtpFechaOrden.SelectedDate;
                }

            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }


            return etiquetasImpresion;

        }

        /// <summary>
        /// Click del boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (listaSacrificios.Count == 0 && listaSacrificiosBorrados.Count > 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.OrdenSacrificio_GuardadoCorrecto,
                        MessageBoxButton.OK,
                        MessageImage.Correct);
            }
            else
            {
                if (listaSacrificios.Count > 0)
                {
                    if (Guardar())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.OrdenSacrificio_GuardadoCorrecto,
                            MessageBoxButton.OK,
                            MessageImage.Correct);
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.OrdenSacrificio_GuardadoIncorrecto,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.OrdenSacrificio_DatosEnBlanco,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
            }
        }

        /// <summary>
        /// Click del boton agregar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            var ordenSacrificio = new OrdenSacrificioDetalleInfo();

            var resultadoValidacion = ValidarDatosEnBlanco();
            var ordenSacrificioPl = new OrdenSacrificioPL();
            if (resultadoValidacion.Resultado)
            {

                if (listaSacrificiosCorraleta.Count > 0)
                {
                    if (!ExistenDetalles())
                    {
                        foreach (var detalleTmp in listaSacrificiosCorraleta)
                        {
                            detalleTmp.Corraleta = corraleta;
                            detalleTmp.DiasEngordaGrano = ordenSacrificioPl.ObtnerDiasEngorda70(detalleTmp.Lote);
                            detalleTmp.CorraletaCodigo = txtCorraletas.Text;
                            listaSacrificios.Add(detalleTmp);

                        }
                        gridSacrificios.ItemsSource = null;
                        gridSacrificios.ItemsSource = listaSacrificios;
                        MostrarTotales();
                        LimpiarControles();
                        listaSacrificiosCorraleta.Clear();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.OrdenSacrificio_SeEncuentraEnorden,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                    }

                }
                else
                {
                    bool corralFuerzaMayor = ValidarCorralesFuerzaMayorOIntensivo();
                    if (corralFuerzaMayor || int.Parse(txtDiasRetiro.Text) >= diferenciasZigma)
                    {

                        if (nuevo)
                        {
                            if (ValidarCabezas())
                            {
                                if (ValidarCorrarRepetido())
                                {
                                    var lotePL = new LotePL();
                                    TipoGanadoInfo tipoGanado = lotePL.ObtenerTipoGanadoCorral(lote);

                                    if (lote.TipoCorralID == TipoCorral.Intensivo.GetHashCode())
                                    {
                                        tipoGanado = ObtenerTipoGanadoIntensivo(lote);

                                        if (tipoGanado == null)
                                        {
                                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.OrdenSacrificio_IntensivoSinInformacion,
                                                MessageBoxButton.OK,
                                                MessageImage.Stop);
                                            return;
                                        }
                                    }

                                    ordenSacrificio.OrdenSacrificioDetalleID = 0;
                                    ordenSacrificio.Cabezas = int.Parse(txtCabezas.Text);
                                    ordenSacrificio.CabezasASacrificar = int.Parse(txtCabezasSacrificar.Text);
                                    ordenSacrificio.Corral = corral;
                                    ordenSacrificio.Corraleta = corraleta;
                                    ordenSacrificio.CorraletaCodigo = txtCorraletas.Text;
                                    ordenSacrificio.DiasRetiro = int.Parse(txtDiasRetiro.Text);
                                    ordenSacrificio.Lote = lote;
                                    ordenSacrificio.Proveedor = proveedor;
                                    ordenSacrificio.Clasificacion = txtTipoGanado.Text;
                                    ordenSacrificio.TipoGanadoID = tipoGanado.TipoGanadoID;
                                    ordenSacrificio.Turno = TurnoEnum.Matutino;
                                    ordenSacrificio.Turnos = ListaTurnos;
                                    ordenSacrificio.Activo = true;
                                    ordenSacrificio.DiasEngordaGrano =
                                    ordenSacrificioPl.ObtnerDiasEngorda70(ordenSacrificio.Lote);

                                    listaSacrificios.Add(ordenSacrificio);
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                       Properties.Resources.OrdenSacrificio_CorralExiste,
                                       MessageBoxButton.OK,
                                       MessageImage.Stop);
                                    e.Handled = true;
                                    return;
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.OrdenSacrificio_CabezasInsuficientes,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                e.Handled = true;
                                txtCabezasSacrificar.Focus();
                                return;

                            }
                        }
                        else
                        {
                            var lotePL = new LotePL();
                            TipoGanadoInfo tipoGanado = lotePL.ObtenerTipoGanadoCorral(lote);
                            if (lote.TipoCorralID == TipoCorral.Intensivo.GetHashCode())
                            {
                                tipoGanado = ObtenerTipoGanadoIntensivo(lote);

                                if (tipoGanado == null)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.OrdenSacrificio_IntensivoSinInformacion,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                    return;
                                }
                            }
                            ordenActualizada.Cabezas = int.Parse(txtCabezas.Text);
                            ordenActualizada.CabezasASacrificar = int.Parse(txtCabezasSacrificar.Text);
                            ordenActualizada.Corral = corral;
                            ordenActualizada.CorraletaCodigo = txtCorraletas.Text;
                            ordenActualizada.Corraleta = corraleta;
                            ordenActualizada.DiasRetiro = int.Parse(txtDiasRetiro.Text);
                            ordenActualizada.Lote = lote;
                            ordenActualizada.Proveedor = proveedor;
                            ordenActualizada.Clasificacion = txtTipoGanado.Text;
                            ordenActualizada.TipoGanadoID = tipoGanado.TipoGanadoID;
                            ordenActualizada.Activo = true;
                            //ordenActualizada.DiasEngordaGrano = ordenSacrificioPl.ObtnerDiasEngorda70(ordenActualizada.Lote);

                            listaSacrificios[indiceActualizado] = ordenActualizada;
                            btnAgregar.Content = Properties.Resources.OrdenSacrificio_btnAgregar;
                        }
                        gridSacrificios.ItemsSource = null;
                        gridSacrificios.ItemsSource = listaSacrificios;
                        MostrarTotales();
                        LimpiarControles();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.OrdenSacrificio_DiasRetiroNoValido,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                    }

                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    resultadoValidacion.Mensaje,
                    MessageBoxButton.OK,
                    MessageImage.Stop);

                e.Handled = true;

            }

        }

        private bool ValidarCorralesFuerzaMayorOIntensivo()
        {
            bool corralFuerzaMayor = false;

            if (corral != null && corral.CorralID > 0)
            {
                if (corral.TipoCorral.TipoCorralID == TipoCorral.Intensivo.GetHashCode())
                {
                    return true;
                }
            }

            var parametrosPl = new ConfiguracionParametrosPL();
            string corralSeleccionado = txtCorral.Text;
            var parametroSolicitado = new ConfiguracionParametrosInfo
            {
                TipoParametro = (int)TiposParametrosEnum.CorraletaDisponibleSacrificio,
                OrganizacionID = organizacionId
            };
            IList<ConfiguracionParametrosInfo> parametros = parametrosPl.ObtenerPorOrganizacionTipoParametro(parametroSolicitado);

            if (parametros == null || !parametros.Any())
            {
                return false;
            }
            var corralesConfigurados = parametros.Select(pa => pa.Valor);

            foreach (var corralConfigurado in corralesConfigurados)
            {
                var corralesSplit = corralConfigurado.Split('|');
                if (!corralesSplit.Any())
                {
                    continue;
                }
                string corralExiste = corralesSplit.FirstOrDefault(co => co.ToUpper().Equals(corralSeleccionado.ToUpper(), StringComparison.InvariantCultureIgnoreCase));
                if (!string.IsNullOrWhiteSpace(corralExiste))
                {
                    corralFuerzaMayor = true;
                    break;
                }
            }
            return corralFuerzaMayor;
        }

        /// <summary>
        /// Valida si existe detalles
        /// </summary>
        /// <returns></returns>
        private bool ExistenDetalles()
        {
            var resultado = false;
            foreach (var detalleTmp in listaSacrificiosCorraleta)
            {

                foreach (var detalleOrden in listaSacrificios)
                {
                    if (detalleTmp.Lote.LoteID == detalleOrden.Lote.LoteID &&
                        detalleTmp.CabezasASacrificar == detalleOrden.CabezasASacrificar)
                    {
                        resultado = true;
                    }
                }


            }

            return resultado;
        }

        /// <summary>
        /// Key down del campo corral
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!string.IsNullOrEmpty(txtCorral.Text))
                {

                    banderaFocus = false;
                    var resultado = ValidarCorral();

                    if (!resultado.Resultado)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            resultado.Mensaje,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        e.Handled = true;
                        txtCorral.Focus();
                        txtCorral.Clear();
                        banderaFocus = true;
                    }
                }
                else
                {
                    e.Handled = true;
                }                
            }
        }

        /// <summary>
        /// Perdida de foco del campo corral
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorral_LostFocus(object sender, RoutedEventArgs e)
        {
            if (banderaFocus)
            {
                if (!string.IsNullOrEmpty(txtCorral.Text))
                {
                    var resultado = ValidarCorral();

                    if (!resultado.Resultado)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            resultado.Mensaje,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        e.Handled = true;
                        txtCorral.Focus();
                        txtCorral.Clear();
                        LimpiarControles();
                    }
                }
            }
            else
            {
                banderaFocus = true;
            }

        }

        /// <summary>
        /// Perdida de foco del campo cabezas a sacrificar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCabezasSacrificar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (banderaFocus)
            {
                if (!string.IsNullOrEmpty(txtCabezasSacrificar.Text))
                {
                    var resultado = ValidarCabezas();

                    if (!resultado)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.OrdenSacrificio_CabezasInsuficientes,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        e.Handled = true;
                        txtCabezasSacrificar.Clear();

                    }
                    try
                    {
                        var cabezas = int.Parse(txtCabezasSacrificar.Text);
                        if (cabezas <= 0)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.OrdenSacrificio_CabezasMayorACero,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            e.Handled = true;
                            txtCabezasSacrificar.Clear();
                        }
                    }
                    catch (Exception)
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                banderaFocus = true;
            }
        }

        /// <summary>
        /// Key down del campo corraletas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorraletas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                banderaFocus = false;
            }
        }

        /// <summary>
        /// Preview del campo cabezas por sacrificar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCabezasSacrificar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        /// <summary>
        /// Click del boton subir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubir_Click(object sender, RoutedEventArgs e)
        {
            if (gridSacrificios.SelectedIndex > -1)
            {
                banderaCambioTurno = false;
                if (gridSacrificios.SelectedIndex > 0)
                {
                    var indice = gridSacrificios.SelectedIndex;
                    var ordenAnterior = listaSacrificios[indice - 1];
                    var ordenTemporal = (OrdenSacrificioDetalleInfo)gridSacrificios.SelectedItem;

                    listaSacrificios.RemoveAt(indice);
                    listaSacrificios.RemoveAt(indice - 1);
                    listaSacrificios.Insert(indice - 1, ordenTemporal);
                    listaSacrificios.Insert(indice, ordenAnterior);

                    gridSacrificios.ItemsSource = null;
                    gridSacrificios.ItemsSource = listaSacrificios;
                    gridSacrificios.SelectedIndex = indice - 1;

                }

            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.OrdenSacrificio_Seleccione,
                    MessageBoxButton.OK,
                    MessageImage.Stop);
            }
        }

        /// <summary>
        /// Click del boton bajar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBajar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridSacrificios.SelectedIndex > -1)
                {
                    banderaCambioTurno = false;
                    if (gridSacrificios.SelectedIndex < gridSacrificios.Items.Count - 1)
                    {
                        var indice = gridSacrificios.SelectedIndex;
                        var ordenSuperior = listaSacrificios[indice + 1];
                        var ordenTemporal = (OrdenSacrificioDetalleInfo)gridSacrificios.SelectedItem;

                        listaSacrificios.RemoveAt(indice + 1);
                        listaSacrificios.RemoveAt(indice);
                        listaSacrificios.Insert(indice, ordenSuperior);
                        listaSacrificios.Insert(indice + 1, ordenTemporal);

                        gridSacrificios.ItemsSource = null;
                        gridSacrificios.ItemsSource = listaSacrificios;
                        gridSacrificios.SelectedIndex = indice + 1;
                    }

                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.OrdenSacrificio_Seleccione,
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
        /// Click del boton editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridSacrificios.SelectedIndex > -1)
                {
                    if (btnAgregar.IsEnabled)
                    {
                        nuevo = false;
                        btnAgregar.Content = Properties.Resources.OrdenSacrificio_btnActualizar;
                        ordenActualizada = (OrdenSacrificioDetalleInfo)gridSacrificios.SelectedItem;
                        indiceActualizado = gridSacrificios.SelectedIndex;
                        corraleta = ordenActualizada.Corraleta;
                        MostrarOrden(ordenActualizada);
                        corral = ordenActualizada.Corral;
                        lote = ordenActualizada.Lote;
                        proveedor = ordenActualizada.Proveedor;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Key down para el campo cabezas a sacrificar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCabezasSacrificar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (string.IsNullOrEmpty(txtCabezasSacrificar.Text))
                {
                    e.Handled = true;
                }
                else
                {

                    try
                    {
                        banderaFocus = false;
                        if (int.Parse(txtCabezasSacrificar.Text) > 0)
                        {

                            if (!ValidarCabezas())
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.OrdenSacrificio_CabezasInsuficientes,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                txtCabezasSacrificar.Clear();
                                e.Handled = true;
                                banderaFocus = true;
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.OrdenSacrificio_CabezasMayorACero,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            txtCabezasSacrificar.Clear();
                            e.Handled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }

                }


            }
        }

        /// <summary>
        /// Click del boton eliminar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridSacrificios.SelectedIndex > -1)
                {
                    if (btnAgregar.IsEnabled)
                    {
                        var ordenEliminado = (OrdenSacrificioDetalleInfo)gridSacrificios.SelectedItem;
                        var indiceEliminado = gridSacrificios.SelectedIndex;
                        var lotePl = new LotePL();
                        var loteTmp = lotePl.ObtenerPorCorralCerrado(organizacionId, ordenEliminado.Corral.CorralID);

                        bool eleminar;
                        if (loteTmp == null)
                        {
                            eleminar = true;
                        }
                        else
                        {
                            eleminar = loteTmp.LoteID == ordenEliminado.Lote.LoteID;
                        }

                        if (eleminar)
                        {

                            if (SkMessageBox.Show(
                                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.OrdenSacrificio_PreguntaBorrar,
                                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                            {

                                listaSacrificiosBorrados.Add(ordenEliminado);
                                listaSacrificios.RemoveAt(indiceEliminado);
                                gridSacrificios.ItemsSource = null;
                                gridSacrificios.ItemsSource = listaSacrificios;
                                MostrarTotales();
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.OrdenSacrificio_ErrorBoorrado,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
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
        /// Preview text del campo corral
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorral_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        /// <summary>
        /// Preview text del campo corraleta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorraletas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        /// <summary>
        /// Evento TextChanged en el campo corraletas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorraletas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtCorraletas.Text))
            {
                ctrlPegar = false;
                txtCorraletas.Text = txtCorraletas.Text.Replace(" ", "");
            }
            else
            {
                if (ctrlPegar)
                {
                    txtCorraletas.Text = textoAnterior;
                    ctrlPegar = false;
                }
            }
        }
        /// <summary>
        /// Evento PreviewKeyDown del campo corraletas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorraletas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                ctrlPegar = true;
                textoAnterior = txtCorraletas.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                ctrlPegar = true;
                textoAnterior = txtCorraletas.Text;
            }
        }
        /// <summary>
        /// Evento TextChanged del campo cabezas a sacrificar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCabezasSacrificar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeros(txtCabezasSacrificar.Text))
            {
                ctrlPegar = false;
                txtCabezasSacrificar.Text = txtCabezasSacrificar.Text.Replace(" ", "");
            }
            else
            {
                if (ctrlPegar)
                {
                    txtCabezasSacrificar.Text = textoAnterior;
                    ctrlPegar = false;
                }
            }
        }
        /// <summary>
        /// Evento PreviewKeyDown del campo cabezas a sacrificar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCabezasSacrificar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                ctrlPegar = true;
                textoAnterior = txtCabezasSacrificar.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                ctrlPegar = true;
                textoAnterior = txtCabezasSacrificar.Text;
            }
        }
        /// <summary>
        /// Evento TextChanged para el campo corral
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorral_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtCorral.Text))
            {
                ctrlPegar = false;
                txtCorral.Text = txtCorral.Text.Replace(" ", "");
            }
            else
            {
                if (!ctrlPegar) return;
                txtCorral.Text = textoAnterior;
                ctrlPegar = false;
            }
        }
        /// <summary>
        /// Evento PreviewKeyDown del campo corral
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorral_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                ctrlPegar = true;
                textoAnterior = txtCorral.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                ctrlPegar = true;
                textoAnterior = txtCorral.Text;
            }
        }

        /// <summary>
        /// Evento OnSelectionChanged del compo de turnos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTurnos_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var indiceSeleccionado = gridSacrificios.SelectedIndex;
            if (indiceSeleccionado >= 0 && banderaCambioTurno)
            {
                ((OrdenSacrificioDetalleInfo)gridSacrificios.SelectedItem).Turno = (TurnoEnum)e.AddedItems[0];
            }
        }
        /// <summary>
        /// Evento OnMouseDown de combo de turnos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTurnos_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            banderaCambioTurno = true;
        }
        /// <summary>
        /// Evento LoadingRow del grid de detalle de la orden de sacrificio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridSacrificios_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var item = e.Row;
            if (item != null)
            {
                var ordenDetalle = (OrdenSacrificioDetalleInfo)item.Item;
                if (ordenDetalle.DiasRetiro > diferenciasZigma)
                {
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                }


            }
        }
        /// <summary>
        /// Evento OnSelectedDateChangen de la fecha de la orden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaOrden_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txtObservacion.Clear();
            ValidarActivacionControles(ValidarFechas());
            LimpiarControles();
            listaSacrificios.Clear();
            listaSacrificiosBorrados.Clear();
            listaSacrificiosCorraleta.Clear();
            CargarOrden();

        }


        #endregion

        #region Metodos
        /// <summary>
        /// Activa o desactiva los controles de edicion de la orden de sacrificio
        /// </summary>
        /// <param name="resultado"></param>
        private void ValidarActivacionControles(ResultadoValidacion resultado)
        {
            gpbDatos.IsEnabled = resultado.Resultado;
            btnGuardar.IsEnabled = resultado.Resultado;
        }
        /// <summary>
        /// Carga la orden del dia actual
        /// </summary>
        private void CargarOrden()
        {
            try
            {
                listaSacrificios.Clear();
                listaSacrificiosBorrados.Clear();
                listaSacrificiosCorraleta.Clear();
                gridSacrificios.ItemsSource = null;
                var ordenSacrifioPl = new OrdenSacrificioPL();
                var resultadoValidacion = ValidarFechas();
                if (dtpFechaOrden.SelectedDate != null)
                {
                    ordenActual = ordenSacrifioPl.ObtenerOrdenSacrificioDelDia(new OrdenSacrificioInfo
                    {
                        OrganizacionID = organizacionId,
                        FechaOrden = (DateTime)dtpFechaOrden.SelectedDate,
                        EstatusID = (int)Estatus.OrdenSacrificioPendiente,
                        Activo = resultadoValidacion.Resultado
                    });
                }
                if (ordenActual != null)
                {
                    txtObservacion.Text = ordenActual.Observacion;
                    if (ordenActual.DetalleOrden != null)
                    {
                        listaSacrificios = ordenActual.DetalleOrden;
                        gridSacrificios.ItemsSource = null;
                        gridSacrificios.ItemsSource = listaSacrificios;
                        MostrarTotales();
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        /// <summary>
        /// consulta el corral por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        private CorralInfo ConsultarCorral(string codigo)
        {
            var corralPl = new CorralPL();
            var corralParametro = new CorralInfo
            {
                Codigo = codigo,
                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId }
            };
            return corralPl.ObtenerPorCodigoGrupo(corralParametro);
        }

        /// <summary>
        /// Se consulta el proveedor del lote
        /// </summary>
        /// <param name="loteOrigen"></param>
        /// <returns></returns>
        private string ConsultarProveedor(LoteInfo loteOrigen)
        {
            var descripcionProveedor = "";
            try
            {
                if (lote.TipoProcesoID == (int)TipoProceso.EngordaPropio)
                {
                    var configuracion = AuxConfiguracion.ObtenerConfiguracion();
                    descripcionProveedor = configuracion.ProveedorPropio;
                    proveedor = new ProveedorInfo { Descripcion = descripcionProveedor };
                }
                else
                {
                    var proveedorPl = new ProveedorPL();
                    proveedor = proveedorPl.ObtenerPorLote(loteOrigen);
                    if (proveedor != null)
                    {
                        descripcionProveedor = proveedor.Descripcion;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return descripcionProveedor;
        }

        /// <summary>
        /// Consultar dias de retiro
        /// </summary>
        /// <param name="loteAconsultar"></param>
        /// <returns></returns>
        private int ConsultarDiasRetiro(LoteInfo loteAconsultar)
        {
            var diasRetiro = 0;
            try
            {
                var repartoPl = new RepartoPL();
                diasRetiro = repartoPl.ObtenerDiasRetiro(loteAconsultar);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return diasRetiro;
        }

        /// <summary>
        /// Leer la configuracion de la pantalla
        /// </summary>
        private void LeerConfiguracion()
        {
            try
            {

                var parametrosPl = new ConfiguracionParametrosPL();
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.OrdenSacrificio,
                    OrganizacionID = organizacionId
                };
                var parametros = parametrosPl.ObtenerPorOrganizacionTipoParametro(parametroSolicitado);
                foreach (var parametro in parametros)
                {
                    ParametrosEnum enumTemporal;
                    var res = Enum.TryParse(parametro.Clave, out enumTemporal);

                    if (res)
                    {
                        switch (enumTemporal)
                        {
                            case ParametrosEnum.diasZigma:
                                diasZigma = int.Parse(parametro.Valor);
                                break;
                            case ParametrosEnum.diasMinimos:
                                diferenciasZigma = int.Parse(parametro.Valor);
                                break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.OrdenSacrificio_ErrorConfiguracion, MessageBoxButton.OK, MessageImage.Error);
                InavilitarControles();
            }
        }

        /// <summary>
        /// Valida datos en blanco
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarDatosEnBlanco()
        {

            var resultado = new ResultadoValidacion();
            if (String.IsNullOrEmpty(txtCorral.Text))
            {
                resultado.Resultado = false;
                resultado.Control = txtCorral;
                resultado.Mensaje = Properties.Resources.OrdenSacrificio_ErrorRequireCorral;
                txtCorral.Focus();
                return resultado;
            }
            if (String.IsNullOrEmpty(txtLote.Text))
            {
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OrdenSacrificio_ErrorRequireLote;
                return resultado;
            }
            if (String.IsNullOrEmpty(txtCabezas.Text))
            {
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OrdenSacrificio_ErrorRequireCabezas;
                return resultado;
            }
            if (String.IsNullOrEmpty(txtTipoGanado.Text))
            {
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OrdenSacrificio_ErrorRequireClasificacion;
                return resultado;
            }
            if (String.IsNullOrEmpty(txtProveedor.Text))
            {
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OrdenSacrificio_ErrorRequireProveedor;
                return resultado;
            }
            if (String.IsNullOrEmpty(txtDiasRetiro.Text))
            {
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OrdenSacrificio_ErrorDiasRetiro;
                return resultado;
            }
            if (String.IsNullOrEmpty(txtCabezasSacrificar.Text))
            {
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OrdenSacrificio_ErrorCabezasSacrificar;
                txtCabezasSacrificar.Focus();
                return resultado;
            }
            if (String.IsNullOrEmpty(txtCorraletas.Text))
            {
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.OrdenSacrificio_ErrorCorraleta;
                txtCorraletas.Focus();
                return resultado;
            }
            resultado.Resultado = true;
            return resultado;
        }
        /// <summary>
        /// Valida si la orden se puede modificar
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarFechas()
        {
            var resultado = new ResultadoValidacion { Resultado = true };
            if (dtpFechaOrden.SelectedDate != null)
            {
                var fechaOrden = (DateTime)dtpFechaOrden.SelectedDate;

                var fechaActual = DateTime.Now;

                var fechaProxima = DateTime.Now.AddDays(2);


                if (fechaOrden.Date.CompareTo(fechaActual.Date) < 0)
                {
                    resultado.Resultado = false;
                }
                else
                {
                    if (fechaOrden.Date.CompareTo(fechaProxima.Date) > 0)
                    {
                        resultado.Resultado = false;
                    }
                }
            }
            return resultado;
        }
        /// <summary>
        /// Mustra los totales
        /// </summary>
        private void MostrarTotales()
        {
            //var totalCabezas = 0;
            var totalSacrificios = gridSacrificios.ItemsSource.Cast<OrdenSacrificioDetalleInfo>().Sum(ordenSacrificioInfo => ordenSacrificioInfo.CabezasASacrificar);

            lblTotalCabezasScrificar.Content = totalSacrificios.ToString(CultureInfo.InvariantCulture);

        }
        /// <summary>
        /// Muestra los datos de la orden
        /// </summary>
        /// <param name="orden"></param>
        private void MostrarOrden(OrdenSacrificioDetalleInfo orden)
        {
            txtDiasRetiro.Text = orden.DiasRetiro.ToString(CultureInfo.InvariantCulture);
            txtCabezasSacrificar.Text = orden.CabezasASacrificar.ToString(CultureInfo.InvariantCulture);
            txtTipoGanado.Text = orden.Clasificacion;
            txtCorraletas.Text = orden.CorraletaCodigo;
            txtCorral.Text = orden.Corral.Codigo;
            txtLote.Text = orden.Lote.Lote;
            txtCabezas.Text = orden.Cabezas.ToString(CultureInfo.InvariantCulture);
            txtProveedor.Text = orden.Proveedor.Descripcion;

        }
        /// <summary>
        /// Guarda la orden actual
        /// </summary>
        /// <returns></returns>
        private bool Guardar()
        {
            var resultado = false;
            try
            {
                var ordenPl = new OrdenSacrificioPL();
                if (ordenActual == null)
                {
                    ordenActual = new OrdenSacrificioInfo { OrdenSacrificioID = 0 };
                }
                if (ordenActual != null)
                {
                    listaSacrificios = (IList<OrdenSacrificioDetalleInfo>)gridSacrificios.ItemsSource;
                    var orden = 1;
                    foreach (var detalle in listaSacrificios)
                    {
                        detalle.Orden = orden;
                        orden++;
                    }
                    ordenActual.OrganizacionID = organizacionId;
                    ordenActual.Observacion = txtObservacion.Text;
                    ordenActual.UsuarioCreacion = usuario;
                    if (dtpFechaOrden.SelectedDate != null)
                    {
                        ordenActual.FechaOrden = (DateTime)dtpFechaOrden.SelectedDate;
                    }
                    else
                    {
                        ordenActual.FechaOrden = new DateTime();
                    }

                    var ordenSacrificioResultado = ordenPl.GuardarOrdenSacrificio(ordenActual, listaSacrificios, organizacionId);

                    if (ordenSacrificioResultado != null)
                    {
                        Eliminar();
                        CargarOrden();
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }
        /// <summary>
        /// Elimina el detalle borrado
        /// </summary>
        private void Eliminar()
        {
            try
            {
                var ordenPl = new OrdenSacrificioPL();

                if (listaSacrificiosBorrados != null)
                {
                    if (listaSacrificiosBorrados.Count > 0)
                    {
                        ordenPl.EliminarDetalleOrdenSacrificio(listaSacrificiosBorrados, usuario);
                        listaSacrificiosBorrados = new List<OrdenSacrificioDetalleInfo>();
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Valida el numero de cabezas
        /// </summary>
        /// <returns></returns>
        private bool ValidarCabezas()
        {
            try
            {
                var ordenSacrifioPl = new OrdenSacrificioPL();
                var cabezasEnEdicion = 0;
                if (!nuevo)
                {
                    if (gridSacrificios.SelectedIndex >= 0)
                    {
                        var detalleSeleccionado = (OrdenSacrificioDetalleInfo)gridSacrificios.SelectedItem;
                        if (detalleSeleccionado != null)
                        {
                            cabezasEnEdicion = detalleSeleccionado.CabezasASacrificar;
                        }

                    }
                }
                var ordenActualId = 0;
                if (ordenActual != null)
                {
                    ordenActualId = ordenActual.OrdenSacrificioID;
                }
                var cabezasEnOtrasOrdenes = ordenSacrifioPl.ObtnerCabezasDiferentesOrdenes(lote, ordenActualId);
                var cabezasEnLaorden =
                    listaSacrificios.Where(ordenSacrificioInfo => ordenSacrificioInfo.Corral.CorralID == corral.CorralID)
                        .Sum(ordenSacrificioInfo => ordenSacrificioInfo.CabezasASacrificar);
                var cabezasDisponibles = int.Parse(txtCabezas.Text) - cabezasEnLaorden + cabezasEnEdicion - cabezasEnOtrasOrdenes;
                var cabezasAsacrificar = int.Parse(txtCabezasSacrificar.Text);

                if (cabezasAsacrificar > cabezasDisponibles)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return true;
        }
        /// <summary>
        /// Valida el corral origen
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCorral()
        {
            var resultado = new ResultadoValidacion { Resultado = false };
            if (txtCorral.Text.Length > 0)
            {
                corral = ConsultarCorral(txtCorral.Text);

                if (corral != null)
                {
                    if (corral.GrupoCorral == (int)GrupoCorralEnum.Produccion)
                    {
                        var lotePl = new LotePL();
                        lote = lotePl.ObtenerPorCorralCerrado(organizacionId, corral.CorralID);
                        if (lote != null)
                        {

                            //var fechaDefault = new DateTime(1900, 1, 1);
                            //if (fechaDefault == lote.FechaCierre)
                            //{
                            //    resultado.Resultado = false;
                            //    resultado.Mensaje = Properties.Resources.OrdenSacrificio_LoteAbiero;
                            //}
                            //else
                            //{
                                resultado.Resultado = true;
                                txtLote.Text = lote.Lote;
                                txtCabezas.Text = lote.Cabezas.ToString(CultureInfo.InvariantCulture);
                                if (banderaAplicaMarel)
                                {
                                    txtCabezasSacrificar.Text = lote.Cabezas.ToString(CultureInfo.InvariantCulture);
                                }
                                TipoGanadoInfo tipoGanadoInfo = lotePl.ObtenerTipoGanadoCorral(lote);
                                txtTipoGanado.Text = tipoGanadoInfo.Descripcion;
                                //txtTipoGanado.Text = lotePl.ObtenerTipoGanadoCorral(lote);
                                txtProveedor.Text = ConsultarProveedor(lote);
                                txtDiasRetiro.Text = ConsultarDiasRetiro(lote).ToString(CultureInfo.InvariantCulture);

                                if(lote.TipoCorralID == TipoCorral.Intensivo.GetHashCode())
                                {
                                    TipoGanadoInfo tipoGanado = ObtenerTipoGanadoIntensivo(lote);

                                    if (tipoGanado == null)
                                    {
                                        resultado.Resultado = false;
                                        resultado.Mensaje = Properties.Resources.OrdenSacrificio_IntensivoSinInformacion;
                                        //return null;
                                    }
                                    else
                                    {
                                        txtTipoGanado.Text = tipoGanado.Descripcion;
                                    }
                                }

                            //}

                        }
                        else
                        {
                            resultado.Resultado = false;
                            resultado.Mensaje = Properties.Resources.OrdenSacrificio_SinLote;
                        }


                    }
                    else
                    {
                        if (corral.GrupoCorral == (int)GrupoCorralEnum.Corraleta && corral.TipoCorral.TipoCorralID == (int)TipoCorral.CorraletaSacrificio)
                        {
                            var lotePl = new LotePL();
                            var corralPl = new CorralPL();
                            lotesCorraleta = lotePl.ObtenerLoteDeCorraleta(corral);

                            if (lotesCorraleta != null)
                            {
                                var cabezasAsacrificar = 0;
                                var clasificacion = "";
                                var proveedores = "";
                                txtLote.Text = "";

                                listaSacrificiosCorraleta.Clear();
                                foreach (var lotetmp in lotesCorraleta)
                                {
                                    var tmpOrden = new OrdenSacrificioDetalleInfo();
                                    cabezasAsacrificar += lotetmp.Cabezas;

                                    txtLote.Text = AgregarComa(txtLote.Text, lotetmp.Lote);

                                    TipoGanadoInfo tipoGanadoInfo = lotePl.ObtenerTipoGanadoCorral(lotetmp);

                                    if (lote.TipoCorralID == TipoCorral.Intensivo.GetHashCode())
                                    {
                                        tipoGanadoInfo = ObtenerTipoGanadoIntensivo(lote);

                                        if (tipoGanadoInfo == null)
                                        {
                                            resultado.Resultado = false;
                                            resultado.Mensaje = Properties.Resources.OrdenSacrificio_IntensivoSinInformacion;
                                            return null;
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(tipoGanadoInfo.Descripcion))
                                    {
                                        clasificacion = AgregarComa(clasificacion, tipoGanadoInfo.Descripcion);
                                    }
                                    var tmpProveedor = ConsultarProveedor(lotetmp);
                                    if (!string.IsNullOrEmpty(tmpProveedor))
                                    {
                                        proveedores = AgregarComa(proveedores, tmpProveedor);
                                    }

                                    tmpOrden.Cabezas = lotetmp.Cabezas;
                                    tmpOrden.Lote = lotetmp;
                                    tmpOrden.Corraleta = corraleta;
                                    tmpOrden.CabezasASacrificar = lotetmp.Cabezas;
                                    tmpOrden.Clasificacion = tipoGanadoInfo.Descripcion;
                                    tmpOrden.TipoGanadoID = tipoGanadoInfo.TipoGanadoID;
                                    tmpOrden.Corral = corralPl.ObtenerPorId(lotetmp.CorralID);
                                    tmpOrden.DiasEngordaGrano = 0;
                                    tmpOrden.DiasRetiro = 0;
                                    tmpOrden.FolioOrdenSacrificio = 0;
                                    tmpOrden.Turno = TurnoEnum.Matutino;
                                    tmpOrden.Turnos = ListaTurnos;
                                    tmpOrden.Proveedor = new ProveedorInfo { Descripcion = tmpProveedor };
                                    tmpOrden.OrigenCorraleta = true;

                                    listaSacrificiosCorraleta.Add(tmpOrden);

                                }

                                txtCabezasSacrificar.Text = cabezasAsacrificar.ToString(CultureInfo.InvariantCulture);
                                txtCabezas.Text = cabezasAsacrificar.ToString(CultureInfo.InvariantCulture);
                                txtCabezasSacrificar.IsEnabled = false;
                                resultado.Resultado = true;

                                txtTipoGanado.Text = clasificacion;
                                txtProveedor.Text = proveedores;
                                txtDiasRetiro.Text = "0";
                            }
                            else
                            {
                                resultado.Resultado = false;
                                resultado.Mensaje = Properties.Resources.OrdenSacrificio_CorraletaSinGanado;
                            }

                        }
                        else
                        {
                            resultado.Resultado = false;
                            resultado.Mensaje = Properties.Resources.OrdenSacrificio_CorralNoProduccion;
                        }

                    }

                }
                else
                {

                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.OrdenSacrificio_CorralNoExiste;

                }
            }

            return resultado;
        }

        private TipoGanadoInfo ObtenerTipoGanadoIntensivo(LoteInfo loteIntensivo)
        {
            TipoGanadoInfo tipoGanado = null;
            var interfaceSalidaTraspasoPL = new InterfaceSalidaTraspasoPL();

            InterfaceSalidaTraspasoInfo interfaceSalida =
                interfaceSalidaTraspasoPL.ObtenerInterfaceSalidaTraspasoPorLote(loteIntensivo);

            if(interfaceSalida == null)
            {
                return null;
            }

            var agrupadosPorTipoGanado = (from animales in interfaceSalida.ListaInterfaceSalidaTraspasoDetalle
                                          group animales by animales.TipoGanado.TipoGanadoID
                                              into tiposGanado
                                              let firstOrDefault = tiposGanado.FirstOrDefault()
                                              where firstOrDefault != null
                                              select new
                                              {
                                                  firstOrDefault.TipoGanado,
                                                  TotalTipoGanado = tiposGanado.Count(),
                                                  Porcentaje = ((decimal)tiposGanado.Count() / (decimal)interfaceSalida.ListaInterfaceSalidaTraspasoDetalle.Count) * 100
                                              }).ToList();

            var porcentajeMayorTipoGanado = agrupadosPorTipoGanado.Max(agrupado => agrupado.Porcentaje);

            var tipoGanadoMayor = (from tipo in agrupadosPorTipoGanado
                                   where tipo.Porcentaje == porcentajeMayorTipoGanado
                                   select tipo);

            var tipoGanadoFinal =
                tipoGanadoMayor.OrderByDescending(tipo => tipo.TipoGanado.TipoGanadoID).FirstOrDefault();

            if (tipoGanadoFinal != null)
            {
                tipoGanado = tipoGanadoFinal.TipoGanado;
            }
            return tipoGanado;
        }

        /// <summary>
        /// Agrega coma a las agrupaciones de datos
        /// </summary>
        /// <param name="text"></param>
        /// <param name="elemento"></param>
        /// <returns></returns>
        private string AgregarComa(string text, string elemento)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = elemento;
            }
            else
            {
                text += ", " + elemento;
            }

            return text;
        }

        /// <summary>
        /// Limpia los controles
        /// </summary>
        private void LimpiarControles()
        {
            btnAgregar.Content = Properties.Resources.OrdenSacrificio_btnAgregar;
            nuevo = true;
            indiceActualizado = -1;
            corral = null;
            //corraleta = null;
            lote = null;
            proveedor = null;
            ordenActualizada = null;
            txtCabezas.Clear();
            txtCabezasSacrificar.Clear();
            txtCorral.Clear();
            txtCorraletas.Clear();
            txtDiasRetiro.Clear();
            txtLote.Clear();
            txtProveedor.Clear();
            txtTipoGanado.Clear();
            txtCorral.Focus();
            txtCabezasSacrificar.Clear();
            txtCabezasSacrificar.IsEnabled = true;
            listaSacrificiosCorraleta.Clear();
        }
        /// <summary>
        /// Consulta para obtener la corraleta de sacrificio configurada para la organizacion
        /// </summary>
        private void ConsultarCorraletaSacrificio()
        {
            var corralPl = new CorralPL();
            var corralParametro = new CorralInfo
            {
                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId }
            };
            var corralSacrificio = corralPl.ObtenerCorraletaSacrificio(corralParametro);
            if (corralSacrificio == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.OrdenSacrificio_ErrorCorraletaSacrificio, MessageBoxButton.OK, MessageImage.Error);
                InavilitarControles();
            }
            else
            {
                corraleta = corralSacrificio;
            }

        }
        /// <summary>
        /// Inivalita los controles
        /// </summary>
        private void InavilitarControles()
        {
            gpbDatos.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnImprimir.IsEnabled = false;
            txtObservacion.IsEnabled = false;
            btnSubir.IsEnabled = false;
            btnBajar.IsEnabled = false;
            dtpFechaOrden.IsEnabled = false;
        }

        private bool ValidarCorrarRepetido()
        {
            if (listaSacrificios.Where(x => x.Corral.CorralID == corral.CorralID).ToList().Count > 0)
            {
                return false;
            }
            return true;
        }

        private bool AplicaEnvioMarel()
        {
            var result = false;
            try
            {
                var paramOrgPl = new ParametroOrganizacionPL();
                var paramOrg = paramOrgPl.ObtenerPorOrganizacionIDClaveParametro(organizacionId, ParametrosEnum.AplicaSalidaSacrificioMarel.ToString());

                if (paramOrg != null)
                {
                    if (paramOrg.Valor.Trim() == "1")
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.OrdenSacrificio_ErrorValidarEnvioMarel,
                        MessageBoxButton.OK,
                        MessageImage.Error);
            }
            
            return result;
        }

        #endregion

    }

}
