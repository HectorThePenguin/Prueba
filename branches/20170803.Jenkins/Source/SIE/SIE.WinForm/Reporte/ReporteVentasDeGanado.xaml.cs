using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteVentasDeGanado.xaml
    /// </summary>
    public partial class ReporteVentasDeGanado
    {
        #region Propiedades

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

        #region CONSTRUCTORES

        public ReporteVentasDeGanado()
        {
            InitializeComponent();
            CargarOrganizaciones();

            InicializaContexto();
        }

        #endregion CONSTRUCTORES

        #region EVENTOS

        /// <summary>
        /// Se ejecuta al cargarse la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            
            LimpiarCampos();
        }

        /// <summary>
        /// Se ejecuta al presionar una tecla en el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FechasKeyDown(object sender, KeyEventArgs e)
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
                if (e.Key == Key.Delete || e.Key == Key.Back)
                {
                    try
                    {
                        var dt = sender as DatePicker;
                        Convert.ToDateTime(dt.Text);
                    }
                    catch (Exception)
                    {
                        Contexto.Valido = false;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }            
        }

        /// <summary>
        /// Se ejecuta al perder el foco el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaInicialLostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaInicial();

            Contexto.Valido = isValid;

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

        /// <summary>
        /// Se ejecuta al perder el foco el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaFinalLostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaFinal();

            Contexto.Valido = isValid;

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
       
        /// <summary>
        /// Se ejecuta al presionar el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportarExcelClick(object sender, RoutedEventArgs e)
        {
            Generar();
        }

        /// <summary>
        /// Se ejecuta al presionar el control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelarClick(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroFechasInfo
            {
                FechaInicial = null,
                FechaFinal = null,
                Valido = false
            };
        }

        /// <summary>
        /// Carga las organizaciones
        /// </summary>
        private void CargarOrganizaciones()
        {
            try
            {
                bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var organizacionesPl = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPl.ObtenerTipoGanaderas();
                if (usuarioCorporativo)
                {

                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReporteConsumoProgramadovsServido_cmbSeleccione,
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    cmbOrganizacion.SelectedItem = organizacion0;
                    cmbOrganizacion.IsEnabled = true;

                }
                else
                {
                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = organizacionId,
                            Descripcion = nombreOrganizacion
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    cmbOrganizacion.SelectedItem = organizacion0;
                    cmbOrganizacion.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ReporteMedicamentosAplicadosSanidad_ErrorCargarOrganizaciones,
                    MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos()
        {
            DtpFechaInicial.SelectedDate = null;
            DtpFechaFinal.SelectedDate = null;
            DtpFechaInicial.IsEnabled = true;
            DtpFechaFinal.IsEnabled = true;
            cmbOrganizacion.SelectedIndex = 0;
            cmbOrganizacion.Focus();
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
        /// Método para validar que la fecha inicial no sea mayor a la actual.
        /// </summary>
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
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaFinal()
        {
            string mensaje = Properties.Resources.ReporteVentasGanado_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteVentasGanado_MsgFechaInicialMayorActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorFechaInicial()
        {
            string mensaje = Properties.Resources.RecepcionReporteVentaMuerte_MsgFechaNoValida;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Muestra mensaje de advertencia en caso de que la fecha
        /// final sea mayor que la fecha actual
        /// </summary>
        private void MostrarMensajeFechaFinalMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteVentasGanado_MsgFechaFinalMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Genera el reporte
        /// </summary>
        private void Generar()
        {
            Buscar();
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            if (ValidaBuscar())
            {
                ObtenerReporte();
            }
        }

        /// <summary>
        /// Método para validar los controles de la pantalla.
        /// </summary>
        /// <returns></returns>
        private bool ValidaBuscar()
        {
            try
            {
                bool resultado = true;
                string mensaje = string.Empty;

                DateTime? fechaIni = Contexto.FechaInicial;
                DateTime? fechaFin = Contexto.FechaFinal;

                var organizacion = (OrganizacionInfo) cmbOrganizacion.SelectedItem;

                if (organizacion == null || organizacion.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.ReporteVentasGanado_MsgSelecioneOrganizacion;
                    cmbOrganizacion.Focus();
                }
                else if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteVentasGanado_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.ReporteVentasGanado_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteVentasGanado_MsgFechaInicialMayorActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteVentasGanado_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.ReporteVentasGanado_MsgFechaInicialMayorFechaFinal;
                    DtpFechaFinal.Focus();
                }
                else if (fechaIni < fechaFin.Value.AddYears(-1))
                {
                    mensaje = Properties.Resources.ReporteVentasGanado_MsgFechaInicialMenorAnio;
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

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerReporte()
        {
            try
            {
                if (DtpFechaInicial.SelectedDate != null && DtpFechaFinal.SelectedDate != null)
                {

                    var organizacion = (OrganizacionInfo) cmbOrganizacion.SelectedItem;

                    var reporteVentaGanadoBL = new ReporteVentaGanadoBL();
                    List<ReporteVentaGanado> resultadoInfo =
                        reporteVentaGanadoBL.GenerarReporteVentaGanado(organizacion.OrganizacionID,
                                                                       DtpFechaInicial.SelectedDate.Value,
                                                                       DtpFechaFinal.SelectedDate.Value);
                    if (resultadoInfo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.ReporteVentaMuerte_MsgSinInformacion, MessageBoxButton.OK,
                                          MessageImage.Warning);
                    }
                    else
                    {
                        var organizacionPl = new OrganizacionPL();

                        organizacion = organizacionPl.ObtenerPorIdConIva(organizacion.OrganizacionID);
                        var titulo = Properties.Resources.ReporteVentaGanado_TituloReporte;

                        var nombreOrganizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" +
                                                 organizacion.Division + ")";

                        foreach (var item in resultadoInfo)
                        {
                            item.Organizacion = nombreOrganizacion;
                            item.Titulo = titulo;
                            item.FechaFinal = DtpFechaFinal.SelectedDate.Value;
                            item.FechaInicial = DtpFechaInicial.SelectedDate.Value;
                        }

                        var documento = new ReportDocument();
                        var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
                                                    "\\Reporte\\RptReporteVentaGanado.rpt");
                        documento.Load(reporte);

                        documento.DataSourceConnections.Clear();
                        documento.SetDataSource(resultadoInfo);
                        documento.Refresh();

                        var forma = new ReportViewer(documento, Properties.Resources.ReporteVentaGanado_TituloReporte);
                        forma.MostrarReporte();
                        forma.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteVentaGanado_FalloReporte,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion METODOS
    }
}
