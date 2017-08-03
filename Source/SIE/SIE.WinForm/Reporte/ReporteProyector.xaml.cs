using System;
using System.Collections.Generic;
using System.Windows;
using SAPBusinessObjects.WPF.Viewer;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using CrystalDecisions.CrystalReports.Engine;
using SIE.WinForm.Controles;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteProyector.xaml
    /// </summary>
    public partial class ReporteProyector
    {
        /// <summary>
        /// Constructor por Default
        /// </summary>
        public ReporteProyector()
        {
            InitializeComponent();
            CargaOrganizaciones();
            //organizacionIdLogin = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da Clic en el botón Generar Reporte en Excel
        /// </summary>
        private void btnGenerar_OnClick(object sender, RoutedEventArgs e)
        {
            ObtenerReporte_CR();
        }

        /// <summary>
        /// Metodo que genera la informaciónq que se va a exportar a Excel
        /// </summary>
        private List<ReporteProyectorInfo> ObtenerReporteProyector(int organizacionID)
        {
            var corralPL = new CorralPL();
            return corralPL.ObtenerReporteProyectorComportamiento(organizacionID);
        }

       /// <summary>
       /// Genera el reporte de crystal
       /// </summary>
        private void ObtenerReporte_CR()
        {
            try
            {
                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                if (organizacion == null || organizacion.OrganizacionID == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                   Properties.Resources.ReporteProyector_MsgSelecioneOrganizacion,
                                   MessageBoxButton.OK, MessageImage.Warning);
                    cmbOrganizacion.Focus();
                    return;
                }

                string division = AuxDivision.ObtenerDivision(organizacion.OrganizacionID);

                List<ReporteProyectorInfo> resultadoInfo = ObtenerReporteProyector(organizacion.OrganizacionID);

                if (resultadoInfo != null && resultadoInfo.Count > 0)
                {

                    foreach (var dato in resultadoInfo)
                    {
                        dato.Titulo = Properties.Resources.ReporteProyector_TituloReporte;
                        dato.Organizacion = String.Format(Properties.Resources.Reporte_NombreEmpresa,  division);
                        dato.FechaReporte = DateTime.Now;
                    }

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptProyector.rpt");
                    documento.Load(reporte);


                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(resultadoInfo);
                    documento.Refresh();


                    var forma = new ReportViewer(documento, Properties.Resources.ReporteProyector_TituloReporte);
                    forma.rptReportViewerControl.ToggleSidePanel = Constants.SidePanelKind.None;
                    forma.MostrarReporte();
                    forma.Show();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionReporteEjecutivo_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteProyector_ErrorExcel, MessageBoxButton.OK,
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

    }
}
