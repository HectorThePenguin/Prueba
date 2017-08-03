using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Controles;
namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para RecepcionReporteEjecutivo.xaml
    /// </summary>
    public partial class RecepcionReporteEjecutivo
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

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public RecepcionReporteEjecutivo()
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
            LimpiarCampos();
            CargaOrganizaciones();
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
            LimpiarCampos();
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
            bool isValid2 = validarPeriodoAnio();

            

            if (isValid && isValid2)
            {
                Contexto.Valido = true;
                return;
            }
            var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
            DtpFechaFinal.MoveFocus(tRequest);
            Contexto.FechaInicial = null;
            Contexto.Valido = false;
            e.Handled = true;

            if (!isValid)
            {
                MostrarMensajeFechaInicialMayorFechaActual();
                return;
            }
            if (!isValid2)
            {
                MostrarMensajeFechaMayoraunAño();
                return;
            }

            
           
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
            bool isValid3 = validarPeriodoAnio();
           
            if (isValid && isValid2 && isValid3)
            {
                Contexto.Valido = true;
                return;

            }
            DtpFechaInicial.Focus();
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            DtpFechaFinal.MoveFocus(tRequest);
            Contexto.FechaFinal = null;
            Contexto.Valido = false;
            e.Handled = true;
            
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
                MostrarMensajeFechaMayoraunAño();
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
        private void LimpiarCampos()
        {

            InicializaContexto();

          //  gridDatos.ItemsSource = new List<ReporteEjecutivoInfo>();
            DtpFechaInicial.SelectedDate = null;
            DtpFechaFinal.SelectedDate = null;
            DtpFechaInicial.IsEnabled = true;
            DtpFechaFinal.IsEnabled = true;
            CargaOrganizaciones();

            DtpFechaInicial.Focus();
        }
        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorFechaActual()
        {
            string mensaje = Properties.Resources.RecepcionReporteDiaDia_MsgFechaFinalMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }
        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaMayoraunAño()
        {
            string mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgFechaFinalMayordeunaño;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
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

                if(organizacion == null || organizacion.OrganizacionID ==0)
                {
                    mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgSelecioneOrganizacion;
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
                    mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgFechaNoValida;
                    DtpFechaFinal.Focus();
                }
                else if (!validarPeriodoAnio())
                {
                    mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgFechaFinalMayordeunaño;
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
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerReporte()
        {
            try
            {
                DateTime fechaIni = DtpFechaInicial.SelectedDate.HasValue
                    ? DtpFechaInicial.SelectedDate.Value
                    : new DateTime();
                DateTime fechaFin = DtpFechaFinal.SelectedDate.HasValue
                    ? DtpFechaFinal.SelectedDate.Value
                    : new DateTime();

                var organizacionSeleccionada = (OrganizacionInfo) cmbOrganizacion.SelectedItem;

                int organizacionId = organizacionSeleccionada.OrganizacionID;
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);

                List<ReporteEjecutivoResultadoInfo> resultadoInfo = ObtenerReportejecutivo(organizacionId, fechaIni,
                    fechaFin);

                if (resultadoInfo != null && resultadoInfo.Count > 0)
                {
                    DtpFechaInicial.IsEnabled = false;
                    DtpFechaFinal.IsEnabled = false;

                    //Cargar reporte de CR



                    var nombreOrganizacion = organizacion != null ? organizacion.Division : String.Empty;

                    var encabezado = new ReporteEjecutivoInfo()
                    {
                        Titulo = Properties.Resources.ReporteEjecutivoCr_Titulo,
                        FechaInicio = fechaIni,
                        FechaFin = fechaFin,
                        Organizacion =
                            Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + nombreOrganizacion + ")"
                    };

                    foreach (var dato in resultadoInfo)
                    {
                        dato.Titulo = encabezado.Titulo;
                        dato.FechaInicio = encabezado.FechaInicio;
                        dato.FechaFin = encabezado.FechaFin;
                        dato.Organizacion = encabezado.Organizacion;
                    }

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
                        "\\Reporte\\RptReporteEjecutivoCR.rpt");
                    documento.Load(reporte);


                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(resultadoInfo);
                    documento.Refresh();


                    var forma = new ReportViewer(documento, encabezado.Titulo);
                    forma.MostrarReporte();
                    forma.Show();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RecepcionReporteEjecutivo_MsgSinInformacion,
                        MessageBoxButton.OK, MessageImage.Warning);
                }

            }
            catch (CrystalReportsException exCr)
            {
                Logger.Error(exCr);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteGenerico_ErrorCargarCrystal, MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteEjecutivo_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteEjecutivo_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }
     
        /// <summary>
        /// Método obtener el reporte ejecutivo
        /// </summary>
        /// <returns></returns>
        private List<ReporteEjecutivoResultadoInfo> ObtenerReportejecutivo(int organizacionId, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                var reporteEjecutivoPL = new ReporteEjecutivoPL();

                
                List<ReporteEjecutivoResultadoInfo> resultadoInfo = reporteEjecutivoPL.GenerarReporteEjecutivo(organizacionId,
                    fechaIni, fechaFin);
                return resultadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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

            if (fecha != null && fecha > DateTime.Today)
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
            string mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
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
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorFechaInicial()
        {
            string mensaje = Properties.Resources.RecepcionReporteEjecutivo_MsgFechaNoValida;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }


        /// <summary>
        /// Validar que no exista mas de un año de rango entre la fecha inicial y la fecha final
        /// </summary>
        /// <returns></returns>
        private bool validarPeriodoAnio()
        {
            bool result = true;
          
            DateTime? fecha = DtpFechaFinal.SelectedDate.HasValue
                                  ? DtpFechaFinal.SelectedDate
                                  : null;
            var fechaInicial = DtpFechaInicial.SelectedDate.HasValue
                                  ? DtpFechaInicial.SelectedDate
                                  : null; 
            
            

            if (fecha != null && fechaInicial != null )
            {
                fechaInicial = DtpFechaInicial.SelectedDate.Value.AddYears(1);
                var fechaFinal = DtpFechaFinal.SelectedDate.Value;
                if (fechaInicial <= fechaFinal )
                {
                    result = false;
                }
            }
            return result;                      
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