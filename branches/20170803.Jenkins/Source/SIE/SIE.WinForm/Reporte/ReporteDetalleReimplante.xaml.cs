using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Filtros;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para RecepcionReporteInventario.xaml
    /// </summary>
    public partial class ReporteDetalleReimplante
    {

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ReporteDetalleReimplante()
        {
            InitializeComponent();
            CargaComboTipoProceso();
            btnGenerar.IsEnabled = false;
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
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Evento para un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void cmbTipoProceso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaControles();
        }
        
        private void DtpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaControles();
        }


        #endregion

        #region Métodos
        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos()
        {
            btnGenerar.IsEnabled = false;
            cmbTipoProceso.IsEnabled = true;
            DtpFecha.IsEnabled = true;
            cmbTipoProceso.SelectedIndex = 0;
            DtpFecha.SelectedDate = null;
            cmbTipoProceso.Focus();
        }

        /// <summary>
        /// Método para validar los controles de la pantalla.
        /// </summary>
        /// <returns></returns>
        private bool ValidaBuscar(bool mostrar)
        {
            try
            {
                bool resultado = true;
                string mensaje = string.Empty;

                DateTime fechaMinima = DateTime.Now.AddDays(-90);
                DateTime fechaSeleccionada = DateTime.Now;

                try
                {
                    fechaSeleccionada = DtpFecha.SelectedDate.Value;
                }
                catch (Exception)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ReporteDetalleReimplante_ValidacionFechaInvalida;
                    DtpFecha.Focus();
                }

                if (fechaSeleccionada != null)
                {
                    if (fechaSeleccionada > DateTime.Today)
                    {
                        resultado = false;
                        mensaje = Properties.Resources.ReporteDetalleReimplante_ValidacionFechaInvalida;
                        DtpFecha.Focus();
                    }
                    else if (fechaSeleccionada < fechaMinima)
                    {
                        resultado = false;
                        mensaje = Properties.Resources.ReporteDetalleReimplante_ValidacionFechaFueraRango;
                        DtpFecha.Focus();
                    }
                }
                if (mostrar && !string.IsNullOrWhiteSpace(mensaje))
                {
                    resultado = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            if (ValidaBuscar(true))
            {
                ObtenerReporte();
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerReporte()
        {
            try
            {
                int organizacionID = 0;
                var organizacionvr = (OrganizacionInfo)cmbTipoProceso.SelectedItem;
                if (organizacionvr != null)
                {
                    organizacionID = organizacionvr.OrganizacionID;
                }

                DateTime fecha = DtpFecha.SelectedDate.Value;

                DataTable resultadoInfo = ObtenerReporteDetalleReimplante(organizacionID, fecha);

                if (resultadoInfo != null && resultadoInfo.Rows.Count > 0)
                {

                    resultadoInfo = AgregarEncabezado(resultadoInfo, fecha);

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteDetalleReimplante.rpt");
                    documento.Load(reporte);


                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(resultadoInfo);
                    documento.Refresh();

                    btnGenerar.IsEnabled = false;
                    DtpFecha.IsEnabled = false;
                    cmbTipoProceso.IsEnabled = false;

                    var forma = new ReportViewer(documento, Properties.Resources.ReporteDetalleReimplante_TituloReporte);
                    forma.MostrarReporte();
                    forma.Show();

                }
                else
                {
                    //gridDatos.ItemsSource = new List<ReporteInventarioInfo>();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteDetalleReimplante_ValidacionSinDatos,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteInventario_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteInventario_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private DataTable AgregarEncabezado(DataTable resultadoInfo, DateTime fecha)
        {

            
            var organizacionPl = new OrganizacionPL();
            int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
            
            string Titulo = Properties.Resources.ReporteDetalleReimplante_TituloReporte;
            var nombreOrganizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + organizacion.Division + ")";

            foreach (DataRow item in resultadoInfo.Rows)
            {
                item.BeginEdit();
                item["Organizacion"] = nombreOrganizacion;
                item["Titulo"] = Titulo;
                item.AcceptChanges();
                item.EndEdit();
            }

            return resultadoInfo;

        }

        /// <summary>
        /// Método obtener el reporte ejecutivo
        /// </summary>
        /// <returns></returns>
        private DataTable ObtenerReporteDetalleReimplante(int organizacionID, DateTime fecha)
        {
            try
            {

                var reporteDetalleReimplantePL = new ReporteDetalleReimplantePL();

               
                DataTable resultadoInfo = reporteDetalleReimplantePL.GenerarReporteDetalleReimplante(organizacionID,
                                                                                                         fecha);
                return resultadoInfo;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Carga los Roles
        /// </summary>
        private void CargaComboTipoProceso()
        {
            try
            {
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
               
                int tipoOrganizacion = organizacion.TipoOrganizacion.TipoOrganizacionID;
               
                var organizacionesPL = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPL.ObtenerTipoGanaderas();

                var organizacion0 =
                    new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReporteDetalleReimplante_TextoDefaultCombo,
                        };
                listaorganizaciones.Insert(0, organizacion0);

                cmbTipoProceso.ItemsSource = listaorganizaciones;

                if (tipoOrganizacion == TipoOrganizacion.Corporativo.GetHashCode())
                {
                    cmbTipoProceso.SelectedIndex = 0;
                }
                else
                {
                    var index = 0;
                    foreach (OrganizacionInfo org in cmbTipoProceso.Items)
                    {
                        if (org.OrganizacionID == organizacionId)
                        {
                        
                            cmbTipoProceso.SelectedIndex = index;
                            break;
                        }

                        index++;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteDetalleReimplante_ErrorCargarOrganizaciones,
                                  MessageBoxButton.OK, MessageImage.Error);

            }
        }


        /// <summary>
        /// Bloquear controles
        /// </summary>
        private void BloquearControles()
        {
            cmbTipoProceso.IsEnabled = false;
            DtpFecha.IsEnabled = false;
            spGenerar.IsEnabled = false;
            //spExportar.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Validar que se haya seleccionado la organizacion, y la fecha de implantacion a consultar
        /// </summary>
        private void ValidaControles()
        {

            Boolean Valido = true;

            var organizacionvr = (OrganizacionInfo)cmbTipoProceso.SelectedItem;
            if (organizacionvr != null)
            {
                if (organizacionvr.OrganizacionID == 0)
                {
                    Valido = false;
                }
            }
            else
            {
                Valido = false;
            }

            //Validar Fecha
            if (DtpFecha.SelectedDate == null)
            {
                Valido = false;
            }
            else
            {
                try
                {
                    DateTime Fecha = DtpFecha.SelectedDate.Value;
                }
                catch (Exception)
                {
                    Valido = false;
                }
            }

            btnGenerar.IsEnabled = Valido;


        }
        #endregion

       
      
       
    }
}
