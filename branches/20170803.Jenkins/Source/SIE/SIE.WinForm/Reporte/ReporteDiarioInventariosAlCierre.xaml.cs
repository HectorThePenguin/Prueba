using System;
using System.Collections.Generic;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Windows.Controls;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles;
using SIE.Services.Info.Enums;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteDiarioInventariosAlCierre.xaml
    /// </summary>
    public partial class ReporteDiarioInventariosAlCierre
    {
        public ReporteDiarioInventariosAlCierre()
        {
            InitializeComponent();
            DtpFechaInicial.IsEnabled = false;
        }

        #region Propiedades
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
        /// Inicializa contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroFechasInfo
            {
                FechaInicial = null,
                Valido = false
            };
        }
        #endregion Propiedades

        #region Metodos
        /// <summary>
        /// Handled Window Salida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarOrganizaciones();
        }

        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos()
        {
            DtpFechaInicial.IsEnabled = false;

            this.cmbOrganizacion.SelectedIndex = 0;
            var org = (OrganizacionInfo)cmbOrganizacion.SelectedItem;
            if (org != null && org.OrganizacionID > 0)
            {
                btnGenerar.IsEnabled = true;
                Contexto.Valido = true;
            }
            else
            {
                btnGenerar.IsEnabled = false;
                Contexto.Valido = false;
            }
        }

        /// <summary>
        /// Cancela las selecciones actuales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
           LimpiarCampos();
        }

        /// <summary>
        /// Carga las organizaciones
        /// </summary>
        private void CargarOrganizaciones()
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
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ReporteMedicamentosAplicadosSanidad_ErrorCargarOrganizaciones,
                    MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Handler del boton generar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Generar();
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Generar()
        {
            Consultar();
        }

        /// <summary>
        /// Método para consultar Reporte 
        /// </summary>
        /// <returns></returns>
        private void Consultar()
        {
            try
            {
                var orgCombo = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(orgCombo.OrganizacionID);

                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var reportediarioinventarioalcierreBL = new ReporteDiarioInventarioAlCierreBL();

                ReporteDiarioInventarioAlCierreBL orpt = new ReporteDiarioInventarioAlCierreBL();


                List<ReporteDiarioInventarioAlCierreInfo> resultadoInfo =
                  reportediarioinventarioalcierreBL.GenerarReporte(organizacion.OrganizacionID, DateTime.Now);


                if (resultadoInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteOperacionSanidad_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCampos();
                    return;
                }
                else
                {

                    string division = organizacion.Division;

                    string fechas = DtpFechaInicial.SelectedDate.Value.ToShortDateString();

                    foreach (var dato in resultadoInfo)
                    {
                        dato.Titulo = Properties.Resources.ReporteInventarioCierre_TituloReporte;
                        dato.Fecha = DateTime.Today;
                        dato.Organizacion = string.Format(Properties.Resources.Reporte_NombreEmpresa, division);
                    }


                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteDiarioInventarioAlCierre.rpt");
                    documento.Load(reporte);


                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(resultadoInfo);
                    documento.Refresh();

                    var forma = new ReportViewer(documento, "");
                    forma.MostrarReporte();
                    forma.Show();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReporteMedicamentosAplicados_FalloCargarReporte, MessageBoxButton.OK,
                                                      MessageImage.Error);
            }
        }

        /// <summary>
        /// Handler del combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionChaged(object sender, SelectionChangedEventArgs e)
        {       
            var org = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
            if (org != null && org.OrganizacionID > 0 )
            {
                btnGenerar.IsEnabled = true;
                Contexto.Valido = true;
            }
            else
            {
                btnGenerar.IsEnabled = false;
                Contexto.Valido = false;
            }
        
        }
        
        #endregion

    }
}
