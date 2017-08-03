using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Filtros;
using CrystalDecisions.CrystalReports.Engine;
using SIE.WinForm.Controles;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para RecepcionReporteInventario.xaml
    /// </summary>
    public partial class RecepcionReporteDiaDiaCr
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
                return (FiltroFechasInfo) DataContext;
            }
            set { DataContext = value; }
        }

        #endregion Propiedades

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public RecepcionReporteDiaDiaCr()
        {
            InitializeComponent();
            InicializaContexto();
            CargaOrganizaciones();
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Generar();
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
            bool isValid2 = ValidaFechaPeriodo90dias();

            Contexto.Valido = isValid;

            if (isValid && isValid2)
            {
                Contexto.Valido = true;
                return;
            }
            var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
            DtpFechaFinal.MoveFocus(tRequest);

            if (!isValid)
            {
                MostrarMensajeFechaInicialMayorFechaActual();
            }
            if (!isValid2)
            {
                string mensaje = Properties.Resources.ReporteDiaadia_ValidacionFechaFueraRango;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                  MessageBoxButton.OK, MessageImage.Warning);

            }

            
            Contexto.FechaInicial = null;
            Contexto.Valido = false;
            e.Handled = true;
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
            bool isValid3 = ValidaFechaPeriodo90dias();
             
            if (isValid && isValid2 && isValid3)
            {
                Contexto.Valido = true;
                return;

            }


            DtpFechaInicial.Focus();
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            DtpFechaFinal.MoveFocus(tRequest);
           
            if (!isValid)
            {
                MostrarMensajeFechaFinalMenorFechaInicial();

            }
            if  (!isValid2)
            {
                MostrarMensajeFechaFinalMayorFechaActual();

            }
            if (!isValid3)
            {
                string mensaje = Properties.Resources.ReporteDiaadia_ValidacionFechaFueraRango;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                  MessageBoxButton.OK, MessageImage.Warning);

            }
            
            Contexto.FechaFinal = null;
            Contexto.Valido = false;
            e.Handled = true;
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
            DtpFechaInicial.IsEnabled = true;
            DtpFechaFinal.IsEnabled = true;
            CargaOrganizaciones();
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

                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                if (organizacion == null || organizacion.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.RecepcionReporteDiaDia_MsgSelecioneOrganizacion;
                    cmbOrganizacion.Focus();
                }

                if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteDiaDia_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteDiaDia_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.RecepcionReporteDiaDia_MsgFechaInicialMayorFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteDiaDia_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.RecepcionReporteDiaDia_MsgFechaFinalMenorFechaInicial;
                    DtpFechaFinal.Focus();
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
        public void Generar()
        {
            if (ValidaGenerar())
            {
                Consultar_CR();
            }
        }
        
        /// <summary>
        /// Método obtener el reporte Dia a Dia
        /// </summary>
        /// <returns></returns>
        private List<ReporteDiaDiaInfo> ObtenerReporteDiaDia()
        {
            try
            {
                var reporteDiaDiaPL = new ReporteDiaDiaPL();
                
                DateTime fechaIni = DtpFechaInicial.SelectedDate.HasValue
                                        ? DtpFechaInicial.SelectedDate.Value
                                        : new DateTime();
                DateTime fechaFin = DtpFechaFinal.SelectedDate.HasValue
                                        ? DtpFechaFinal.SelectedDate.Value
                                        : fechaIni;

                var organizacionSeleccionada = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                int organizacionId = organizacionSeleccionada.OrganizacionID;

                List<ReporteDiaDiaInfo> resultadoInfo = reporteDiaDiaPL.GenerarReporteDiaDia(organizacionId,fechaIni, fechaFin);
                
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
        /// Método para validar que la fecha final no sea mayor a la actual.
        /// </summary>
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
        /// Método para validar Periodo mayor a 90 dias
        /// </summary>
        private bool ValidaFechaPeriodo90dias()
        {
            bool result = true;
            if (DtpFechaFinal.SelectedDate.HasValue && DtpFechaInicial.SelectedDate.HasValue)
            {
                DateTime? fecha = DtpFechaFinal.SelectedDate.HasValue
                    ? DtpFechaFinal.SelectedDate
                    : null;

                DateTime fechaMax = DtpFechaInicial.SelectedDate.Value.AddDays(90);



                if (fecha != null && fecha > fechaMax)
                {
                    result = false;
                }
            }

            return result;
        }
        
        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.RecepcionReporteDiaDia_MsgFechaInicialMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
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


        private void Consultar_CR()
        {


            try
            {
                var organizacionSeleccionada = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
                int organizacionId = organizacionSeleccionada.OrganizacionID;
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion + "(" + organizacion.Division + ")" : String.Empty;
                
                var encabezado = new ReporteDiaDiaInfo()
                {
                    Titulo = Properties.Resources.RecepcionReporteDiaDia_Titulo,
                    FechaInicio = DtpFechaInicial.SelectedDate.Value,
                    FechaFin = DtpFechaFinal.SelectedDate.Value,
                    Organizacion = nombreOrganizacion
                };


                List<ReporteDiaDiaInfo> resultadoInfo = ObtenerReporteDiaDia();
                
                if (resultadoInfo != null && resultadoInfo.Count > 0)
                {
                    resultadoInfo = resultadoInfo.OrderBy(registro => registro.FechaLlegada).ToList();
                    foreach (var dato in resultadoInfo)
                    {
                        dato.Titulo = encabezado.Titulo;
                        dato.FechaInicio = encabezado.FechaInicio;
                        dato.FechaFin = encabezado.FechaFin;
                        dato.Organizacion = Properties.Resources.RecepcionReporteDiaaDia_MsgTituloDivision + " (" + organizacion.Division + ")";
                    }

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteDiaaDia.rpt");
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

                DtpFechaInicial.IsEnabled = false;
                DtpFechaFinal.IsEnabled = false;


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteMedicamentosAplicados_FalloCargarReporte, MessageBoxButton.OK,
                                                      MessageImage.Error);
            }
        }
        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMenorFechaInicial()
        {
            string mensaje = Properties.Resources.RecepcionReporteDiaDia_MsgFechaFinalMenorFechaInicial;
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
