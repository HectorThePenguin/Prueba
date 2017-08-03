using System;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Collections.Generic;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para AdministracionRuteoEdicion.xaml
    /// </summary>
    public partial class AdministracionRuteoEdicion 
    {

        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private RuteoInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (RuteoInfo) DataContext;
                }
                set { DataContext = value; }
        }

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AdministracionRuteoEdicion ()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para ver el detalle de un ruteo existente
        /// </summary>
        /// <param name="ruteoInfo"></param>
        public AdministracionRuteoEdicion (RuteoInfo ruteoInfo)
        {
            InitializeComponent();
            ruteoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = ruteoInfo;
        }

        #endregion Constructores

        #region Eventos
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ucPaginacionDetalle.DatosDelegado += ObtenerListaRuteoDetalle;
            ucPaginacionDetalle.AsignarValoresIniciales();
            Buscar();
        }

        /// <summary>
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Valida la entrada de caracteres cuando se escriben en los cuadros de textos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDescripcion_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }
        #endregion Eventos

        #region Métodos
        private RuteoInfo ObtenerFiltros()
        {
            return Contexto;
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaRuteoDetalle(int inicio, int limite)
        {
            try
            {
                if (ucPaginacionDetalle.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacionDetalle.CompararObjetos(Contexto, ucPaginacionDetalle.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacionDetalle.Inicio = 1;
                        inicio = 1;
                    }
                }

                var ruteoPL = new AdministracionRuteoPL();
                RuteoInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<RuteoDetalleInfo> resultadoInfo = ruteoPL.ObtenerDetallePorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatosDetalle.ItemsSource = resultadoInfo.Lista;
                    ucPaginacionDetalle.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacionDetalle.TotalRegistros = 0;
                    ucPaginacionDetalle.AsignarValoresIniciales();
                    gridDatosDetalle.ItemsSource = new List<RuteoDetalleInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionRuteo_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionRuteo_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new RuteoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                OrganizacionOrigen = new OrganizacionInfo(),
                OrganizacionDestino = new OrganizacionInfo()
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        private void Buscar()
        {
            ObtenerListaRuteoDetalle(ucPaginacionDetalle.Inicio, ucPaginacionDetalle.Limite);
        }
       
        #endregion Métodos
    }
}
