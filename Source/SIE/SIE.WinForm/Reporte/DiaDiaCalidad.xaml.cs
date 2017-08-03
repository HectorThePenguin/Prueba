using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Filtros;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para DiaDiaCalidad.xaml
    /// </summary>
    public partial class DiaDiaCalidad 
    {
        public DiaDiaCalidad()
        {
            InitializeComponent();
            Limpiar();
            ValidaFormulario();
        }

        #region Eventos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerReporte();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaFormulario();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrganizacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaFormulario();
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Cargar combo de organizaciones
        /// </summary>
        private void CargaComboOrganizacion()
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
                    btnGenerar.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteDiaDiaCalidad_ErrorCargarCombos,
                                  MessageBoxButton.OK, MessageImage.Error);

            }
        }
        /// <summary>
        /// Limpiar el formulario
        /// </summary>
        private void Limpiar()
        {
            
            cmbOrganizacion.IsEditable = true;
            DtpFecha.IsEnabled = true;
            try
            {
                var reportePl = new ReporteDiaDiaCalidadPL();
                DtpFecha.SelectedDate = reportePl.ObtenerFechaServidor();
                CargaComboOrganizacion();
                ValidaFormulario();
                var orgCombo = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
                if(orgCombo.OrganizacionID == 0)
                    btnGenerar.IsEnabled = false;

                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

            }
        }
        /// <summary>
        /// Validar el formulario
        /// </summary>
        private void ValidaFormulario()
        {
            Boolean Valido = true;

            if (cmbOrganizacion.SelectedIndex == 0)
                Valido = false;

            if (DtpFecha.SelectedDate == null)
            {
                Valido = false;
            }
            else if (DtpFecha.SelectedDate.Value.Date > DateTime.Now.Date)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteDiaDiaCalidad_FechaMayorActual,
                                  MessageBoxButton.OK, MessageImage.Warning);
                Limpiar();
            }
            btnGenerar.IsEnabled = Valido;
        }
        /// <summary>
        /// Generar y mostrar reporte
        /// </summary>
        private void ObtenerReporte()
        {
            try
            {
                int organizacionID = 0;
                var organizacionvr = (OrganizacionInfo)cmbOrganizacion.SelectedItem;
                if (organizacionvr != null)
                {
                    organizacionID = organizacionvr.OrganizacionID;
                }

                DateTime fecha = DtpFecha.SelectedDate.Value;

                IList<ReporteDiaDiaCalidadInfo> resultadoInfo = ObtenerReporteDiaDiaCalidad(organizacionID, fecha);

                IList<ReporteDiaDiaCalidadAnalisisInfo> resultadoAnalisis = ObtenerReporteDiaDiaCalidadOtros(organizacionID, fecha);

                if (resultadoInfo != null && resultadoInfo.Count > 0)
                {
                    resultadoInfo = AgregarEncabezado(resultadoInfo, fecha);

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteDiaDiaCalidad.rpt");
                    documento.Load(reporte);

                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(resultadoInfo);

                    documento.OpenSubreport("rptDiaDiaCalidadAnalisis").SetDataSource(resultadoAnalisis);
                    documento.Refresh();

                    var forma = new ReportViewer(documento, Properties.Resources.ReporteDiaDiaCalidad_Titulo);
                    forma.MostrarReporte();
                    forma.Show();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteDiaDiaCalidad_MensajeReporteSinDatos,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteDiaDiaCalidad_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteDiaDiaCalidad_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }
        /// <summary>
        /// Obtener datos del reporte
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private IList<ReporteDiaDiaCalidadInfo> ObtenerReporteDiaDiaCalidad(int organizacionID, DateTime fecha)
        {
            try
            {

                var reporteDiaDiaCalidadPL = new ReporteDiaDiaCalidadPL();


                IList<ReporteDiaDiaCalidadInfo> resultadoInfo = reporteDiaDiaCalidadPL.ObtenerReporteDiaDiaCalidad(organizacionID,
                                                                                                         fecha);
                return resultadoInfo;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtener datos del reporte
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private IList<ReporteDiaDiaCalidadAnalisisInfo> ObtenerReporteDiaDiaCalidadOtros(int organizacionID, DateTime fecha)
        {
            try
            {
                var reportePl = new ReporteDiaDiaCalidadPL();
                var resultadoInfo = reportePl.ObtenerReporteDiaDiaCalidadAnalisis(organizacionID, fecha);
                return resultadoInfo;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Agregar al reporte el titulo 
        /// </summary>
        /// <param name="resultadoInfo"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private IList<ReporteDiaDiaCalidadInfo> AgregarEncabezado(IList<ReporteDiaDiaCalidadInfo> resultadoInfo, DateTime fecha)
        {
            var organizacionPl = new OrganizacionPL();
            var orgCombo = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
            var organizacion = organizacionPl.ObtenerPorIdConIva(orgCombo.OrganizacionID);

            var titulo = Properties.Resources.ReporteDiaDiaCalidad_Titulo;
            var nombreOrganizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + organizacion.Division + ")";

            foreach (var dato in resultadoInfo)
            {
                dato.Organizacion = nombreOrganizacion;
                dato.Titulo = titulo;
                dato.Fecha = DtpFecha.SelectedDate.Value;
            }


            return resultadoInfo;

        }


#endregion
    }
}