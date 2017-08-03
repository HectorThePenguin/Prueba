using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReportePaseaProceso.xaml
    /// </summary>
    public partial class ReportePaseaProceso
    {
        #region Propiedades
        
        /// <summary>
        /// Propiedad que contiene el DataContext de la pantalla
        /// </summary>
        private FiltroFechasInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroFechasInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ReportePaseaProceso()
        {
            InitializeComponent();
          
        }

       #endregion

        #region Metodos
        /// <summary>
        /// Inicializar el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroFechasInfo
            {
                OrgazizacionId = 0,
                FechaInicial = null,
                FechaFinal = null,
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

                var resultadoInfo = ObtenerReportePaseaProceso(organizacionId); 

                if (resultadoInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReportePaseaProceso_MensajeReporteSinDatos,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }


                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                var titulo = Properties.Resources.Reporte_Pase_a_Proceso_Titulo;

                var nombreOrganizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + organizacion.Division + ")";
                var fecha = DateTime.Now;
                
                foreach (var item in resultadoInfo)
                {
                   
                    item.Organizacion = nombreOrganizacion;
                    item.Titulo = titulo;
                    item.Fecha = fecha;

                }
                

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReportePaseaProceso.rpt");
                documento.Load(reporte);


                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();


                var forma = new ReportViewer(documento, Properties.Resources.Reporte_Pase_a_Proceso_Titulo);
                forma.MostrarReporte();
                forma.Show();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReportePaseaProceso_FalloCargarReporte, MessageBoxButton.OK,
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

                var organizacionesPl = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPl.ObtenerTipoGanaderas();
                if (usuarioCorporativo)
                {

                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReportePaseaProceso_cmbSeleccione,
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
                                  Properties.Resources.ReportePaseaProceso_ErrorCargarCombos,
                                  MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Metodo para obtener los datos del informe
        /// </summary>
        /// <param name="organizacionId"></param>
       
        /// <returns></returns>
        private IList<ReportePaseaProcesoInfo> ObtenerReportePaseaProceso(int organizacionId)
        {
            try
            {
                var reportePl = new ReportePaseaProcesoPL();
                return reportePl.ObtenerReportePaseaProceso(organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos()
        {
            cmbOrganizacion.SelectedIndex = 0;
            cmbOrganizacion.Focus();
        }

        /// <summary>
        /// Validar el Formulario
        /// </summary>
        /// <returns></returns>
        private bool ValidarFormulario()
        {
            bool resultado = true;

            var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

            if (organizacion == null || organizacion.OrganizacionID == 0)
            {
                resultado = false;
            }
            btnGenerar.IsEnabled = resultado;
            return resultado;
        }

        
        #endregion Metodos

        #region Eventos


        /// <summary>
        /// Evento del boton limpiar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
        /// <summary>
        /// Evento de carga del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            CargaOrganizaciones();
        }
        /// <summary>
        /// Evento del boton Generar del reporte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
                Generar();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrganizacion_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ValidarFormulario();
        }
        
        #endregion Eventos


    }
}
