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
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Filtros;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para RecepcionReporteInventario.xaml
    /// </summary>
    public partial class RecepcionReporteInventario
    {
        #region Propiedades

        private FiltroReporteInventario Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroReporteInventario) DataContext;
            }
            set { DataContext = value; }
        }

        private DateTime fechaMinima;
        private int invervalo;

        #endregion Propiedades

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public RecepcionReporteInventario()
        {
            InitializeComponent();
            ObtenerFechaMinima();
            InicializaContexto();
            CargaComboTipoProceso();
            LimpiarCampos();
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

            if (isValid)
            {
                return;
            }
            var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
            DtpFechaFinal.MoveFocus(tRequest);
            MostrarMensajeFechaActual();
            Contexto.FechaInicial = null; //ObtenerFechaInicial();
            e.Handled = true;
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
        /// LostFocus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaFinal_OnLostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaFinal();

            Contexto.Valido = isValid;

            if (isValid)
            {
                return;
            }

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

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMenorFechaInicial()
        {
            string mensaje = Properties.Resources.RecepcionReporteInventario_MsgFechaNoValida;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorActual()
        {
            string mensaje = Properties.Resources.RecepcionReporteInventario_MsgFechaMayorActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Selecciona un Tipo de Proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TipoProceso_SelectionChaged(object sender, SelectionChangedEventArgs e)
        {
            if (Contexto.TipoProceso != null && Contexto.TipoProceso.TipoProcesoID >= 0)
            {
                Contexto.Valido = true;
            }
            else
            {
                Contexto.Valido = false;
            }
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroReporteInventario
                           {
                               TipoProceso = new TipoProcesoInfo
                                                 {
                                                     TipoProcesoID = 0,
                                                     Descripcion = Properties.Resources.cbo_Seleccione,
                                                 },
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
            cmbTipoProceso.IsEnabled = true;
            DtpFechaInicial.IsEnabled = true;
            DtpFechaFinal.IsEnabled = true;
            CargaOrganizaciones();
            cmbTipoProceso.Focus();
        }

        /// <summary>
        /// Método para validar los controles de la pantalla.
        /// </summary>
        /// <returns></returns>
        private bool ValidaBuscar(bool mostrar)
        {
            try
            {
                bool resultado = true;
                string mensaje = string.Empty;

                int tipoProcesoId = Contexto.TipoProceso.TipoProcesoID;
                DateTime? fechaIni = Contexto.FechaInicial;
                DateTime? fechaFin = Contexto.FechaFinal;

                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                if (organizacion == null || organizacion.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.RecepcionReporteInventario_MsgSelecioneOrganizacion;
                    cmbOrganizacion.Focus();
                }

                if (tipoProcesoId < 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.RecepcionReporteInventario_MsgTipoProcesoRequerido;
                    cmbTipoProceso.Focus();
                }
                else if (fechaIni == null && fechaFin != null)
                {
                    mensaje = Properties.Resources.RecepcionReporteInventario_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni != null && fechaFin == null)
                {
                    mensaje = Properties.Resources.RecepcionReporteInventario_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else
                    if (fechaIni != null)
                    {
                        fechaFin = Contexto.FechaFinal ?? fechaIni;

                        if (fechaIni > DateTime.Today)
                        {
                            mensaje = Properties.Resources.RecepcionReporteInventario_MsgFechaActual;
                            DtpFechaInicial.Focus();
                        }
                        else if (fechaIni < fechaMinima)
                        {
                            mensaje = string.Format(Properties.Resources.ReporteInventario_FechaMenorTresMeses, invervalo);
                            DtpFechaInicial.Focus();
                        }
                        else if (fechaFin < fechaIni)
                        {
                            mensaje = Properties.Resources.RecepcionReporteInventario_MsgFechaNoValida;
                            DtpFechaFinal.Focus();
                        }
                    }
                if (fechaIni != null && fechaFin != null)
                {
                    fechaFin = Contexto.FechaFinal ?? fechaIni;

                    TimeSpan diasIntervalo = fechaFin.Value - fechaIni.Value;
                    if (diasIntervalo.Days > invervalo)
                    {
                        mensaje = string.Format(Properties.Resources.ReporteInventario_FechaFueraIntervalo, invervalo);
                        Contexto.FechaFinal = null;
                        Contexto.FechaInicial = null;
                    }
                }
                if (mostrar && !string.IsNullOrWhiteSpace(mensaje))
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
            if (ValidaBuscar(true))
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
                int tipoProcesoId = cmbTipoProceso.SelectedItem != null
                                        ? (int) cmbTipoProceso.SelectedValue
                                        : 0;
                string tipoProcesoString = cmbTipoProceso.SelectedItem != null
                                               ? ((TipoProcesoInfo) cmbTipoProceso.SelectedItem).Descripcion.Trim()
                                               : String.Empty;
                DateTime fechaIni;
                DateTime fechaFin;
                if (DtpFechaInicial.SelectedDate == null && DtpFechaFinal.SelectedDate == null)
                {
                    fechaIni = DateTime.Now.Date.AddMonths(-3);
                    fechaFin = DateTime.Now.Date;
                }
                else if (DtpFechaInicial.SelectedDate != null && DtpFechaFinal.SelectedDate == null)
                {
                    fechaIni = DtpFechaInicial.SelectedDate.Value;
                    fechaFin = DtpFechaInicial.SelectedDate.Value;
                }
                else
                {
                    fechaIni = DtpFechaInicial.SelectedDate.HasValue
                                   ? DtpFechaInicial.SelectedDate.Value
                                   : fechaMinima;
                    fechaFin = DtpFechaFinal.SelectedDate.HasValue
                                   ? DtpFechaFinal.SelectedDate.Value
                                   : DateTime.Today;
                }
                var organizacionSeleccionada = (OrganizacionInfo)cmbOrganizacion.SelectedItem;
                var organizacionId = organizacionSeleccionada.OrganizacionID;
                List<ReporteInventarioInfo> resultadoInfo = ObtenerReporteInventario(organizacionId, tipoProcesoId,
                                                                                     fechaIni, fechaFin);
                if (resultadoInfo != null && resultadoInfo.Count > 0)
                {
                    cmbTipoProceso.IsEnabled = false;
                    DtpFechaInicial.IsEnabled = false;
                    DtpFechaFinal.IsEnabled = false;

                    var organizacionPl = new OrganizacionPL();
                    var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                    var nombreOrganizacion = organizacion != null ? organizacion.Division : String.Empty;
                    var encabezado = new ReporteEncabezadoInfo
                                         {
                                             Titulo = Properties.Resources.ReporteResumenInventario_TituloReporte,
                                             FechaInicio = fechaIni,
                                             FechaFin = fechaFin,
                                             Organizacion =
                                                 Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" +
                                                 nombreOrganizacion + ")"
                                         };

                    foreach (var dato in resultadoInfo)
                    {
                        dato.TipoProceso = tipoProcesoString;
                        dato.Titulo = encabezado.Titulo + " " + dato.TipoProceso;
                        dato.FechaInicio = encabezado.FechaInicio;
                        dato.FechaFin = encabezado.FechaFin;
                        dato.Organizacion = encabezado.Organizacion;
                    }

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteResumenInventarioDeGanado.rpt");
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
                                      Properties.Resources.RecepcionReporteInventario_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteInventario_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteInventario_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Método obtener el reporte ejecutivo
        /// </summary>
        /// <returns></returns>
        private List<ReporteInventarioInfo> ObtenerReporteInventario(int organizacionId, int tipoproceso, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                var reporteInventarioPL = new ReporteInventarioPL();
                List<ReporteInventarioInfo> resultadoInfo = reporteInventarioPL.GenerarReporteInventario(organizacionId, tipoproceso,
                                                                                                         fechaIni,
                                                                                                         fechaFin);
                return resultadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        
        /// <summary>
        /// Carga los Roles
        /// </summary>
        private void CargaComboTipoProceso()
        {
            try
            {
                var tipoProcesoPL = new TipoProcesoPL();
                IList<TipoProcesoInfo> listaTipoProceso = tipoProcesoPL.ObtenerTodos(EstatusEnum.Activo);
                if (listaTipoProceso != null)
                {
                    var tipoProceso =
                        new TipoProcesoInfo
                            {
                                TipoProcesoID = -1,
                                Descripcion = Properties.Resources.cbo_Seleccione,
                            };
                    listaTipoProceso.Insert(0, tipoProceso);
                    var tipoProceso1 = new TipoProcesoInfo
                    {
                        TipoProcesoID = 0,
                        Descripcion = Properties.Resources.cmb_Todos,
                    };
                    listaTipoProceso.Insert(1, tipoProceso1);
                    cmbTipoProceso.ItemsSource = listaTipoProceso;
                    cmbTipoProceso.SelectedItem = tipoProceso;
                }
                else
                {
                    BloquearControles();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionReporteInventario_MsgNoExisteTiposProceso,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteInventario_MsgErrorExportarPdf,
                                  MessageBoxButton.OK, MessageImage.Error);
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
            if ( fecha != null && fecha > DateTime.Today)
            {
                result = false;
            }
            return result;
        }        

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaActual()
        {
            string mensaje = Properties.Resources.RecepcionReporteInventario_MsgFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// 
        /// </summary>
        private void BloquearControles()
        {
            cmbTipoProceso.IsEnabled = false;
            DtpFechaInicial.IsEnabled = false;
            DtpFechaFinal.IsEnabled = false;
            spGenerar.IsEnabled = false;
        }

        /// <summary>
        /// Obtiene la fecha minima en la que se podra consultar el reporte
        /// </summary>
        private void ObtenerFechaMinima()
        {
            var parametroOrganizacionPL = new ParametroOrganizacionPL();
            ParametroOrganizacionInfo parametroOrganizacion = parametroOrganizacionPL.
                ObtenerPorOrganizacionIDClaveParametro(
                    AuxConfiguracion.ObtenerOrganizacionUsuario(),
                    ParametrosEnum.DiasAtrasReporteInventario.ToString());
            if (parametroOrganizacion != null)
            {
                invervalo = Convert.ToInt32(parametroOrganizacion.Valor);
                fechaMinima = DateTime.Now.AddDays(invervalo * -1);
            }
            else
            {
                fechaMinima = DateTime.Now.AddDays(-90);
                invervalo = 90;
            }
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
