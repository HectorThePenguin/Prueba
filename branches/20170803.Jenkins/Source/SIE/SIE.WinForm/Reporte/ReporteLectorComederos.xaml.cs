using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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


namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteLectorComederos.xaml
    /// </summary>
    public partial class ReporteLectorComederos 
    {
         #region Propiedades

        /// <summary>
        /// Propiedad que contiene el DataContext de la pantalla
        /// </summary>
        private FiltroReporteDetalleReimplante Contexto
        {
            set { DataContext = value; }
        }

        public ReporteLectorComederos()
        {
            InitializeComponent();
            CargaOrganizaciones();
            CargarHorarios();
            InicializaContexto();
        }
        #endregion
        
        #region Metodos
        /// <summary>
        /// Inicializar el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroReporteDetalleReimplante
            {
                Fecha = DateTime.Now,
                Valido = false
            };
        }
        /// <summary>
        /// Generar el reporte de Lector Comederos
        /// </summary>
        /// <returns></returns>
        private void Generar()
        {
            try
            {
                var organizacionCombo = (OrganizacionInfo)cmbOrganizacion.SelectedItem;
                var organizacionId = 0;
                if (organizacionCombo != null)
                {
                    organizacionId = organizacionCombo.OrganizacionID;
                }

                DateTime? fechaHoy;
                if (DtpFecha.SelectedDate != null)
                {
                    fechaHoy = DtpFecha.SelectedDate;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.ReporteLectorComederos_MensajeFechaError,
                                 MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                int horario;
                if (cmbHorarios.SelectedItem != null)
                {
                    horario = cmbHorarios.SelectedIndex;
                }     
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.ReporteLectorComederos_MensajeHorarioError,
                                     MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCampos();
                    return;
                }
                
                var resultadoInfo = ObtenerReporteLectorComederos(organizacionId, horario, fechaHoy);

                if (resultadoInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteLectorComederos_MensajeReporteSinDatos,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCampos();
                    return;
                }


                var organizacionPl = new OrganizacionPL();
                if (organizacionCombo != null)
                {
                    var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionCombo.OrganizacionID);

                    var titulo = Properties.Resources.ReporteLectorComederos_TituloReporte;
                    var nombreOrganizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + organizacion.Division + ")";


                    foreach (var item in resultadoInfo)
                    {
                        item.NombreOrganizacion = nombreOrganizacion;
                        item.Titulo = titulo;
                    }
                }

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteLectorComederos.rpt");
                var rutaReporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\ReporteLectorComederos.xls");
                documento.Load(reporte);

                
                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();

                documento.ExportToDisk(ExportFormatType.Excel, rutaReporte);

                Process.Start(rutaReporte);

                //var forma = new ReportViewer(documento, Properties.Resources.ReporteLectorComederos_Titulo);
                //forma.MostrarReporte();
                //forma.Show();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteLectorComederos_FalloCargarReporte, MessageBoxButton.OK,
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
                var usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                //Obtener la organizacion del usuario
                var organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var organizacionesPL = new OrganizacionPL();
                var listaorganizaciones = organizacionesPL.ObtenerTipoGanaderas();
                if (usuarioCorporativo)
                {

                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReporteLectorComederos_cmbSeleccione,
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
                    btnGenerar.IsEnabled = true;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteLectorComederos_ErrorCargarCombos,
                                  MessageBoxButton.OK, MessageImage.Error);

            }

        }

        private void CargarHorarios()
        {
                var horariosComedero = new ObservableCollection<string>();
                horariosComedero.Add("Mañana");
                horariosComedero.Add("Tarde");
            cmbHorarios.ItemsSource = horariosComedero;
        }

        /// <summary>
        /// Obtiene los datos del reporte
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="horario"></param>
        /// <param name="fechaHoy"></param>
        /// <returns></returns>
        private IList<ReporteLectorComederosInfo> ObtenerReporteLectorComederos(int organizacionId, int horario, DateTime? fechaHoy)
        {
            IList<ReporteLectorComederosInfo> resultadoInfo;
            try
            {
                var reporteLectorComederosPl = new ReporteLectorComederosPL();
                resultadoInfo = reporteLectorComederosPl.ObtenerReporteLectorComederos(organizacionId, horario, fechaHoy);
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultadoInfo;
        }


        /// <summary>
        /// Limpia los datos de la pantalla
        /// </summary>
        private void LimpiarCampos()
        {
            cmbOrganizacion.SelectedIndex = 0;
            DtpFecha.SelectedDate = DateTime.Now.Date;
            cmbHorarios.SelectedIndex = 0;
            var organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var organizacionPl = new OrganizacionPL();

            var organizacion = organizacionPl.ObtenerPorID(organizacionId);
            var tipoOrganizacion = organizacion.TipoOrganizacion.TipoOrganizacionID;
            if (tipoOrganizacion == TipoOrganizacion.Corporativo.GetHashCode())
            {
                cmbOrganizacion.IsEnabled = true;
                cmbOrganizacion.SelectedIndex = 0;
                cmbOrganizacion.Focus();
                btnGenerar.IsEnabled = false;
            }
            else
            {
                btnGenerar.IsEnabled = true;
            }
            btnGenerar.Focus();
        }

        #endregion Metodos

        #region Eventos
      
        /// <summary>
        /// Handler limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        /// <summary>
        /// Handler Control loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();

            cmbHorarios.SelectedIndex = 0;
            DtpFecha.SelectedDate = DateTime.Now.Date;
            DtpFecha.IsEnabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Generar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrganizacion_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnGenerar.IsEnabled = ((OrganizacionInfo)cmbOrganizacion.SelectedItem).OrganizacionID != 0;
        }

        /// <summary>
        /// Dtp Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFecha_Loaded(object sender, RoutedEventArgs e)
        {
            cmbHorarios.SelectedIndex = 0;
            DtpFecha.SelectedDate = DateTime.Now.Date;
        }
        #endregion Eventos


    }
}
