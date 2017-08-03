using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Linq;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteMedicamentosAplicados.xaml
    /// </summary>
    public partial class ReporteMedicamentosAplicados
    {
        #region Propiedades

        /// <summary>
        /// Propiedad que contiene los tratamientos a mostrar en la pantalla
        /// </summary>
        private readonly int[] tratamientosValidos = new[] { TipoMovimiento.Corte.GetHashCode(), TipoMovimiento.CortePorTransferencia.GetHashCode(), TipoMovimiento.Reimplante.GetHashCode() };

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

        public ReporteMedicamentosAplicados()
        {
            InitializeComponent();
        }

        #region Metodos

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
                var organizacionSeleccionada = (OrganizacionInfo) cmbOrganizacion.SelectedItem;

                int organizacionId = organizacionSeleccionada.OrganizacionID;

                var tipoMovimiento = (TipoMovimientoInfo)cmbTipoMovimiento.SelectedItem;
                int tipoTratamiento = 0;
                if (tipoMovimiento != null)
                {
                    tipoTratamiento = tipoMovimiento.TipoMovimientoID;
                }


                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Division : String.Empty;
                var encabezado = new ReporteEncabezadoInfo()
                {
                    Titulo = Properties.Resources.ReporteMedicamentosAplicados_TituloReporte,
                    FechaInicio = fechaIni,
                    FechaFin = fechaFin,
                    Organizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + nombreOrganizacion + ")"
                };

                List<ReporteMedicamentosAplicadosModel> resultadoInfo = ObtenerReporteMedicamentosAplicados(encabezado, tipoTratamiento, organizacionId, fechaIni, fechaFin);

                if (resultadoInfo == null || !resultadoInfo.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteOperacionSanidad_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCampos();
                    return;
                }

                DtpFechaInicial.IsEnabled = false;
                DtpFechaFinal.IsEnabled = false;
                cmbTipoMovimiento.IsEnabled = false;

                foreach (var dato in resultadoInfo)
                {
                    dato.Organizacion = encabezado.Organizacion;
                    dato.Titulo = encabezado.Titulo;
                    dato.FechaFin = encabezado.FechaFin;
                    dato.FechaInicio = encabezado.FechaInicio;
                    dato.Id = tipoTratamiento;
                }

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteMedicamentosAplicados.rpt");
                documento.Load(reporte);


                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();


                var forma = new ReportViewer(documento, encabezado.Titulo);
                forma.MostrarReporte();
                forma.Show();
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
        /// Método para cargar los Tipos de Tratamiento
        /// </summary>
        /// <returns></returns>
        private void CargarTiposTratamiento()
        {
            var tipoMovimientoPL = new TipoMovimientoPL();
            IList<TipoMovimientoInfo> listaMovimientos = tipoMovimientoPL.ObtenerTodos(EstatusEnum.Activo);
            List<TipoMovimientoInfo> listaMovimientosValidos =
                listaMovimientos.Where(mov => tratamientosValidos.Contains(mov.TipoMovimientoID)).ToList();

            var tipoMovimientoTotalGeneral = new TipoMovimientoInfo
                {
                    TipoMovimientoID = 0,
                    Descripcion = Properties.Resources.ReporteMedicamentosAplicados_TotalGeneral
                };
            listaMovimientosValidos.Insert(0, tipoMovimientoTotalGeneral);
            cmbTipoMovimiento.ItemsSource = listaMovimientosValidos;
            if (cmbTipoMovimiento.SelectedItem == null)
            {
                cmbTipoMovimiento.SelectedIndex = 0;
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
                    mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgSelecioneOrganizacion;
                    cmbOrganizacion.Focus();
                }

                if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgFechaInicialMayorFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgFechaFinalMenorFechaInicial;
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
        /// Metodo para obtener los datos del informe
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="tipoTratamiento"></param>
        /// <param name="organizacionId"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        private List<ReporteMedicamentosAplicadosModel> ObtenerReporteMedicamentosAplicados(ReporteEncabezadoInfo encabezado, int tipoTratamiento, int organizacionId, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                var reporteMedicamentosAplicadosPL = new ReporteMedicamentosAplicadosPL();
                List<ReporteMedicamentosAplicadosModel> resultadoInfo = reporteMedicamentosAplicadosPL.ObtenerReporteMedicamentoCabezasAplicados(encabezado, organizacionId, fechaIni, fechaFin, tipoTratamiento);
                return resultadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
            string mensaje = Properties.Resources.RecepcionMedicamentosAplicados_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgFechaInicialMayorFechaActual;
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
            if (fecha != null
                && (fecha > DateTime.Today || fecha < Contexto.FechaInicial))
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

            if (fecha != null
                && (fecha > DateTime.Today || fecha > fechaFinal))
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
            string mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgFechaFinalMenorFechaInicial;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorActual()
        {
            string mensaje = Properties.Resources.ReporteMedicamentosAplicados_MsgFechaFinalMayorFechaActual;
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
            cmbTipoMovimiento.SelectedIndex = 0;
            cmbTipoMovimiento.IsEnabled = true;
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
            else if (DtpFechaInicial.SelectedDate > DateTime.Now)
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
            else if (DtpFechaFinal.SelectedDate > DateTime.Today)
            {
                MostrarMensajeFechaFinalMayorActual();
            }
            Contexto.FechaFinal = null;
            Contexto.FechaInicial = null;
            Contexto.Valido = false;
            e.Handled = true;
            DtpFechaInicial.Focus();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void ControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            CargarTiposTratamiento();
            CargaOrganizaciones();
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Generar();
        }

        #endregion Eventos
    }
}
