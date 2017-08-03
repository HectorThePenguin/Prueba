using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Diagnostics;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ImpresionCalidadMezclado.xaml
    /// </summary>
    public partial class ImpresionCalidadMezclado
    {
        private IList<ImpresionCalidadMezcladoModel> impresionTarjeta;
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FiltroImpresionCalidadMezclado Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroImpresionCalidadMezclado)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto = new FiltroImpresionCalidadMezclado
                {
                    Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID = Auxiliar.AuxConfiguracion.ObtenerOrganizacionUsuario()
                        }
                };
        }

        public ImpresionCalidadMezclado()
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
            gridDatos.ItemsSource = null;
        }

        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            if (!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.ImpresionCalidadMezclado_FechaInvalida, MessageBoxButton.OK,
                               MessageImage.Warning);
                return;
            }
            if (dtpFecha.SelectedDate.Value > DateTime.Now.Date)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ImpresionCalidadMezclado_FechaMayor, MessageBoxButton.OK,
                                MessageImage.Warning);
                dtpFecha.SelectedDate = null;
                return;
            }
            gridDatos.ItemsSource = null;
            ObtenerCalidadMezclado();
            if (impresionTarjeta == null || !impresionTarjeta.Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.ImpresionCalidadMezclado_SinMezclas, MessageBoxButton.OK,
                               MessageImage.Warning);
            }
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var boton = e.Source as Button;
                if (boton != null)
                {
                    var elemento = boton.CommandParameter as ImpresionCalidadMezcladoModel;
                    var calidad = new CalidadMezcladoFormulasAlimentoPL();
                    var listaAuxiliar = new List<ImpresionCalidadMezcladoModel> {elemento};
                    calidad.CalculosDetalle(listaAuxiliar);
                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
                                                "\\Administracion\\Impresiones\\rptEf.MezRotomix 2.rpt");
                    documento.Load(reporte);

                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(listaAuxiliar);
                    documento.Refresh();

                    ExportOptions rptExportOption;
                    var rptFileDestOption = new DiskFileDestinationOptions();
                    var rptFormatOption = new PdfRtfWordFormatOptions();
                    string reportFileName = string.Format("{0}_{1}_{2}.pdf", @"C:\CheckListCalidadMezclado",
                                                          elemento.Tecnica,
                                                          Contexto.Fecha.ToString("ddMMyyyy"));
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
                                  Properties.Resources.ImpresionSupervisionTecnicasDeteccion_ErrorImpresion,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void ObtenerCalidadMezclado()
        {
            try
            {
                
                var calidadMezcladoFormulasAlimentoPL = new CalidadMezcladoFormulasAlimentoPL();
                if(!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue )
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.ImpresionCalidadMezclado_FechaInvalida, MessageBoxButton.OK,
                              MessageImage.Warning);
                    return;
                }
                Contexto.Fecha = dtpFecha.SelectedDate.Value;
                impresionTarjeta =
                    calidadMezcladoFormulasAlimentoPL.ObtenerImpresionCalidadMezclado(Contexto);
                if (impresionTarjeta != null && impresionTarjeta.Any())
                {
                    impresionTarjeta.ToList().ForEach(impre =>
                                                          {
                                                              impre.Descripcion =
                                                                  string.Format(
                                                                      Properties.Resources.
                                                                          ImpresionCalidadMezclado_NomenclaturaFormato,
                                                                      impre.Tecnica,
                                                                      impre.FechaPremezcla.ToString("dd/MM/yyyy"));
                                                          });
                }
                gridDatos.ItemsSource = impresionTarjeta;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionCalidadMezclado_ErrorConsultarMezclas, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }
    }
}
