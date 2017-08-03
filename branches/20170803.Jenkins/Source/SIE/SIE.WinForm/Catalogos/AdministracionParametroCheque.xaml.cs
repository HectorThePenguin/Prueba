using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para AdministracionParametroCheque.xaml
    /// </summary>
    public partial class AdministracionParametroCheque 
    {
        
        #region Propiedades

        /// <summary>
        /// Contexto de la aplicacion
        /// </summary>
        private CatParametroBancoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CatParametroBancoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor principal
        /// </summary>
        public AdministracionParametroCheque()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CatParametroBancoInfo { Activo = EstatusEnum.Activo };
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Carga de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlBase_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerParametroChequePorBanco;
                ucPaginacion.AsignarValoresIniciales();
                cmbTipoParametro.SelectedIndex = 1;
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionParametroCheque_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Accion del boton buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Validacion para solo letras de los campos de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSoloLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetras(e.Text);
        }

        /// <summary>
        /// Accion del boton nuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var edicionParametroCheque = new AdministracionParametroChequeEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.ParametroChequeRegistrar_Titulo }
                };
                MostrarCentrado(edicionParametroCheque);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionParametroCheque_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Accion del boton editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var parametroChequeSeleccionado = (CatParametroBancoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (parametroChequeSeleccionado != null)
                {
                    var bancoEdicion = new AdministracionParametroChequeEdicion(parametroChequeSeleccionado)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.ParametroChequeEdicion_Titulo }
                    };
                    MostrarCentrado(bancoEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionParametroCheque_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        #endregion

        #region Métodos

        /// <summary>
        /// Obtienes los parametros del banco paginado
        /// </summary>
        /// <param name="inicio">inicio del paginado</param>
        /// <param name="limite">Total del registro</param>
        private void ObtenerParametroChequePorBanco(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }

                var administracionParametroCheque = new AdministracionParametroChequePL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                CatParametroBancoInfo filtro = ObtenerFiltros();
                ResultadoInfo<CatParametroBancoInfo> resultadoInfo = administracionParametroCheque.ObtenerParametroChequePaginado(pagina, filtro);
                if (resultadoInfo != null && resultadoInfo.Lista != null && resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<CatParametroBancoInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionParametroCheque_ErrorObtener, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionParametroCheque_ErrorObtener, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerParametroChequePorBanco(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtien el filtro para la busqueda
        /// </summary>
        /// <returns></returns>
        private CatParametroBancoInfo ObtenerFiltros()
        {
            try
            {
                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Reinicia el paginado del grid
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion
        
    }
}