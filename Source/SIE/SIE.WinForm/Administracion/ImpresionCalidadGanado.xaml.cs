using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using CrystalDecisions.Shared;
using System.Diagnostics;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ImpresionCalidadGanado.xaml
    /// </summary>
    public partial class ImpresionCalidadGanado
    {
        private List<ImpresionCalidadGanadoModel> impresionTarjeta;
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FiltroImpresionCalidadGanado Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroImpresionCalidadGanado)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto = new FiltroImpresionCalidadGanado
                {
                    Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID = Auxiliar.AuxConfiguracion.ObtenerOrganizacionUsuario()
                        },
                    EntradaGanado = new ImpresionCalidadGanadoModel()
                };
        }

        public ImpresionCalidadGanado()
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
            cmbFolioEntrada.ItemsSource = null;
            gridDatos.ItemsSource = null;
        }

        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            if (impresionTarjeta == null || !impresionTarjeta.Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.ImpresionCalidadGanado_FechaInvalida, MessageBoxButton.OK,
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
                    var elemento = boton.CommandParameter as ImpresionCalidadGanadoModel;
                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
                                                "\\Administracion\\Impresiones\\rptImpresionCalidadDeGanado.rpt");
                    documento.Load(reporte);

                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(new List<ImpresionCalidadGanadoModel> { elemento });
                    documento.Refresh();

                    ExportOptions rptExportOption;
                    var rptFileDestOption = new DiskFileDestinationOptions();
                    var rptFormatOption = new PdfRtfWordFormatOptions();
                    string reportFileName = string.Format("{0}_{1}.pdf", @"C:\ImpresionCalidadGanado",
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
                                  Properties.Resources.ImpresionCalidadGanado_ErrorImpresion,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void CargarEntradasImprimir()
        {
            try
            {
                if (Contexto.EntradaGanado.EntradaGanadoID == 0)
                {
                    gridDatos.ItemsSource = impresionTarjeta.Where(impre => !string.IsNullOrEmpty(impre.Descripcion));
                }
                else
                {
                    List<ImpresionCalidadGanadoModel> entradasFiltradas =
                        impresionTarjeta.Where(
                            impre => impre.EntradaGanadoID == Contexto.EntradaGanado.EntradaGanadoID && !string.IsNullOrEmpty(impre.Descripcion)).ToList();

                    if (entradasFiltradas.Any())
                    {
                        gridDatos.ItemsSource = entradasFiltradas;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionCalidadGanado_ErrorConsultarEntradas, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void ObtenerEntradasGanado()
        {
            try
            {
                var entradaGanadoPL = new EntradaGanadoPL();
                if (!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.ImpresionCalidadGanado_FechaInvalida, MessageBoxButton.OK,
                              MessageImage.Warning);
                    return;
                }
                Contexto.Fecha = dtpFecha.SelectedDate.Value;
                impresionTarjeta =
                    entradaGanadoPL.ObtenerEntradasImpresionCalidadGanado(Contexto);
                if (impresionTarjeta != null && impresionTarjeta.Any())
                {
                    impresionTarjeta.ForEach(impre =>
                        {
                            impre.Descripcion =
                                string.Format(Properties.Resources.ImpresionCalidadGanado_NomenclaturaFormato,
                                              impre.FolioEntrada);
                        });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionCalidadGanado_ErrorConsultarEntradas, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void DtpFecha_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue)
            {
                return;
            }
            if (dtpFecha.SelectedDate.Value > DateTime.Now.Date)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ImpresionCalidadGanado_FechaMayor, MessageBoxButton.OK,
                                MessageImage.Warning);
                dtpFecha.SelectedDate = null;
                return;
            }
            cmbFolioEntrada.ItemsSource = null;
            gridDatos.ItemsSource = null;
            ObtenerEntradasGanado();
            if (impresionTarjeta == null || !impresionTarjeta.Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.ImpresionCalidadGanado_SinEntradas, MessageBoxButton.OK,
                               MessageImage.Warning);
                return;
            }
            var impresionTodos = new ImpresionCalidadGanadoModel
                {
                    FolioEntrada = Properties.Resources.cbo_Seleccionar,
                    EntradaGanadoID = 0
                };
            impresionTarjeta.Insert(0, impresionTodos);
            cmbFolioEntrada.ItemsSource = impresionTarjeta;
            cmbFolioEntrada.SelectedValue = impresionTodos.EntradaGanadoID;
            cmbFolioEntrada.SelectedIndex = 0;
        }
    }

}
