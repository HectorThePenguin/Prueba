using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Manejo
{
    public partial class CancelacionOrdenSacrificio
    {
        #region VARIABLES
        private int organizacionId;
        Dictionary<int, int> dicCabezas;
        Dictionary<int, int> datoAnteriorCabezas;
        Dictionary<int, int> datosDetalleOrden;
        private int usuarioId;
        private IList<OrdenSacrificioDetalleInfo> listaOrdenDetalle;
        public OrdenSacrificioInfo Modelo
        {
            get { return DataContext as OrdenSacrificioInfo; }
            set { DataContext = value; }
        }
        #endregion VARIABLES

        #region CONSTRUCTOR
        public CancelacionOrdenSacrificio()
        {
            try
            {
                listaOrdenDetalle = new List<OrdenSacrificioDetalleInfo>();
                organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
                usuarioId = AuxConfiguracion.ObtenerUsuarioLogueado();
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #endregion CONSTRUCTOR

        #region METODOS
        public void ObtenerOrdenSacrificio()
        {
            string fecha = dtpFecha.SelectedDate.ToString();
            datosDetalleOrden = new Dictionary<int, int>();
            dicCabezas = new Dictionary<int, int>();
            datoAnteriorCabezas = new Dictionary<int, int>();
            var ordensacrificioPL = new OrdenSacrificioPL();
            Modelo = ordensacrificioPL.OrdenSacrificioFecha(fecha, organizacionId);
            if (Modelo == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.CancelacionOrdenSacrificio_NoExisteOS, fecha.Substring(0, 10)), MessageBoxButton.OK, MessageImage.Warning);
            }
            else
            {
                Modelo.DetalleOrden = ordensacrificioPL.ObtenerDetalleOrden(Modelo.OrdenSacrificioID);
                if (Modelo.DetalleOrden != null)
                {
                    if (Modelo.DetalleOrden.Count == 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ValidarExisteOS, MessageBoxButton.OK, MessageImage.Warning);
                    }
                    else
                    {
                        foreach (var od in Modelo.DetalleOrden)
                        {
                            if (od.Corral.TipoCorral.TipoCorralID != TipoCorral.Intensivo.GetHashCode() && od.CabezasActuales == 0)
                            {
                                od.Seleccionable = false;
                            }
                            listaOrdenDetalle.Add(od);
                        }

                        foreach (var dd in Modelo.DetalleOrden)
                        {
                            datosDetalleOrden.Add(dd.Lote.LoteID, dd.CabezasASacrificar);
                        }
                        Modelo.DetalleOrden.ToList().ForEach(e =>
                        {
                            dicCabezas.Add(e.Lote.LoteID, e.CabezasASacrificar);
                            datoAnteriorCabezas.Add(e.Lote.LoteID, e.CabezasASacrificar);
                        });
                        gridOrdenesSacrificios.GetBindingExpression(
                            DataGrid.ItemsSourceProperty)
                            .UpdateTarget();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ValidarExisteOS, MessageBoxButton.OK, MessageImage.Warning);
                }
            }
        }

        private bool ValidaGuardar()
        {
            var result = false;
            try
            {
                if (!dtpFecha.SelectedDate.HasValue)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ValidarFecha, MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorValidar, MessageBoxButton.OK, MessageImage.Error);
            }
            return result;
        }

        private bool ValidarCorralesActivos()
        {
            var pl = new OrdenSacrificioPL();
            bool result = false;
            var detSeleccionado = new List<OrdenSacrificioDetalleInfo>();
            foreach (var d in Modelo.DetalleOrden)
            {
                if (d.Seleccionar == 1)
                {
                    detSeleccionado.Add(d);
                }
            }
            var corralesConLote = pl.ValidarCorralConLotesActivos(organizacionId, detSeleccionado);
            var mensaje = string.Empty;

            foreach (var ccl in corralesConLote)
            {
                mensaje = mensaje != string.Empty ? string.Format("{0}, {1}", mensaje, ccl) : string.Format("{0}", ccl);
            }

            if (mensaje != string.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.CancelacionOrdenSacrificio_MsjCorralesConLote, mensaje), MessageBoxButton.OK, MessageImage.Warning);
            }
            else
            {
                result = true;
            }

            return result;
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
                Logger.Error(new Exception(Properties.Resources.CancelacionOrdenSacrificio_ErrorValidarEnvioMarel));
                throw;
            }

            return result;
        }

        private void Guardar()
        {
            try
            {
                var osPl = new OrdenSacrificioPL();
                var guardar = true;
                string fecha = dtpFecha.SelectedDate.Value.ToString("yyyy-MM-dd");

                var detSeleccionado = new List<OrdenSacrificioDetalleInfo>();
                foreach (var d in Modelo.DetalleOrden)
                {
                    if (d.Seleccionar == 1)
                    {
                        detSeleccionado.Add(d);
                    }
                }

                var xml = ArmarXMLGuardar(detSeleccionado, fecha);
                var xml2 = ArmarXMLGuardarMarel(detSeleccionado, fecha);
                if (AplicaEnvioMarel())
                {
                    if (ConsultarEstatus(detSeleccionado))
                    {
                        if (!osPl.CancelacionOrdenSacrificioMarel(xml2, Modelo.OrganizacionID))
                        {
                            guardar = false;
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorCancelarOS, MessageBoxButton.OK, MessageImage.Correct);
                        }
                    }
                    else
                    {
                        guardar = false;
                    }
                }
                else
                {
                    /// aqui hay que mover
                    if (!osPl.CancelacionOrdenSacrificioSCP(xml, fecha, organizacionId))
                    {
                        guardar = false;
                    }
                }

                if (guardar)
                {
                    if (osPl.ValidaCancelacionCabezasSIAP(xml, Modelo.OrdenSacrificioID, usuarioId))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_GuardarExito, MessageBoxButton.OK, MessageImage.Correct);
                        ObtenerOrdenSacrificio();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorCancelarOS, MessageBoxButton.OK, MessageImage.Correct);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        private string ArmarXMLGuardar(IEnumerable<OrdenSacrificioDetalleInfo> listaDetalleOrdenFinal, string fecha)
        {
            var result = "<OrdenSacrificio>";
            foreach (var d in listaDetalleOrdenFinal)
            {
                result += "<DetalleOrden>";
                result += "<Corral>" + d.Corral.Codigo + "</Corral>";
                result += "<Lote>" + d.Lote.Lote + "</Lote>";
                result += "<LoteID>" + d.Lote.LoteID + "</LoteID>";
                result += "<Fecha>" + fecha + "</Fecha>";
                result += "<CabezasSacrificio>" + d.CabezasASacrificar + "</CabezasSacrificio>";
                result += "<FolioOrdenSacrificio>" + d.FolioOrdenSacrificio + "</FolioOrdenSacrificio>";
                result += "<OrdenSacrificio>" + d.OrdenSacrificioID + "</OrdenSacrificio>";
                result += "</DetalleOrden>";
            }
            result += "</OrdenSacrificio>";
            return result;
        }

        private string ArmarXMLGuardarMarel(IEnumerable<OrdenSacrificioDetalleInfo> listaDetalleOrdenFinal, string fecha)
        {
            var result = "<OrdenSacrificio>";
            foreach (var d in listaDetalleOrdenFinal)
            {
                result += "<DetalleOrden>";
                result += "<Corral>" + d.Corral.Codigo + "</Corral>";
                result += "<Lote>" + d.Lote.Lote + "</Lote>";
                result += "<Fecha>" + fecha + "</Fecha>";
                result += "<CabezasSacrificio>" + d.CabezasASacrificar + "</CabezasSacrificio>";
                result += "</DetalleOrden>";
            }
            result += "</OrdenSacrificio>";
            return result;
        }

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
                etiquetasImpresion.Observaciones = Modelo.Observacion.ToString();

                if (dtpFecha.SelectedDate != null)
                {
                    etiquetasImpresion.FechaReporte = (DateTime)dtpFecha.SelectedDate;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorEtiquetas, MessageBoxButton.OK, MessageImage.Error);
            }
            return etiquetasImpresion;
        }

        private bool ConsultarEstatus(List<OrdenSacrificioDetalleInfo> detalle)
        {
            var result = false;
            try
            {
                var pl = new OrdenSacrificioPL();
                string fecha = dtpFecha.SelectedDate.Value.ToString("yyyy-MM-dd");
                var lotes = pl.ConsultarEstatusOrdenSacrificioEnMarel(organizacionId, fecha, detalle);
                result = ObtenerLotesConDeterminadoEstatus(lotes);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorConsultarEstatus, MessageBoxButton.OK, MessageImage.Error);
            }
            return result;
        }

        private bool ObtenerLotesConDeterminadoEstatus(List<SalidaSacrificioInfo> detalle)
        {
            var result = true;
            var mensaje1 = string.Empty;
            var mensaje3 = string.Empty;

            if (detalle.Any())
            {
                foreach (var d in detalle.Where(item => item.Estatus == EstatusExportMarel.TransmitidoPorInnova.GetHashCode()))
                {
                    result = false;
                    if (mensaje1 == string.Empty)
                    {
                        mensaje1 = string.Format("\n\n{0}", d.Lote);
                    }
                    else
                    {
                        mensaje1 = string.Format("{0},\n{1}", mensaje1, d.Lote);
                    }
                }

                if (mensaje1 != string.Empty)
                {
                    MessageBox.Show(string.Format(Properties.Resources.CancelacionOrdenSacrificio_MsjTransInnova, mensaje1), Properties.Resources.CancelacionOrdenSacrificio_Titulo, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                foreach (var d in detalle.Where(item => item.Estatus == EstatusExportMarel.CanceladoPorOrden.GetHashCode()))
                {
                    result = false;
                    if (mensaje3 == string.Empty)
                    {
                        mensaje3 = string.Format("\n\n{0}", d.Lote);
                    }
                    else
                    {
                        mensaje3 = string.Format("{0},\n{1}", mensaje3, d.Lote);
                    }
                }

                if (mensaje3 != string.Empty)
                {
                    MessageBox.Show(string.Format(Properties.Resources.CancelacionOrdenSacrificio_MsjCancSIAPoSCP, mensaje3), Properties.Resources.CancelacionOrdenSacrificio_Titulo, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                result = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorInconsistencias, MessageBoxButton.OK, MessageImage.Error);
            }


            return result;
        }

        #endregion METODOS

        #region EVENTOS
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var fechaInicial = DateTime.Now.AddDays(0);
            //dtpFecha.DisplayDateStart = fechaInicial;
            dtpFecha.SelectedDate = DateTime.Now;
            dtpFecha.Focus();
        }



        private void btnBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            //obtenerOrdenSacrificio();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerOrdenSacrificio();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var seleccionados = 0;
                foreach (var d in Modelo.DetalleOrden)
                {
                    if (dicCabezas[d.Lote.LoteID] < d.CabezasASacrificar)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.CancelacionOrdenSacrificio_LoteMayoresOrigina, d.Lote.Lote, dicCabezas[d.Lote.LoteID]), MessageBoxButton.OK, MessageImage.Error);
                        return;
                    }
                    if (d.Seleccionar == 1)
                    {
                        seleccionados++;
                    }
                }
                if (seleccionados > 0)
                {
                    if(ValidarCorralesActivos())
                    {
                        if (dtpFecha.SelectedDate.HasValue && dtpFecha.SelectedDate.Value < DateTime.Now )
                        {
                            CancelarSoloOrdenSIAP();
                            return;
                        }

                        if (!dtpFecha.SelectedDate.HasValue)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ValidarFecha, MessageBoxButton.OK, MessageImage.Warning);
                        }
                        else
                        {
                            Guardar();
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_SinSeleccionar, MessageBoxButton.OK, MessageImage.Warning);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void btnBajar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridOrdenesSacrificios.SelectedIndex > -1)
                {

                    if (gridOrdenesSacrificios.SelectedIndex < gridOrdenesSacrificios.Items.Count - 1)
                    {
                        var indice = gridOrdenesSacrificios.SelectedIndex;
                        var ordenSuperior = listaOrdenDetalle[indice + 1];
                        var ordenTemporal = (OrdenSacrificioDetalleInfo)gridOrdenesSacrificios.SelectedItem;

                        listaOrdenDetalle.RemoveAt(indice + 1);
                        listaOrdenDetalle.RemoveAt(indice);
                        listaOrdenDetalle.Insert(indice, ordenSuperior);
                        listaOrdenDetalle.Insert(indice + 1, ordenTemporal);

                        gridOrdenesSacrificios.ItemsSource = null;
                        gridOrdenesSacrificios.ItemsSource = listaOrdenDetalle;
                        gridOrdenesSacrificios.SelectedIndex = indice + 1;
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorInesperado, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void btnSubir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridOrdenesSacrificios.SelectedIndex > -1)
                {
                    if (gridOrdenesSacrificios.SelectedIndex > 0)
                    {
                        var indice = gridOrdenesSacrificios.SelectedIndex;
                        var ordenAnterior = listaOrdenDetalle[indice - 1];
                        var ordenTemporal = (OrdenSacrificioDetalleInfo)gridOrdenesSacrificios.SelectedItem;
                        listaOrdenDetalle.RemoveAt(indice);
                        listaOrdenDetalle.RemoveAt(indice - 1);
                        listaOrdenDetalle.Insert(indice - 1, ordenTemporal);
                        listaOrdenDetalle.Insert(indice, ordenAnterior);
                        gridOrdenesSacrificios.ItemsSource = null;
                        gridOrdenesSacrificios.ItemsSource = listaOrdenDetalle;
                        gridOrdenesSacrificios.SelectedIndex = indice - 1;
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorInesperado, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void txtCabezasSacrificar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridOrdenesSacrificios.Items.Count > 0)
                {
                    var ordenSacrificioPl = new OrdenSacrificioPL();
                    ordenSacrificioPl.ImprimirOrdenSacrificio(ObtenerEtiquetasImpresion(), Modelo.DetalleOrden);
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorInesperado, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion EVENTOS

        private void CancelarSoloOrdenSIAP()
        {
            var osPl = new OrdenSacrificioPL();
            string fecha = dtpFecha.SelectedDate.Value.ToString("yyyy-MM-dd");

            var detSeleccionado = new List<OrdenSacrificioDetalleInfo>();
            foreach (var d in Modelo.DetalleOrden)
            {
                if (d.Seleccionar == 1)
                {
                    detSeleccionado.Add(d);
                }
            }

            var xml = ArmarXMLGuardar(detSeleccionado, fecha);

            if (osPl.ValidaCancelacionCabezasSIAP(xml, Modelo.OrdenSacrificioID, usuarioId))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_GuardarExito, MessageBoxButton.OK, MessageImage.Correct);
                ObtenerOrdenSacrificio();
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorCancelarOS, MessageBoxButton.OK, MessageImage.Correct);
            }
        }

        private void DtpFecha_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dtpFecha.SelectedDate.HasValue)
                {
                    if (dtpFecha.SelectedDate.Value.Date < DateTime.Now.AddDays(-2).Date)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CancelacionOrdenSacrificio_FechaInvalida,
                            MessageBoxButton.OK, MessageImage.Warning);
                        dtpFecha.SelectedDate = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CancelacionOrdenSacrificio_ErrorInesperado, MessageBoxButton.OK, MessageImage.Error);
            }
        }
    }
}

