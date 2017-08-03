using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
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
using System.Resources;
using System.Drawing;


namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteBitacoraLlegada.xaml
    /// </summary>
    public partial class ReporteBitacoraLlegada
    {
        /// <summary>
        /// Propiedad que contiene el control de ayuda para la organizacion
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        /// <summary>
        /// Propiedad que contiene la orgnizacion seleccionadad en la ayuda
        /// </summary>
        private OrganizacionInfo organizacionSeleccionada;
        /// <summary>
        /// Propiedad que contiene la bandera cuando el usuario en sesion es corporativo
        /// </summary>
        private bool isUsuarioCorporativo = true;
        /// <summary>
        /// Propiedad que contiene la fecha inicial a exportar
        /// </summary>
        private DateTime fechaInicialExportar;
        /// <summary>
        /// Propiedad que contiene la fecha final a exportar
        /// </summary>
        private DateTime fechaFinalExportar;
        /// <summary>
        /// Constructor
        /// </summary>
        public ReporteBitacoraLlegada()
        {
            InitializeComponent();
            InicializarDatosUsuario();
        }
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
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroFechasInfo
            {
                OrgazizacionId = 0,
                FechaInicial = null,
                FechaFinal = null,
                Valido = false
            };
        }
        /// <summary>
        /// Inicializar datos de usuario
        /// </summary>
        private void InicializarDatosUsuario()
        {
            InicializaContexto();
            AgregarAyudaOrganizacion(splAyudaOrganizacion);
        }
        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaFinal()
        {
            string mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }
        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgFechaInicialMayorFechaActual;
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
            string mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }
        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorActual()
        {
            string mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgFechaFinalMayorFechaActual;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }
        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos(bool cancelar)
        {
            InicializaContexto();
            if (cancelar)
            {
                fechaInicialExportar = DateTime.MinValue;
                fechaFinalExportar = DateTime.MinValue;
            }

            LimpiarOrganizacion();
            organizacionSeleccionada = null;

            DtpFechaInicial.IsEnabled = true;
            DtpFechaFinal.IsEnabled = true;

            skAyudaOrganizacion.AsignarFoco();
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
        /// Metodo para obtener los datos del informe
        /// El TipoFecha esta a la espera de 1.-Fecha Llegada, 2.-Fecha Entrada y 3.-Fecha Pesaje
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="tipoTratamiento"></param>
        /// <param name="organizacionId"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        private List<ReporteBitacoraLlegadaInfo> ObtenerReporteBitacoraLlegada(ReporteEncabezadoInfo encabezado, int organizacionId, int tipoFecha, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                var reporteBitacoraLlegadaPL = new ReporteBitacoraLlegadaPL();
                List<ReporteBitacoraLlegadaInfo> resultadoInfo = reporteBitacoraLlegadaPL.ObtenerReporteBitacoraLlegada(encabezado, organizacionId, tipoFecha, fechaIni, fechaFin);
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
                ParametroGeneralPL parametroGeneralPl = new ParametroGeneralPL();
                ParametroGeneralInfo estandarInfo = parametroGeneralPl.ObtenerPorClaveParametro(ParametrosEnum.EstandarTiempo.ToString());
                ParametroGeneralInfo porcMinimoInfo = parametroGeneralPl.ObtenerPorClaveParametro(ParametrosEnum.PorcMinimo.ToString());
                ParametroGeneralInfo porcMaximoInfo = parametroGeneralPl.ObtenerPorClaveParametro(ParametrosEnum.PorcMaximo.ToString());

                if (estandarInfo == null || porcMaximoInfo == null || porcMaximoInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteBitacoraLlegada_MsgParametrosNoEncontrados,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }

                int organizacionId = organizacionSeleccionada.OrganizacionID;

                int tipoFecha = 1;
                if(rbFechaEntrada.IsChecked == true) tipoFecha = 2;
                else if(rbFechaPesaje.IsChecked == true) tipoFecha = 3;

                DateTime fechaIni = DtpFechaInicial.SelectedDate.HasValue
                                       ? DtpFechaInicial.SelectedDate.Value
                                       : new DateTime();
                DateTime fechaFin = DtpFechaFinal.SelectedDate.HasValue
                                        ? DtpFechaFinal.SelectedDate.Value
                                        : fechaIni;

                

                var organizacionPl = new OrganizacionPL();
                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var encabezado = new ReporteEncabezadoInfo()
                {
                    Titulo = Properties.Resources.ReporteBitacoraLlegada_Titulo,
                    FechaInicio = fechaIni,
                    FechaFin = fechaFin,
                    //Organizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + nombreOrganizacion + ")"
                    Organizacion = nombreOrganizacion
                };

                List<ReporteBitacoraLlegadaInfo> resultadoInfo = ObtenerReporteBitacoraLlegada(encabezado, organizacionId, tipoFecha, fechaIni, fechaFin);

                if (resultadoInfo == null || !resultadoInfo.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteBitacoraLlegada_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCampos(true);
                    return;
                }
              
                string [] arrEstandar = estandarInfo.Valor.Split(':');
                TimeSpan Estandar = new TimeSpan(int.Parse(arrEstandar[0]), int.Parse(arrEstandar[1]), int.Parse(arrEstandar[2]));
                int PorcMinimo = int.Parse(porcMinimoInfo.Valor);
                int PorcMaximo = int.Parse(porcMaximoInfo.Valor);
                double DestaraMenosTiempoEstandarMasEstandar;

                DtpFechaInicial.IsEnabled = false;
                DtpFechaFinal.IsEnabled = false;

                foreach (var dato in resultadoInfo)
                {
                    dato.Organizacion = encabezado.Organizacion;
                    dato.Titulo = encabezado.Titulo;
                    dato.FechaFin = encabezado.FechaFin;
                    dato.FechaInicio = encabezado.FechaInicio;

                    DestaraMenosTiempoEstandarMasEstandar = dato.LlegadaDestara.Subtract(dato.TiempoEstandar).Add(Estandar).TotalSeconds;
                    if (dato.TiempoEstandar.TotalSeconds == 0 ||
                       (DestaraMenosTiempoEstandarMasEstandar / dato.TiempoEstandar.TotalSeconds) * 100 >= PorcMaximo)
                    {
                        dato.Indicador = 1; //Rojo
                    }
                    else if ((DestaraMenosTiempoEstandarMasEstandar / dato.TiempoEstandar.TotalSeconds) * 100 > PorcMinimo)
                    {
                        dato.Indicador = 2; //Amarillo
                    }
                    else
                    {
                        dato.Indicador = 3;//Verde
                    }
                }

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteBitacoraLlegada.rpt");
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
                MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteBitacoraLlegada_MsgErrorExportarExcel, MessageBoxButton.OK,
                                                      MessageImage.Error);
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

                if (organizacionSeleccionada == null || organizacionSeleccionada.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgSelecioneOrganizacion;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgFechaInicialMayorFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgFechaInicialMayorFechaFinal;
                    DtpFechaFinal.Focus();
                }
                else if (fechaIni < fechaFin.Value.AddYears(-1))
                {
                    mensaje = Properties.Resources.ReporteBitacoraLlegada_MsgFechaInicialMenorAnio;
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
        /// Obtener la ayuda de seleccion de organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion(StackPanel stackPanel)
        {
            try
            {
                var organizacionInfo = new OrganizacionInfo
                {
                    Activo = EstatusEnum.Activo
                };

                skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, organizacionInfo
                                                   , "PropiedadClaveCatalogoParametroOrganizacion"
                                                   , "PropiedadDescripcionCatalogoParametroOrganizacion",
                                                   "", false, 50, 9, true)
                {
                    AyudaPL = new OrganizacionPL(),
                    MensajeClaveInexistente = Properties.Resources.ReporteBitacoraLlegada_AyudaOrganizacionClaveInexistente,
                    MensajeBusquedaCerrar = Properties.Resources.ReporteBitacoraLlegada_AyudaOrganizacionSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.ReporteBitacoraLlegada_AyudaOrganizacionMensajeBusqueda,
                    MensajeAgregar = Properties.Resources.ReporteBitacoraLlegada_AyudaOrganizacionMensajeAgregar,
                    TituloEtiqueta = Properties.Resources.ReporteBitacoraLlegada_AyudaOrganizacionTituloEtiqueta,
                    TituloPantalla = Properties.Resources.ReporteBitacoraLlegada_AyudaOrganizacionTituloPantalla
                };

                skAyudaOrganizacion.ObtenerDatos += ObtenerDatosOrganizacion;
                skAyudaOrganizacion.LlamadaMetodosNoExistenDatos += LimpiarOrganizacion;

                skAyudaOrganizacion.AsignaTabIndex(0);
                stackPanel.Children.Clear();
                stackPanel.Children.Add(skAyudaOrganizacion);
                skAyudaOrganizacion.TabIndex = 0;  
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }      
        /// <summary>
        /// Limpia los campos de la ayuda organizacion
        /// </summary>
        private void LimpiarOrganizacion()
        {
            if(isUsuarioCorporativo)
                skAyudaOrganizacion.LimpiarCampos();
        }
        /// <summary>
        /// Obtiene la informacion de la clave seleccionada
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosOrganizacion(string clave)
        {
            try
            {
                if (skAyudaOrganizacion.Info != null)
                {
                    organizacionSeleccionada = skAyudaOrganizacion.Info;
                    skAyudaOrganizacion.Info = new OrganizacionInfo
                    {
                        Activo = EstatusEnum.Activo
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Handler fechas keydown
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
            DateTime fechaInicio;
            DateTime fechaFin;

            DateTime.TryParse(DtpFechaInicial.Text, out fechaInicio);
            DateTime.TryParse(DtpFechaFinal.Text, out fechaFin);

            if (fechaInicio == DateTime.MinValue || fechaFin == DateTime.MinValue)
            {
                Contexto.Valido = false;
            }
        }
        /// <summary>
        /// Handler fecha final
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Handler fecha inicial
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
        /// <summary>
        /// Handler boton limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos(false);
        }
        /// <summary>
        /// Handler boton generar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Generar();
        }
        /// <summary>
        /// Handler forma cargada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReporteBitacoraLlegada_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!AuxConfiguracion.ObtenerUsuarioCorporativo())
            {
                //Obtener la organizacion del usuario
                var organizacionPl = new OrganizacionPL();
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                OrganizacionInfo organizacionInfo = organizacionPl.ObtenerPorID(organizacionId);

                skAyudaOrganizacion.Info = organizacionInfo;
                skAyudaOrganizacion.Clave = organizacionInfo.OrganizacionID.ToString();
                skAyudaOrganizacion.Descripcion = organizacionInfo.Descripcion;
                skAyudaOrganizacion.UpdateLayout();
                organizacionSeleccionada = organizacionInfo;
                skAyudaOrganizacion.IsEnabled = false;
                isUsuarioCorporativo = false;
            }
        }
    }
}
