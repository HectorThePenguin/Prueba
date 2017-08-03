using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Linq;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Reporte   
{
    /// <summary>
    /// Lógica de interacción para ReporteDetalleCorte.xaml
    /// </summary>
    public partial class ReporteDetalleCorteTransferencia
    {

        #region Propiedades

        /// <summary>
        /// Propiedad que contiene los tratamientos a mostrar en la pantalla
        /// </summary>
        private readonly int[] tratamientosValidos = new[] { TipoMovimiento.Corte.GetHashCode(), TipoMovimiento.CortePorTransferencia.GetHashCode(), TipoMovimiento.Reimplante.GetHashCode() };


        private FiltroFechasInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroFechasInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion Propiedades

        #region Eventos
        /// <summary>
        /// Realiza la exportacion a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Generar();
        }
        /// <summary>
        /// Buscar
        /// </summary>
        public void Generar()
        {
            if (ValidaGenerar())
            {
                Consultar();
            }
        }

        /// <summary>
        /// Método para consultar Reporte 
        /// </summary>
        /// <returns></returns>
        private void Consultar()
        {
            try
            {

                if (cmbOrganizacion.SelectedIndex > 0)
                {
                    var organizacion = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
                    var organizacionPl = new OrganizacionPL();
                    
                    organizacion = organizacionPl.ObtenerPorIdConIva(organizacion.OrganizacionID);

                    var encabezado = new ReporteEncabezadoInfo()
                    {
                        TituloReporte = Properties.Resources.ReporteDetalleCorteTransferencia_TituloReporteDetalle,
                        TituloPeriodo = "Del " + DtpFechaInicial.Text + " al " + DtpFechaFinal.Text,
                        Organizacion = string.Format(Properties.Resources.Reporte_NombreEmpresa, organizacion.Division),
                    };

                    List<ReporteDetalleCorteModel> resultadoInfo = ObtenerReporteDetalleCorte(encabezado, organizacion);

                    if (resultadoInfo == null || !resultadoInfo.Any())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ReporteDetalleCorte_MsgSinInformacion,
                            MessageBoxButton.OK, MessageImage.Warning);
                        LimpiarCampos(true);
                        return;
                    }

                    DtpFechaInicial.IsEnabled = false;
                    DtpFechaFinal.IsEnabled = false;

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
                        "\\Reporte\\ReporteDetalleCorte.rpt");
                    documento.Load(reporte);


                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(resultadoInfo);
                    documento.Refresh();


                    var forma = new ReportViewer(documento, encabezado.TituloReporte);
                    forma.MostrarReporte();
                    forma.Show();
                }
                else
                {
                    ;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteMedicamentosAplicados_FalloCargarReporte, MessageBoxButton.OK,
                                                      MessageImage.Error);
            }
        }

        /// <summary>
        /// Método obtener el reporte Operacion de Sanidad
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        private List<ReporteDetalleCorteModel> ObtenerReporteDetalleCorte(ReporteEncabezadoInfo encabezado, OrganizacionInfo organizacion)
        {
            try
            {
                var reporteDetalleCortePl = new ReporteDetalleCortePL();

                DateTime fechaIni = DtpFechaInicial.SelectedDate.HasValue
                                        ? DtpFechaInicial.SelectedDate.Value
                                        : new DateTime();
                DateTime fechaFin = DtpFechaFinal.SelectedDate.HasValue
                                        ? DtpFechaFinal.SelectedDate.Value
                                        : fechaIni;
               
                int idUsuario = AuxConfiguracion.ObtenerUsuarioLogueado();

                List<ReporteDetalleCorteModel> resultadoInfo = reporteDetalleCortePl.ObtenerReporteDetalleCorte(encabezado, organizacion.OrganizacionID, fechaIni, fechaFin, idUsuario,TipoMovimiento.CortePorTransferencia.GetHashCode());

                return resultadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Método para validar los controles de la pantalla.
        /// </summary>
        /// <returns></returns>
        private bool ValidaGenerar()
        {
            try
            {
                bool resultado = true;
                string mensaje = string.Empty;

                DateTime? fechaIni = Contexto.FechaInicial;
                DateTime? fechaFin = Contexto.FechaFinal;

                if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteDetalleCorte_MsgSelecionePeriodo; //Debe selecionar un periodo.
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.ReporteDetalleCorte_MsgFechaIniRequerida; //Debe capturar la fecha inicial.
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteDetalleCorte_MsgFechaInicialMayorFechaActual; //El periodo de fechas no es válido, la fecha inicial no debe ser mayor a la actual.
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteDetalleCorte_MsgFechaFinRequerida; //Debe capturar la fecha final.
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.ReporteDetalleCorte_MsgFechaFinalMenorFechaInicial; //El periodo de fechas no es válido, la fecha final no debe ser menor a la inicial.
                    DtpFechaFinal.Focus();
                }
                else if (fechaIni < fechaFin.Value.AddYears(-1))
                {
                    mensaje = Properties.Resources.ReporteDetalleCorte_MsgFechaInicialMenorAnio; //Rango de fechas no permitido. Favor de ajustar al máximo de 1 año con respecto a la fecha final.
                    DtpFechaInicial.Focus();
                }

                if (!string.IsNullOrWhiteSpace(mensaje))
                {
                    resultado = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private void CmbOrganizacion_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var combo = (ComboBox) sender;
                if (combo.SelectedIndex == 0)
                {
                    LimpiarCampos(true);
                }
                else
                {
                    if (DtpFechaInicial.SelectedDate.HasValue && DtpFechaFinal.SelectedDate.HasValue)
                    {
                        Contexto.Valido = true;
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargaComboTipoProceso();
            LimpiarCampos(true);
        }

        private void Fechas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
                e.Handled = true;
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void DtpFechaInicial_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaInicial();

            Contexto.Valido = isValid && cmbOrganizacion.SelectedIndex > 0;;

            if (isValid)
            {
                return;
            }
            var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
            DtpFechaFinal.MoveFocus(tRequest);
            
            if (DtpFechaInicial.SelectedDate > DtpFechaFinal.SelectedDate)
            {
                MostrarMensajeFechaInicialMayorFechaFinal();
            }
            else
            {
                MostrarMensajeFechaInicialMayorFechaActual();
            }

            Contexto.FechaInicial = null;
            Contexto.FechaFinal = null;
            Contexto.Valido = false;
            e.Handled = true;
            DtpFechaInicial.Focus();
        }

        private void DtpFechaFinal_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaFinal();

            Contexto.Valido = isValid && cmbOrganizacion.SelectedIndex > 0;

            if (isValid)
            {
                return;
            }

            DtpFechaInicial.Focus();
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            DtpFechaFinal.MoveFocus(tRequest);
            if (DtpFechaFinal.SelectedDate > DateTime.Today)
            {
                MostrarMensajeFechaFinalMayorFechaActual();
            }
            else
            {
                if (Contexto.FechaInicial != null)
                {
                    MostrarMensajeFechaFinalMayorFechaInicial();
                }
            }
            Contexto.FechaFinal = null;
            Contexto.FechaInicial = null;
            Contexto.Valido = false;
            e.Handled = true;
            DtpFechaInicial.Focus();
        }

        private void CargaComboTipoProceso()
        {
            try
            {
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);

                int tipoOrganizacion = organizacion.TipoOrganizacion.TipoOrganizacionID;

                var organizacionesPL = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPL.ObtenerTipoGanaderas();

                var organizacion0 =
                    new OrganizacionInfo
                    {
                        OrganizacionID = 0,
                        Descripcion = Properties.Resources.ReporteDetalleCorte_TextoSeleccione,
                    };
                listaorganizaciones.Insert(0, organizacion0);

                cmbOrganizacion.ItemsSource = listaorganizaciones;

                if (tipoOrganizacion == TipoOrganizacion.Corporativo.GetHashCode())
                {
                    cmbOrganizacion.SelectedIndex = 0;
                }
                else
                {
                    var index = 0;
                    foreach (OrganizacionInfo org in cmbOrganizacion.Items)
                    {
                        if (org.OrganizacionID == organizacionId)
                        {

                            cmbOrganizacion.SelectedIndex = index;
                            break;
                        }

                        index++;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteDetalleCorte_ErrorCargarOrganizaciones,
                                  MessageBoxButton.OK, MessageImage.Error);

            }
        }


        #endregion
        public ReporteDetalleCorteTransferencia()
        {
            InitializeComponent();
            InicializaContexto();
        }
        
        #region Metodos
        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaFinal()
        {
            string mensaje = Properties.Resources.ReporteDetalleCorte_MsgFechaInicialMayorFechaFinal; //El periodo de fechas no es válido, la fecha inicial no puede ser mayor a la fecha final.
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteDetalleCorte_MsgFechaActual; //El periodo de fechas no es válido. La Fecha Inicial no debe ser mayor a la fecha actual.
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorFechaInicial()
        {
            string mensaje = Properties.Resources.ReporteDetalleCorte_MsgFechaNoValida; //El periodo de fechas no es válido. La fecha final no debe ser menor a la fecha inicial.
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Muestra mensaje de advertencia en caso de que la fecha
        /// final sea mayor que la fecha actual
        /// </summary>
        private void MostrarMensajeFechaFinalMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteDetalleCorte_MsgFechaFinalMayorFechaInicial; //El periodo de fechas no es válido. La Fecha Final no debe ser mayor a la fecha actual.
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        private bool ValidaFechaInicial()
        {
            bool result = true;
            DateTime? fecha = DtpFechaInicial.SelectedDate.HasValue
                                  ? DtpFechaInicial.SelectedDate
                                  : null;
            DateTime? fechaFinal = DtpFechaFinal.SelectedDate;

            if (fecha != null && (fecha > DateTime.Today || fecha > fechaFinal))
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Método para validar que la fecha inicial no sea mayor a la actual.
        /// </summary>
        private bool ValidaFechaFinal()
        {
            bool result = true;
            DateTime? fecha = DtpFechaFinal.SelectedDate.HasValue
                                  ? DtpFechaFinal.SelectedDate
                                  : null;
            if (fecha != null && (fecha < Contexto.FechaInicial || fecha > DateTime.Today))
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Cancela las selecciones actuales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos(true);
        }
        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos(bool cancelar)
        {
            DtpFechaInicial.SelectedDate = null;
            DtpFechaFinal.SelectedDate = null;
            DtpFechaInicial.IsEnabled = true;
            DtpFechaFinal.IsEnabled = true;
        }

        private void InicializaContexto()
        {
            Contexto = new FiltroFechasInfo
            {
                FechaInicial = null,
                FechaFinal = null,
                Valido = false
            };
        }
        #endregion
    }
}
