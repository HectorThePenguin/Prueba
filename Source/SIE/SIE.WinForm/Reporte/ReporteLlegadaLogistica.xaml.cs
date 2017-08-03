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
using SIE.WinForm.Controles.Ayuda;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Windows.Controls;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteLlegadaLogistica.xaml
    /// </summary>
    public partial class ReporteLlegadaLogistica
    {
        #region Propiedades

        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
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
        private OrganizacionInfo Organizacion;
        private bool IsCorporativo = false;
        #endregion

        #region Contructores
        public ReporteLlegadaLogistica()
        {
            InitializeComponent();
            CargarAyudas();
        }
        #endregion

        #region Metodos

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
            if (IsCorporativo)
            {
                skAyudaOrganizacion.AsignarFoco();
                LimpiarTodoOrganizacion();
                DtpFechaInicial.IsEnabled = false;
                DtpFechaFinal.IsEnabled = false;
            }
            else
                DtpFechaInicial.Focus();
        }

        /// <summary>
        /// Funcion que carga las ayudas
        /// </summary>
        private void CargarAyudas()
        {
            CargaOrganizaciones();
        }

        /// <summary>
        /// Método para cargar las organizaciones dependiendo si es corporativo
        /// </summary>
        /// <returns></returns>
        private void CargaOrganizaciones()
        {
            try
            {
                bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                IsCorporativo = usuarioCorporativo;

                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);

                if (usuarioCorporativo)
                {
                    AgregarAyudaOrganizacion();
                    skAyudaOrganizacion.AsignarFoco();

                    DtpFechaFinal.IsEnabled = false;
                    DtpFechaInicial.IsEnabled = false;
                }
                else
                {
                    Organizacion = organizacion;
                    txtOrganizacion.Text = string.Format("{0} - {1}", Organizacion.OrganizacionID, Organizacion.Descripcion);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteLlegadaLogistica_ErrorCargarOrganizacion,
                                  MessageBoxButton.OK, MessageImage.Error);

            }

        }

        /// <summary>
        /// Agrega la ayuda para la busqueda de Organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            try
            {
                skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(250,
                    false,
                    new OrganizacionInfo()
                        {
                            OrganizacionID = 0,
                            Activo = EstatusEnum.Activo
                        }
                    ,
                    "PropiedadClaveReporteLlegadaLogistica",
                    "PropiedadDescripcionReporteLlegadaLogistica",
                    true,
                    50,
                    true)
                {
                    AyudaPL = new OrganizacionPL(),
                    MensajeClaveInexistente = Properties.Resources.SolicitudPremezcla_OrganizacionNoExiste,
                    MensajeBusquedaCerrar = Properties.Resources.SolicitudPremezcla_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.SolicitudPremezcla_Busqueda,
                    MensajeAgregar = Properties.Resources.SolicitudPremezclas_Seleccionar,
                    TituloEtiqueta = Properties.Resources.SolicitudPremezclas_LeyendaBusqueda,
                    TituloPantalla = Properties.Resources.SolicitudPremezclas_TituloBusquedaOrganizacion,
                    MetodoPorDescripcion = "ObtenerPorPagina"

                };

                skAyudaOrganizacion.ObtenerDatos += ObtenerDatosOrganizacion;
                skAyudaOrganizacion.LlamadaMetodosNoExistenDatos += LimpiarTodoOrganizacion;

                skAyudaOrganizacion.AsignaTabIndex(0);
                splAyudaOrganizacion.Children.Clear();
                splAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
                skAyudaOrganizacion.AsignarFoco();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene la organizacion seleccionada en el filtro
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatosOrganizacion(string filtro)
        {
            try
            {
                Organizacion = skAyudaOrganizacion.Info;
                if (Organizacion != null && Organizacion.OrganizacionID != 0)
                {
                    DtpFechaFinal.IsEnabled = true;
                    DtpFechaInicial.IsEnabled = true;
                    DtpFechaInicial.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Limpia los campos cuando no existe un valor valido en la ayuda
        /// </summary>
        private void LimpiarTodoOrganizacion()
        {
            try
            {
                skAyudaOrganizacion.LimpiarCampos();
                skAyudaOrganizacion.Info = new OrganizacionInfo()
                {
                    OrganizacionID = 0,
                    Activo = EstatusEnum.Activo
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
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
            string mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgFechaFinalMenorFechaInicial;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorActual()
        {
            string mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgFechaFinalMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaFinal()
        {
            string mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgFechaInicialMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Buscar
        /// </summary>
        private void Generar()
        {
            if (ValidaGenerar())
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

                if (DtpFechaInicial.SelectedDate != null && DtpFechaFinal.SelectedDate != null &&
                    Organizacion.OrganizacionID > 0)
                {
                    var reporteLlegadaLogistica = new ReporteLlegadaLogisticaPL();
                    
                    var dateinicio = DtpFechaInicial.SelectedDate.Value;
                    var datefinal = DtpFechaFinal.SelectedDate.Value;
                    DateTime FechaInicioReporte = new DateTime(dateinicio.Year, dateinicio.Month, dateinicio.Day, 0, 0, 0);
                    DateTime FechaFinalReporte = new DateTime(datefinal.Year, datefinal.Month, datefinal.Day, 23, 59, 59);
                    List<ReporteLlegadaLogisticaDatos> resultadoInfo = reporteLlegadaLogistica.GenerarReporteLlegadaLogistica(Organizacion.OrganizacionID, dateinicio, datefinal);
                    if (resultadoInfo == null || resultadoInfo.Count == 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.ReporteLlegadaLogistica_MsgSinInformacion, MessageBoxButton.OK,
                                          MessageImage.Warning);
                    }
                    else
                    {
                        var organizacionPL = new OrganizacionPL();

                        var organizacion = organizacionPL.ObtenerPorIdConIva(Organizacion.OrganizacionID);

                        string division = organizacion != null ? organizacion.Division : string.Empty;
                        string titulo = string.Format(Properties.Resources.ReporteLlegadaLogistica_RangoFecha, DtpFechaInicial.SelectedDate.Value.ToShortDateString(), DtpFechaFinal.SelectedDate.Value.ToShortDateString());

                        foreach (var dato in resultadoInfo)
                        {
                            dato.Titulo = Properties.Resources.ReporteLlegadaLogistica_Titulo;
                            dato.TituloPeriodo = titulo;
                            dato.Organizacion = string.Format(Properties.Resources.Reporte_NombreEmpresa, division);
                        }


                        var documento = new ReportDocument();
                        var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteLlegadaLogisita.rpt");
                        documento.Load(reporte);


                        documento.DataSourceConnections.Clear();
                        documento.SetDataSource(resultadoInfo);
                        documento.Refresh();


                        var forma = new ReportViewer(documento,
                            Properties.Resources.ReporteLlegadaLogistica_Titulo);
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
                                  Properties.Resources.ReporteLlegadaLogistica_MsgErrorExportarExcel,
                                  MessageBoxButton.OK, MessageImage.Error);
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

                if (Organizacion == null || Organizacion.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgSelecioneOrganizacion;
                    skAyudaOrganizacion.AsignarFoco();
                }

                if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgFechaInicialMayorFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgFechaFinalMenorFechaInicial;
                    DtpFechaFinal.Focus();
                }
                else if (fechaIni < fechaFin.Value.AddYears(-1))
                {
                    mensaje = Properties.Resources.ReporteLlegadaLogistica_MsgFechaInicialMenorAnio;
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

        #endregion

        #region Eventos

        private void ControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
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
            DateTime fechaInicio;
            DateTime fechaFin;

            DateTime.TryParse(DtpFechaInicial.Text, out fechaInicio);
            DateTime.TryParse(DtpFechaFinal.Text, out fechaFin);

            if (fechaInicio == DateTime.MinValue || fechaFin == DateTime.MinValue)
            {
                Contexto.Valido = false;
            }
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Generar();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
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
        #endregion
    }
}
