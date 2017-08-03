using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Catalogos;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Lógica de interacción para TarifarioGastosFijos.xaml
    /// </summary>
    public partial class TarifarioGastosFijos
    {
        #region Propiedades
        
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TarifarioInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TarifarioInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion Propiedades

        #region Contructor

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        public TarifarioGastosFijos()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor para ver detalle de gastos fijos
        /// </summary>
        /// <param name="tarifarioInfo"></param>
        public TarifarioGastosFijos(TarifarioInfo tarifarioInfo)
        {
            InitializeComponent();
            tarifarioInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = tarifarioInfo;
        }

        #endregion Constructor

        #region Eventos
       
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionDeGastosFijos_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionDeGastosFijos_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para regresar a la pantalla anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion Eventos

        #region Metodos

        /// <summary>
        /// Contexto
        /// </summary>
        private TarifarioInfo ObtenerFiltros()
        {
            return Contexto;
        }
        
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new TarifarioInfo();
            {
            };
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaGastosFijos()
        {
            try
            {
                var administracionDeGastosFijosPL = new AdministracionDeGastosFijosPL();
                TarifarioInfo filtro = ObtenerFiltros();
                ResultadoInfo<AdministracionDeGastosFijosInfo> resultadoInfo = administracionDeGastosFijosPL.ObtenerTodos(filtro);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridGastosFijos.ItemsSource = resultadoInfo.Lista;
                    var sumarAlTotal = resultadoInfo.Lista.Sum(x => x.Importe);
                    Total.Content = sumarAlTotal;
                }
                else
                {
                    gridGastosFijos.ItemsSource = new List<Condicion>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionDeGastosFijos_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionDeGastosFijos_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaGastosFijos();
        }

        #endregion Metodos

    }
}
