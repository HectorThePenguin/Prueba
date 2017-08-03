using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Modelos;
using SIE.WinForm.Controles.Ayuda;
using SIE.WinForm.Auxiliar;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Interaction logic for AlimentacionConsumoCorral.xaml
    /// </summary>
    public partial class AlimentacionConsumoCorral
    {

        /// <summary>
        /// Contenedor de la clase
        /// </summary>
        private AlimentacionConsumoCorralModel Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AlimentacionConsumoCorralModel)DataContext;
            }
            set { DataContext = value; }
        }

        #region Variables
        SKAyuda<LoteInfo> ayudaLote;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AlimentacionConsumoCorral()
        {
            
            InitializeComponent();
            InicializaContexto();
            AgregarAyudaLote();
            CargarOrganizaciones();
            
            
        }
        #endregion

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

        #region Eventos
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Contexto.EnviarMensaje += Contexto_EnviarMensaje;
            Contexto.MostrarBotones = Visibility.Hidden;
            txtCorral.Focus();
        }

        void Contexto_EnviarMensaje(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new EventHandler((s, ea) =>
            {
                dynamic x = s;
                MessageBoxResult result =
                    SkMessageBox.Show(
                        Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        x.mensaje,
                        x.boton,
                        x.imagen
                        );
                if (result == MessageBoxResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(Contexto.RutaArchivo))
                    {
                        Process.Start(Contexto.RutaArchivo);
                        Contexto.RutaArchivo = string.Empty;
                    }
                }
                if (result == MessageBoxResult.Yes)
                {
                    Contexto.LimpiarCancelar();
                    MoveFocus(
                        new System.Windows.Input.TraversalRequest(System.Windows.Input.FocusNavigationDirection.First));
                }
            }), sender, e);
        }

        /// <summary>
        /// Evento para cuando se borra del control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
           {
                Contexto.Lote = new LoteInfo { OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario() };
                Contexto.Corral.CorralID = 0;
            }
        }

        /// <summary>
        /// Evento para cuando el control pierde el foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
            {
                Contexto.MostrarMensaje(Properties.Resources.ReporteConsumoCorral_SeleccioneOrganizacion,
                    MessageBoxButton.OK, MessageImage.Error);

                cmbOrganizacion.Focus();
                return;
            }
            else
            {
                ayudaLote.IsEnabled = false;
                Contexto.TraerCorral(Contexto.Organizacion.OrganizacionID);
                ayudaLote.Info = Contexto.Lote;
                ayudaLote.IsEnabled = true;
            }
        }
        private void ButtonLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Contexto.Limpiar();
            cmbOrganizacion.SelectedIndex = 0;
            MoveFocus(new System.Windows.Input.TraversalRequest(System.Windows.Input.FocusNavigationDirection.First));
        }
        private void ButtonGenerar_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var button = (sender as Button);
            if (button.IsEnabled)
            {
                button.Focus();
            }
        }
        private void ButtonGenerar_Click(object sender, RoutedEventArgs e)
        {
            //var model = Contexto;
            //EjecutarBackground(() => model.GenerarReporte(), () => Contexto.EstaOcupado = false);
            //Contexto.EstaOcupado = true;
            var organizacion = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
            Contexto.GenerarReporte(organizacion.OrganizacionID);
        }

        private void ButtonExportar_Click(object sender, RoutedEventArgs e)
        {
            //var model = Contexto;
            //EjecutarBackground(() => model.ExportarReporte(), () => Contexto.EstaOcupado = false);
            //Contexto.EstaOcupado = true;
            Contexto.ExportarReporte();
        }

        private void CancelarClick(object sender, RoutedEventArgs e)
        {
            Contexto.Cancelar();
        }

        private void Organizacion_SelectionChaged(object sender, SelectionChangedEventArgs e)
        {
            var org = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
            if (org != null && org.OrganizacionID > 0 )
            {
                Contexto.Organizacion = (OrganizacionInfo) cmbOrganizacion.SelectedItem;
                txtCorral.IsEnabled = true;
                ayudaLote.IsEnabled = true;
            }
            else
            {
                cmbOrganizacion.Focus();
                txtCorral.IsEnabled = false;
                ayudaLote.IsEnabled = false;
                Contexto.Limpiar();
            }
        }
        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new AlimentacionConsumoCorralModel();
            Contexto.Limpiar();
        }

        
        private void AgregarAyudaLote()
        {
            ayudaLote = new SKAyuda<LoteInfo>(
                   60,
                   true,
                   Contexto.Lote,
                   "ClaveLotesPorCorral",
                   "DescripcionLotesPorCorral",
                   true,
                   true)
                   {
                       AyudaPL = new LotePL(),
                       MensajeAgregar = Properties.Resources.AyudaLote_Agregar,
                       MensajeBusqueda = Properties.Resources.AyudaLote_Busqueda,
                       MensajeBusquedaCerrar = Properties.Resources.AyudaLote_BusquedaCerrar,
                       MensajeClaveInexistente = Properties.Resources.AyudaLote_CodigoInvalido,
                       TituloEtiqueta = Properties.Resources.AyudaLote_Etiqueta,
                       TituloPantalla = Properties.Resources.AyudaLote_Pantalla,
                   };
            ayudaLote.PuedeBuscar = () =>
            {
                return Contexto.Corral.CorralID > 0;
            };
            ayudaLote.AyudaLimpia += (s, e) =>
                                         {
                                             Contexto.PuedeGenerarReporte = false;
                                         };
            ayudaLote.AyudaConDatos += (s, e) => Contexto.TraerLote();
            ayudaLote.MensajeNoPuedeBuscar = Properties.Resources.AyudaLote_MsgDependenciaOrganizacion;
            spAyudaLote.Children.Add(ayudaLote);
            ayudaLote.AsignaTabIndex(1);
        }
        #endregion

        
    }
}

