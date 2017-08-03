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
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Linq;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteIndicadoresOperacionSanidad.xaml
    /// </summary>
    public partial class ReporteIndicadoresOperacionSanidad
    {
        #region Propiedades
        /// <summary>
        /// Propiedad que contiene el DataContext de la pantalla
        /// </summary>
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

        #region Constructores
        public ReporteIndicadoresOperacionSanidad()
        {
            InitializeComponent();
            LimpiarCampos();
            CargaOrganizaciones();
        }
        #endregion Constructores

        #region Metodos

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorActual()
        {
            string mensaje = Properties.Resources.ReporteIndicadoresOperacionSanidad_MsgFechaFinalMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
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

                var organizacion = (OrganizacionInfo) cmbOrganizacion.SelectedItem;

                if(organizacion == null || organizacion.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.ReporteOperacionSanidad_MsgSelecioneOrganizacion;
                    cmbOrganizacion.Focus();
                }

                if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteOperacionSanidad_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.ReporteOperacionSanidad_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteOperacionSanidad_MsgFechaInicialMayorFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteOperacionSanidad_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.ReporteOperacionSanidad_MsgFechaFinalMenorFechaInicial;
                    DtpFechaFinal.Focus();
                }
                else if (fechaIni < fechaFin.Value.AddYears(-1))
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgFechaInicialMenorAnio;
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
        public void Generar()
        {
            if (ValidaGenerar())
            {
                Consultar();
            }
        }

        /// <summary>
        /// Método obtener el reporte Operacion de Sanidad
        /// </summary>
        /// <returns></returns>
        private List<ReporteOperacionSanidadInfo> ObtenerReporteOperacionSanidad(int organizacionId, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                var reporteOperacionSanidadPl = new ReporteOperacionSanidadPL();

                List<ReporteOperacionSanidadInfo> resultadoInfo = reporteOperacionSanidadPl.GenerarReporteOperacionSanidad(organizacionId, fechaIni, fechaFin);

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
        private void Consultar()
        {
            try
            {

                DateTime fechaIni = DtpFechaInicial.SelectedDate.HasValue
                                       ? DtpFechaInicial.SelectedDate.Value
                                       : new DateTime();
                DateTime fechaFin = DtpFechaFinal.SelectedDate.HasValue
                                        ? DtpFechaFinal.SelectedDate.Value
                                        : fechaIni;

                var organizacionInfo = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                int organizacionId = organizacionInfo.OrganizacionID;

                List<ReporteOperacionSanidadInfo> resultadoInfo = ObtenerReporteOperacionSanidad(organizacionId, fechaIni, fechaFin);

                if (resultadoInfo == null || !resultadoInfo.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteOperacionSanidad_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCampos();
                    return;
                }

                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);

               
                DtpFechaInicial.IsEnabled = false;
                DtpFechaFinal.IsEnabled = false;
               
                var nombreOrganizacion = organizacion != null ? organizacion.Division : String.Empty;
                var encabezado = new ReporteEncabezadoInfo
                {
                    Titulo = Properties.Resources.ReporteOperadoresSanidad_TituloReporte,
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
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteOperacionSanidad.rpt");
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
                                  Properties.Resources.ReporteIndicadoresSanidad_FalloCargarReporte,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteIndicadoresSanidad_FalloCargarReporte,
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
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaFinal()
        {
            string mensaje = Properties.Resources.ReporteOperacionSanidad_MsgFechaInicialMayorFechaFinal1;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteOperacionSanidad_MsgFechaInicialMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
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
            if (fecha != null && (fecha > DateTime.Today || fecha < Contexto.FechaInicial))
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
        private void MostrarMensajeFechaFinalMenorFechaInicial()
        {
            string mensaje = Properties.Resources.ReporteOperacionSanidad_MsgFechaFinalMenorFechaInicial;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
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
            DtpFechaInicial.Focus();
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

        #endregion Metodos

        #region Eventos

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
            DateTime fechaInicio;
            DateTime fechaFin;

            DateTime.TryParse(DtpFechaInicial.Text, out fechaInicio);
            DateTime.TryParse(DtpFechaFinal.Text, out fechaFin);

            if (fechaInicio == DateTime.MinValue || fechaFin == DateTime.MinValue)
            {
                Contexto.Valido = false;
            }
        }

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

        private void DtpFechaFinal_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaFinal();

            Contexto.Valido = isValid;

            if (isValid)
            {
                return;
            }
            //DtpFechaInicial.Focus();
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            DtpFechaFinal.MoveFocus(tRequest);
            if (DtpFechaFinal.SelectedDate < DtpFechaInicial.SelectedDate)
            {
                MostrarMensajeFechaFinalMenorFechaInicial();
            }
            else
            {
                MostrarMensajeFechaFinalMayorActual();
            }
            Contexto.FechaFinal = null;
            Contexto.FechaInicial = null;
            Contexto.Valido = false;
            e.Handled = true;
            DtpFechaInicial.Focus();
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Generar();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        #endregion Eventos

    }
}
