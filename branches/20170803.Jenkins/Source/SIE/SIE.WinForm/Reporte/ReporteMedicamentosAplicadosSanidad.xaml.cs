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


namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteMedicamentosAplicadosSanidad.xaml
    /// </summary>
    public partial class ReporteMedicamentosAplicadosSanidad
    {
        private readonly int[] tratamientosValidos = new[] { TipoMovimiento.EntradaEnfermeria.GetHashCode(), TipoMovimiento.EntradaSalidaEnfermeria.GetHashCode(), TipoMovimiento.SalidaEnfermeria.GetHashCode() };
        /// <summary>
        /// Propiedad que contiene la fecha inicial a exportar
        /// </summary>
        private DateTime fechaInicialExportar;

        /// <summary>
        /// Propiedad que contiene la fecha final a exportar
        /// </summary>
        private DateTime fechaFinalExportar;

        /// <summary>
        /// Propiedad que contiene la ayuda de almacen
        /// </summary>
        private SKAyuda<AlmacenInfo> skAyudaAlmacen;
        private AlmacenInfo almacenSeleccionadoInfo;

        public ReporteMedicamentosAplicadosSanidad()
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
            //CargarOrganizaciones();
            CargarTiposTratamiento();
            InicializaContexto();
        }

        /// <summary>
        /// Método para cargar los Tipos de Tratamiento
        /// </summary>
        /// <returns></returns>
        private void CargarTiposTratamiento()
        {
            try
            {
                var tipoMovimientoPL = new TipoMovimientoPL();
                IList<TipoMovimientoInfo> listaMovimientos = tipoMovimientoPL.ObtenerTodos(EstatusEnum.Activo);
                List<TipoMovimientoInfo> listaMovimientosValidos =
                    listaMovimientos.Where(mov => tratamientosValidos.Contains(mov.TipoMovimientoID)).ToList();

                var tipoMovimientoTotalGeneral = new TipoMovimientoInfo
                {
                    TipoMovimientoID = 0,
                    Descripcion = Properties.Resources.ReporteMedicamentosAplicadosSanidad_TotalGeneral
                };
                listaMovimientosValidos.Insert(0, tipoMovimientoTotalGeneral);
                cmbTipoMovimiento.ItemsSource = listaMovimientosValidos;
                if (cmbTipoMovimiento.SelectedItem == null)
                {
                    cmbTipoMovimiento.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
               Logger.Error(ex);
               SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.ReporteMedicamentosAplicadosSanidad_ErrorCargarTipoTratamientos,
                                 MessageBoxButton.OK, MessageImage.Error);

            }

        }

        /// <summary>
        /// Carga las organizaciones
        /// </summary>
        private void CargarOrganizaciones()
        {
            try
            {
                bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
                //int tipoOrganizacion = organizacion.TipoOrganizacion.TipoOrganizacionID;
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var organizacionesPl = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPl.ObtenerTipoGanaderas();
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
                                  Properties.Resources.ReporteMedicamentosAplicadosSanidad_ErrorCargarOrganizaciones,
                                  MessageBoxButton.OK, MessageImage.Error);

            }

        }

        //Agrega la ayuda para la organizacion
        private void AgregarAyudaAlmacen()
        {
            try
            {
                skAyudaAlmacen = new SKAyuda<AlmacenInfo>(
                    180,
                    false,
                    new AlmacenInfo()
                    {
                        CodigoAlmacen = string.Empty,
                        Descripcion = string.Empty,
                        Organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem,
                        ListaTipoAlmacen = new List<TipoAlmacenInfo>() {{new TipoAlmacenInfo(){ TipoAlmacenID = (int)TipoAlmacenEnum.Enfermeria }},},
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadClaveReporteMedicamentosAplicados",
                    "PropiedadDescripcionReporteMedicamentosAplicados",
                    true,
                    60,
                    true)
                {
                    AyudaPL = new AlmacenPL(),
                    MensajeClaveInexistente = Properties.Resources.AyudaAlmacen_CodigoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.AyudaAlmacen_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.AyudaAlmacen_Busqueda,
                    MensajeAgregar = Properties.Resources.AyudaAlmacen_Seleccionar,
                    TituloEtiqueta = Properties.Resources.AyudaAlmacen_LeyendaBusqueda,
                    TituloPantalla = Properties.Resources.AyudaAlmacen_Busqueda_Titulo,
                    MetodoPorDescripcion = "ObtenerPorPaginaTipoAlmacen"
                };

                skAyudaAlmacen.ObtenerDatos += ObtenerDatosAyudaAlmacen;
                skAyudaAlmacen.LlamadaMetodosNoExistenDatos += LimpiarTodoAyudaAlmacen;

                skAyudaAlmacen.AsignaTabIndex(0);
                splAyudaAlmacen.Children.Clear();
                splAyudaAlmacen.Children.Add(skAyudaAlmacen);
                skAyudaAlmacen.TabIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        //Inicializa el filtro de la ayuda de entrada producto
        private void InicializaFiltroAyudaAlmacen()
        {
            skAyudaAlmacen.Info = new AlmacenInfo()
            {
                CodigoAlmacen = string.Empty,
                Descripcion = string.Empty,
                Organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem,
                ListaTipoAlmacen = new List<TipoAlmacenInfo>() { { new TipoAlmacenInfo() { TipoAlmacenID = (int)TipoAlmacenEnum.Enfermeria } }, },
                Activo = EstatusEnum.Activo
            };
        }

        //Asigna la informacion de la ayuda cuando se selecciona un registro
        private void ObtenerDatosAyudaAlmacen(string filtro)
        {
            almacenSeleccionadoInfo = skAyudaAlmacen.Info;
            InicializaFiltroAyudaAlmacen();
        }

        //Limpia la ayuda cuando no existe el registro buscado
        private void LimpiarTodoAyudaAlmacen()
        {
            skAyudaAlmacen.Info = null;
            almacenSeleccionadoInfo = null;
            InicializaFiltroAyudaAlmacen();
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaFinal()
        {
            string mensaje = Properties.Resources.RecepcionMedicamentosAplicadosSanidad_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.ReporteMedicamentosAplicadosSanidad_MsgFechaInicialMayorFechaActual;
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
            string mensaje = Properties.Resources.RecepcionMedicamentosAplicadosSanidad_MsgFechaInicialMayorFechaFinal;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaFinalMayorActual()
        {
            string mensaje = Properties.Resources.ReporteMedicamentosAplicadosSanidad_MsgFechaFinalMayorFechaActual;
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

            cmbTipoMovimiento.SelectedIndex = 0;
            cmbTipoMovimiento.IsEnabled = true;

            cmbOrganizacion.SelectedIndex = 0;

            skAyudaAlmacen.IsEnabled = true;
            LimpiarTodoAyudaAlmacen();

            DtpFechaInicial.IsEnabled = true;
            DtpFechaFinal.IsEnabled = true;
            DtpFechaInicial.Focus();
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
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="tipoTratamiento"></param>
        /// <param name="organizacionId"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        private List<ReporteMedicamentosAplicadosModel> ObtenerReporteMedicamentosAplicadosSanidad(ReporteEncabezadoInfo encabezado, int tipoTratamiento, int organizacionId, int almacenId, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                var reporteMedicamentosAplicadosPL = new ReporteMedicamentosAplicadosPL();
                List<ReporteMedicamentosAplicadosModel> resultadoInfo = reporteMedicamentosAplicadosPL.ObtenerReporteMedicamentosAplicadosSanidad(encabezado, organizacionId, almacenId, fechaIni, fechaFin, tipoTratamiento);
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

                var orgCombo = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
                int organizacionId = orgCombo.OrganizacionID;

                string strTitulo = Properties.Resources.ReporteMedicamentosAplicadosSanidad_TituloReporte;

                int almacenId = 0;
                if (almacenSeleccionadoInfo != null)
                {
                    almacenId = almacenSeleccionadoInfo.AlmacenID;
                    strTitulo += "(" + almacenSeleccionadoInfo.Descripcion + ")";
                }

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
                    Titulo = strTitulo,
                    FechaInicio = fechaIni,
                    FechaFin = fechaFin,
                    Organizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + nombreOrganizacion + ")"
                };

                List<ReporteMedicamentosAplicadosModel> resultadoInfo = ObtenerReporteMedicamentosAplicadosSanidad(encabezado, tipoTratamiento, organizacionId, almacenId, fechaIni, fechaFin);

                if (resultadoInfo == null || !resultadoInfo.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteOperacionSanidad_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCampos(true);
                    return;
                }

                DtpFechaInicial.IsEnabled = false;
                DtpFechaFinal.IsEnabled = false;
                cmbTipoMovimiento.IsEnabled = false;
                skAyudaAlmacen.IsEnabled = false;

                foreach (var dato in resultadoInfo)
                {
                    dato.Organizacion = encabezado.Organizacion;
                    dato.Titulo = encabezado.Titulo;
                    dato.FechaFin = encabezado.FechaFin;
                    dato.FechaInicio = encabezado.FechaInicio;
                    dato.Id = tipoTratamiento;
                }

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteMedicamentosAplicadosSanidad.rpt");
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
                       Properties.Resources.ReporteMedicamentosAplicados_FalloCargarReporte, MessageBoxButton.OK,
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

                var organizacion = (OrganizacionInfo) cmbOrganizacion.SelectedItem;

                if (organizacion == null || organizacion.OrganizacionID == 0)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicadosSanidad_MsgSelecioneOrganizacion;
                    cmbOrganizacion.Focus();
                }
                else if (fechaIni == null && fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicadosSanidad_MsgSelecionePeriodo;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni == null)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicadosSanidad_MsgFechaIniRequerida;
                    DtpFechaInicial.Focus();
                }
                else if (fechaIni > DateTime.Today)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicadosSanidad_MsgFechaInicialMayorFechaActual;
                    DtpFechaInicial.Focus();
                }
                else if (fechaFin == null)
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicadosSanidad_MsgFechaFinRequerida;
                    DtpFechaFinal.Focus();
                }
                else if (fechaFin < fechaIni)
                {
                    mensaje = Properties.Resources.RecepcionMedicamentosAplicadosSanidad_MsgFechaInicialMayorFechaFinal;
                    DtpFechaFinal.Focus();
                }
                else if (fechaIni < fechaFin.Value.AddYears(-1))
                {
                    mensaje = Properties.Resources.ReporteMedicamentosAplicadosSanidad_MsgFechaInicialMenorAnio;
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
        /// handler combo organizaciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrganizacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ;
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
        private void ReporteMedicamentosAplicadosSanidad_OnLoaded(object sender, RoutedEventArgs e)
        {
            CargarOrganizaciones();
            AgregarAyudaAlmacen();
        }
    }
}
