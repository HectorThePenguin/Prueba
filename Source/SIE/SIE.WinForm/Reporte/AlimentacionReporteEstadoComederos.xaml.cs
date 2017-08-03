using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.Services.Info.Modelos;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Diagnostics;
using SIE.Services.Info.Reportes;
using CrystalDecisions.CrystalReports.Engine;
using SIE.WinForm.Controles;
using System.Windows.Input;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Interaction logic for AlimentacionReporteEstadoComederos.xaml
    /// </summary>
    public partial class AlimentacionReporteEstadoComederos
    {
        SIE.Services.Servicios.BL.ReporteEstadoComederoBL controlador;

        private SIE.Services.Info.Modelos.AlimentacionEstadoComederoModel Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (SIE.Services.Info.Modelos.AlimentacionEstadoComederoModel)DataContext;
            }
            set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AlimentacionReporteEstadoComederos()
        {
            InitializeComponent();
            controlador = new Services.Servicios.BL.ReporteEstadoComederoBL();
            CargaOrganizaciones();
            InicializaContexto();
            dtFecha.SelectedDate = DateTime.Now;
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// Evento para la generacion del Reporte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            GenerarReporte();
            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Evento para la restauracion de la pantalla a su estado inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrganizacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

            if (organizacion != null && organizacion.OrganizacionID > 0)
            {
                btnGenerar.IsEnabled = true;
            }
            else
            {
                btnGenerar.IsEnabled = false;
            }
        }
        #endregion Eventos

        #region Métodos

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
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new SIE.Services.Info.Modelos.AlimentacionEstadoComederoModel();
            Contexto.OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            Contexto.Division = AuxDivision.ObtenerDivision(Contexto.OrganizacionID);
        }
        private void GenerarReporte()
        {
            try
            {
                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                if (organizacion == null || organizacion.OrganizacionID == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteEstadoComerdero_MsgSelecioneOrganizacion,
                       MessageBoxButton.OK, MessageImage.Warning);
                    cmbOrganizacion.Focus();
                    return;
                }

                string division = Contexto.Division;
                int organizacionID = organizacion.OrganizacionID;
                DateTime fechaReporte = dtFecha.SelectedDate.Value;
                //infos reporte página principal
                List<ReporteEstadoComederoInfo> ListaRptEstadoComedero = new List<ReporteEstadoComederoInfo>();
                ReporteEstadoComederoInfo oRptEstadoComedero = new ReporteEstadoComederoInfo();
                //Infos para el primer sub informe
                List<AlimentacionEstadoComederoReporteModel> detalle = new List<AlimentacionEstadoComederoReporteModel>();
                List<ReporteEstadoComederosDetalleInfo> ListaDetalle = new List<ReporteEstadoComederosDetalleInfo>();
                AlimentacionEstadoComederoModel modelos = new AlimentacionEstadoComederoModel();
                ReporteEstadoComederosDetalleInfo oListaDetalle;
                //Infos para el segundo sub informe
                List<ReporteEstadoComederoCorralesEstadoInfo> ListaCorralesComederos = new List<ReporteEstadoComederoCorralesEstadoInfo>();
                ReporteEstadoComederoCorralesFormulaInfo oCorralesFormula;
                //infos para el tercer subinforme
                ReporteEstadoComederoCorralesEstadoInfo oCorralesComederos;
                AlimentacionEstadoComederoModel modelo = new AlimentacionEstadoComederoModel();
                //Comienza seccion generar datos informe pagina princiapal
                oRptEstadoComedero.Titulo = Properties.Resources.ReporteEstadoComerdero_TituloPorFormula;
                oRptEstadoComedero.RangoFechas = fechaReporte.ToShortDateString();
                oRptEstadoComedero.Organizacion = string.Format(Properties.Resources.Reporte_NombreEmpresa, division);
                ListaRptEstadoComedero.Add(oRptEstadoComedero);
                //Termina seccion generar datos informe pagina principal
                //-->
                //Comienza generar datos para el primer informe
                modelos = controlador.GenerarReporteDetallado(organizacionID);
                foreach (var dato in modelos.DatosReporteComederoInfo)
                {
                    oListaDetalle = new ReporteEstadoComederosDetalleInfo();
                    oListaDetalle.Codigo = dato.Corral;
                    oListaDetalle.Lote = dato.Lote;
                    oListaDetalle.TipoGanado = dato.TipoGanado;
                    oListaDetalle.Cabezas = dato.Cabezas;
                    oListaDetalle.DiasEngorda = dato.DiasEngorda;
                    oListaDetalle.PesoProyectado = dato.PesoProyectado;
                    oListaDetalle.DiasUltimaFormula = dato.DiasUltimaFormula;
                    oListaDetalle.Prom5Dias = dato.Promedio5Dias;
                    oListaDetalle.EstadoComederoID = dato.EstadoComederoHoy;
                    oListaDetalle.AMProgramadoHoy = dato.AlimentacionProgramadaMatutinaHoy;
                    oListaDetalle.FormulaDescAMProgramadoHoy = dato.FormulaProgramadaMatutinaHoy;
                    oListaDetalle.PMProgramadoHoy = dato.AlimentacionProgramadaVespertinaHoy;
                    oListaDetalle.FormulaDescPMProgramadoHoy = dato.FormulaProgramadaVespertinaHoy;
                    oListaDetalle.TotalProgramadoHoy = dato.TotalProgramadoHoy;
                    oListaDetalle.ECRealServidorAyer = dato.EstadoComederoRealAyer;
                    oListaDetalle.AMRealServidorAyer = dato.AlimentacionRealMatutinaAyer;
                    oListaDetalle.FormulaDescAMServidaAyer = dato.FormulaRealMatutinaAyer;
                    oListaDetalle.PMRealServidorAyer = dato.AlimentacionRealVespertinoAyer;
                    oListaDetalle.FormulaDescPMServidaAyer = dato.FormulaRealVespertinoAyer;
                    oListaDetalle.TotalServidoAyer = dato.TotalRealAyer;
                    oListaDetalle.Kilogramos3 = dato.Kilogramos3Dias;
                    oListaDetalle.CxC3 = dato.ConsumoCabeza3Dias;
                    oListaDetalle.EC3 = dato.EstadoComedero3Dias;
                    oListaDetalle.Kilogramos4 = dato.Kilogramos4Dias;
                    oListaDetalle.CxC4 = dato.ConsumoCabeza4Dias;
                    oListaDetalle.EC4 = dato.EstadoComedero4Dias;
                    oListaDetalle.Kilogramos5 = dato.Kilogramos5Dias;
                    oListaDetalle.CxC5 = dato.ConsumoCabeza5Dias;
                    oListaDetalle.EC5 = dato.EstadoComedero5Dias;
                    oListaDetalle.Kilogramos6 = dato.Kilogramos6Dias;
                    oListaDetalle.CxC6 = dato.ConsumoCabeza6Dias;
                    oListaDetalle.EC6 = dato.EstadoComedero6Dias;
                    oListaDetalle.Kilogramos7 = dato.Kilogramos7Dias;
                    oListaDetalle.CxC7 = dato.ConsumoCabeza7Dias;
                    oListaDetalle.EC7 = dato.EstadoComedero7Dias;

                    oListaDetalle.Titulo = Properties.Resources.ReporteEstadoComerdero_TituloPrincipal;
                    oListaDetalle.RangoFechas = modelos.FechaServidorBD.ToShortDateString();
                    oListaDetalle.Organizacion = string.Format(Properties.Resources.Reporte_NombreEmpresa, division);

                    ListaDetalle.Add(oListaDetalle);
                }
                //Termina negerar datos para el primer reporte
                //-->
                //Comienza generar datos para el segundo reporte
                List<ReporteEstadoComederoCorralesFormulaInfo> ListaCorralesFormula =
                  new List<ReporteEstadoComederoCorralesFormulaInfo>();
                modelo = controlador.GenerarSegundoReporte(organizacionID);
                foreach (var cpf in modelo.CorralesPorFormula)
                {
                    oCorralesFormula = new ReporteEstadoComederoCorralesFormulaInfo();

                    oCorralesFormula.corral = cpf.TotalCorrales.ToString();
                    oCorralesFormula.Formula = cpf.FormulaDescripcion;
                    ListaCorralesFormula.Add(oCorralesFormula);
                }
                foreach (var dato in ListaCorralesFormula)
                {
                    dato.Titulo = Properties.Resources.ReporteEstadoComerdero_TituloPorFormula;
                    dato.RangoFechas = DateTime.Now.ToShortDateString();
                    dato.Organizacion = string.Format(Properties.Resources.Reporte_NombreEmpresa, division);
                }
                //Finaliza generar datos para el segundo reporte
                //-->
                //Comienza generar datos para el tercer sub informe
                foreach (var cpec in modelo.CorralesPorEstadoComedero)
                {
                    oCorralesComederos = new ReporteEstadoComederoCorralesEstadoInfo();

                    oCorralesComederos.IdEstadoComedero = cpec.EstadoComederoID;
                    oCorralesComederos.DescripcionComedero = cpec.EstadoComederoDescripcion;
                    oCorralesComederos.Corrales = cpec.TotalCorrales;

                    ListaCorralesComederos.Add(oCorralesComederos);
                }
                foreach (var dato in ListaCorralesComederos)
                {
                    dato.Titulo = Properties.Resources.ReporteEstadoComerdero_TituloPorEstado;
                    dato.RangoFechas = DateTime.Now.ToShortDateString();
                    dato.Organizacion = string.Format(Properties.Resources.Reporte_NombreEmpresa, division);
                }
                //Termina generar datos para el tercer sub informe
               if (ListaDetalle.Count > 0)
                {

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
                        "\\Reporte\\RptReporteEstadoComedero.rpt");
                    documento.Load(reporte);

                    documento.DataSourceConnections.Clear();

                    documento.SetDataSource(ListaRptEstadoComedero);

                    documento.OpenSubreport("RptReporteEstadoComederoDetalle.rpt")
                        .SetDataSource(ListaDetalle);

                    documento.OpenSubreport("RptReportesEstadoComederoCorralesFormula.rpt")
                        .SetDataSource(ListaCorralesFormula);

                    documento.OpenSubreport("RptReportesEstadoComederoCorralesComederos.rpt")
                        .SetDataSource(ListaCorralesComederos);

                    documento.Refresh();

                    var forma = new ReportViewer(documento, Properties.Resources.ReporteEstadoComerdero_TituloPrincipal);
                    forma.MostrarReporte();
                    forma.Show();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ReporteEstadoComerdero_NoExistenDatos,
                        //"No existe información en la fecha mostrada en pantalla. Favor de verificar.",
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ReporteEstadoComerdero_FalloCargarReporte,
                    //"No existe información en la fecha mostrada en pantalla. Favor de verificar.",
                        MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion
    }
}

