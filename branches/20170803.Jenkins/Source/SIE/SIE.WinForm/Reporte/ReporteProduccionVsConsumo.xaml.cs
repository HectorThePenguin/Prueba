using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteProduccionVsConsumo.xaml
    /// </summary>
    public partial class ReporteProduccionVsConsumo
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

        /// <summary>
        /// Constructor
        /// </summary>
        public ReporteProduccionVsConsumo()
        {
            InitializeComponent();
            InicializaContexto();
        }

        #endregion

        #region Metodos
        /// <summary>
        /// Inicializar el contexto
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
        /// Generar el reporte de Produccion vs consumo
        /// </summary>
        /// <returns></returns>
        private void Generar()
        {
            try
            {
                var organizacionCombo = (OrganizacionInfo)cmbOrganizacion.SelectedItem;
                int organizacionId = 0;
                if (organizacionCombo != null)
                {
                    organizacionId = organizacionCombo.OrganizacionID;
                }

                DateTime fechaInicial = DtpFechaInicial.SelectedDate.Value;
                DateTime fechaFinal = DtpFechaFinal.SelectedDate.Value;

                var resultadoInfo = ObtenerReporteProduccionVsConsumo(organizacionId,fechaInicial,fechaFinal);

                if (resultadoInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteProduccionVsConsumo_MensajeReporteSinDatos,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }

                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                var titulo = Properties.Resources.ReporteProduccionVsConsumo_TituloReporte;

                var nombreOrganizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + organizacion.Division + ")";

                foreach (var item in resultadoInfo)
                {
                    item.OrganizacionReporte = nombreOrganizacion;
                    item.Titulo = titulo;
                    item.FechaDesde = fechaInicial;
                    item.FechaHasta = fechaFinal;

                }

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteProduccionVsConsumo.rpt");
                documento.Load(reporte);


                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();


                var forma = new ReportViewer(documento, Properties.Resources.ReporteProduccionVsConsumo_TituloReporte);
                forma.MostrarReporte();
                forma.Show();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteProduccionVsConsumo_FalloCargarReporte, MessageBoxButton.OK,
                                                      MessageImage.Error);
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

                var organizacionesPl = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPl.ObtenerTipoGanaderas();
                if (usuarioCorporativo)
                {

                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReporteProduccionVsConsumo_cmbSeleccione,
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
                                  Properties.Resources.ReporteProduccionVsConsumo_ErrorCargarCombos,
                                  MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Metodo para obtener los datos del informe
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechainicial"></param>
        /// <param name="fechafinal"></param>
        /// <returns></returns>
        private IList<ReporteProduccionVsConsumoInfo> ObtenerReporteProduccionVsConsumo(int organizacionId,DateTime fechainicial,DateTime fechafinal)
        {
            try
            {
                var reportePl = new ReporteProduccionVsConsumoPl();
                return reportePl.ObtenerReporteResumenInventario(organizacionId,fechainicial,fechafinal);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos()
        {
            cmbOrganizacion.SelectedIndex = 0;
            DtpFechaInicial.SelectedDate = null;
            DtpFechaFinal.SelectedDate = null;
            cmbOrganizacion.Focus();
        }

        /// <summary>
        /// Validar el Formulario
        /// </summary>
        /// <returns></returns>
        private bool ValidarFormulario()
        {
            bool resultado = true;

            var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

            if (organizacion == null || organizacion.OrganizacionID == 0)
            {
                resultado = false;
            }
            btnGenerar.IsEnabled = resultado;
            return resultado;
        }
        /// <summary>
        /// Valida las Fechas
        /// </summary>
        private Boolean ValidaFechas(Boolean mensajes = true)
        {
            if (DtpFechaInicial.SelectedDate != null && DtpFechaFinal.SelectedDate != null)
            {
                if (DtpFechaInicial.SelectedDate.Value > DtpFechaFinal.SelectedDate.Value)
                {
                    if (mensajes)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ReporteProduccionVsConsumo_MensajeFechaFinalMenorInicial,
                            MessageBoxButton.OK, MessageImage.Warning);
                        DtpFechaFinal.SelectedDate = null;
                    }
                    return false;
                }

                if (DtpFechaInicial.SelectedDate.Value.AddYears(1) < DtpFechaFinal.SelectedDate.Value)
                {
                    if (mensajes)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ReporteProduccionVsConsumo_MensajeRangoInvalidoMayorAño,
                            MessageBoxButton.OK, MessageImage.Warning);
                        DtpFechaFinal.SelectedDate = null;
                    }
                    return false;
                }
                return true;
            }

            return false;

        }
        #endregion Metodos

        #region Eventos


        /// <summary>
        /// Evento del boton limpiar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
        /// <summary>
        /// Evento de carga del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            CargaOrganizaciones();
        }
        /// <summary>
        /// Evento del boton Generar del reporte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
                Generar();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrganizacion_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ValidarFormulario();
        }

        /// <summary>
        /// Evento disparado al cambiar la fecha inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaInicial_SelectedDateChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DtpFechaInicial.SelectedDate != null)
            {
                if (DtpFechaInicial.SelectedDate.Value.Date > DateTime.Now.Date)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ReporteProduccionVsConsumo_MensajeFechaInicialMayorActual,
                        MessageBoxButton.OK, MessageImage.Warning);
                    DtpFechaInicial.SelectedDate = null;
                    return;
                }
                ValidaFechas();
            }
            ValidarFormulario();
        }

        /// <summary>
        /// Evento disparado al cambiar la fecha Final
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFechaFinal_SelectedDateChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DtpFechaFinal.SelectedDate != null)
            {
                if (DtpFechaFinal.SelectedDate.Value.Date > DateTime.Now.Date)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ReporteProduccionVsConsumo_MensajeFechaFinalMayorActual,
                        MessageBoxButton.OK, MessageImage.Warning);
                    DtpFechaFinal.SelectedDate = null;
                    return;
                }

                ValidaFechas();
            }
            ValidarFormulario();
        }

        #endregion Eventos


    }
}
