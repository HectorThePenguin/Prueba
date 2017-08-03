using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteMuertesGanado.xaml
    /// </summary>
    public partial class ReporteMuertesGanado 
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
        public ReporteMuertesGanado()
        {
            InitializeComponent();
            InicializaContexto();
            CargaOrganizaciones();
        }
        #endregion Constructor

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
            Contexto.Valido = ValidaFechaInicial();
        }

        /// <summary>
        /// LostFocus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaFinal_LostFocus(object sender, RoutedEventArgs e)
        {
            Contexto.Valido = ValidaFechaFinal();
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
                FechaInicial = DateTime.Now,
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
            InicializaContexto();
            Contexto.FechaInicial = null;
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Generar()
        {
            if (ValidaGenerar())
            {
                MostrarReporte();
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

                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                if (organizacion == null || organizacion.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.ReporteMuertesGanado_MsgSelecioneOrganizacion;
                    cmbOrganizacion.Focus();
                }

                if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteMuertesGanado_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.ReporteMuertesGanado_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteMuertesGanado_MsgFechaInicialMayorFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteMuertesGanado_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }

                else if (fechaFin > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteMuertesGanado_MsgFechaFinMayorActual;
                    DtpFechaInicial.Focus();
                }

                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.ReporteMuertesGanado_MsgFechaFinalMenorFechaInicial;
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
        /// Método para mostrar el informe
        /// </summary>
        /// <returns></returns>
        private void MostrarReporte()
        {
            try
            {
                DateTime fechaIni = DtpFechaInicial.SelectedDate.HasValue
                                       ? DtpFechaInicial.SelectedDate.Value
                                       : new DateTime();
                DateTime fechaFin = DtpFechaFinal.SelectedDate.HasValue
                                        ? DtpFechaFinal.SelectedDate.Value
                                        : fechaIni;

                var organizacionInfo = (OrganizacionInfo) cmbOrganizacion.SelectedItem;

                int organizacionId = organizacionInfo.OrganizacionID;

                IList<ReporteMuertesGanadoInfo> resultadoInfo = ObtenerReporteMuertesGanado(organizacionId, fechaIni, fechaFin);

                if (resultadoInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteMuertesGanado_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }

                var organizacionPl = new OrganizacionPL();
                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Division : String.Empty;
                var encabezado = new ReporteEncabezadoInfo
                {
                    Titulo = Properties.Resources.ReporteMuertesGanado_TituloReporte,
                    FechaInicio = fechaIni,
                    FechaFin = fechaFin,
                    Organizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + nombreOrganizacion + ")"
                };

                foreach (var dato in resultadoInfo)
                {
                    dato.Titulo = encabezado.Titulo;
                    dato.FechaInicio = encabezado.FechaInicio;
                    dato.FechaFin = encabezado.FechaFin;
                    dato.Organizacion = encabezado.Organizacion;
                }

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteMuertesDeGanado.rpt");
                documento.Load(reporte);

                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();

                var forma = new ReportViewer(documento, encabezado.Titulo);
                forma.MostrarReporte();
                forma.Show();
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteMuertesGanado_FalloCargarReporte,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteMuertesGanado_FalloCargarReporte,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Método obtener el reporte muertes de ganado.
        /// </summary>
        /// <returns></returns>
        private IList<ReporteMuertesGanadoInfo> ObtenerReporteMuertesGanado(int organizacionId, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                var reporte = new ReporteMuertesGanadoBL();
                var filtro = new FiltroFechasInfo
                                 {
                                     OrgazizacionId = organizacionId,
                                     FechaInicial = fechaIni,
                                     FechaFinal = fechaFin
                                 };
                IList<ReporteMuertesGanadoInfo> resultadoInfo = reporte.Generar(filtro);
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
            if (!DtpFechaInicial.SelectedDate.HasValue)
                return false;
            var fecha = DtpFechaInicial.SelectedDate.Value.SoloFecha();

            if (fecha > DateTime.Today)
            {
                MostrarMensaje(Properties.Resources.ReporteMuertesGanado_MsgFechaInicialMayorFechaActual);
                Contexto.FechaInicial = null;
                return false;
            }
            if (!DtpFechaFinal.SelectedDate.HasValue)
                return false;
            var fechaFinal = DtpFechaFinal.SelectedDate.Value.SoloFecha();
            if (fecha > fechaFinal)
            {
                MostrarMensaje(Properties.Resources.ReporteMuertesGanado_MsgFechaInicialMayorFechaActual);
                Contexto.FechaInicial = null;
                return false;
            }

            if (!validarPeriodoAño())
            {
                MostrarMensaje(Properties.Resources.ReporteMuertesGanado_MsgRangoFechaNoValido);
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Método para validar que la fecha inicial no sea mayor a la actual.
        /// </summary>
        private bool ValidaFechaFinal()
        {
            if (!DtpFechaFinal.SelectedDate.HasValue)
                return false;
            var fechaFinal = DtpFechaFinal.SelectedDate.Value.SoloFecha();

            if (fechaFinal > DateTime.Now)
            {
                MostrarMensaje(Properties.Resources.ReporteMuertesGanado_MsgFechaFinMayorActual);
                Contexto.FechaFinal = null;
                return false;
            }

            if (!DtpFechaInicial.SelectedDate.HasValue)
                return false;
            var fecha = DtpFechaInicial.SelectedDate.Value.SoloFecha();

            if (fechaFinal < fecha)
            {
                MostrarMensaje(Properties.Resources.ReporteMuertesGanado_MsgFechaFinalMenorFechaInicial);
                Contexto.FechaFinal = null;
                return false;
            }

            if (!validarPeriodoAño())
            {
                MostrarMensaje(Properties.Resources.ReporteMuertesGanado_MsgRangoFechaNoValido);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validar que no exista mas de un año de rango entre la fecha inicial y la fecha final
        /// </summary>
        /// <returns></returns>
        private bool validarPeriodoAño()
        {
            var fechaInicial = DtpFechaInicial.SelectedDate.Value.AddYears(1);
            var fechaFinal = DtpFechaFinal.SelectedDate.Value;
            return fechaInicial >= fechaFinal;
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensaje(string mensaje)
        {
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
