using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Services.Info.Constantes;
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.PL;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using System.Reflection;
using System.Windows.Media;
using System.Configuration;
using System.Linq;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Interaction logic for CorteGanadoBusqueda.xaml
    /// </summary>
    public partial class CorteGanadoBusqueda : Window
    {
        #region Constructor
        private List<EntradaGanadoInfo> _entradaGanado;
        private int organizacionID;

        public List<EntradaGanadoInfo> ListaEntradaGanado
        {
            get { return _entradaGanado; }
            set { _entradaGanado = value; }
        }
        public CorteGanadoBusqueda()
        {
            InitializeComponent();
            organizacionID = Convert.ToInt32(Application.Current.Properties["OrganizacionID"]);
        }
        internal void InicializaPaginador()
        {
            try
            {
                PaginacionPartidas.DatosDelegado += ObtenerPartidas;
                PaginacionPartidas.AsignarValoresIniciales();
                ObtenerPartidas(PaginacionPartidas.Inicio, PaginacionPartidas.Limite);
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    ex.ToString(), MessageBoxButton.OK, SuKarne.Controls.Enum.MessageImage.Error);
            }
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento cerrar pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Evento cargar ayuda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
        }
        /// <summary>
        /// Evento pintar row en grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridResultado_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //TODO: Revisar la implementacion de lectura de valores de configuracion
            var numDiasEntradaParaCorte = int.Parse(ConfigurationManager.AppSettings["numDiasEntradaParaCorte"]);
            var valor = (EntradaGanadoInfo)e.Row.Item;
            var tiempo = DateTime.Now - valor.FechaEntrada;
            //Todos los renglones donde la fecha de entrada sea > 3 dias con la actual se pinta en rojo
            e.Row.Background = tiempo.Days > numDiasEntradaParaCorte ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
        }
        /// <summary>
        /// Evento seleccionar un registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarPartina();
        }

        private void GridResultado_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SeleccionarPartina();
        }

        private void GridResultado_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SeleccionarPartina();
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo para seleccionar la partida seleccionado
        /// </summary>
        private void SeleccionarPartina()
        {
            try
            {

                if (GridResultado.SelectedIndex == -1)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_BusquedaSeleccionar,
                                      MessageBoxButton.OK,
                                      SuKarne.Controls.Enum.MessageImage.Warning);
                }
                else
                {
                    ListaEntradaGanado = new List<EntradaGanadoInfo>();

                    var row = (DataGridRow)GridResultado.ItemContainerGenerator.ContainerFromIndex(GridResultado.SelectedIndex);
                    if (row.IsSelected)
                    {
                        var listaAyuda = new EntradaGanadoInfo();
                        listaAyuda = ((EntradaGanadoInfo)(row.Item));
                        ListaEntradaGanado.Add(listaAyuda);
                    }

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

        private IList<EntradaGanadoInfo> agruparGrid(IList<EntradaGanadoInfo> listaEntrada)
        {
            var listaResultado = new List<EntradaGanadoInfo>();
            bool encontrado;
            var datosAgrupados = new HashSet<EntradaGanadoCodigoAgrupado>();
            EntradaGanadoCodigoAgrupado datoAgrupado;
            foreach (var entradaGanadoInfo in listaEntrada)
            {
                encontrado = false;
                foreach (var tmpEntradaGanadoInfo in listaResultado)
                {
                    if (entradaGanadoInfo.CodigoCorral == tmpEntradaGanadoInfo.CodigoCorral)
                    {
                        encontrado = true;

                        tmpEntradaGanadoInfo.FolioEntradaAgrupado += " | " + entradaGanadoInfo.FolioEntrada;
                        tmpEntradaGanadoInfo.OrganizacionOrigenAgrupado += " | " + entradaGanadoInfo.OrganizacionOrigen;
                        tmpEntradaGanadoInfo.TipoOrigenAgrupado += " | " + entradaGanadoInfo.TipoOrigen;

                        tmpEntradaGanadoInfo.CabezasRecibidasAgrupadas += entradaGanadoInfo.CabezasRecibidas;
                        tmpEntradaGanadoInfo.EsAgrupado = true;
                    }
                    datoAgrupado = new EntradaGanadoCodigoAgrupado
                    {
                        OrganizacionID = entradaGanadoInfo.OrganizacionOrigenID,
                        FolioEntrada = entradaGanadoInfo.FolioEntrada,
                        TipoOrigenID = entradaGanadoInfo.TipoOrigen,
                        EntradaGanadoID = entradaGanadoInfo.EntradaGanadoID,
                        EmbarqueID = entradaGanadoInfo.EmbarqueID,
                        FolioOrigen = entradaGanadoInfo.FolioOrigen
                    };
                    datosAgrupados.Add(datoAgrupado);
                }
                if (!encontrado)
                {
                    entradaGanadoInfo.FolioEntradaAgrupado = entradaGanadoInfo.FolioEntrada.ToString();
                    entradaGanadoInfo.OrganizacionOrigenAgrupado = entradaGanadoInfo.OrganizacionOrigen;
                    entradaGanadoInfo.CabezasRecibidasAgrupadas = entradaGanadoInfo.CabezasRecibidas;
                    entradaGanadoInfo.TipoOrigenAgrupado = entradaGanadoInfo.TipoOrigen.ToString();

                    listaResultado.Add(entradaGanadoInfo);
                }
            }
            listaResultado.ForEach(datos =>
                                       {
                                           datos.EntradaGanadoCodigoAgrupados =
                                               datosAgrupados.Where(
                                                   embarqueID => embarqueID.EmbarqueID == datos.EmbarqueID).ToList();
                                       });
            return listaResultado;
        }

        /// <summary>
        /// Metodo para obtener corrales con sus lotes activos
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerPartidas(int inicio, int limite)
        {
            try
            {
                var entradaPL = new EntradaGanadoPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<EntradaGanadoInfo> resultadoBusqueda = entradaPL.ObtenerPartidasProgramadasPorPaginas(pagina,int.MinValue,
                    organizacionID);
                if (resultadoBusqueda != null && resultadoBusqueda.Lista != null &&
                    resultadoBusqueda.Lista.Count > 0)
                {
                    GridResultado.ItemsSource = agruparGrid(resultadoBusqueda.Lista);
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
                    "Ocurrio un error al consultar las partidas", MessageBoxButton.OK, SuKarne.Controls.Enum.MessageImage.Error);
            }
        }
        #endregion

       

        

       

       
    }
}
