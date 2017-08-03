using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteCorralesEnfermeria.xaml
    /// </summary>
    public partial class ReporteCorralesEnfermeria
    {

        #region Propiedades

        private FiltroReporteCorralesEnfermeria Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroReporteCorralesEnfermeria)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        //private SKAyuda<EnfermeriaInfo> skAyudaEnfermeria;

        #endregion Propiedades

        #region Constructor
        public ReporteCorralesEnfermeria()
        {
            InitializeComponent();
            InicializaContexto();
            CargarAyudas();
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
            skAyudaEnfermeria.ObjetoNegocio = new EnfermeriaPL();
            LimpiarCampos();
            CargarOrganizaciones();
        }

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            MostrarReporte();
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

        #endregion

        #region Métodos

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroReporteCorralesEnfermeria
            {
                Fecha = DateTime.Today,
                Enfermeria = new EnfermeriaInfo
                    {
                        Descripcion = string.Empty,
                        Organizacion = string.Format("{0}", AuxConfiguracion.ObtenerOrganizacionUsuario()),
                        Activo = EstatusEnum.Activo
                    },
                Valido = true
            };
        }

        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos()
        {
            InicializaContexto();
            skAyudaEnfermeria.AsignarFoco();
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Generar()
        {
            MostrarReporte();
        }


        /// <summary>
        /// Método para mostrar el reporte
        /// </summary>
        /// <returns></returns>
        private void MostrarReporte()
        {
            try
            {
                OrganizacionInfo organizacionSeleccionada = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
                int organizacionId = organizacionSeleccionada.OrganizacionID;

                IList<ReporteCorralesEnfermeriaInfo> resultadoInfo = ObtenerReporteCorralesEnfermeria();

                if (resultadoInfo == null || resultadoInfo.Count == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ReporteCorralesEnfermeria_MsgNoExistenDatosReporte,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCampos();
                    return;
                }

                DateTime fecha = DtpFechaInicial.SelectedDate.HasValue
                                     ? DtpFechaInicial.SelectedDate.Value
                                     : DateTime.Today;


                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Division : String.Empty;

                var encabezado = new ReporteEncabezadoInfo()
                {
                    Titulo = Properties.Resources.ReporteCorralesEnfermeria_TituloReporte,
                    FechaInicio = fecha,
                    Organizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + nombreOrganizacion + ")"
                };

                foreach (var dato in resultadoInfo)
                {
                    dato.Titulo = encabezado.Titulo;
                    dato.Fecha = encabezado.FechaInicio;
                    dato.Organizacion = encabezado.Organizacion;
                }

                var documento = new ReportDocument();
                var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\rptReporteInventarioCorralesEnfermeria.rpt");
                documento.Load(reporte);


                documento.DataSourceConnections.Clear();
                documento.SetDataSource(resultadoInfo);
                documento.Refresh();


                var forma = new ReportViewer(documento, encabezado.Titulo);
                forma.MostrarReporte();
                forma.Show();



            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteCorralesEnfermeria_FalloMostrarReporte,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteCorralesEnfermeria_FalloMostrarReporte,
                                  MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Método obtener el reporte muertes de ganado.
        /// </summary>
        /// <returns></returns>
        private IList<ReporteCorralesEnfermeriaInfo> ObtenerReporteCorralesEnfermeria()
        {
            try
            {
                var reporte = new ReporteCorralesEnfermeriaBL();
                IList<ReporteCorralesEnfermeriaInfo> resultadoInfo = reporte.GenerarConFormato(Contexto);
                return resultadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaEnfermeria();
        }


        /// <summary>
        /// Método para agregar el control ayuda Enfermeria origen
        /// </summary>
        private void AgregarAyudaEnfermeria()
        {
            skAyudaEnfermeria.AyudaLimpia += (sender, args) =>
                                             {
                                                 OrganizacionInfo organizacion =
                                                     (OrganizacionInfo)cmbOrganizacion.SelectedItem;
                                                 if (organizacion != null)
                                                 {
                                                     Contexto.Enfermeria = new EnfermeriaInfo
                                                                           {
                                                                               Descripcion = string.Empty,
                                                                               Organizacion =
                                                                                   string.Format("{0}",
                                                                                       organizacion.OrganizacionID),
                                                                               Activo = EstatusEnum.Activo
                                                                           };
                                                 }
                                             };

            //skAyudaEnfermeria.IsEnabled = false;
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

                var organizacionesPl = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPl.ObtenerTipoGanaderas();
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
                    cmbOrganizacion.SelectedIndex = 0;
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
                    cmbOrganizacion.SelectedIndex = 0;
                    cmbOrganizacion.IsEnabled = false;
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




        #endregion

        private void CmbOrganizacion_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            skAyudaEnfermeria.LimpiarCampos();
        }
    }
}
