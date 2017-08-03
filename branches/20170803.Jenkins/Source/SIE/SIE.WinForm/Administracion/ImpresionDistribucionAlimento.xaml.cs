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
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Base.Infos;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ImpresionDistribucionAlimento.xaml
    /// </summary>
    public partial class ImpresionDistribucionAlimento
    {
        private IList<ImpresionDistribucionAlimentoModel> impresionDistribucionAlimento;
        private IList<CorralInfo> corrales;
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private FiltroImpresionDistribucionAlimento Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroImpresionDistribucionAlimento)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto = new FiltroImpresionDistribucionAlimento
                {
                    Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID = Auxiliar.AuxConfiguracion.ObtenerOrganizacionUsuario()
                        },
                    TipoServicio = new TipoServicioInfo()
                };
        }

        public ImpresionDistribucionAlimento()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            InicializaContexto();
            ObtenerCorrales();
        }

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            InicializaContexto();
            cmbTipoServicio.ItemsSource = null;
            gridDatos.ItemsSource = null;
        }

        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            if (impresionDistribucionAlimento == null || !impresionDistribucionAlimento.Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.ImpresionDistribucionAlimento_FechaInvalida, MessageBoxButton.OK,
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
                    var elemento = boton.CommandParameter as ImpresionDistribucionAlimentoModel;
                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
                                                "\\Administracion\\Impresiones\\rptImpresionCheckListDistribucionDeAlimento.rpt");
                    documento.Load(reporte);
                    documento.DataSourceConnections.Clear();

                    List<ImpresionDistribucionAlimentoModel> datosReporte =
                        impresionDistribucionAlimento.Where(
                            servicio => servicio.TipoServicio.TipoServicioId == elemento.TipoServicio.TipoServicioId).
                            ToList();
                    int totalCorrales = corrales.Count;
                    datosReporte =
                        datosReporte.GroupBy(corral => corral.Corral).Select(
                            grupo => new ImpresionDistribucionAlimentoModel
                                         {
                                             Corral = grupo.Key,
                                             Camion = grupo.Select(cam => cam.Camion).FirstOrDefault(),
                                             Descripcion = grupo.Select(desc => desc.Descripcion).FirstOrDefault(),
                                             DescripcionCorta =
                                                 grupo.Select(desc => desc.DescripcionCorta).FirstOrDefault(),
                                             Estatus = grupo.Select(est => est.Estatus).FirstOrDefault(),
                                             Fecha = grupo.Select(fecha => fecha.Fecha).FirstOrDefault(),
                                             Lector = grupo.Select(lect => lect.Lector).FirstOrDefault(),
                                             TipoServicio = grupo.Select(tipo => tipo.TipoServicio).FirstOrDefault(),
                                             TotalCorrales = totalCorrales
                                         }).ToList();

                    int totalOK =
                        datosReporte.Count(
                            desc => desc.DescripcionCorta.Trim().Equals("OK", StringComparison.InvariantCultureIgnoreCase));
                    int totalM =
                        datosReporte.Count(
                            desc => desc.DescripcionCorta.Trim().Equals("M", StringComparison.InvariantCultureIgnoreCase));
                    int totalE =
                        datosReporte.Count(
                            desc =>
                            desc.DescripcionCorta.Trim().Equals("E", StringComparison.InvariantCultureIgnoreCase));
                    int totalNA =
                        datosReporte.Count(
                            desc =>
                            desc.DescripcionCorta.Trim().Equals("N/A", StringComparison.InvariantCultureIgnoreCase));
                    int totalS =
                        datosReporte.Count(
                            desc =>
                            desc.DescripcionCorta.Trim().Equals("S", StringComparison.InvariantCultureIgnoreCase));
                    int totalD =
                        datosReporte.Count(
                            desc =>
                            desc.DescripcionCorta.Trim().Equals("D", StringComparison.InvariantCultureIgnoreCase));
                    int totalB =
                        datosReporte.Count(
                            desc =>
                            desc.DescripcionCorta.Trim().Equals("B", StringComparison.InvariantCultureIgnoreCase));
                    datosReporte.ForEach(datos =>
                                             {
                                                 datos.TotalTipoB = totalB;
                                                 datos.TotalTipoD = totalD;
                                                 datos.TotalTipoE = totalE;
                                                 datos.TotalTipoM = totalM;
                                                 datos.TotalTipoNA = totalNA;
                                                 datos.TotalTipoOK = totalOK;
                                                 datos.TotalTipoS = totalS;
                                             });
                    documento.SetDataSource(datosReporte);
                    documento.Refresh();

                    ExportOptions rptExportOption;
                    var rptFileDestOption = new DiskFileDestinationOptions();
                    var rptFormatOption = new PdfRtfWordFormatOptions();
                    string reportFileName = string.Format("{0}_{1}_{2}.pdf", @"C:\CheckListDistribucionAlimento",
                                                          Contexto.Fecha.ToString("ddMMyyyy"),
                                                          elemento.TipoServicio.Descripcion);
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
                List<ImpresionDistribucionAlimentoModel> entradasAgrupadas =
                    (from servicio in impresionDistribucionAlimento
                     group servicio by servicio.TipoServicio.TipoServicioId into agrup 
                     let impresionDistribucionAlimentoModel = agrup.FirstOrDefault() 
                     where impresionDistribucionAlimentoModel != null 
                     select new ImpresionDistribucionAlimentoModel
                         {
                             Camion = impresionDistribucionAlimentoModel.Camion,
                             Corral = impresionDistribucionAlimentoModel.Corral,
                             Descripcion = impresionDistribucionAlimentoModel.Descripcion,
                             DescripcionCorta = impresionDistribucionAlimentoModel.DescripcionCorta,
                             Estatus = impresionDistribucionAlimentoModel.Estatus,
                             Fecha = impresionDistribucionAlimentoModel.Fecha,
                             Lector = impresionDistribucionAlimentoModel.Lector,
                             TipoServicio = impresionDistribucionAlimentoModel.TipoServicio
                         }).ToList();

                if (Contexto.TipoServicio.TipoServicioId == 0)
                {
                    gridDatos.ItemsSource = entradasAgrupadas.Where(impre => !string.IsNullOrEmpty(impre.Descripcion));
                }
                else
                {
                    List<ImpresionDistribucionAlimentoModel> entradasFiltradas =
                        entradasAgrupadas.Where(
                            impre => impre.TipoServicio.TipoServicioId == Contexto.TipoServicio.TipoServicioId && !string.IsNullOrEmpty(impre.Descripcion)).ToList();

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
                                  Properties.Resources.ImpresionDistribucionAlimento_ErrorConsultarDistribuciones, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void ObtenerDistribucionAlimento()
        {
            try
            {
                var loteDistribucionAlimentoBL = new LoteDistribucionAlimentoBL();
                if (!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.ImpresionDistribucionAlimento_FechaInvalida, MessageBoxButton.OK,
                              MessageImage.Warning);
                    return;
                }
                Contexto.Fecha = dtpFecha.SelectedDate.Value;
                impresionDistribucionAlimento =
                    loteDistribucionAlimentoBL.ObtenerImpresionDistribucionAlimento(Contexto);
                if (impresionDistribucionAlimento != null && impresionDistribucionAlimento.Any())
                {
                    impresionDistribucionAlimento.ToList().ForEach(impre =>
                        {
                            impre.Descripcion =
                                string.Format(Properties.Resources.ImpresionDistribucionAlimento_NomenclaturaFormato,
                                              impre.TipoServicio.Descripcion);
                        });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionDistribucionAlimento_ErrorConsultarDistribuciones, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void DtpFecha_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var checkListRoladoBL = new CheckListRoladoraBL();
            checkListRoladoBL.ObtenerDatosImpresionCheckListRoladora(dtpFecha.SelectedDate.Value, 1,
                                                                     Contexto.Organizacion.OrganizacionID);
            //if (!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue)
            //{
            //    return;
            //}
            //if (dtpFecha.SelectedDate.Value > DateTime.Now.Date)
            //{
            //    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
            //                    Properties.Resources.ImpresionDistribucionAlimento_FechaMayor, MessageBoxButton.OK,
            //                    MessageImage.Warning);
            //    dtpFecha.SelectedDate = null;
            //    return;
            //}
            //cmbTipoServicio.ItemsSource = null;
            //gridDatos.ItemsSource = null;
            //ObtenerDistribucionAlimento();
            //if (impresionDistribucionAlimento == null || !impresionDistribucionAlimento.Any())
            //{
            //    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
            //                   Properties.Resources.ImpresionDistribucionAlimento_SinEntradas, MessageBoxButton.OK,
            //                   MessageImage.Warning);
            //    return;
            //}

            //var tiposServiciosAgrupados = (from serv in impresionDistribucionAlimento
            //                               group serv by
            //                                   new {serv.TipoServicio.TipoServicioId, serv.TipoServicio.Descripcion}
            //                               into agru
            //                               select new TipoServicioInfo
            //                                   {
            //                                       TipoServicioId = agru.Key.TipoServicioId,
            //                                       Descripcion = agru.Key.Descripcion
            //                                   }).OrderBy(serv => serv.Descripcion).ToList();

            //var tipoServicioTodos = new TipoServicioInfo
            //    {
            //        TipoServicioId = 0,
            //        Descripcion = Properties.Resources.cbo_Seleccionar
            //    };

            //tiposServiciosAgrupados.Insert(0, tipoServicioTodos);
            //cmbTipoServicio.ItemsSource = tiposServiciosAgrupados;
            //cmbTipoServicio.SelectedIndex = 0;
        }

        private void ObtenerCorrales()
        {
            try
            {
                var corralPL = new CorralPL();
                var pagina = new PaginacionInfo
                                 {
                                     Inicio = 1,
                                     Limite = 100000
                                 };
                var corral = new CorralInfo
                                 {
                                     Organizacion = new OrganizacionInfo
                                                        {
                                                            OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                                                        },
                                     TipoCorral = new TipoCorralInfo(),
                                     Activo = EstatusEnum.Activo
                                 };
                ResultadoInfo<CorralInfo> resultadoCorrales = corralPL.ObtenerPorPagina(pagina, corral);
                if (resultadoCorrales != null && resultadoCorrales.Lista != null
                    && resultadoCorrales.Lista.Any())
                {
                    corrales = resultadoCorrales.Lista;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionDistribucionAlimento_ErrorConsultarDistribuciones,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }
    }
}
