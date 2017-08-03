using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using System.Linq;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteConsumoProgramadovsServido.xaml
    /// </summary>
    public partial class ReporteConsumoProgramadovsServido
    {
        #region Propiedades

        /// <summary>
        /// Propiedad que contiene el DataContext de la pantalla
        /// </summary>
        private FiltroReporteDetalleReimplante Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroReporteDetalleReimplante)DataContext;
            }
            set { DataContext = value; }
        }

        public ReporteConsumoProgramadovsServido()
        {
            InitializeComponent();
            CargaOrganizaciones();
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
        /// Generar el reporte de solicitud de procesos
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
                DateTime fecha = DateTime.Now;
                
                var resultadoInfo = ObtenerReporteConsumoProgramadovsServido(organizacionId, fecha);

                if (resultadoInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteConsumoProgramadovsServido_MensajeReporteSinDatos,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCampos(true);
                    return;
                }
              
                
                cmbOrganizacion.IsEnabled = false;

                int organizacionIdu = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();
                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionIdu);
                string titulo = Properties.Resources.ReporteConsumoProgramadovsServido_TituloReporte;
                var nombreOrganizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + organizacion.Division + ")";

                foreach (var item in resultadoInfo)
                {
                    item.Titulo = titulo;
                    item.Organizacion = nombreOrganizacion;
                }


                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteConsumoProgramadovsServido.rpt");
                documento.Load(reporte);

                
                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();


                var forma = new ReportViewer(documento, Properties.Resources.ReporteConsumoProgramadovsServido_Titulo);
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
                                  Properties.Resources.ReporteConsumoProgramadovsServido_ErrorCargarCombos,
                                  MessageBoxButton.OK, MessageImage.Error);

            }

        }

        /// <summary>
        /// Obtener reporte de consumo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private IList<ReporteConsumoProgramadovsServidoInfo> ObtenerReporteConsumoProgramadovsServido(int organizacionId, DateTime fecha)
        {
            IList<ReporteConsumoProgramadovsServidoInfo> resultadoInfo = null;
            try
            {
                var reportePl = new ReporteConsumoProgramadovsServidoPL();
                resultadoInfo = reportePl.ObtenerReporteConsumoProgramadovsServido(organizacionId, fecha);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultadoInfo;
        }

       
        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos(bool cancelar)
        {
            cmbOrganizacion.SelectedIndex = 0;
            DtpFecha.SelectedDate = DateTime.Now.Date;
            int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var organizacionPl = new OrganizacionPL();

            var organizacion = organizacionPl.ObtenerPorID(organizacionId);
            int tipoOrganizacion = organizacion.TipoOrganizacion.TipoOrganizacionID;
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
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos(true);
           
            DtpFecha.SelectedDate = DateTime.Now.Date;
            DtpFecha.IsEnabled = false;
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrganizacion_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (((OrganizacionInfo)cmbOrganizacion.SelectedItem).OrganizacionID != 0)
                btnGenerar.IsEnabled = true;
            else
                btnGenerar.IsEnabled = false;
        }

        /// <summary>
        /// Handler de carga de la fecha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFecha_Loaded(object sender, RoutedEventArgs e)
        {
            DtpFecha.SelectedDate = DateTime.Now;
        }

        #endregion Eventos

       

       
    }
}
