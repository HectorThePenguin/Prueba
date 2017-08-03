using System;                      
using System.Collections.Generic;  
using System.Linq;                 
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
    /// Lógica de interacción para AdministracionDeGastosFijos.xaml
    /// </summary>
    public partial class AdministracionDeGastosFijos
    {
        #region Propiedades
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private AdministracionDeGastosFijosInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AdministracionDeGastosFijosInfo)DataContext;
            }
            set { DataContext = value; }
        }
        #endregion
        
        
        #region Constructores
        /// <summary>
        /// Inicializa el componente de administracion de gastos fijos
        /// </summary>
        public AdministracionDeGastosFijos()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new AdministracionDeGastosFijosInfo()
            {
                Descripcion = string.Empty,
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
            };
        }
        #endregion
        
        
        #region Eventos
        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtValidarSoloLetrasYNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        /// <summary>
        /// Evento para Cargar la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CargarCboEstatus();
                ucPaginacion.DatosDelegado += ObtenerListaGastosFijos;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
                txtDescripcion.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlmacenUsuario_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlmacenUsuario_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento para mandar a llamar buscar
        /// </summary>
        /// <returns></returns>
        public void BtnBuscarClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Evento para el creado de un nuevo Gasto Fijo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevoClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var gastoFijoEdicion = new AdministracionDeGastosFijosEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.AdministracionDeGastosFijosEdicion_TituloNuevo }
                };
                MostrarCentrado(gastoFijoEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.AdministracionDeGastosFijos_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditarClick(object sender, RoutedEventArgs e)
        {
            var btnEditar = (Button) e.Source;
            try
            {
                var gastoInfoSelecionado = (AdministracionDeGastosFijosInfo) Extensor.ClonarInfo(btnEditar.CommandParameter);
                if (gastoInfoSelecionado != null)
                {
                    var gastosEdicion = new AdministracionDeGastosFijosEdicion(gastoInfoSelecionado)
                    {
                        ucTitulo = {TextoTitulo = Properties.Resources.AdministracionDeGastosFijosEdicion_TituloEdicion}
                    };
                    MostrarCentrado(gastosEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AdministracionDeGastosFijos_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion


        #region Métodos
        
        /// <summary>
        /// Carga los valores del combo de estatus
        /// </summary>
        private void CargarCboEstatus()
        {
            try
            {
                IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
                cboEstatus.ItemsSource = estatusEnums;
                cboEstatus.SelectedItem = EstatusEnum.Activo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaGastosFijos(int inicio, int limite)
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

                var administracionDeGastosFijosPL = new AdministracionDeGastosFijosPL();
                AdministracionDeGastosFijosInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<AdministracionDeGastosFijosInfo> resultadoInfo = administracionDeGastosFijosPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridGastosFijos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    ucPaginacion.AsignarValoresIniciales();
                    gridGastosFijos.ItemsSource = new List<AdministracionDeGastosFijos>();
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
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private AdministracionDeGastosFijosInfo ObtenerFiltros()
        {
            return Contexto;
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaGastosFijos(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Reinicia el paginador
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion
    }
}