using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Validacion;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Windows.Controls;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporeteVentaMuerte.xaml
    /// </summary>
    public partial class ReporteVentasMuertes
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

        #region CONTRUCTORES

        public ReporteVentasMuertes()
        {
            InitializeComponent();
            CargaOrganizaciones();
            InicializaContexto();
        }

        #endregion CONSTRUCTORES

        #region EVENTOS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrganizacion_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //if (((OrganizacionInfo)cmbOrganizacion.SelectedItem).OrganizacionID != 0)
            //    Contexto.Valido = true;
            //else
            //    Contexto.Valido = false;
            //;
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
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void DtpFechaInicial_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaInicial();
            bool isValid2 = ValidarPeriodoAnio();
            Contexto.Valido = isValid & isValid2;

            if (isValid & isValid2)
            {
                return;
            }
            var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
            DtpFechaFinal.MoveFocus(tRequest);
            Contexto.FechaInicial = null;
            Contexto.FechaFinal = null;
            Contexto.Valido = false;
            e.Handled = true;
            DtpFechaInicial.Focus();

            if (!isValid2)
            {
                MostrarMensajeFechaInicialMayorUnAnio();
                return;
            }

            if (DtpFechaInicial.SelectedDate > DtpFechaFinal.SelectedDate)
            {
                MostrarMensajeFechaInicialMayorFechaFinal();
            }
            else
            {
                MostrarMensajeFechaInicialMayorFechaActual();
            }
           
            
        }

        private void DtpFechaFinal_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaFinal();
            bool isValid2 = ValidarPeriodoAnio();

            Contexto.Valido = isValid & isValid2;

            if (isValid & isValid2)
            {
                return;
            }

            DtpFechaInicial.Focus();
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            DtpFechaFinal.MoveFocus(tRequest);
           

            if (!isValid2)
            {
                MostrarMensajeFechaInicialMayorUnAnio();
            }
            else if (DtpFechaFinal.SelectedDate > DateTime.Today)
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
        /// Realiza la exportacion a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportarExcel_Click(object sender, RoutedEventArgs e)
        {
            Generar();
        }

        /// <summary>
        /// Cancela las selecciones actuales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        /// <summary>
        /// Se ejecuta al cargarse la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        #endregion EVENTOS

        #region METODOS

        private void CargaOrganizaciones()
        {
            try
            {
                bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var organizacionesPL = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPL.ObtenerTipoGanaderas();
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
                                  Properties.Resources.ReporteConsumoProgramadovsServido_ErrorCargarCombos,
                                  MessageBoxButton.OK, MessageImage.Error);

            }

        }

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
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos()
        {
            DtpFechaInicial.SelectedDate = null;
            DtpFechaFinal.SelectedDate = null;
            DtpFechaInicial.IsEnabled = true;
            DtpFechaFinal.IsEnabled = true;

            DtpFechaInicial.Focus();
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
        private void MostrarMensajeFechaInicialMayorUnAnio()
        {
            string mensaje = Properties.Resources.RecepcionReporteVentaMuerte_MsgFechaFinalMayordeunanio;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaFinal()
        {
            string mensaje = Properties.Resources.RecepcionReporteVentaMuerte_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.RecepcionReporteVentaMuerte_MsgFechaActual;
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
            string mensaje = Properties.Resources.RecepcionReporteVentaMuerte_MsgFechaFinalMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Validar que no exista mas de un año de rango entre la fecha inicial y la fecha final
        /// </summary>
        /// <returns></returns>
        private bool ValidarPeriodoAnio()
        {
            bool result = true;

            DateTime? fecha = DtpFechaFinal.SelectedDate.HasValue
                                  ? DtpFechaFinal.SelectedDate
                                  : null;
            var fechaInicial = DtpFechaInicial.SelectedDate.HasValue
                                  ? DtpFechaInicial.SelectedDate
                                  : null;



            if (fecha != null && fechaInicial != null)
            {
                fechaInicial = DtpFechaInicial.SelectedDate.Value.AddYears(1);
                var fechaFinal = DtpFechaFinal.SelectedDate.Value;
                if (fechaInicial <= fechaFinal)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Genera el archivo en formato de excel
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

                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;
                if (organizacion == null || organizacion.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.ReporteVentaMuerte_MsgSelecioneOrganizacion;
                    cmbOrganizacion.Focus();
                }
                if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.RecepcionReporteVentaMuerte_MsgFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.RecepcionReporteVentaMuerte_MsgFechaInicialMayorFechaFinal;
                    DtpFechaFinal.Focus();
                }
                else if (!ValidarPeriodoAnio())
                {
                    mensaje = Properties.Resources.RecepcionReporteVentaMuerte_MsgFechaFinalMayordeunanio;
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
                    int organizacionId = (int) cmbOrganizacion.SelectedValue;

                    var reporteVentaMuerteBL = new ReporteVentaMuerteBL();
                    List<ReporteVentaMuerteInfo> resultadoInfo =
                        reporteVentaMuerteBL.GenerarReporteVentaMuerte(organizacionId, DtpFechaInicial.SelectedDate.Value,
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

                        var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);

                        string formatoFecha =
                            string.Format(Properties.Resources.ReporteVentaMuerte_RangoFecha,
                            DtpFechaInicial.SelectedDate.Value.ToShortDateString(),
                            DtpFechaFinal.SelectedDate.Value.ToShortDateString());

                        foreach (var dato in resultadoInfo)
                        {
                            dato.Titulo = Properties.Resources.ReporteVentaMuerte_TituloReporte;
                            dato.RangoFechas = formatoFecha;
                            dato.Organizacion = string.Format(Properties.Resources.Reporte_NombreEmpresa, organizacion.Division);
                        }
                       

                        var documento = new ReportDocument();
                        var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteVentaMuerte.rpt");
                        documento.Load(reporte);


                        documento.DataSourceConnections.Clear();
                        documento.SetDataSource(resultadoInfo);
                        documento.Refresh();


                        var forma = new ReportViewer(documento, Properties.Resources.ReporteVentaMuerte_TituloReporte);
                        forma.MostrarReporte();
                        forma.Show();
                    }
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteEjecutivo_MsgErrorExportarExcel,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion METODOS
    }
}
