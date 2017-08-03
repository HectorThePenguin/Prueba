using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Data;
using System.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Controls;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteKardexGanado.xaml
    /// </summary>
    public partial class ReporteKardexGanado
    {
        public ReporteKardexGanado()
        {
            InitializeComponent();
            CargaComboOrganizaciones();
            CargaComboTipoProceso();
            DtpFechaInicio.DisplayDateEnd = DateTime.Now;
            DtpFechaFin.DisplayDateEnd = DateTime.Now;
        }

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.cmbTipoProceso.SelectedIndex = 0;
        }

        private void ucTitulo_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTipoProceso.SelectedIndex > 0)
            {
                if (DtpFechaInicio.SelectedDate != null && DtpFechaFin.SelectedDate != null)
                {
                    if (DtpFechaInicio.SelectedDate <= DtpFechaFin.SelectedDate)
                        GenerarReporte();
                    else
                    {
                        MostrarMensaje(Properties.Resources.ReporteKardexGanado_FechaFinMayor);
                    }
                }
                else
                {
                    MostrarMensaje(Properties.Resources.ReporteKardexGanado_FechaVacia);
                }
            }
            else
            {
                MostrarMensaje(Properties.Resources.ReporteKardexGanado_SeleccionarTipoProceso);
            }
        }

        private void GenerarReporte()
        {
            try
            {
                var resultadoInfo = ObtenerReporteKardexGanado();

                if (resultadoInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteKardexGanado_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                else
                {
                    //string division = AuxDivision.ObtenerDivision(AuxConfiguracion.ObtenerOrganizacionUsuario());

                    var organizacionPl = new OrganizacionPL();

                    int idOrganizacion = int.Parse(this.cmbOrganizacion.SelectedValue.ToString());

                    var organizacion = organizacionPl.ObtenerPorIdConIva(idOrganizacion);

                    var titulo = Properties.Resources.ReporteKardexGanado_TituloReporte;
                    var nombreOrganizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + organizacion.Division + ")";

                    foreach (var item in resultadoInfo)
                    {
                        item.Organizacion = nombreOrganizacion;
                        item.Titulo = titulo;
                        item.Fecha = DateTime.Now;
                        item.Tipo = item.Tipo == string.Empty ? Properties.Resources.ReporteKardexGanado_Recepcion : item.Tipo;
                    }

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\ReporteKardexDeGanado.rpt");
                    documento.Load(reporte);

                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(resultadoInfo);
                    documento.Refresh();

                    //ReportViewer
                    var forma = new ReportViewer(documento, Properties.Resources.ReporteKardexGanado_TituloReporte);
                    forma.MostrarReporte();
                    forma.Show();
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
                                  Properties.Resources.ReporteKardexGanado_MsgErrorGenerarRPT,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }


        private IList<ReporteKardexGanadoInfo> ObtenerReporteKardexGanado()
        {
            try
            {
                var reporte = new ReporteKardexGanadoPL();

                int iOranizacion = cmbOrganizacion.SelectedItem != null
                                        ? (int)cmbOrganizacion.SelectedValue
                                        : 0;

                int iTipoProceso = cmbTipoProceso.SelectedItem != null
                                        ? (int)cmbTipoProceso.SelectedValue
                                        : 0;

                DateTime fechaInicio = DtpFechaInicio.SelectedDate.HasValue
                                        ? DtpFechaInicio.SelectedDate.Value
                                        : new DateTime(); 

                DateTime fechaFin = DtpFechaFin.SelectedDate.HasValue
                                        ? DtpFechaFin.SelectedDate.Value
                                        : new DateTime(); 

                var filtro = new FiltroParametrosKardexGanado
                {
                    OrgazizacionId=iOranizacion,
                    TipoProceso = iTipoProceso,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin
                };

                var resultadoInfo = reporte.Generar(filtro);

                return resultadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
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

        private void LimpiarCampos()
        {
            this.btnGenerar.IsEnabled = false;
            this.cmbTipoProceso.SelectedIndex = 0;
            this.cmbTipoProceso.IsEnabled = false;
            CargaComboOrganizaciones();
            this.DtpFechaFin.SelectedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
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
        private void DtpFecha_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidarFecha();
        }

        private void ValidarFecha()
        {
            if (DtpFechaFin.SelectedDate != null)
            {
                var fechaFinal = DtpFechaFin.SelectedDate.Value.SoloFecha();

                if (fechaFinal > DateTime.Now)
                {
                    MostrarMensaje(Properties.Resources.ReporteKardexGanado_MsgFechaInicialMayorFechaActual);
                    this.btnGenerar.IsEnabled = false;
                    this.DtpFechaFin.SelectedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                else
                {
                    this.btnGenerar.IsEnabled = true;
                }
            }
        }
        private void MostrarMensaje(string mensaje)
        {
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Selecciona un Tipo de Proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Organizacion_SelectionChaged(object sender, SelectionChangedEventArgs e)
        {
            ValidaControles();
        }

        private void ValidaControles()
        {

            Boolean Valido = true;
            var organizacionvr = (OrganizacionInfo)this.cmbOrganizacion.SelectedItem;

            if (organizacionvr != null)
            {
                if (organizacionvr.OrganizacionID == 0)
                {
                    Valido = false;
                }
                else
                {
                    this.cmbTipoProceso.IsEnabled = true;
                }
            }
            else
            {
                Valido = false;
            }


            
            //ValidaTipoProceso
            /*int tipoProceso = cmbTipoProceso.SelectedIndex;
            if (tipoProceso > 0)
            {
                this.DtpFecha.IsEnabled = true;
                Valido = true;
            }
            else
            {
                this.DtpFecha.IsEnabled = false ;
                Valido = false;
            }*/

            //Validar Fecha
            /*if (DtpFecha.SelectedDate == null || !DtpFecha.IsEnabled)
            {
                Valido = false;
            }
            else
            {
                DateTime FechaD = DtpFecha.SelectedDate.Value;
                if (FechaD <= DateTime.Now)
                {
                    
                    Valido = true;
                }
                else
                {
                    try
                    {
                        DateTime Fecha = DtpFecha.SelectedDate.Value;
                    }
                    catch (Exception)
                    {
                        Valido = false;
                    }
                }
                
            }*/

            btnGenerar.IsEnabled = Valido;


        }

        /// <summary>
        /// Selecciona un Tipo de Proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TipoProceso_SelectionChaged(object sender, SelectionChangedEventArgs e)
        {
            ValidaControles();   
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
                            TipoProcesoID = 0,
                            Descripcion = Properties.Resources.cbo_Seleccione,
                        };
                    listaTipoProceso.Insert(0, tipoProceso);
                    cmbTipoProceso.ItemsSource = listaTipoProceso;
                    cmbTipoProceso.SelectedItem = tipoProceso;
                }
                else
                {
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

        private void CargaComboOrganizaciones()
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

                cmbOrganizacion.ItemsSource = listaorganizaciones;

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
                    cmbOrganizacion.SelectedIndex = 0;
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
                    cmbOrganizacion.SelectedIndex = 0;
                    cmbOrganizacion.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteKardexGanado_ErrorCargarOrganizaciones,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void DtpFechaInicio_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ValidarFechaInicio();
        }

        private void ValidarFechaInicio()
        {
            if (DtpFechaInicio.SelectedDate != null)
            {
                var fechaFinal = DtpFechaInicio.SelectedDate.Value.SoloFecha();

                if (fechaFinal > DateTime.Now)
                {
                    MostrarMensaje(Properties.Resources.ReporteKardexGanado_MsgFechaInicialMayorFechaActual);
                    this.btnGenerar.IsEnabled = false;
                    this.DtpFechaInicio.SelectedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }
                else
                {
                    this.btnGenerar.IsEnabled = true;
                }
            }
        }
    }
}

		