using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para EntradaGanadoEnfermeriaBusqueda.xaml
    /// </summary>
    public partial class EntradaGanadoEnfermeriaBusqueda
    {
        #region Constructor

        private AnimalDeteccionInfo animalEnfermeriaSeleccionado;
        private EnfermeriaInfo corralSeleccionado;
        private int organizacionID;
        private string rutaBaseFotosDeteccion;
        public bool Seleccionado { get; set; }
        public AnimalDeteccionInfo AnimalEnfermeria
        {
            get { return animalEnfermeriaSeleccionado; }
            set { animalEnfermeriaSeleccionado = value; }
        }

        public EnfermeriaInfo Corral
        {
            get { return corralSeleccionado; }
            set { corralSeleccionado = value; }
        }
        public EntradaGanadoEnfermeriaBusqueda()
        {
            InitializeComponent();
            organizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]);
            ObtenerConfiguracion();
        }
        #endregion

        #region Eventos
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Evento click de boton selecionar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            if (animalEnfermeriaSeleccionado != null)
            {
                Seleccionado = true;
                Close();
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_PreguntaSeleccionBusqueda,
                                     MessageBoxButton.OK,
                                     MessageImage.Warning);
            }
        }
        /// <summary>
        /// evento click del boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(
                       Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_BusquedaPreguntaCancelar,
                       MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {

                animalEnfermeriaSeleccionado = null;
                AnimalEnfermeria = null;
                Seleccionado = false;
                Close();
            }

        }
        /// <summary>
        /// Evento cambio de seleccion del subgrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubGridResultado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var gridEnferma = (DataGrid)sender;
                if (gridEnferma.SelectedIndex >= 0)
                {
                    animalEnfermeriaSeleccionado = ((AnimalDeteccionInfo)gridEnferma.SelectedItem);
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Evento cambio de seleccion de grid principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridResultado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var gridPartidas = (DataGrid)sender;
                if (gridPartidas.SelectedIndex >= 0)
                {
                    corralSeleccionado = ((EnfermeriaInfo)gridPartidas.SelectedItem);
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Evento para el manejo del renglon de detalle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridResultado_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            var row = e.Row;
            FrameworkElement tb = GetTemplateChildByName(row, "RowHeaderToggleButton");
            if (tb != null)
            {
                if (row.DetailsVisibility == Visibility.Visible)
                {
                    (tb as ToggleButton).IsChecked = true;
                }
                else
                {
                    (tb as ToggleButton).IsChecked = false;
                }
            }


        }
        /// <summary>
        /// Clic del boton que contrae el renglon detalle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject obj = (DependencyObject)e.OriginalSource;
            while (!(obj is DataGridRow) && obj != null) obj = VisualTreeHelper.GetParent(obj);
            if (obj is DataGridRow)
            {
                if ((obj as DataGridRow).DetailsVisibility == Visibility.Visible)
                {
                    (obj as DataGridRow).IsSelected = false;
                }
                else
                {
                    (obj as DataGridRow).IsSelected = true;
                }
            }
        }
        /// <summary>
        /// Eventos para el manejo del renglon de detalle
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FrameworkElement GetTemplateChildByName(DependencyObject parent, string name)
        {
            int childnum = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childnum; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is FrameworkElement &&

                        ((FrameworkElement)child).Name == name)
                {
                    return child as FrameworkElement;
                }
                else
                {
                    var s = GetTemplateChildByName(child, name);
                    if (s != null)
                        return s;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Evento que controla el enter en el subgrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubGridResultado_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SeleccionarPartina((DataGrid)sender);
            }

        }

        #endregion

        #region Metodos
        /// <summary>
        /// Inicializa el paginador del grid
        /// </summary>
        internal void InicializaPaginador()
        {
            try
            {
                PaginacionPartidas.DatosDelegado += ObtenerPartidasConCabezasEnfermas;
                PaginacionPartidas.AsignarValoresIniciales();
                Seleccionado = false;
                ObtenerPartidasConCabezasEnfermas(PaginacionPartidas.Inicio, PaginacionPartidas.Limite);
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    ex.ToString(), MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Metodo para obtener la configuracion necesaria
        /// </summary>
        private void ObtenerConfiguracion()
        {
            var parametrosPl = new ConfiguracionParametrosPL();
            var parametroSolicitado = new ConfiguracionParametrosInfo
            {
                TipoParametro = (int)TiposParametrosEnum.Imagenes,
                OrganizacionID = organizacionID
            };
            var parametros = parametrosPl.ObtenerPorOrganizacionTipoParametro(parametroSolicitado);

            rutaBaseFotosDeteccion = parametros[0].Valor;
        }
        /// <summary>
        /// Metodo para obtener las partidas con cabezas enfermas
        /// </summary>
        /// <param name="inicio">Numero de renglon inicial</param>
        /// <param name="limite">Numero de renglon final</param>
        private void ObtenerPartidasConCabezasEnfermas(int inicio, int limite)
        {
            try
            {
                var enfermeriaPl = new EnfermeriaPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<EnfermeriaInfo> resultadoBusqueda = enfermeriaPl.ObtenerCorralesConGanadoDetectadoEnfermo(organizacionID, pagina);
                if (resultadoBusqueda != null && resultadoBusqueda.Lista != null &&
                    resultadoBusqueda.Lista.Count > 0)
                {
                    GridResultado.ItemsSource = resultadoBusqueda.Lista;
                    PaginacionPartidas.TotalRegistros = resultadoBusqueda.TotalRegistros;
                }
                else
                {
                    PaginacionPartidas.TotalRegistros = 0;
                    GridResultado.ItemsSource = new List<EntradaGanadoInfo>();
                }
                GridResultado.SelectedIndex = -1;

            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_ErrorBusqueda, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Selecciona una partida
        /// </summary>
        /// <param name="gridEnferma"></param>
        private void SeleccionarPartina(DataGrid gridEnferma)
        {
            try
            {

                if (gridEnferma.SelectedIndex == -1)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_BusquedaSeleccionar,
                                      MessageBoxButton.OK,
                                      MessageImage.Warning);
                }
                else
                {
                    
                    animalEnfermeriaSeleccionado = ((AnimalDeteccionInfo)gridEnferma.SelectedItem);
                    Close();
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        #endregion


    }
}
