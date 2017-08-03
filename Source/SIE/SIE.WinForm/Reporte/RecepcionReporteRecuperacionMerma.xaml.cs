using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.BL;
using System.Windows.Controls;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Services.Info.Info;
using SIE.WinForm.Controles;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para RecepcionReporteRecuperacionMerma.xaml
    /// </summary>
    public partial class RecepcionReporteRecuperacionMerma
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

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;
        #endregion Propiedades

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public RecepcionReporteRecuperacionMerma()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos(false);
            CargaOrganizaciones();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
     

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Evento para un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos(false);
        }
        
        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// LostFocus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaInicial_LostFocus(object sender, RoutedEventArgs e)
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

        private bool ValidaFechaFinalMayorActual()
        {
            bool result = true;
            DateTime? fecha = DtpFechaFinal.SelectedDate.HasValue
                                  ? DtpFechaFinal.SelectedDate
                                  : null;


            if (fecha != null && fecha > DateTime.Today)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// LostFocus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaFinal_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaFinal();
            bool isValid2 = ValidaFechaFinalMayorActual();
            bool isValid3 = ValidarPeriodoAnio();
            Contexto.Valido = isValid;

            if (isValid && isValid2 && isValid3)
            {
                Contexto.Valido = true;
                return;

            }

            DtpFechaInicial.Focus();
            Contexto.FechaInicial = null;
            Contexto.FechaFinal = null;
            Contexto.Valido = false;

            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            DtpFechaFinal.MoveFocus(tRequest);
            if (!isValid)
            {
                MostrarMensajeFechaFinalMayorFechaInicial();
                return;
            }
            if (!isValid2)
            {
                MostrarMensajeFechaFinalMayorFechaActual();
                return;
            }

            if (!isValid3)
            {
                MostrarMensajeFechaMayoraunAnio();
                return;
            }
        }

        #endregion

        #region Métodos
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
        private void LimpiarCampos(bool cancelar)
        {
            InicializaContexto();

            DtpFechaInicial.SelectedDate = null;
            DtpFechaFinal.SelectedDate = null;
            DtpFechaInicial.IsEnabled = true;
            DtpFechaFinal.IsEnabled = true;

            DtpFechaInicial.Focus();
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

                if(organizacion == null || organizacion.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgSelecioneOrganizacion;
                    cmbOrganizacion.Focus();
                }

                if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaInicialMayorFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaFinalMenorFechaInicial;
                    DtpFechaFinal.Focus();
                }else if (fechaIni > fechaFin)
                {
                    mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaInicialMayorFechaFinal;
                    DtpFechaInicial.Focus();
                }
                else if (!ValidarPeriodoAnio())
                {
                    mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaFinalMayorAnio;
                    DtpFechaInicial.Focus();
                }
                if (!string.IsNullOrWhiteSpace(mensaje))
                {
                    resultado = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje, MessageBoxButton.OK, MessageImage.Warning);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Validar periodo de un año
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
        /// Buscar
        /// </summary>
        private void Buscar()
        {
            if (ValidaBuscar())
            {
                GenerarReporte();
            }
        }

       
        /// <summary>
        /// Método obtener el reporte Recuperacion de Merma
        /// </summary>
        /// <returns></returns>
        private List<ReporteRecuperacionMermaInfo> ObtenerReporte()
        {
            try
            {
                var reporteRecuperacionMermaPL = new ReporteRecuperacionMermaPL();

                var organizacion = (OrganizacionInfo) cmbOrganizacion.SelectedItem;

                int organizacionID = organizacion.OrganizacionID;

                DateTime fechaIni = DtpFechaInicial.SelectedDate.HasValue
                                        ? DtpFechaInicial.SelectedDate.Value
                                        : new DateTime();
                DateTime fechaFin = DtpFechaFinal.SelectedDate.HasValue
                                        ? DtpFechaFinal.SelectedDate.Value
                                        : new DateTime();

                List<ReporteRecuperacionMermaInfo> resultadoInfo =
                    reporteRecuperacionMermaPL.GenerarReporteRecuperacionMerma(organizacionID, fechaIni, fechaFin);
                return resultadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método para exportar a excel
        /// </summary>
        /// <returns></returns>
        private void GenerarReporte()
        {
            try
            {
                List<ReporteRecuperacionMermaInfo> resultadoInfo = ObtenerReporte();

                if (resultadoInfo == null || resultadoInfo.Count == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ReporteRecuperacionMerma_MsgSinInformacion,
                        MessageBoxButton.OK, MessageImage.Warning);

                    return;
                }
                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                int organizacionId = organizacion.OrganizacionID;

                
                string division = AuxDivision.ObtenerDivision(organizacionId);

                string titulo = string.Format(Properties.Resources.ReporteVentaMuerte_RangoFecha, 
                    DtpFechaInicial.SelectedDate.Value.ToShortDateString(), 
                    DtpFechaFinal.SelectedDate.Value.ToShortDateString());

                foreach (var dato in resultadoInfo)
                {
                    dato.Titulo = Properties.Resources.ReporteRecuperacionMerma_NombreReporte;
                    dato.RangoFechas = titulo;
                    dato.Organizacon = string.Format(Properties.Resources.Reporte_NombreEmpresa, division);
                }


                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteRecuperacionMerma.rpt");
                documento.Load(reporte);


                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();


                var forma = new ReportViewer(documento, Properties.Resources.ReporteRecuperacionMerma_NombreReporte);
                forma.MostrarReporte();
                forma.Show();
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
                                  Properties.Resources.RecepcionReporteRecuperacionMerma_MsgErrorExportarExcel, MessageBoxButton.OK, MessageImage.Error);

            }
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
        /// Método para validar que la fecha inicial no sea mayor a la actual.
        /// </summary>
        private bool ValidaFechaFinal()
        {
            bool result = true;
            DateTime? fecha = DtpFechaFinal.SelectedDate.HasValue
                                  ? DtpFechaFinal.SelectedDate
                                  : null;
            if (fecha != null && fecha < Contexto.FechaInicial)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaInicialMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaFinal()
        {
            string mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorFechaInicial()
        {
            string mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaFinalMenorFechaInicial;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorFechaActual()
        {
            string mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaFinalMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaMayoraunAnio()
        {
            string mensaje = Properties.Resources.RecepcionReporteRecuperacionMerma_MsgFechaFinalMayorAnio;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para cargar las organizaciones
        /// </summary>
        /// <returns></returns>
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
                    //btnGenerar.IsEnabled = true;
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

        #endregion
    }
}
