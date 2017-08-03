using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ImpresionSupervisionTecnicaDeteccion.xaml
    /// </summary>
    public partial class ImpresionSupervisionTecnicaDeteccion 
    {
      private List<ImpresionSupervisionDetectoresModel> impresionsupervision;
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FiltroImpresionSupervisionDetectores Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroImpresionSupervisionDetectores)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto = new FiltroImpresionSupervisionDetectores
                {
                    Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID = Auxiliar.AuxConfiguracion.ObtenerOrganizacionUsuario()
                        },
                };
        }

        public ImpresionSupervisionTecnicaDeteccion()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            InicializaContexto();
        }

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            InicializaContexto();
            cmbDetector.ItemsSource = null;
            gridDatos.ItemsSource = null;
        }

        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            if(impresionsupervision == null || !impresionsupervision.Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.ImpresionSupervisionTecnicaDeteccion_FechaInvalida, MessageBoxButton.OK,
                             MessageImage.Warning);
                return;
            }
            CargarEntradasImprimir();
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var boton = e.Source as Button;
                if (boton != null)
                {
                    var elemento = boton.CommandParameter as ImpresionSupervisionDetectoresModel;
                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
                                                "\\Administracion\\Impresiones\\rptSupervisionTecnicadeDetecciondeCorrales.rpt");
                    documento.Load(reporte);

                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(new List<ImpresionSupervisionDetectoresModel> { elemento });
                    documento.Refresh();

                    ExportOptions rptExportOption;
                    var rptFileDestOption = new DiskFileDestinationOptions();
                    var rptFormatOption = new PdfRtfWordFormatOptions();
                    string reportFileName = string.Format("{0}_{1}.pdf", @"C:\SupervisionTecnnicaDeteccionCorrales",
                                                          Contexto.FechaSupervision.ToString("ddMMyyyy"));
                    rptFileDestOption.DiskFileName = reportFileName;
                    rptExportOption = documento.ExportOptions;
                    {
                        rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                        rptExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rptExportOption.ExportDestinationOptions = rptFileDestOption;
                        rptExportOption.ExportFormatOptions = rptFormatOption;
                    }
                    documento.Export();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.Impresion_ReporteGeneradoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    Process.Start(reportFileName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionCalidadGanado_ErrorImpresion,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void CargarEntradasImprimir()
        {
            try
            {
                if(Contexto.SupervisionDetectoresID == 0)
                {
                    gridDatos.ItemsSource = impresionsupervision.Where(impre=> !string.IsNullOrEmpty(impre.Descripcion));
                }
                else
                {
                    List<ImpresionSupervisionDetectoresModel> entradasFiltradas =
                        impresionsupervision.Where(
                            impre => impre.SupervisionDetectoresID == Contexto.SupervisionDetectoresID && !string.IsNullOrEmpty(impre.Descripcion)).ToList();

                    if(entradasFiltradas.Any())
                    {
                        gridDatos.ItemsSource = entradasFiltradas;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionSupervisionTecnicaDeteccion_ErrorConsultarSupervision, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void ObtenerEntradasGanado()
        {
            try
            {
                var supervisionDetectoresBL = new SupervisionDetectoresBL();
                if(!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue )
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.ImpresionSupervisionTecnicaDeteccion_FechaInvalida, MessageBoxButton.OK,
                              MessageImage.Warning);
                    return;
                }
                Contexto.FechaSupervision = dtpFecha.SelectedDate.Value;
                impresionsupervision =
                    supervisionDetectoresBL.ObtenerSupervisionDetectoresImpresion(Contexto);
                if (impresionsupervision != null && impresionsupervision.Any())
                {
                    impresionsupervision.ForEach(impre =>
                        {
                            impre.Descripcion =
                                string.Format(Properties.Resources.ImpresionSupervisionTecnicaDeteccion_NomenclaturaFormato,
                                              impre.Detector);
                        });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionSupervisionTecnicaDeteccion_ErrorConsultarSupervision, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void DtpFecha_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue)
            {
                return;
            }
            if (dtpFecha.SelectedDate.Value > DateTime.Now.Date)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ImpresionSupervisionTecnicaDeteccion_FechaMayor, MessageBoxButton.OK,
                                MessageImage.Warning);
                dtpFecha.SelectedDate = null;
                return;
            }
            cmbDetector.ItemsSource = null;
            gridDatos.ItemsSource = null;
            ObtenerEntradasGanado();
            if(impresionsupervision == null || !impresionsupervision.Any())
            {
                 SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ImpresionSupervisionTecnicaDeteccion_SinSupervisiones, MessageBoxButton.OK,
                                MessageImage.Warning);
                return;
            }
            var impresionTodos = new ImpresionSupervisionDetectoresModel
                {
                    Detector = Properties.Resources.cbo_Seleccionar,
                    SupervisionDetectoresID = 0
                };
            impresionsupervision.Insert(0,impresionTodos);
            cmbDetector.ItemsSource = impresionsupervision;
            cmbDetector.SelectedIndex = 0;
        }
    }
}
