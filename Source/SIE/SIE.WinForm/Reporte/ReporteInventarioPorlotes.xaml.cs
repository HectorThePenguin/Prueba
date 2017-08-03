using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Linq;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
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
    /// Lógica de interacción para ReporteInventarioPorlotes.xaml
    /// </summary>
    public partial class ReporteInventarioPorlotes
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

        public ReporteInventarioPorlotes()
        {
            InitializeComponent();
            CargaFamilias();
            DtpFecha.SelectedDate = DateTime.Now;
            InicializaContexto();
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
                FechaFinal =null  , 
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
                int familiaID = ((FamiliaInfo) cmbFamilia.SelectedItem).FamiliaID;
                var organizacionInfo = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                if (organizacionInfo == null || organizacionInfo.OrganizacionID == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.ReporteInventarioPorlotes_MsgSelecioneOrganizacion,
                                    MessageBoxButton.OK, MessageImage.Warning);
                    cmbOrganizacion.Focus();
                    return;
                }
                //int organizacionId = Auxiliar.AuxConfiguracion.ObtenerOrganizacionUsuario();
                int organizacionId = organizacionInfo.OrganizacionID;


                int tipoAlmacenID = 0;

                if (familiaID == FamiliasEnum.MateriaPrimas.GetHashCode())
                {
                    tipoAlmacenID = rdbInventarioPropio.IsChecked == true
                                        ? 1
                                        : 0;
                }

                var fecha = DtpFecha.SelectedDate.Value;
                var resultadoInfo = ObtenerReporteInventarioPorlotes(organizacionId, familiaID, tipoAlmacenID, fecha);
                if (resultadoInfo == null || resultadoInfo.Count == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteInventarioPorlotes_MensajeReporteSinDatos,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }

                var organizacionPl = new OrganizacionPL();
                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                string titulo = Properties.Resources.ReporteInventarioPorlotes_Titulo;
                var nombreOrganizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" +
                                         organizacion.Division + ")";

                foreach (var item in resultadoInfo)
                {
                    item.Organizacion = nombreOrganizacion;
                    item.Titulo = titulo;
                    item.Fecha = fecha;
                }

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory,
                                            "\\Reporte\\RptReporteInventarioPorLotes.rpt");
                documento.Load(reporte);

                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();

                var forma = new ReportViewer(documento, Properties.Resources.ReporteInventarioPorlotes_Titulo);
                forma.MostrarReporte();
                forma.Show();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteMedicamentosAplicados_FalloCargarReporte,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Método para cargar las organizaciones
        /// </summary>
        /// <returns></returns>
        private void CargaFamilias()
        {
            try
            {
                var familiaPL = new FamiliaPL();
                IList<FamiliaInfo> listaFamilias = familiaPL.ObtenerTodos(EstatusEnum.Activo);

                if (listaFamilias != null)
                {
                    listaFamilias =
                        listaFamilias.Where(
                            f =>
                            f.FamiliaID == FamiliasEnum.MateriaPrimas.GetHashCode() ||
                            f.FamiliaID == FamiliasEnum.Premezclas.GetHashCode()).ToList();
                    var Seleccione = new FamiliaInfo
                                         {
                                             FamiliaID = 0,
                                             Descripcion = Properties.Resources.ReporteInventarioPorlotes_cmbSeleccione
                                         };

                    listaFamilias.Insert(0, Seleccione);
                    cmbFamilia.ItemsSource = listaFamilias;
                    cmbFamilia.SelectedItem = Seleccione;
                }
                else
                {
                        throw  new Exception("Error al cargar las Familias");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteInventarioPorlotes_ErrorCargarCombos,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }


        /// <summary>
        /// Genera los datos del reporte
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="familiaId"></param>
        /// <param name="tipoAlmacenId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private IList<ReporteInventarioPorLotesInfo> ObtenerReporteInventarioPorlotes(int organizacionId, int familiaId, int tipoAlmacenId, DateTime fecha)
        {
            try
            {
                var reportePl = new ReporteInventarioPorlotesPL();
                var resultadoInfo = reportePl.ObtenerReporteInventarioPorlotes(organizacionId, familiaId, tipoAlmacenId, fecha);
                return resultadoInfo;
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
            cmbFamilia.SelectedIndex = 0;
            btnGenerar.IsEnabled = false;
            cmbFamilia.Focus();
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
            Generar();
        }
        
        /// <summary>
        /// Evento disparado al cambiar la familia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFamilia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFamilia.SelectedIndex == 0)
            {
                btnGenerar.IsEnabled = false;
                spRadiobuttons.Visibility = Visibility.Hidden;
            }
            else
            {
                if (((FamiliaInfo)cmbFamilia.SelectedItem).FamiliaID == FamiliasEnum.MateriaPrimas.GetHashCode())
                {
                    spRadiobuttons.Visibility = Visibility.Visible;
                    rdbInventarioPropio.IsChecked = true;
                }
                else
                    spRadiobuttons.Visibility = Visibility.Hidden;
                btnGenerar.IsEnabled = true;
            }
        }

        private void DtpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime fechaHoy = DateTime.Now;
            if (DtpFecha.SelectedDate.HasValue)
            {
                fechaHoy = new DateTime(DtpFecha.SelectedDate.Value.Year, DtpFecha.SelectedDate.Value.Month,
                                        DtpFecha.SelectedDate.Value.Day);
            }
            if (fechaHoy > DateTime.Today)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteInventarioPorlotes_FechaMayorActual,
                                  MessageBoxButton.OK, MessageImage.Stop);

                DtpFecha.SelectedDate = DateTime.Now;
            }
        }

        #endregion Eventos
    }
}
