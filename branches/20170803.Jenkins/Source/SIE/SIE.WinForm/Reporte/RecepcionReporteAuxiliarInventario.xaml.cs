using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Linq;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para RecepcionReporteAuxiliarInventario.xaml
    /// </summary>
    public partial class RecepcionReporteAuxiliarInventario
    {
        #region Propiedades

        private CorralReporteAuxiliarInventarioInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CorralReporteAuxiliarInventarioInfo)DataContext;
            }
            set { DataContext = value; }
        }

        //private SKAyuda<CorralInfo> skAyudaCorral;

        private List<GrupoCorralInfo> GruposCorral { get; set; }

        private CorralInfo Corral { get; set; }


        #endregion Propiedades

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public RecepcionReporteAuxiliarInventario()
        {
            InitializeComponent();
            InicializaContexto();
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
            LimpiarCampos(true);
            ObtenerGrupoCorral();
            skAyudaLote.ObjetoNegocio = new LotePL();
            skAyudaLote.AyudaConDatos += skAyudaLote_AyudaConDatos;
            //cmbGrupoCorral.SelectedIndex = 0;
            CargaOrganizaciones();
        }

        private void skAyudaLote_AyudaConDatos(object sender, EventArgs e)
        {
            var lotePL = new LotePL();
            LoteInfo lotePesoCompra = lotePL.ObtenerPesoCompraPorLote(Contexto.LoteInfo);
            if(lotePesoCompra != null)
            {
                DtpFechaInicial.SelectedDate = Contexto.LoteInfo.FechaInicio;
                txtPesoOrigen.Text = lotePesoCompra.PesoCompra.ToString(CultureInfo.InvariantCulture);
            }
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
            LimpiarCampos(true);
        }
        /// <summary>
        /// Se ejecuta al momento de cambiar el item seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbTipoCorral_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int valorSeleccionado = 0; //Convert.ToInt32(cmbGrupoCorral.SelectedValue);
            Corral.TipoCorral.GrupoCorral.GrupoCorralID = valorSeleccionado;
            //skAyudaCorral.Info = Corral;
            Contexto.CorralInfo.TipoCorral = Corral.TipoCorral;
            Contexto.Lote = string.Empty;
            Contexto.Clasificacion = string.Empty;
            Contexto.TipoGanado = string.Empty;

            //InicializaContexto();
            //skAyudaCorral.LimpiarCampos();
            //LimpiarCampos(false);
        }


        #endregion

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CorralReporteAuxiliarInventarioInfo
                           {
                               CorralInfo = new CorralInfo(),
                               LoteInfo = new LoteInfo()
                           };
            Corral = new CorralInfo
                         {
                             TipoCorral = new TipoCorralInfo
                                              {
                                                  TipoCorralID = -1,
                                                  GrupoCorral = new GrupoCorralInfo
                                                                    {
                                                                        GrupoCorralID = -1
                                                                    }
                                              }
                         };

        }

        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos(bool completo)
        {
            InicializaContexto();

            DtpFechaInicial.SelectedDate = null;
            skAyudaLote.LimpiarCampos();

            if (completo)
            {
                //cmbGrupoCorral.SelectedIndex = 0;
                //cmbGrupoCorral.Focus();
                CargaOrganizaciones();
            }
        }

        /// <summary>
        /// Método para validar los controles de la pantalla.
        /// </summary>
        /// <returns></returns>
        private bool ValidaBuscar()
        {
            try
            {
                bool resultado = true;

                if (Contexto.LoteInfo == null || Contexto.LoteInfo.LoteID == 0)
                {
                    MostrarMensajeNoExisteCorral();
                    skAyudaLote.LimpiarCampos();
                    InicializaContexto();
                    skAyudaLote.AsignarFoco();
                    LimpiarCampos(false);
                    resultado = false;
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
            if (ValidaBuscar())
            {
                ObtenerReporteGrid();
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerReporteGrid()
        {
            try
            {

                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorIdConIva(organizacionId);
                var nombreOrganizacion = organizacion != null ? organizacion.Division : String.Empty;
                var encabezado = new ReporteEncabezadoInfo
                {
                    Titulo = Properties.Resources.ReporteAuxiliarDeInventarioCorral_TituloReporte,
                    Organizacion = Properties.Resources.ReportesSukarneAgroindustrial_Titulo + " (" + nombreOrganizacion + ")"
                };

                List<AuxiliarDeInventarioInfo> resultadoInfo = ObtenerReporte();

                if (resultadoInfo != null && resultadoInfo.Count > 0)
                {

                    foreach (var dato in resultadoInfo)
                    {
                        dato.Titulo = encabezado.Titulo;
                        dato.Organizacion = encabezado.Organizacion;
                        dato.FechaInicio = DateTime.Now;
                    }

                    var documento = new ReportDocument();
                    var reporte = String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "\\Reporte\\RptReporteAuxiliarDeInventarioPorCorral.rpt");
                    documento.Load(reporte);


                    documento.DataSourceConnections.Clear();
                    documento.SetDataSource(resultadoInfo);
                    documento.Refresh();


                    var forma = new ReportViewer(documento, encabezado.Titulo);
                    forma.MostrarReporte();
                    forma.Show();


                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteAuxiliarInventario_MsgNoExistenDatosReporte, MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteAuxiliarInventario_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteAuxiliarInventario_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Método obtener el reporte ejecutivo
        /// </summary>
        /// <returns></returns>
        private List<AuxiliarDeInventarioInfo> ObtenerReporte()
        {
            try
            {
                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                var reporteAuxiliarInventarioPL = new ReporteAuxilizarInventarioPL();
                //int corralId = Contexto.CorralID;
                int loteID = Contexto.LoteInfo.LoteID;

                var grupoCorral = (GrupoCorralEnum)Contexto.CorralInfo.GrupoCorral;// (GrupoCorralEnum)cmbGrupoCorral.SelectedValue;
                List<AuxiliarDeInventarioInfo> resultadoInfo = reporteAuxiliarInventarioPL.ObtenerDatosReporteAuxiliarInventario(loteID, grupoCorral, organizacion.OrganizacionID);
                return resultadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeNoExisteCorral()
        {
            string mensaje = Properties.Resources.RecepcionReporteAuxiliarInventario_MsgCorralRequerido;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }




        /// <summary>
        /// Valida los datos del corral
        /// </summary>
        private void VerificaCorral(object sender, EventArgs e)
        {
            var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;
            var reporteAuxilizarInventarioPL = new ReporteAuxilizarInventarioPL();
            CorralInfo corral = Contexto.CorralInfo;
            Contexto =
                reporteAuxilizarInventarioPL.ObtenerDatosCorral(corral.Codigo, organizacion.OrganizacionID);
            if (Contexto.LoteID == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteAuxiliarInventario_LoteActivo,
                                  MessageBoxButton.OK, MessageImage.Warning);
                skAyudaLote.LimpiarCampos();
                skAyudaLote.AsignarFoco();
                return;
            }
            Contexto.CorralInfo = corral;
        }

        /// <summary>
        /// Obtiene los tipos de corral
        /// </summary>
        private void ObtenerGrupoCorral()
        {
            try
            {
                var grupoCorralPL = new GrupoCorralPL();
                IList<GrupoCorralInfo> gruposCorral = grupoCorralPL.ObtenerTodos();
                if (gruposCorral != null && gruposCorral.Any())
                {
                    GruposCorral = gruposCorral.Where(grupo => (GrupoCorralEnum)grupo.GrupoCorralID == GrupoCorralEnum.Enfermeria
                                                            || (GrupoCorralEnum)grupo.GrupoCorralID == GrupoCorralEnum.Produccion
                                                            || (GrupoCorralEnum)grupo.GrupoCorralID == GrupoCorralEnum.Recepcion).
                        OrderBy(
                            descripcion => descripcion.Descripcion).ToList();
                    if (GruposCorral.Any())
                    {
                        var grupoCorral = new GrupoCorralInfo
                                             {
                                                 GrupoCorralID = 0,
                                                 Descripcion = Properties.Resources.cbo_Seleccione
                                             };
                        GruposCorral.Insert(0, grupoCorral);
                        //cmbGrupoCorral.ItemsSource = GruposCorral;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteAuxiliarInventario_MsgErrorObtenerTiposCorral,
                                  MessageBoxButton.OK, MessageImage.Error);
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
                    Contexto.CorralInfo.Organizacion = organizacion0;
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

        #endregion

        private void CmbOrganizacion_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LimpiarCampos(false);
            //cmbGrupoCorral.SelectedIndex = 0;
            if (cmbOrganizacion.SelectedIndex > 0)
            {
                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;
                Contexto.CorralInfo.Organizacion = organizacion;
            }
        }


        private void txtCorral_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Contexto.CorralInfo.Organizacion = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
                if (Contexto.CorralInfo.Organizacion == null || Contexto.CorralInfo.Organizacion.OrganizacionID == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.ReporteConsumoCorral_SeleccioneOrganizacion,
                                        MessageBoxButton.OK, MessageImage.Error);

                    cmbOrganizacion.Focus();
                }
                else
                {
                    var corralPL = new CorralPL();

                    var corral = corralPL.ObtenerCorralPorCodigo(Contexto.CorralInfo.Organizacion.OrganizacionID, Contexto.CorralInfo.Codigo);
                    Contexto.CorralInfo = corral;
                    Contexto.LoteInfo.CorralID = Contexto.CorralInfo.CorralID;
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

        private void txtCorral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                //Contexto.Lote = new LoteInfo { OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario() };
                Contexto.CorralInfo.CorralID = 0;
                Contexto.LoteInfo = new LoteInfo();
            }
        }
    }
}
